using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using Xamarin.Forms.Core;



using System.Collections.ObjectModel;


using Xamarin.Forms;

namespace PruebaEscaner
{
    public partial class ListaDeProductosPage : ContentPage
    {
        
        public ListaDeProductosPage(Entidades.EntProductos producto)
        {
            InitializeComponent();
            btnSalir.Clicked += btnSalir_Clicked;
            btnAgregarLista.Clicked += BtnAgregarLista_Clicked;
            btnCarrito.Clicked += BtnCarrito_Clicked;


            lblcodigO_INASA.Text = producto.codigO_INASA;
            lblcodigO_Original.Text = producto.codigO_ORIGINAL;

            imgProducto = LoadImage(producto.imageN_BASE64);

            //lblCosto_MXN.Text = producto.costO_MXN;
            lblItem_Almacen_Ford.Text = producto.iteM_ALMACEN_FORD;
            lblcodigO_Ford.Text = producto.codigO_FORD;
            lblDescripcion.Text = producto.descrip;
            lblblanket.Text = producto.blanket;
            lblprecio_Venta.Text = producto.preciO_VENTA;
            //lblsustituto.Text = producto.sustituto;
            //lblcosto_sustituto.Text = producto.costo_sustituto;
            lblExistencia.Text = producto.existtencia;

        }

        private void BtnCarrito_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new CarritoPage());
        }

        private void BtnAgregarLista_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Se Agrego el Articulo", "Favor de consultar Carrito", "OK");
        }


        private void btnSalir_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new MenuConsultarCodigo(false));
        }


        
        public Image LoadImage(string strImagen)
        {
            Image image = new Image();

            var byteArray = Convert.FromBase64String(strImagen);
            Stream stream = new MemoryStream(byteArray);
            var imageSource = ImageSource.FromStream(() => stream);            
            image.Source = imageSource;

            return image;

        }

    }
}
