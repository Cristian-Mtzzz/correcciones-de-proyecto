using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.IO;
using ExcelDataReader;
using PreyectoDesarrollo_unicah.CLASES;
using System.Data.SqlClient;
//System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);



namespace PreyectoDesarrollo_unicah.FRMS_ADMIN
{
    public partial class FrmMigrar : Form
    {
        public FrmMigrar()
        {
            InitializeComponent();
        }



        private bool CodigoFacultadExiste(string codigoFacultad)
        {
            using (SqlConnection conexion = new SqlConnection(CONEXION_BD.conexion))
            {
                conexion.Open();
                string query = "SELECT COUNT(*) FROM DecanoFacultad WHERE codigo_facu = @CodigoFacultad";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@CodigoFacultad", codigoFacultad);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private void InsertarEmpleado(string codigoEmpleado, string primerNombre, string segundoNombre, string primerApellido, string segundoApellido, string codigoFacultad)
        {
            using (SqlConnection conexion = new SqlConnection(CONEXION_BD.conexion))
            {
                conexion.Open();
                string query = "INSERT INTO empleados (codigo_empleado, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, facultad, rol, usuario, contraseña) " +
                               "VALUES (@CodigoEmpleado, @PrimerNombre, @SegundoNombre, @PrimerApellido, @SegundoApellido, @CodigoFacultad, 'Empleado', @Usuario, @Contraseña)";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@CodigoEmpleado", codigoEmpleado); // Código de empleado
                    cmd.Parameters.AddWithValue("@PrimerNombre", primerNombre);
                    cmd.Parameters.AddWithValue("@SegundoNombre", segundoNombre);
                    cmd.Parameters.AddWithValue("@PrimerApellido", primerApellido);
                    cmd.Parameters.AddWithValue("@SegundoApellido", segundoApellido);
                    cmd.Parameters.AddWithValue("@CodigoFacultad", codigoFacultad); // Código de Facultad
                    cmd.Parameters.AddWithValue("@Usuario", $"{primerNombre.ToLower()}.{primerApellido.ToLower()}"); // Generar usuario
                    cmd.Parameters.AddWithValue("@Contraseña", "123456"); // Contraseña predeterminada
                    cmd.ExecuteNonQuery();
                }
            }
        }



        private void InsertarClase(string codigoAsignatura, string codigoFacultad, string asignatura, string seccion, string aula, string edificio, string fechaInicio, string fechaFinal)
        {
            using (SqlConnection conexion = new SqlConnection(CONEXION_BD.conexion))
            {
                conexion.Open();
                string query = "INSERT INTO Clases (cod_Asignatura, Facultad, asignatura, seccion, aula, edificio, inicioDia, finDia, diasPermitidos) " +
                               "VALUES (@CodigoAsignatura, @CodigoFacultad, @Asignatura, @Seccion, @Aula, @Edificio, @InicioDia, @FinDia, @DiasPermitidos)";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@CodigoAsignatura", codigoAsignatura);
                    cmd.Parameters.AddWithValue("@CodigoFacultad", codigoFacultad); // Código de Facultad
                    cmd.Parameters.AddWithValue("@Asignatura", asignatura);
                    cmd.Parameters.AddWithValue("@Seccion", seccion);
                    cmd.Parameters.AddWithValue("@Aula", aula);
                    cmd.Parameters.AddWithValue("@Edificio", edificio);
                    cmd.Parameters.AddWithValue("@InicioDia", DateTime.Parse(fechaInicio)); // Convertir a DateTime
                    cmd.Parameters.AddWithValue("@FinDia", DateTime.Parse(fechaFinal)); // Convertir a DateTime
                    cmd.Parameters.AddWithValue("@DiasPermitidos", 90); // Días permitidos predeterminados
                    cmd.ExecuteNonQuery();
                }
            }
        }


        private void GuardarDatos()
        {
            foreach (DataGridViewRow row in dgvMigrar.Rows)
            {
                if (row.IsNewRow) continue;

                // Extraer datos de las columnas
                string codigoEmpleado = row.Cells[4].Value?.ToString(); // Código de empleado
                string primerNombre = row.Cells[0].Value?.ToString();
                string segundoNombre = row.Cells[1].Value?.ToString();
                string primerApellido = row.Cells[2].Value?.ToString();
                string segundoApellido = row.Cells[3].Value?.ToString();
                string codigoFacultad = row.Cells[5].Value?.ToString(); // Código de Facultad
                string codigoAsignatura = row.Cells[6].Value?.ToString(); // Código de Clase
                string asignatura = row.Cells[7].Value?.ToString(); // Asignatura
                string seccion = row.Cells[8].Value?.ToString(); // Sección
                string aula = row.Cells[9].Value?.ToString(); // Aula
                string edificio = row.Cells[10].Value?.ToString(); // Edificio
                string fechaInicio = row.Cells[11].Value?.ToString(); // Fecha inicio
                string fechaFinal = row.Cells[12].Value?.ToString(); // Fecha final

                try
                {
                    // Validar si el código de facultad existe
                    if (!CodigoFacultadExiste(codigoFacultad))
                    {
                        MessageBox.Show($"El código de facultad '{codigoFacultad}' no existe en la base de datos. Registro omitido.", "Error de clave foránea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    // Guardar en la tabla empleados
                    InsertarEmpleado(codigoEmpleado, primerNombre, segundoNombre, primerApellido, segundoApellido, codigoFacultad);

                    // Guardar en la tabla Clases
                    InsertarClase(codigoAsignatura, codigoFacultad, asignatura, seccion, aula, edificio, fechaInicio, fechaFinal);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            MessageBox.Show("Datos guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void btncargar_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = "Excel Files|*.xls;*.xlsx",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;

                // Habilitar soporte de codificación
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                try
                {
                    using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            // Leer el archivo como un DataSet
                            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                {
                                    UseHeaderRow = true // Usa la primera fila como encabezado
                                }
                            });

                            // Obtener la primera hoja
                            DataTable table = result.Tables[0];

                            // Deshabilitar encabezados predeterminados del DataGridView
                            dgvMigrar.ColumnHeadersVisible = false;

                            // Cargar al DataGridView
                            dgvMigrar.DataSource = table;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al leer el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmMigrar_Load(object sender, EventArgs e)
        {
            lblPersona.Text = ACCIONES_BD.Persona();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarDatos();
        }
    }
}

