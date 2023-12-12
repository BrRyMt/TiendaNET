using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using System.Data;
using System.Configuration;



namespace DAL
{
    public class DBAccess
    {
        //Objeto conexion
        private SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["AccesoTiendaNET"].ConnectionString);

        //Métodos
        public SqlConnection getconexion()
        {
            //Llamamos  a la conexion
            return this.conexion;
        
        }

        public void abrirConexion()
        {
            if (this.conexion.State == ConnectionState.Closed)
            {
                this.conexion.Open();
            }
        }

        public void CerrarConexion()
        {
            if (this.conexion.State == ConnectionState.Open)
            {
                this.conexion.Close();

                //
            }
        }

        /// <summary>
        /// Es un método general que retorna una conexion de datos de una consulta
        /// que no tiene variables de entrada
        /// </summary>
        /// <param name="spu">Nombre del procedimeinto almacenado</param>
        /// <returns>
        /// Coleccion de datos de tipo DataTable
        /// </returns>
        public DataTable listarDatos(string spu)
        {
            DataTable dt = new DataTable();
            this.abrirConexion();
            SqlCommand comando = new SqlCommand(spu, this.getconexion());
            comando.CommandType = CommandType.StoredProcedure;
            dt.Load(comando.ExecuteReader());
            this.CerrarConexion();

            return dt;
        }

        public DataTable ListarDatosVariable(string spu, string nombreVariable, object valorVariable)
        {
            DataTable dt = new DataTable();
            this.abrirConexion();
            SqlCommand comando = new SqlCommand(spu, this.getconexion());
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue(nombreVariable, valorVariable);

            dt.Load(comando.ExecuteReader());
            this.CerrarConexion();

            return dt;
        }

    }
}
