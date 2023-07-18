using System;
using Delivery.Controladores;
using Delivery.Entidades;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Controladores
{
    public class pMenuItem
    {
        public static List<MenuItem> GetAll()
        {
            List<MenuItem> items = new List<MenuItem>();
            SQLiteCommand cmd = new SQLiteCommand("SELECT Id, Nombre, Descripcion, Precio FROM MenuItem;");
            cmd.Connection = Conexion.Connection;
            if (cmd.Connection.State != System.Data.ConnectionState.Open) // verifica si la conexión ya está abierta
            {
                cmd.Connection.Open();
            }
            SQLiteDataReader obdr = cmd.ExecuteReader();
            while (obdr.Read())
            {
                MenuItem item = new MenuItem();
                item.Id = obdr.GetInt32(0);
                item.Nombre = obdr.GetString(1);
                item.Descripcion = obdr.GetString(2);
                item.Precio = obdr.GetDouble(3);
                items.Add(item);
            }
            if (cmd.Connection.State != System.Data.ConnectionState.Closed) // verifica si la conexión ya está cerrada
            {
                cmd.Connection.Close();
            }
            return items;
        }


        public static List<MenuItem> GetByPedido(int idPedido)
        {
            List<MenuItem> items = new List<MenuItem>();
            SQLiteCommand cmd = new SQLiteCommand("SELECT IdMenuItem FROM PedidoMenuItem where IdPedido = @idPedido;");
            cmd.Parameters.Add(new SQLiteParameter("@idPedido", idPedido));
            cmd.Connection = Conexion.Connection;
            SQLiteDataReader obdr = cmd.ExecuteReader();
            while (obdr.Read())
            {
                items.Add(GetById(obdr.GetInt32(0)));
            }
            return items;
        }

        public static void Save(MenuItem m)
        {
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO MenuItem (Nombre, Descripcion, Precio) VALUES (@nombre, @descripcion, @precio);");
            cmd.Connection = Conexion.Connection;
            if (cmd.Connection.State != System.Data.ConnectionState.Open) // verifica si la conexión ya está abierta
            {
                cmd.Connection.Open();
            }
            cmd.Parameters.Add(new SQLiteParameter("@nombre", m.Nombre));
            cmd.Parameters.Add(new SQLiteParameter("@descripcion", m.Descripcion));
            cmd.Parameters.Add(new SQLiteParameter("@precio", m.Precio));
            cmd.ExecuteNonQuery();
            if (cmd.Connection.State != System.Data.ConnectionState.Closed) // verifica si la conexión ya está cerrada
            {
                cmd.Connection.Close();
            }
            Program.menuItems = GetAll();
        }

        public static void Delete(MenuItem m)
        {
            SQLiteCommand cmd = new SQLiteCommand("DELETE FROM MenuItem WHERE Id = @id;");
            cmd.Connection = Conexion.Connection;
            cmd.Connection.Open();
            cmd.Parameters.Add(new SQLiteParameter("@id", m.Id));
            cmd.ExecuteNonQuery();
        }

        public static void Modify(MenuItem m)
        {
            SQLiteCommand cmd = new SQLiteCommand("UPDATE MenuItem SET Nombre = @nombre, Descripcion = @descripcion, Precio = @precio WHERE Id = @id;");
            cmd.Connection = Conexion.Connection;
            if (cmd.Connection.State != System.Data.ConnectionState.Open) // verifica si la conexión ya está abierta
            {
                cmd.Connection.Open();
            }
            cmd.Parameters.Add(new SQLiteParameter("@nombre", m.Nombre));
            cmd.Parameters.Add(new SQLiteParameter("@descripcion", m.Descripcion));
            cmd.Parameters.Add(new SQLiteParameter("@precio", m.Precio));
            cmd.Parameters.Add(new SQLiteParameter("@id", m.Id));
            cmd.ExecuteNonQuery();
            if (cmd.Connection.State != System.Data.ConnectionState.Closed) // verifica si la conexión ya está cerrada
            {
                cmd.Connection.Close();
            }
            Program.menuItems = GetAll();

        }


        public static MenuItem GetById(int id)
        {
            MenuItem m = new MenuItem();
            SQLiteCommand cmd = new SQLiteCommand("SELECT Id, Nombre, Descripcion, Precio FROM MenuItem Where Id = @id;");
            cmd.Parameters.Add(new SQLiteParameter("@id", id));
            cmd.Connection = Conexion.Connection;
            SQLiteDataReader obdr = cmd.ExecuteReader();
            while (obdr.Read())
            {
                m.Id = obdr.GetInt32(0);
                m.Nombre = obdr.GetString(1);
                m.Descripcion = obdr.GetString(2);
                m.Precio = obdr.GetDouble(3);
            }
            return m;
        }



    }
}
