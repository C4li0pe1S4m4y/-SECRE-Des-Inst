using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
     public class mFadn
    {
        public int id_fand { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Fecha_creacion { get; set; }
        public string Telefono { get; set; }
        public string correo_electronico { get; set; }
        public byte[] logo { get; set; }

        public void mFand(int sfand,string snombre,string sdireccio,string stelefono,string scorreo,byte[] logo)
        {
            this.id_fand = sfand;
            this.Nombre = snombre;
            this.Direccion = sdireccio;
            this.Telefono = stelefono;
            this.correo_electronico = scorreo;
            this.logo = logo;
        }
    }
}
