using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BOL;
using ENTITIES;
using DESIGNER.Tools;   //Herramientas

namespace DESIGNER.Modules
{
    public partial class frmProductos : Form
    {
        Producto producto = new Producto();
        //ENTIDAD
        EProducto entproducto = new EProducto();
        //
  

        Categoria categoria = new Categoria();
        Subcategoria subcategoria = new Subcategoria();
        Marca marca = new Marca();

        //Bandera: Variable LÓGICA que controla estados
        bool categoriasListas = false;

        public frmProductos()
        {
            InitializeComponent();
        }

        #region Método para la carga de datos

        private void ActualizarMarcas()
        {
            cboMarcas.DataSource = marca.listar();
            cboMarcas.DisplayMember = "marca";
            cboMarcas.ValueMember = "idmarca";
            cboMarcas.Refresh();
            cboMarcas.Text = "";
        }

        private void ActualizarCategorias()
        {
            cboCategoria.DataSource = categoria.listar();
            cboCategoria.DisplayMember="categoria";
            cboCategoria.ValueMember="idcategoria";
            cboCategoria.Refresh();
            cboCategoria.Text = "";
        
        }



        private void ActualizarProductos()
        {
            gridPersonas.DataSource = producto.listar();
            gridPersonas.Refresh();
        }

        #endregion
        private void reiniciarInterfaz()
        {
            cboCategoria.Text = "";
            cboMarcas.Text = "";
            cboSubcat.Text = "";
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            txtGarantia.Clear();
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            ActualizarProductos();
            ActualizarMarcas();
            ActualizarCategorias();
            categoriasListas = true;
            txtDescripcion.MaxLength = 100;

            gridPersonas.Columns[0].Visible = false;
            gridPersonas.Columns[1].Width = 130;
            gridPersonas.Columns[2].Width = 130;
            gridPersonas.Columns[3].Width = 150;
            gridPersonas.Columns[4].Width = 240;
            gridPersonas.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridPersonas.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridPersonas.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void cboCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (categoriasListas)
            {
                //Invocar al metodo que carga las subcategorias
                int idcategoria = Convert.ToInt32(cboCategoria.SelectedValue.ToString());
                cboSubcat.DataSource = subcategoria.listar(idcategoria);
                cboSubcat.DisplayMember = "subcategoria";
                cboSubcat.ValueMember = "idsubcategoria";
                cboSubcat.Refresh();
                cboSubcat.Text = "";
            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {


            if (Aviso.Preguntar("¿Estas seguro de ingresar un nuevo producto?") == DialogResult.Yes)
            {
                entproducto.idmarca = Convert.ToInt32(cboMarcas.SelectedValue);
                entproducto.idsubcategoria = Convert.ToInt32(cboSubcat.SelectedValue);
                entproducto.descripcion = txtDescripcion.Text;
                entproducto.garantia = Convert.ToInt32(txtGarantia.Text);
                entproducto.precio = Convert.ToDouble(txtPrecio.Text);
                entproducto.stock = Convert.ToInt32(txtStock.Text);

                if (producto.Registrar(entproducto) > 0)
                {
                    ActualizarProductos();
                    reiniciarInterfaz();
                    Aviso.Informar("Producto Registrado");
                }
                else
                {
                    Aviso.Advertir("Error, no se pudo registrar");

                }
            }


        }

        private void gridProductos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            gridPersonas.ClearSelection();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //1. Crear instacia del reporte (CrystalReport)
            Reports.rptProductos reporte = new Reports.rptProductos();

            //2. Asigna los datos al objeto reporte (creado paso 1)
            reporte.SetDataSource(producto.listar());
            reporte.Refresh();

            //3.
            Reports.frmVisorReportes formulario = new Reports.frmVisorReportes();
            //4.
            formulario.Visor.ReportSource = reporte;
            formulario.Refresh();
            formulario.ShowDialog();
        }

        /// <summary>
        /// Genera un archivo fisico del reporte
        /// </summary>
        /// <param name="extension">Utilice: XLS o PDF</param>
        private void ExportarDatos(string extension)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = $"Archivos en Formato {extension.ToUpper()}|*.{extension.ToLower()}";
            sd.Title = "Reporte de productos";

            if (sd.ShowDialog() == DialogResult.OK)
            {
                //Creamos una version del reporte en formato PDF
                Reports.rptProductos reporte = new Reports.rptProductos();
                reporte.SetDataSource(producto.listar());
                reporte.Refresh();

                //3. El reporte creado y en memoria se ESCIBIRA en el storage
                if (extension.ToUpper() == "PDF")
                {
                    reporte.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, sd.FileName);
                }
                else if (extension.ToUpper() == "XLSX")
                {
                    reporte.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook, sd.FileName);
                }

                //4. Notificar al usuario la creacion del archivo
                Aviso.Informar("Se ha creado el reporte correctamente");
            }

        }
        private void btnExportarxsls_Click(object sender, EventArgs e)
        {
            ExportarDatos("XLSX");
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            ExportarDatos("PDF");
        }

        private void cboSubcat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
