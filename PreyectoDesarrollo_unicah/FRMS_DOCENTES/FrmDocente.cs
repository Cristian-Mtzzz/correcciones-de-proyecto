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
    public partial class frmDocente : Form
    {
        public frmDocente()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 Login = new Form1();
            Login.Show();
        }

        private void frmDocente_Load(object sender, EventArgs e)
        {
            ACCIONES_BD objPersona = new ACCIONES_BD();
            lblPersona.Text = objPersona.nombre + objPersona.apellido; 
        }
    }
}
