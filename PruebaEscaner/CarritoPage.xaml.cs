using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Collections.ObjectModel;
using System.IO;

namespace PruebaEscaner
{

    public partial class CarritoPage : ContentPage
    {
        List<Entidades.EntProductos> ListaDeProductoCotizador;
        Rdn.RDNCarrito rDNCarrito = new Rdn.RDNCarrito();
        
        public CarritoPage(List<Entidades.EntProductos> ListaDeProdcutoCarrito)
        {            
            InitializeComponent();
            ListaDeProductoCotizador = ListaDeProdcutoCarrito;         
            lstProductos.ItemsSource = ListaDeProdcutoCarrito;
            

            //lstProductos.ItemsSource = BindingContext;            
        }

        public void LlenarListaDeProductosCotizar(List<Entidades.EntProductos> ListaDeProdcutoCarrito) {

            List<Entidades.EntProductos> ListaDeProductoCotizador  = ListaDeProdcutoCarrito;
            //return ListaDeProductoCotizador;
        }

        private async void btnCotizacion_Clicked(System.Object sender, System.EventArgs e)
        {
            
            try
            {
                List<string> Destinatarios = new List<string>();
                Destinatarios.Add("Jcasarrubias@inasa.com.mx");
                string strCuerpoCorreo = string.Empty;
                strCuerpoCorreo = CuerpoCotizacion(ListaDeProductoCotizador);


                var mensaje = new EmailMessage {
                    Subject = "Cotizacion",
                    Body = strCuerpoCorreo,
                    To = Destinatarios,               
                    
                };

                await Email.ComposeAsync(mensaje);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public string CuerpoCotizacion(List<Entidades.EntProductos> ListadeProductos){

            string TextoCompleto = string.Empty;
            string strPrecioFinal = string.Empty;
            double douPreciototal = 0;
            

            try
            {
                foreach (var item in ListadeProductos)
                {
                    
                    TextoCompleto = TextoCompleto + "Codigo Inasa : " + item.codigO_INASA + "\r" + "\n" + "Precio del ITEM : " + item.preciO_MONEDA + "\r" + "\n" + "Descripcion del ITEM : " + item.descrip + "\r" + "\n" + "---------------------" + "\r" + "\n";
                    douPreciototal = douPreciototal + Convert.ToDouble(item.precio);
                    
                }

                TextoCompleto = TextoCompleto + "\r" + "\n" + "Precio Total de cotizacion :  " + douPreciototal.ToString();
            }
            catch (Exception ex)
            {
                 DisplayAlert("Error", ex.Message, "OK");
            }

            return TextoCompleto;
        }

        private void btnAtras_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                string strCantidad = string.Empty;
               

                if (strCantidad.Equals("0"))
                {
                    Application.Current.Properties["TieneProductoElCarrito"] = "NO";
                    ListaDeProductoCotizador = rDNCarrito.MostrarTodosLosProductos();
                    this.Navigation.PushModalAsync(new MenuConsultarCodigo(true, "Prueba", ListaDeProductoCotizador));
                    //this.Navigation.PopModalAsync(); 
                }
                else
                {
                    Application.Current.Properties["TieneProductoElCarrito"] = "SI";                    
                    this.Navigation.PushModalAsync(new MenuConsultarCodigo(true, "Prueba", ListaDeProductoCotizador));
                    //this.Navigation.PopModalAsync(); 
                }

            }

            catch (Exception ex)
            {
                
            }
        }
        
        
        public ImageSource LoadImage(string strImagen)
        {           
            var byteArray = Convert.FromBase64String(strImagen);
            Stream stream = new MemoryStream(byteArray);
            var imageSource = ImageSource.FromStream(() => stream);
            //image.Source = imageSource;

            
            return imageSource;
            

        }

        private void EliminarProductoRepetido()
        {
            string strItemSeleccionado = string.Empty;
           // strItemSeleccionado = lstProductos.ItemSelected;
        }

    }
}
