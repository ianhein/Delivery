using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Entidades;
using Delivery.Varios;
using Delivery;
using System.Collections;

namespace Delivery.Controladores
{
    public class nCliente
    {
        public static void Crear()
        {
            Console.Clear();
            bool creacionExitosa = false;
            do
            {
                try
                {
                    Cliente c = new Cliente();
                    Console.WriteLine("Ingrese el nombre del cliente: ");
                    try 
                    {
                        c.Nombre = Console.ReadLine();
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Nombre no válido. Por favor, ingrese un nombre válido.");
                        continue;
                    }
                    Console.WriteLine("Ingrese el apellido del cliente: ");
                    try
                    {
                        c.Apellido = Console.ReadLine();
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Apellido no válido. Por favor, ingrese un apellido válido.");
                        continue;
                    }
                    Console.WriteLine("Ingrese la dirección del cliente: ");
                    try
                    {
                        c.Direccion = Console.ReadLine();
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Dirección no válida. Por favor, ingrese una dirección válida.");
                        continue;
                    }
                    pCliente.Crear(c);
                    Console.WriteLine("Cliente creado con éxito.");
                    bool masclientes = false;
                    do
                    {
                        masclientes= false;
                        Console.WriteLine("Desea agregar otro cliente? (S/N)");
                        string resp = Console.ReadLine();
                        if (resp.ToUpper() == "S")
                        {
                            masclientes = true;
                            creacionExitosa = true;
                        }
                        else if (resp.ToUpper() == "N")
                        {
                            masclientes = false;
                            creacionExitosa = true;
                        }
                        else
                        {
                            Console.WriteLine("Respuesta no válida. Por favor, ingrese S o N.");
                            masclientes = true;
                        }
                    } while (masclientes);

                    creacionExitosa = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ha ocurrido un error al crear el cliente: " + ex.Message);
                    creacionExitosa = false;
                }
            } while (!creacionExitosa);
        }

        public static void Listar(bool pausa = true)
        {

            string[,] tabla = new string[Program.clientes.Count + 1, 3];
            tabla[0, 0] = "Id";
            tabla[0, 1] = "Nombre Completo";
            tabla[0, 2] = "Direccion";
            foreach (Cliente c in Program.clientes)
            {
                tabla[Program.clientes.IndexOf(c) + 1, 0] = (Program.clientes.IndexOf(c) + 1).ToString();
                tabla[Program.clientes.IndexOf(c) + 1, 1] = c.Nombre + " " + c.Apellido;
                tabla[Program.clientes.IndexOf(c) + 1, 2] = c.Direccion;
            }
            Herramientas.DibujaTabla(tabla);
            if (pausa)
            {
                Console.ReadLine();
            }

        }

        public static int Seleccionar()
        {
            Console.Clear();
            int i = 0;
            Listar(false);
            Console.WriteLine("Ingrese el id del cliente: ");
            i = Herramientas.IngresoEnteros(1, Program.clientes.Count);
            Console.ReadLine();
            return i - 1;

        }

        public static void Eliminar()
        {
            Console.Clear();
            Console.WriteLine("Apriete cualquier tecla para continuar...");
            try
            {
                int i = Seleccionar();
                pCliente.Delete(Program.clientes[i]);
                Console.WriteLine("Cliente eliminado con éxito.");
                Console.ReadLine();
                Console.WriteLine("Desea eliminar otro cliente? (S/N)");
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

        public static void Modificar(Cliente c)
        {
            int op;
            do
            {
                Console.Clear();
                string[] tabla = new string[4];
                tabla[0] = "Nombre";
                tabla[1] = "Apellido";
                tabla[2] = "Direccion";
                tabla[3] = "Salir";

                Herramientas.DibujoMenu("Modificar datos del Cliente", tabla);
                op = Herramientas.IngresoEnteros(1, 5);

                switch (op)
                {
                    case 1:
                        Console.WriteLine("Ingrese el nuevo nombre para {0} {1}: ", c.Nombre, c.Apellido);
                        c.Nombre = Console.ReadLine();
                        pCliente.Modify(c);
                        break;
                    case 2:
                        Console.WriteLine("Ingrese el nuevo apellido para {0} {1}: ", c.Nombre, c.Apellido);
                        c.Apellido = Console.ReadLine();
                        pCliente.Modify(c);
                        break;
                    case 3:
                        Console.WriteLine("Ingrese la nueva direccion para {0} {1} en {2}: ", c.Nombre, c.Apellido, c.Direccion);
                        c.Direccion = Console.ReadLine();
                        pCliente.Modify(c);
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Opción inválida. Por favor, intente de nuevo.");
                        break;

                }
            }
            while (op != 4);
            Menu();

        }

        public static void Menu()
        {
            Console.Clear();
            string[] opciones = new string[5];
            opciones[0] = "Crear Cliente";
            opciones[1] = "Listar Clientes";
            opciones[2] = "Eliminar Cliente";
            opciones[3] = "Modificar Cliente";
            opciones[4] = "Salir";

            Herramientas.DibujoMenu("Menu Clientes", opciones);
            int op = Herramientas.IngresoEnteros(1, 5);

            switch (op)
            {
                case 1:
                    Crear();
                    Menu();
                    break;
                case 2:
                    Listar();
                    Menu();

                    break;
                case 3:
                    Eliminar();
                    Menu();
                    break;
                case 4:
                    int clienteSeleccionado = Seleccionar();
                    if (clienteSeleccionado >= 0)
                    {
                        Console.Clear();
                        Modificar(Program.clientes[clienteSeleccionado]);
                    }
                    else
                    {
                        Console.WriteLine("No se seleccionó ningún cliente. Volviendo al menú...");
                    }
                    Menu();
                    break;
                default:
                    break;
            }

        }

    }
}
