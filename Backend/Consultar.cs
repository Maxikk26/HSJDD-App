using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directorio.Backend
{
    public class Consultar
    {
        Conexion pg;
        NpgsqlDataAdapter da;
        public DataSet ds = new DataSet();
        public DataTable dt = new DataTable();
        public Consultar()
        {
            pg = new Conexion();
        }

        private void ejecutar(string sql)
        {
            try
            {
                if (pg.start())
                {
                    da = new NpgsqlDataAdapter(sql, pg.conn);
                    dt.Reset();
                    da.Fill(dt);
                    pg.stop();
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        private void ejecutarSimple(string sql)
        {
            try
            {
                da = new NpgsqlDataAdapter(sql, pg.conn);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                pg.stop();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void medicos()
        {
            try
            {
                string sql = "SELECT id_medico AS id, cedula AS cedula, p_nombre, s_nombre, p_apellido, s_apellido, estatus AS activo FROM medico ORDER BY id_medico";
                ejecutar(sql);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void medicosActivos()
        {
            try
            {
                if (pg.start())
                {
                    string sql = "SELECT * FROM medico WHERE estatus = true ORDER BY id_medico";
                    ejecutarSimple(sql);
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public void medicoEspecifico(string nombre)
        {
            try
            {
                if (pg.start())
                {
                    string sql = "SELECT * FROM medico WHERE p_nombre LIKE '" + nombre + "%' ORDER BY id_medico";
                    ejecutarSimple(sql);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void medicoEspecificoActivo(string nombre)
        {
            try
            {
                if (pg.start())
                {
                    string sql = "SELECT * FROM medico WHERE p_nombre LIKE '" + nombre + "%' AND estatus = true ORDER BY id_medico";
                    ejecutarSimple(sql);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void correos()
        {
            try
            {
                string sql = "SELECT ME.id_medico, ME.p_nombre, ME.p_apellido, ME.s_apellido, traercorreos(ME.id_medico) FROM medico ME";
                ejecutar(sql);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void telefonos(string nombre,bool medico)
        {
            try
            {
                if (pg.start())
                {
                    string sql;
                    if(medico)
                        sql = "SELECT ME.id_medico,TE.telefono,ME.p_nombre,ME.p_apellido,ME.s_apellido FROM medico ME, telefono TE WHERE ME.id_medico = TE.medico_id AND ME.p_nombre LIKE '"+nombre+"%'";
                    else
                        sql = "SELECT TE.id_telefono,TE.telefono,ME.p_nombre,ME.p_apellido,ME.s_apellido FROM medico ME, telefono TE WHERE ME.id_medico = TE.medico_id AND ME.p_nombre LIKE '" + nombre + "%'";

                    ejecutarSimple(sql);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void especialidades()
        {
            try
            {
                string sql = "select * from especialidad ORDER BY especialidad";
                ejecutar(sql);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void especialidadEspecifica(string nombre)
        {
            try
            {
                if (pg.start())
                {
                    string sql = "SELECT * FROM especialidad WHERE especialidad LIKE '" + nombre + "%' ORDER BY id_especialidad";
                    ejecutarSimple(sql);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void especialidadesSecundarias()
        {
            try
            {
                string sql = "SELECT DISTINCT e_secundaria FROM pertenencia";
                ejecutar(sql);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void horaDesde(bool dia)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void horaHasta(bool dia)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void consultorios()
        {
            try
            {
                string sql = "SELECT numero FROM consultorio ORDER BY id_consultorio";
                ejecutar(sql);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void todosConsultorios()
        {
            try
            {
                string sql = "SELECT CO.numero AS consultorio,CO.referencia,PI.piso,PI.numero FROM consultorio CO, piso PI WHERE CO.piso_id = PI.id_piso ORDER BY CO.id_consultorio";
                ejecutar(sql);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void consultorioEspecifico(string nombre)
        {
            try
            {
                if (pg.start())
                {
                    string sql = "SELECT CO.id_consultorio,CO.numero AS consultorio,CO.referencia,PI.piso,PI.numero FROM consultorio CO, piso PI WHERE CO.piso_id = PI.id_piso AND CO.numero LIKE'" + nombre + "%' ";
                    ejecutarSimple(sql);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
