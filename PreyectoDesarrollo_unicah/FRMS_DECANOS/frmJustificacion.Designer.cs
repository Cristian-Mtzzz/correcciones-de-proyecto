namespace PreyectoDesarrollo_unicah
{
    partial class frmJustificacion
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
            Button btnAgregar;
            Button btnVoy;
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            lblPersona = new Label();
            pictureBox3 = new PictureBox();
            label2 = new Label();
            dgvJustificacion = new DataGridView();
            clmClase = new DataGridViewTextBoxColumn();
            clmFecha = new DataGridViewTextBoxColumn();
            clmSeccion = new DataGridViewTextBoxColumn();
            clmDoc = new DataGridViewTextBoxColumn();
            clmJustifica = new DataGridViewTextBoxColumn();
            label1 = new Label();
            txtJustifica = new TextBox();
            lblCaracteres = new Label();
            label3 = new Label();
            cmbEdificio = new ComboBox();
            txtBusco = new TextBox();
            label4 = new Label();
            btnAgregar = new Button();
            btnVoy = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvJustificacion).BeginInit();
            SuspendLayout();
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(602, 789);
            btnAgregar.Margin = new Padding(3, 4, 3, 4);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(144, 59);
            btnAgregar.TabIndex = 29;
            btnAgregar.Text = "INSERT&AR JUSTIFICACI�N";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnVoy
            // 
            btnVoy.Location = new Point(753, 803);
            btnVoy.Margin = new Padding(3, 4, 3, 4);
            btnVoy.Name = "btnVoy";
            btnVoy.Size = new Size(144, 32);
            btnVoy.TabIndex = 27;
            btnVoy.Text = "&Regresar";
            btnVoy.UseVisualStyleBackColor = true;
            btnVoy.Click += btnVoy_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.minimizar_signo;
            pictureBox2.Location = new Point(839, 0);
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
            pictureBox1.Location = new Point(879, 0);
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
            panel1.MouseDown += MoveForm_MouseDown;
            // 
            // lblPersona
            // 
            lblPersona.AutoSize = true;
            lblPersona.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPersona.ForeColor = Color.White;
            lblPersona.Location = new Point(677, 87);
            lblPersona.Name = "lblPersona";
            lblPersona.Size = new Size(163, 23);
            lblPersona.TabIndex = 10;
            lblPersona.Text = "Nombre_Persona";
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
            label2.Location = new Point(354, 164);
            label2.Name = "label2";
            label2.Size = new Size(161, 20);
            label2.TabIndex = 30;
            label2.Text = "Justificaci�n detallada";
            // 
            // dgvJustificacion
            // 
            dgvJustificacion.AllowUserToAddRows = false;
            dgvJustificacion.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvJustificacion.Columns.AddRange(new DataGridViewColumn[] { clmClase, clmFecha, clmSeccion, clmDoc, clmJustifica });
            dgvJustificacion.Location = new Point(15, 363);
            dgvJustificacion.Margin = new Padding(3, 4, 3, 4);
            dgvJustificacion.Name = "dgvJustificacion";
            dgvJustificacion.ReadOnly = true;
            dgvJustificacion.RowHeadersWidth = 51;
            dgvJustificacion.Size = new Size(883, 405);
            dgvJustificacion.TabIndex = 26;
            dgvJustificacion.CellContentClick += dgvJustificacion_CellContentClick;
            // 
            // clmClase
            // 
            clmClase.HeaderText = "Asignatura";
            clmClase.MinimumWidth = 6;
            clmClase.Name = "clmClase";
            clmClase.ReadOnly = true;
            clmClase.Width = 150;
            // 
            // clmFecha
            // 
            clmFecha.HeaderText = "Fecha de Ausencia";
            clmFecha.MinimumWidth = 6;
            clmFecha.Name = "clmFecha";
            clmFecha.ReadOnly = true;
            clmFecha.Width = 80;
            // 
            // clmSeccion
            // 
            clmSeccion.HeaderText = "Secci�n";
            clmSeccion.MinimumWidth = 6;
            clmSeccion.Name = "clmSeccion";
            clmSeccion.ReadOnly = true;
            clmSeccion.Width = 66;
            // 
            // clmDoc
            // 
            clmDoc.HeaderText = "Docente";
            clmDoc.MinimumWidth = 6;
            clmDoc.Name = "clmDoc";
            clmDoc.ReadOnly = true;
            clmDoc.Width = 120;
            // 
            // clmJustifica
            // 
            clmJustifica.HeaderText = "Justificaci�n";
            clmJustifica.MinimumWidth = 6;
            clmJustifica.Name = "clmJustifica";
            clmJustifica.ReadOnly = true;
            clmJustifica.Width = 304;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(354, 124);
            label1.Name = "label1";
            label1.Size = new Size(219, 20);
            label1.TabIndex = 25;
            label1.Text = "JUSTIFICACI�N DE ASISTENCIA";
            // 
            // txtJustifica
            // 
            txtJustifica.Location = new Point(511, 159);
            txtJustifica.Margin = new Padding(3, 4, 3, 4);
            txtJustifica.Multiline = true;
            txtJustifica.Name = "txtJustifica";
            txtJustifica.Size = new Size(386, 195);
            txtJustifica.TabIndex = 37;
            txtJustifica.Text = "\r\n";
            txtJustifica.TextChanged += txtJustifica_TextChanged;
            // 
            // lblCaracteres
            // 
            lblCaracteres.AutoSize = true;
            lblCaracteres.BackColor = SystemColors.Window;
            lblCaracteres.BorderStyle = BorderStyle.FixedSingle;
            lblCaracteres.Location = new Point(512, 159);
            lblCaracteres.Name = "lblCaracteres";
            lblCaracteres.Size = new Size(154, 22);
            lblCaracteres.TabIndex = 38;
            lblCaracteres.Text = "L�mite de caracteres";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 243);
            label3.Name = "label3";
            label3.Size = new Size(104, 20);
            label3.TabIndex = 42;
            label3.Text = "Filtrar Edificio:";
            // 
            // cmbEdificio
            // 
            cmbEdificio.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEdificio.FormattingEnabled = true;
            cmbEdificio.Items.AddRange(new object[] { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" });
            cmbEdificio.Location = new Point(117, 239);
            cmbEdificio.Margin = new Padding(3, 4, 3, 4);
            cmbEdificio.Name = "cmbEdificio";
            cmbEdificio.Size = new Size(89, 28);
            cmbEdificio.TabIndex = 41;
            cmbEdificio.SelectedIndexChanged += cmbEdificio_SelectedIndexChanged;
            // 
            // txtBusco
            // 
            txtBusco.Location = new Point(16, 195);
            txtBusco.Margin = new Padding(3, 4, 3, 4);
            txtBusco.Name = "txtBusco";
            txtBusco.Size = new Size(217, 27);
            txtBusco.TabIndex = 40;
            txtBusco.KeyDown += txtBusco_KeyDown;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 168);
            label4.Name = "label4";
            label4.Size = new Size(68, 20);
            label4.TabIndex = 43;
            label4.Text = "Docente:";
            // 
            // frmJustificacion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(907, 860);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(cmbEdificio);
            Controls.Add(txtBusco);
            Controls.Add(lblCaracteres);
            Controls.Add(txtJustifica);
            Controls.Add(label2);
            Controls.Add(btnAgregar);
            Controls.Add(btnVoy);
            Controls.Add(dgvJustificacion);
            Controls.Add(label1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmJustificacion";
            Text = "FrmReporte";
            Load += frmJustificación_Load;
            MouseDown += MoveForm_MouseDown;
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvJustificacion).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Panel panel1;
        private PictureBox pictureBox3;
        private Label label2;
        private DataGridView dgvJustificacion;
        private Label label1;
        private TextBox txtJustifica;
        private Label lblCaracteres;
        private Label lblPersona;
        private Label label3;
        private ComboBox cmbEdificio;
        private TextBox txtBusco;
        private Label label4;
        private DataGridViewTextBoxColumn clmClase;
        private DataGridViewTextBoxColumn clmFecha;
        private DataGridViewTextBoxColumn clmSeccion;
        private DataGridViewTextBoxColumn clmDoc;
        private DataGridViewTextBoxColumn clmJustifica;
    }
}