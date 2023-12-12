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

namespace DESIGNER.Modules
{
    public partial class frmEmpresa : Form
    {
        public frmEmpresa()
        {
            InitializeComponent();
        }

        //Funciones de la Empresa (Listar-Registar)
        Empresa Empresa = new Empresa();

        //Objeto de entidad que tiene los datos de Razon, ruc, etc
        EEmpresa entEmpresa = new EEmpresa();

        DataView DtEmp;
        DataTable dtval;

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Aviso.Preguntar("¿Seguro de ingresar esta empresa?") == DialogResult.Yes)
            {

                entEmpresa.razonsocial= txtRazonsocial.Text;
                entEmpresa.ruc = txtRuc.Text;
                entEmpresa.telefono = txtTelefono.Text;
                entEmpresa.email = txtEmail.Text;
                entEmpresa.website = txtWebsite.Text;
                entEmpresa.direccion = txtDireccion.Text;

                dtval = Empresa.Validacion(txtRazonsocial.Text, txtRuc.Text);

                if (dtval.Rows.Count == 0)
                {
                    if (Empresa.Registrar(entEmpresa) > 0)
                    {
                        ActualizarDataView();

                        Aviso.Informar("Registrado Correctamente");
                        Empresa.Registrar(entEmpresa);
                    }
                    else
                    {
                        Aviso.Advertir($"No se puede completar la accion,{dtval.Rows.Count}");
                    }
                }
                else {
                    Aviso.Advertir($"Ya existe esa empresa, revise la informacion,{dtval.Rows.Count}");
                
                }


                

            }

        }

        private void ActualizarDataView()
        {
            DtEmp = new DataView(Empresa.Listar());
            GridEmpresas.DataSource = DtEmp;
            GridEmpresas.Refresh();

            GridEmpresas.Columns[6].Visible = false;

            GridEmpresas.Columns[0].Width = 280;
        }

        private void frmEmpresa_Load(object sender, EventArgs e)
        {
            ActualizarDataView();
            txtRuc.MaxLength=11;
            txtTelefono.MaxLength = 9;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            DtEmp.RowFilter = "razonsocial LIKE '%" + txtBuscar.Text + "%' or ruc LIKE '%" + txtBuscar.Text + "%'";
        }

        private void txtRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
