using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class EncuentroModel
    {
        private SqlConnection connection;

        public EncuentroModel() 
        {
            String constring = ConfigurationManager.ConnectionStrings["conexionDB"].ToString();
            connection = new SqlConnection(constring);
        }

        public List<Entity.Encuentro> listarEncuentro()
        {
            List<Entity.Encuentro> encuentros = new List<Entity.Encuentro>();

            SqlCommand cmd = new SqlCommand("sp_listar_encuentro", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            connection.Open();
            sd.Fill(dt);
            connection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                encuentros.Add(
                    new Entity.Encuentro
                    {
                        id = Convert.ToInt32(dr["encuentro_id"]),
                        local = Convert.ToString(dr["encuentro_local"]),
                        visitante = Convert.ToString(dr["encuentro_visitante"]),
                        jugado = Convert.ToByte(dr["encuentro_jugado"]),
                        marcador_local = Convert.ToInt32(dr["encuentro_marcador_local"]),
                        marcador_visitante = Convert.ToInt32(dr["encuentro_marcador_visitante"]),
                        probabilidad_local = float.Parse(dr["encuentro_probabilidad_local"].ToString()),
                        probabilidad_empate = float.Parse(dr["encuentro_probabilidad_empate"].ToString()),
                        probabilidad_visita = float.Parse(dr["encuentro_probabilidad_visitante"].ToString()),
                        jornada = Convert.ToInt32(dr["encuentro_jornada"]),
                    });
            }
            return encuentros;
        }

        public Entity.Encuentro obtenerEncuentro(int id)
        {
            Entity.Encuentro encuentro = new Entity.Encuentro();
            SqlCommand cmd = new SqlCommand("sp_obtener_encuentro", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            connection.Open();
            sd.Fill(dt);
            connection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                encuentro.id = Convert.ToInt32(dr["encuentro_id"]);
                encuentro.local = Convert.ToString(dr["encuentro_local"]);
                encuentro.visitante = Convert.ToString(dr["encuentro_visitante"]);
                encuentro.jugado = Convert.ToByte(dr["encuentro_jugado"]);
                encuentro.marcador_local = Convert.ToInt32(dr["encuentro_marcador_local"]);
                encuentro.marcador_visitante = Convert.ToInt32(dr["encuentro_marcador_visitante"]);
                encuentro.probabilidad_local = float.Parse(dr["encuentro_probabilidad_local"].ToString());
                encuentro.probabilidad_empate = float.Parse(dr["encuentro_probabilidad_empate"].ToString());
                encuentro.probabilidad_visita = float.Parse(dr["encuentro_probabilidad_visitante"].ToString());
                encuentro.jornada = Convert.ToInt32(dr["encuentro_jornada"]);
            }
            return encuentro;
        }

        public bool insertarEncuentro(Entity.Encuentro encuentro)
        {
            SqlCommand cmd = new SqlCommand("sp_insertar_encuentro", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@local", encuentro.local);
            cmd.Parameters.AddWithValue("@visitante", encuentro.visitante);
            cmd.Parameters.AddWithValue("@probabilidad_local", encuentro.probabilidad_local);
            cmd.Parameters.AddWithValue("@probabilidad_empate", encuentro.probabilidad_empate);
            cmd.Parameters.AddWithValue("@probabilidad_visitante", encuentro.probabilidad_visita);
            cmd.Parameters.AddWithValue("@jornada", encuentro.jornada);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool actualizarEncuentro(Entity.Encuentro encuentro)
        {
            SqlCommand cmd = new SqlCommand("sp_actualizar_encuentro", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", encuentro.id);
            cmd.Parameters.AddWithValue("@local", encuentro.local);
            cmd.Parameters.AddWithValue("@visitante", encuentro.visitante);
            cmd.Parameters.AddWithValue("@jugado", encuentro.jugado); 
            cmd.Parameters.AddWithValue("@marcador_local", encuentro.marcador_local);
            cmd.Parameters.AddWithValue("@marcador_visitante", encuentro.marcador_visitante);
            cmd.Parameters.AddWithValue("@probabilidad_local", encuentro.probabilidad_local);
            cmd.Parameters.AddWithValue("@probabilidad_empate", encuentro.probabilidad_empate);
            cmd.Parameters.AddWithValue("@probabilidad_visita", encuentro.probabilidad_visita);
            cmd.Parameters.AddWithValue("@jornada", encuentro.jornada);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool BorrarEncuentro(int id)
        {
            SqlCommand cmd = new SqlCommand("sp_borrar_encuentro", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }//end class
}//end namespace