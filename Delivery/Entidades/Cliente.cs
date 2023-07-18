using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Entidades
{
    public class Cliente
    {
        int idCliente;
        string nombre;
        string apellido;
        string direccion;

        public int IdCliente { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }

        public Cliente()
        { }

        public Cliente(int idCliente, string nombre, string apellido, string direccion)
        {
            this.idCliente = idCliente;
            this.nombre = nombre;
            this.apellido = apellido;
            this.direccion = direccion;
        }
    }

}
