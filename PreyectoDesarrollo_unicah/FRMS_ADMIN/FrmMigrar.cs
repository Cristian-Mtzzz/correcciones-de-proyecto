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
using DocumentFormat.OpenXml.Spreadsheet;
using System.Globalization;
//System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);



namespace PreyectoDesarrollo_unicah.FRMS_ADMIN
{
    public partial class FrmMigrar : Form
    {
        public FrmMigrar()
        {
            InitializeComponent();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
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




        private void InsertarEmpleado(string codigoEmpleado, string primerNombre, string segundoNombre, string primerApellido, string segundoApellido)
        {
            using (SqlConnection conexion = new SqlConnection(CONEXION_BD.conexion))
            {
                conexion.Open();
                string query = "INSERT INTO empleados (codigo_empleado, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, facultad, rol, usuario, contraseña) " +
                               "VALUES (@CodigoEmpleado, @PrimerNombre, @SegundoNombre, @PrimerApellido, @SegundoApellido, 'Empleado', @Usuario, @Contraseña)";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@CodigoEmpleado", codigoEmpleado); // Código de empleado
                    cmd.Parameters.AddWithValue("@PrimerNombre", primerNombre);
                    cmd.Parameters.AddWithValue("@SegundoNombre", segundoNombre);
                    cmd.Parameters.AddWithValue("@PrimerApellido", primerApellido);
                    cmd.Parameters.AddWithValue("@SegundoApellido", segundoApellido);
                    //cmd.Parameters.AddWithValue("@CodigoFacultad", codigoFacultad); // Código de Facultad
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
                string codigoEmpleado = row.Cells[0].Value?.ToString(); // Código de empleado
                string primerNombre = row.Cells[1].Value?.ToString();
                string segundoNombre = row.Cells[2].Value?.ToString();
                string primerApellido = row.Cells[3].Value?.ToString();
                string segundoApellido = row.Cells[4].Value?.ToString();
                string facultad = row.Cells[5].Value?.ToString();
                string codigoFacultad = row.Cells[6].Value?.ToString(); // Código de Facultad
                string codigoAsignatura = row.Cells[7].Value?.ToString(); // Código de Clase
                string asignatura = row.Cells[8].Value?.ToString(); // Asignatura
                string seccion = row.Cells[9].Value?.ToString(); // Sección
                string aula = row.Cells[10].Value?.ToString(); // Aula
                string edificio = row.Cells[11].Value?.ToString(); // Edificio
                string fechaInicio = row.Cells[12].Value?.ToString(); // Fecha inicio
                string fechaFinal = row.Cells[11].Value?.ToString(); // Fecha final

                try
                {
                    // Validar si el código de facultad existe
                    /* if (!CodigoFacultadExiste(codigoFacultad))
                     {
                         MessageBox.Show($"El código de facultad '{codigoFacultad}' no existe en la base de datos. Registro omitido.", "Error de clave foránea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         continue;
                     }*/

                    // Guardar en la tabla empleados
                    InsertarEmpleado(codigoEmpleado, primerNombre, segundoNombre, primerApellido, segundoApellido);

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

                            //mostrar datos
                            foreach (DataColumn col in table.Columns)
                            {
                                Console.WriteLine("Columna: " + col.ColumnName);
                                MessageBox.Show("Columna: " + col.ColumnName);
                            }

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
        /*
        private void loaddata()
        {
            using (CONEXION_BD.conectar)
            {
                CONEXION_BD.conectar.Open();
                SqlCommand verificar = new SqlCommand("select primer_nombre, segundo_nombre, primer_apellido, segundo_apellido from empleados where codigo_empleado=@ codigoEmpleado", CONEXION_BD.conectar);
                dgvMigrar.columns[0].readOnly = true ;

                object resultado = verificar.ExecuteScalar();

                if (resultado == null)

                }
        }*/

        private void FrmMigrar_Load(object sender, EventArgs e)
        {
            lblPersona.Text = ACCIONES_BD.Persona();

            CONEXION_BD.conectar.Open();
            //loaddata();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //GuardarDatos();

            /*
            int filasagregadas = 0;
            foreach (DataGridViewRow row in dgvMigrar.Rows)
            {
                if (row.IsNewRow) continue; // Ignorar la fila nueva

                //int codigoEmpleado = Convert.ToInt32(row.Cells[0].Value); // Código de empleado
                string primerNombre = row.Cells[1].Value?.ToString();
                string segundoNombre = row.Cells[2].Value?.ToString();
                string primerApellido = row.Cells[3].Value?.ToString();
                string segundoApellido = row.Cells[4].Value?.ToString();
                string facultad = row.Cells[5].Value?.ToString();
                string codigoFacultad = row.Cells[6].Value?.ToString(); // Código de Facultad
                string codigoAsignatura = row.Cells[7].Value?.ToString(); // Código de Clase
                string asignatura = row.Cells[8].Value?.ToString(); // Asignatura
                string seccion = row.Cells[9].Value?.ToString(); // Sección
                string aula = row.Cells[10].Value?.ToString(); // Aula
                string edificio = row.Cells[11].Value?.ToString(); // Edificio
                string fechaInicio = row.Cells[12].Value?.ToString(); // Fecha inicio
                string fechaFinal = row.Cells[13].Value?.ToString(); // Fecha final

                // Aquí puedes procesar los datos o insertarlos en la base de datos

            */






            /*

            CONEXION_BD conexionBD = new CONEXION_BD();

            try
            {
                conexionBD.abrir(); // Abrir la conexión

                foreach (DataGridViewRow row in dgvMigrar.Rows)
                {
                    if (row.IsNewRow) continue;

                    // === Datos para empleados ===
                    
                    string primerNombre = row.Cells["Primer Nombre"].Value?.ToString();
                    string segundoNombre = row.Cells["Segundo Nombre"].Value?.ToString();
                    string primerApellido = row.Cells["Primer Apellido"].Value?.ToString();
                    string segundoApellido = row.Cells["Segundo Apellido"].Value?.ToString();
                    string facultad = row.Cells["Facultad"].Value?.ToString();

                    string queryEmpleados = "INSERT INTO empleados (codigo_empleado, facultad, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido) " +
                                            "VALUES (@codigo_empleado, @facultad, @primer_nombre, @segundo_nombre, @primer_apellido, @segundo_apellido)";

                    using (SqlCommand cmd = new SqlCommand(queryEmpleados, CONEXION_BD.conectar))
                    {
                        cmd.Parameters.AddWithValue("@codigo_empleado", GenerarCodigoEmpleado());
                        cmd.Parameters.AddWithValue("@facultad", facultad);
                        cmd.Parameters.AddWithValue("@primer_nombre", primerNombre);
                        cmd.Parameters.AddWithValue("@segundo_nombre", segundoNombre);
                        cmd.Parameters.AddWithValue("@primer_apellido", primerApellido);
                        cmd.Parameters.AddWithValue("@segundo_apellido", segundoApellido);
                        cmd.ExecuteNonQuery();
                    }

                    // === Datos para clases ===
                    string codAsignatura = row.Cells["Código de Clase"].Value?.ToString();
                    string asignatura = row.Cells["Asignatura"].Value?.ToString();
                    string seccion = row.Cells["Sección"].Value?.ToString();
                    string aula = row.Cells["Aula"].Value?.ToString();
                    string edificio = row.Cells["Edificio"].Value?.ToString();

                    string queryClases = "INSERT INTO Clases (cod_Asignatura, Facultad, asignatura, edificio, aula, seccion) " +
                                         "VALUES (@cod_Asignatura, @Facultad, @asignatura, @edificio, @aula, @seccion)";

                    using (SqlCommand cmd = new SqlCommand(queryClases, CONEXION_BD.conectar))
                    {
                        cmd.Parameters.AddWithValue("@cod_Asignatura", codAsignatura);
                        cmd.Parameters.AddWithValue("@Facultad", facultad);
                        cmd.Parameters.AddWithValue("@asignatura", asignatura);
                        cmd.Parameters.AddWithValue("@edificio", edificio);
                        cmd.Parameters.AddWithValue("@aula", aula);
                        cmd.Parameters.AddWithValue("@seccion", seccion);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Datos insertados correctamente.", "Éxito");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR al guardar los datos: {ex.Message}", "ERROR");
            }
            finally
            {
                conexionBD.cerrar(); // Cerrar la conexión al final
            }
        }

        // Puedes mejorar esto con un autoincremento real desde la base si lo prefieres
        private int GenerarCodigoEmpleado()
        {
            Random rnd = new Random();
            return rnd.Next(1000, 9999); // Puedes reemplazar esto por una consulta si lo deseas autoincremental

        }*/

            string filePath = @"C:\Users\marti\Desktop\FORMATO DE ASISTENCIA PARA REPO.xlsx";

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    DataTable table = result.Tables[0];

                    string columnas = string.Join("\n", table.Columns.Cast<DataColumn>().Select(c => c.ColumnName));
                    MessageBox.Show("Columnas del Excel:\n" + columnas);

                    // Mostrar en DataGridView
                    dgvMigrar.DataSource = table;

                    // Abrir conexión
                    CONEXION_BD conexionBD = new CONEXION_BD();
                    conexionBD.abrir();

                    foreach (DataRow row in table.Rows)
                    {
                        try
                        {
                            // Datos del empleado
                            int codigoEmpleado = int.Parse(row["Column0"]?.ToString().Trim());
                            string primerNombre = row["Column1"]?.ToString().Trim();
                            string segundoNombre = row["Column2"]?.ToString().Trim();
                            string primerApellido = row["Column3"]?.ToString().Trim();
                            string segundoApellido = row["Column4"]?.ToString().Trim();
                            string usuario = row["Column5"]?.ToString().Trim();
                            string contraseña = row["Column6"]?.ToString().Trim();
                            string rol = row["Column7"]?.ToString().Trim();
                            string facultad = row["Column8"]?.ToString().Trim();

                            // Datos de la clase (asignatura)
                            string codAsignatura = row["Column10"]?.ToString().Trim();
                            string asignatura = row["Column11"]?.ToString().Trim();
                            string seccion = row["Column12"]?.ToString().Trim();
                            string edificio = row["Column14"]?.ToString().Trim();
                            string aula = row["Column13"]?.ToString().Trim(); // Añadido para el aula
                            string iniciodia = row["Column15"]?.ToString().Trim();
                            string findia = row["Column16"]?.ToString().Trim();
                            string diaspermitidos = row["Column17"]?.ToString().Trim();

                            // Insertar en Empleados
                            string checkEmpleadoQuery = "SELECT COUNT(*) FROM Empleados WHERE codigo_empleado = @CodigoEmpleado";
                            using (SqlCommand checkCmd = new SqlCommand(checkEmpleadoQuery, CONEXION_BD.conectar))
                            {
                                checkCmd.Parameters.AddWithValue("@CodigoEmpleado", codigoEmpleado);
                                int count = (int)checkCmd.ExecuteScalar();

                                if (count == 0)
                                {
                                    string insertEmpleadoQuery = @"
                            INSERT INTO Empleados 
                                (codigo_empleado, facultad, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, usuario, contraseña, rol) 
                            VALUES 
                                (@CodigoEmpleado, @Facultad, @PrimerNombre, @SegundoNombre, @PrimerApellido, @SegundoApellido, @Usuario, @Contraseña, @Rol)";

                                    using (SqlCommand cmd = new SqlCommand(insertEmpleadoQuery, CONEXION_BD.conectar))
                                    {
                                        cmd.Parameters.AddWithValue("@CodigoEmpleado", codigoEmpleado);
                                        cmd.Parameters.AddWithValue("@Facultad", facultad);
                                        cmd.Parameters.AddWithValue("@PrimerNombre", primerNombre);
                                        cmd.Parameters.AddWithValue("@SegundoNombre", segundoNombre ?? "");
                                        cmd.Parameters.AddWithValue("@PrimerApellido", primerApellido);
                                        cmd.Parameters.AddWithValue("@SegundoApellido", segundoApellido ?? "");
                                        cmd.Parameters.AddWithValue("@Usuario", usuario);
                                        cmd.Parameters.AddWithValue("@Contraseña", contraseña);
                                        cmd.Parameters.AddWithValue("@Rol", rol);

                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }

                            // Insertar en Clases
                            string checkClaseQuery = "SELECT COUNT(*) FROM Clases WHERE cod_Asignatura = @CodAsignatura AND seccion = @Seccion";
                            using (SqlCommand checkCmd = new SqlCommand(checkClaseQuery, CONEXION_BD.conectar))
                            {
                                checkCmd.Parameters.AddWithValue("@CodAsignatura", codAsignatura);
                                checkCmd.Parameters.AddWithValue("@Seccion", seccion);
                                int count = (int)checkCmd.ExecuteScalar();

                                if (count == 0)
                                {
                                    string insertClaseQuery = @"
                            INSERT INTO Clases 
                                (cod_Asignatura, Facultad, asignatura, edificio, aula, seccion, inicioDia, finDia, diasPermitidos) 
                            VALUES 
                                (@CodAsignatura, @Facultad, @Asignatura, @Edificio, @Aula, @Seccion, @InicioDia, @FinDia, @DiasPermitidos)";

                                    using (SqlCommand cmd = new SqlCommand(insertClaseQuery, CONEXION_BD.conectar))
                                    {
                                        cmd.Parameters.AddWithValue("@CodAsignatura", codAsignatura);
                                        cmd.Parameters.AddWithValue("@Facultad", facultad);
                                        cmd.Parameters.AddWithValue("@Asignatura", asignatura);
                                        cmd.Parameters.AddWithValue("@Edificio", edificio ?? "N/A");
                                        cmd.Parameters.AddWithValue("@Aula", aula);
                                        cmd.Parameters.AddWithValue("@Seccion", seccion);
                                        cmd.Parameters.AddWithValue("@InicioDia", iniciodia);  // Usar el valor tal cual del Excel
                                        cmd.Parameters.AddWithValue("@FinDia", findia);     // Usar el valor tal cual del Excel
                                        cmd.Parameters.AddWithValue("@DiasPermitidos", diaspermitidos);

                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al procesar fila: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    conexionBD.cerrar();
                }
            }

        }
    }
}


