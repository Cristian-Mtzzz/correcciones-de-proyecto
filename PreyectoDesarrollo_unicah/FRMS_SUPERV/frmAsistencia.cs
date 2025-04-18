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


        private void FrmAsiste_Load(object sender, EventArgs e)
        {
            lblPersona.Text = ACCIONES_BD.Persona();

            dgvAsiste = (dgvAsiste as DataGridView);

            FiltroInicial();

            dgvAsiste.CurrentCellDirtyStateChanged += (s, ev)
            => {
                if (dgvAsiste.IsCurrentCellDirty)
                    dgvAsiste.CommitEdit(DataGridViewDataErrorContexts.Commit);
               };

            ACCIONES_BD.tablaSupervisor(dgvAsiste);
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
            ACCIONES_BD.FiltrarDatosSuperv(txtDoc.Text, txtClase.Text, cmbAula.Text, cmbEdificio.Text, cmbSeccion.Text, dgvAsiste);

        }

        private void dgvAsiste_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
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
            }
        }

        private void dgvAsiste_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAsiste.Columns[e.ColumnIndex].Name == "AsistenciaHoy" && e.RowIndex >= 0)
                dgvAsiste.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
    }
}
