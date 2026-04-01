using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Venta_Form
{
    //*internal class Conexion

    public class Conexion
    {
        // Usamos el nombre del servidor que encontramos: localhost,1437
        // Cambiamos Integrated Security por User ID y Password
        public SqlConnection leer = new SqlConnection("Data Source=localhost,1437; Initial Catalog=Store_BD; User ID=sa; Password=P@ssw0rd; TrustServerCertificate=True");

        public void abrir()
        {
            if (leer.State == ConnectionState.Closed)
                leer.Open();
        }

        public void cerrar()
        {
            if (leer.State == ConnectionState.Open)
                leer.Close();
        }
    }



}
