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
                        pg.stop();
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

        public string eliminarCorreoID(string id)
        {
            if (pg.start())
            {
                using (var cmd = new NpgsqlCommand("SELECT eliminarCorreoID(@idcorreo)", pg.conn))
                {
                    cmd.Parameters.AddWithValue("idcorreo", id);
                    bool x = (bool)cmd.ExecuteScalar();
                    if (x)
                    {
                        pg.stop();
                        return "Correo elimiando satisfactoriamente";
                    }
                    else
                    {
                        pg.stop();
                        return "Se ha generado un error al eliminar, revise los datos ingresados";
                    }
                }
            }
            else
                return "Se genero un error en la base de datos, si el error persiste contactar con el administrador";

        }

        public string eliminarTelefonoID(string id)
        {
            if (pg.start())
            {
                using (var cmd = new NpgsqlCommand("SELECT eliminarTelefonoID(@id)", pg.conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    bool x = (bool)cmd.ExecuteScalar();
                    if (x)
                    {
                        pg.stop();
                        return "Teléfono elimiando satisfactoriamente";
                    }
                    else
                    {
                        pg.stop();
                        return "Se ha generado un error al eliminar, revise los datos ingresados";
                    }
                }
            }
            else
                return "Se genero un error en la base de datos, si el error persiste contactar con el administrador";
        }

    }
}
