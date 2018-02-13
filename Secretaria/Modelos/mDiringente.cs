using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class mDiringente
    {
        public int idDirigente { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public int tipo_dirigente { get; set; }
        public string DPI { get; set; }
        public string Lugar_extendio { get; set; }
        public int NIT { get; set; }
        public string Fecha_nacimiento { get; set; }
        public string Fecha_posesion { get; set; }
        public string Fecha_Finalizacion { get; set; }
        public string Estado { get; set; }
        public string Motivo { get; set; }
        public string correo_electronico { get; set; }
        public string usuario_creo { get; set; }
        public string usuario_modifica { get; set; }
        public string Fecha_crea { get; set; }
        public string Fecha_modifica { get; set; }

        public void mDiringentee(int sDirigente, string sNombres, string sApellidos, int sTipoD)
        {
            this.idDirigente = sDirigente;
            this.nombres = sNombres;
            this.apellidos = sApellidos;
            this.tipo_dirigente = sTipoD;
        }
    }

}
