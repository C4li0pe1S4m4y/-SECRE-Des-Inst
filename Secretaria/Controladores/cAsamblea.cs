using Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Controladores
{
    public class cAsamblea
    {
        cConexion conectar;
        public bool login(string usuario, string pass)
        {
            conectar = new cConexion();

            conectar.AbrirConexion();
            string query = string.Format("select a.usuario from (select usuario, CAST(AES_DECRYPT(Contrasena, 'SCOGA') AS CHAR(50)) as Contrasena from dbsecretaria.sg_usuario where habilitado = 1) as a where a.usuario = '{0}' and a.Contrasena = '{1}'; ",
                usuario, pass);
            MySqlCommand cmd = new MySqlCommand(query, conectar.conectar);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (!string.IsNullOrEmpty(dr.GetString("usuario")))
                {
                    return true;
                }
            }
            return false;
        }

        public DataTable InsertarAsamblea(mAsamblea objAsamblea)
        {
            conectar = new cConexion();
            DataTable dt = new DataTable();
            conectar.AbrirConexion();
            string query = string.Format("insert into dbsecretaria.sg_asamblea(id_tipo_asamblea,descripcion,fecha,estado,inicio,final) " +
                    "values({0},'{1}',STR_TO_DATE('{2}', '%d/%m/%Y'),1,NULL,NULL);", objAsamblea.tipo_asamblea, objAsamblea.descripcion, objAsamblea.fecha);
            MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
            consulta.Fill(dt);
            conectar.CerrarConexion();
            return dt;
        }

        public void DdlTipoAsamblea(DropDownList drop)
        {
            conectar = new cConexion();
            drop.ClearSelection();
            drop.Items.Clear();
            drop.AppendDataBoundItems = true;
            drop.Items.Add("<< Elija un valor >>");
            drop.Items[0].Value = "0";
            DataTable tabla = new DataTable();
            string query = String.Format("select id_tipo_asamblea, nombre from dbsecretaria.sg_tipo_asamblea; ");
            conectar.AbrirConexion();
            MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
            consulta.Fill(tabla);
            conectar.CerrarConexion();
            drop.DataSource = tabla;
            drop.DataTextField = "nombre";
            drop.DataValueField = "id_tipo_asamblea";
            drop.DataBind();
        }

        public DataTable ListadoAsambleas()
        {
            conectar = new cConexion();
            DataTable dt = new DataTable();
            conectar.AbrirConexion();
            string query = string.Format("select a.id_asamblea numero,a.descripcion, DATE_FORMAT(a.fecha, '%d/%m/%Y') as 'fecha',t.nombre as 'tipoAsamblea', e.nombre as 'estado' from dbsecretaria.sg_asamblea a left join dbsecretaria.sg_tipo_asamblea t on a.id_tipo_asamblea = t.id_tipo_asamblea left join dbsecretaria.sg_estado_asamblea e on a.estado = e.id_estado order by e.id_estado, a.fecha asc;");
            MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
            consulta.Fill(dt);
            conectar.CerrarConexion();
            return dt;
        }

        public mAsamblea Obtner_Asamblea(int id)
        {
            mAsamblea objAsamblea = new mAsamblea();
            conectar = new cConexion();
            string permiso = string.Format(" select id_asamblea, estado, fecha, nombre, descripcion, inicio, final, id_tipo_asamblea from dbsecretaria.sg_asamblea where id_asamblea ={0}; "
            , id);
            conectar.AbrirConexion();
            MySqlCommand cmd = new MySqlCommand(permiso, conectar.conectar);

            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                objAsamblea.id_asamblea = dr.GetInt16("id_asamblea");
                objAsamblea.estado = dr.GetInt16("estado");
                objAsamblea.fecha = dr.GetString("fecha");
                objAsamblea.descripcion = dr.GetString("descripcion");
                if (!dr.IsDBNull(dr.GetOrdinal("inicio")))
                    objAsamblea.inicio = dr.GetString(dr.GetOrdinal("inicio"));
                if (!dr.IsDBNull(dr.GetOrdinal("final")))
                    objAsamblea.final = dr.GetString(dr.GetOrdinal("final"));
                objAsamblea.tipo_asamblea = dr.GetInt16("id_tipo_asamblea");
            }
            conectar.CerrarConexion();
            return objAsamblea;
        }

        public void ActualizarEstado(int id, int estado)
        {
            conectar = new cConexion();
            DataTable dte = new DataTable();
            DataTable dtf = new DataTable();
            conectar.AbrirConexion();

            string query1, query2 = "";

            query1 = string.Format("UPDATE dbsecretaria.sg_asamblea set " +
                    "estado = {1} WHERE id_asamblea={0}", id, estado);

            if (estado == 2)
            {
                query2 = string.Format("UPDATE dbsecretaria.sg_asamblea set " +
                    "inicio = STR_TO_DATE(now(), '%Y-%m-%d %H:%i:%s') WHERE id_asamblea={0}", id);
            }

            if (estado==3 || estado==4)
            {
                query2 = string.Format("UPDATE dbsecretaria.sg_asamblea set " +
                    "final = STR_TO_DATE(now(), '%Y-%m-%d %H:%i:%s') WHERE id_asamblea={0}", id);
            }
                
            try
            {
                MySqlDataAdapter consulta1 = new MySqlDataAdapter(query1, conectar.conectar);
                consulta1.Fill(dte);


                if (estado != 1)
                {
                    MySqlDataAdapter consulta2 = new MySqlDataAdapter(query2, conectar.conectar);
                    consulta2.Fill(dtf);
                }
                    
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            }
            conectar.CerrarConexion();
        }

    }
}
