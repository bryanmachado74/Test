using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class ApuestaModel
    {
        private SqlConnection connection;

        public ApuestaModel()
        {
            String constring = ConfigurationManager.ConnectionStrings["conexionDB"].ToString();
            connection = new SqlConnection(constring);
        }

        public List<Entity.Apuesta> listarApuesta()
        {
            List<Entity.Apuesta> apuestas = new List<Entity.Apuesta>();

            SqlCommand cmd = new SqlCommand("sp_listar_apuesta", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            connection.Open();
            sd.Fill(dt);
            connection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                apuestas.Add(
                    new Entity.Apuesta
                    {
                        id = Convert.ToInt32(dr["apuesta_id"]),
                        cliente = Convert.ToInt32(dr["apuesta_cliente"]),
                        encuentro = Convert.ToInt32(dr["apuesta_encuentro"]),
                        eleccion = Convert.ToString(dr["apuesta_eleccion"]),
                        monto = Convert.ToInt32(dr["apuesta_monto"]),
                    });
            }
            return apuestas;
        }

        public Entity.Apuesta obtenerApuesta(int id)
        {
            Entity.Apuesta apuesta = new Entity.Apuesta();
            SqlCommand cmd = new SqlCommand("sp_obtener_apuesta", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            connection.Open();
            sd.Fill(dt);
            connection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                apuesta.id = Convert.ToInt32(dr["apuesta_id"]);
                apuesta.cliente = Convert.ToInt32(dr["apuesta_cliente"]);
                apuesta.encuentro = Convert.ToInt32(dr["apuesta_encuentro"]);
                apuesta.eleccion = Convert.ToString(dr["apuesta_eleccion"]);
                apuesta.monto = Convert.ToInt32(dr["apuesta_monto"]);
            }
            return apuesta;
        }

        public bool insertarApuesta(Entity.Apuesta apuesta)
        {
            SqlCommand cmd = new SqlCommand("sp_insertar_apuesta", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cliente", apuesta.cliente);
            cmd.Parameters.AddWithValue("@encuentro", apuesta.encuentro);
            cmd.Parameters.AddWithValue("@eleccion", apuesta.eleccion);
            cmd.Parameters.AddWithValue("@monto", apuesta.monto);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool actualizarApuesta(Entity.Apuesta apuesta)
        {
            SqlCommand cmd = new SqlCommand("sp_actualizar_apuesta", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", apuesta.id);
            cmd.Parameters.AddWithValue("@cliente", apuesta.cliente);
            cmd.Parameters.AddWithValue("@encuentro", apuesta.encuentro);
            cmd.Parameters.AddWithValue("@eleccion", apuesta.eleccion);
            cmd.Parameters.AddWithValue("@monto", apuesta.monto);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool BorrarApuesta(int id)
        {
            SqlCommand cmd = new SqlCommand("sp_borrar_apuesta", connection);
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