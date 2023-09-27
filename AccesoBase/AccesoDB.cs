using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

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
    }
}