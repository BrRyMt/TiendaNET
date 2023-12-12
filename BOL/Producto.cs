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
    public class Producto
    {
        DBAccess conexion = new DBAccess();
        

        public DataTable listar()
        {
            return conexion.listarDatos("spu_productos_listar");
        }

        public int Registrar(EProducto entidad)
        {
            int totalregistro = 0;

            try
            {
                SqlCommand comando = new SqlCommand("spu_Productos_registrar", conexion.getconexion());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.abrirConexion();
                comando.Parameters.AddWithValue("@idmarca", entidad.idmarca);
                comando.Parameters.AddWithValue("@idsubcategoria", entidad.idsubcategoria);
                comando.Parameters.AddWithValue("@descripcion", entidad.descripcion);
                comando.Parameters.AddWithValue("@garantia", entidad.garantia);
                comando.Parameters.AddWithValue("@precio", entidad.precio);
                comando.Parameters.AddWithValue("@stock", entidad.stock);

                totalregistro = comando.ExecuteNonQuery();
            }
            catch
            {
                totalregistro = -1;
            }
            finally {
                conexion.CerrarConexion();
            }

            return totalregistro;
        }

    }
}
