using PreyectoDesarrollo_unicah.CLASES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices; //Relacionado con Dll (Librer�a)
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PreyectoDesarrollo_unicah
{
    public partial class frmDocente : Form
    {
        public frmDocente()
        {
            InitializeComponent();
            dgvDoc.AutoGenerateColumns = true;

        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 Login = new Form1();
            Login.Show();
        }

        private void cargarDatosEmpleadoYClase()
        {
            CONEXION_BD conexionBD = new CONEXION_BD();
            conexionBD.abrir();

            try
            {
                // Preparar la consulta o el procedimiento almacenado
                string query = "sp_MostrarDatosEmpleadoYClase";  // El nombre de tu procedimiento almacenado

                // Usar la conexión abierta en 'conexionBD.conectar'
                using (SqlCommand cmd = new SqlCommand(query, CONEXION_BD.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Configurar el adaptador de datos para llenar el DataTable
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    // Llenar el DataTable con los datos provenientes del procedimiento almacenado
                    da.Fill(dt);

                    // Verificar si se llenaron datos
                    if (dt.Rows.Count > 0)
                    {
                        // Asignar los datos al DataGridView
                        dgvDoc.DataSource = dt;  // dgvDoc es el DataGridView para mostrar los datos
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron datos para mostrar.", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción y mostrar un mensaje de error
                MessageBox.Show($"Error al cargar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Cerrar la conexión
                conexionBD.cerrar();
            }
        }




        private void frmDocente_Load(object sender, EventArgs e) //Método del formulario
        {
            // Ajuste de formulario
            lblPersona.Text = ACCIONES_BD.Persona();
            cargarDatosEmpleadoYClase();
        }





        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 Login = new Form1();
            Login.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void dgvDoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmDocente_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void lblPersona_Click(object sender, EventArgs e)
        {
            lblPersona.Text = ACCIONES_BD.nombre + " " + ACCIONES_BD.apellido;
        }


        private void dgvDoc_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void mesDoc_DateSelected(object sender, DateRangeEventArgs e)
        {

        }

        private void sqlDataReaderBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
