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
        public static IContainer Contenedor() {

            var builder = new ContainerBuilder();

            builder.RegisterType<AccesoDB>().As<IAcceso>();
            builder.RegisterType<UsuariosManager>().As<IUsuario>();

            return builder.Build();

        }

        static void Main(string[] args) 
        {
            // quiero traer un listado de usuario o UsuariosManager 
            IUsuario user; 
           

            Console.WriteLine("Práctica de Inyección de dependencias ");

            //   AgregarUser();

            List<IUsuario> listado = new List<IUsuario>();


            listado = ObtenerListado();

            foreach (IUsuario usuario in listado)
            {
                Console.WriteLine($"Usuario {usuario.Nombre} {usuario.Apellido}");
            }

            Console.WriteLine("yo sé que ahora sí");
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
                using (Contenedor().BeginLifetimeScope())
                {
                    var conexion = Contenedor().Resolve<IAcceso>();
                    Contenedor().Resolve<IAcceso>().AgregarUno(user, conexion);
                }
                
                Console.WriteLine($"El usuario {_nombre} fue cargado"); 

            }

          List<IUsuario> crearLista() {
                using (var scope = Contenedor().BeginLifetimeScope()) {
                   return Contenedor().Resolve<IUsuario>().ObtenerListadodeUsuario();
                }
               
            }

          List<IUsuario> ObtenerListado()
            {
               var lista = crearLista();

                using (Contenedor().BeginLifetimeScope())
                {
                    var conexion = Contenedor().Resolve<IAcceso>(); 
                   return lista = Contenedor().Resolve<IAcceso>().ListarUsuario(conexion);
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
