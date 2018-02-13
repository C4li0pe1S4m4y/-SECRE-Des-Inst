using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class mAsistencia
    {
        public int id_asistencia { get; set; }
        public string Estado { get; set; }
        public int id_asamblea { get; set; }
        public int idDirigente { get; set; }
        public string hora_entrada { get; set; }
        public string hora_salida { get; set; }
        public int id_tipo_asistencia { get; set; }
    }
}
