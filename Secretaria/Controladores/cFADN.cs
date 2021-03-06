﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using Modelos;

namespace Controladores
{
    public class cFADN
    {
        cConexion conectar;
        public DataTable ListadoFADN()
        {
            conectar = new cConexion();
            DataTable dt = new DataTable();
            conectar.AbrirConexion();
            string query = string.Format("select id_fand as numero, Nombre,Direccion,Telefono,correo_electronico as Correo from dbsecretaria.sg_fadn; ");
            MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
            consulta.Fill(dt);
            conectar.CerrarConexion();
            return dt;
        }

        public mFadn Obtener_Fadn(int id)
        {   
            mFadn objFand = new mFadn();
            conectar = new cConexion();
            string permiso = string.Format(" select id_fand,Nombre,Direccion,Telefono,correo_electronico as Correo from dbsecretaria.sg_fadn where id_fand = {0}; "
            , id);


            conectar.AbrirConexion();
            MySqlCommand cmd = new MySqlCommand(permiso, conectar.conectar);

            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                objFand.id_fand = dr.GetInt16("id_fand");
                objFand.Nombre = dr.GetString("Nombre");
                objFand.Direccion = dr.GetString("Direccion");
                objFand.Telefono = dr.GetString("Telefono");
                objFand.correo_electronico = dr.GetString("Correo");
                
                
            }
            return objFand;
        }

        public DataTable Obtener_Junta(int id)
        {
            conectar = new cConexion();
            DataTable dt = new DataTable();
            conectar.AbrirConexion();
            string query = string.Format("select td.descripcion Cargo, CONCAT(d.Nombres,' ', d.Apellidos) Nombre,d.Estado, c.Fecha_inicio as 'Fecha Inicio', c.Fecha_final as 'Fecha Final',d.dpi,d.Lugar_extendio AS 'Lugar Extendido', c.acuerdo_cej as 'Acuerdo', fecha_acuerdo AS 'Fecha', c.Acreditacion_cdag 'Acreditacion', c.Fecha_acreditacion as 'Fecha.' " +
                " from dbsecretaria.sg_comite_ejecutivo c inner join dbsecretaria.sg_dirigente d on c.id_dirigente = d.idDirigente inner join dbsecretaria.sg_tipo_dirigente td on td.idTipo_dirigente = d.Tipo_dirigente" +
                " where c.id_fadn = {0} and c.Estado = 'electo' and c.Estado_Comite = 1 order by cargo;",id);
            MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
            consulta.Fill(dt);
            conectar.CerrarConexion();
            return dt;
        }

        public DataTable Obtener_Comite_Interino(int id)
        {
            conectar = new cConexion();
            DataTable dt = new DataTable();
            conectar.AbrirConexion();
            string query = string.Format("select td.descripcion Cargo, CONCAT(d.Nombres,' ', d.Apellidos) Nombre, c.Fecha_inicio as 'Fecha Inicio', c.Fecha_final as 'Fecha Final',d.dpi,d.Lugar_extendio AS 'Lugar Extendido', c.acuerdo_cej as 'Acuerdo', fecha_acuerdo AS 'Fecha', c.Acreditacion_cdag 'Acreditacion', c.Fecha_acreditacion as 'Fecha.' " +
                " from dbsecretaria.sg_comite_ejecutivo c inner join dbsecretaria.sg_dirigente d on c.id_dirigente = d.idDirigente inner join dbsecretaria.sg_tipo_dirigente td on td.idTipo_dirigente = d.Tipo_dirigente" +
                " where c.id_fadn = {0} and c.Estado = 'interino' and c.Estado_Comite = 1 order by cargo;", id);
            MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
            consulta.Fill(dt);
            conectar.CerrarConexion();
            return dt;
        }

        public Int16 TotalFadn()
        {
            Int16 t = new Int16();
            conectar = new cConexion();
            conectar.AbrirConexion();
            string query = string.Format("SELECT COUNT(id_fand) FROM dbsecretaria.sg_fadn;");
            MySqlCommand consulta = new MySqlCommand(query, conectar.conectar);
            t = Convert.ToInt16(consulta.ExecuteScalar());
            conectar.CerrarConexion();
            return t;
        }

        public void DdlFadn(DropDownList drop)
        {
            conectar = new cConexion();
            drop.ClearSelection();
            drop.Items.Clear();
            drop.AppendDataBoundItems = true;
            drop.Items.Add("<< FADN >>");
            drop.Items[0].Value = "0";
            DataTable tabla = new DataTable();
            string query = String.Format("select id_fand, nombre from dbsecretaria.sg_fadn;");
            conectar.AbrirConexion();
            MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
            consulta.Fill(tabla);
            conectar.CerrarConexion();
            drop.DataSource = tabla;
            drop.DataTextField = "nombre";
            drop.DataValueField = "id_fand";
            drop.DataBind();
        }
    }
}
