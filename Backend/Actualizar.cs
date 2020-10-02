using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directorio.Backend
{
    public class Actualizar
    {
        Conexion pg;
        public Actualizar()
        {
            pg = new Conexion();
        }

        public string actualizarMedico1(Medico med, string id)
        {
            if (pg.start())
                using (var cmd = new NpgsqlCommand("SELECT actualizarMedico1(@id,@pnombre,@snombre,@papellido,@sapellido)", pg.conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("pnombre", med.pnombre);
                    cmd.Parameters.AddWithValue("snombre", med.snombre);
                    cmd.Parameters.AddWithValue("papellido", med.papellido);
                    cmd.Parameters.AddWithValue("sapellido", med.sapellido);
                    bool x = (bool)cmd.ExecuteScalar();
                    if (x)
                    {
                        pg.conn.Close();
                        return "Actualizado satisfactoriamente";
                    }
                    else
                    {
                        pg.conn.Close();
                        return "Error, intentelo de nuevo";
                    }
                }
            else
                return "Error en la conexión con la base de datos";
        }

        public string actualizarMedico2(Medico med, string id)
        {
            if(pg.start())
                using (var cmd = new NpgsqlCommand("SELECT actualizarMedico2(@id,@pnombre,@papellido,@sapellido)", pg.conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("pnombre", med.pnombre);
                    cmd.Parameters.AddWithValue("papellido", med.papellido);
                    cmd.Parameters.AddWithValue("sapellido", med.sapellido);
                    bool x = (bool)cmd.ExecuteScalar();
                    if (x)
                    {
                        pg.stop();
                        return "Actualizado satisfactoriamente";
                    }
                    else
                        return "Error, intentelo de nuevo";
                }
            else
                return "Error en la conexión con la base de datos";

        }

        public string actualizarCorreo(string id, string correo)
        {
            if(pg.start())
                using (var cmd = new NpgsqlCommand("SELECT actualizarCorreo(@id,@correo)", pg.conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("correo", correo);
                    bool x = (bool)cmd.ExecuteScalar();
                    if (x)
                        return "Actualizado satisfactoriamente";
                    else
                        return "Error, intentelo de nuevo";
                }
            else
                return "Se genero un error en la base de datos, si el error persiste contactar con el administrador";

        }

        public string actualizarConsultorio(string id, string numero, string piso)
        {
            if (pg.start())
            {
                using (var cmd = new NpgsqlCommand("SELECT actualizarConsultorio(@id,@numero,@piso)", pg.conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("numero", numero);
                    cmd.Parameters.AddWithValue("piso", piso);
                    bool x = (bool)cmd.ExecuteScalar();
                    if (x)
                        return "Actualizado satisfactoriamente";
                    else
                        return "Error, intentelo de nuevo";
                }
            }
            else
                return "Se genero un error en la base de datos, si el error persiste contactar con el administrador";

        }
    }
}
