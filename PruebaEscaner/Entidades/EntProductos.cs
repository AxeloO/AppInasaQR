using System;
namespace PruebaEscaner.Entidades
{
    public class EntProductos
    {
        public string codigO_ORIGINAL { get; set; }
        public string codigO_INASA { get; set; }
        public string codigO_FORD { get; set; }
        public string imagen { get; set; }
        public string imageN_BASE64 { get; set; }
        public string descrip { get; set; }
        public string unidaD_DE_MEDIDA { get; set; }
        public string inventariO_FORD { get; set; }
        public string inventariO_INASA { get; set; }
        public string statuS_FORD { get; set; }
        public string statuS_INASA { get; set; }
        public string blanket { get; set; }
        public string precio { get; set; }
        public string preciO_MONEDA { get; set; }        
        public string statuS_FABRICANTE { get; set; }
        public string sustituto { get; set; }
        public string imagen_sustituto { get; set; }
        public string costo_sustituto { get; set; }
        public string error { get; set; }
        public string descripcionError { get; set; }
    }
}
