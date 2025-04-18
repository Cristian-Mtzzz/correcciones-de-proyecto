namespace PreyectoDesarrollo_unicah
{
    partial class frmMigracion
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
            Button btnLogout;
            Button btnExcel;
            Button btnSQL;
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            lblPersona = new Label();
            pictureBox3 = new PictureBox();
            dgvAdmin = new DataGridView();
            clmAsignaturaFacu = new DataGridViewTextBoxColumn();
            clmCurso = new DataGridViewTextBoxColumn();
            clmSeccion = new DataGridViewTextBoxColumn();
            clmEmpleado = new DataGridViewTextBoxColumn();
            clmAulaFull = new DataGridViewTextBoxColumn();
            label1 = new Label();
            lblParcial = new Label();
            lblWeek = new Label();
            mesAdmin = new MonthCalendar();
            dtpInicio = new DateTimePicker();
            label2 = new Label();
            label3 = new Label();
            dtpFin = new DateTimePicker();
            groupBox1 = new GroupBox();
            btnPeriodo = new Button();
            btnLogout = new Button();
            btnExcel = new Button();
            btnSQL = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAdmin).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(866, 474);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(122, 21);
            btnLogout.TabIndex = 14;
            btnLogout.Text = "&REGRESAR";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += Salir;
            // 
            // btnExcel
            // 
            btnExcel.Location = new Point(922, 446);
            btnExcel.Name = "btnExcel";
            btnExcel.Size = new Size(122, 22);
            btnExcel.TabIndex = 15;
            btnExcel.Text = "&EXPORTAR A EXCEL";
            btnExcel.UseVisualStyleBackColor = true;
            btnExcel.Click += btnExcel_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.minimizar_signo;
            pictureBox2.Location = new Point(995, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(29, 20);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.CERRAR;
            pictureBox1.Location = new Point(1030, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(29, 20);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            pictureBox1.Click += Salir;
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
            panel1.Size = new Size(1062, 87);
            panel1.TabIndex = 11;
            panel1.MouseDown += MoveForm_MouseDown;
            // 
            // lblPersona
            // 
            lblPersona.AutoSize = true;
            lblPersona.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPersona.ForeColor = Color.White;
            lblPersona.Location = new Point(876, 63);
            lblPersona.Name = "lblPersona";
            lblPersona.Size = new Size(132, 18);
            lblPersona.TabIndex = 16;
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
            // dgvAdmin
            // 
            dgvAdmin.AllowUserToAddRows = false;
            dgvAdmin.AllowUserToDeleteRows = false;
            dgvAdmin.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAdmin.Columns.AddRange(new DataGridViewColumn[] { clmAsignaturaFacu, clmCurso, clmSeccion, clmEmpleado, clmAulaFull });
            dgvAdmin.Location = new Point(13, 124);
            dgvAdmin.Name = "dgvAdmin";
            dgvAdmin.ReadOnly = true;
            dgvAdmin.Size = new Size(746, 368);
            dgvAdmin.TabIndex = 13;
            dgvAdmin.SelectionChanged += dgvAdmin_SelectionChanged;
            // 
            // clmAsignaturaFacu
            // 
            clmAsignaturaFacu.HeaderText = "Referencia";
            clmAsignaturaFacu.Name = "clmAsignaturaFacu";
            clmAsignaturaFacu.ReadOnly = true;
            clmAsignaturaFacu.Width = 115;
            // 
            // clmCurso
            // 
            clmCurso.HeaderText = "Curso";
            clmCurso.Name = "clmCurso";
            clmCurso.ReadOnly = true;
            clmCurso.Width = 170;
            // 
            // clmSeccion
            // 
            clmSeccion.HeaderText = "Secci�n";
            clmSeccion.Name = "clmSeccion";
            clmSeccion.ReadOnly = true;
            clmSeccion.Width = 58;
            // 
            // clmEmpleado
            // 
            clmEmpleado.HeaderText = "Empleado";
            clmEmpleado.Name = "clmEmpleado";
            clmEmpleado.ReadOnly = true;
            clmEmpleado.Width = 325;
            // 
            // clmAulaFull
            // 
            clmAulaFull.HeaderText = "Aula";
            clmAulaFull.Name = "clmAulaFull";
            clmAulaFull.ReadOnly = true;
            clmAulaFull.Width = 183;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(453, 99);
            label1.Name = "label1";
            label1.Size = new Size(129, 15);
            label1.TabIndex = 12;
            label1.Text = "MIGRACI�N DE DATOS";
            // 
            // lblParcial
            // 
            lblParcial.AutoSize = true;
            lblParcial.Location = new Point(828, 256);
            lblParcial.Name = "lblParcial";
            lblParcial.Size = new Size(42, 15);
            lblParcial.TabIndex = 16;
            lblParcial.Text = "Parcial";
            // 
            // lblWeek
            // 
            lblWeek.AutoSize = true;
            lblWeek.Location = new Point(939, 256);
            lblWeek.Name = "lblWeek";
            lblWeek.Size = new Size(49, 15);
            lblWeek.TabIndex = 17;
            lblWeek.Text = "Semana";
            // 
            // mesAdmin
            // 
            mesAdmin.Location = new Point(790, 272);
            mesAdmin.Name = "mesAdmin";
            mesAdmin.TabIndex = 19;
            mesAdmin.DateSelected += mesAdmin_DateSelected;
            // 
            // dtpInicio
            // 
            dtpInicio.Location = new Point(60, 22);
            dtpInicio.Name = "dtpInicio";
            dtpInicio.Size = new Size(210, 23);
            dtpInicio.TabIndex = 20;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 28);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 21;
            label2.Text = "Inicio:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 66);
            label3.Name = "label3";
            label3.Size = new Size(35, 15);
            label3.TabIndex = 23;
            label3.Text = "Final:";
            // 
            // dtpFin
            // 
            dtpFin.Location = new Point(59, 60);
            dtpFin.Name = "dtpFin";
            dtpFin.Size = new Size(210, 23);
            dtpFin.TabIndex = 22;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dtpInicio);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(dtpFin);
            groupBox1.Location = new Point(768, 110);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(276, 100);
            groupBox1.TabIndex = 24;
            groupBox1.TabStop = false;
            groupBox1.Text = "Periodo";
            // 
            // btnPeriodo
            // 
            btnPeriodo.Location = new Point(872, 216);
            btnPeriodo.Name = "btnPeriodo";
            btnPeriodo.Size = new Size(98, 23);
            btnPeriodo.TabIndex = 25;
            btnPeriodo.Text = "Definir Periodo";
            btnPeriodo.UseVisualStyleBackColor = true;
            btnPeriodo.Click += btnPeriodo_Click;
            // 
            // btnSQL
            // 
            btnSQL.Location = new Point(774, 446);
            btnSQL.Name = "btnSQL";
            btnSQL.Size = new Size(133, 22);
            btnSQL.TabIndex = 26;
            btnSQL.Text = "&MIGRAR EXCEL A SQL";
            btnSQL.UseVisualStyleBackColor = true;
            btnSQL.Click += btnSQL_Click;
            // 
            // frmMigracion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1056, 504);
            Controls.Add(btnSQL);
            Controls.Add(btnPeriodo);
            Controls.Add(groupBox1);
            Controls.Add(mesAdmin);
            Controls.Add(lblWeek);
            Controls.Add(lblParcial);
            Controls.Add(btnExcel);
            Controls.Add(panel1);
            Controls.Add(btnLogout);
            Controls.Add(dgvAdmin);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmMigracion";
            Text = "FrmReporte";
            Load += frmMigración_Load;
            MouseDown += MoveForm_MouseDown;
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAdmin).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Panel panel1;
        private PictureBox pictureBox3;
        private DataGridView dgvAdmin;
        private Label label1;
        private Label lblPersona;
        private Label lblParcial;
        private Label lblWeek;
        private MonthCalendar mesAdmin;
        private DataGridViewTextBoxColumn clmAsignaturaFacu;
        private DataGridViewTextBoxColumn clmCurso;
        private DataGridViewTextBoxColumn clmSeccion;
        private DataGridViewTextBoxColumn clmEmpleado;
        private DataGridViewTextBoxColumn clmAulaFull;
        private DateTimePicker dtpInicio;
        private Label label2;
        private Label label3;
        private DateTimePicker dtpFin;
        private GroupBox groupBox1;
        private Button btnPeriodo;
    }
}