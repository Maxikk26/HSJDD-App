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
        Conexion pg;
        public DataSet ds = new DataSet();
        public DataTable dt = new DataTable();
        public Eliminar()
        {
            pg = new Conexion();
        }

        public bool eliminar(int id)
        {
            try
            {
                if (pg.start())
                    using (var cmd = new NpgsqlCommand("SELECT eliminarMedicoId(@p)", pg.conn))
                    {
                        cmd.Parameters.AddWithValue("p", id);
                        bool res = (bool)cmd.ExecuteScalar();
                        return res;
                    }
                else
                    return false;
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }


    }
}
