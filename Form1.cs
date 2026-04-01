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
    public partial class Form1 : Form
    
        {
            
            Conexion con = new Conexion();

            public Form1()
            {
                InitializeComponent();
            }

            private void Form1_Load(object sender, EventArgs e)
            {
                try
                {
                    con.abrir();
                    string consulta = "SELECT idcliente, nombre FROM Cliente";
                    SqlDataAdapter da = new SqlDataAdapter(consulta, con.leer);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboBoxCliente.DataSource = dt;
                    comboBoxCliente.DisplayMember = "nombre";
                    comboBoxCliente.ValueMember = "idcliente";
                    comboBoxCliente.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los clientes: " + ex.Message);
                }
                finally
                {
                    con.cerrar();
                }
            }

            private void btnAgregar_Click(object sender, EventArgs e)
            {


            
            Form2 ventana = new Form2();
            if (ventana.ShowDialog() == DialogResult.OK)
            {
               
                dataGridViewVenta.Rows.Add(
                    ventana.IdSeleccionado,
                    ventana.NombreSeleccionado,
                    ventana.PrecioSeleccionado,
                    1,
                    ventana.PrecioSeleccionado
                );
                CalcularTotal();
            }
        }
        
        
        
        

       
        private void CalcularTotal()
        {
            double total = 0;
            foreach (DataGridViewRow fila in dataGridViewVenta.Rows)
            {
                
                if (fila.Cells["Subtotal"].Value != null)
                {
                    total += Convert.ToDouble(fila.Cells["Subtotal"].Value);
                }
            }
            lblTotal.Text = total.ToString("N2");
        }

       
        private void label1_Click(object sender, EventArgs e) { }

            private void groupBox1_Enter(object sender, EventArgs e) { }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) 
            {
             
            if (comboBoxCliente.SelectedIndex == -1 || dataGridViewVenta.Rows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un cliente y agregue productos.");
                return;
            }

            try
            {
                con.abrir();
                
                SqlCommand cmd = new SqlCommand("sp_InsertarVenta", con.leer);
                cmd.CommandType = CommandType.StoredProcedure;

               
                cmd.Parameters.AddWithValue("@idcliente", comboBoxCliente.SelectedValue);

                
                cmd.Parameters.AddWithValue("@total", Convert.ToDecimal(lblTotal.Text));

                cmd.ExecuteNonQuery();
                MessageBox.Show("¡Venta guardada exitosamente en la base de datos!");

               
                dataGridViewVenta.Rows.Clear();
                lblTotal.Text = "0.00";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
            finally
            {
                con.cerrar();
            }
        }



            

        private void dataGridViewVenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
            
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
