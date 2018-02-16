using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controladores;
using Modelos;
using System.Drawing;

namespace secretaria.FADN
{
    public partial class ModificacionFADN : System.Web.UI.Page
    {
        cFADN controladorFand;
        mFadn modeloFand;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int nom = Convert.ToInt16(Request.QueryString["numero"]);
                controladorFand = new cFADN();
                modeloFand = new mFadn();
                //Datos Federacion.
                
                modeloFand = controladorFand.Obtener_Fadn(nom);
                lblFederacion.Text = modeloFand.Nombre;
                txtDireccion.Text = modeloFand.Direccion;
                txtTelefono.Text = modeloFand.Telefono;
                txtCorreo.Text = modeloFand.correo_electronico;
                if (modeloFand.logo != null)
                {
                    string base64String = Convert.ToBase64String(modeloFand.logo, 0, modeloFand.logo.Length);
                    ImagenFADN.ImageUrl = "data:image/jpeg;base64," + base64String;

                }
                //Datos junta
                gvComite.DataSource = controladorFand.Obtener_Junta(nom);
                gvComite.DataBind();
                //Datos Interino
                gvInterino.DataSource = controladorFand.Obtener_Comite_Interino(nom);
                gvInterino.DataBind();
            }
           
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Boolean fileOK = false;
            int nom = Convert.ToInt16(Request.QueryString["numero"]);
            controladorFand = new cFADN();
            modeloFand = new mFadn();
            if (FileUpload1.HasFile)
            {
                String fileExtension =
                    System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                String[] allowedExtensions =
                    {".gif", ".png", ".jpeg", ".jpg"};
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
            }
            if (fileOK)
            {
                try
                {
                    modeloFand.logo = FileUpload1.FileBytes;
                    lblResultado.Text = "File uploaded!";
                }
                catch (Exception ex)
                {
                    lblResultado.Text = "File could not be uploaded.";
                }
            }
            else
            {
                lblResultado.Text = "Cannot accept files of this type.";
            }
            modeloFand.id_fand = nom;
            modeloFand.Direccion = txtDireccion.Text;
            modeloFand.Telefono = txtTelefono.Text;
            modeloFand.correo_electronico = txtCorreo.Text;
            modeloFand.Nombre = lblFederacion.Text;
            try
            {
                //controladorFand.Actualizar_FADN(modeloFand);
                lblResultado.Visible = true;
                lblResultado.ForeColor = Color.LightGreen;
                lblResultado.Text = "Datos Guardados Con Exito";
                ScriptManager.RegisterStartupScript(this, typeof(string), "Mensaje", "alert('Datos Guardados Con Exito');", true);

            }
            catch (Exception ex)
            {
                lblResultado.Visible = true;
                lblResultado.ForeColor = Color.Red;
                lblResultado.Text = "Los Datos no Fueron Guardados";
                lblResultado.Text = "Error " +  ex.Message;
            }
           
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            txtCorreo.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
        }
    }
}