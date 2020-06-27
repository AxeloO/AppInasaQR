using System;
using System.Collections.Generic;
using System.Data;


namespace PruebaEscaner.Rdn
{
    public class RDNCarrito
    {
        List<Entidades.EntProductos> ListaDeProdcutoCarrito = new List<Entidades.EntProductos>();


        public void AgregarAListaCarrito(Entidades.EntProductos ProductoAgregar)
         {
            ListaDeProdcutoCarrito.Add(ProductoAgregar);
        }

        public void InicializarEntidades()
        {
            List<Entidades.EntProductos> ListaDeProdcutoCarrito = new List<Entidades.EntProductos>();
        }

        public DataTable MostrarBotonCarrito()
        {
            DataTable dtBtnCarrito = new DataTable();
            DataRow drCantidad = dtBtnCarrito.NewRow();
            DataRow drTotalPrecio = dtBtnCarrito.NewRow();
            DataColumn dtcCantidad = dtBtnCarrito.Columns.Add("Cantidad",typeof(int));
            DataColumn dycTotalPrecio = dtBtnCarrito.Columns.Add("TotalPrecio", typeof(double));                                    
            double ldbTotalPLecio = 0;


            foreach (var producto in ListaDeProdcutoCarrito)
            {
                double dbPrecioModeda = double.Parse(producto.precio);
                ldbTotalPLecio = ldbTotalPLecio + dbPrecioModeda;
            }

            drCantidad["Cantidad"] = ListaDeProdcutoCarrito.Count;
            drTotalPrecio["TotalPrecio"] = ldbTotalPLecio;

            dtBtnCarrito.Rows.Add(drCantidad);
            dtBtnCarrito.Rows.Add(drTotalPrecio);
            return dtBtnCarrito;
        }

        public DataTable MostrarBotonCarritoRefresh(List<Entidades.EntProductos> ListaDeProdcutoCarritoRefresh)
        {
            DataTable dtBtnCarrito = new DataTable();
            DataRow drCantidad = dtBtnCarrito.NewRow();
            DataRow drTotalPrecio = dtBtnCarrito.NewRow();
            DataColumn dtcCantidad = dtBtnCarrito.Columns.Add("Cantidad", typeof(int));
            DataColumn dycTotalPrecio = dtBtnCarrito.Columns.Add("TotalPrecio", typeof(double));
            double ldbTotalPLecio = 0;


            foreach (var producto in ListaDeProdcutoCarritoRefresh)
            {
                double dbPrecioModeda = double.Parse(producto.precio);
                ldbTotalPLecio = ldbTotalPLecio + dbPrecioModeda;
            }

            drCantidad["Cantidad"] = ListaDeProdcutoCarritoRefresh.Count;
            drTotalPrecio["TotalPrecio"] = ldbTotalPLecio;

            dtBtnCarrito.Rows.Add(drCantidad);
            dtBtnCarrito.Rows.Add(drTotalPrecio);
            return dtBtnCarrito;
        }

        public List<Entidades.EntProductos> MostrarTodosLosProductos()
        {
            return ListaDeProdcutoCarrito;
        }

        public List<Entidades.EntProductos> LeerProductosEnCarrito(List<Entidades.EntProductos> TodosLosProductos)
        {
            ListaDeProdcutoCarrito = TodosLosProductos;            
            return ListaDeProdcutoCarrito;
        }

    }
}
