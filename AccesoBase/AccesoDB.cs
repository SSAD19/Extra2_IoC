using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using System.Linq;
using Entidades;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace AccesoBase
{
    public class AccesoDB : IDisposable, IAcceso
    {
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

        public void AgregarUno(IUsuario usuario, IAcceso conexion) {

        string comando = "INSERT INTO UsuariosManager (nombre, apellido, edad, activo) VALUES (@Nombre, @Apellido, @Edad, @Activo)";

        try {
            var conexion2 = conexion.GetConnection();
            conexion2.Execute(comando, new { Nombre = usuario.Nombre, Apellido = usuario.Apellido, Edad = usuario.Edad, Activo = true });

        }
        catch (Exception e)
        {
            Console.WriteLine($"No se pudo generar el alta. ERROR: {e.Message}");
        }
    }

        public List<IUsuario> ListarUsuario(IAcceso conexion)
        {
            string comando = "select * from UsuariosManager";
            var lista = new List<IUsuario>();

            try
            {
                var conexion2 = conexion.GetConnection();
                var list =conexion2.Query<UsuariosManager>(comando).ToList();
               
                lista.AddRange(list.Cast<IUsuario>().ToList());

                Console.WriteLine("Se recuperó la información correctamente.");
               
                return lista; 

            }
            catch (Exception e)
            {

                Console.WriteLine($"No se pudo recuperar el listado solicitado. ERROR: {e.Message}");
                return lista; 
            }
            
        }
    }

  }
