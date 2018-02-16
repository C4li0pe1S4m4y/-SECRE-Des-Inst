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
    public partial class login : System.Web.UI.Page
    {
        private cUsuario obUsuario;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                obUsuario = new cUsuario();
                this.Session["Usuario"] = this.txtusuario.Text;
                if (obUsuario.login(txtusuario.Text, txtpassword.Text))
                {
                    //redirect
                    FormsAuthentication.RedirectFromLoginPage(this.txtusuario.Text, false);
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    lblError.Text = "Usuario o Contraña Incorectos";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
                throw;
            }

        }
    }
}