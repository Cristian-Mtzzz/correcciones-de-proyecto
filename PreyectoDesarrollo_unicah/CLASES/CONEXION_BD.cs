

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace PreyectoDesarrollo_unicah.CLASES
{

    internal class CONEXION_BD
    {
        public static string conexion = "Server=tcp:mssql-193001-0.cloudclusters.net,10058;Initial Catalog=reposuper;User ID=CristianM;Password=Changeme00!+;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        
        //public static string conexion ="Server=2AM89P39-PC\\SQLEXPRESS;Database=reposuper;Trusted_Connection=True;";

        public static SqlConnection conectar = new SqlConnection(conexion);

        public CONEXION_BD() 
        {
        }

        public void abrir() //aqui se habre la conexion de la BDD
        {
         try
            {
                if (conectar.State == ConnectionState.Closed)
                {
                    conectar.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR AL ABRIR LA CONEXIÓN: {ex.Message}", "ERROR");  //captura los errores y los envia en un messagebox
            }
        }


        public void cerrar()    //aqui se cierra la conexion de la BDD
        {
            try {

                if (conectar.State == ConnectionState.Open)
                {
                   conectar.Close();
                }
            }
            catch (Exception ex)    
            {

                MessageBox.Show($"ERROR AL CERRAR LA CONEXIÓN: {ex.Message}", "ERROR"); //captura los errores y los envia en un messagebox
            }
        }
    }
}


