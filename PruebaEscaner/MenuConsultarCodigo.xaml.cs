using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

using Xamarin.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaEscaner
{
    public partial class MenuConsultarCodigo : ContentPage
    {
        string strTipoDeBusqueda = string.Empty;
        ListaDeProductosPage _listaDeProductosPage = new ListaDeProductosPage();

        public MenuConsultarCodigo()
        {
            InitializeComponent();

            btnAtras.Clicked += BtnAtras_Clicket;
            btnBusquedaManual.Clicked += BtnBusquedaManual_Clicket;
            //para el Picker
            pickerTipoBusqueda.Items.Add("Codigo Inasa");
            pickerTipoBusqueda.Items.Add("Codigo Original");
            pickerTipoBusqueda.Items.Add("Item Almacen Ford");
            pickerTipoBusqueda.Items.Add("Codigo Ford");

            //variables globales

        }

        private void pickerTipoBusqueda_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            strTipoDeBusqueda = pickerTipoBusqueda.Items[pickerTipoBusqueda.SelectedIndex];

        }

        private void BtnAtras_Clicket(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new MainPage());
        }

        private void BtnBusquedaManual_Clicket(object sender, EventArgs e)
        {
            string strIpDeConexion = string.Empty;
            string strMetodoCadena = string.Empty;
            string strOpcionElegida = string.Empty;
            string strCodigoIndresado = string.Empty;

            try
            {
                //cadeja de jorge
                //strIpDeConexion = "http://192.168.0.12:8081/";
                strIpDeConexion = "http://192.168.100.18:100/";
                strMetodoCadena = "LeerExcel/";
                strOpcionElegida = strTipoDeBusqueda;
                strCodigoIndresado = entryBusquedaManual.Text;
                var strLigaConsultar = string.Empty;


                if (string.IsNullOrEmpty(strOpcionElegida))
                {
                    DisplayAlert("Debe elegir un tipo de Busqueda", "Opcion no Seleccionada", "OK");
                }
                if (string.IsNullOrEmpty(strCodigoIndresado))
                {
                    DisplayAlert("Debe ingresar el codigo a buscar", "Opcion no Seleccionada", "OK");
                }
                if (!string.IsNullOrEmpty(strCodigoIndresado) & !string.IsNullOrEmpty(strOpcionElegida))
                {


                    switch (strOpcionElegida)
                    {
                        case "Codigo Inasa":
                            strLigaConsultar = strIpDeConexion + strMetodoCadena + "1&" + strCodigoIndresado;
                            obtenerProductos(strLigaConsultar);
                            break;
                        case "Codigo Original":
                            strLigaConsultar = strIpDeConexion + strMetodoCadena + "2&" + strCodigoIndresado;
                            break;
                        case "Item Almacen Ford":
                            strLigaConsultar = strIpDeConexion + strMetodoCadena + "3&" + strCodigoIndresado;
                            break;
                        case "Codigo Ford":
                            strLigaConsultar = strIpDeConexion + strMetodoCadena + "4&" + strCodigoIndresado;
                            break;
                        default:
                            break;
                    }


                }


            }
            catch (Exception ex)
            {
                DisplayAlert(ex.Message.ToString(), "A ocurrido un Error, favor de reportarlo con el administrador " + strMetodoCadena, "OK");
            }

        }

        private async void obtenerProductos(string strLigaConsultar)

        {
            try
            {
                btnAtras.IsEnabled = false;
                HttpClient client = new HttpClient();
                var respuestaprodcutos = await client.GetAsync(strLigaConsultar);
                var ResultadoProductos = respuestaprodcutos.Content.ReadAsStringAsync().Result;
                btnAtras.IsEnabled = true;                               

                if (!string.IsNullOrEmpty(ResultadoProductos))
                {
                    var productos = JsonConvert.DeserializeObject<Entidades.EntProductos>(ResultadoProductos);

                    if (productos.error.Equals("2"))
                    {
                        await DisplayAlert("No se encontraron Resultados", "Favor de verifivar el codigo o buscar otro producto", "OK");
                    }

                    if (productos.error.Equals("0"))
                    {

                       bool boolRespuesa =_listaDeProductosPage.ObtenerProducto(productos);

                        if (boolRespuesa)
                        {
                            this.Navigation.PushModalAsync(new ListaDeProductosPage());
                        }
                        
                       

                    }

                }
              
            }

            catch (Exception ex)
            {
               await  DisplayAlert(ex.Message.ToString(), "A ocurrido un Error al conectar con el Servidor (ConsumiendoExcel), favor de reportarlo con el administrador " , "OK");
            }

            
            
        }

    }


}
