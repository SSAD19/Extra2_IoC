using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public abstract class  Usuarios:IUsuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public bool Activo { get; set; }



        public abstract IUsuario AgregarUno(string _nombre, string _apellido, int _edad);
        public abstract List<IUsuario> ObtenerListadodeUsuario();
        public abstract IUsuario VerUno();
        
    }
}
