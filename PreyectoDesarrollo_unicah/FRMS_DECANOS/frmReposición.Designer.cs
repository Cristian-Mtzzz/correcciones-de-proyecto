namespace PreyectoDesarrollo_unicah
{
    partial class frmReposici�n
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
            Button btnBusco;
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            lblPersona = new Label();
            pictureBox3 = new PictureBox();
            label2 = new Label();
            nudReposicion = new NumericUpDown();
            label1 = new Label();
            dataGridView1 = new DataGridView();
            clmClase = new DataGridViewTextBoxColumn();
            clmFecha = new DataGridViewTextBoxColumn();
            clmSeccion = new DataGridViewTextBoxColumn();
            clmDocente = new DataGridViewTextBoxColumn();
            clmRepo = new DataGridViewTextBoxColumn();
            txtBusco = new TextBox();
            cmbEdificio = new ComboBox();
            label3 = new Label();
            lblMeses = new Label();
            btnDay = new Button();
            btnVoy = new Button();
            btnBusco = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudReposicion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnDay
            // 
            btnDay.Location = new Point(194, 161);
            btnDay.Margin = new Padding(3, 4, 3, 4);
            btnDay.Name = "btnDay";
            btnDay.Size = new Size(77, 31);
            btnDay.TabIndex = 21;
            btnDay.Text = "INSERT&AR";
            btnDay.UseVisualStyleBackColor = true;
            // 
            // btnVoy
            // 
            btnVoy.Location = new Point(599, 565);
            btnVoy.Margin = new Padding(3, 4, 3, 4);
            btnVoy.Name = "btnVoy";
            btnVoy.Size = new Size(117, 39);
            btnVoy.TabIndex = 20;
            btnVoy.Text = "&Regresar";
            btnVoy.UseVisualStyleBackColor = true;
            btnVoy.Click += btnVoy_Click;
            // 
            // btnBusco
            // 
            btnBusco.Location = new Point(470, 161);
            btnBusco.Margin = new Padding(3, 4, 3, 4);
            btnBusco.Name = "btnBusco";
            btnBusco.Size = new Size(77, 31);
            btnBusco.TabIndex = 25;
            btnBusco.Text = "&Buscar";
            btnBusco.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.minimizar_signo;
            pictureBox2.Location = new Point(657, 5);
            pictureBox2.Margin = new Padding(3, 4, 3, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(33, 27);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.CERRAR;
            pictureBox1.Location = new Point(697, 5);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(33, 27);
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
            panel1.Location = new Point(-5, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(925, 116);
            panel1.TabIndex = 11;
            panel1.MouseDown += panel1_MouseDown;
            // 
            // lblPersona
            // 
            lblPersona.AutoSize = true;
            lblPersona.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPersona.ForeColor = Color.White;
            lblPersona.Location = new Point(579, 84);
            lblPersona.Name = "lblPersona";
            lblPersona.Size = new Size(163, 23);
            lblPersona.TabIndex = 25;
            lblPersona.Text = "Nombre_Persona";
            lblPersona.Click += lblPersona_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.BackgroundImageLayout = ImageLayout.None;
            pictureBox3.Image = Properties.Resources.CircularFondoAzul;
            pictureBox3.Location = new Point(-29, 0);
            pictureBox3.Margin = new Padding(3, 4, 3, 4);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(163, 108);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 167);
            label2.Name = "label2";
            label2.Size = new Size(129, 20);
            label2.TabIndex = 23;
            label2.Text = "D�a de reposici�n:";
            // 
            // nudReposicion
            // 
            nudReposicion.Location = new Point(137, 164);
            nudReposicion.Margin = new Padding(3, 4, 3, 4);
            nudReposicion.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            nudReposicion.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudReposicion.Name = "nudReposicion";
            nudReposicion.Size = new Size(46, 27);
            nudReposicion.TabIndex = 22;
            nudReposicion.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(304, 129);
            label1.Name = "label1";
            label1.Size = new Size(179, 20);
            label1.TabIndex = 18;
            label1.Text = "REPORTE DE REPOSICI�N";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { clmClase, clmFecha, clmSeccion, clmDocente, clmRepo });
            dataGridView1.Location = new Point(18, 200);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(697, 357);
            dataGridView1.TabIndex = 24;
            // 
            // clmClase
            // 
            clmClase.HeaderText = "Asignatura";
            clmClase.MinimumWidth = 6;
            clmClase.Name = "clmClase";
            clmClase.ReadOnly = true;
            clmClase.Width = 125;
            // 
            // clmFecha
            // 
            clmFecha.HeaderText = "Fecha de Ausencia";
            clmFecha.MinimumWidth = 6;
            clmFecha.Name = "clmFecha";
            clmFecha.ReadOnly = true;
            clmFecha.Width = 130;
            // 
            // clmSeccion
            // 
            clmSeccion.HeaderText = "Secci�n";
            clmSeccion.MinimumWidth = 6;
            clmSeccion.Name = "clmSeccion";
            clmSeccion.ReadOnly = true;
            clmSeccion.Width = 125;
            // 
            // clmDocente
            // 
            clmDocente.HeaderText = "Docente";
            clmDocente.MinimumWidth = 6;
            clmDocente.Name = "clmDocente";
            clmDocente.ReadOnly = true;
            clmDocente.Width = 125;
            // 
            // clmRepo
            // 
            clmRepo.HeaderText = "Fecha de Reposici�n";
            clmRepo.MinimumWidth = 6;
            clmRepo.Name = "clmRepo";
            clmRepo.ReadOnly = true;
            clmRepo.Width = 138;
            // 
            // txtBusco
            // 
            txtBusco.Location = new Point(312, 163);
            txtBusco.Margin = new Padding(3, 4, 3, 4);
            txtBusco.Name = "txtBusco";
            txtBusco.Size = new Size(150, 27);
            txtBusco.TabIndex = 26;
            // 
            // cmbEdificio
            // 
            cmbEdificio.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEdificio.FormattingEnabled = true;
            cmbEdificio.Items.AddRange(new object[] { "Edificio:", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" });
            cmbEdificio.Location = new Point(624, 161);
            cmbEdificio.Margin = new Padding(3, 4, 3, 4);
            cmbEdificio.Name = "cmbEdificio";
            cmbEdificio.Size = new Size(89, 28);
            cmbEdificio.TabIndex = 27;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(571, 167);
            label3.Name = "label3";
            label3.Size = new Size(50, 20);
            label3.TabIndex = 28;
            label3.Text = "Filtrar:";
            // 
            // lblMeses
            // 
            lblMeses.AutoSize = true;
            lblMeses.Location = new Point(661, 129);
            lblMeses.Name = "lblMeses";
            lblMeses.Size = new Size(60, 20);
            lblMeses.TabIndex = 29;
            lblMeses.Text = "Periodo";
            lblMeses.Visible = false;
            // 
            // frmReposici�n
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(727, 619);
            Controls.Add(lblMeses);
            Controls.Add(label3);
            Controls.Add(cmbEdificio);
            Controls.Add(txtBusco);
            Controls.Add(btnBusco);
            Controls.Add(dataGridView1);
            Controls.Add(label2);
            Controls.Add(nudReposicion);
            Controls.Add(btnDay);
            Controls.Add(btnVoy);
            Controls.Add(label1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmReposici�n";
            Text = "FrmReporte";
            Load += frmReposici�n_Load;
            MouseDown += frmReposici�n_MouseDown;
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudReposicion).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Panel panel1;
        private PictureBox pictureBox3;
        private Label label2;
        private NumericUpDown nudReposicion;
        private Label label1;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn clmClase;
        private DataGridViewTextBoxColumn clmFecha;
        private DataGridViewTextBoxColumn clmSeccion;
        private DataGridViewTextBoxColumn clmDocente;
        private DataGridViewTextBoxColumn clmRepo;
        private Label lblPersona;
        private TextBox txtBusco;
        private ComboBox cmbEdificio;
        private Label label3;
        private Label lblMeses;
    }
}