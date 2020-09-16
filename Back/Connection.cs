using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directorio.Back
{
    public class Connection
    {
        public NpgsqlConnection conn;
        public bool start()
        {
            try
            {
                string connstring = String.Format("Server=johnny.heliohost.org;Port=5432;" +
                    "User Id=hsjdd_admin;Password=HSJDD-dm2020;Database=hsjdd_medicos;");
                conn = new NpgsqlConnection(connstring);
                conn.Open();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool stop()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
