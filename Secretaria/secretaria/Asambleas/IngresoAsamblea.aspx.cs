using Controladores;
using Modelos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace secretaria.Asambleas
{
    public partial class IngresoAsamblea : System.Web.UI.Page
    {
        cAsamblea contAsamblea;
        mAsamblea modelAsamblea;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                contAsamblea = new cAsamblea();
                descripcion.InnerText = " ";
                contAsamblea.DdlTipoAsamblea(ddlTipoAsamblea);
                
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            modelAsamblea = new mAsamblea();
            contAsamblea = new cAsamblea();
            modelAsamblea.tipo_asamblea = int.Parse(ddlTipoAsamblea.SelectedItem.Value);
            modelAsamblea.descripcion = descripcion.InnerText;
            modelAsamblea.fecha = fechaAsamblea.Value;
            try
            {
                contAsamblea.InsertarAsamblea(modelAsamblea);
                lblResultado.Visible = true;
                lblResultado.ForeColor = Color.LightGreen;
                lblResultado.Text = "Datos Guardados Con Exito";
                Response.Redirect("~/ListadoAsambleas.aspx");
            }
            catch (Exception ex)
            {
                lblResultado.Visible = true;
                lblResultado.ForeColor = Color.Red;
                lblResultado.Text = "Los Datos no Fueron Guardados.";
                lblResultado.Text = "Error " + ex.Message;
                limpiarCampos();
                throw;
            }
            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
        }

        public void limpiarCampos()
        {
            ddlTipoAsamblea.SelectedIndex = 0;
            fechaAsamblea.Value = "";
            descripcion.Value = "";
        }
    }
}