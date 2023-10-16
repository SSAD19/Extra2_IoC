using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoBase
{
    public interface IAcceso
    { 
        public IDbConnection GetConnection();
        public void Dispose();
        public void AgregarUno(IUsuario usuario, IAcceso conexion); 
        public List<IUsuario> ListarUsuario(IAcceso conexion); 
    }
}
