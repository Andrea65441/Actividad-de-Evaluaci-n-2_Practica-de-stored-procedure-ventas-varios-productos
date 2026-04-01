using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Venta_Form
{
    public partial class Form2 : Form
    {

        Conexion con = new Conexion();


       
        public string IdSeleccionado { get; set; }
        public string NombreSeleccionado { get; set; }
        public string PrecioSeleccionado { get; set; }

        public Form2()
        {
            InitializeComponent();
        }



        private void dataGridViewProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


           
            if (e.RowIndex >= 0)
            {
                
                IdSeleccionado = dataGridViewProductos.CurrentRow.Cells["Código"].Value.ToString();
                NombreSeleccionado = dataGridViewProductos.CurrentRow.Cells["Producto"].Value.ToString();
                PrecioSeleccionado = dataGridViewProductos.CurrentRow.Cells["Precio"].Value.ToString();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        

        
        
            private void Form2_Load(object sender, EventArgs e)
            {
                
                CargarProductos();
            }

        private void CargarProductos()
        {
            try
            {
                con.abrir();
                string consulta = "SELECT idProducto as Código, nombre as Producto, precio as Precio, existencia as Existencia FROM Articulo";
                SqlDataAdapter da = new SqlDataAdapter(consulta, con.leer);
                DataTable dt = new DataTable();
                da.Fill(dt);

                
                dataGridViewProductos.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            finally { con.cerrar(); }
        }

        
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
        
            DataTable dt = (DataTable)dataGridViewProductos.DataSource;
            if (dt != null)
            {
                
                dt.DefaultView.RowFilter = string.Format("Producto LIKE '%{0}%'", txtBuscar.Text.Trim().Replace("'", "''"));
            }
        }
       
        
        
        private void groupBox1_Enter(object sender, EventArgs e)
            {

            }
    }
}
