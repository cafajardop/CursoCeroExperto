using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemos
{
    class NombreCompleto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var estudiantes = new[]
            {
                new
                {
                    EstudianteID = 1,
                    Nombre = "Héctor",
                    ApellidoPaterno = "Pérez",
                    Universidad = "Real de Brasil"
                },
                new
                {
                    EstudianteID = 2,
                    Nombre = "Ana",
                    ApellidoPaterno = "Nepomuceno",
                    Universidad = "Oxford"
                },
                new
                {
                    EstudianteID = 3,
                    Nombre = "Pedro",
                    ApellidoPaterno = "Sánchez",
                    Universidad = "Harvard"
                },
                new
                {
                    EstudianteID = 4,
                    Nombre = "José",
                    ApellidoPaterno = "Infante",
                    Universidad = "Harvard"
                },
                new
                {
                    EstudianteID = 5,
                    Nombre = "Regina",
                    ApellidoPaterno = "Bustamante",
                    Universidad = "Oxford"
                },
                new
                {
                    EstudianteID = 6,
                    Nombre = "Rodrigo",
                    ApellidoPaterno = "Jiménez",
                    Universidad = "Brooklyn"
                },
                new
                {
                    EstudianteID = 7,
                    Nombre = "Miguel",
                    ApellidoPaterno = "Hernández",
                    Universidad = "UNAM"
                },
                new
                {
                    EstudianteID = 8,
                    Nombre = "Marilyn",
                    ApellidoPaterno = "Monroe",
                    Universidad = "UNAM"
                },
                new
                {
                    EstudianteID = 9,
                    Nombre = "Leonardo",
                    ApellidoPaterno = "Estrada",
                    Universidad = "Brooklyn"
                },
                new
                {
                    EstudianteID = 10,
                    Nombre = "Ricardo",
                    ApellidoPaterno = "Rojas",
                    Universidad = "Real de Brasil"
                },
            };
            var universidades = new[]
            {
                new
                {
                    Universidad = "Real de Brasil",
                    Ciudad = "Brasilia",
                    Pais = "Brasil"
                },
                new
                {
                    Universidad = "Oxford",
                    Ciudad = "Oxford",
                    Pais = "Reino Unido"
                },
                new
                {
                    Universidad = "Harvard",
                    Ciudad = "Cambridge",
                    Pais = "Estados Unidos"
                },
                new
                {
                    Universidad = "Brooklyn",
                    Ciudad = "Nueva York",
                    Pais = "Estados Unidos"
                },
                new
                {
                    Universidad = "UNAM",
                    Ciudad = "Ciudad de México",
                    Pais = "México"
                },
            };

            //METODO SELECT
            //Estudiantes sin cadena interpolada
            IEnumerable<string> nombreEstudiantes = estudiantes.Select(x => x.Nombre);

            //Estudiantes sin cadena interpolada
            IEnumerable<string> nombreEstudiantesCadena = estudiantes.Select(x => $"{x.Nombre} {x.ApellidoPaterno}");

            //Retornar una instancia de nombre completo
            IEnumerable<NombreCompleto> nombreEstudClass = estudiantes.Select(x => new NombreCompleto
            {
                Nombre = x.Nombre,
                Apellido = x.ApellidoPaterno
            });

            //Tipo anonimo sin crear una nueva clase
            var nombreEstudiantesAnonimo = estudiantes.Select(x => new
            {
                Nombre = x.Nombre,
                Apellido = x.ApellidoPaterno
            });

            //Recorrer con Foreach
            foreach (var item in nombreEstudiantes)
            {
                Console.WriteLine(item);
            }
            //Recorrer con For con cadena Interpolada
            for (int i = 0; i < nombreEstudiantesCadena.Count(); i++)
            {
                Console.WriteLine(nombreEstudiantesCadena.ElementAt(i));
            }

            //METODO WHERE
            //Buscar Donde el pais sea estados unidos y solo selecionar la universidad
            var universidadColombia = universidades
                .Where(u => u.Pais == "Estados Unidos")
                .Select(u => u.Universidad);

            foreach (var universidad in universidadColombia)
            {
                Console.WriteLine(universidad);
            }
            //ORDENANDO Y AGRUPANDO CAMPOS
            //Ordenar las universidades de forma alfabetica
            IEnumerable<string> nombresUniversidades = universidades
                .OrderBy(o => o.Universidad)
                .Select(o => o.Universidad);

            //Ordenar las universidades de forma alfabetica oder by descending
            IEnumerable<string> nombresUniversidadesDescen = universidades
                .OrderByDescending(o => o.Universidad)
                .Select(o => o.Universidad);

            //Especificar la otra llave para ordenar con ThenBy
            IEnumerable<string> nombresUniversidadesDescenThenBy = universidades
                .OrderByDescending(o => o.Universidad)
                .ThenBy(o => o.Pais)
                .Select(o => o.Universidad);
            //Hacer un group by
            var universidadesPaisGroupBy = universidades
                .GroupBy(u => u.Pais);

            //Recorrer el group by
            foreach (var grupo in universidadesPaisGroupBy)
            {
                Console.WriteLine($"Universidad: {grupo.Key} \t {grupo.Count()}");
                foreach (var universidad in grupo)
                {
                    Console.WriteLine($"\t {universidad.Universidad}");// \t Es un espacio en blanco
                }
            }

            //METODOS DE RESUMEN
            int numeroUniversidades = universidades
                .Select(x => x.Universidad)
                .Count();
            Console.WriteLine(numeroUniversidades);

            //Eliminar valores duplicados Distinct
            int numeroPaisesDistinct = universidades
                .Select(u => u.Pais)
                .Distinct()
                .Count();

            //METODO JOIN
            //Obtener la instancia de un objeto anonimo significa que no es necesario crear clases
            var join = estudiantes
                .Select(e => new
                {
                    e.Nombre,
                    e.ApellidoPaterno,
                    e.Universidad
                })
                .Join(universidades,
                    e => e.Universidad,
                    u => u.Universidad,
                    (e, u) => new { e.Nombre, e.ApellidoPaterno, u.Pais });

            foreach (var fila in join)
            {
                Console.WriteLine(fila);
            }
            //OPERADORES DE CONSULTA
            //Sin tipo anonimo es decir seleccionando la consulta sin devolver otro objeto
            var nombreEstudianteConsulta = from nombres in estudiantes
                                           select nombres.Nombre;
            //Con tipo anonimo
            var nombreEstudianteConsultaAnonimo = from nombres in estudiantes
                                                  select new
                                                  {
                                                      nombres.Nombre,
                                                      nombres.ApellidoPaterno
                                                  };
            //Con filtro where con lista y equals normal
            var universidadesCOL = from universidad in universidades
                                   where universidad.Pais == "Estados unidos"
                                   select universidad.Pais.ToList();

            //Con filtro where string.equals
            var universidadesCOLString = from universidad in universidades
                                         where string.Equals(universidad.Pais, "Estados Unidos")
                                         select universidad.Pais.ToList();

            //Operador Group by
            var nombresUniversidadesGroupBy = from u in universidades
                                              orderby u.Universidad
                                              select u.Universidad;

            foreach (var group in nombresUniversidadesGroupBy)
            {
                Console.WriteLine(group);
            }

            //Ordenamiento descending ordenamiento de forma inversa
            var nombresUniversidadesGroupByDescending = from u in universidades
                                                        orderby u.Universidad descending
                                                        select u.Universidad;
            //Agrupar con llave
            var universidadesGroupLLave = from u in universidades
                                          group u by u.Pais;

            //Recorrer el group by => se obtiene el mismo conportamiento con una expresion lamda
            foreach (var grupo in universidadesGroupLLave)
            {
                Console.WriteLine($"Universidad: {grupo.Key} \t {grupo.Count()}");
                foreach (var universidad in grupo)
                {
                    Console.WriteLine($"\t {universidad.Universidad}");// \t Es un espacio en blanco
                }
            }

            //Count con Operadores
            int numeroUniversidadesOperators = (from u in universidades
                                                select u.Universidad).Count();

            Console.WriteLine(numeroUniversidadesOperators);

            //Count con un Distinct
            int numeroUniversidadesOperatorsDistinct = (from u in universidades
                                                        select u.Pais).Distinct().Count();
            //Join con operadores de consulta
            var joinOperator = from u in universidades
                       join e in estudiantes
                       on u.Universidad equals e.Universidad
                       select new { e.Nombre, e.ApellidoPaterno, u.Pais };

            foreach (var joinOpe in joinOperator)
            {
                Console.WriteLine(joinOpe);
            }

            //Obtener una parte Take()
            var parte = estudiantes.Take(2); //Obtener las dos filas de estudiantes
            foreach (var estudiante in parte)
            {
                Console.WriteLine(estudiante.Nombre);
            }
            //Como obtener el 2 y 3 estudiante metodo SKIP => Significa saltar
            var parteSkip = estudiantes.Skip(1).Take(2); //Quiero saltar un registro con skip y tomar los siguientes dos elementos con take(2)

            //Metodo Any y devuelve un boolean si se cumple ya sea true o false
            var cualquier = estudiantes.Any(x => x.Nombre.StartsWith("H"));

            //Metodo All nos sirve para verficar si todos los registros cumplen con la verificacion que hacemos ahi que sean diferentes de vacio
            var todos = estudiantes.All(x => x.Nombre != "");

            //Obtener la primer coincidencia retorna el primer resultado si no cumple nos retornara una excepcion
            var primero = estudiantes.First(x => x.Nombre.StartsWith("H"));
            
            //Si no se encuentra nos devuelve un null
            var primeroDefault = estudiantes.FirstOrDefault(x => x.Nombre.StartsWith("H"));

            //Evaluacion Diferida
            var estudiantesEvaluacionDiferida = estudiantes
                .Select(x => x.Nombre);

            var estudiantesEvaluacionDiferidaTolist = estudiantes
                .Select(x => x.Nombre)
                .ToList(); //=> el tolist se refiere a una copia por lo cual no modifica a estudiante
            foreach (var est in estudiantesEvaluacionDiferida)
            {
                Console.WriteLine(est);
            }
            //Procederemos a modificar su valor y redefinimos su elemento y lo reemplazamos
            estudiantes[0] = new
            {
                EstudianteID = 1,
                Nombre = "Carlos",
                ApellidoPaterno = "Fajardo",
                Universidad = "Unipanamericana"
            };

            foreach (var est in estudiantesEvaluacionDiferida)
            {
                Console.WriteLine(est);
            }

            Console.ReadLine();
        }
    }
}