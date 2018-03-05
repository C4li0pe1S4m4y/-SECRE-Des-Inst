using Controladores;
using Microsoft.Reporting.WebForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace secretaria.ReportesSistema
{
    public partial class DirigentesCompletos : System.Web.UI.Page
    {
        public string thisConnectionString = ConfigurationManager.ConnectionStrings["dbsecretariaConnectionString"].ConnectionString;
        public MySqlParameter[] SearchValue = new MySqlParameter[1];
        public MySqlParameter[] Fechas = new MySqlParameter[2];
        public MySqlParameter[] Todos = new MySqlParameter[3];
        string[] filtros = new string[6];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cDirigente cd = new cDirigente();
                cd.Fill_FadnDDL(ddl_fadn);
                cd.Fill_ddlDirigente(ddl_dirigente);
                filtros[0] = "c.Acuerdo_cej, c.Fecha_acuerdo";
                filtros[1] = "c.Acreditacion_cdag, c.Fecha_Acreditacion";
                filtros[2] = "c.no_finiquito, c.fecha_finiquito";
                filtros[3] = "c.acta_posesion, c.fecha_posesion";
                filtros[4] = "d.dpi";
                filtros[5] = "d.nit";
                MySqlConnection thisConnection = new MySqlConnection(thisConnectionString);
                System.Data.DataSet thisDataSet = new System.Data.DataSet();
                System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                stringBuilder.Append(busqueda2());
                thisDataSet = MySqlHelper.ExecuteDataset(thisConnection, stringBuilder.ToString());
                ReportDataSource datasource = new ReportDataSource("DataSet1", thisDataSet.Tables[0]);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                if (thisDataSet.Tables[0].Rows.Count == 0)
                {

                }

                ReportViewer1.LocalReport.Refresh();
            }
        }

        protected void ddl_fadn_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtros[0] = "c.Acuerdo_cej, c.Fecha_acuerdo";
            filtros[1] = "c.Acreditacion_cdag, c.Fecha_Acreditacion";
            filtros[2] = "c.no_finiquito, c.fecha_finiquito";
            filtros[3] = "c.acta_posesion, c.fecha_posesion";
            filtros[4] = "d.dpi";
            filtros[5] = "d.nit";
            MySqlConnection thisConnection = new MySqlConnection(thisConnectionString);
            System.Data.DataSet thisDataSet = new System.Data.DataSet();
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            stringBuilder.Append(busqueda2());
            if (tb_fecha_inicio.Text != "" && tb_fecha_fin.Text != "")
            {

                stringBuilder.Append(" and c.Fecha_inicio >= '" + tb_fecha_inicio.Text + "' ");
                stringBuilder.Append(" and c.Fecha_final <= '" + tb_fecha_fin.Text + "' ");



            }
            if (ddl_dirigente.SelectedIndex > 0)
            {
                stringBuilder.Append(" and t.descripcion =  '" + ddl_dirigente.SelectedValue + "' ");
            }
            stringBuilder.Append(" and c.id_fadn = " + ddl_fadn.SelectedValue);
            stringBuilder.Append(" ORDER BY f.nombre, d.Tipo_dirigente ");
            thisDataSet = MySqlHelper.ExecuteDataset(thisConnection, stringBuilder.ToString());
            ReportDataSource datasource = new ReportDataSource("DataSet1", thisDataSet.Tables[0]);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            if (thisDataSet.Tables[0].Rows.Count == 0)
            {

            }

            ReportViewer1.LocalReport.Refresh();
        }

        public string busqueda2()
        {
            string query = string.Format("SELECT f.nombre, d.Nombres, d.Apellidos AS Nombres,d.estado, t.descripcion, c.Fecha_inicio," +
          " c.Fecha_final,{0}, {1},{2},{3},{4},{5}" +
          " FROM sg_comite_ejecutivo c INNER JOIN sg_dirigente d ON c.id_dirigente = d.idDirigente INNER JOIN " +
          "sg_fadn f ON f.id_fand = c.id_fadn INNER JOIN sg_tipo_dirigente t ON t.idTipo_dirigente = d.Tipo_dirigente" +
          " WHERE (c.Estado_Comite >=0) ",
          filtros[0], filtros[1], filtros[2], filtros[3], filtros[4], filtros[5]);
            return query;
        }



        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            filtros[0] = "c.Acuerdo_cej, c.Fecha_acuerdo";
            filtros[1] = "c.Acreditacion_cdag, c.Fecha_Acreditacion";
            filtros[2] = "c.no_finiquito, c.fecha_finiquito";
            filtros[3] = "c.acta_posesion, c.fecha_posesion";
            filtros[4] = "d.dpi";
            filtros[5] = "d.nit";
            MySqlConnection thisConnection = new MySqlConnection(thisConnectionString);
            System.Data.DataSet thisDataSet = new System.Data.DataSet();
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            stringBuilder.Append(busqueda2());
            if (ddl_fadn.SelectedIndex > 0)
            {
                stringBuilder.Append(" and c.Fecha_inicio >= '" + tb_fecha_inicio.Text + "' ");
                stringBuilder.Append(" and c.Fecha_final <= '" + tb_fecha_fin.Text + "' ");
                stringBuilder.Append(" and c.id_fadn = " + ddl_fadn.SelectedValue);

            }
            else
            {
                stringBuilder.Append(" and c.Fecha_inicio >= '" + tb_fecha_inicio.Text + "' ");
                stringBuilder.Append(" and c.Fecha_final <= '" + tb_fecha_fin.Text + "' ");

            }


            /* Put the stored procedure result into a dataset */

            stringBuilder.Append(" ORDER BY c.id_fadn, d.Tipo_dirigente ");

            thisDataSet = MySqlHelper.ExecuteDataset(thisConnection, stringBuilder.ToString());
            ReportDataSource datasource = new ReportDataSource("DataSet1", thisDataSet.Tables[0]);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);

            if (thisDataSet.Tables[0].Rows.Count == 0)
            {

            }

            ReportViewer1.LocalReport.Refresh();
        }

        protected void cblFiltros_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cblFiltros.Items[0].Selected == true)
                filtros[0] = "c.Acuerdo_cej, c.Fecha_acuerdo";
            else
                filtros[0] = "'null' Acuerdo_cej, 'null' Fecha_acuerdo";
            if (cblFiltros.Items[1].Selected == true)
                filtros[1] = "c.Acreditacion_cdag, c.Fecha_Acreditacion";
            else
                filtros[1] = "'null' Acreditacion_cdag, 'null' Fecha_Acreditacion";
            if (cblFiltros.Items[2].Selected == true)
                filtros[2] = "c.no_finiquito, c.fecha_finiquito";
            else
                filtros[2] = "'null' no_finiquito, 'null' fecha_finiquito";
            if (cblFiltros.Items[3].Selected == true)
                filtros[3] = "c.acta_posesion, c.fecha_posesion";
            else
                filtros[3] = "'null' acta_posesion, 'null' fecha_posesion";
            if (cblFiltros.Items[4].Selected == true)
                filtros[4] = "d.dpi";
            else
                filtros[4] = "'null' dpi";
            if (cblFiltros.Items[5].Selected == true)
                filtros[5] = "d.nit";
            else
                filtros[5] = "0 nit";

            MySqlConnection thisConnection = new MySqlConnection(thisConnectionString);
            System.Data.DataSet thisDataSet = new System.Data.DataSet();
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            stringBuilder.Append(busqueda2());
            /* Put the stored procedure result into a dataset */
            if (ddl_fadn.SelectedIndex > 0)
            {
                if (tb_fecha_inicio.Text != "" && tb_fecha_fin.Text != "")
                {
                    stringBuilder.Append(" and c.Fecha_inicio >= '" + tb_fecha_inicio.Text + "' ");
                    stringBuilder.Append(" and c.Fecha_final <= '" + tb_fecha_fin.Text + "' ");
                    stringBuilder.Append(" and c.id_fadn = " + ddl_fadn.SelectedValue);

                }
                else
                {
                    stringBuilder.Append(" and c.id_fadn = " + ddl_fadn.SelectedValue);

                }

            }
            else if (tb_fecha_inicio.Text != "" && tb_fecha_fin.Text != "")
            {
                stringBuilder.Append(" and c.Fecha_inicio >= '" + tb_fecha_inicio.Text + "' ");
                stringBuilder.Append(" and c.Fecha_final <= '" + tb_fecha_fin.Text + "' ");
            }



            stringBuilder.Append(" ORDER BY c.id_fadn, d.Tipo_dirigente ");

            thisDataSet = MySqlHelper.ExecuteDataset(thisConnection, stringBuilder.ToString());
            ReportDataSource datasource = new ReportDataSource("DataSet1", thisDataSet.Tables[0]);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);

            if (thisDataSet.Tables[0].Rows.Count == 0)
            {

            }

            ReportViewer1.LocalReport.Refresh();

        }

        protected void ddl_dirigente_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtros[0] = "c.Acuerdo_cej, c.Fecha_acuerdo";
            filtros[1] = "c.Acreditacion_cdag, c.Fecha_Acreditacion";
            filtros[2] = "c.no_finiquito, c.fecha_finiquito";
            filtros[3] = "c.acta_posesion, c.fecha_posesion";
            filtros[4] = "d.dpi";
            filtros[5] = "d.nit";
            MySqlConnection thisConnection = new MySqlConnection(thisConnectionString);
            System.Data.DataSet thisDataSet = new System.Data.DataSet();
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            stringBuilder.Append(busqueda2());
            if (ddl_fadn.SelectedIndex > 0)
            {
                stringBuilder.Append(" and c.id_fadn = " + ddl_fadn.SelectedValue);
            }
            stringBuilder.Append(" and t.descripcion =  '" + ddl_dirigente.SelectedValue + "' ");
            stringBuilder.Append(" ORDER BY c.id_fadn, d.Tipo_dirigente ");

            thisDataSet = MySqlHelper.ExecuteDataset(thisConnection, stringBuilder.ToString());

            ReportDataSource datasource = new ReportDataSource("DataSet1", thisDataSet.Tables[0]);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            if (thisDataSet.Tables[0].Rows.Count == 0)
            {

            }

            ReportViewer1.LocalReport.Refresh();
        }
    }
}
