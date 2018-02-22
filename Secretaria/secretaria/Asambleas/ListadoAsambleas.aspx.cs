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
            if (!IsPostBack)
            {
                contAsamblea = new cAsamblea();
                gvListadoA.DataSource = contAsamblea.ListadoAsambleas();
                gvListadoA.DataBind();
            }
                
        }

        protected void gvListadoA_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["numero"] = gvListadoA.SelectedValue;
            Response.Redirect("/Asistencias/ControlAsistencia.aspx?numero=" + Convert.ToString(ViewState["numero"]));
        }

        protected void opcionesAsamblea_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
        }

        protected void opcionesAsamblea_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            cAsistencia contAsistencia = new cAsistencia();

            if (e.CommandName == "Ingresar")
            {
                int index = Convert.ToInt16(e.CommandArgument);
                GridViewRow selectedRow = gvListadoA.Rows[index];
                TableCell idAsamblea = selectedRow.Cells[0];
                Int16 Asamblea = Convert.ToInt16(idAsamblea.Text);

                Response.Redirect("/Asistencias/ControlAsistencia.aspx?numero=" + Asamblea);
            }

            if (e.CommandName == "Listado")
            {
                int index = Convert.ToInt16(e.CommandArgument);
                GridViewRow selectedRow = gvListadoA.Rows[index];
                TableCell idAsamblea = selectedRow.Cells[0];
                Int16 Asamblea = Convert.ToInt16(idAsamblea.Text);
                Response.Redirect("/Asistencias/ControlAsistenciaSoloLectura.aspx?numero=" + Asamblea);
            }
        }

    }

    
}