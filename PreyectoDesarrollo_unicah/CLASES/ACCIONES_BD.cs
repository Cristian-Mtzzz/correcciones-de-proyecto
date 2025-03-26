﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Security.Policy;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using System.Drawing;

namespace PreyectoDesarrollo_unicah.CLASES
{
    class ACCIONES_BD
    {
        //Atributos
        public static string nombre, apellido;
        public static string docente;
        public CONEXION_BD conexion = new CONEXION_BD();

        //Constructor
        public ACCIONES_BD()
        {
            nombre = "";
            apellido = "";
        }

        public ACCIONES_BD(string codigo) //Constructor parametrizado del docente
        {
            if (!string.IsNullOrEmpty(codigo)) //Validación requerida de codigo transferido
            {
                docente = codigo;
            }
        }

        public static void AdminContra(string contraseña)
        {
            using (SqlConnection conexion = new SqlConnection(CONEXION_BD.conectar.ConnectionString))
            {
                conexion.Open();
                string query = "UPDATE Empleados SET Contraseña = @Contraseña WHERE Rol = 'Administrador'";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Contraseña", contraseña);
                    cmd.ExecuteNonQuery(); //Esto permite la ejecución de insert o update
                }
            }
        }

        public static void RegistrarAsistencia(DataGridView dgv, string Docente, string clase, string seccion, string aula, string edificio, DateTime fechaMarca, bool Marco)
        {
            using (SqlConnection conn = new SqlConnection(CONEXION_BD.conectar.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("PA_Marcar_Asistencia", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Docente", Docente);
                cmd.Parameters.AddWithValue("@Asigno", clase);
                cmd.Parameters.AddWithValue("@Seccion", seccion);
                cmd.Parameters.AddWithValue("@Aula", aula);
                cmd.Parameters.AddWithValue("@Edificio", edificio);
                cmd.Parameters.AddWithValue("@Fecha", fechaMarca);
                cmd.Parameters.AddWithValue("@Marca", Marco);
                cmd.ExecuteNonQuery(); //Esto permite la ejecución de insert o update 
            }
        }

        public static DataTable CargarAsistenciaSuperv(MonthCalendar supervisorFechas, string Docente, string clase, string seccion, string aula, string edificio)
        {
            DataTable dtFechas = new DataTable();
            using (SqlConnection conn = new SqlConnection(CONEXION_BD.conectar.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("PA_Asistencia_Superv", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Docente", Docente);
                cmd.Parameters.AddWithValue("@Asigno", clase);
                cmd.Parameters.AddWithValue("@Seccion", seccion);
                cmd.Parameters.AddWithValue("@Aula", aula);
                cmd.Parameters.AddWithValue("@Edificio", edificio);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtFechas);
            }

            foreach (DataRow row in dtFechas.Rows) //De la tabla del SQL para obtener campo fecha
            {
                supervisorFechas.AddBoldedDate(Convert.ToDateTime(row["Fecha"]));
            }

            supervisorFechas.UpdateBoldedDates();

            return dtFechas;
        }

        public static DataTable tablaSupervisor(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(CONEXION_BD.conectar.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("PA_Supervisor", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            if (dt.Rows.Count > 0)
            {
                dgv.Columns.Clear();

                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                dgv.DataSource = bs;
                bs.ResetBindings(false);
                dgv.AutoGenerateColumns = true;
                dgv.Refresh(); // Forzar actualización de la UI

                dgv.Columns[0].Width = 300;
                dgv.Columns[1].Width = 250;
                dgv.Columns[2].Width = 60;
                dgv.Columns[3].Width = 150;
                dgv.Columns[4].Visible = false;
            }
            return dt;
        }

        public static DataTable tablaAdmin (DataGridView dgv)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(CONEXION_BD.conectar.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("PA_Admin", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            if (dt.Rows.Count > 0)
            {
                dgv.Columns.Clear();

                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                dgv.DataSource = bs;
                bs.ResetBindings(false);
                dgv.AutoGenerateColumns = true;
                dgv.Refresh(); // Forzar actualización de la UI

                //Ajustar ancho de las columnas 
                dgv.Columns[0].Width = 115;
                dgv.Columns[1].Width = 170;
                dgv.Columns[2].Width = 58;
                dgv.Columns[3].Width = 183;
                dgv.Columns[4].Width = 325;
            }
            return dt;
        }

        public static DataTable CargarAsistenciaAdmin(MonthCalendar adminFechas, string refiero, string curso, string seccion, string aula, string empleo)
        {
            DataTable dtFechas = new DataTable();
            using (SqlConnection conn = new SqlConnection(CONEXION_BD.conectar.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("PA_Asistencia_Admin", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Referencia", refiero);
                cmd.Parameters.AddWithValue("@Curso", curso);
                cmd.Parameters.AddWithValue("@Seccion", seccion);
                cmd.Parameters.AddWithValue("@Aula", aula);
                cmd.Parameters.AddWithValue("@Empleado", empleo);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtFechas);
            }


            foreach (DataRow row in dtFechas.Rows) //De la tabla del SQL para obtener campo fecha
            {
                adminFechas.AddBoldedDate(Convert.ToDateTime(row["Fecha"]));
            }

            adminFechas.UpdateBoldedDates();

            return dtFechas;
        }

        public static DataTable tablaJustifica(DataGridView dgv) //Decano
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(CONEXION_BD.conectar.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("PA_Justifica", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            if (dt.Rows.Count > 0)
            {
                dgv.Columns.Clear();

                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                dgv.DataSource = bs;
                bs.ResetBindings(false);
                dgv.AutoGenerateColumns = true;
                dgv.Refresh(); // Forzar actualización de la UI

                //Ajustar ancho de las columnas
                dgv.Columns[0].Visible = false;
                dgv.Columns[1].Width = 150;
                dgv.Columns[2].Width = 80;
                dgv.Columns[3].Width = 66;
                dgv.Columns[4].Width = 120;
                dgv.Columns[5].Width = 304;
            }

            return dt;
        }

        public static void Justifico (DataGridView dgv, int Ausencia, string Justificacion)
        {
            using (SqlConnection conn = new SqlConnection(CONEXION_BD.conectar.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("PA_Insertar_Justificacion", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_Asistencia", Ausencia);
                    cmd.Parameters.AddWithValue("@Justificacion", Justificacion);
                    cmd.ExecuteNonQuery(); //Esto permite la ejecución de insert o update
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgv.DataSource = dt;
                }
            }
        }

        public static DataTable tablaRepone(DataGridView dgv) //Decano
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(CONEXION_BD.conectar.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("PA_Repone", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            if (dt.Rows.Count > 0)
            {
                dgv.Columns.Clear();

                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                dgv.DataSource = bs;
                bs.ResetBindings(false);
                dgv.AutoGenerateColumns = true;
                dgv.Refresh(); // Forzar actualización de la UI

                //Ajustar ancho de las columnas
                dgv.Columns[0].Visible = false;
                dgv.Columns[1].Width = 150;
                dgv.Columns[2].Width = 80;
                dgv.Columns[3].Width = 66;
                dgv.Columns[4].Width = 120;
                dgv.Columns[5].Width = 304;
            }
            return dt;
        }

        public static void Repongo(DataGridView dgv, int Ausencia, int dia)
        {
            using (SqlConnection conn = new SqlConnection(CONEXION_BD.conectar.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("PA_Insertar_Justificacion", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_Asistencia", Ausencia);
                    cmd.Parameters.AddWithValue("@Justificacion", Justificacion);
                    cmd.ExecuteNonQuery(); //Esto permite la ejecución de insert o update
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgv.DataSource = dt;
                }
            }
        }

        public void tabla_docente(DataGridView dgv)
        {
            DataTable dt = codigo_doc(); // Se llena los valores del PA según el código en DataTable

            /* Depuración: Mostrar columnas leídas del dgv 
            foreach (DataColumn col in dt.Columns)
            {
                MessageBox.Show($"Columna encontrada: {col.ColumnName}", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }*/

            if (dt.Rows.Count > 0)
            {
                /* Limpia las columnas actuales para evitar duplicados posiblemente por el PA
                o columnas mal detectadas                 por más de una fila*/
                dgv.Columns.Clear();

                // Usa BindingSource para enlazar los datos
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                dgv.DataSource = bs;
                bs.ResetBindings(false);

                dgv.AutoGenerateColumns = true;
                dgv.Refresh(); // Forzar actualización de la UI

                dgv.Columns[0].Width = 125;
                dgv.Columns[1].Width = 58;
                dgv.Columns[2].Width = 20;
                dgv.Columns[3].Width = 22;
                dgv.Columns[4].Width = 22;
                dgv.Columns[5].Width = 20;
                dgv.Columns[6].Width = 20;
                dgv.Columns[7].Width = 20;
            }
            else
            {
                MessageBox.Show("No se encontraron registros.");
            }
        }


        /*        public static void presenteSup(string docente, string asignatura, string seccion, string dia)
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(CONEXION_BD.conectar.ConnectionString))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("PA_Marcar_Asistencia", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@Asignatura", asignatura);
                                cmd.Parameters.AddWithValue("@Docente", docente);
                                cmd.Parameters.AddWithValue("@Seccion", seccion);
                                cmd.Parameters.AddWithValue("@Fecha", DateTime.Today);
                                cmd.Parameters.AddWithValue("@Dia", dia);
                                                cmd.ExecuteNonQuery(); //Esto permite la ejecución de insert o update 
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al marcar asistencia: " + ex.Message);
                    }
                }

                public static void RegistrarFalta(string docente, string asignatura, string seccion, string dia)
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(CONEXION_BD.conectar.ConnectionString))
                        {
                            conn.Open();
                            SqlCommand cmd = new SqlCommand("PA_Registrar_Falta", conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Asignatura", asignatura);
                            cmd.Parameters.AddWithValue("@Docente", docente);
                            cmd.Parameters.AddWithValue("@Seccion", seccion);
                            cmd.Parameters.AddWithValue("@Fecha", DateTime.Today);
                            cmd.Parameters.AddWithValue("@Dia", dia);

                            //                cmd.ExecuteNonQuery(); //Esto permite la ejecución de insert o update
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al registrar falta: " + ex.Message);
                    }
                }
        */

        /*public void cargar(DataGridView dgv, string nombreTabla)
        {
            try
            {
                string consulta = $"SELECT * FROM {nombreTabla}";
                SqlDataAdapter da = new SqlDataAdapter(consulta, conexion.conectar); //usa la conexión de la clase `CONEXION_BD`
                DataTable dt = new DataTable();
                da.Fill(dt);

                MessageBox.Show($"Filas cargadas: {dt.Rows.Count}"); //esto verifica cuántas filas se cargaron

                if (dt.Rows.Count > 0) //se asegura de que hayan datos cargados al dataGridView
                {
                    dgv.DataSource = dt; //asigna el dataTable al dataGridView
                }
                else
                {
                    MessageBox.Show("No se encontraron datos en la tabla.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);   //captura los errores y los envia en un messagebox
                }
            }
            catch { }
        }
        */
    }
}
