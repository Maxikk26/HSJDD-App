using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directorio.Backend
{
    class EliminarBack
    {
        NpgsqlDataAdapter da;
        public DataSet ds = new DataSet();
        public DataTable dt = new DataTable();
        public EliminarBack()
        {

        }

        public bool eliminar(Conexion con, int id)
        {
            try
            {
                using (var cmd = new NpgsqlCommand("SELECT eliminarMedicoId(@p)", con.conn))
                {
                    cmd.Parameters.AddWithValue("p", id);
                    bool res = (bool)cmd.ExecuteScalar();
                    return res;
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }


        public void medicos(Conexion con)
        {
            string sql = "SELECT * FROM medico WHERE estatus = true ORDER BY id_medico";
            da = new NpgsqlDataAdapter(sql, con.conn);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];

        }

        public void medicoEspecifico(Conexion con,string nombre)
        {
            string sql = "SELECT * FROM medico WHERE p_nombre LIKE '" + nombre + "%' AND estatus = true ORDER BY id_medico";
            da = new NpgsqlDataAdapter(sql, con.conn);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
        }


    }
}
