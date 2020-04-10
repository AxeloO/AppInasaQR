using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace PruebaEscaner
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            btIniciarSesion.Clicked += BtIniciarSesion_Clicked;
            btnConultarProducto.Clicked += btnConultarProducto_Clicked;
        }

        private async void BtIniciarSesion_Clicked(object sender, EventArgs e)
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

            await DisplayAlert("Version de Prueba 1.0.0 ", "Esta es una version de Prueba, favor de solicitar Version Completa", "OK");
        }

        private void btnConultarProducto_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new MenuConsultarCodigo());

        }
    }
}
