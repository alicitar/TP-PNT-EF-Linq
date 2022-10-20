using System;
using System.Linq;

namespace PruebaEFCoreCF
{
    class Program
    {
        static void Main(string[] args)
        {

            AgregarEstudianteBill();
            AgregarEstudianteMary(); 
            AgregarEstudianteBill();
            AgregarEstudianteMary();
            //se agregaron 4 registros porque al eliminar elimina el 4to, sino daba error
            ConsultarEstudianteLambda();
            ConsultarEstudianteQuery();
            ListarTodosLosEstudiantes();
            ActualizacionYEliminacionDeDatos();
            ListarTodosLosEstudiantes();
        }

        static void AgregarEstudianteBill()
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("Agregar Estudiante - Bill");
            Console.WriteLine("--------------------");
            // Agregar un estudiante
            using (var context = new SchoolContext())
            {
                var std = new Student()
                {
                    Name = "Bill"
                };
                context.Students.Add(std);
                context.SaveChanges();
            }
            Console.WriteLine("Estudiante agregado!");
            Console.ReadKey();
            Console.WriteLine("--------------------");
        }

        static void AgregarEstudianteMary()
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("Agregar Estudiante - Mary");
            Console.WriteLine("--------------------");
            // Agregar otro estudiante
            using (var context = new SchoolContext())
            {
                var std = new Student()
                {
                    Name = "Mary"
                };
                context.Students.Add(std);
                context.SaveChanges();
            }
            Console.WriteLine("Otro Estudiante agregado!");
            Console.ReadKey();
            Console.WriteLine("--------------------");
        }

        static void ConsultarEstudianteLambda()
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("Consulta de Estudiante LAMBDA");
            Console.WriteLine("--------------------");
            //Consultar un estudiante con expresión Lambda
            using (var context = new SchoolContext())
            {
                var consulta = context.Students.Where(s => s.StudentId == 2);
                var estudiante = consulta.FirstOrDefault<Student>();
                Console.WriteLine("(LINQ)Encontré al estudiante 2 y se llama " + estudiante.Name);
            }

            Console.WriteLine("--------------------");
            Console.ReadKey();
        }

        static void ConsultarEstudianteQuery()
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("Consulta de Estudiante");
            Console.WriteLine("--------------------");
            //Consultar un estudiante con expresión de consulta
            using (var context = new SchoolContext())
            {

                var cons = from st in context.Students
                           where st.StudentId == 2
                           select st;
                var estu = cons.FirstOrDefault<Student>();
                Console.WriteLine("Encontré al estudiante 2 y se llama " + estu.Name);
            }

            Console.WriteLine("--------------------");
            Console.ReadKey();
        }

        static void ListarTodosLosEstudiantes()
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("Lista de estudiantes");
            Console.WriteLine("--------------------");
            using (var context = new SchoolContext())
            {
                var estudiante = from s in context.Students
                                 orderby s.Name
                                 select s;
                foreach (var item in estudiante)
                {
                    Console.WriteLine("Estudiante {0} ", item.Name);
                }
            }
            Console.WriteLine("--------------------");
            Console.ReadKey();
        }

        static void ActualizacionYEliminacionDeDatos()
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("Actualización y Eliminación");
            Console.WriteLine("--------------------");

            using (var context = new SchoolContext())
            {
                var listaEstudiantes = context.Students.ToList<Student>();
                //Agrega un nuevo estudiante
                context.Students.Add(new Student()
                {
                    Name = "NUEVO",
                });
                //Actualiza un estudiante
                Student estudianteActualizado = listaEstudiantes.Where(s => s.StudentId == 2).FirstOrDefault<Student>();
                estudianteActualizado.Name = "RODRIGO";
                //Elimina un estudiante en posición 3
                context.Students.Remove(listaEstudiantes.ElementAt<Student>(3));
                //Ejecuta instrucciones Insert, Update & Delete en la base de datos
                context.SaveChanges();
            }

            Console.WriteLine("--------------------");
            Console.ReadKey();
        }
    }
}