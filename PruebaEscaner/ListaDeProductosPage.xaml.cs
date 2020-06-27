using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using Xamarin.Forms.Core;
using System.Data;



using System.Collections.ObjectModel;


using Xamarin.Forms;

namespace PruebaEscaner
{

    public partial class ListaDeProductosPage : ContentPage
    {
        //List<Entidades.EntProductos> ListaDeProdcutoCarrito = new List<Entidades.EntProductos>();
        List<Entidades.EntProductos> ListaDeProdcutoCarrito;
        DataTable dtCalcularCarrito = new DataTable();
        Entidades.EntProductos productoActual = new Entidades.EntProductos();
        Rdn.RDNCarrito rDNCarrito = new Rdn.RDNCarrito();
        private bool CarritoConProducto = false;


        public ListaDeProductosPage(Entidades.EntProductos producto, List<Entidades.EntProductos> ListaDeProductos)
        {
            ListaDeProdcutoCarrito = ListaDeProductos;

            InitializeComponent();
            ConsultarSiHayProductos();

            productoActual = producto;
            btnSalir.Clicked += btnSalir_Clicked;
            btnAgregarLista.Clicked += BtnAgregarLista_Clicked;
            btnCarrito.Clicked += BtnCarrito_Clicked;


            lblcodigO_Original.Text = producto.codigO_ORIGINAL;
            lblcodigO_INASA.Text = producto.codigO_INASA;
            lblcodigO_Ford.Text = producto.codigO_FORD;
            imgProducto.Source = LoadImage(producto.imageN_BASE64);
            lblDescripcion.Text = producto.descrip;
            lblUnidad_De_Medida.Text = producto.unidaD_DE_MEDIDA;
            lblInventario_Ford.Text = producto.inventariO_FORD;
            lblInventario_Inasa.Text = producto.inventariO_INASA;
            lblEstatus_Ford.Text = producto.statuS_FORD;
            lblEstatus_Inasa.Text = producto.statuS_INASA;
            lblblanket.Text = producto.blanket;
            lbl_Precio_Moneda.Text = producto.preciO_MONEDA;
            lblEstatus_Fabrica.Text = producto.statuS_FABRICANTE;


        }

        private void ConsultarSiHayProductos()
        {
            try
            {
                string strCantidad = string.Empty;
                string strTotalPrecio = string.Empty;

                if (ListaDeProdcutoCarrito != null)
                {
                    if (ListaDeProdcutoCarrito.Count != 0)
                    {
                        DisplayAlert("Ya cuenta con Productos en el Carrito", "Favor de consultar Carrito", "OK");

                        dtCalcularCarrito = rDNCarrito.MostrarBotonCarritoRefresh(ListaDeProdcutoCarrito);

                        strCantidad = dtCalcularCarrito.Rows[0]["Cantidad"].ToString();
                        strTotalPrecio = dtCalcularCarrito.Rows[1]["TotalPrecio"].ToString();

                        btnCarrito.Text = strCantidad + " Articulos : " + strTotalPrecio;
                       
                    }

                }
            }

            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void BtnCarrito_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Application.Current.Properties["NombreUsuarioLogin"] == "Invitado")
                {
                    DisplayAlert("Advertencia Para ingresar al carrito de compras debe Iniciar sesion primero", "Favor de comunicarse con el administrador de la aplicacion para generar su Usuario", "OK");
                }
                else
                {
                    if (Application.Current.Properties["TieneProductoElCarrito"] == "SI")

                    {
                        rDNCarrito.LeerProductosEnCarrito(ListaDeProdcutoCarrito);
                        ListaDeProdcutoCarrito = rDNCarrito.MostrarTodosLosProductos();
                        this.Navigation.PushModalAsync(new CarritoPage(ListaDeProdcutoCarrito));
                    }
                    else
                    {
                        ListaDeProdcutoCarrito = rDNCarrito.MostrarTodosLosProductos();
                        this.Navigation.PushModalAsync(new CarritoPage(ListaDeProdcutoCarrito));
                    }
                }
                
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
            

        }

        private void BtnAgregarLista_Clicked(object sender, EventArgs e)
        {
            try
            {
                string strCantidad = string.Empty;
                string strTotalPrecio = string.Empty;

                if (Application.Current.Properties["TieneProductoElCarrito"] == "SI")
                {
                    rDNCarrito.LeerProductosEnCarrito(ListaDeProdcutoCarrito);
                    rDNCarrito.AgregarAListaCarrito(productoActual);
                    DisplayAlert("Se Agrego el Articulo", "Favor de consultar Carrito", "OK");
                    dtCalcularCarrito = rDNCarrito.MostrarBotonCarrito();


                    strCantidad = dtCalcularCarrito.Rows[0]["Cantidad"].ToString();
                    strTotalPrecio = dtCalcularCarrito.Rows[1]["TotalPrecio"].ToString();

                    btnCarrito.Text = strCantidad + " Articulos : " + strTotalPrecio;
                }
                else
                {
                    rDNCarrito.AgregarAListaCarrito(productoActual);
                    DisplayAlert("Se Agrego el Articulo", "Favor de consultar Carrito", "OK");
                    dtCalcularCarrito = rDNCarrito.MostrarBotonCarrito();


                    strCantidad = dtCalcularCarrito.Rows[0]["Cantidad"].ToString();
                    strTotalPrecio = dtCalcularCarrito.Rows[1]["TotalPrecio"].ToString();

                    btnCarrito.Text = strCantidad + " Articulos : " + strTotalPrecio;
                }                                            

            }

            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }


        private void btnSalir_Clicked(object sender, EventArgs e)
        {
            try
            {
                string strCantidad = string.Empty;
                dtCalcularCarrito = rDNCarrito.MostrarBotonCarrito();
                strCantidad = dtCalcularCarrito.Rows[0]["Cantidad"].ToString();

                if (strCantidad.Equals("0"))
                {
                    Application.Current.Properties["TieneProductoElCarrito"] = "NO";
                    ListaDeProdcutoCarrito = rDNCarrito.MostrarTodosLosProductos();
                    this.Navigation.PushModalAsync(new MenuConsultarCodigo(true, "Prueba", ListaDeProdcutoCarrito));
                    //this.Navigation.PopModalAsync(); 
                }
                else
                {
                    Application.Current.Properties["TieneProductoElCarrito"] = "SI";
                    ListaDeProdcutoCarrito = rDNCarrito.MostrarTodosLosProductos();
                    this.Navigation.PushModalAsync(new MenuConsultarCodigo(true, "Prueba", ListaDeProdcutoCarrito));
                    //this.Navigation.PopModalAsync(); 
                }

            }

            catch (Exception ex)
            {

            }

        }



        public ImageSource LoadImage(string strImagen)
        {
            Image image = new Image();

            var byteArray = Convert.FromBase64String(strImagen);
            Stream stream = new MemoryStream(byteArray);
            var imageSource = ImageSource.FromStream(() => stream);
            //image.Source = imageSource;

            return imageSource;

        }

    }


}





