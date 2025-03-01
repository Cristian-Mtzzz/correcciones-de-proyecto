using System.Data.SqlClient;
using System.Runtime.InteropServices;


namespace PreyectoDesarrollo_unicah
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        frmOlvideDatos contra = new frmOlvideDatos();

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtusuario_Enter(object sender, EventArgs e)
        {
            //se a�ade un tipo de marca de agua a los txtbox
            if (txtusuario.Text == "Usuario:")
            {
                txtusuario.Text = "";                
            }
        }

        private void txtusuario_Leave(object sender, EventArgs e)
        {
            if (txtusuario.Text == "")
            {
                txtusuario.Text = "Usuario:";                
            }
        }

        private void txtcontrase�a_Enter(object sender, EventArgs e)
        {
            if (txtcontrase�a.Text == "Contrase�a:")
            {
                txtcontrase�a.Text = "";                
                txtcontrase�a.UseSystemPasswordChar = true;
            }
        }

        private void txtcontrase�a_Leave(object sender, EventArgs e)
        {
            if (txtcontrase�a.Text == "")
            {
                txtcontrase�a.Text = "Contrase�a:";
                txtcontrase�a.UseSystemPasswordChar = false;
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usuario = txtusuario.Text.Trim();
            string contrase�a = txtcontrase�a.Text.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrase�a) ||
                usuario == "Usuario:" || contrase�a == "Contrase�a:") //Vac�o con o sin un dato
            {
                MessageBox.Show("Por favor ingrese todos los datos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Realizar la conexi�n y consulta
            try
            {
                string cadenaConexion = "Server=DESKTOP-F4DAE1B\\SQLEXPRESS;Database=Supervision_Unicah;Integrated Security=True;TrustServerCertificate=True;";
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    // Consulta para obtener el rol, nombre y apellido
                    string consulta = "SELECT nombre_empleado, apellido_empleado, rol FROM Empleados WHERE usuario = @usuario AND contrase�a = @contrasena";
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@contrasena", contrase�a);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Si coincide usuario y contrase�a, y hay datos del select
                            {
                                string nombre = reader["nombre_empleado"].ToString();
                                string apellido = reader["apellido_empleado"].ToString();
                                string rolUsuario = reader["rol"].ToString();

                                MessageBox.Show($"Bienvenido, {nombre} {apellido}. Tu rol es: {rolUsuario}", "Inicio de Sesi�n", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (rolUsuario == "supervisor")
                                {
                                    // Abrir la pantalla del supervisor
                                    FrmReporte supervisor = new FrmReporte();
                                    supervisor.Show();
                                    this.Hide();
                                }
                                else if (rolUsuario == "decano")
                                {
                                    // Abrir las pantallas del decano
                                    frmJustificaci�n decano = new frmJustificaci�n();
                                    decano.Show();
                                    this.Hide();
                                }
                                else if (rolUsuario == "docente")
                                {
                                    // Abrir las pantallas del docente
                                    FrmDocente docente = new FrmDocente();
                                    docente.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Rol no reconocido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                // Usuario o contrase�a incorrectos
                                MessageBox.Show("Usuario o contrase�a incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtusuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            //este es para poder mover el form
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //este es para poder mover el form
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            contra.ShowDialog();
            this.Dispose(false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}
