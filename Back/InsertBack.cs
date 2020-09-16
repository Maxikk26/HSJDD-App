using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directorio.Back
{
    public class InsertBack
    {
        NpgsqlDataAdapter da;
        public DataSet ds = new DataSet();
        public DataTable dt = new DataTable();
        Connection pg;
        public InsertBack(Connection con)
        {
            pg = con;
        }

        private void ejecutar(string sql)
        {
            da = new NpgsqlDataAdapter(sql, pg.conn);
            dt.Reset();
            da.Fill(dt);
        }

        public void especialidades()
        {
            string sql = "SELECT * FROM especialidad";
            ejecutar(sql);
        }

        public void especialidadesSecundarias()
        {
            string sql = "SELECT DISTINCT e_secundaria FROM pertenencia";
            ejecutar(sql);
        }

        public void horaDesde(bool dia)
        {
            if (dia)
            {
                string sql = "SELECT DISTINCT to_char(desde,'HH12:MI') AS desde FROM hora WHERE desde <> '01:00' ORDER BY desde ASC";
                ejecutar(sql);
            }
            else
            {
                string sql = "SELECT DISTINCT to_char(desde,'HH12:MI') AS desde FROM hora WHERE desde >= '01:00' AND desde <= '05:00' ORDER BY desde ASC";
                ejecutar(sql);
            }
        }

        public void horaHasta(bool dia)
        {
            if (dia)
            {
                string sql = "SELECT DISTINCT to_char(hasta,'HH12:MI') AS hasta FROM hora WHERE desde <> '01:00' ORDER BY hasta ASC";
                ejecutar(sql);
            }
            else
            {
                string sql = "SELECT DISTINCT to_char(hasta,'HH12:MI') AS hasta FROM hora WHERE hasta >= '01:00' AND hasta <= '05:00' ORDER BY hasta ASC";
                ejecutar(sql);
            }
        }

        public void consultorios()
        {
            string sql = "SELECT numero FROM consultorio";
            ejecutar(sql);
        }
    }
}
