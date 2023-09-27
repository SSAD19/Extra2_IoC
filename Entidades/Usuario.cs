namespace Entidades
{
    public class Usuario 
    {
       public int Id { get;set ; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }

       public  Usuario(int _id, string _nombre, string _apellido, int _edad) {
            Id = _id;
            Nombre = _nombre;
            Apellido = _apellido;
            Edad = _edad; 
        }

       public  Usuario(string _nombre, string _apellido, int _edad)
        {
            Nombre = _nombre;
            Apellido = _apellido;
            Edad = _edad;
        }

        public Usuario() { }
    }
}