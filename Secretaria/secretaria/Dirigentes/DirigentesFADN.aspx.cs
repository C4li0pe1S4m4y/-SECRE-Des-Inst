using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controladores;
using Modelos;
using System.Data;
namespace secretaria.Dirigentes
{
    public partial class DirigentesFADN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                cDirigente cd = new cDirigente();
                cd.Fill_FadnDDL(ddl_fadn);
                cd.FillDeptos(ddl_dpto);
                cd.FillEstadosDirigente(ddl_estado);
                cd.FillEstadosComite(ddl_estado_comite);
            }
        }

        protected void gv_fadn_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel_opciones_dirigente.Visible = true;
            lbtn_cambiar.Attributes.Clear();
            lbtn_cambiar.Attributes.Add("idDir", gv_fadn.SelectedDataKey.Value.ToString());
            btn_cambio_estado.Attributes.Clear();
            btn_cambio_estado.Attributes.Add("idDir", gv_fadn.SelectedDataKey.Value.ToString());
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            int value = int.Parse(ddl_fadn.SelectedValue);
            if (value > 0)
            {
                cFADN controladorFand = new cFADN();
                //Datos Federacion.
                mFadn modeloFand = controladorFand.Obtener_Fadn(value);
                lbl_fadn.Text = modeloFand.Nombre;
                lbl_correo.Text = modeloFand.correo_electronico;
                //Datos junta
                cDirigente controladorDirigente = new cDirigente();
                DataTable dta = controladorDirigente.Obtener_Junta(value);
                gv_fadn.DataSource = dta;
                gv_fadn.DataBind();
                panel_junta.Visible = true;
                controladorDirigente.Suspendidos(gv_suspendidos, value);
                if(controladorDirigente.FillVacantes(ddl_vacantes, value))
                {
                    lbtn_ingresar.Enabled = true;
                }
                else
                {
                    lbtn_ingresar.Enabled = false;
                }
                panel_vacantes.Visible = true;
                lbtn_ingresar.Attributes.Clear();
                lbtn_ingresar.Attributes.Add("fadn", value.ToString());
            }
            else
            {
                lbl_fadn.Text = "";
                lbl_correo.Text = "";
                panel_junta.Visible = false;
                panel_vacantes.Visible = false;
                ddl_fadn.Focus();
            }
            panel_opciones_dirigente.Visible = false;
        }

        protected void lbtn_baja_Click(object sender, EventArgs e)
        {
            panel_estado.Visible = true;
        }

        protected void ddl_estado_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool show = false;
            string seleccion = ddl_estado.SelectedItem.Text;
            if(seleccion == "otro")
            {
                lbl_motivo.Visible = true;
                tb_motivo.Visible = true;
                tb_motivo.Text = "";
            }
            else
            {
                lbl_motivo.Visible = false;
                tb_motivo.Visible = false;
                if(seleccion == "suspendido")
                {
                    show = true;
                }
            }

            lbl_inicio_susp.Visible = show;
            lbl_fin_susp.Visible = show;
            tb_fecha_inicio_susp.Visible = show;
            tb_fecha_fin_susp.Visible = show;
        }

        protected void lbtn_ingresar_Click(object sender, EventArgs e)
        {
            if (int.Parse(ddl_vacantes.SelectedValue) > 0)
            {
                ClearDirigente();
                ClearComite();
                lbl_titulo_modal.Attributes.Clear();
                lbl_titulo_modal.Attributes.Add("tipo", ddl_vacantes.SelectedValue);
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "abrirModal('#myModal');", true);
                btn_actualizar_comite.Visible = false;
                btn_actualizar_dir.Visible = false;
                btn_anterior.Visible = true;
                btn_siguiente.Visible = true;
                btn_aplicar_dirigente.Visible = true;
                lbl_titulo_modal.Text = "Ingresar nuevo dirigente";
            }
        }

        protected void lbtn_cambiar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "abrirModal('#myModal');", true);
            btn_actualizar_comite.Visible = true;
            btn_actualizar_dir.Visible = true;
            btn_anterior.Visible = false;
            btn_siguiente.Visible = false;
            btn_aplicar_dirigente.Visible = false;
            lbl_titulo_modal.Text = "Modificar dirigente";
            cDirigente cdir = new cDirigente();
            cdir.FillDirigente(new TextBox[5] { tb_nombres, tb_apellidos, tb_dpi, tb_nit, tb_correo }, ddl_dpto,
                int.Parse(lbtn_cambiar.Attributes["idDir"]));
            cdir.FillComite(new TextBox[13] { tb_periodo, tb_fechainicio, tb_fechafinal, tb_acuerdo, tb_fecha_acuerdo, tb_acreditacion, tb_fecha_acreditacion,
                tb_finiquito, tb_fecha_finiquito,  tb_acta_tomap, tb_fecha_acta_tomap,tb_no_tedefe, tb_fecha_tedefe }, ddl_estado_comite, int.Parse(lbtn_cambiar.Attributes["idDir"]));
        }

        protected void btn_cambio_estado_Click(object sender, EventArgs e)
        {
            cDirigente cdir = new cDirigente();
            if(cdir.Dar_Baja(int.Parse(btn_cambio_estado.Attributes["idDir"]), ddl_estado.SelectedItem.Text, tb_motivo.Text))
            {
                Response.Redirect("DirigentesFADN.aspx");
            }
        }

        void ClearDirigente()
        {
            tb_nombres.Text = "";
            tb_apellidos.Text = "";
            tb_dpi.Text = "";
            ddl_dpto.SelectedIndex = 0;
            tb_nit.Text = "";            
            tb_correo.Text = "";
        }

        void ClearComite()
        {
            ddl_estado_comite.SelectedIndex = 0;
            tb_periodo.Text = "0";
            tb_fecha_tedefe.Text = "";
            tb_no_tedefe.Text = "";
            tb_fechainicio.Text = "";
            tb_fechafinal.Text = "";
            tb_acuerdo.Text = "";
            tb_fecha_acuerdo.Text = "";
            tb_finiquito.Text = "";
            tb_fecha_finiquito.Text = "";
            tb_acreditacion.Text = "";
            tb_fecha_acreditacion.Text = "";
            tb_acta_tomap.Text = "";
            tb_fecha_acta_tomap.Text = "";
        }

        bool ValidarDirigente()
        {
            return true;
        }

        protected void btn_aplicar_dirigente_Click(object sender, EventArgs e)
        {
            cDirigente cDir = new cDirigente();
            if (cDir.IngresarDirigente(new string[8] { tb_nombres.Text, tb_apellidos.Text, lbl_titulo_modal.Attributes["tipo"].ToString(), tb_dpi.Text, ddl_dpto.SelectedItem.Text,
                tb_nit.Text,  tb_correo.Text, Session["Usuario"].ToString()},
                new string[16] {lbtn_ingresar.Attributes["fadn"], tb_periodo.Text, tb_fechainicio.Text, tb_fechafinal.Text, tb_acuerdo.Text, tb_fecha_acuerdo.Text,  tb_acreditacion.Text, tb_fecha_acreditacion.Text,
                ddl_estado_comite.SelectedItem.Text, tb_finiquito.Text, tb_fecha_finiquito.Text, tb_acta_tomap.Text, tb_fecha_acta_tomap.Text,
                    Session["Usuario"].ToString(),  tb_no_tedefe.Text, tb_fecha_tedefe.Text}))
            {
                ClearDirigente();
                ClearComite();
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "cerrarModal('#myModal');", true);
            }
        }

        protected void btn_siguiente_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "irTab('#comite');", true);
        }

        protected void btn_actualizar_dir_Click(object sender, EventArgs e)
        {
            cDirigente cdir = new cDirigente();
            if(cdir.ActualizarDirigente(new string[8] { tb_nombres.Text, tb_apellidos.Text, tb_dpi.Text, ddl_dpto.SelectedItem.Text,
                tb_nit.Text,  tb_correo.Text, Session["Usuario"].ToString(),lbtn_cambiar.Attributes["idDir"]}))
            {
            }
        }

        protected void btn_actualizar_comite_Click(object sender, EventArgs e)
        {
            string Fecha_acuerdo = ""; string fehca_acreditacion = ""; string fehca_acta = ""; string fecha_finiquito = "";string fecha_tedefe = "";
            if (string.IsNullOrEmpty(tb_fecha_acuerdo.Text))
                Fecha_acuerdo = "null";
            else
                Fecha_acuerdo = "'" + tb_fecha_acuerdo.Text+ "'";
            if (string.IsNullOrEmpty(tb_fecha_acreditacion.Text))
                fehca_acreditacion = "null";
            else
                fehca_acreditacion = "'" + tb_fecha_acreditacion.Text + "'" ;
            if (string.IsNullOrEmpty(tb_fecha_acta_tomap.Text))
                fehca_acta = "null";
            else
                fehca_acta = "'" + tb_fecha_acta_tomap.Text + "'" ;
            if (string.IsNullOrEmpty(tb_fecha_finiquito.Text))
                fecha_finiquito = "null";
            else
                fecha_finiquito = "'" + tb_fecha_finiquito.Text + "'" ;
            if (string.IsNullOrEmpty(tb_fecha_tedefe.Text))
                fecha_tedefe = "null";
            else
                fecha_tedefe = "'" + tb_fecha_tedefe.Text + "'" ; 
            cDirigente cdir = new cDirigente();
            cdir.ActualizarComite(new string[16] {  ddl_estado_comite.SelectedItem.Text, tb_periodo.Text, tb_fechainicio.Text, tb_fechafinal.Text, tb_acuerdo.Text, Fecha_acuerdo,  tb_acreditacion.Text, fehca_acreditacion,
                tb_acta_tomap.Text, fehca_acta, Session["Usuario"].ToString(), tb_finiquito.Text, fecha_finiquito,
                      tb_no_tedefe.Text, fecha_tedefe, lbtn_cambiar.Attributes["idDir"]});
        }

        protected void btn_anterior_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "irTab('#dirigente');", true);
        }

        protected void gv_suspendidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem li = ddl_vacantes.Items.FindByValue(gv_suspendidos.SelectedDataKey[1].ToString());
            if(li != null)
            {
                cDirigente cdir = new cDirigente();
                if(cdir.QuitarSuspension(int.Parse(gv_suspendidos.SelectedDataKey[0].ToString())))
                {
                    Response.Redirect("DirigentesFADN.aspx");
                }
            }
        }
    }
}