using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Entidades;

namespace Delivery.Entidades
{
    public class Pedido
    {
        int id;
        DateTime fecha;
        Cliente cliente;
        double montoTotal;

        public DateTime Fecha { get; set; }
        public Cliente Cliente { get; set; }
        public List<MenuItem> Items { get; set; }
        public double MontoTotal { get => montoTotal; set => montoTotal = value; }
        public int Id { get => id; set => id = value; }

        public Pedido()
        {
            Items = new List<MenuItem>();
        }

        public Pedido(int id, DateTime fecha, Cliente cliente, List<MenuItem> items, double montoTotal)
        {
            this.id = id;
            this.fecha = fecha;
            this.cliente = cliente;
            this.Items = items ?? new List<MenuItem>();
            this.montoTotal = montoTotal;
        }
    }

}
