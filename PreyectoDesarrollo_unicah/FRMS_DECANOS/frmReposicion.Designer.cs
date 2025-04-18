namespace PreyectoDesarrollo_unicah
{
    partial class frmReposicion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Button btnDay;
            Button btnVoy;
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            lblPersona = new Label();
            pictureBox3 = new PictureBox();
            label1 = new Label();
            dgvRepone = new DataGridView();
            txtBusco = new TextBox();
            cmbEdificio = new ComboBox();
            label3 = new Label();
            dtpReposicion = new DateTimePicker();
            label2 = new Label();
            btnDay = new Button();
            btnVoy = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvRepone).BeginInit();
            SuspendLayout();
            // 
            // btnDay
            // 
            btnDay.Location = new Point(16, 154);
            btnDay.Name = "btnDay";
            btnDay.Size = new Size(211, 23);
            btnDay.TabIndex = 21;
            btnDay.Text = "INSERT&AR D�A DE REPOSICI�N";
            btnDay.UseVisualStyleBackColor = true;
            btnDay.Click += btnDay_Click;
            // 
            // btnVoy
            // 
            btnVoy.Location = new Point(524, 424);
            btnVoy.Name = "btnVoy";
            btnVoy.Size = new Size(102, 29);
            btnVoy.TabIndex = 20;
            btnVoy.Text = "&REGRESAR";
            btnVoy.UseVisualStyleBackColor = true;
            btnVoy.Click += btnVoy_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.minimizar_signo;
            pictureBox2.Location = new Point(575, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(29, 20);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.CERRAR;
            pictureBox1.Location = new Point(610, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(29, 20);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.HotTrack;
            panel1.Controls.Add(lblPersona);
            panel1.Controls.Add(pictureBox3);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(-4, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(809, 87);
            panel1.TabIndex = 11;
            panel1.MouseDown += MoveForm_MouseDown;
            // 
            // lblPersona
            // 
            lblPersona.AutoSize = true;
            lblPersona.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPersona.ForeColor = Color.White;
            lblPersona.Location = new Point(507, 63);
            lblPersona.Name = "lblPersona";
            lblPersona.Size = new Size(132, 18);
            lblPersona.TabIndex = 25;
            lblPersona.Text = "Nombre_Persona";
            // 
            // pictureBox3
            // 
            pictureBox3.BackgroundImageLayout = ImageLayout.None;
            pictureBox3.Image = Properties.Resources.CircularFondoAzul;
            pictureBox3.Location = new Point(-25, 0);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(143, 81);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(266, 97);
            label1.Name = "label1";
            label1.Size = new Size(142, 15);
            label1.TabIndex = 18;
            label1.Text = "REPORTE DE REPOSICI�N";
            // 
            // dgvRepone
            // 
            dgvRepone.AllowUserToAddRows = false;
            dgvRepone.AllowUserToDeleteRows = false;
            dgvRepone.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRepone.Location = new Point(16, 183);
            dgvRepone.Name = "dgvRepone";
            dgvRepone.ReadOnly = true;
            dgvRepone.RowHeadersWidth = 51;
            dgvRepone.Size = new Size(564, 235);
            dgvRepone.TabIndex = 24;
            // 
            // txtBusco
            // 
            txtBusco.Location = new Point(266, 145);
            txtBusco.Name = "txtBusco";
            txtBusco.Size = new Size(223, 23);
            txtBusco.TabIndex = 26;
            txtBusco.KeyDown += txtBusco_KeyDown;
            // 
            // cmbEdificio
            // 
            cmbEdificio.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEdificio.FormattingEnabled = true;
            cmbEdificio.Items.AddRange(new object[] { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" });
            cmbEdificio.Location = new Point(533, 145);
            cmbEdificio.Name = "cmbEdificio";
            cmbEdificio.Size = new Size(78, 23);
            cmbEdificio.TabIndex = 27;
            cmbEdificio.SelectedIndexChanged += cmbEdificio_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(533, 122);
            label3.Name = "label3";
            label3.Size = new Size(82, 15);
            label3.TabIndex = 28;
            label3.Text = "Filtrar Edificio:";
            // 
            // dtpReposicion
            // 
            dtpReposicion.Location = new Point(12, 122);
            dtpReposicion.Name = "dtpReposicion";
            dtpReposicion.Size = new Size(215, 23);
            dtpReposicion.TabIndex = 22;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(350, 122);
            label2.Name = "label2";
            label2.Size = new Size(45, 15);
            label2.TabIndex = 30;
            label2.Text = "Buscar:";
            // 
            // frmReposicion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(636, 464);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(cmbEdificio);
            Controls.Add(txtBusco);
            Controls.Add(dgvRepone);
            Controls.Add(dtpReposicion);
            Controls.Add(btnDay);
            Controls.Add(btnVoy);
            Controls.Add(label1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmReposicion";
            Text = "FrmReporte";
            Load += new EventHandler(frmReposición_Load);

            MouseDown += MoveForm_MouseDown;
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvRepone).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Panel panel1;
        private PictureBox pictureBox3;
        private Label label1;
        private DataGridView dgvRepone;
        private Label lblPersona;
        private TextBox txtBusco;
        private ComboBox cmbEdificio;
        private Label label3;
        private DateTimePicker dtpReposicion;
        private Label label2;
    }
}