using Controladores;
using Modelos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace secretaria.Usuarios
{
    public partial class IngresoUsuario : System.Web.UI.Page
    {
        cUsuario contUsuario;
        mUsuario modelUsuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                contUsuario = new cUsuario();
                contUsuario.DdlTipoUsuario(ddlTipoUsuario);
                txtUsuario.Text = " ";
                txtcontra.Text = " ";
            }


        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            modelUsuario = new mUsuario();
            contUsuario = new cUsuario();
            modelUsuario.Nombre = txtNombre.Text;
            modelUsuario.Usuario = txtUsuario.Text;
            modelUsuario.contraseña = txtcontra.Text;
            modelUsuario.habilitado = 1;
            modelUsuario.tipo_usuario = int.Parse(ddlTipoUsuario.SelectedValue);
            try
            {
                contUsuario.InsertUsuario(modelUsuario);
                lblResultado.Visible = true;
                lblResultado.ForeColor = Color.LightGreen;
                lblResultado.Text = "Datos Guardados Con Exito";
                ScriptManager.RegisterStartupScript(this, typeof(string), "Mensaje", "alert('Datos Guardados Con Exito');", true);
                limpiarCampos();   
            }
            catch (Exception ex)
            {
                lblResultado.Visible = true;
                lblResultado.ForeColor = Color.Red;
                lblResultado.Text = "Los Datos no Fueron Guardados.";
                lblResultado.Text = "Error " + ex.Message;
                throw;
            }

        }

        public void limpiarCampos()
        {
            txtNombre.Text = "";
            txtUsuario.Text = "";
            ddlTipoUsuario.SelectedIndex = 0;
        }
    }
}