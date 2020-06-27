using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;


namespace PruebaEscaner
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        List<Entidades.EntProductos> ListaDeProductos;
        string strUsuario = string.Empty;
        string strPassword = string.Empty;

        public MainPage()
        {
            InitializeComponent();
            btIniciarSesion.Clicked += BtIniciarSesion_Clicked;
            btnConultarProducto.Clicked += btnConultarProducto_Clicked;
        }

        private async void BtIniciarSesion_Clicked(object sender, EventArgs e)
        {
            
            string strLigaConsultar = string.Empty;
            string resultadoLogin = string.Empty;
            string strIpDeConexion;
            string strMetodoCadena = "Login/";
            //strIpDeConexion = "http://192.168.0.7:8081/";
            //strIpDeConexion = "http://192.168.10.218:100/";
            //strIpDeConexion = "http://192.168.100.43:100/";
            strIpDeConexion = "http://192.168.100.24:100/";

            try
            {
                

            if (string.IsNullOrEmpty(entryUsuario.Text))
            {
                await DisplayAlert("Error", "El campo USUARIO no puede estar vacio", "Aceptar");
                entryUsuario.Focus();
                return;
            }
            if (string.IsNullOrEmpty(entryPassword.Text))
            {
                await DisplayAlert("Error", "El campo CONTRASEñA no puede estar vacio", "Aceptar");
                entryPassword.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(entryUsuario.Text) && !string.IsNullOrEmpty(entryPassword.Text))
            {
                    
                    
                    strLigaConsultar = strIpDeConexion + strMetodoCadena + entryUsuario.Text +"&"+ entryPassword.Text;

                    using (HttpClient cliente = new HttpClient())

                    {
                    btIniciarSesion.IsEnabled = false;
                    var respuestaLogind = await cliente.GetAsync(strLigaConsultar);
                    resultadoLogin = respuestaLogind.Content.ReadAsStringAsync().Result;
                    btIniciarSesion.IsEnabled = true;                    
                    }

                    var login = JsonConvert.DeserializeObject<Entidades.EntUsuario>(resultadoLogin);

                    if (login.validarUsuario)
                    {
                        Application.Current.Properties["NombreUsuarioLogin"] = login.usuario.ToString();
                        Application.Current.Properties["TieneProductoElCarrito"] = "NO";                      
                        await this.Navigation.PushModalAsync(new MenuConsultarCodigo(true, login.usuario, ListaDeProductos));
                    }
                    else
                    {
                        await DisplayAlert("Error", "usuario o contraseña no validos", "Aceptar");
                    }

            }
        }

        
                catch (Exception ex)
            {
                await DisplayAlert(ex.Message.ToString(), "A ocurrido un Error al conectar con el Servidor (Conexion Validar Usuario), favor de reportarlo con el administrador ", "OK");
            }
        }

        private void btnConultarProducto_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new MenuConsultarCodigo(false,"Invitado", ListaDeProductos));

        }
    }
}
