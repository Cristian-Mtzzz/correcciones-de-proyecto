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
    public partial class frmJustificaci�n : Form
    {
        public frmJustificaci�n()
        {
            InitializeComponent();
        }

        private void frmJustificaci�n_Load(object sender, EventArgs e)
        {
            // ACCIONES_BD.cargar(dgvDoc,)
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
