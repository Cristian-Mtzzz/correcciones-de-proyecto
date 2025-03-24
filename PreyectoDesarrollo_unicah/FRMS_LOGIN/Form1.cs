using PreyectoDesarrollo_unicah.CLASES;
using PreyectoDesarrollo_unicah.FRMS_ADMIN;
using PreyectoDesarrollo_unicah.FRMS_SUPERV;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices; //Relacionado con Dll (Librería)


namespace PreyectoDesarrollo_unicah
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

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

        private void txtcontraseña_Enter(object sender, EventArgs e)
        {
            if (txtcontraseña.Text == "Contraseña:")
            {
                txtcontraseña.Text = "";
                txtcontraseña.UseSystemPasswordChar = true;
            }
        }

        private void txtcontraseña_Leave(object sender, EventArgs e)
        {
            if (txtcontraseña.Text == "")
            {
                txtcontraseña.Text = "Contraseña:";
                txtcontraseña.UseSystemPasswordChar = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usuario = txtusuario.Text;
            string contraseña = txtcontraseña.Text;

            if (usuario == "Usuario:")
            {
                txtusuario.Clear();
                MessageBox.Show("Usuario no puede quedar vacío.", "Error Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtusuario.Text = usuario;
                return;
            }

            using (SqlConnection conexion = new SqlConnection(CONEXION_BD.conectar.ConnectionString))
            {
                conexion.Open();
                using (SqlCommand cmd = new SqlCommand("PA_Admin_Save", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() && contraseña == "Contraseña:") // Verifica si hay usuario y contraseña para leer otros datos
                        {
                            if (MessageBox.Show("Saludos Administrador, no podemos otorgar el acceso con su contraseña vacía, ¿olvidó su contraseña?", "Contraseña vacía", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                            {
                                frmPierdoContraseña Lost = new frmPierdoContraseña();
                                this.Hide();
                                Lost.Show();
                            }
                            return;
                        }
                    }
                }
            }

            if (contraseña == "Contraseña:")
            {
                txtcontraseña.Clear();
                MessageBox.Show("Contraseña no puede quedar vacía, en caso de no obtener, consultar al administrador.", "Error Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtcontraseña.Text = contraseña;
                return;
            }

            using (SqlConnection conexion = new SqlConnection(CONEXION_BD.conectar.ConnectionString))
            {
                conexion.Open();

                using (SqlCommand cmd = new SqlCommand("PA_Login", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@contrasena", contraseña);

                    // Consulta para obtener el rol, nombre y apellido
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Verifica si hay usuario y contraseña para leer otros datos
                        {
                            string nombre = reader["nombre1"].ToString();
                            string apellido = reader["apellido1"].ToString();
                            string rolUsuario = reader["rol"].ToString();
                            string codigoDocente = usuario.ToString();
                            ACCIONES_BD.nombre = nombre;
                            ACCIONES_BD.apellido = apellido;
                            ACCIONES_BD.docente = codigoDocente;

                            MessageBox.Show($"Bienvenido(a), {nombre} {apellido}. Su rol es: {rolUsuario}", "Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            if (rolUsuario == "administrador")
                            {
                                // Abrir la pantalla del administrador
                                frmAdmin admin = new frmAdmin();
                                admin.Show();
                                this.Hide();
                            }
                            else if (rolUsuario == "supervisor")
                            {
                                // Abrir las pantallas del supervisor
                                frmSupervisor supervisor = new frmSupervisor();
                                supervisor.Show();
                                this.Hide();
                            }
                            else if (rolUsuario == "decano")
                            {
                                // Abrir las pantallas del decano
                                frmDecano decano = new frmDecano();
                                decano.Show();
                                this.Hide();
                            }
                            else if (rolUsuario == "docente")
                            {
                                // Abrir las pantallas del docente
                                frmDocente doc = new frmDocente();
                                doc.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Rol no reconocido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            /*Cambio variables porque por ejemplo en cmd al colocarlo, "else" que es un proceso
                            del using no permite repetir la misma variable, se hace ambiguo*/
                            using (SqlCommand CMD = new SqlCommand("PA_Admin_Save", conexion)) 
                            {
                                CMD.CommandType = CommandType.StoredProcedure;
                                CMD.Parameters.AddWithValue("@Usuario", usuario);

                                reader.Close();
                                using (SqlDataReader reading = CMD.ExecuteReader())
                                {
                                    if (reading.Read())
                                    {
                                        // Usuario o contraseña incorrectos
                                        if (MessageBox.Show("Saludos Administrador, su contraseña es incorrecta, ¿olvidó su contraseña?", "Contraseña incorrecta", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                                        {
                                            frmPierdoContraseña Lost = new frmPierdoContraseña();
                                            this.Hide();
                                            Lost.Show();
                                        }
                                        return;
                                    }
                                }
                            }
                            MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            //este es para poder mover el form
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0); //El evento en memoria se mantiene
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //este es para poder mover el form
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0); //El evento en memoria se mantiene
        }


        private void button2_Click(object sender, EventArgs e)
        {
            frmDocente doc = new frmDocente();
            doc.Show();
        }

        private void txtusuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones validar = new Validaciones();

            validar.ValidarUsuario(e, txtusuario);
        }

        private void txtcontraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones validar = new Validaciones();
            validar.ValidarContraseña(e, txtcontraseña);
        }
    }
}
