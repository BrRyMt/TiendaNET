using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using DAL; //ACCESO A CONEXION SERVER + BD
using System.Data;  //Objetos manejadores de datos
using System.Data.SqlClient; //Acceso MSSQL Server
using ENTITIES;     //Entidades (estructuras)

namespace BOL
{
    //Clase pública
    public class Usuario
    {
        //Instancia de la clase de conexión
        DBAccess conexion = new DBAccess();



        /// <summary>
        /// Inicia Sesion utilizando datos del servidor
        /// </summary>
        /// <param name="email">identificador o nombre de usuario</param>
        /// <returns>
        /// Objeto conteniendo toda la fila (varios campos)
        /// </returns>

        public DataTable iniciarSesion(string email)
        {
            //1. Objeto que contendra el resultado
            DataTable dt = new DataTable();

            //2. Abrir conexion
            conexion.abrirConexion();

            //3. Objeto para enviar consulta
            SqlCommand comando = new SqlCommand("spu_usuarios_login",conexion.getconexion());

            //4. Tipo de comando(Procedimiento almacenado)
            comando.CommandType = CommandType.StoredProcedure;

            //5. Pasar las variables
            comando.Parameters.AddWithValue("@email", email);

            //6. Ejecutar y obtener/leer los datos
            dt.Load(comando.ExecuteReader());

            //7.Cerrar
            conexion.CerrarConexion();



            //8. Retornamos el objeto con la info
            return dt;
        }

        public DataTable login(string email)
        {
            return conexion.ListarDatosVariable("spu_usuarios_login","@email",email);
        }


        public int Registrar(EUsuario entidad)
        {
            int totalRegistros =0;


            SqlCommand comando = new SqlCommand("spu_usuarios_registrar", conexion.getconexion());
            comando.CommandType = CommandType.StoredProcedure;

            conexion.abrirConexion();

            try
            {
                comando.Parameters.AddWithValue("@apellidos", entidad.apellidos);
                comando.Parameters.AddWithValue("@nombres", entidad.nombres);
                comando.Parameters.AddWithValue("@email", entidad.email);
                comando.Parameters.AddWithValue("@claveacceso", entidad.claveacceso);
                comando.Parameters.AddWithValue("@nivelacceso", entidad.nivelacceso);

                totalRegistros=comando.ExecuteNonQuery();
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
            SqlCommand comando = new SqlCommand("spu_usuarios_listar", conexion.getconexion());
            comando.CommandType = CommandType.StoredProcedure;

            conexion.abrirConexion();
            dt.Load(comando.ExecuteReader());
            conexion.CerrarConexion();

            return dt;
        
        }
    }
}
