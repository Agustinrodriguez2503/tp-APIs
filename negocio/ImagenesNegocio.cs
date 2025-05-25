using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ImagenesNegocio
    {
        public List<Imagenes> listar()
        {
            List<Imagenes> lista = new List<Imagenes>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select Id, IdArticulo, ImagenUrl From Imagenes");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagenes aux = new Imagenes();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.IdArticulo = (int)datos.Lector["IdArticulo"];
                    aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

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
                datos.cerrarConexion();
            }
        }

        public void agregar(Imagenes nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                //Inserta en la Tabla Articulos
                datos.setearConsulta("Insert into IMAGENES (IdArticulo, ImagenUrl) values (@IdArticulo, @ImagenUrl)");
                datos.setearParametro("@IdArticulo", nuevo.IdArticulo);
                datos.setearParametro("@ImagenUrl", nuevo.ImagenUrl);
                datos.ejecutarAccion();


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }        
    }
}

