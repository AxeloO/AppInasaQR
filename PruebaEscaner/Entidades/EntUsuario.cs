using System;
namespace PruebaEscaner.Entidades
{
    public class EntUsuario
    {
        public string id { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public string perfil { get; set; }
        public string error { get; set; }
        public string descripcionError { get; set; }
        public bool validarUsuario { get; set; }
    }
}
