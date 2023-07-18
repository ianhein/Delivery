using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Entidades
{
    public class MenuItem
    {
        int id;
        string nombre;
        string descripcion;
        double precio;

        public MenuItem()
        { }

        public MenuItem(int id, string nombre, string descripcion, double precio)
        {
            this.id = id;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.precio = precio;
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public double Precio { get => precio; set => precio = value; }
    }
}
