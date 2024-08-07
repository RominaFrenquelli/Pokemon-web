using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelos;

namespace Business
{
    public class PokemonBusiness
    {
        public List<Pokemon> Listar(string id = "")
        {
            List<Pokemon> listado = new List<Pokemon>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; dataBase=POKEDEX_DB; integrated security=True;";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad,P.Id from POKEMONS P, ELEMENTOS E, ELEMENTOS D where E.Id = P.IdTipo AND D.Id = P.IdDebilidad and Activo = 1 ";
                if(id != "")
                    comando.CommandText += " AND P.Id =" + id;
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    Pokemon pok = new Pokemon();
                    pok.Id = (int)lector["Id"];
                    pok.Numero = lector.GetInt32(0);
                    pok.Nombre = (string) lector["Nombre"];
                    pok.Descripcion = (string)lector["Descripcion"];

                    if (!(lector["UrlImagen"] is DBNull))
                        pok.UrlImagen = (string)lector["UrlImagen"];

                    pok.Tipo = new Elemento();
                    pok.Tipo.Id = (int)lector["IdTipo"];
                    pok.Tipo.Descripcion = (string)lector["Tipo"];
                    pok.Debilidad =new Elemento();
                    pok.Debilidad.Id = (int)lector["IdDebilidad"];
                    pok.Debilidad.Descripcion = (string)lector["Debilidad"];

                    listado.Add(pok);
                }
                conexion.Close();
                return listado;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Listar con activar y desactivar
        public List<Pokemon> Listar2(string id = "")
        {
            List<Pokemon> listado = new List<Pokemon>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; dataBase=POKEDEX_DB; integrated security=True;";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad, P.Id, P.Activo from POKEMONS P, ELEMENTOS E, ELEMENTOS D where E.Id = P.IdTipo AND D.Id = P.IdDebilidad ";
                if (id != "")
                    comando.CommandText += " AND P.Id =" + id;
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    Pokemon pok = new Pokemon();
                    pok.Id = (int)lector["Id"];
                    pok.Numero = lector.GetInt32(0);
                    pok.Nombre = (string)lector["Nombre"];
                    pok.Descripcion = (string)lector["Descripcion"];

                    if (!(lector["UrlImagen"] is DBNull))
                        pok.UrlImagen = (string)lector["UrlImagen"];

                    pok.Tipo = new Elemento();
                    pok.Tipo.Id = (int)lector["IdTipo"];
                    pok.Tipo.Descripcion = (string)lector["Tipo"];
                    pok.Debilidad = new Elemento();
                    pok.Debilidad.Id = (int)lector["IdDebilidad"];
                    pok.Debilidad.Descripcion = (string)lector["Debilidad"];

                    pok.Activo = bool.Parse(lector["Activo"].ToString());

                    listado.Add(pok);
                }
                conexion.Close();
                return listado;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Pokemon> ListarConSP()
        {
            List<Pokemon> lista = new List<Pokemon>();
            ConectarDB datos = new ConectarDB();
            try
            {
                datos.SetearStoredProcedure("storedListar");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Pokemon pok = new Pokemon();
                    pok.Id = (int)datos.Lector["Id"];
                    pok.Numero = datos.Lector.GetInt32(0);
                    pok.Nombre = (string)datos.Lector["Nombre"];
                    pok.Descripcion = (string)datos.Lector["Descripcion"];

                    if (!(datos.Lector["UrlImagen"] is DBNull))
                        pok.UrlImagen = (string)datos.Lector["UrlImagen"];

                    pok.Tipo = new Elemento();
                    pok.Tipo.Id = (int)datos.Lector["IdTipo"];
                    pok.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                    pok.Debilidad = new Elemento();
                    pok.Debilidad.Id = (int)datos.Lector["IdDebilidad"];
                    pok.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];

                    pok.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(pok);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Agregar (Pokemon nuevo)
        {
            ConectarDB datos = new ConectarDB();
            try
            {
                datos.SetearConsulta("Insert into POKEMONS (Numero, Nombre, Descripcion, Activo, IdTipo, IdDebilidad, UrlImagen)values(" + nuevo.Numero + ", '" + nuevo.Nombre + "', '" + nuevo.Descripcion + "', 1, @idTipo, @idDebilidad, @urlImagen)");
                datos.SetearParametro("@idTipo", nuevo.Tipo.Id);
                datos.SetearParametro("@idDebilidad", nuevo.Debilidad.Id);
                datos.SetearParametro("@urlImagen", nuevo.UrlImagen);
                datos.EjecutarAccion();
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

        public void AgregarConSP(Pokemon nuevo)
        {
            ConectarDB datos = new ConectarDB();
            try
            {
                datos.SetearStoredProcedure("StoredAgregrarPokemon");
                datos.SetearParametro("@Numero", nuevo.Numero);
                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Descripcion", nuevo.Descripcion);
                datos.SetearParametro("@UrlImagen", nuevo.UrlImagen);
                datos.SetearParametro("@IdTipo", nuevo.Tipo.Id);
                datos.SetearParametro("@IdDebilidad", nuevo.Debilidad.Id);
                
                datos.EjecutarAccion();
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

        public void Modificar(Pokemon modificado)
        {
            ConectarDB datos = new ConectarDB();
            try
            {
                datos.SetearConsulta("update POKEMONS set Numero = @numero, Nombre = @nombre, Descripcion = @descripcion, UrlImagen = @urlImagen, IdTipo = @idTipo, IdDebilidad = @idDebilidad where Id = @id");
                datos.SetearParametro("@numero", modificado.Numero);
                datos.SetearParametro("@nombre", modificado.Nombre);
                datos.SetearParametro("@descripcion", modificado.Descripcion);
                datos.SetearParametro("@urlImagen", modificado.UrlImagen);
                datos.SetearParametro("@idTipo", modificado.Tipo.Id);
                datos.SetearParametro("@idDebilidad", modificado.Debilidad.Id);
                datos.SetearParametro("@id", modificado.Id);

                datos.EjecutarAccion();
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

        public void ModificarConSP(Pokemon modificado)
        {
            ConectarDB datos = new ConectarDB();
            try
            {
                datos.SetearStoredProcedure("StoredModificarPokemon");
                datos.SetearParametro("@numero", modificado.Numero);
                datos.SetearParametro("@nombre", modificado.Nombre);
                datos.SetearParametro("@descripcion", modificado.Descripcion);
                datos.SetearParametro("@urlImagen", modificado.UrlImagen);
                datos.SetearParametro("@idTipo", modificado.Tipo.Id);
                datos.SetearParametro("@idDebilidad", modificado.Debilidad.Id);
                datos.SetearParametro("@id", modificado.Id);

                datos.EjecutarAccion();
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

        public void Eliminar(int id)
        {
            ConectarDB datos = new ConectarDB();
            try
            {
                datos.SetearConsulta("delete from POKEMONS where Id = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarAccion();
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

        public List<Pokemon> Filtrar(string campo, string criterio, string filtro, string estado)
        {
            List<Pokemon> lista = new List<Pokemon>();
            ConectarDB datos = new ConectarDB();
            try
            {
                string consulta = "Select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad,P.Id, P.Activo from POKEMONS P, ELEMENTOS E, ELEMENTOS D where E.Id = P.IdTipo AND D.Id = P.IdDebilidad AND ";
                switch (campo)
                {
                    case "Numero":
                        switch (criterio)
                        {
                            case "Mayor a":
                                consulta += "Numero > " + filtro;
                                break;
                            case "Menor a":
                                consulta += "Numero < " + filtro;
                                break;
                            default:
                                consulta += "Numero = " + filtro;
                                break;

                        }
                        break;
                    case "Nombre":
                        switch (criterio)
                        {
                            case "Comienza con ":
                                consulta += "Nombre like '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += "Nombre like '%" + filtro + "'";
                                break;
                            default:
                                consulta += "Nombre like '%" + filtro + "%'";
                                break;
                        }
                        break;
                    default:
                        switch (criterio)
                        {
                            case "Comienza con ":
                                consulta += "E.Descripcion like '" + filtro + "%' ";
                                break;
                            case "Termina con":
                                consulta += "E.Descripcion like '%" + filtro + "' ";
                                break;
                            default:
                                consulta += "E.Descripcion like '%" + filtro + "%' ";
                                break;
                        }
                        break;
                }

                if (estado == "Activo")
                    consulta += " and P.Activo = 1";
                else if (estado == "Inactivo")
                    consulta += " and P.Activo = 0";

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Pokemon pok = new Pokemon();
                    pok.Id = (int)datos.Lector["Id"];
                    pok.Numero = datos.Lector.GetInt32(0);
                    pok.Nombre = (string)datos.Lector["Nombre"];
                    pok.Descripcion = (string)datos.Lector["Descripcion"];

                    if (!(datos.Lector["UrlImagen"] is DBNull))
                        pok.UrlImagen = (string)datos.Lector["UrlImagen"];

                    pok.Tipo = new Elemento();
                    pok.Tipo.Id = (int)datos.Lector["IdTipo"];
                    pok.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                    pok.Debilidad = new Elemento();
                    pok.Debilidad.Id = (int)datos.Lector["IdDebilidad"];
                    pok.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];
                    pok.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(pok);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public void EliminarLogico(int id, bool activo = false)
        {
                ConectarDB datos = new ConectarDB();
            try
            {
                datos.SetearConsulta("update POKEMONS set Activo = @Activo where Id = @id");
                datos.SetearParametro("@id", id);
                datos.SetearParametro("@Activo", activo);
                datos.EjecutarAccion();
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
