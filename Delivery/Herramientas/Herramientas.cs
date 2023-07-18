using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Delivery.Varios
{
    class Herramientas
    {
        public static int IngresoEnteros()
        {
            ConsoleKeyInfo k;
            string cadena = "";
            do
            {
                k = Console.ReadKey(true);
                if (k.KeyChar > 47 && k.KeyChar < 58)
                {
                    Console.Write(k.KeyChar);
                    cadena = cadena + k.KeyChar.ToString();


                }
                if (k.Key == ConsoleKey.Backspace && cadena.Length > 0)
                {
                    Console.CursorLeft = Console.CursorLeft - 1;
                    Console.Write(" ");
                    Console.CursorLeft = Console.CursorLeft - 1;
                    cadena = cadena.Substring(0, cadena.Length - 1);
                }

            } while (k.Key != ConsoleKey.Enter || cadena.Length == 0);



            //Console.WriteLine("El valor ingresado es {0}", cadena);
            if (cadena.Length > 0)
            {
                return int.Parse(cadena);
            }
            else { return IngresoEnteros(); }
        }

        public static DateTime IngresoFecha()
        {
            DateTime fecha = new DateTime();
            bool error = false;
            do
            {
                error = false;
                try
                {
                    fecha = DateTime.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Formato incorrecto, intente de nuevo...");
                    error = true;
                }
            } while (error);

            return fecha;
        }

        public static int IngresoEnteros(int min, int max)
        {
            ConsoleKeyInfo k;
            string cadena = "";
            do
            {
                k = Console.ReadKey(true);
                if (k.KeyChar > 47 && k.KeyChar < 58)
                {
                    Console.Write(k.KeyChar);
                    cadena = cadena + k.KeyChar.ToString();


                }
                if (k.Key == ConsoleKey.Backspace)
                {
                    Console.CursorLeft = Console.CursorLeft - 1;
                    Console.Write(" ");
                    Console.CursorLeft = Console.CursorLeft - 1;
                    cadena = cadena.Substring(0, cadena.Length - 1);
                }

            } while (k.Key != ConsoleKey.Enter || cadena.Length == 0);



            //Console.WriteLine("El valor ingresado es {0}", cadena);
            if (cadena.Length > 0)
            {

                int v = int.Parse(cadena);
                if (v >= min && v <= max)
                {
                    return v;
                }
                else
                {
                    Console.WriteLine("\nIngrese un valor entre {0} y {1}", min, max);
                    return IngresoEnteros(min, max);
                }
            }
            else { return IngresoEnteros(min, max); }
        }

        public static void DibujoMenu(string titulo, string[] opciones)
        {
            //Busco cual es la palabra mas larga del menu
            //para definir el ancho del mismo
            int largo = titulo.Length;
            for (int i = 0; i < opciones.Length; i++)
            {
                if (largo < opciones[i].Length)
                {
                    largo = opciones[i].Length;
                }
            }
            //a la palabra mas larga del menu le sumo 8 para los espacios y el marco
            largo = largo + 8;

            //Posicion donde comienza a dibujar el menu
            //esta posicion la repito en todos los lugares que comienzo o ubico el cursor 
            //en una linea
            Console.CursorLeft = 10;

            //Dibujo la linea superior del menu
            for (int i = 0; i < largo; i++)
            {
                if (i == 0)
                    Console.Write("╔");
                else
                    if (i == largo - 1)
                    Console.Write("╗\n");


                else
                    Console.Write("═");


            }

            //Coloco el tiulo del menu centrado.
            Console.CursorLeft = 10;
            Console.Write("║");
            Console.CursorLeft = (largo - titulo.Length) / 2 + 10;
            Console.Write(titulo);
            Console.CursorLeft = largo - 1 + 10;
            Console.Write("║\n");

            //Cierro el cuadro del titulo
            Console.CursorLeft = 10;
            for (int i = 0; i < largo; i++)
            {
                if (i == 0)
                    Console.Write("╠");
                else
                    if (i == largo - 1)
                    Console.Write("╣\n");
                else
                        if (i == 4)
                    Console.Write("╦");
                else
                    Console.Write("═");


            }

            //recorro el array de las opciones y las imprimo
            // con el marco correspondiente
            Console.CursorLeft = 10;
            for (int i = 0; i < opciones.Length; i++)
            {
                Console.CursorLeft = 10;

                Console.Write("║ ");
                if (i < 9)
                {
                    Console.Write("0{0}║ {1}", i + 1, opciones[i]);
                }
                else
                {
                    Console.Write("{0}║ {1}", i + 1, opciones[i]);
                }
                Console.CursorLeft = largo - 1 + 10;
                Console.Write("║\n");
            }
            Console.CursorLeft = 10;

            //cierro el cuadro del menu
            for (int i = 0; i < largo; i++)
            {
                if (i == 0)
                    Console.Write("╚");
                else
                    if (i == largo - 1)
                    Console.Write("╝\n");
                else
                        if (i == 4)
                    Console.Write("╩");
                else
                    Console.Write("═");


            }

        }
        public static void DibujaTabla(string[,] tabla)
        {
            int max = 0;
            //Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < tabla.GetLength(0); i++)
            {
                for (int j = 0; j < tabla.GetLength(1); j++)
                {
                    if (tabla[i, j] != null)
                        if (tabla[i, j].Length > max)
                            max = tabla[i, j].Length;
                }

            }

            max = max + 2;

            for (int j = 0; j < tabla.GetLength(1); j++)
            {
                if (j == 0)
                    Console.Write("╔");
                for (int a = 0; a < max - 1; a++)
                {
                    Console.Write("═");
                }
                if (j < tabla.GetLength(1) - 1)
                {

                    Console.Write("╦");
                }
                if (j == tabla.GetLength(1) - 1)
                    Console.Write("╗");
            }

            Console.WriteLine();
            for (int i = 0; i < tabla.GetLength(0); i++)
            {

                for (int j = 0; j < tabla.GetLength(1); j++)
                {
                    Console.CursorLeft = max * j;
                    Console.Write("║");
                    if (i == 0)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" {0} ", tabla[i, j]);
                    Console.ForegroundColor = ConsoleColor.White;

                }
                if (i == 0 || i == tabla.GetLength(0) - 1)
                {
                    Console.CursorLeft = max * tabla.GetLength(1);
                    Console.Write("║");
                    Console.WriteLine();
                    for (int k = 0; k < tabla.GetLength(1); k++)
                    {
                        if (k == 0 && i < tabla.GetLength(0) - 1)
                            Console.Write("╠");

                        if (k == 0 && i == tabla.GetLength(0) - 1)
                            Console.Write("╚");

                        for (int a = 0; a < max - 1; a++)
                        {
                            Console.Write("═");
                        }
                        if (k < tabla.GetLength(1) - 1)
                        {
                            if (i < tabla.GetLength(0) - 1)
                                Console.Write("╬");
                            else
                                Console.Write("╩");
                        }
                        if (k == tabla.GetLength(1) - 1)
                        {
                            if (i == tabla.GetLength(0) - 1)
                                Console.Write("╝");
                            else
                                Console.Write("╣");
                        }
                    }
                }
                else
                {
                    Console.CursorLeft = max * tabla.GetLength(1);
                    Console.Write("║");

                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void DibujaTabla(string[,] tabla, StreamWriter writer)
        {
            int max = 0;
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < tabla.GetLength(0); i++)
            {
                for (int j = 0; j < tabla.GetLength(1); j++)
                {
                    if (tabla[i, j] != null)
                        if (tabla[i, j].Length > max)
                            max = tabla[i, j].Length;
                }

            }

            max = max + 3;

            for (int j = 0; j < tabla.GetLength(1); j++)
            {
                if (j == 0)
                    writer.Write("╔");
                for (int a = 0; a < max - 1; a++)
                {
                    writer.Write("═");
                }
                if (j < tabla.GetLength(1) - 1)
                {

                    writer.Write("╦");
                }
                if (j == tabla.GetLength(1) - 1)
                    writer.Write("╗");
            }

            writer.WriteLine();
            for (int i = 0; i < tabla.GetLength(0); i++)
            {

                for (int j = 0; j < tabla.GetLength(1); j++)
                {

                    //Console.CursorLeft = (max * j) ;
                    writer.Write("║");
                    if (i == 0)
                        Console.ForegroundColor = ConsoleColor.White;
                    else
                        Console.ForegroundColor = ConsoleColor.White;
                    writer.Write(" {0} ", tabla[i, j]);
                    for (int h = tabla[i, j].Length + 2; h < max - 1; h++)
                        writer.Write(" ");

                    Console.ForegroundColor = ConsoleColor.Yellow;

                }
                if (i == 0 || i == tabla.GetLength(0) - 1)
                {
                    //Console.CursorLeft = max * tabla.GetLength(1);
                    writer.Write("║");
                    writer.WriteLine();
                    for (int k = 0; k < tabla.GetLength(1); k++)
                    {
                        if (k == 0 && i < tabla.GetLength(0) - 1)
                            writer.Write("╠");

                        if (k == 0 && i == tabla.GetLength(0) - 1)
                            writer.Write("╚");

                        for (int a = 0; a < max - 1; a++)
                        {
                            writer.Write("═");
                        }
                        if (k < tabla.GetLength(1) - 1)
                        {
                            if (i < tabla.GetLength(0) - 1)
                                writer.Write("╬");
                            else
                                writer.Write("╩");
                        }
                        if (k == tabla.GetLength(1) - 1)
                        {
                            if (i == tabla.GetLength(0) - 1)
                                writer.Write("╝");
                            else
                                writer.Write("╣");
                        }
                    }
                }
                else
                {
                    //Console.CursorLeft = max * tabla.GetLength(1);
                    writer.Write("║");

                }
                writer.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

}

