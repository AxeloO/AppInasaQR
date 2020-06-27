using System;
using System.Collections.Generic;

using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PruebaEscaner
{
    public partial class ListaDeProductosPage : ContentPage
    {
        private string strcodigO_INASA;
        private string codigO_ORIGINAL;
        private string imagen;
        private string imageN_BASE64;
        private string costO_MXN;
        private string iteM_ALMACEN_FORD;
        private string codigO_FORD;
        private string descrip;
        private string blanket;
        private string preciO_VENTA;
        private string sustituto;
        private string imagen_sustituto;
        private string costo_sustituto;
        private string existtencia;
        private string error;
        private string descripcionError;


        public ListaDeProductosPage()
        {
            InitializeComponent();
            btnSalir.Clicked += btnSalir_Clicked;

            lblcodigO_INASA.Text = strcodigO_INASA;


        }   

        private void BtnSalir_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public bool ObtenerProducto(Entidades.EntProductos producto)
        {

             strcodigO_INASA = producto.codigO_INASA;
            //lvListaDeProductos = producto.codigO_ORIGINAL;


            return true;
        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}
