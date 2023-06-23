using System.Threading;

namespace ClaseHilos
{
   internal class Producto
   {
      public string Nombre { get; set; }
      public decimal PrecioUnitarioDolares { get; set; }
      public int CantidadEnStock { get; set; }

      public Producto(string nombre, decimal precioUnitario, int cantidadEnStock)
      {
         Nombre = nombre;
         PrecioUnitarioDolares = precioUnitario;
         CantidadEnStock = cantidadEnStock;
      }
   }
   internal class Solution //reference: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock
   {
        

        static List<Producto> productos = new List<Producto>
        {
            new Producto("Camisa", 10, 50),
            new Producto("Pantalón", 8, 30),
            new Producto("Zapatilla/Champión", 7, 20),
            new Producto("Campera", 25, 100),
            new Producto("Gorra", 16, 10)
        };

      static int precio_dolar = 500;
       static Barrier barrera = new Barrier(2);

        public static void Tarea1()
      {

            lock (productos)
            {
                foreach (var producto in productos)
                {
                    producto.CantidadEnStock += 10;
                }

            }
            barrera.SignalAndWait();
            //Listar();
            //throw new NotImplementedException();
        }
      public static void Tarea2()
      {
            precio_dolar = 520;
            Console.WriteLine($"Precio Dolar: {precio_dolar}");

            barrera.SignalAndWait();
            //throw new NotImplementedException();
        }
        static void Tarea3()
      {
            //foreach (var producto in productos)
            //{
            //    Console.WriteLine($"Producto:{producto.Nombre} - cantidad: {producto.CantidadEnStock} - {precio_dolar}");
            //}
            //throw new NotImplementedException();
            lock (productos)
            {
                    Console.WriteLine("Informe de inventario:");
                    Console.WriteLine($"Precio Dolar: {precio_dolar}");
                    Console.WriteLine("Producto\tCantidad en stock\tPrecio total (en moneda local)");
                    decimal totalInventario = 0;

                    foreach (var producto in productos)
                    {
                        decimal precioTotalProducto = producto.CantidadEnStock * producto.PrecioUnitarioDolares * precio_dolar;
                        totalInventario += precioTotalProducto;
                        Console.WriteLine($"{producto.Nombre}\t{producto.CantidadEnStock}\t\t{precioTotalProducto}");
                    }

                    Console.WriteLine($"Total del inventario: {totalInventario}");
            }
        }

 

        
        internal static void Ejercicio2()
        {

            foreach (var producto in productos)
            {
                producto.PrecioUnitarioDolares = producto.PrecioUnitarioDolares * (decimal)0.1;
            }


        }
        internal static void Excecute()
        {
            // Ejecutar Tarea 1 y Tarea 2 en hilos separados
            var tarea1 = new Thread(() => Tarea1());
            var tarea2 = new Thread(() => Tarea2());

            tarea1.Start();
            tarea2.Start();

            // Esperar a que ambas tareas finalicen
            tarea1.Join();
            tarea2.Join();

            // Ejecutar ActualizarPrecios
            Ejercicio2();

            // Ejecutar Tarea 3
            Tarea3();
        }



        //  internal static void Excecute()
        //{

        //          Thread hilo = new Thread(new ThreadStart(Hilo_hijo));
        //          hilo.Name = "Tarea1 y Tarea 2 ";
        //          hilo.Start();
        //          hilo.Join();
        //          Console.WriteLine("Hilo hijo terminado");
        //          Tarea3();
        //          Console.WriteLine("Hilo listo");


        //      //throw new NotImplementedException();
        //}
    }
}