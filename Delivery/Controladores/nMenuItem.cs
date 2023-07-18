using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Entidades;
using Delivery.Varios;

namespace Delivery.Controladores
{
    public class nMenuItem
    {
        public static void Crear()
        {
            Console.Clear();
            bool creacionExitosa = false;
            do
            {
                try
                {
                    MenuItem m = new MenuItem();
                    Console.WriteLine("Ingrese el nombre del item: ");
                    try
                    {
                        m.Nombre = Console.ReadLine();
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Nombre no válido. Por favor, ingrese un nombre válido.");
                        continue;
                    }
                    Console.WriteLine("Ingrese la descripción del item: ");
                    try
                    {
                        m.Descripcion = Console.ReadLine();
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Descripción no válida. Por favor, ingrese una descripción válida.");
                        continue;
                    }
                    Console.WriteLine("Ingrese el precio del item: ");
                    try
                    {
                        m.Precio = double.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Precio no válido. Por favor, ingrese un precio válido.");
                        continue;
                    }
                    pMenuItem.Save(m);
                    Console.WriteLine("Item creado con éxito.");
                    pMenuItem.GetAll();
                    bool masitems = false;
                    do
                    {
                        masitems = false;
                        Console.WriteLine("Desea agregar otro item? (S/N)");
                        string resp = Console.ReadLine();
                        if (resp.ToUpper() == "S")
                        {
                           masitems = true;
                           creacionExitosa = true;
                        } 
                        else if (resp.ToUpper() == "N")
                        {
                            masitems = false;
                            creacionExitosa = true;
                        }
                        else
                        {
                            Console.WriteLine("Respuesta no válida. Por favor, ingrese S o N.");
                            masitems = true;
                        }   
                    } while (masitems);
                    creacionExitosa = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ha ocurrido un error al crear el item: " + ex.Message);
                    creacionExitosa = false;
                }
            } while (!creacionExitosa);

        }

        public static void Listar()
        {
            string[,] tabla = new string[Program.menuItems.Count + 1, 3];
            tabla[0, 0] = "Nombre";
            tabla[0, 1] = "Descripcion";
            tabla[0, 2] = "Precio";
            foreach (MenuItem m in Program.menuItems)
            {
                tabla[Program.menuItems.IndexOf(m) + 1, 0] = m.Nombre;
                tabla[Program.menuItems.IndexOf(m) + 1, 1] = m.Descripcion;
                tabla[Program.menuItems.IndexOf(m) + 1, 2] = m.Precio.ToString();
            }
            Herramientas.DibujaTabla(tabla);
        }

        public static int Seleccionar()
        {
            int i = 0;
            Listar();
            Console.WriteLine("Ingrese el id del item: ");
            i = Herramientas.IngresoEnteros(1, Program.menuItems.Count);
            return i - 1;
        }

        public static void Eliminar()
        {
            try
            {
                int i = Seleccionar();
                Program.menuItems.RemoveAt(i);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ha ocurrido un error al eliminar el item: " + ex.Message);
                Eliminar();
            }
        }

        public static void Modificar(MenuItem m)
        {
            int op;
            do
            {
                Console.Clear();
                string[] tabla = new string[5];
                tabla[0] = "ID";
                tabla[1] = "Nombre";
                tabla[2] = "Descripcion";
                tabla[3] = "Precio";
                tabla[4] = "Salir";

                Herramientas.DibujoMenu("Modificar Item" + m.ToString(), tabla);
                op = Herramientas.IngresoEnteros(1, 5);

                switch (op)
                {
                    case 1:
                        Console.WriteLine("Ingrese el nuevo nombre del item: ");
                        m.Nombre = Console.ReadLine();
                        pMenuItem.Modify(m);
                        break;
                    case 2:
                        Console.WriteLine("Ingrese la nueva descripcion del item: ");
                        m.Descripcion = Console.ReadLine();
                        pMenuItem.Modify(m);
                        break;
                    case 3:
                        Console.WriteLine("Ingrese el nuevo precio del item: ");
                        m.Precio = Herramientas.IngresoEnteros();
                        pMenuItem.Modify(m);
                        break;
                    case 4:
                        Console.WriteLine("Salir del menú de modificación");
                        break;
                }
            } while (op != 4);
        }

        public static void Menu()
        {
            Console.Clear();
            string[] opciones = new string[5];
            opciones[0] = "Crear Item";
            opciones[1] = "Listar Items";
            opciones[2] = "Eliminar Item";
            opciones[3] = "Modificar Item";
            opciones[4] = "Salir";
            Herramientas.DibujoMenu("Menu Item", opciones);
            Console.CursorLeft = 10;
            Console.Write("Ingrese una opcion: ");
            int op = Herramientas.IngresoEnteros(1, opciones.Length);

            switch (op)
            {
                case 1: Crear(); Menu(); break;
                case 2: Listar(); Menu(); break;
                case 3: Eliminar(); Menu(); break;
                case 4:
                    Modificar(Program.menuItems[Seleccionar()]); Menu(); break;
                case 5: break;
                default: Menu(); break;
            }

        }


    }
}
