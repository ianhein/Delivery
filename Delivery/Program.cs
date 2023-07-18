using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Entidades;
using Delivery.Controladores;
using Delivery.Varios;
using System.Threading;

namespace Delivery
{
    class Program
    {
        public static List<Cliente> clientes = new List<Cliente>();
        public static List<MenuItem> menuItems = new List<MenuItem>();
        public static List<Pedido> pedidos = new List<Pedido>();
        static void Main(string[] args)
        {

            Conexion.OpenConexion();
            clientes = pCliente.GetAll();
            menuItems = pMenuItem.GetAll();
            pedidos = pPedido.GetAll();
            Menu();

        }

        public static void Menu()
        {
            Console.Clear();
            string[] opciones = new string[4];
            opciones[0] = "Clientes";
            opciones[1] = "Menu";
            opciones[2] = "Pedidos";
            opciones[3] = "Salir";

            Herramientas.DibujoMenu("Delivery", opciones);
            Console.WriteLine("Ingrese una opcion: ");
            int opcion = Herramientas.IngresoEnteros(1, 4);

            switch (opcion)
            {
                case 1:
                    nCliente.Menu();
                    Menu();
                    break;
                case 2:
                    nMenuItem.Menu();
                    Menu();
                    break;
                case 3:
                    nPedido.Menu();
                    Menu();
                    break;
                case 4:
                    break;
            }
        }
    }
}




