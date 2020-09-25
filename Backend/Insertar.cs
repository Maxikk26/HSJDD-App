using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directorio.Backend
{
    public class Insertar
    {
        NpgsqlDataAdapter da;
        public DataSet ds = new DataSet();
        public DataTable dt = new DataTable();
        private List<bool> insert = new List<bool>();
        private int x = 0;
        Conexion pg;
        public Insertar(Conexion con)
        {
            pg = con;
        }

        public string crearInsert(Medico medico, Horario diurno, Horario vespertino)
        {
            try
            {
                insertarMedico(medico);
                switch (x)
                {
                    case 1:
                        insert.Add(true);
                        break;
                    case 2:
                        return "Medico actualmente registrado, cambiando estatus a 'Activo'";
                    case 3:
                        return "Meidco ya existe y activo en el sistema";
                }
                insertarTelefono(medico);
                insertarCorreo(medico);
                insertarMedicoConsultorio(medico);
                insertarPertenecia(medico);
                validarAsistencia(medico,diurno);
                validarAsistencia(medico,vespertino);
                if (insert.Contains(false))
                    return "Error, revise los campos ingresados";
                else
                    return "Medico registrado satisfactoriamente!";
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
                x = (int)cmd.ExecuteScalar();
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
            if(medico.esecundaria != "")
                using (var cmd = new NpgsqlCommand("SELECT insertarPertenencia(@cedula,@cargo,@especialidad,@esecundaria)", pg.conn))
                {
                    cmd.Parameters.AddWithValue("cedula", medico.cedula);
                    cmd.Parameters.AddWithValue("cargo", medico.cargo);
                    cmd.Parameters.AddWithValue("especialidad", medico.especialidad);
                    cmd.Parameters.AddWithValue("esecundaria", medico.esecundaria);
                    insert.Add((bool)cmd.ExecuteScalar());
                }
            else
                using (var cmd = new NpgsqlCommand("SELECT insertarPertenencia2(@cedula,@cargo,@especialidad)", pg.conn))
                {
                    cmd.Parameters.AddWithValue("cedula", medico.cedula);
                    cmd.Parameters.AddWithValue("cargo", medico.cargo);
                    cmd.Parameters.AddWithValue("especialidad", medico.especialidad);
                    insert.Add((bool)cmd.ExecuteScalar());
                }
        }

        public string crearInsertEspecialidad(string especialidad)
        {
            bool x = false;
            using (var cmd = new NpgsqlCommand("SELECT insertarEspecialidad(@especialidad)", pg.conn))
            {
                cmd.Parameters.AddWithValue("especialidad",especialidad);
                x = (bool)cmd.ExecuteScalar();
                if (x)
                {
                    using (var cmad = new NpgsqlCommand("SELECT insertarDiaEspecialidad(@especialidad)", pg.conn))
                    {
                        cmad.Parameters.AddWithValue("especialidad", especialidad);
                        x = (bool)cmad.ExecuteScalar();
                        if(x)
                            return "Especialidad registrada satisfactoriamente";
                        else
                            return "Error al registrar la especialidad, intente de nuevo";
                    }
                }
                else
                    return "Error al registrar la especialidad, intente de nuevo";
            }
        }

        public string crearInsertConsultorio(string consultorio, string piso)
        {
            bool exists = validarConsultorio(consultorio);
            if (!exists)
                return "Ya existe el consultorio";
            else
                using (var cmd = new NpgsqlCommand("SELECT insertarConsultorio(@consultorio,@piso)", pg.conn))
                {
                    cmd.Parameters.AddWithValue("consultorio", consultorio);
                    cmd.Parameters.AddWithValue("piso", piso);
                    bool x = (bool)cmd.ExecuteScalar();
                    if (x)
                        return "Consultorio registrado satisfactoriamente";
                    else
                        return "Se ha generado un error al registrar, intentelo de nuevo";
                }
        }

        private bool validarConsultorio(string con)
        {
            using (var cmd = new NpgsqlCommand("SELECT validarConsultorio(@consultorio)", pg.conn))
            {
                cmd.Parameters.AddWithValue("consultorio",con);
                bool x = (bool)cmd.ExecuteScalar();
                return x;
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
