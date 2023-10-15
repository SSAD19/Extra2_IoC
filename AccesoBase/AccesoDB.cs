using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using System.Linq;
using Entidades;
using System.Runtime.CompilerServices;

namespace AccesoBase
{
    public class AccesoDB : IDisposable
    {
       // private IUsuario _userNuevo { get; set;}


        private IDbConnection dbConex;
        const string conexString = "Server=127.0.0.1;Database=ExtradosBootcamp; User=root;";

       
        // metodo para abrir conexion
        public IDbConnection GetConnection()
        {
            try {

                dbConex = new MySqlConnection(conexString);

                if (dbConex.State != ConnectionState.Open)
                {
                    dbConex.Open();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"No se pudoconectar a la base de datos. ERROR: {e.Message}"); 
            }

            return dbConex;
        }


        public void Dispose()
        {
            if (dbConex != null && dbConex.State != ConnectionState.Closed)
            {
                dbConex.Close();
            }

            dbConex.Dispose();
        }

        //consultar - recuperar datos 
        public  List<IUsuario> ListarUsuario(AccesoDB conexion)
        {
            string comando = "select * from Usuario";
            try
            {
                var conexion2 = conexion.GetConnection();
               return conexion2.Query<IUsuario>(comando).ToList();
            }
            catch (Exception e) {

                 Console.WriteLine($"No se pudo recuperar el listado solicitado. ERROR: {e.Message}");

                return new List<IUsuario>();
            }
            
        }
    

        public void AgregarUno(IUsuario usuario, AccesoDB conexion) {

        string comando = "INSERT INTO Usuario (nombre, apellido, edad, activo) VALUES (@Nombre, @Apellido, @Edad, @Activo)";

        try {
            var conexion2 = conexion.GetConnection();
            conexion2.Execute(comando, new { Nombre = usuario.Nombre, Apellido = usuario.Apellido, Edad = usuario.Edad, Activo = true });

        }
        catch (Exception e)
        {
            Console.WriteLine($"No se pudo generar el alta. ERROR: {e.Message}");
        }
    }
        

        }

        //revisr 


  }
