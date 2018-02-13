using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class mUsuario
    {
        public int idUsuario { get; set; }
        public string Usuario { get; set; }
        public string contraseña { get; set; }
        public int tipo_usuario { get; set; }
        public int habilitado { get; set; }
        public string Nombre { get; set; }
    }
}
