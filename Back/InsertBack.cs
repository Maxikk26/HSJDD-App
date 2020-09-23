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
        private List<bool> insert = new List<bool>();
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

        public bool crearInsert(Medico medico, Horario diurno, Horario vespertino)
        {
            try
            {
                insertarMedico(medico);
                insertarTelefono(medico);
                insertarCorreo(medico);
                insertarMedicoConsultorio(medico);
                insertarPertenecia(medico);
                validarAsistencia(medico,diurno);
                validarAsistencia(medico,vespertino);
                if (insert.Contains(false))
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void insertarMedico(Medico medico)
        {
            using (var cmd = new NpgsqlCommand("SELECT insertarMedico(@cedula,@pnombre,@snombre,@papellido,@sapellido)", pg.conn))
            {
                cmd.Parameters.AddWithValue("cedula", medico.cedula);
                cmd.Parameters.AddWithValue("pnombre", medico.pnombre);
                cmd.Parameters.AddWithValue("snombre", medico.snombre);
                cmd.Parameters.AddWithValue("papellido", medico.papellido);
                cmd.Parameters.AddWithValue("sapellido", medico.sapellido);
                insert.Add((bool)cmd.ExecuteScalar());
            }
        }
        private void insertarTelefono(Medico medico)
        {
            using (var cmd = new NpgsqlCommand("SELECT insertarTelefono(@cedula,@celular)", pg.conn))
            {
                string celular = medico.cprefijo + "-" + medico.cnumero;
                cmd.Parameters.AddWithValue("cedula", medico.cedula);
                cmd.Parameters.AddWithValue("celular", celular);
                insert.Add((bool)cmd.ExecuteScalar());
            }
            using (var cmd = new NpgsqlCommand("SELECT insertarTelefono(@cedula,@fijo)", pg.conn))
            {
                string fijo = medico.fprefijo + "-" + medico.fnumero;
                cmd.Parameters.AddWithValue("cedula", medico.cedula);
                cmd.Parameters.AddWithValue("fijo", fijo);
                insert.Add((bool)cmd.ExecuteScalar());
            }
        }

        private void insertarCorreo(Medico medico)
        {
            using (var cmd = new NpgsqlCommand("SELECT insertarCorreo(@cedula,@correo)", pg.conn))
            {
                cmd.Parameters.AddWithValue("cedula", medico.cedula);
                cmd.Parameters.AddWithValue("correo", medico.correo);
                insert.Add((bool)cmd.ExecuteScalar());
            }
        }
        private void insertarMedicoConsultorio(Medico medico)
        {
            using (var cmd = new NpgsqlCommand("SELECT insertarMedicoConsultorio(@cedula,@consultorio)", pg.conn))
            {
                cmd.Parameters.AddWithValue("cedula", medico.cedula);
                cmd.Parameters.AddWithValue("consultorio", medico.consultorio);
                insert.Add((bool)cmd.ExecuteScalar());
            }
            if (medico.consultorio2 != "")
            {
                
                using (var cmd = new NpgsqlCommand("SELECT insertarMedicoConsultorio(@cedula,@consultorio)", pg.conn))
                {
                    cmd.Parameters.AddWithValue("cedula", medico.cedula);
                    cmd.Parameters.AddWithValue("consultorio", medico.consultorio2);
                    insert.Add((bool)cmd.ExecuteScalar());
                }
            }
           
        }

        private void insertarPertenecia(Medico medico)
        {
            using (var cmd = new NpgsqlCommand("SELECT insertarPertenencia(@cedula,@cargo,@especialidad,@esecundaria)", pg.conn))
            {
                cmd.Parameters.AddWithValue("cedula", medico.cedula);
                cmd.Parameters.AddWithValue("cargo", medico.cargo);
                cmd.Parameters.AddWithValue("especialidad", medico.especialidad);
                cmd.Parameters.AddWithValue("esecundaria", medico.esecundaria);
                insert.Add((bool)cmd.ExecuteScalar());
            }
        }

        private void validarAsistencia(Medico medico,Horario horario)
        {
            foreach(string s in horario.Dias)
            {
                switch (s)
                {
                    case "Lunes":
                        insertarAsistencia(medico, horario, s);
                        break;
                    case "Martes":
                        insertarAsistencia(medico, horario, s);
                        break;
                    case "Miércoles":
                        insertarAsistencia(medico, horario, s);
                        break;
                    case "Jueves":
                        insertarAsistencia(medico, horario, s);
                        break;
                    case "Viernes":
                        insertarAsistencia(medico, horario, s);
                        break;
                }
            }
            
        }

        private void insertarAsistencia(Medico medico, Horario horario, string dia)
        {
            using (var cmd = new NpgsqlCommand("SELECT insertarAsistencia(@cedula,@desde,@hasta,@dia)", pg.conn))
            {
                cmd.Parameters.AddWithValue("cedula", medico.cedula);
                cmd.Parameters.AddWithValue("desde", horario.desde);
                cmd.Parameters.AddWithValue("hasta", horario.hasta);
                cmd.Parameters.AddWithValue("dia", dia);
                insert.Add((bool)cmd.ExecuteScalar());
            }
        }
    }
}
