using Entidades;
using Autofac;
using Dapper;
using System.Linq;
using AccesoBase;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Bcpg;
using static System.Formats.Asn1.AsnWriter;

namespace extradosClase4
{
    class Program 
    {

        //Contenedor Autofac 
        private static IContainer Contenedor() {

            var builder = new ContainerBuilder();

            builder.RegisterType<AccesoDB>();
            builder.RegisterType<UsuariosManager>().As<IUsuario>();

            return builder.Build();

        }

        static void Main(string[] args) 
        {
            // quiero traer un listado de usuario o UsuariosManager 
            IUsuario user; 
            var _contenedor = Contenedor();

            Console.WriteLine("Práctica de Inyección de dependencias ");

            //   AgregarUser();

            var listado = ObtenerListado(Contenedor().Resolve<IUsuario>().ObtenerListadodeUsuario());

            Console.WriteLine(listado);

            Console.WriteLine("funca o no funca? ");
            Console.ReadLine(); 




            void AgregarUser() {
                string _nombre = null;
                string _apellido =null;
                int _edad = -1; 

                do
                {
                    if (_nombre == null ) {
                        Console.WriteLine("Indique nombre");
                        _nombre = Console.ReadLine();
                    }
                    if (_apellido == null)
                    {
                        Console.WriteLine("Indique apellido");
                        _apellido = Console.ReadLine();
                    }
                    if (_edad < 0) {
                        Console.WriteLine("Indique edad");
                        int.TryParse(Console.ReadLine(), out _edad);
                    }
                  
                } while (string.IsNullOrEmpty(_nombre) || string.IsNullOrEmpty(_apellido) || _edad<0);

                using (var scope = Contenedor().BeginLifetimeScope()) {
                    user = Contenedor().Resolve<IUsuario>().AgregarUno(_nombre, _apellido, _edad);
                }
                using (var conexion = Contenedor().Resolve<AccesoDB>())
                {
                   
                    Contenedor().Resolve<AccesoDB>().AgregarUno(user, conexion);
                }
                
                Console.WriteLine($"El usuario {_nombre} fue cargado"); 

            }


            List<IUsuario> ObtenerListado(List<IUsuario> lista)
            {
                using (var conexion = Contenedor().Resolve<AccesoDB>())
                {
                    return lista = Contenedor().Resolve<AccesoDB>().ListarUsuario(conexion);
                }
            }
          
        }

    }
}

/*
 * 
 * 
 *  List<IUsuario> ObtenerListado()
            {
                
                using (var conexion = new AccesoDB())
                {
                    return Contenedor().Resolve<AccesoDB>().ListarUsuario(conexion);
                }
            }

        //Primer código que s ehizo para el DAO
            List<Usuario> variosUsuarios = new List<Usuario>();

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

 * */
