using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ElementoBusiness
    {
        public List<Elemento> Listar()
        {
            List<Elemento> lista = new List<Elemento>();
            ConectarDB datos = new ConectarDB();

            try
            {
                datos.SetearConsulta("Select Id, Descripcion from ELEMENTOS");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Elemento aux = new Elemento();
                    aux.Id = (int) datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex; 
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
