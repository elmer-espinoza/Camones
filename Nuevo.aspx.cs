using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Drawing.Imaging;

namespace Facturacion
{
    public partial class Nuevo : System.Web.UI.Page
    {

        //string constr = "Server=LENOVO;initial catalog=Camones;Trusted_Connection=yes;";
        string constr = System.Configuration.ConfigurationManager.ConnectionStrings["FacturaConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                txtFecha.Text = DateTime.Now.ToString("yyyyMMdd");
                txtFecha.ReadOnly = true;
                txtCantidad.Text = "1";
                lblPrecio.Text = "0.00";
                DataSet ds = new DataSet();
                SqlCommand cmd;
                string query = "";
                SqlConnection cnn = new SqlConnection(constr);

                cnn.Open();

                query = "SELECT MAX(Factura_id) + 1 as Factura_id FROM[Facturacion].[dbo].[Factura]";
                SqlDataAdapter dan = new SqlDataAdapter(query, cnn);
                dan.Fill(ds, "Last");
                txtNumero.Text = ds.Tables["Last"].Rows[0]["Factura_id"].ToString();


                query = "SELECT * FROM [Facturacion].[dbo].[Cliente] order by razon_social";
                SqlDataAdapter da = new SqlDataAdapter(query, cnn);
                da.Fill(ds, "Clientes");
                cboCliente.DataSource = ds.Tables["Clientes"];
                cboCliente.DataTextField = "razon_social";
                cboCliente.DataValueField = "cliente_id";
                cboCliente.DataBind();

                query = "SELECT * FROM [Facturacion].[dbo].[Moneda]";
                SqlDataAdapter dam = new SqlDataAdapter(query, cnn);
                dam.Fill(ds, "Monedas");
                cboMoneda.DataSource = ds.Tables["Monedas"];
                cboMoneda.DataTextField = "descripcion";
                cboMoneda.DataValueField = "moneda_id";
                cboMoneda.DataBind();

                query = "SELECT * FROM [Facturacion].[dbo].[Producto] order by descripcion";
                SqlDataAdapter dap = new SqlDataAdapter(query, cnn);
                dap.Fill(ds, "Productos");
                cboProducto.DataSource = ds.Tables["Productos"];
                cboProducto.DataTextField = "descripcion";
                cboProducto.DataValueField = "producto_id";
                cboProducto.DataBind();

                cnn.Close();

                DataTable dt = new DataTable();

                dt.Columns.Add(new DataColumn("Sec", typeof(Int32)));
                dt.Columns.Add(new DataColumn("Codigo", typeof(String)));
                dt.Columns.Add(new DataColumn("Descripcion", typeof(String)));
                dt.Columns.Add(new DataColumn("Cantidad", typeof(Int32)));
                dt.Columns.Add(new DataColumn("Precio", typeof(Double)));
                dt.Columns.Add(new DataColumn("SubTotal", typeof(Double)));

                Session.Add("Temp", dt);

            }

            RefreshItems();
        }


        protected void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "SELECT * FROM [Facturacion].[dbo].[Cliente] where cliente_id = " + cboCliente.SelectedValue;
            SqlConnection cnn = new SqlConnection(constr);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(query, cnn);
            cnn.Open();
            da.Fill(ds, "Cliente");
            lblDireccion.Text = ds.Tables["Cliente"].Rows[0]["Direccion"].ToString();
            lblRuc.Text = ds.Tables["Cliente"].Rows[0]["Ruc"].ToString();
            cnn.Close();
        }

        protected void cboProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "SELECT * FROM [Facturacion].[dbo].[Producto] where producto_id = " + cboProducto.SelectedValue;
            SqlConnection cnn = new SqlConnection(constr);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(query, cnn);
            cnn.Open();
            da.Fill(ds, "Producto");
            lblPrecio.Text = ds.Tables["Producto"].Rows[0]["Precio"].ToString();
            cnn.Close();
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            {

                DataTable dt = (DataTable)Session["Temp"];

                DataRow dr;

                dr = dt.NewRow();
                dr[0] = dt.Rows.Count + 1;
                dr[1] = cboProducto.SelectedItem.Value;
                dr[2] = cboProducto.SelectedItem.Text;
                dr[3] = Int32.Parse(txtCantidad.Text);
                dr[4] = Double.Parse(lblPrecio.Text);
                dr[5] = Double.Parse(lblPrecio.Text) * Int32.Parse(txtCantidad.Text);
                dt.Rows.Add(dr);

                RefreshItems();

            }
        }

        protected void btnRemover_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["Temp"];
            dt.Rows.Clear();
            Session.Add("Temp", dt);
            RefreshItems();
        }

        protected void RefreshItems() {
            DataTable dt = (DataTable)Session["Temp"];

            Double vv = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                vv = vv + Int32.Parse(dt.Rows[i]["Cantidad"].ToString()) * Double.Parse(dt.Rows[i]["Precio"].ToString());
            }

            lblTotalValorVenta.Text = vv.ToString();
            lblTotalImpuestos.Text = (vv * 0.18).ToString();
            lblTotalFactura.Text = (vv * 1.18).ToString();

            DataView dv = new DataView(dt);
            DataGrid dgItems = new DataGrid();

            TemplateColumn chk = new TemplateColumn();
            chk.HeaderText = "Sel";
            dgItems.Columns.Add(chk);

            dgItems.DataSource = dv;
            dgItems.DataBind();

            dgItems.CellPadding = 15;
            dgItems.CellSpacing = 10;

            phItems.Controls.Clear();

            if (dt.Rows.Count > 0) {
                phItems.Controls.Add(dgItems);
            }

        }


        protected void btnGrabar_Click(object sender, EventArgs e)
        {

            string query = "";
            query = "INSERT INTO [Facturacion].[dbo].[Factura] ([Factura_id], [Fecha], [Cliente_id], [Razon_social], [Ruc], [Moneda_id], [Total_Valor_Venta], [Total_Impuestos], [Total_Factura]) VALUES ("+txtNumero.Text+", '"+txtFecha.Text +"', "+cboCliente.SelectedValue+", '"+cboCliente.SelectedItem.Text.Trim()+"', '"+lblRuc.Text.Trim() +"', "+cboMoneda.SelectedValue +", "+lblTotalValorVenta.Text+", "+lblTotalImpuestos.Text+", "+lblTotalFactura.Text+") ";
            SqlConnection cnn = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(query, cnn);

            cnn.Open();

            cmd.ExecuteNonQuery();

            DataTable dt = (DataTable)Session["Temp"];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                query = "INSERT INTO [Facturacion].[dbo].[Detalle]  ([Factura_id], [Secuencia], [Producto_id], [Cantidad], [Precio_Unitario]) VALUES (" + txtNumero.Text +", "+ dt.Rows[i]["Sec"].ToString() + ", "+ dt.Rows[i]["Codigo"].ToString() + ", "+ dt.Rows[i]["Cantidad"].ToString() + ", "+ dt.Rows[i]["Precio"].ToString() + ")";
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
            }

            cnn.Close();

            Response.Redirect("Documentos.aspx");
        }


        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        // dgItems.Columns.Add(CreaBoundColumn("Codigo", "Codigo"));
        // dgItems.Columns.Add(CreaBoundColumn("Descripcion", "Descripcion"));
        // dgItems.Columns.Add(CreaBoundColumn("Cantidad", "Cantidad"));
        // dgItems.Columns.Add(CreaBoundColumn("Precio", "Precio"));
        // dgItems.Columns.Add(CreaBoundColumn("SubTotal", "Subtotal"));


        BoundColumn CreaBoundColumn(String Campo, String Cabecera)
        {

            BoundColumn columna = new BoundColumn();
            columna.DataField = Campo;
            columna.HeaderText = Cabecera;
            return columna;
        }

    }
}