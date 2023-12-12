using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL;
using System.Data;  //Objetos manejadores de datos
using System.Data.SqlClient; //Acceso MSSQL Server

namespace BOL
{
    public class Subcategoria
    {
        DBAccess conexion = new DBAccess();

        public DataTable listar(int idcategoria)
        {

            return conexion.ListarDatosVariable("spu_subcategoria_Listar", "@idcategoria", idcategoria);
        
        }

    }
}
