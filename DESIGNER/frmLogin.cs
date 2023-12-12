using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Incluir Librerias
using CryptSharp;  //Claves encriptadas
using BOL;
using DESIGNER.Tools; //Traemos nuestras herramientas



namespace DESIGNER
{
    public partial class frmLogin : Form
    {
        //Instancia de la clase

        Usuario usuario = new Usuario();
        DataTable dtRpta = new DataTable();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void Login()
        {
            if (txtEmail.Text.Trim() == String.Empty)
            {
                ErrorLogin.SetError(txtEmail, "Porfavor ingrese su Email");
                txtEmail.Focus();
            }
            else
            {
                ErrorLogin.Clear();
                if (txtClaveAcceso.Text.Trim() == String.Empty)
                {
                    ErrorLogin.SetError(txtEmail, "Porfavor ingrese su contraseña");
                    txtClaveAcceso.Focus();
                }
                else
                {
                    ErrorLogin.Clear();
                    //Los datos son ingresados, validamos el acceso
                    dtRpta = usuario.iniciarSesion(txtEmail.Text);

                    if (dtRpta.Rows.Count > 0)
                    {
                        string claveencriptada = dtRpta.Rows[0][4].ToString();
                        string apellidos = dtRpta.Rows[0][1].ToString();
                        string nombres = dtRpta.Rows[0][2].ToString();

                        if (Crypter.CheckPassword(txtClaveAcceso.Text, claveencriptada))
                        {
                            Aviso.Informar($"Welcome! {apellidos} {nombres}");
                            frmMain formMain = new frmMain();
                            formMain.Show(); //Abre el formulario principal
                            this.Hide(); //Login se oculta
                        }
                        else
                        {

                            Aviso.Advertir("Password Error!");
                        }
                    }
                    else
                    {
                        Aviso.Advertir("User not exist!");
                    }

                }

            }

        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            Login();


        }

        private void txtClaveAcceso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==Convert.ToChar(Keys.Enter))
            {
                Login();
            
            }
        }
    }
}
