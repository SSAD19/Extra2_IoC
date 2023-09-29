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

            Console.WriteLine("Ingrese el ID del usuario a eliminar : "); //baja lógica
            idUser = int.Parse(Console.ReadLine());
            BajaUsuario(idUser);

            Console.WriteLine("Gracias por utilizar  nuetsro sistema.");

            //traer listado de productos  

            //Alta 
            void AgregarUsuario()
            {
                
                Console.WriteLine("Nombre del usuario: ");
                string nombre = Console.ReadLine();
                Console.WriteLine("Apellido del usuario: ");
                string apellido = Console.ReadLine();
                Console.WriteLine("Edad del usuario: ");
                int edad = int.Parse(Console.ReadLine());

                Usuario userNuevo = new Usuario(nombre, apellido, edad, true);

                using (var conexion = new AccesoDB())
                {
                    // llamar metodo 
                   conexion.AgregarUsuario(userNuevo, conexion); 
                   Console.WriteLine("Se agregó un nuevo usuario");
                }
            }


            // baja logica 
            void BajaUsuario(int _id)
            {
                using (var conexion = new AccesoDB())
                {
                  conexion.bajaLogica(_id, conexion);

                }
            }

            //modificar Usuario
            void ModifUsuario(Usuario userNuevo)
            {

                Console.WriteLine($"Nombre del usuario:{userNuevo.Nombre}.Indique nuevo nombre:");
                userNuevo.Nombre = Console.ReadLine();
                Console.WriteLine($"Apellido del usuario:{userNuevo.Apellido}. Indique nuevo apellido: ");
                userNuevo.Apellido = Console.ReadLine();
                Console.WriteLine($"Edad del usuario: {userNuevo.Edad}. Indique nueva edad:");
                userNuevo.Edad = int.Parse(Console.ReadLine());


                using (var conexion = new AccesoDB())
                {
                    // llamar metodo 
                    conexion.ModificarUser(userNuevo, conexion);

                    Console.WriteLine($"Se modificó el usuario {userNuevo.Id} - {userNuevo.Nombre} ");
                }
            }

            //consultar listado

            List<Usuario> ListarUsuario()
            {
                using (var conexion = new AccesoDB())
                {
                    return conexion.ListarUsuario(conexion);
                }
            }

            //consultar un usuario
            Usuario verUsuario(int _id)
            {
                using (var conexion = new AccesoDB())
                {
                  return  conexion.VerUser(_id, conexion);
                    
                }
            }





        }

    }
}