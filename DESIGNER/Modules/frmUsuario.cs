using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BOL;              //Logica
using ENTITIES;         //Estructura
using DESIGNER.Tools;   //Herramientas
using CryptSharp;

namespace DESIGNER.Modules
{
    public partial class frmUsuario : Form
    {
        //Objeto "Usuario" contiene las funciones  Registrar, eliminar, listar, etc
        Usuario usuario = new Usuario();

        //Objeto "entUsuarui" contiene los Datos => Apellidos, nombres, email, clave ,etc
        EUsuario entUsuario = new EUsuario();

        string nivelAcceso ="INV" ;

        //Reservado un espacio de memoria para el objeto
        DataView dv;



        public frmUsuario()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //
            if (Aviso.Preguntar("¿Esta seguro de guardar?") == DialogResult.Yes)
            {

                string claveencriptada = Crypter.Blowfish.Crypt(txtClave.Text);

                entUsuario.apellidos = txtApellidos.Text;
                entUsuario.nombres = txtNombres.Text;
                entUsuario.email = txtEmail.Text;
                entUsuario.claveacceso = claveencriptada;
                entUsuario.nivelacceso = nivelAcceso;

                if (usuario.Registrar(entUsuario) > 0)
                {
                    reiniciarInterfaz();
                    actualizarDatos();
                    Aviso.Informar("New user resgist");

                }
                else
                {
                    Aviso.Advertir("No se puedo terminar el registro");
                }

                usuario.Registrar(entUsuario);
                Aviso.Informar("New user add!");

            }
        }

        private void actualizarDatos()
        {
            dv = new DataView(usuario.Listar());
            

            gridUsuarios.DataSource = dv;
            gridUsuarios.Refresh();

            gridUsuarios.Columns[0].Visible = false;  //ID
            gridUsuarios.Columns[4].Visible = false;    //CLAVE

            gridUsuarios.Columns[1].Width = 181; //Apellidos
            gridUsuarios.Columns[2].Width = 181; //Nombres
            gridUsuarios.Columns[3].Width = 180; //Email
            gridUsuarios.Columns[5].Width = 180; //Nivelacceso

            gridUsuarios.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan;

        }

        private void reiniciarInterfaz()
        {
            txtApellidos.Clear();
            txtNombres.Clear();
            txtEmail.Clear();
            txtClave.Clear();
            optInvitado.Checked=true;
            nivelAcceso = "INV";
            txtApellidos.Focus();
        }


        private void optAdministrador_CheckedChanged(object sender, EventArgs e)
        {
            nivelAcceso = "ADM";
        }

        private void optInvitado_CheckedChanged(object sender, EventArgs e)
        {
            nivelAcceso = "INV";
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            actualizarDatos();
        }

        private void gridUsuarios_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            gridUsuarios.ClearSelection();
        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            dv.RowFilter = "apellidos LIKE '%" + txtBuscar.Text + "%' or nombres LIKE '%" + txtBuscar.Text + "%'";
        }
    }
}
