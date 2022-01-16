using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Notas_MongoDB.Models;

namespace Notas_MongoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("ProgramaNotas");
            var estudiantesDB = database.GetCollection<Estudiantes>("Estudiantes");
            var consulta = estudiantesDB.AsQueryable<Estudiantes>();
            
            //CODIGO PRINCIPAL
            Console.WriteLine("\t\t======BIENVENIDO USUARIO======");
            while (true)
            {
                Console.WriteLine("\n++MENÚ PRINCIPAL++");
                Console.WriteLine("Administrador de notas. \n");
                Console.WriteLine("Seleccionar: ");
                Console.WriteLine("Opcion 1. Registrar notas.");
                Console.WriteLine("Opcion 2. Consultar registros.");
                Console.WriteLine("Opcion 3. Modificar registros.");
                Console.WriteLine("Opcion 4. Eliminar registros.");
                Console.WriteLine("Opcion 5. Cerrar programa.");
                int teclado = int.Parse(Console.ReadLine());
                switch (teclado)
                {
                    case 1:
                        Console.WriteLine("\n++REGISTRAR++");
                        Console.WriteLine("Ingresar: \n-Nombre. \n-Primera Nota. \n-Segunda Nota.");
                        var estudiantes = new Estudiantes()
                        {
                            Nombre = Console.ReadLine(),
                            Nota1 = int.Parse(Console.ReadLine()),
                            Nota2 = int.Parse(Console.ReadLine())
                        };
                        estudiantesDB.InsertOne(estudiantes);
                        break;

                    case 2:
                        while (true)
                        {
                            Console.WriteLine("\n++CONSULTAR++");
                            Console.WriteLine("Seleccionar: ");
                            Console.WriteLine("Opcion 1. Consultar por ID.");
                            Console.WriteLine("Opcion 2. Consulta general.");
                            Console.WriteLine("Opcion 3. Atrás.");
                            int teclado1 = int.Parse(Console.ReadLine());
                            switch (teclado1)
                            {
                                case 1:
                                    Console.WriteLine("++Consultar por ID++");
                                    Console.WriteLine("++Inserte la ID del estudiante: ");
                                    var filtro = from c in consulta
                                              where c.Id == Console.ReadLine()
                                              select c;
                                    Console.WriteLine();
                                    foreach (Estudiantes estudiante in filtro)
                                    {
                                        Console.WriteLine(estudiante.ToJson());
                                    }
                                    break;
                                case 2:
                                    Console.WriteLine("++Consulta general++");
                                    Console.WriteLine();
                                    foreach (Estudiantes estudiante in consulta)
                                    {
                                        Console.WriteLine(estudiante.ToJson());
                                    }
                                    break;
                                case 3:
                                    
                                    break;
                            }
                            if (teclado1 <= 0 || teclado1 > 3)
                                Console.WriteLine("Por favor, seleccione una opción válida.");
                            if (teclado1 == 3)
                                break;
                        }
                        break;

                    case 3:
                        Console.WriteLine("\n++MODIFICAR++");
                        Console.WriteLine("++Inserte la ID del estudiante: ");
                        var filtro1 = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine("Ingresar: \n-Nombre. \n-Primera Nota. \n-Segunda Nota.");
                        var modificar = new Estudiantes()
                        {
                            Id = filtro1,
                            Nombre = Console.ReadLine(),
                            Nota1 = int.Parse(Console.ReadLine()),
                            Nota2 = int.Parse(Console.ReadLine())
                        };
                        estudiantesDB.ReplaceOne(d=>d.Id == filtro1, modificar);
                        break;

                    case 4:
                        while (true)
                        {
                            Console.WriteLine("\n++ELIMINAR++");
                            Console.WriteLine("Seleccionar: ");
                            Console.WriteLine("Opcion 1. Eliminar por ID.");
                            Console.WriteLine("Opcion 2. Eliminar en general.");
                            Console.WriteLine("Opcion 3. Atrás.");
                            int teclado1 = int.Parse(Console.ReadLine());
                            switch (teclado1)
                            {
                                case 1:
                                    Console.WriteLine("++Eliminar por ID++");
                                    estudiantesDB.DeleteOne(d => d.Id == Console.ReadLine());
                                    Console.WriteLine("Registro eliminado...");
                                    break;
                                case 2:
                                    Console.WriteLine("++Eliminar en general++");
                                    foreach (Estudiantes estudiante in consulta)
                                    {
                                        estudiantesDB.DeleteMany(estudiante.ToJson());
                                    }
                                    Console.WriteLine("Todos los registros fueron eliminados...");
                                    break;
                                case 3:

                                    break;
                            }
                            if (teclado1 <= 0 || teclado1 > 3)
                                Console.WriteLine("Por favor, seleccione una opción válida.");
                            if (teclado1 == 3)
                                break;
                        }
                        break;

                    case 5:

                        break;
                }
                if (teclado <= 0 || teclado > 5)
                    Console.WriteLine("Por favor, seleccionar una opción válida.");
                if (teclado == 5)
                {
                    Console.WriteLine("\nGracias por usar el programa :D");
                    break;
                }

            }
        }
    }
}
