using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controladores;
using System.Configuration;
using MySql.Data.MySqlClient;
using Microsoft.Reporting.WebForms;

namespace secretaria.ReportesSistema
{
    public partial class DirigentesActivos : System.Web.UI.Page
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
            if (tb_fecha_inicio.Text != "" && tb_fecha_fin.Text != "")
            {
                Todos[0] = new MySqlParameter("@fechaInicio", tb_fecha_inicio.Text);
                Todos[1] = new MySqlParameter("@fechaFin", tb_fecha_fin.Text);
                Todos[2] = new MySqlParameter("@fadn", ddl_fadn.SelectedValue);
                thisDataSet = MySqlHelper.ExecuteDataset(thisConnection, busqueda(2), Todos);
            }
            else
            {
                SearchValue[0] = new MySqlParameter("@fadn", ddl_fadn.SelectedValue);
                thisDataSet = MySqlHelper.ExecuteDataset(thisConnection, busqueda(2), SearchValue);
            }
      
      
            ReportDataSource datasource = new ReportDataSource("DataSet1", thisDataSet.Tables[0]);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            if (thisDataSet.Tables[0].Rows.Count == 0)
            {

            }

            ReportViewer1.LocalReport.Refresh();
        }

        public string busqueda(int op)
        {
            string query = "";
            if (ddl_fadn.SelectedIndex > 0)
            {
                if (tb_fecha_inicio.Text != "" && tb_fecha_fin.Text != "")
                {
                            query = string.Format("SELECT f.nombre, d.Nombres, d.Apellidos AS Nombres,d.estado, t.descripcion, c.Fecha_inicio," +
                 " c.Fecha_final,{0}, {1},{2},{3},{4},{5}" +
                 " FROM sg_comite_ejecutivo c INNER JOIN sg_dirigente d ON c.id_dirigente = d.idDirigente INNER JOIN " +
                 "sg_fadn f ON f.id_fand = c.id_fadn INNER JOIN sg_tipo_dirigente t ON t.idTipo_dirigente = d.Tipo_dirigente" +
                 " WHERE (c.Estado_Comite = 1) and c.id_fadn = @fadn and c.Fecha_inicio >= @fechaInicio and c.Fecha_final <= @fechaFin ORDER BY f.nombre, d.Tipo_dirigente" ,
                 filtros[0], filtros[1], filtros[2], filtros[3],filtros[4],filtros[5]);

                }
                else
                {
                    query = string.Format("SELECT f.nombre, d.Nombres, d.Apellidos AS Nombres,d.estado, t.descripcion, c.Fecha_inicio," +
              " c.Fecha_final,{0}, {1},{2},{3},{4},{5}" +
              " FROM sg_comite_ejecutivo c INNER JOIN sg_dirigente d ON c.id_dirigente = d.idDirigente INNER JOIN " +
              "sg_fadn f ON f.id_fand = c.id_fadn INNER JOIN sg_tipo_dirigente t ON t.idTipo_dirigente = d.Tipo_dirigente" +
              " WHERE (c.Estado_Comite = 1) and c.id_fadn = @fadn ORDER BY f.nombre, d.Tipo_dirigente", filtros[0], filtros[1], filtros[2], filtros[3],filtros[4],filtros[5]);

                }

            }
            else if (tb_fecha_inicio.Text != "" && tb_fecha_fin.Text != "")
            {
                query = string.Format("SELECT f.nombre, d.Nombres, d.Apellidos AS Nombres,d.estado, t.descripcion, c.Fecha_inicio," +
             " c.Fecha_final,{0}, {1},{2},{3},{4},{5}" +
             " FROM sg_comite_ejecutivo c INNER JOIN sg_dirigente d ON c.id_dirigente = d.idDirigente INNER JOIN " +
             "sg_fadn f ON f.id_fand = c.id_fadn INNER JOIN sg_tipo_dirigente t ON t.idTipo_dirigente = d.Tipo_dirigente" +
             " WHERE (c.Estado_Comite = 1) and c.Fecha_inicio >= @fechaInicio and c.Fecha_final <= @fechaFin  ORDER BY f.nombre, d.Tipo_dirigente", filtros[0], filtros[1], filtros[2], filtros[3],filtros[4],filtros[5]);

            }
            else if(ddl_dirigente.SelectedIndex >0 )
            {
                query = string.Format("SELECT f.nombre, d.Nombres, d.Apellidos AS Nombres,d.estado ,t.descripcion, c.Fecha_inicio," +
            " c.Fecha_final,{0}, {1},{2},{3},{4},{5}" +

            " FROM sg_comite_ejecutivo c INNER JOIN sg_dirigente d ON c.id_dirigente = d.idDirigente INNER JOIN " +
            "sg_fadn f ON f.id_fand = c.id_fadn INNER JOIN sg_tipo_dirigente t ON t.idTipo_dirigente = d.Tipo_dirigente" +
            " WHERE (c.Estado_Comite = 1) and t.descripcion = @dirigente ORDER BY f.nombre, d.Tipo_dirigente", filtros[0], filtros[1], filtros[2], filtros[3],filtros[4],filtros[5]);

            }
            else
            {
                query = string.Format("SELECT f.nombre, d.Nombres, d.Apellidos AS Nombres,d.estado, t.descripcion, c.Fecha_inicio," +
           " c.Fecha_final,{0}, {1},{2},{3},{4},{5}" +

           " FROM sg_comite_ejecutivo c INNER JOIN sg_dirigente d ON c.id_dirigente = d.idDirigente INNER JOIN " +
           "sg_fadn f ON f.id_fand = c.id_fadn INNER JOIN sg_tipo_dirigente t ON t.idTipo_dirigente = d.Tipo_dirigente" +
           " WHERE (c.Estado_Comite = 1) ORDER BY f.nombre, d.Tipo_dirigente", filtros[0], filtros[1], filtros[2], filtros[3],filtros[4],filtros[5]);

            }

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
            if (ddl_fadn.SelectedIndex>0)
            {
                Todos[0] = new MySqlParameter("@fechaInicio", tb_fecha_inicio.Text);
                Todos[1] = new MySqlParameter("@fechaFin", tb_fecha_fin.Text);
                Todos[2] = new MySqlParameter("@fadn", ddl_fadn.SelectedValue);
                thisDataSet = MySqlHelper.ExecuteDataset(thisConnection, busqueda(2), Todos);
            }
            else
            {
                Fechas[0] = new MySqlParameter("@fechaInicio", tb_fecha_inicio.Text);
                Fechas[1] = new MySqlParameter("@fechaFin", tb_fecha_fin.Text);
                thisDataSet = MySqlHelper.ExecuteDataset(thisConnection, busqueda(2), Fechas);
            }
           
          
            /* Put the stored procedure result into a dataset */
            

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

            /* Put the stored procedure result into a dataset */
            if (ddl_fadn.SelectedIndex>0)
            {
                if (tb_fecha_inicio.Text != "" && tb_fecha_fin.Text != "")
                {
                    Todos[0] = new MySqlParameter("@fechaInicio", tb_fecha_inicio.Text);
                    Todos[1] = new MySqlParameter("@fechaFin", tb_fecha_fin.Text);
                    Todos[2] = new MySqlParameter("@fadn", ddl_fadn.SelectedValue);
                    thisDataSet = MySqlHelper.ExecuteDataset(thisConnection, busqueda(2), Todos);
                }
                else
                {
                    SearchValue[0] = new MySqlParameter("@fadn", ddl_fadn.SelectedValue);
                    thisDataSet = MySqlHelper.ExecuteDataset(thisConnection, busqueda(2), SearchValue);
                }

            }
            else if (tb_fecha_inicio.Text != "" && tb_fecha_fin.Text != "")
            {
                Fechas[0] = new MySqlParameter("@fechaInicio", tb_fecha_inicio.Text);
                Fechas[1] = new MySqlParameter("@fechaFin", tb_fecha_fin.Text);
                thisDataSet = MySqlHelper.ExecuteDataset(thisConnection, busqueda(2), Fechas);
            }
            else
            {
                thisDataSet = MySqlHelper.ExecuteDataset(thisConnection, busqueda(2));
            }
           
            

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
            SearchValue[0] = new MySqlParameter("@dirigente", ddl_dirigente.SelectedValue);
            thisDataSet = MySqlHelper.ExecuteDataset(thisConnection, busqueda(2),SearchValue);
            thisDataSet = MySqlHelper.ExecuteDataset(thisConnection, busqueda(1),
              SearchValue);

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