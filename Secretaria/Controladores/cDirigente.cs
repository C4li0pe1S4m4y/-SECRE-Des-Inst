using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;
using Modelos;
using System.Web.Services;

namespace Controladores
{
    public class cDirigente
    {
        cConexion conectar;
        public void Fill_FadnDDL(DropDownList ddl)
        {
            DataTable dt = new DataTable();
            try
            {
                conectar = new cConexion();
                conectar.AbrirConexion();
                string query = string.Format("select id_fand as numero, nombre from dbsecretaria.sg_fadn; ");
                MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
                consulta.Fill(dt);
                conectar.CerrarConexion();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            };
            ddl.Items.Clear();
            ddl.AppendDataBoundItems = true;
            ddl.Items.Add("-- Seleccione uno --");
            ddl.Items[0].Value = "-1";
            ddl.DataSource = dt;
            ddl.DataTextField = "nombre";
            ddl.DataValueField = "numero";
            ddl.DataBind();
        }

        public void Fill_ddlDirigente(DropDownList ddl)
        {
            DataTable dt = new DataTable();
            try
            {
                conectar = new cConexion();
                conectar.AbrirConexion();
                string query = string.Format("select idTipo_dirigente as numero, descripcion as nombre from dbsecretaria.sg_tipo_dirigente; ");
                MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
                consulta.Fill(dt);
                conectar.CerrarConexion();
            }
            catch { return; };
            ddl.Items.Clear();
            ddl.AppendDataBoundItems = true;
            ddl.Items.Add("-- Seleccione uno --");
            ddl.Items[0].Value = "-1";
            ddl.DataSource = dt;
            ddl.DataTextField = "nombre";
            ddl.DataValueField = "nombre";
            ddl.DataBind();
        }

        public DataTable Obtener_Junta(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                conectar = new cConexion();
                conectar.AbrirConexion();
                string query = string.Format("select c.id_dirigente ID, td.descripcion Cargo, CONCAT(d.Nombres,' ', d.Apellidos) Nombre, DATE_FORMAT(c.Fecha_inicio, '%d-%m-%Y') as 'Fecha Inicio', DATE_FORMAT(c.Fecha_final,'%d-%m-%Y') as 'Fecha Final'," +
                    "d.dpi,d.Lugar_extendio AS 'Lugar Extendido', c.acuerdo_cej as 'Acuerdo', DATE_FORMAT(fecha_acuerdo, '%d-%m-%Y') AS 'Fecha', c.Acreditacion_cdag 'Acreditacion', DATE_FORMAT(c.Fecha_acreditacion, '%d-%m-%Y') as 'Fecha.' " +
                    " from dbsecretaria.sg_comite_ejecutivo c inner join dbsecretaria.sg_dirigente d on c.id_dirigente = d.idDirigente inner join dbsecretaria.sg_tipo_dirigente td on td.idTipo_dirigente = d.Tipo_dirigente" +
                    " where c.id_fadn = {0} and d.Estado = 'activo' and c.Estado_Comite = 1 order by cargo;", id);
                MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
                consulta.Fill(dt);
                conectar.CerrarConexion();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            };
            return dt;
        }

        public DataTable dtDirigentes()
        {
            DataTable dt = new DataTable();
            try
            {
                conectar = new cConexion();
                conectar.AbrirConexion();
                string query = string.Format("select d.idDirigente, CONCAT(d.Nombres, ' ', d.Apellidos, ' - ', ' (', f.nombre, ')') as dirigente " +
                    "from dbsecretaria.sg_fadn f inner join dbsecretaria.sg_comite_ejecutivo c on f.id_fand = c.id_fadn inner join dbsecretaria.sg_dirigente d on c.id_dirigente = d.idDirigente inner join dbsecretaria.sg_tipo_dirigente t on d.Tipo_dirigente = t.idTipo_dirigente " +
                    "where d.Estado = 'activo' and t.idTipo_dirigente in(1,4) " +
                    "order by dirigente;");
                MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
                consulta.Fill(dt);
                conectar.CerrarConexion();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            };
            return dt;
        }


        public void FillDeptos(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.AppendDataBoundItems = true;
            ddl.Items.Add("-- Seleccione uno --");
            ddl.Items[0].Value = "-1";
            string[] departamentos = new string[22] {"Alta Verapaz", "Baja Verapaz", "Chimaltenango", "Chiquimula", "El Progreso", "Escuintla", "Guatemala",
                "Huehuetenango", "Izabal", "Jalapa", "Jutiapa", "Petén", "Quetzaltenango", "Quiché", "Retalhuleu", "Sacatepéquez", "San Marcos", "Santa Rosa",
                "Sololá", "Suchitepéquez", "Totonicapán", "Zacapa"};
            for (int i = 0; i < departamentos.Length; i++)
            {
                ListItem li = new ListItem(departamentos[i], (i + 1).ToString());
                ddl.Items.Add(li);
            }
            ddl.DataBind();
        }

        public void FillEstadosDirigente(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.AppendDataBoundItems = true;
            ddl.Items.Add("-- Seleccione uno --");
            ddl.Items[0].Value = "-1";
            string[] estados = new string[5] { "suspendido", "renuncia", "fallecimiento", "fin del período", "otro" };
            for (int i = 0; i < estados.Length; i++)
            {
                ListItem li = new ListItem(estados[i], (i + 1).ToString());
                ddl.Items.Add(li);
            }
            ddl.DataBind();
        }

        public void FillEstadosComite(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.AppendDataBoundItems = true;
            ddl.Items.Add("-- Seleccione uno --");
            ddl.Items[0].Value = "-1";
            string[] estados = new string[4] { "electo", "interino", "transición", "otros" };
            for (int i = 0; i < estados.Length; i++)
            {
                ListItem li = new ListItem(estados[i], (i + 1).ToString());
                ddl.Items.Add(li);
            }
            ddl.DataBind();
        }

        public bool FillVacantes(DropDownList ddl, int idf)
        {
            DataTable dt = tipoDirigente(idf);
            string[] tipos = new string[5] { "Presidente", "Secretario", "Tesorero", "Vocal I", "Vocal II" };
            ddl.Items.Clear();
            for (int i = 0; i < tipos.Length; i++)
            {
                ListItem li = new ListItem(tipos[i], (i + 1).ToString());
                ddl.Items.Add(li);
            }
            int contador = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ddl.Items.Remove(ddl.Items.FindByValue(dr["Tipo_dirigente"].ToString()));
                contador++;
            }
            if (contador == 5)
            {
                ListItem li = new ListItem("No hay vacantes disponibles", "-1");
                ddl.Items.Add(li);
                return false;
            }
            ddl.DataBind();
            return true;
        }

        public DataTable tipoDirigente(int idfadn)
        {
            DataTable dt = new DataTable();
            try
            {
                conectar = new cConexion();
                conectar.AbrirConexion();
                string query = string.Format("SELECT Tipo_dirigente " +
                    "from dbsecretaria.sg_comite_ejecutivo c left join dbsecretaria.sg_dirigente d on c.id_dirigente = d.idDirigente " +
                    "where c.id_fadn = {0} and d.Estado = 'activo' and c.Estado_Comite = 1 order by Tipo_dirigente", idfadn);
                MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
                consulta.Fill(dt);
                conectar.CerrarConexion();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            };
            return dt;
        }

        public bool Dar_Baja(int idDir, string estado, string motivo)
        {
            conectar = new cConexion();
            conectar.AbrirConexion();
            MySqlTransaction transaccion = conectar.conectar.BeginTransaction();
            MySqlCommand command = conectar.conectar.CreateCommand();
            command.Transaction = transaccion;
            try
            {

                command.CommandText = string.Format("UPDATE dbsecretaria.sg_dirigente SET Estado = '{0}', Motivo = '{1}'  WHERE idDirigente = {2};", estado, motivo, idDir);
                command.ExecuteNonQuery();
                if (estado != "suspendido")
                {
                    command.CommandText = string.Format("UPDATE dbsecretaria.sg_comite_ejecutivo SET Estado_Comite = 0 WHERE id_dirigente = {0}", idDir);
                    command.ExecuteNonQuery();
                }
                transaccion.Commit();
            }
            catch
            {
                try
                {
                    transaccion.Rollback();
                }
                
                catch (Exception ex)
                {
                    string error = ex.Message;
                    throw;
                };
                conectar.CerrarConexion();
                return false;
            };
            return true;
        }

        public void Suspendidos(GridView gv, int idFadn)
        {
            DataTable dt = new DataTable();
            try
            {
                conectar = new cConexion();
                conectar.AbrirConexion();
                string query = string.Format("SELECT b.idDirigente as ID, b.Tipo_dirigente as COD, CONCAT(b.Nombres, ' ', b.Apellidos) as Dirigente, c.descripcion FROM dbsecretaria.sg_comite_ejecutivo a " +
                    "JOIN dbsecretaria.sg_dirigente b ON a.id_dirigente = b.idDirigente JOIN dbsecretaria.sg_tipo_dirigente c ON b.Tipo_dirigente = c.idTipo_dirigente " +
                    "WHERE a.id_fadn = {0} AND b.Estado = 'suspendido' AND a.Estado_Comite = 1; ", idFadn);
                MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
                consulta.Fill(dt);
                conectar.CerrarConexion();
            }
            catch { return; };
            if (dt.Rows.Count > 0)
            {
                gv.DataSource = dt;
                gv.DataBind();

            }
        }

        public bool FillDirigente(TextBox[] datos_dirigente, DropDownList ddl, int idDir)
        {
            DataTable dt = new DataTable();
            try
            {
                conectar = new cConexion();
                conectar.AbrirConexion();
                string query = string.Format("SELECT Nombres, Apellidos, DPI," +
                    "DATE_FORMAT(Fecha_posesion, '%Y-%m-%d') as FP, DATE_FORMAT(Fecha_finalizacion, '%Y-%m-%d') as FF, NIT, correo_electronico, Lugar_Extendio"
                    + " FROM dbsecretaria.sg_dirigente WHERE idDirigente = {0};", idDir);
                MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
                consulta.Fill(dt);
                conectar.CerrarConexion();
                if(dt.Rows.Count> 0)
                {
                    DataRow dr = dt.Rows[0];
                    datos_dirigente[0].Text = dr["Nombres"].ToString();
                    datos_dirigente[1].Text = dr["Apellidos"].ToString();
                    datos_dirigente[2].Text = dr["DPI"].ToString();
                    datos_dirigente[3].Text = dr["FP"].ToString();
                    datos_dirigente[4].Text = dr["FF"].ToString();
                    datos_dirigente[5].Text = dr["NIT"].ToString();
                    datos_dirigente[6].Text = dr["correo_electronico"].ToString();
                    ddl.SelectedValue = ddl.Items.FindByText(dr["Lugar_extendio"].ToString()).Value;
                }
            }
            catch
            { return false; };
            return true;
        }

        public bool FillComite(TextBox[] datos_comite, DropDownList ddl, int idDir)
        {
            DataTable dt = new DataTable();
            try
            {
                conectar = new cConexion();
                conectar.AbrirConexion();
                string query = string.Format("SELECT Periodo, DATE_FORMAT(Fecha_inicio, '%Y-%m-%d') as FI, DATE_FORMAT(Fecha_final, '%Y-%m-%d') as FF, Acuerdo_cej," +
                    "DATE_FORMAT(Fecha_acuerdo, '%Y-%m-%d') as FA, Acreditacion_cdag,  DATE_FORMAT(Fecha_Acreditacion, '%Y-%m-%d') as FAc, no_finiquito, "
                    + "DATE_FORMAT(fecha_finiquito, '%Y-%m-%d') as FFini, acta_posesion, DATE_FORMAT(fecha_posesion, '%Y-%m-%d') as FP, no_tedefe,  " +
                    "DATE_FORMAT(fecha_tedefe, '%Y-%m-%d') as fecha_tedefe, Estado FROM dbsecretaria.sg_comite_ejecutivo WHERE id_dirigente = {0};", idDir);
                MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
                consulta.Fill(dt);
                conectar.CerrarConexion();
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    datos_comite[0].Text = dr["Periodo"].ToString();
                    datos_comite[1].Text = dr["FI"].ToString();
                    datos_comite[2].Text = dr["FF"].ToString();
                    datos_comite[3].Text = dr["Acuerdo_cej"].ToString();
                    datos_comite[4].Text = dr["FA"].ToString();
                    datos_comite[5].Text = dr["Acreditacion_cdag"].ToString();
                    datos_comite[6].Text = dr["FAc"].ToString();
                    datos_comite[7].Text = dr["no_finiquito"].ToString();
                    datos_comite[8].Text = dr["FFini"].ToString();
                    datos_comite[9].Text = dr["acta_posesion"].ToString();
                    datos_comite[10].Text = dr["FP"].ToString();
                    datos_comite[11].Text = dr["no_tedefe"].ToString();
                    datos_comite[12].Text = dr["fecha_tedefe"].ToString();
                    ddl.SelectedValue = ddl.Items.FindByText(dr["Estado"].ToString()).Value;
                }
            }
            catch (Exception ex)
            { return false; };
            return true;
        }

        public bool ActualizarDirigente(string[] datos_dirigente)
        {
            conectar = new cConexion();
            conectar.AbrirConexion();
            MySqlTransaction transaccion = conectar.conectar.BeginTransaction();
            MySqlCommand command = conectar.conectar.CreateCommand();
            command.Transaction = transaccion;
            try
            {
                command.CommandText = string.Format("UPDATE dbsecretaria.sg_dirigente SET  Nombres = '{0}', Apellidos = '{1}', DPI = '{2}', Lugar_extendio = '{3}', NIT = '{4}',"+
                "Fecha_posesion = '{5}', Fecha_finalizacion = '{6}', correo_electronico = '{7}', usuario_modifica = '{8}', Fecha_modifica = now() WHERE idDirigente = {9}; ",
                datos_dirigente[0], datos_dirigente[1], datos_dirigente[2], datos_dirigente[3],datos_dirigente[4], datos_dirigente[5], datos_dirigente[6], datos_dirigente[7], 
                datos_dirigente[8], datos_dirigente[9]);
                command.ExecuteNonQuery();
                transaccion.Commit();
                conectar.CerrarConexion();
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    transaccion.Rollback();
                }
                
                catch (Exception ex2)
                {
                    string error = ex2.Message;
                    throw;
                };
                conectar.CerrarConexion();
                return false;
            };
        }

        public bool ActualizarComite(string[] datos_comite)
        {
            conectar = new cConexion();
            conectar.AbrirConexion();
            MySqlTransaction transaccion = conectar.conectar.BeginTransaction();
            MySqlCommand command = conectar.conectar.CreateCommand();
            command.Transaction = transaccion;
            try
            {
                command.CommandText = string.Format("UPDATE `dbsecretaria`.`sg_comite_ejecutivo` SET Estado = '{0}', Periodo = {1}, Fecha_inicio = '{2}',Fecha_final = '{3}',Acuerdo_cej = '{4}'," +
                "Fecha_acuerdo = '{5}',Acreditacion_cdag = '{6}',Fecha_Acreditacion = '{7}',acta_posesion = '{8}', fecha_posesion='{9}',Fecha_modifica = now() ,usuario_modifica = '{10}', " +
                "no_finiquito = '{11}', fecha_finiquito = '{12}', no_tedefe = '{13}', fecha_tedefe = '{14}' WHERE id_dirigente = {15};"
                , datos_comite[0], datos_comite[1], datos_comite[2], datos_comite[3], datos_comite[4], datos_comite[5], datos_comite[6], datos_comite[7], datos_comite[8], 
                datos_comite[9], datos_comite[10], datos_comite[11], datos_comite[12], datos_comite[13], datos_comite[14], datos_comite[15]);
                command.ExecuteNonQuery();
                transaccion.Commit();
                conectar.CerrarConexion();
                return true;
            }
            catch(Exception ex)
            {
                try
                {
                    transaccion.Rollback();
                }
                
                catch (Exception ex2)
                {
                    string error = ex2.Message;
                    throw;
                };
                conectar.CerrarConexion();
                return false;
            };
        }

        public bool IngresarDirigente(string[] datos_dirigente, string[] datos_comite)
        {
            conectar = new cConexion();
            conectar.AbrirConexion();
            MySqlTransaction transaccion = conectar.conectar.BeginTransaction();
            MySqlCommand command = conectar.conectar.CreateCommand();
            command.Transaction = transaccion;
            try
            {
                string temp = string.Format("INSERT INTO dbsecretaria.sg_dirigente(Nombres,Apellidos,Tipo_dirigente,DPI,Lugar_extendio,NIT," +
                "correo_electronico,usuario_creo,Fecha_crea, Estado) VALUES('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}',now(),'activo');", datos_dirigente[0], datos_dirigente[1], datos_dirigente[2], datos_dirigente[3],
                datos_dirigente[4], datos_dirigente[5], datos_dirigente[6], datos_dirigente[7]);
                command.CommandText = temp;
                command.ExecuteNonQuery();
                temp = string.Format("INSERT INTO dbsecretaria.sg_comite_ejecutivo(id_dirigente, id_fadn, Periodo,Fecha_inicio,Fecha_final,Acuerdo_cej,Fecha_acuerdo,Acreditacion_cdag," +
                "Fecha_Acreditacion,Estado,no_finiquito, fecha_finiquito, acta_posesion, fecha_posesion, Fecha_creacion,usuario_crea, Estado_Comite, no_tedefe, fecha_tedefe)"+
                " VALUES((SELECT MAX(idDirigente)FROM dbsecretaria.sg_dirigente),{0},{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',now(),'{13}',1,'{14}','{15}') ", datos_comite[0], datos_comite[1], datos_comite[2]
                , datos_comite[3], datos_comite[4], datos_comite[5], datos_comite[6], datos_comite[7], datos_comite[8], datos_comite[9], datos_comite[10], datos_comite[11], 
                datos_comite[12], datos_comite[13], datos_comite[14], datos_comite[15]);
                command.CommandText = temp;
                command.ExecuteNonQuery();
                transaccion.Commit();
                conectar.CerrarConexion();
                return true;
            }
            catch(Exception ex)
            {
                
                try
                {
                    transaccion.Rollback();
                }
                
                catch (Exception ex2)
                {
                    string error = ex2.Message;
                    throw;
                };
                conectar.CerrarConexion();
                return false;
            };
        }

        public bool QuitarSuspension(int idDir)
        {
            conectar = new cConexion();
            conectar.AbrirConexion();
            MySqlTransaction transaccion = conectar.conectar.BeginTransaction();
            MySqlCommand command = conectar.conectar.CreateCommand();
            command.Transaction = transaccion;
            try
            {
                command.CommandText = string.Format("UPDATE dbsecretaria.sg_dirigente SET Estado = 'activo' WHERE idDirigente = {0};", idDir);
                command.ExecuteNonQuery();
                transaccion.Commit();
                conectar.CerrarConexion();
                return true;
            }
            catch
            {
                try
                {
                    transaccion.Rollback();
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    throw;
                };
                conectar.CerrarConexion();
                return false;
            };
        }
    }
}
