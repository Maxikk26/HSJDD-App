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
        public Actualizar(Conexion con)
        {
            pg = con;
        }

        public string actualizarMedico1(Medico med, string id)
        {
            using (var cmd = new NpgsqlCommand("SELECT actualizarMedico1(@id,@pnombre,@snombre,@papellido,@sapellido)", pg.conn))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("pnombre", med.pnombre);
                cmd.Parameters.AddWithValue("snombre", med.snombre);
                cmd.Parameters.AddWithValue("papellido", med.papellido);
                cmd.Parameters.AddWithValue("sapellido", med.sapellido);
                bool x = (bool)cmd.ExecuteScalar();
                if (x)
                    return "Actualizado satisfactoriamente";
                else
                    return "Error, intentelo de nuevo";
            }
        }

        public string actualizarMedico2(Medico med, string id)
        {
            using (var cmd = new NpgsqlCommand("SELECT actualizarMedico2(@id,@pnombre,@papellido,@sapellido)", pg.conn))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("pnombre", med.pnombre);
                cmd.Parameters.AddWithValue("papellido", med.papellido);
                cmd.Parameters.AddWithValue("sapellido", med.sapellido);
                bool x = (bool)cmd.ExecuteScalar();
                if (x)
                    return "Actualizado satisfactoriamente";
                else
                    return "Error, intentelo de nuevo";
            }
        }
    }
}
