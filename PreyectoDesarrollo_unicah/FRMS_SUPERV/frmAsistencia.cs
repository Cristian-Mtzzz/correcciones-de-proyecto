﻿using PreyectoDesarrollo_unicah.CLASES;
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

        DataGridView Asistes;
        public frmAsistencia()
        {
            InitializeComponent();
            this.MouseDown += frmSupervisor_MouseDown;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            cmbHora.SelectedIndex = 0;
        }

        private void LimiteMes()
        {
            int año = DateTime.Now.Year;

            // Definir el rango (20 enero - 18 abril del año actual), académicamente
            mesSupervisor.MinDate = new DateTime(año, 1, 20);
            mesSupervisor.MaxDate = new DateTime(año, 4, 18);
        }

        private void FrmAsiste_Load(object sender, EventArgs e)
        {
            lblPersona.Text = ACCIONES_BD.nombre + " " + ACCIONES_BD.apellido;

            dgvAsiste = (dgvAsiste as DataGridView);

            //Ajustes del formulario
            LimiteMes();
            FiltroInicial();

            //Ajustes del bdd
            ACCIONES_BD.tablaSupervisor(dgvAsiste);
            ACCIONES_BD.CargarAsistenciaSuperv(mesSupervisor, (string)dgvAsiste.CurrentRow.Cells[0].Value, (string)dgvAsiste.CurrentRow.Cells[1].Value, (string)dgvAsiste.CurrentRow.Cells[2].Value, (string)dgvAsiste.CurrentRow.Cells[3].Value, (string)dgvAsiste.CurrentRow.Cells[4].Value);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            frmSupervisor Menu = new frmSupervisor();
            Menu.Show();
        }

        private void frmSupervisor_MouseDown(object sender, MouseEventArgs e) //Evento del ratón "e"
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0); //El evento en memoria se mantiene
            }
        }

        private void mesSupervisor_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime fechaSeleccionada = e.Start.Date;

            if (MessageBox.Show("¿Marcar asistencia para esta fecha?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ACCIONES_BD.RegistrarAsistencia(dgvAsiste, (string)dgvAsiste.CurrentRow.Cells[0].Value, (string)dgvAsiste.CurrentRow.Cells[1].Value, (string)dgvAsiste.CurrentRow.Cells[2].Value, (string)dgvAsiste.CurrentRow.Cells[3].Value, (string)dgvAsiste.CurrentRow.Cells[4].Value, fechaSeleccionada.Date, true);
                mesSupervisor.AddBoldedDate(fechaSeleccionada);
                mesSupervisor.UpdateBoldedDates();
            }
            else
            {
                ACCIONES_BD.RegistrarAsistencia(dgvAsiste, (string)dgvAsiste.CurrentRow.Cells[0].Value, (string)dgvAsiste.CurrentRow.Cells[1].Value, (string)dgvAsiste.CurrentRow.Cells[2].Value, (string)dgvAsiste.CurrentRow.Cells[3].Value, (string)dgvAsiste.CurrentRow.Cells[4].Value, fechaSeleccionada.Date, false);
                mesSupervisor.RemoveBoldedDate(fechaSeleccionada);
                mesSupervisor.UpdateBoldedDates();
            }
        }

        private void dgvAsiste_SelectionChanged(object sender, EventArgs e) //Método seguro para almacenar asistencias
        {
            if (dgvAsiste.CurrentRow != null)
            {
                // Extraer los valores de la fila seleccionada.
                string Docente = dgvAsiste.CurrentRow.Cells[0].Value.ToString();
                string clase = dgvAsiste.CurrentRow.Cells[1].Value.ToString();
                string seccion = dgvAsiste.CurrentRow.Cells[2].Value.ToString();
                string aula = dgvAsiste.CurrentRow.Cells[3].Value.ToString();
                string edificio = dgvAsiste.CurrentRow.Cells[4].Value.ToString();

                // Limpiar las fechas resaltadas previas en el MonthCalendar.
                mesSupervisor.RemoveAllBoldedDates();

                // Llama al método para cargar las fechas marcadas para ese registro.
                ACCIONES_BD.CargarAsistenciaSuperv(mesSupervisor, Docente, clase, seccion, aula, edificio);
            }
        }

        private void txtClase_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones validar = new Validaciones();

            validar.ValidarFiltro(e, txtClase);
        }

        private void txtDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones validar = new Validaciones();

            validar.ValidarFiltro(e, txtDoc);
        }
    }
}
