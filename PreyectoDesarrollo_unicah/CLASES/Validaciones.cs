using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreyectoDesarrollo_unicah.CLASES
{
    internal class Validaciones
    {
        public static bool Usuario (object sender, EventArgs e, string usuario, string contraseña)
        {
            if ((usuario == "Usuario:" || usuario == "") && 
               (contraseña == "Contraseña:" || contraseña == "Contraseña nueva:" || contraseña == "")) 
            {
                MessageBox.Show("Datos no ingresados, ingrese sus datos", "Error Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            /*
            if (!SoloNumero(usuario) && usuario.Length <= 8)
            {
                MessageBox.Show("El usuario corresponde a números", "Error Letras", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }*/

            if (usuario != "Usuario:" && usuario.Length > 8)
            {
                MessageBox.Show("Su usuario debe contener ocho o menos caracteres.", "Usuario Largo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (usuario == "Usuario:" || usuario == "")
            {
                MessageBox.Show("Usuario no ingresado, ingrese su usuario", "Error Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

                public static bool SoloNumero(string usuario) 
        {
            return usuario.All(char.IsDigit); 
        }
        
        public static bool Contraseña(object sender, EventArgs e, string usuario, string contraseña, Form Login)
        {
            if (!ACCIONES_BD.AdminCasoContra(usuario, contraseña, Login))
                return false;

            if (contraseña == "Contraseña:" || string.IsNullOrWhiteSpace(contraseña))
            {
                MessageBox.Show("Contraseña no puede quedar vacía, en caso de no obtener, consultar al administrador.", "Contraseña Vacía", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if ((contraseña != "Contraseña:") && contraseña.Length < 8)
            {
                MessageBox.Show("Su contraseña debe contener más de ocho caracteres.\nComuníquese con el Administrador, y espere a que le asigne contraseña correcta", "Contraseña Corta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (contraseña == "Contraseña nueva:" || string.IsNullOrWhiteSpace(contraseña))
            {
                MessageBox.Show("Contraseña no puede quedar vacía.", "Contraseña Vacía", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (contraseña != "Contraseña nueva:" && contraseña.Length < 8)
            {
                MessageBox.Show("La contraseña debe tener al menos 8 caracteres.", "Contraseña Corta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }


        public static bool CodeVale(object sender, EventArgs e, string codigo, TextBox txtCodigo)
        {
            if (txtCodigo.Text == "Código:" || txtCodigo.Text == "") 
            {
                MessageBox.Show("Código no ingresado, ingresar código", "Código Vacío", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtCodigo.Text != "Código:" && txtCodigo.Text.Length != 5)
            {
                MessageBox.Show("El código debe contener cinco caracteres", "Error Cantidad Código", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public static bool CasoAsigno(string contraseña, TextBox txtContraseña)
        {
            if (contraseña == "Contraseña nueva:" || string.IsNullOrWhiteSpace(contraseña))
            {
                txtContraseña.Clear();
                MessageBox.Show("Contraseña no puede quedar vacía.", "Contraseña Vacía", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContraseña.Text = contraseña;
                return false;
            }

            if (contraseña != "Contraseña nueva:" && contraseña.Length < 8)
            {
                MessageBox.Show("La contraseña debe tener al menos 8 caracteres.", "Contraseña Corta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
    }
}


