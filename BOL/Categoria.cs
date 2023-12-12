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
    public class Categoria
    {
        DBAccess conexion = new DBAccess();

        public DataTable listar()
        {
            return conexion.listarDatos("spu_Listar_Categorias");
        }
    }
}
