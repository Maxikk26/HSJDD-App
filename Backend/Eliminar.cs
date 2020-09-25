using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directorio.Backend
{
    class Eliminar
    {
        NpgsqlDataAdapter da;
        public DataSet ds = new DataSet();
        public DataTable dt = new DataTable();
        public Eliminar()
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


    }
}
