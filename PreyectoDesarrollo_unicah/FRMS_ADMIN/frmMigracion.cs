using PreyectoDesarrollo_unicah.CLASES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PreyectoDesarrollo_unicah
{
    public partial class frmMigracion : Form
    {
        public frmMigracion()
        {
            InitializeComponent();
        }

        private void frmMigraci�n_Load(object sender, EventArgs e)
        {
            lblPersona.Text = ACCIONES_BD.nombre + " " + ACCIONES_BD.apellido;
            //Ajustes del bdd
            ACCIONES_BD.tablaAdmin(dgvAdmin);
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

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 Login = new Form1();
            Login.Show();
        }
    }
}
