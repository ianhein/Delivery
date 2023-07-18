using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Controladores;
using Delivery.Entidades;
using Delivery.Varios;

namespace Delivery.Controladores
{
    public class nPedido
    {
        public static void Crear()
        {
            bool creacionExitosa = false;
            do
            {
                try
                {
                    Pedido p = new Pedido();
                    MenuItem m = new MenuItem();

                    Console.WriteLine("Ingrese la fecha del pedido: ");
                    try
                    {
                        p.Fecha = DateTime.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Fecha no válida. Por favor, ingrese una fecha en el formato correcto.");
                        continue;
                    }

                    Console.WriteLine("Ingrese el cliente del pedido: ");
                    p.Cliente = Program.clientes[nCliente.Seleccionar()];

                    Console.WriteLine("Ingrese el monto total del pedido: ");
                    try
                    {
                        p.MontoTotal = double.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Monto total no válido. Por favor, ingrese un número.");
                        continue;
                    }

                    bool maspedidos = false;
                    do
                    {
                        maspedidos = false;
                        Console.WriteLine("Ingrese el item del pedido: ");
                        int mt = nMenuItem.Seleccionar();
                        pPedido.SavePedidoItem(p, Program.menuItems[mt]);

                        Console.WriteLine("Desea agregar otro item? (S/N)");
                        string resp = Console.ReadLine();
                        if (resp.ToUpper() == "S")
                        {
                            maspedidos = true;
                        }
                    } while (maspedidos);

                    creacionExitosa = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ha ocurrido un error al crear el pedido: " + ex.Message);
                }
            } while (!creacionExitosa);
        }



        public static void Listar()
        {
            Console.Clear();
            string[,] tabla = new string[Program.pedidos.Count + 1, 4];
            tabla[0, 0] = "Id";
            tabla[0, 1] = "Fecha";
            tabla[0, 2] = "Cliente";
            tabla[0, 3] = "Monto Total";

            foreach (var item in Program.pedidos)
            {
                tabla[Program.pedidos.IndexOf(item) + 1, 0] = (Program.pedidos.IndexOf(item) + 1).ToString();
                tabla[Program.pedidos.IndexOf(item) + 1, 1] = item.Fecha.ToString();
                tabla[Program.pedidos.IndexOf(item) + 1, 2] = item.Cliente.Nombre + " " + item.Cliente.Apellido;
                tabla[Program.pedidos.IndexOf(item) + 1, 3] = "$ " + item.MontoTotal.ToString();
            }
            Herramientas.DibujaTabla(tabla);
            Console.ReadKey();
        }

        public static int Seleccionar()
        {
            int i = 0;
            Listar();
            Console.WriteLine("Ingrese el id del pedido: ");
            i = Herramientas.IngresoEnteros(1, Program.pedidos.Count);
            return i - 1;
        }

        public static void Eliminar()
        {
            try
            {
                int i = Seleccionar();
                pPedido.Delete(Program.pedidos[i]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ha ocurrido un error al eliminar el item: " + ex.Message);
                Eliminar();
            }
        }


        public static void Menu()
        {
            Console.Clear();
            string[] opciones = new string[4];
            opciones[0] = "Crear Pedido";
            opciones[1] = "Listar Pedidos";
            opciones[2] = "Eliminar Pedidos";
            opciones[3] = "Salir";

            Herramientas.DibujoMenu("Menu Pedidos", opciones);
            int op = Herramientas.IngresoEnteros(1, 5);

            switch (op)
            {
                case 1:
                    Crear();
                    break;
                case 2:
                    Listar();
                    Menu();
                    break;
                case 3:
                    Eliminar();
                    break;
                case 4:
                    break;
            }

        }
    }
}
