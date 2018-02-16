using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controladores;
using System.Web.Security;

namespace secretaria
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = Session["Usuario"].ToString();
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
           
           
        }
    }
}