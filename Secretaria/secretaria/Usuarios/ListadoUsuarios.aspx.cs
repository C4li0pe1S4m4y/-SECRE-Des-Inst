using Controladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace secretaria.Usuarios
{
    public partial class ListadoUsuarios : System.Web.UI.Page
    {
        cUsuario contUsuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            contUsuario = new cUsuario();
            gvListado.DataSource = contUsuario.ListadoUsuarios();

            gvListado.DataBind();
        }

        protected void gvListado_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["numero"] = gvListado.SelectedValue;
            Response.Redirect("ModificarUsuario.aspx?numero=" + Convert.ToString(ViewState["numero"]));
        }
    }
}