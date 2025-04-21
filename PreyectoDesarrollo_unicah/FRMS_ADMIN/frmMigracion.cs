using ClosedXML.Excel;
using PreyectoDesarrollo_unicah.CLASES;
using PreyectoDesarrollo_unicah.FRMS_ADMIN;
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
    public partial class frmMigracion : Form
    {
        public frmMigracion()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void frmMigración_Load(object sender, EventArgs e)
        {
            lblPersona.Text = ACCIONES_BD.Persona();

            mesAdmin.MinDate = dtpInicio.Value;
            ACCIONES_BD.Periodo(dtpInicio, dtpFin, btnPeriodo, mesAdmin);
            ACCIONES_BD.tablaAdmin(dgvAdmin);
            ACCIONES_BD.CargarAsistenciaAdmin(mesAdmin, (string)dgvAdmin.CurrentRow.Cells[0].Value, (string)dgvAdmin.CurrentRow.Cells[1].Value, (string)dgvAdmin.CurrentRow.Cells[2].Value, (string)dgvAdmin.CurrentRow.Cells[3].Value, (string)dgvAdmin.CurrentRow.Cells[4].Value);
        }

        private void btnPeriodo_Click(object sender, EventArgs e)
        {
            DateTime inicio = dtpInicio.Value;
            DateTime fin = dtpFin.Value;

            // Establecer el rango en el calendario
            mesAdmin.MinDate = inicio;
            mesAdmin.MaxDate = fin;

            if (inicio.Date == DateTime.Now.Date)
                if (MessageBox.Show("�Seguro que quiere definir el inicio hoy?" +
                "\nNo podr� definir de nuevo si no es el que desea", "Iniciar Periodo",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    ACCIONES_BD.CrearPeriodo(inicio, fin);
                    dtpInicio.Enabled = false;
                    dtpFin.Enabled = false;
                    btnPeriodo.Enabled = false;
                }
            if (inicio.Date != DateTime.Now.Date)
                ACCIONES_BD.CrearPeriodo(inicio, fin);
        }

        private void Salir(object sender, EventArgs e)
        {
            this.Close();
            frmAdmin Admin = new frmAdmin();
            Admin.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void dgvAdmin_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAdmin.CurrentRow != null)
            {
                string refiero = dgvAdmin.CurrentRow.Cells[0].Value.ToString();
                string curso = dgvAdmin.CurrentRow.Cells[1].Value.ToString();
                string seccion = dgvAdmin.CurrentRow.Cells[2].Value.ToString();
                string aula = dgvAdmin.CurrentRow.Cells[3].Value.ToString();
                string empleo = dgvAdmin.CurrentRow.Cells[4].Value.ToString();

                mesAdmin.RemoveAllBoldedDates();

                ACCIONES_BD.CargarAsistenciaAdmin(mesAdmin, refiero, curso, seccion, aula, empleo);
            }

        }

        private DataTable TransferirDatosExcel()
        {
            DataTable dt = new DataTable();

            foreach (DataGridViewColumn columna in dgvAdmin.Columns)
            {
                dt.Columns.Add(columna.HeaderText);
            }

            for (int parcial = 1; parcial <= 3; parcial++)
            {
                for (int semana = 1; semana <= 4; semana++)
                {
                    string[] diasSemana = { "Lunes", "Martes", "Mi�rcoles", "Jueves", "Viernes", "S�bado" };
                    foreach (string dia in diasSemana)
                    {
                        dt.Columns.Add($"Parcial {parcial} - Semana {semana} - {dia}");
                    }
                }
            }

            foreach (DataGridViewRow fila in dgvAdmin.Rows)
            {
                if (!fila.IsNewRow)
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < dgvAdmin.Columns.Count; i++)
                    {
                        dr[i] = fila.Cells[i].Value?.ToString();
                    }

                    string refiero = fila.Cells[0].Value?.ToString();
                    string curso = fila.Cells[1].Value?.ToString();
                    string seccion = fila.Cells[2].Value?.ToString();
                    string aula = fila.Cells[3].Value?.ToString();
                    string empleado = fila.Cells[4].Value?.ToString();

                    List<DateTime> fechasAsistencia = ACCIONES_BD.CargarAsistenciaAdminExcel(refiero, curso, seccion, aula, empleado);

                    foreach (DateTime fecha in fechasAsistencia)
                    {
                        DateTime fechaInicio = mesAdmin.MinDate;
                        int diasOffset = (fecha - fechaInicio).Days;
                        if (diasOffset < 0 || diasOffset >= 12 * 7)
                        {
                            continue;
                        }

                        int numeroSemana = diasOffset / 7; int diaEnSemana = diasOffset % 7;

                        if (diaEnSemana >= 6)
                            continue;
                        int baseColumnIndex = dgvAdmin.Columns.Count; int semanaColumnOffset = numeroSemana * 6; int columnIndex = baseColumnIndex + semanaColumnOffset + diaEnSemana;

                        dr[columnIndex] = "P";
                    }

                    for (int c = dgvAdmin.Columns.Count; c < dt.Columns.Count; c++)
                    {
                        if (string.IsNullOrEmpty(dr[c].ToString()))
                        {
                            dr[c] = "-";
                        }
                    }

                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = TransferirDatosExcel();

            int baseColumns = 5; int columnasPorParcial = 4 * 6;
            using (var workbook = new XLWorkbook())
            {
                for (int parcial = 0; parcial < 3; parcial++)
                {
                    DataTable dtParcial = dt.Clone();

                    for (int i = dtParcial.Columns.Count - 1; i >= baseColumns; i--)
                    {
                        dtParcial.Columns.RemoveAt(i);
                    }

                    for (int i = 0; i < columnasPorParcial; i++)
                    {
                        int indiceReal = baseColumns + (parcial * columnasPorParcial) + i;
                        dtParcial.Columns.Add(dt.Columns[indiceReal].ColumnName, typeof(string));
                    }

                    foreach (DataRow row in dt.Rows)
                    {
                        DataRow newRow = dtParcial.NewRow();

                        for (int i = 0; i < baseColumns; i++)
                        {
                            newRow[i] = row[i];
                        }
                        for (int i = 0; i < columnasPorParcial; i++)
                        {
                            int indiceReal = baseColumns + (parcial * columnasPorParcial) + i;
                            newRow[baseColumns + i] = row[indiceReal];
                        }
                        dtParcial.Rows.Add(newRow);
                    }

                    var ws = workbook.Worksheets.Add($"Parcial {parcial + 1}");
                    ws.Cell(1, 1).InsertTable(dtParcial);
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Archivo Excel (.xlsx)|*.xlsx",
                    Title = "Guardar archivo Excel",
                    FileName = "Asistencia_Por_Parciales.xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    try
                    {
                        workbook.SaveAs(saveFileDialog.FileName);
                    }
                    catch (IOException) //En caso que no se guarde por cualquier error
                    {
                        MessageBox.Show("El archivo est� abierto en Excel.\nPor favor, cerrar antes de guardar.", "Archivo en uso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
            }
        }

        private void mesAdmin_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime fechaSeleccionada = e.Start;

            DateTime fechaInicio = dtpInicio.Value;
            int offsetDias = (fechaSeleccionada - fechaInicio).Days;
            int indiceSemana = offsetDias / 7;
            int indiceParcial = indiceSemana / 4;
            int parcial = indiceParcial + 1; int semanaEnParcial = (indiceSemana % 4) + 1;
            lblParcial.Text = $"Parcial {parcial}";
            lblWeek.Text = $"Semana {semanaEnParcial}";
        }

        private void MoveForm_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnSQL_Click(object sender, EventArgs e)
        {
            string rutaExcel = ""; //Por defecto asignado, o vac�o para despu�s luego asignarse
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
                ofd.Title = "Seleccionar archivo Excel";


                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        rutaExcel = ofd.FileName;
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("Debe cerrar el Excel seleccionado", "Excel abierto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            if (string.IsNullOrEmpty(rutaExcel))
            {
                MessageBox.Show("No se seleccion� alg�n archivo.");
                return;
            }

            // Abrir el workbook con ClosedXML
            using (var workbook = new XLWorkbook(rutaExcel))
            {
                // Procesar cada hoja de forma independiente (suponemos hojas 1 a 5)
                for (int h = 1; h <= 5; h++)
                {
                    // Crear un nuevo DataTable para la hoja actual
                    DataTable dt = new DataTable();

                    // Obtener la hoja
                    var hoja = workbook.Worksheet(h);
                    bool encabezado = true;

                    // Recorrer las filas usadas en la hoja
                    foreach (var fila in hoja.RowsUsed())
                    {
                        if (encabezado)
                        {
                            // Agregar columnas seg�n la primera fila (encabezado)
                            foreach (var celda in fila.Cells())
                            {
                                dt.Columns.Add(celda.Value.ToString());
                            }
                            encabezado = false;
                        }
                        else
                        {
                            // Agregar filas: crear un DataRow y llenarlo con los valores de la fila
                            DataRow dr = dt.NewRow();
                            int i = 0;
                            foreach (var celda in fila.Cells(1, dt.Columns.Count))
                            {
                                dr[i] = celda.Value.ToString();
                                i++;
                            }
                            dt.Rows.Add(dr);
                        }
                    }
                    // Usar SqlBulkCopy para insertar los datos en la tabla SQL correspondiente
                    using (SqlConnection conexion = new SqlConnection(CONEXION_BD.conectar.ConnectionString))
                    {
                        conexion.Open();
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conexion))
                        {
                            // Ahora, seg�n el �ndice de la hoja, determinamos el nombre de la tabla SQL destino.
                            string tablaDestino = "";
                            switch (h)
                            {
                                case 1:
                                    tablaDestino = "DecanoFacultad";
                                    bulkCopy.ColumnMappings.Add("Codigo_Facultad", "codigo_facu");
                                    bulkCopy.ColumnMappings.Add("ID_Empleado", "ID_Empleado");
                                    break;
                                case 2:
                                    tablaDestino = "Sitio";
                                    bulkCopy.ColumnMappings.Add("Edificio", "Edificio");
                                    bulkCopy.ColumnMappings.Add("Aula", "Aula");
                                    bulkCopy.ColumnMappings.Add("Seccion", "Seccion");
                                    break;
                                case 3:
                                    tablaDestino = "Clases";
                                    bulkCopy.ColumnMappings.Add("Codigo_Asignatura", "Cod_Asignatura");
                                    bulkCopy.ColumnMappings.Add("Codigo_Facultad", "Cod_Facultad");
                                    bulkCopy.ColumnMappings.Add("Curso", "Asignatura");
                                    bulkCopy.ColumnMappings.Add("InicioDia", "InicioDia");
                                    bulkCopy.ColumnMappings.Add("FinDia", "FinDia");
                                    bulkCopy.ColumnMappings.Add("DiaElegido", "DiasPermitidos");
                                    break;
                                case 4:
                                    tablaDestino = "Nombres_Completos";
                                    bulkCopy.ColumnMappings.Add("Nombre1", "Nombre1");
                                    bulkCopy.ColumnMappings.Add("Nombre2", "Nombre2");
                                    bulkCopy.ColumnMappings.Add("Nombre3", "Apellido1");
                                    bulkCopy.ColumnMappings.Add("Nombre4", "Apellido1");
                                    break;
                                default:
                                    // Si por alguna raz�n llega a otro valor, sal del bucle o configura un valor por defecto.
                                    continue;
                            }

                            bulkCopy.DestinationTableName = tablaDestino;
                            bulkCopy.WriteToServer(dt);
                        }
                    }
                }
            }
            MessageBox.Show("Importaci�n completada exitosamente.", "�xito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lblPersona_Click(object sender, EventArgs e)
        {

        }
    }
}