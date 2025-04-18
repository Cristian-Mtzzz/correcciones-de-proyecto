using PreyectoDesarrollo_unicah.CLASES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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


        private void frmDocente_Load(object sender, EventArgs e) //M�todo del formulario
        {
            //Ajuste de forulario
            lblPersona.Text = ACCIONES_BD.Persona();

            //Ajustes de BDD
            ACCIONES_BD.Periodo(mesDoc);
            dgvDoc.AutoGenerateColumns = true;
            ACCIONES_BD.tabla_docente(dgvDoc, ACCIONES_BD.empleado);
            ACCIONES_BD.CargarAsistenciaDoc(
                mesDoc,
                (string)dgvDoc.CurrentRow.Cells[0].Value,
                (string)dgvDoc.CurrentRow.Cells[1].Value,
                (string)dgvDoc.CurrentRow.Cells[2].Value,
                (string)dgvDoc.CurrentRow.Cells[3].Value);
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
            if (dgvDoc.CurrentRow != null)
            {
                // Extraer los valores de la fila seleccionada.
                string Clase = dgvDoc.CurrentRow.Cells[0].Value.ToString();
                string seccion = dgvDoc.CurrentRow.Cells[1].Value.ToString();
                string aula = dgvDoc.CurrentRow.Cells[2].Value.ToString();
                string edificio = dgvDoc.CurrentRow.Cells[3].Value.ToString();

                // Limpiar las fechas resaltadas previas en el MonthCalendar.
                mesDoc.RemoveAllBoldedDates();

                // Llama al m�todo para cargar las fechas marcadas para ese registro.
                ACCIONES_BD.CargarAsistenciaDoc(mesDoc, Clase, seccion, aula, edificio);
            }
        }

        private void mesDoc_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime fechaSeleccionada = e.Start.Date;
            // Definir la fecha de inicio del primer parcial
            DateTime fechaInicio = new DateTime(DateTime.Now.Year, 1, 20); // 20 de enero

            // Calcular la diferencia en d�as
            int offsetDias = (fechaSeleccionada - fechaInicio).Days; // Puede ser negativo si est� antes del 20/ene

            // Cada semana son 7 d�as
            // Tenemos 12 semanas en total (3 parciales * 4 semanas)
            // Rango total: 0 <= offsetDias < 12 * 7 = 84

            if (offsetDias < 0 || offsetDias >= 12 * 7)
            {
                // Fuera de rango (antes del 20/ene o despu�s de 12 semanas)
                lblParcial.Text = "Fuera de rango";
                lblWeek.Text = "";
                return;
            }

            // Calcular el �ndice de la semana (0 a 11)
            int indiceSemana = offsetDias / 7; // entero

            // Calcular el �ndice de parcial (0 a 2)
            // 4 semanas por parcial => parcial = floor(indiceSemana / 4)
            int indiceParcial = indiceSemana / 4; // 0 = parcial 1, 1 = parcial 2, 2 = parcial 3

            // Convertir a 1-based
            int parcial = indiceParcial + 1;     // 1..3
            int semanaEnParcial = (indiceSemana % 4) + 1; // 1..4

            // Mostrar en labels
            lblParcial.Text = $"Parcial {parcial}";
            lblWeek.Text = $"Semana {semanaEnParcial}";
        }
    }
}
