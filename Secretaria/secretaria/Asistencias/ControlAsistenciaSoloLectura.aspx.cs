using Controladores;
using Modelos;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace secretaria.Asistencias
{
    public partial class ControlAsistenciaSoloLectura : System.Web.UI.Page
    {
        cAsistencia contAsistencia = new cAsistencia();
        mAsistencia modelAsistencia = new mAsistencia();
        cDirigente contDirigente = new cDirigente();
        mDiringente modelDirigente = new mDiringente();
        mAsamblea modelAsamblea = new mAsamblea();
        cAsamblea contAsamblea = new cAsamblea();
        cFADN contFADN = new cFADN();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int nom = Convert.ToInt16(Request.QueryString["numero"]);
                gvListadoAsistencia.DataSource = contAsistencia.ListadoAsistencia(nom, "ASC");
                gvListadoAsistencia.DataBind();
                actualizar();
            }
        }

        protected void opcionesAsistente_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[8].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string lectura = DataBinder.Eval(e.Row.DataItem, "Lectura").ToString();

                switch(lectura)
                {
                    case "1":
                        e.Row.BackColor = System.Drawing.Color.Aquamarine;
                        break;

                    case "2":
                        e.Row.BackColor = System.Drawing.Color.AliceBlue;
                        break;

                    default:
                        e.Row.BackColor = System.Drawing.Color.Empty;
                        break;
                }
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvListadoAsistencia, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
            }
        }

        protected void gvListadoAsistencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = gvListadoAsistencia.SelectedRow.RowIndex;
            string id = gvListadoAsistencia.SelectedRow.Cells[0].Text;

            gvListadoAsistencia.SelectedRow.BackColor = System.Drawing.Color.Aquamarine;
            contAsistencia.ActualizarLectura(int.Parse(id), "1");

            int nom = Convert.ToInt16(Request.QueryString["numero"]);
            gvListadoAsistencia.DataSource = contAsistencia.ListadoAsistencia(nom, "ASC");
            gvListadoAsistencia.DataBind();
        }
        
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            actualizar();
        }

        protected void actualizar()
        {
            int nom = Convert.ToInt16(Request.QueryString["numero"]);
            int necQuorum = (contFADN.TotalFadn() / 2) + 1;
            modelAsamblea = contAsamblea.Obtner_Asamblea(nom);
            int estadoAsamblea = modelAsamblea.estado;

            gvListadoAsistencia.DataSource = contAsistencia.ListadoAsistencia(nom, "ASC");
            gvListadoAsistencia.DataBind();
            int totalListado = gvListadoAsistencia.Rows.Count;
            int totalAsistentes = 0;
            if (lblTotalAsistentes.Text != "") totalAsistentes = int.Parse(lblTotalAsistentes.Text);

            lblTotalAsistentes.Text = Convert.ToString(contAsistencia.TotalAsistentes(nom)); lblTotalAsistentes.DataBind();
            lblTotalRetirados.Text = Convert.ToString(contAsistencia.TotalRetirados(nom)); lblTotalAsistentes.DataBind();
            lblTotalFederados.Text = Convert.ToString(contAsistencia.TotalFederados(nom)); lblTotalFederados.DataBind();

            if (contAsistencia.TotalFederados(nom) < necQuorum) lblTotalFederados.CssClass = "label label-danger";
            else lblTotalFederados.CssClass = "label label-success";

            switch (estadoAsamblea)
            {
                case 1:
                    if (contAsistencia.TotalFederados(nom) < necQuorum)
                    {
                        lblEstadoAsamblea2.Text = "No se ha iniciado la Asamblea."; lblEstadoAsamblea2.DataBind();                        
                    }
                    else
                    {
                        lblEstadoAsamblea2.Text = "No se ha iniciado la Asamblea."; lblEstadoAsamblea2.DataBind();                        
                    }
                    
                    break;

                case 2:                    
                    lblEstadoAsamblea2.Text = "La Asamblea se ha iniciado."; lblEstadoAsamblea2.DataBind();
                    lblHora.Text = modelAsamblea.inicio; lblHora.DataBind();                    
                    break;

                case 3:
                    lblEstadoAsamblea2.Text = "El Quórum ne se pudo realizar. La asamblea finalizó."; lblEstadoAsamblea2.DataBind();
                    lblHora.Text = modelAsamblea.final; lblHora.DataBind();                    
                    break;

                case 4:                    
                    lblEstadoAsamblea2.Text = "La Asamblea ha terminado."; lblEstadoAsamblea2.DataBind();
                    lblHora.Text = modelAsamblea.final; lblHora.DataBind();                    
                    break;
            }
            modelAsamblea = contAsamblea.Obtner_Asamblea(nom);
            lblDescripcion.Text = modelAsamblea.descripcion;          
        }
    }
}