using System.Collections.Generic;


namespace Entidades
{
    public class UsuarioComun : Usuarios
    {
        public override IUsuario AgregarUno(string _nombre, string _apellido, int _edad)
        {
            return new UsuarioComun() { Nombre = _nombre, Apellido = _apellido, Edad = _edad, Activo = true };
        }

        public override List<IUsuario> ObtenerListadodeUsuario()
        {
            throw new NotImplementedException();
        }

        public override IUsuario VerUno()
        {
            return new UsuarioComun();
        }
    }
    }



