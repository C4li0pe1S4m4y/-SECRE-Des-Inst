using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class mComiteEjecutivo
    {
        public int idComite { get; set; }
        public int idDirigente { get; set; }
        public int idFadn { get; set; }
        public int periodo { get; set; }
        public string Fecha_inicio { get; set; }
        public string Fecha_final { get; set; }
        public string Acuerdo_cej { get; set; }
        public string Fecha_acuerdo { get; set; }
        public string Estado { get; set; }
        public string acreditacion_cdag { get; set; }
        public string Fecha_acreditacion { get; set; }
        public int estado_comite { get; set; }
  
        public string Fecha_creacion { get; set; }
        public string Fecha_modificacion { get; set; }
        public string usuario_crea { get; set; }
        public string usuario_modifica { get; set; }
        public string noNombrameinto { get; set; }
        public string  fehcaNombrameinto { get; set; }
    }
}
