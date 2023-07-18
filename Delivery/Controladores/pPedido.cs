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

                // Leer la fecha como cadena y convertirla a un DateTime
                string fechaString = obdr.GetString(1);
                DateTime fecha;
                try
                {
                    fecha = DateTime.ParseExact(fechaString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                    p.Fecha = fecha;
                }
                catch (FormatException)
                {
                    Console.WriteLine("La fecha en la base de datos no tiene el formato correcto: " + fechaString);
                    continue;  // Pasar al siguiente registro
                }

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
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Pedido(Fecha,IdCliente,MontoTotalComprado) VALUES(@fecha,@idCliente,@montoTotalComprado)");
            cmd.Connection = Conexion.Connection;
            if (cmd.Connection.State != System.Data.ConnectionState.Open) // verifica si la conexión ya está abierta
            {
                cmd.Connection.Open();
            }

            cmd.Parameters.Add(new SQLiteParameter("@fecha", p.Fecha.ToString("yyyy-MM-dd HH:mm:ss")));
            cmd.Parameters.Add(new SQLiteParameter("@idCliente", p.Cliente.IdCliente));
            cmd.Parameters.Add(new SQLiteParameter("@montoTotalComprado", p.MontoTotal));

            // Vamos a imprimir los valores que estamos intentando guardar
            Console.WriteLine($"Guardar pedido: Fecha={p.Fecha}, IdCliente={p.Cliente.IdCliente}, MontoTotal={p.MontoTotal}");

            int affectedRows = cmd.ExecuteNonQuery();

            // Vamos a imprimir cuantas filas fueron afectadas por el comando SQL
            Console.WriteLine($"Filas afectadas: {affectedRows}");

            foreach (MenuItem m in p.Items)
            {
                SavePedidoItem(p, m);
            }

            if (cmd.Connection.State != System.Data.ConnectionState.Closed) // verifica si la conexión ya está cerrada
            {
                cmd.Connection.Close();
            }

            Program.pedidos = GetAll();
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
            cmd.Connection = Conexion.Connection;
            if (cmd.Connection.State != System.Data.ConnectionState.Open) // verifica si la conexión ya está abierta
            {
                cmd.Connection.Open();
            }
            cmd.Parameters.Add(new SQLiteParameter("@id", p.Id));
            cmd.ExecuteNonQuery();
            if (cmd.Connection.State != System.Data.ConnectionState.Closed) // verifica si la conexión ya está cerrada
            {
                cmd.Connection.Close();
            }
            Program.pedidos = GetAll();
        }

        public static void Modify(Pedido p)
        {
            SQLiteCommand cmd = new SQLiteCommand("UPDATE Pedido SET Fecha = @fecha, MontoTotalComprado = @montoTotal WHERE Id = @id;");
            cmd.Connection = Conexion.Connection;
            if (cmd.Connection.State != System.Data.ConnectionState.Open) // verifica si la conexión ya está abierta
            {
                cmd.Connection.Open();
            }
            cmd.Parameters.Add(new SQLiteParameter("@fecha", p.Fecha));
            cmd.Parameters.Add(new SQLiteParameter("@montoTotal", p.MontoTotal));
            cmd.Parameters.Add(new SQLiteParameter("@id", p.Id));
            cmd.ExecuteNonQuery();
            if (cmd.Connection.State != System.Data.ConnectionState.Closed) // verifica si la conexión ya está cerrada
            {
                cmd.Connection.Close();
            }
            Program.pedidos = GetAll();
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

        public static List<Pedido> ListarPedidosEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            Conexion.OpenConexion();

            string fechaInicioStr = fechaInicio.ToString("yyyy-MM-dd HH:mm:ss");
            string fechaFinStr = fechaFin.ToString("yyyy-MM-dd HH:mm:ss");

            SQLiteCommand cmd = new SQLiteCommand("SELECT Id, Fecha, IdCliente, MontoTotalComprado FROM Pedido WHERE Fecha BETWEEN @fechaInicio AND @fechaFin ORDER BY Fecha;");
            cmd.Parameters.AddWithValue("@fechaInicio", fechaInicioStr);
            cmd.Parameters.AddWithValue("@fechaFin", fechaFinStr);

            cmd.Connection = Conexion.Connection;
            SQLiteDataReader obdr = cmd.ExecuteReader();

            List<Pedido> pedidos = new List<Pedido>();
            while (obdr.Read())
            {
                Pedido p = new Pedido();
                p.Id = obdr.GetInt32(0);
                p.Fecha = DateTime.ParseExact(obdr.GetString(1), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                p.Cliente = pCliente.GetById(obdr.GetInt32(2));
                p.MontoTotal = obdr.GetDouble(3);
                pedidos.Add(p);
            }

            Conexion.CloseConnection();
            return pedidos;
        }


    }
}
