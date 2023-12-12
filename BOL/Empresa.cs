using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL;
using System.Data;  //Objetos manejadores de datos
using System.Data.SqlClient; //Acceso MSSQL Server
using ENTITIES;



namespace BOL
{
    public class Empresa
    {

        DBAccess conexion = new DBAccess();


        public DataTable Validacion(string RazonSoc,string Ruc)
        {
            DataTable dt = new DataTable();
            conexion.abrirConexion();
            SqlCommand comando = new SqlCommand("spu_validacion", conexion.getconexion());
            comando.CommandType = CommandType.StoredProcedure;
            

            comando.Parameters.AddWithValue("@razonsocial", RazonSoc);
            comando.Parameters.AddWithValue("@ruc", Ruc);

            dt.Load(comando.ExecuteReader());

            conexion.CerrarConexion();
            return dt;

        }

        public int Registrar(EEmpresa entidad)
        {
            int totalRegistros = 0;


            SqlCommand comando = new SqlCommand("spu_registrar_empresa", conexion.getconexion());
            comando.CommandType = CommandType.StoredProcedure;

            conexion.abrirConexion();

            try
            {
                comando.Parameters.AddWithValue("@razonsocial", entidad.razonsocial);
                comando.Parameters.AddWithValue("@ruc", entidad.ruc);
                comando.Parameters.AddWithValue("@direccion", entidad.direccion);
                comando.Parameters.AddWithValue("@telefono", entidad.telefono);
                comando.Parameters.AddWithValue("@email", entidad.email);
                comando.Parameters.AddWithValue("@website", entidad.website);
                //comando.Parameters.AddWithValue("@create_at", entidad.create_at);
                //comando.Parameters.AddWithValue("@updated_at", entidad.updated_at);
                //comando.Parameters.AddWithValue("@inactive_at", entidad.inactive_at);

                totalRegistros = comando.ExecuteNonQuery();
            }
            catch
            {
                totalRegistros = -1;
            }
            finally
            {
                conexion.CerrarConexion();
            }

            return totalRegistros;

        }

        public DataTable Listar()
        {
            DataTable dt = new DataTable();
            SqlCommand comando = new SqlCommand("spu_listar_empresa", conexion.getconexion());
            comando.CommandType = CommandType.StoredProcedure;

            conexion.abrirConexion();
            dt.Load(comando.ExecuteReader());
            conexion.CerrarConexion();

            return dt;

        }
    }
}
