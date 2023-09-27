using Entidades;
using Dapper;
using AccesoBase;
using System.Security.Cryptography.X509Certificates;

namespace extradosClase4
{
    class Program 
    {
        static void Main(string[] args) 
        {
             List<Usuario> variosUsuarios = new List<Usuario>();

            AgregarUsuario();
            AgregarUsuario();
           


            variosUsuarios = ListarUsuario();

            int i = 0; 
            foreach (var usuario in variosUsuarios)
            {
                i++; 
                Console.WriteLine($" Usuario {i} ={usuario.Nombre}");
            }

            Console.WriteLine("Ingrese el ID del usuario a buscar : ");
            int idUser = int.Parse(Console.ReadLine());

            Usuario verUno = new Usuario();
            verUno = verUsuario(idUser);
           
            Console.WriteLine($" Usuario {verUno.Id} = {verUno.Nombre}, {verUno.Apellido}, {verUno.Edad}");


            //traer listado de productos  



            static void AgregarUsuario() {
                using (var conexion = new AccesoDB())
                {

                    var conexion2 = conexion.GetConnection();
                        Console.WriteLine("Nombre del usuario: ");
                        string nombre = Console.ReadLine();
                        Console.WriteLine("Apellido del usuario: ");
                        string apellido = Console.ReadLine();
                        Console.WriteLine("Edad del usuario: ");
                        int edad = int.Parse(Console.ReadLine());

                        Usuario userNuevo = new Usuario(nombre, apellido, edad);


                        var comando = "INSERT INTO Usuario (nombre, apellido, edad) VALUES (@Nombre, @Apellido, @Edad)";

                        conexion2.Execute(comando, userNuevo);

                        Console.WriteLine("Se agregó un nuevo usuario"); 
                    
                }
            }


           static List<Usuario>  ListarUsuario()
            {
                using (var conexion = new AccesoDB())
                {
                         string comando = "select * from Usuario";

                        var conexion2 = conexion.GetConnection(); 
                        var usuarios = conexion2.Query<Usuario>(comando).ToList();

                        return usuarios;
                }
            }

            static Usuario verUsuario(int _id)
            {
                using (var conexion = new AccesoDB())
                {
                    string comando = "select * from Usuario where id=@Id";

                    var conexion2 = conexion.GetConnection();
                    var usuario = conexion2.QuerySingleOrDefault<Usuario>(comando, new { Id = _id });

                    return usuario;
                }
            }





        }

    }
}