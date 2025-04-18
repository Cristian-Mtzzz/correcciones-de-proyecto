using DocumentFormat.OpenXml.Wordprocessing;
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
    public partial class frmReposicion : Form
    {
        public frmReposicion()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void frmReposición_Load(object sender, EventArgs e)
        {
            lblPersona.Text = ACCIONES_BD.Persona();
            dtpReposicion.MinDate = DateTime.Today;            

            //Ajuste del formulario
            cmbEdificio.SelectedIndex = 0;

            //Ajustes del bdd
            ACCIONES_BD.tablaRepone(dgvRepone, ACCIONES_BD.empleado);
        }

        private void btnVoy_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDecano menu = new frmDecano();
            menu.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDecano menu = new frmDecano();
            menu.Show();
        }

        private void MoveForm_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);  //El evento en memoria se mantiene

        }

        private void btnDay_Click(object sender, EventArgs e) 
        {
            //Ajustes en la BDD
            ACCIONES_BD.Repongo(dgvRepone, (int)dgvRepone.CurrentRow.Cells[0].Value, dtpReposicion);
            ACCIONES_BD.tablaRepone(dgvRepone, ACCIONES_BD.empleado);
        }

        private void txtBusco_KeyDown(object sender, KeyEventArgs e)
        {
            ACCIONES_BD.FiltrarDatosRepo(txtBusco.Text, cmbEdificio.Text, dgvRepone);
        }

        private void cmbEdificio_SelectedIndexChanged(object sender, EventArgs e)
        {
            ACCIONES_BD.FiltrarDatosRepo(txtBusco.Text, cmbEdificio.Text, dgvRepone);
        }
    }
}
