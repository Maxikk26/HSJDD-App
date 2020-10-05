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
            try
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
            catch(Exception ex)
            {
                return "Se genero un error en la base de datos, intente de nuevo. Si el error persiste contactar con el administrador";
            }
        }

        public string actualizarMedico2(Medico med, string id)
        {
            try
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
            catch(Exception ex)
            {
                return "Se genero un error en la base de datos, intente de nuevo. Si el error persiste contactar con el administrador";

            }


        }

        public string actualizarConsultorio(string id, string numero, string piso)
        {
            try
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
            catch(Exception ex)
            {
                return "Se genero un error en la base de datos, intente de nuevo. Si el error persiste contactar con el administrador";

            }
        }

        public string actualizarEspecialidad(string id, string especialidad)
        {
            try
            {
                if (pg.start())
                {
                    using (var cmd = new NpgsqlCommand("SELECT actualizarEspecialidad(@id,@especialidad)", pg.conn))
                    {
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.Parameters.AddWithValue("especialidad", especialidad);
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
            catch(Exception ex)
            {
                return "Se genero un error en la base de datos, intente de nuevo. Si el error persiste contactar con el administrador";

            }
        }
    }
}
