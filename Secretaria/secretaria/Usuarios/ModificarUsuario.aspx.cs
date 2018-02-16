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
    public partial class ModificarUsuario : System.Web.UI.Page
    {
        mUsuario modelUsuario;
        cUsuario controlUsuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                modelUsuario = new mUsuario();
                controlUsuario = new cUsuario();
                int nom = Convert.ToInt16(Request.QueryString["numero"]);
                modelUsuario = controlUsuario.Obtner_Usuario(nom);
                lblUsuario.Text = modelUsuario.Usuario;
                txtNombre.Text = modelUsuario.Nombre;
                ddlEstado.Items.Clear();
                ddlEstado.AppendDataBoundItems = true;
                ddlEstado.Items.Add("<< Elija un valor >>");
                ddlEstado.Items[0].Value = "0";
                ddlEstado.Items.Add("Activo");
                ddlEstado.Items[1].Value = "1";
                ddlEstado.Items.Add("Incativo");
                ddlEstado.Items[2].Value = "0";
                ddlEstado.DataBind();
                controlUsuario.DdlTipoUsuario(ddlTipoUsuario);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            modelUsuario = new mUsuario();
            controlUsuario = new cUsuario();
            modelUsuario.idUsuario = Convert.ToInt16(Request.QueryString["numero"]);
            modelUsuario.Nombre = txtNombre.Text;
            modelUsuario.contraseña = txtContraseña.Text;
            modelUsuario.habilitado = int.Parse(ddlEstado.SelectedItem.Value);
            modelUsuario.tipo_usuario = int.Parse(ddlTipoUsuario.SelectedValue);
            try
            {
                controlUsuario.ActualizarUsuario(modelUsuario);
                lblResultado.Visible = true;
                lblResultado.ForeColor = Color.LightGreen;
                lblResultado.Text = "Datos Guardados Con Exito";
                ScriptManager.RegisterStartupScript(this, typeof(string), "Mensaje", "alert('Datos Guardados Con Exito');", true);

            }
            catch (Exception ex)
            {
                lblResultado.Visible = true;
                lblResultado.ForeColor = Color.Red;
                
                lblResultado.Text = "Error " + ex.Message;
            }
        }
    }
}