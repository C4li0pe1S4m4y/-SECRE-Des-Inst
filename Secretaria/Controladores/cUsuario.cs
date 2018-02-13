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
    public class cUsuario
    {
        cConexion conectar;
        public bool login(string usuario, string pass)
        {
            conectar = new cConexion();

            conectar.AbrirConexion();
            string query = string.Format("select a.usuario from (select usuario, CAST(AES_DECRYPT(contrasena, 'SCOGA') AS CHAR(50)) as contrasena from dbsecretaria.sg_usuario where habilitado = 1) as a where a.usuario = '{0}' and a.contrasena = '{1}'; ",
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


        public DataTable InsertUsuario(mUsuario objUsuario)
        {
            conectar = new cConexion();
            DataTable dt = new DataTable();
            conectar.AbrirConexion();
            string query = string.Format("insert into dbsecretaria.sg_usuario(usuario,Contrasena,tipo_usuario,habilitado,nombre) " +
                    "values('{0}',AES_ENCRYPT('{1}', 'SCOGA'),{2},1,'{3}');", objUsuario.Usuario, objUsuario.contraseña, objUsuario.tipo_usuario, objUsuario.Nombre);
            MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
            consulta.Fill(dt);
            conectar.CerrarConexion();
            return dt;
        }

        public void DdlTipoUsuario(DropDownList drop)
        {
            conectar = new cConexion();
            drop.ClearSelection();
            drop.Items.Clear();
            drop.AppendDataBoundItems = true;
            drop.Items.Add("<< Elija un valor >>");
            drop.Items[0].Value = "0";
            DataTable tabla = new DataTable();
            string query = String.Format("select id_tipo, Descripcion from dbsecretaria.sg_tipo_usuariio; ");
            conectar.AbrirConexion();
            MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
            consulta.Fill(tabla);
            conectar.CerrarConexion();
            drop.DataSource = tabla;
            drop.DataTextField = "Descripcion";
            drop.DataValueField = "id_tipo";
            drop.DataBind();
        }

        public DataTable ListadoUsuarios()
        {
            conectar = new cConexion();
            DataTable dt = new DataTable();
            conectar.AbrirConexion();
            string query = string.Format("select u.id_usuario numero,u.usuario,u.nombre,t.descripcion from dbsecretaria.sg_usuario u inner join dbsecretaria.sg_tipo_usuariio t on u.tipo_usuario = t.id_tipo;");
            MySqlDataAdapter consulta = new MySqlDataAdapter(query, conectar.conectar);
            consulta.Fill(dt);
            conectar.CerrarConexion();
            return dt;
        }


        public mUsuario Obtner_Usuario(int id)
        {
            mUsuario objUsuario = new mUsuario();
            conectar = new cConexion();
            string permiso = string.Format(" select usuario, nombre from dbsecretaria.sg_usuario where id_usuario ={0}; "
            , id);
            conectar.AbrirConexion();
            MySqlCommand cmd = new MySqlCommand(permiso, conectar.conectar);

            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                objUsuario.Usuario = dr.GetString("usuario");
                objUsuario.Nombre = dr.GetString("nombre");
            }
            return objUsuario;
        }

        public void ActualizarUsuario(mUsuario objusuario)
        {
            conectar = new cConexion();
            DataTable dt = new DataTable();
            conectar.AbrirConexion();
            string query = string.Format("update dbsecretaria.sg_usuario set " +
                    "nombre = '{0}',contrasena = AES_ENCRYPT('{1}', 'SCOGA'),tipo_usuario={2},habilitado={3} where id_usuario={4}", objusuario.Nombre, objusuario.contraseña, objusuario.tipo_usuario, objusuario.habilitado, objusuario.idUsuario);
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
