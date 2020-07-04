using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using ZXing.Net.Mobile.Forms;

using System.Data;
using Xamarin.Forms;


using Plugin.Permissions.Abstractions;
using Plugin.Permissions;


namespace PruebaEscaner
{
    public partial class MenuConsultarCodigo : ContentPage
    {
        string strTipoDeBusqueda = string.Empty;
        bool validarUsuario = false;
        private List<Entidades.EntProductos>  ListaDeProductosEnviar;
        DataTable dtCalcularCarrito = new DataTable();
        Rdn.RDNCarrito rDNCarrito = new Rdn.RDNCarrito();

        public MenuConsultarCodigo(bool boolUsuario, string strusuario, List<Entidades.EntProductos> ListaDeProductos)
        {
            InitializeComponent();
            ListaDeProductosEnviar = ListaDeProductos;
            btnAtras.Clicked += BtnAtras_Clicket;
            btnBusquedaManual.Clicked += BtnBusquedaManual_Clicket;
            btnBusquedaPorCodigo.Clicked += BtnBusquedaPorCodigo_Clicked;
            btnCarritoCompra.Clicked += BtnCarritoCompra_Clicked;
            //para el Picker
            pickerTipoBusqueda.Items.Add("Codigo Inasa");
            pickerTipoBusqueda.Items.Add("Codigo Original");           
            pickerTipoBusqueda.Items.Add("Codigo Ford");
            lblUsrlogin.Text = strusuario;

            //variables globales

            validarUsuario = boolUsuario;

        }

        private void BtnCarritoCompra_Clicked(object sender, EventArgs e)
        {
            try
            {
                if(Application.Current.Properties["NombreUsuarioLogin"] == "Invitado")
                {
                    DisplayAlert("Advertencia Para ingresar al carrito de compras debe Iniciar sesion primero", "Favor de comunicarse con el administrador de la aplicacion para generar su Usuario", "OK");
                }
                else
                {
                    if (Application.Current.Properties["TieneProductoElCarrito"] == "SI")
                    {
                        rDNCarrito.LeerProductosEnCarrito(ListaDeProductosEnviar);
                        ListaDeProductosEnviar = rDNCarrito.MostrarTodosLosProductos();
                        this.Navigation.PushModalAsync(new CarritoPage(ListaDeProductosEnviar));
                    }
                    else
                    {
                        DisplayAlert("No cuenta con Productos en el Carrito", "Favor de consultar Carrito", "OK");
                    }
                }                
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void BtnBusquedaPorCodigo_Clicked(object sender, EventArgs e)
        {
            try
            {

                string strIpDeConexion = string.Empty;
                string strMetodoCadena = string.Empty;
                string strCodigoEscaneado = string.Empty;

                var scannerPage = new ZXingScannerPage();
                scannerPage.Title = "Lector de QR";

                var status = await CrossPermissions.Current.CheckPermissionStatusAsync<CameraPermission>();
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Se necesitan permisos para usar la camara", "¿Desea otorgar permisos a la camara?", "OK");
                    }

                    status = await CrossPermissions.Current.RequestPermissionAsync<CameraPermission>();
                }

                if (status == PermissionStatus.Granted)
                {
                    scannerPage.OnScanResult += (result) =>
                    {
                        scannerPage.IsScanning = false;
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Navigation.PopModalAsync();

                            //strIpDeConexion = "http://192.168.0.7:8081/";
                            strIpDeConexion = "http://192.168.100.43:100/";
                            //strIpDeConexion = "http://192.168.10.218:100/";
                            //strIpDeConexion = "http://192.168.100.24:100/";
                            strMetodoCadena = "LeerExcel/";
                            strCodigoEscaneado = result.Text;
                            var strLigaConsultar = string.Empty;

                            strLigaConsultar = strIpDeConexion + strMetodoCadena + "2&" + strCodigoEscaneado;
                            obtenerProductos(strLigaConsultar);                 
                        });
                    };
                    await Navigation.PushModalAsync(scannerPage);
                }

                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Se necesitan permisos para usar la camara", "Favor de Otorgar permisos a la camara", "OK");
                }
            }

            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");

            }

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
                //strIpDeConexion = "http://192.168.0.7:8081/";
                strIpDeConexion = "http://192.168.100.43:100/";
                //strIpDeConexion = "http://192.168.10.218:100/";
                //strIpDeConexion = "http://192.168.100.24:100/";
                strMetodoCadena = "LeerExcel/";
                strOpcionElegida = strTipoDeBusqueda;
                strCodigoIndresado = entryBusquedaManual.Text;
                string strLigaConsultar = string.Empty;


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
                            obtenerProductos(strLigaConsultar);
                            break;                        
                        case "Codigo Ford":
                            strLigaConsultar = strIpDeConexion + strMetodoCadena + "3&" + strCodigoIndresado;
                            obtenerProductos(strLigaConsultar);
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
                var ResultadoProductos = string.Empty;
                btnAtras.IsEnabled = false;
                btnBusquedaManual.IsEnabled = false;
                btnBusquedaPorCodigo.IsEnabled = false;

                using (HttpClient cliente = new HttpClient())

                {
                    var respuestaprodcutos = await cliente.GetAsync(strLigaConsultar);
                    ResultadoProductos = respuestaprodcutos.Content.ReadAsStringAsync().Result;
                    btnAtras.IsEnabled = true;
                    btnBusquedaManual.IsEnabled = true;
                    btnBusquedaPorCodigo.IsEnabled = true;
                }


                if (!string.IsNullOrEmpty(ResultadoProductos))
                {
                    var productos = JsonConvert.DeserializeObject<Entidades.EntProductos>(ResultadoProductos);

                    if (productos.error.Equals("2"))
                    {
                        await DisplayAlert("No se encontraron resultados", "Favor de verifivar el codigo o buscar otro producto", "OK");
                    }

                    if (productos.error.Equals("0"))
                    {
                        await this.Navigation.PushModalAsync(new ListaDeProductosPage(productos, ListaDeProductosEnviar));
                    }

                }

            }

            catch (Exception ex)
            {
                await DisplayAlert(ex.Message.ToString(), "A ocurrido un Error al conectar con el Servidor (ConsumiendoExcel), favor de reportarlo con el administrador ", "OK");
            }



        }

        private void ConsultarSiHayProductos()
        {
            try
            {
                string strCantidad = string.Empty;
                string strTotalPrecio = string.Empty;

                if (validarUsuario)
                {
                    if (ListaDeProductosEnviar != null)
                    {
                        if (ListaDeProductosEnviar.Count != 0)
                        {
                            DisplayAlert("Cuenta con Productos en el Carrito", "Favor de consultar Carrito", "OK");

                            dtCalcularCarrito = rDNCarrito.MostrarBotonCarritoRefresh(ListaDeProductosEnviar);

                            strCantidad = dtCalcularCarrito.Rows[0]["Cantidad"].ToString();
                            strTotalPrecio = dtCalcularCarrito.Rows[1]["TotalPrecio"].ToString();

                            btnCarritoCompra.Text = strCantidad + " Articulos : " + strTotalPrecio;


                        }

                    }
                }
                else
                {
                    DisplayAlert("Debe Iniciar Sesion", "", "OK");
                }
            }

            catch (Exception ex)
            {

            }
        }




    }


}
