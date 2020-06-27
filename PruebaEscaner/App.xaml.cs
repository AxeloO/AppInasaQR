using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PruebaEscaner
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            Current.Properties["TieneProductoElCarrito"] = "NO";
            Current.Properties["NombreUsuarioLogin"] = "Invitado";
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
