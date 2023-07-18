using Delivery.Controladores;
using Delivery.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Controladores
{
    public class pPedido
    {
        public static List<Pedido> GetAll()
        {
            Conexion.OpenConexion();
            List<Pedido> pedidos = new List<Pedido>();
            SQLiteCommand cmd = new SQLiteCommand("SELECT Id, Fecha, IdCliente, MontoTotalComprado FROM Pedido;");
            cmd.Connection = Conexion.Connection;
            SQLiteDataReader obdr = cmd.ExecuteReader();

            while (obdr.Read())
            {
                Pedido p = new Pedido();
                p.Id = obdr.GetInt32(0);
                p.Fecha = DateTime.ParseExact(obdr.GetString(1), "M/d/yyyy", CultureInfo.InvariantCulture);
                p.Cliente = pCliente.GetById(obdr.GetInt32(2));
                p.MontoTotal = obdr.GetDouble(3);
                Console.WriteLine($"Cliente: {p.Cliente.Nombre} {p.Cliente.Apellido}");


                pedidos.Add(p);
            }
            Conexion.CloseConnection();
            return pedidos;
        }

        public static void Save(Pedido p)
        {
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Pedido(Fecha,IdCliente,MontoTotal) VALUES(@fecha,@idCliente,@montoTotal)");
            cmd.Parameters.Add(new SQLiteParameter("@fecha", p.Fecha));
            cmd.Parameters.Add(new SQLiteParameter("@idCliente", p.Cliente.IdCliente));
            cmd.Parameters.Add(new SQLiteParameter("@montoTotalComprado", p.MontoTotal));
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();

            foreach (MenuItem m in p.Items)
            {
                SavePedidoItem(p, m);
            }
        }

        public static void SavePedidoItem(Pedido p, MenuItem m)
        {
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO PedidoItem(idPedido, idMenuItem) VALUES(@idPedido, @idMenuItem)");
            cmd.Parameters.Add(new SQLiteParameter("@idPedido", p.Id));
            cmd.Parameters.Add(new SQLiteParameter("@idMenuItem", m.Id));
            cmd.Connection = Conexion.Connection;

            cmd.ExecuteNonQuery();
        }

        public static void Delete(Pedido p)
        {
            SQLiteCommand cmd = new SQLiteCommand("DELETE FROM Pedido WHERE Id = @id;");
            cmd.Parameters.Add(new SQLiteParameter("@id", p.Id));
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();
        }

        public static void Modify(Pedido p, Cliente c)
        {
            p.Cliente = c; // Asignar el nuevo cliente al pedido
            SQLiteCommand cmd = new SQLiteCommand("UPDATE Pedido SET Fecha = @fecha, IdCliente = @idCliente, MontoTotal = @montoTotal WHERE Id = @id;");
            cmd.Parameters.Add(new SQLiteParameter("@fecha", p.Fecha));
            cmd.Parameters.Add(new SQLiteParameter("@idCliente", p.Cliente.IdCliente));
            cmd.Parameters.Add(new SQLiteParameter("@montoTotal", p.MontoTotal));
            cmd.Connection = Conexion.Connection;
            cmd.ExecuteNonQuery();
        }

        public static void Search(string frase)
        {
            SQLiteCommand cmd = new SQLiteCommand("SELECT Pedido.Id, Pedido.Fecha, Pedido.IdCliente, Pedido.MontoTotal, Cliente.Nombre, Cliente.Apellido, Cliente.Direccion FROM Pedido JOIN Cliente ON Pedido.IdCliente = Cliente.Id WHERE Cliente.Nombre LIKE @nombre OR Pedido.Nombre LIKE @nombre;");
            cmd.Parameters.Add(new SQLiteParameter("@nombre", "%" + frase + "%"));
            cmd.Connection = Conexion.Connection;
            SQLiteDataReader obdr = cmd.ExecuteReader();

            while (obdr.Read())
            {
                Cliente c = new Cliente();
                c.IdCliente = obdr.GetInt32(0);
                c.Nombre = obdr.GetString(1);
                c.Apellido = obdr.GetString(2);
                c.Direccion = obdr.GetString(3);

            }
        }

    }
}
