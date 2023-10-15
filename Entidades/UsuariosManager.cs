using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class UsuariosManager : Usuario 
    {
        public UsuariosManager(){  }


        public override UsuariosManager AgregarUno(string _nombre, string _apellido, int _edad)
        {
            return new UsuariosManager() { Nombre = _nombre, Apellido = _apellido, Edad = _edad, Activo = true };
        }

        public override UsuariosManager VerUno()
        {
            throw new NotImplementedException();
        }

        public override List<IUsuario> ObtenerListadodeUsuario()
        {
            return new List<IUsuario>();
        }

         
    }
}

/*
 *  public UsuariosManager(int _id, string _nombre, string _apellido,  bool _act)
        {
            Id = _id;
            Nombre = _nombre;
            Apellido = _apellido;
            Activo = _act;
        }
*/
