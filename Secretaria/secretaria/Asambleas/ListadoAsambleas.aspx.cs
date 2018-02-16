using Controladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace secretaria.Asambleas
{
    public partial class ListadoAsambleas : System.Web.UI.Page
    {
        cAsamblea contAsamblea;
        protected void Page_Load(object sender, EventArgs e)
        {
            contAsamblea = new cAsamblea();
            gvListadoA.DataSource = contAsamblea.ListadoAsambleas();
            gvListadoA.DataBind();
        }

        protected void gvListadoA_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["numero"] = gvListadoA.SelectedValue;
            Response.Redirect("/Asistencias/ControlAsistencia.aspx?numero=" + Convert.ToString(ViewState["numero"]));
        }

    }
}