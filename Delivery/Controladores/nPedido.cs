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
            Console.Clear();
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
                        p.Fecha = Herramientas.IngresoFecha();
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

                    creacionExitosa = true;
                    pPedido.Save(p);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ha ocurrido un error al crear el pedido: " + ex.Message);
                }
            } while (!creacionExitosa);
        }



        public static void Listar(bool pausa = true)
        {
            Console.Clear();
            string[,] tabla = new string[Program.pedidos.Count + 1, 3];
            tabla[0, 0] = "Fecha";
            tabla[0, 1] = "Cliente";
            tabla[0, 2] = "Monto Total";

            foreach (var item in Program.pedidos)
            {
                tabla[Program.pedidos.IndexOf(item) + 1, 0] = item.Fecha.ToString();
                tabla[Program.pedidos.IndexOf(item) + 1, 1] = item.Cliente.Nombre + " " + item.Cliente.Apellido;
                tabla[Program.pedidos.IndexOf(item) + 1, 2] = "$" + item.MontoTotal.ToString();
            }
            Herramientas.DibujaTabla(tabla);
            Console.ReadKey();
            if (pausa)
            {
                Console.ReadLine();
            }
        }

        public static void ListarDosFechasPedidos()
        {
            Console.WriteLine("Ingrese la fecha inicial: ");
            DateTime fechaInicial = Herramientas.IngresoFecha();
            Console.WriteLine("Ingrese la fecha final: ");
            DateTime fechaFinal = Herramientas.IngresoFecha();
            Console.Clear();
            string[,] tabla = new string[Program.pedidos.Count + 1, 3];
            tabla[0, 0] = "idPedido ";
            tabla[0, 1] = "Fecha";

            foreach (Pedido p in pPedido.ListarPedidosEntreFechas(fechaInicial,fechaFinal))
            {
                tabla[Program.pedidos.IndexOf(p) + 1, 0] = p.Id.ToString();
                tabla[Program.pedidos.IndexOf(p) + 1, 1] = p.Fecha.ToString();
               
            }
            Herramientas.DibujaTabla(tabla);

            Console.ReadLine();
        }

        public static int Seleccionar()
        {
            int i = 0;
            Listar(false);
            Console.WriteLine("Ingrese el id del pedido: ");
            i = Herramientas.IngresoEnteros(1, Program.pedidos.Count);
            return i - 1;
        }

        public static void Eliminar()
        {
            Console.Clear();
            Console.WriteLine("Apriete cualquier tecla para continuar...");
            try
            {
                int i = Seleccionar();
                pPedido.Delete(Program.pedidos[i]);
                Console.WriteLine("Pedido eliminado con éxito.");
                Console.ReadLine();
                Console.WriteLine("Desea eliminar otro pedido? (S/N)");
                string resp = Console.ReadLine();
                if (resp.ToUpper() == "S")
                {
                    Eliminar();
                }
                else if (resp.ToUpper() == "N")
                {
                    Menu();
                }
                else
                {
                    Console.WriteLine("Respuesta no válida. Por favor, ingrese S o N.");
                    Eliminar();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ha ocurrido un error al eliminar el item: " + ex.Message);
                Eliminar();
            }
        }

        public static void Modificar()
        {
            Listar(false);
            Console.WriteLine("Ingrese el ID del pedido que desea modificar: ");
            int idPedido = int.Parse(Console.ReadLine()); 

            Pedido p = Program.pedidos.FirstOrDefault(pedido => pedido.Id == idPedido);

            if (p != null) 
            {
                Console.WriteLine("Ingrese la nueva fecha del pedido: ");
                try
                {
                    p.Fecha = Herramientas.IngresoFecha();
                }
                catch (FormatException)
                {
                    Console.WriteLine("Fecha no válida. Por favor, ingrese una fecha en el formato correcto.");
                    return;
                }

                Console.WriteLine("Ingrese el nuevo monto total del pedido: ");
                try
                {
                    p.MontoTotal = double.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Monto total no válido. Por favor, ingrese un número.");
                    return;
                }

                pPedido.Modify(p);
            }
            else
            {
                Console.WriteLine("No se encontró un pedido con ese ID. Por favor, intente de nuevo.");
            }
        }



        public static void Menu()
        {
            Console.Clear();
            string[] opciones = new string[6];
            opciones[0] = "Crear Pedido";
            opciones[1] = "Listar Pedidos";
            opciones[2] = "Eliminar Pedidos";
            opciones[3] = "Modificar Pedidos";
            opciones[4] = "Lista por dos fechas ";
            opciones[5] = "Salir";

            Herramientas.DibujoMenu("Menu Pedidos", opciones);
            int op = Herramientas.IngresoEnteros(1, 6);

            switch (op)
            {
                case 1:
                    Crear();
                    Console.ReadLine();
                    Menu();

                    break;
                case 2:
                    Listar();
                    Menu();
                    break;
                case 3:
                    Eliminar();
                    break;
                case 4:
                    Modificar();
                    Menu();
                    break;
                case 5:
                    ListarDosFechasPedidos();
                    Menu();
                    break;
            }

        }
    }
}
