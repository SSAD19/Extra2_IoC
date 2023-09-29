using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using Entidades; 

namespace AccesoBase
{
    public class AccesoDB : IDisposable
    {

        private IDbConnection dbConex;
        const string conexString = "Server=127.0.0.1;Database=ExtradosBootcamp; User=root;";

       
        // metodo para abrir conexion
        public IDbConnection GetConnection()
        {
             dbConex = new MySqlConnection(conexString);

            if (dbConex.State != ConnectionState.Open)
            {
                dbConex.Open();
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



        public void AgregarUsuario(Usuario _userNuevo, AccesoDB conexion)
        {
                 var comando = "INSERT INTO Usuario (nombre, apellido, edad) VALUES (@Nombre, @Apellido, @Edad, @Activo)";

                 var conexion2 = conexion.GetConnection();
                 conexion2.Execute(comando, new { Nombre = _userNuevo.Nombre, Apellido = _userNuevo.Apellido, Edad = _userNuevo.Edad, Activo = true});

        }

        //bajaLogica 

        public void bajaLogica(int _id, AccesoDB conexion)
        {
            var comando = "UPDATE Usuario SET activo = false WHERE id = @Id";

            var conexion2 = conexion.GetConnection();
            conexion2.Execute(comando, new { Id = _id });

        }
        //Modificar  

        public void ModificarUser(Usuario _userNuevo, AccesoDB conexion)
        {
            var comando = "UPDATE Usuario SET nombre= @Nombre, apellido =@Apellido, edad =@Edad, activo =@Activo  WHERE id = @Id";

            var conexion2 = conexion.GetConnection();
            conexion2.Execute(comando, new {Nombre= _userNuevo.Nombre, Apellido = _userNuevo.Apellido, Edad= _userNuevo.Edad, Activo=_userNuevo.Activo});

        }

        //Eliminación física 
        public void EliminarUser(int _id, AccesoDB conexion)
        {
            var comando = "DELETE Usuario WHERE id = @Id";

            var conexion2 = conexion.GetConnection();
            conexion2.Execute(comando, new { Id = _id });

        }


        //consultar - recuperar datos 
        public  List<Usuario> ListarUsuario(AccesoDB conexion)
        {
                string comando = "select * from Usuario";

                var conexion2 = conexion.GetConnection();
                 return conexion2.Query<Usuario>(comando).ToList();
        }

    //revisr 

        public  Usuario VerUser(int _id, AccesoDB conexion)
        {
                string comando = "select * from Usuario where id=@Id";

                var conexion2 = conexion.GetConnection();
                return conexion2.QuerySingleOrDefault<Usuario>(comando, new{ Id = _id });
          
        }
    }
}