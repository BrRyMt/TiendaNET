using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES
{
    public class EEmpresa
    {
        public int      idempresa { get; set; }
        public string   razonsocial { get; set; }
        public string   ruc { get; set; }
        public string   direccion { get; set; }
        public string   telefono { get; set; }
        public string   email { get; set; }
        public string   website { get; set; }
        public string   create_at { get; set; }

        //public  DateTime updated_at { get; set; }

        //public  DateTime inactive_at { get; set; }
}
}
