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
    public class cAsistencia
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

        public void DdlTipoAsistencia(DropDownList drop, int id_asamblea, int idDirigente)
        {
            conectar = new cConexion();
            drop.ClearSelection();
            drop.Items.Clear();
            drop.AppendDataBoundItems = true;
            drop.Items.Add("<< Tipo de Asistencia >>");
            drop.Items[0].Value = "0";
            DataTable tabla = new DataTable();
            string query = String.Format("select id_tipo_asistencia, nombre "+
                "from dbsecretaria.sg_tipo_asistencia "+
                "where id_tipo_asistencia not in(select a.id_tipo_asistencia "+
                "from dbsecretaria.sg_fadn f "+
                "inner join dbsecretaria.sg_comite_ejecutivo c on f.id_fand = c.id_fadn "+
                "inner join dbsecretaria.sg_dirigente d on d.idDirigente = c.id_dirigente "+
                "inner join dbsecretaria.sg_asistencia a on a.idDirigente = d.idDirigente "+
                "where f.id_fand = (select f.id_fand from dbsecretaria.sg_fadn f "+
                      "inner join dbsecretaria.sg_comite_ejecutivo c on f.id_fand = c.id_fadn "+
                      "inner join dbsecretaria.sg_dirigente d on d.idDirigente = c.id_dirigente "+
                      "where d.idDirigente = {0}) "+
                "and a.id_asamblea = {1});",idDirigente, id_asamblea);
            conectar.AbrirConexion();
            MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
            consulta.Fill(tabla);
            conectar.CerrarConexion();
            drop.DataSource = tabla;
            drop.DataTextField = "nombre";
            drop.DataValueField = "id_tipo_asistencia";
            drop.DataBind();
        }

        public DataTable ListadoAsistencia(int id, string orden)
        {
            conectar = new cConexion();
            DataTable dt = new DataTable();
            conectar.AbrirConexion();
            string query = "";
            if(orden == "ASC")
            {
                query = string.Format("SELECT a.id_asistencia numero, a.Estado, DATE_FORMAT(a.hora_entrada, '%H:%i') as 'entrada',DATE_FORMAT(a.hora_salida, '%H:%i') as 'salida',CONCAT(d.Nombres,' ',d.Apellidos) as 'nombreC',td.descripcion,ta.nombre as 'tipoA',f.nombre as 'federacion' " +
                "FROM dbsecretaria.sg_fadn f INNER JOIN dbsecretaria.sg_comite_ejecutivo c ON f.id_fand = c.id_fadn " +
                "RIGHT JOIN dbsecretaria.sg_dirigente d ON c.id_dirigente = d.idDirigente " +
                "LEFT JOIN dbsecretaria.sg_tipo_dirigente td ON d.Tipo_dirigente = td.idTipo_dirigente " +
                "RIGHT JOIN dbsecretaria.sg_asistencia a ON d.idDirigente = a.idDirigente " +
                "LEFT JOIN dbsecretaria.sg_tipo_asistencia ta ON a.id_tipo_asistencia = ta.id_tipo_asistencia " +
                "INNER JOIN dbsecretaria.sg_asamblea asa ON a.id_asamblea = asa.id_asamblea " +
                "WHERE asa.id_asamblea = {0} ORDER BY a.hora_entrada ASC;"
                , id);
            }
            else
            {
                query = string.Format("SELECT a.id_asistencia numero, a.Estado, DATE_FORMAT(a.hora_entrada, '%H:%i') as 'entrada',DATE_FORMAT(a.hora_salida, '%H:%i') as 'salida',CONCAT(d.Nombres,' ',d.Apellidos) as 'nombreC',td.descripcion,ta.nombre as 'tipoA',f.nombre as 'federacion' " +
                "FROM dbsecretaria.sg_fadn f INNER JOIN dbsecretaria.sg_comite_ejecutivo c ON f.id_fand = c.id_fadn " +
                "RIGHT JOIN dbsecretaria.sg_dirigente d ON c.id_dirigente = d.idDirigente " +
                "LEFT JOIN dbsecretaria.sg_tipo_dirigente td ON d.Tipo_dirigente = td.idTipo_dirigente " +
                "RIGHT JOIN dbsecretaria.sg_asistencia a ON d.idDirigente = a.idDirigente " +
                "LEFT JOIN dbsecretaria.sg_tipo_asistencia ta ON a.id_tipo_asistencia = ta.id_tipo_asistencia " +
                "INNER JOIN dbsecretaria.sg_asamblea asa ON a.id_asamblea = asa.id_asamblea " +
                "WHERE asa.id_asamblea = {0} ORDER BY a.hora_entrada DESC;"
                , id);
            }
            
            MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
            consulta.Fill(dt);
            conectar.CerrarConexion();
            return dt;
        }

        public Int16 TotalAsistentes(int id)
        {
            Int16 t = new Int16();
            conectar = new cConexion();
            conectar.AbrirConexion();
            string query = string.Format("SELECT COUNT(id_asistencia) FROM dbsecretaria.sg_asistencia WHERE id_asamblea = {0} AND id_tipo_asistencia = 1;", id);
            MySqlCommand consulta = new MySqlCommand(query, conectar.conectar);
            t = Convert.ToInt16(consulta.ExecuteScalar());
            conectar.CerrarConexion();
            return t;
        }

        public Int16 TotalRetirados(int id)
        {
            Int16 t = new Int16();
            conectar = new cConexion();
            conectar.AbrirConexion();
            string query = string.Format("SELECT COUNT(id_asistencia) FROM dbsecretaria.sg_asistencia WHERE id_asamblea = {0} and Estado = 'Retirado';", id);
            MySqlCommand consulta = new MySqlCommand(query, conectar.conectar);
            t = Convert.ToInt16(consulta.ExecuteScalar());
            conectar.CerrarConexion();
            return t;
        }

        public Int16 TotalFederados(int id)
        {
            Int16 t = new Int16();
            conectar = new cConexion();
            conectar.AbrirConexion();
            string query = string.Format("SELECT COUNT(id_asistencia) FROM dbsecretaria.sg_asistencia WHERE id_asamblea = {0} AND id_tipo_asistencia = 1 AND Estado='Presente';", id);
            MySqlCommand consulta = new MySqlCommand(query, conectar.conectar);
            t = Convert.ToInt16(consulta.ExecuteScalar());
            conectar.CerrarConexion();
            return t;
        }

        public DataTable InsertarAsistencia(mAsistencia objAsistencia)
        {
            Int16 t = new Int16();
            conectar = new cConexion();
            DataTable dt = new DataTable();
            conectar.AbrirConexion();
            string buscar = string.Format("SELECT COUNT(idDirigente) FROM dbsecretaria.sg_asistencia WHERE idDirigente={0} and id_asamblea={1}; ", objAsistencia.idDirigente, objAsistencia.id_asamblea);
            MySqlCommand consultaB = new MySqlCommand(buscar, conectar.conectar);
            t = Convert.ToInt16(consultaB.ExecuteScalar());

            string query = string.Format("insert into dbsecretaria.sg_asistencia(Estado,id_asamblea,idDirigente,hora_entrada,id_tipo_asistencia) " +
                "values('{0}',{1},{2},STR_TO_DATE(now(), '%Y-%m-%d %H:%i:%s'),{3});", objAsistencia.Estado, objAsistencia.id_asamblea, objAsistencia.idDirigente, objAsistencia.id_tipo_asistencia);
            MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
            if(t==0)consulta.Fill(dt);
            
            conectar.CerrarConexion();
            return dt;
        }

        public void ActualizarAsistencia(int id, string estado)
        {
            conectar = new cConexion();
            DataTable dt = new DataTable();
            conectar.AbrirConexion();

            string query = "";

            if(estado=="Retirado")
            {
                query = string.Format("UPDATE dbsecretaria.sg_asistencia set " +
                    "Estado = '{1}', hora_salida = STR_TO_DATE(now(), '%Y-%m-%d %H:%i:%s') WHERE id_asistencia={0}", id, estado);
            }
            else
            {
                query = string.Format("UPDATE dbsecretaria.sg_asistencia set " +
                    "Estado = '{1}', hora_salida = NULL WHERE id_asistencia={0}", id, estado);
            }
            
            try
            {
                MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
                consulta.Fill(dt);            
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            }
            conectar.CerrarConexion();
        }

        public void TerminarAsamblea(int id)
        {
            conectar = new cConexion();
            DataTable dt = new DataTable();
            conectar.AbrirConexion();

                string query = string.Format("UPDATE dbsecretaria.sg_asistencia set " +
                    "hora_salida = STR_TO_DATE(now(), '%Y-%m-%d %H:%i:%s') WHERE id_asamblea={0} and hora_salida IS NULL", id);
  
            try
            {
                MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
                consulta.Fill(dt);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            }
            conectar.CerrarConexion();
        }

        public void EliminarAsistencia(int id)
        {
            conectar = new cConexion();
            DataTable dt = new DataTable();
            conectar.AbrirConexion();
            string query = string.Format("DELETE FROM dbsecretaria.sg_asistencia "+
                    "WHERE id_asistencia={0};", id);
            try
            {
                MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
                consulta.Fill(dt);
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
