using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
     public class mBitacora
    {
        public int id_bitacora { get; set; }
        public string acuerdo { get; set; }
        public string estado_anterior { get; set; }
        public string estado_actual { get; set; }
        public string usuario { get; set; }
        public string Fecha { get; set; }
        public string ip { get; set; }
        public string mac { get; set; }
        public string pc { get; set; }
    }
}
