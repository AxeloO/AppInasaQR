using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace PruebaEscaner
{
    public partial class QRReaderPage : ContentPage
    {
        public QRReaderPage()
        {
            InitializeComponent();
        }

        private void EscanearQR(object sender, EventArgs e)
        {
            Scanner();
        }

        private void Scanner()
        {
           // var ScanerPage_OnScanResult = new ZXingScannerPage();

            //ScanerPage_OnScanResult.Title = "Lector De QR";
            //ScanerPage_OnScanResult.OnScanResult += 

        }

       
    }
}
