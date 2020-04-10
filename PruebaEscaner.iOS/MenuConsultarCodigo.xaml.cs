
using System;
using System.Collections.Generic;
using Newtonsoft;

using Xamarin.Forms;

namespace PruebaEscaner
{
    public partial class MenuConsultarCodigo : ContentPage
    {
        public MenuConsultarCodigo()
        {
            InitializeComponent();
            btnAtras.Clicked += BtnAtras_Clicked;
            btnBusquedaManual.Clicked += BtnBusquedaManual_Clicked;           
            opcionBusquedaPicker.Items.Add("Codigo Inasa");
            opcionBusquedaPicker.Items.Add("Codigo Original");
            opcionBusquedaPicker.Items.Add("Item Almacen Ford");
            opcionBusquedaPicker.Items.Add("Codigo Ford");
            
        }

        private async void BtnBusquedaManual_Clicked(object sender, EventArgs e)
        {
            var httpClient = new 
        }

        private void BtnAtras_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new MainPage());
        }

        private void opcionBusquedaPicker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            var name = opcionBusquedaPicker.Items[opcionBusquedaPicker.SelectedIndex];
            DisplayAlert(name, "Selected value", "OK");
        }

        

    }
}
