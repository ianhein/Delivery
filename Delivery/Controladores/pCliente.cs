using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Controladores;
using Delivery.Entidades;

namespace Delivery.Controladores
{
    class pCliente
    {
        public static List<Cliente> GetAll()
        {
            List<Cliente> clientes = new List<Cliente>();
            SQLiteCommand cmd = new SQLiteCommand("SELECT Id, Nombre, Apellido, Direccion FROM Cliente");
            cmd.Connection = Conexion.Connection;
            if (cmd.Connection.State != System.Data.ConnectionState.Open) // verifica si la conexión ya está abierta
            {
                cmd.Connection.Open();
            }
            SQLiteDataReader obdr = cmd.ExecuteReader();
            while (obdr.Read())
            {
                Cliente c = new Cliente();
                c.IdCliente = obdr.GetInt32(0);
                c.Nombre = obdr.GetString(1);
                c.Apellido = obdr.GetString(2);
                c.Direccion = obdr.GetString(3);
                clientes.Add(c);
            }
            if (cmd.Connection.State != System.Data.ConnectionState.Closed) // verifica si la conexión ya está cerrada
            {
                cmd.Connection.Close();
            }
            return clientes;
        }



        public static void Listar()
        {
            SQLiteCommand cmd = new SQLiteCommand("SELECT Id, Nombre, Apellido, Direccion FROM Cliente");
            cmd.Connection = Conexion.Connection;
            SQLiteDataReader obdr = cmd.ExecuteReader();
            while (obdr.Read())
            {
                Console.WriteLine($"Nombre: {obdr.GetString(1)}, Apellido: {obdr.GetString(2)}, Dirección: {obdr.GetString(3)}");
            }   
        }

        public static List<Cliente> ListarMayorMenor()
        {
            List<Cliente> clientes = new List<Cliente>();
            SQLiteCommand cmd = new SQLiteCommand("SELECT p.MontoTotalComprado, c.Nombre, c.Apellido FROM Pedido p JOIN Cliente c ON c.Id = p.IdCliente ORDER BY p.MontoTotalComprado DESC");
            cmd.Connection = Conexion.Connection;
            if (cmd.Connection.State != System.Data.ConnectionState.Open) // verifica si la conexión ya está abierta
            {
                cmd.Connection.Open();
            }
            SQLiteDataReader obdr = cmd.ExecuteReader();
            while (obdr.Read())
            {
                Cliente c = new Cliente();
                Pedido p = new Pedido();
                c.Nombre = obdr.GetString(1);
                c.Apellido = obdr.GetString(2);
                p.MontoTotal= obdr.GetDouble(0);

                Console.WriteLine($"Nombre: {obdr.GetString(0)}, Apellido: {obdr.GetString(1)}, MontoTotalComprado: {obdr.GetDouble(2)},");
                clientes.Add(c);

            }
            if (cmd.Connection.State != System.Data.ConnectionState.Closed) // verifica si la conexión ya está cerrada
            {
                cmd.Connection.Close();
            }
            return clientes;

        }

        public static void Crear(Cliente c)
        {

            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Cliente (Nombre, Apellido, Direccion) VALUES (@nombre, @apellido, @direccion);");
            cmd.Connection = Conexion.Connection;
            if (cmd.Connection.State != System.Data.ConnectionState.Open) // verifica si la conexión ya está abierta
            {
                cmd.Connection.Open();
            }
            cmd.Parameters.Add(new SQLiteParameter("@nombre", c.Nombre));
            cmd.Parameters.Add(new SQLiteParameter("@apellido", c.Apellido));
            cmd.Parameters.Add(new SQLiteParameter("@direccion", c.Direccion));
            cmd.ExecuteNonQuery();
            if (cmd.Connection.State != System.Data.ConnectionState.Closed) // verifica si la conexión ya está cerrada
            {
                cmd.Connection.Close();
            }
            Program.clientes = GetAll();
        }

        public static void Delete(Cliente c)
        {

            SQLiteCommand cmd = new SQLiteCommand("DELETE FROM Cliente WHERE Id = @id;");
            cmd.Connection = Conexion.Connection;
            if (cmd.Connection.State != System.Data.ConnectionState.Open) // verifica si la conexión ya está abierta
            {
                cmd.Connection.Open();
            }
            cmd.Parameters.Add(new SQLiteParameter("@id", c.IdCliente));
            cmd.ExecuteNonQuery();
            if (cmd.Connection.State != System.Data.ConnectionState.Closed) // verifica si la conexión ya está cerrada
            {
                cmd.Connection.Close();
            }
            Program.clientes = GetAll();

        }

        public static void Modify(Cliente C)
        {

            SQLiteCommand cmd = new SQLiteCommand("UPDATE Cliente SET Nombre = @nombre, Apellido = @apellido, Direccion = @direccion WHERE Id = @id;");
            cmd.Connection = Conexion.Connection;
            if (cmd.Connection.State != System.Data.ConnectionState.Open) // verifica si la conexión ya está abierta
            {
                cmd.Connection.Open();
            }
            cmd.Parameters.Add(new SQLiteParameter("@nombre", C.Nombre));
            cmd.Parameters.Add(new SQLiteParameter("@apellido", C.Apellido));
            cmd.Parameters.Add(new SQLiteParameter("@direccion", C.Direccion));
            cmd.Parameters.Add(new SQLiteParameter("@id", C.IdCliente));
            cmd.ExecuteNonQuery();
            if (cmd.Connection.State != System.Data.ConnectionState.Closed) // verifica si la conexión ya está cerrada
            {
                cmd.Connection.Close();
            }
            Program.clientes = GetAll();
        }


        public static Cliente GetById(int id)
        {
            Cliente c = new Cliente();
            SQLiteCommand cmd = new SQLiteCommand("SELECT Id, Nombre, Apellido, Direccion FROM Cliente where Id = @id;");
            cmd.Parameters.Add(new SQLiteParameter("@id", id));
            cmd.Connection = Conexion.Connection;
            SQLiteDataReader obdr = cmd.ExecuteReader();
            while (obdr.Read())
            {
                c.IdCliente = obdr.GetInt32(0);
                c.Nombre = obdr.GetString(1);
                c.Apellido = obdr.GetString(2);
                c.Direccion = obdr.GetString(3);
            }
            return c;
        }


        public static List<Cliente> buscarPorFrase(string frase)
        {
            List<Cliente> clientes = new List<Cliente>();
            SQLiteCommand cmd = new SQLiteCommand("SELECT codCliente, nombre, apellido, direccion FROM Cliente WHERE direccion LIKE @frase");
            cmd.Parameters.Add(new SQLiteParameter("@frase", "%" + frase + "%"));
            cmd.Connection = Conexion.Connection;
            SQLiteDataReader obdr = cmd.ExecuteReader();
            while (obdr.Read())
            {
                Cliente c = new Cliente();
                c.IdCliente = obdr.GetInt32(0);
                c.Nombre = obdr.GetString(1);
                c.Apellido = obdr.GetString(2);
                c.Direccion = obdr.GetString(3);
                clientes.Add(c);
            }
            return clientes;
        }



    }
}
