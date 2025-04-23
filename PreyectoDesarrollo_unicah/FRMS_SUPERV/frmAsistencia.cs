using DocumentFormat.OpenXml.Wordprocessing;
using PreyectoDesarrollo_unicah.CLASES;
using PreyectoDesarrollo_unicah.FRMS_SUPERV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PreyectoDesarrollo_unicah
{
    public partial class frmAsistencia : Form
    {
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture(); //Externo por la importación realizada en comando

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        public frmAsistencia()
        {
            InitializeComponent();
            this.MouseDown += frmSupervisor_MouseDown;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            frmSupervisor Menu = new frmSupervisor();
            Menu.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //Procesos en carga del formulario
        private void FiltroInicial()
        {
            cmbEdificio.SelectedIndex = 0;
            cmbAula.SelectedIndex = 0;
        }



        private void CargarAsistencias()
        {
            dgvAsiste.Columns.Clear();
            dgvAsiste.Rows.Clear();

            using (SqlConnection con = new SqlConnection(CONEXION_BD.conexion))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("PA_ObtenerAsistencia", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvAsiste.DataSource = dt;

                    // Detectar columnas de asistencia por nombre y poner checkbox
                    for (int i = dgvAsiste.Columns.Count - 1; i >= 0; i--)
                    {
                        DataGridViewColumn col = dgvAsiste.Columns[i];
                        if (col.Name.StartsWith("Semana") && col.ValueType == typeof(bool))
                        {
                            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn
                            {
                                Name = col.Name,
                                HeaderText = col.HeaderText,
                                DataPropertyName = col.DataPropertyName,
                                TrueValue = true,
                                FalseValue = false,
                                IndeterminateValue = false
                            };
                            int index = col.Index;
                            dgvAsiste.Columns.RemoveAt(i); // Elimina por índice
                            dgvAsiste.Columns.Insert(index, chk);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la asistencia: " + ex.Message);
                }
            }
        }




        private void FrmAsiste_Load(object sender, EventArgs e)
        {
            lblPersona.Text = ACCIONES_BD.Persona();
            CargarAsistencias(); // Ya carga y convierte columnas en checkbox

            dgvAsiste.CurrentCellDirtyStateChanged += (s, ev) =>
            {
                if (dgvAsiste.IsCurrentCellDirty)
                    dgvAsiste.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
        }






        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            frmSupervisor Menu = new frmSupervisor();
            Menu.Show();
        }

        private void frmSupervisor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void Filtros(object sender, EventArgs e)
        {
            // ACCIONES_BD.FiltrarDatosSuperv(txtDoc.Text, txtClase.Text, cmbAula.Text, cmbEdificio.Text, cmbSeccion.Text, dgvAsiste);

        }

        private void dgvAsiste_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            /*/if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var columna = dgvAsiste.Columns[e.ColumnIndex];
            if (columna.Name == "AsistenciaHoy")
            {
                var fila = dgvAsiste.Rows[e.RowIndex];

                string docente = fila.Cells[0].Value?.ToString();
                string asignatura = fila.Cells[1].Value?.ToString();
                string seccion = fila.Cells[2].Value?.ToString();
                string aula = fila.Cells[3].Value?.ToString();
                string edificio = fila.Cells[4].Value?.ToString();
                bool marca = Convert.ToBoolean(fila.Cells["AsistenciaHoy"].Value);

                ACCIONES_BD.RegistrarAsistencia(dgvAsiste, docente, asignatura, seccion, aula, edificio, marca);
            }*/
        }

        private void dgvAsiste_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*if (dgvAsiste.Columns[e.ColumnIndex].Name == "AsistenciaHoy" && e.RowIndex >= 0)
                dgvAsiste.CommitEdit(DataGridViewDataErrorContexts.Commit);*/
        }




        // boton de guardar asistencia
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                CONEXION_BD conexionBD = new CONEXION_BD();
                conexionBD.abrir();

                foreach (DataGridViewRow row in dgvAsiste.Rows)
                {
                    if (row.IsNewRow) continue;

                    SqlCommand cmd = new SqlCommand("PA_InsertarActualizarAsistencia", CONEXION_BD.conectar);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@codigo_empleado", Convert.ToInt32(row.Cells["codigo_empleado"].Value));
                    cmd.Parameters.AddWithValue("@cod_Asignatura", row.Cells["cod_Asignatura"].Value?.ToString());

                    for (int semana = 1; semana <= 4; semana++)
                    {
                        string[] dias = { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes" };
                        foreach (string dia in dias)
                        {
                            string columna = $"Semana{semana}_{dia}";
                            bool valor = false;

                            if (row.Cells[columna].Value != DBNull.Value && row.Cells[columna].Value != null)
                                valor = Convert.ToBoolean(row.Cells[columna].Value);

                            cmd.Parameters.AddWithValue($"@{columna}", valor);
                        }
                    }

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Asistencia guardada correctamente.");
                conexionBD.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar asistencia: " + ex.Message);
            }
        }

    }
}
