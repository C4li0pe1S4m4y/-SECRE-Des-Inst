using Controladores;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Drawing;
using System.Data;

using System.IO;
using System.Web.UI.HtmlControls;
using System.Text;

namespace secretaria.Asistencias
{
    public partial class ControlAsistenciaSoloLectura : System.Web.UI.Page
    {
        cAsistencia contAsistencia;
        mAsistencia modelAsistencia;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvListadoAsistencia.DataBind();
                actualizar();
            }
        }

        protected void gvListadoAsistencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["numero"] = gvListadoAsistencia.SelectedValue;
            Response.Redirect("/Asistencias/ControlAsistencia.aspx?numero=" + Convert.ToString(ViewState["numero"]));
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            modelAsistencia = new mAsistencia();
            contAsistencia = new cAsistencia();
            int nom = Convert.ToInt16(Request.QueryString["numero"]);


            try
            {
                modelAsistencia.Estado = "Presente";
                modelAsistencia.id_asamblea = Convert.ToInt16(Request.QueryString["numero"]);
                

                contAsistencia.InsertarAsistencia(modelAsistencia);
                actualizar();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                Response.Redirect("/Asistencias/ControlAsistencia.aspx?numero=" + Convert.ToString(nom));
                throw;
            }
        }

        protected void btIniciarQuorum_Click(object sender, EventArgs e)
        {
            mAsamblea modelAsamblea = new mAsamblea();
            cAsamblea contAsamblea = new cAsamblea();
            int nom = Convert.ToInt16(Request.QueryString["numero"]);

            try
            {
                contAsamblea.ActualizarEstado(nom, 2);
                actualizar();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            }
        }

        protected void btFinalizarQuorum_Click(object sender, EventArgs e)
        {
            mAsamblea modelAsamblea = new mAsamblea();
            cAsamblea contAsamblea = new cAsamblea();
            cAsistencia contAsistencia = new cAsistencia();
            int nom = Convert.ToInt16(Request.QueryString["numero"]);

            try
            {
                contAsistencia.TerminarAsamblea(nom);
                contAsamblea.ActualizarEstado(nom, 4);
                actualizar();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            }
        }

        protected void btFinalizarAsamblea_Click(object sender, EventArgs e)
        {
            mAsamblea modelAsamblea = new mAsamblea();
            cAsamblea contAsamblea = new cAsamblea();
            cAsistencia contAsistencia = new cAsistencia();
            int nom = Convert.ToInt16(Request.QueryString["numero"]);

            try
            {
                contAsistencia.TerminarAsamblea(nom);
                contAsamblea.ActualizarEstado(nom, 3);
                actualizar();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            }
        }

        protected void btReporte_Click(object sender, EventArgs e)
        {
            ExportToExcel(".xls", gvListadoAsistencia);
        }

        private void ExportToExcel(string nameReport, GridView wControl)
        {
            cAsamblea contAsamblea = new cAsamblea();
            mAsamblea modelAsamblea = new mAsamblea();
            int nom = Convert.ToInt16(Request.QueryString["numero"]);

            modelAsamblea = contAsamblea.Obtner_Asamblea(nom);
            HttpResponse response = Response;
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pageToRender = new Page();
            HtmlForm form = new HtmlForm();
            form.Controls.Add(wControl);
            pageToRender.Controls.Add(form);
            response.Clear();
            response.Buffer = true;
            response.ContentType = "application/vnd.ms-excel";

            modelAsamblea = contAsamblea.Obtner_Asamblea(nom);
            nameReport = modelAsamblea.fecha + nameReport;
            response.AddHeader("Content-Disposition", "attachment;filename=" + nameReport);
            response.Charset = "UTF-8";
            response.ContentEncoding = Encoding.Default;
            pageToRender.RenderControl(htw);
            response.Write(sw.ToString());
            response.End();
        }


        protected void opcionesAsistente_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            e.Row.Cells[0].Visible = false;
        }

        protected void opcionesAsistente_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            cAsistencia contAsistencia = new cAsistencia();

            if (e.CommandName == "Ingresar")
            {
                int nom = Convert.ToInt16(Request.QueryString["numero"]);
                int index = Convert.ToInt16(e.CommandArgument);

                GridViewRow selectedRow = gvListadoAsistencia.Rows[index];
                TableCell idAsistencia = selectedRow.Cells[0];
                Int16 Asistente = Convert.ToInt16(idAsistencia.Text);
                contAsistencia.ActualizarAsistencia(Asistente, "Presente");

                actualizar();
            }

            if (e.CommandName == "Retirar")
            {
                int nom = Convert.ToInt16(Request.QueryString["numero"]);
                int index = Convert.ToInt16(e.CommandArgument);

                GridViewRow selectedRow = gvListadoAsistencia.Rows[index];
                TableCell idAsistencia = selectedRow.Cells[0];
                Int16 Asistente = Convert.ToInt16(idAsistencia.Text);
                contAsistencia.ActualizarAsistencia(Asistente, "Retirado");

                actualizar();
            }

            if (e.CommandName == "Quitar")
            {

                int nom = Convert.ToInt16(Request.QueryString["numero"]);
                int index = Convert.ToInt16(e.CommandArgument);

                GridViewRow selectedRow = gvListadoAsistencia.Rows[index];
                TableCell idAsistencia = selectedRow.Cells[0];
                Int16 Asistente = Convert.ToInt16(idAsistencia.Text);
                contAsistencia.EliminarAsistencia(Asistente);
                actualizar();
            }

        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            actualizar();
            //gvListadoAsistencia.DataBind();
        }

        protected void actualizar()
        {
            cAsistencia contAsistencia = new cAsistencia();
            cAsamblea contAsamblea = new cAsamblea();
            cFADN contFADN = new cFADN();

            mAsamblea modelAsamblea = new mAsamblea();


            int nom = Convert.ToInt16(Request.QueryString["numero"]);
            int necQuorum = (contFADN.TotalFadn() / 2) + 1;
            modelAsamblea = contAsamblea.Obtner_Asamblea(nom);
            int estadoAsamblea = modelAsamblea.estado;
            gvListadoAsistencia.DataSource = contAsistencia.ListadoAsistencia(nom, "ASC"); gvListadoAsistencia.DataBind();
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

            cFADN contFadn = new cFADN();
            cDirigente contDirigente = new cDirigente();
            mDiringente modelDirigente = new mDiringente();
            DataTable dtDirigentes = contDirigente.dtDirigentes(nom, 0);
            
        }
    }
}