using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Web.Http;
using System.Web.UI.WebControls.WebParts;
using dominio;
using Microsoft.Ajax.Utilities;
using negocio;
using TPAPIs_equipo_7a.Areas.HelpPage.Models;

namespace TPAPIs_equipo_7a.Controllers
{
    public class ArticuloController : ApiController
    {
        // GET: api/Articulo
        // LISTADO
        public IEnumerable<Articulo> Get()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            return negocio.ObtenerArticulosConImagenes();
        }

        // GET: api/Articulo/5
        // BUSQUEDA POR ID
        public Articulo Get(int id)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            List<Articulo> lista = negocio.ObtenerArticulosConImagenes();

            return lista.Find(x => x.Id == id);
        }

        // POST: api/Articulo
        // Se agrega un producto a la base de datos (sin imagen),
        // luego de creado el articulo se le agrega la imagen en el POST de imagen, ya que necesitamos el IDArticulo.
        public HttpResponseMessage Post([FromBody] ArticuloDto art)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ArticuloNegocio negocio = new ArticuloNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            //Validación de marca.
            Marca idMarca = marcaNegocio.listar().Find(x => x.Id == art.Marca);
            if (idMarca == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "La marca ingresada no existe.");

            //Validación de categoria.
            Categoria idCategoria = categoriaNegocio.listar().Find(x => x.Id == art.Categoria);
            if (idCategoria == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "La categoría ingresada no existe.");

            //NO son necesarias estas validaciones ya que se valida en ArticulosDto
            ////Validación de nombre.
            //if (art.Nombre == null)
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Debe ingresar un nombre.");

            ////Validación de descripción.
            //if (art.Descripcion == null)
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Debe ingresar una descripción.");

            ////Validación de código.
            //if (art.Codigo == null)
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Debe ingresar un código.");

            ////Validación de precio.
            //if (art.Precio <= 0)
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Debe ingresar un precio mayor a 0.");


            Articulo nuevo = new Articulo();

            nuevo.Nombre = art.Nombre;
            nuevo.Codigo = art.Codigo;
            nuevo.Descripcion = art.Descripcion;
            nuevo.Marca = new Marca();
            nuevo.Marca.Id = art.Marca;
            nuevo.Categoria = new Categoria();
            nuevo.Categoria.Id = art.Categoria;
            nuevo.Precio = art.Precio;

            negocio.agregar(nuevo);
            return Request.CreateResponse(HttpStatusCode.OK, "Artículo agregado correctamente.");
        }

        // POST: api/Articulo/Id/Imagenes
        // Se agrega un listado de imagenes a un ID de articulo ya ingresado
        [HttpPost]
        [Route("api/Articulo/{id}/Imagenes")]
        public HttpResponseMessage AgregarListaImagenes(int id, [FromBody] List<string> listaUrls)
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            ImagenesNegocio imagenesNegocio = new ImagenesNegocio();

            //Verificar si el articulo existe
            Articulo buscado = articuloNegocio.ObtenerArticulosConImagenes().Find(x => x.Id == id);
            if (buscado == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"El producto con ID {id} no fue encontrado.");
            }

            if (listaUrls == null || !listaUrls.Any())
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "La lista de URLs de imágenes no puede estar vacía.");
            }

            //Por cada url que se haya ingresado en la lista se generará un registro en la tabla de imagenes
            foreach (var url in listaUrls)
            {
                if (string.IsNullOrWhiteSpace(url))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Una o más URLs de imagen proporcionadas no son válidas o están vacías.");
                }

                Uri uriResult;
                bool esUrlValida = Uri.TryCreate(url, UriKind.Absolute, out uriResult) &&
                                         (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                if (!esUrlValida)
                {
                    // Si la URL no es válida o no usa http/https, retorna un error
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"La URL proporcionada '{url}' no tiene un formato válido o no es una URL absoluta HTTP/HTTPS.");
                }
                try
                {
                    Imagenes nuevo = new Imagenes();
                    nuevo.IdArticulo = id;
                    nuevo.ImagenUrl = url;

                    imagenesNegocio.agregar(nuevo);

                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Ocurrió un error al intentar agregar las imágenes: " + ex.Message);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"Imágenes agregadas correctamente al producto ID {id}.");
        }

        // PUT: api/Articulo/5
        //MODIFICAR
        public HttpResponseMessage Put(int id, [FromBody] ArticuloDto art)
        {

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ArticuloNegocio negocio = new ArticuloNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            Articulo buscado = negocio.ObtenerArticulosConImagenes().Find(x => x.Id == id);

            if (buscado != null)
            {
                //Validación de marca.
                Marca idMarca = marcaNegocio.listar().Find(x => x.Id == art.Marca);
                if (idMarca == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "La marca ingresada no existe.");

                //Validación de categoria.
                Categoria idCategoria = categoriaNegocio.listar().Find(x => x.Id == art.Categoria);
                if (idCategoria == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "La categoría ingresada no existe.");

                Articulo nuevo = new Articulo();

                nuevo.Nombre = art.Nombre;
                nuevo.Codigo = art.Codigo;
                nuevo.Descripcion = art.Descripcion;
                nuevo.Marca = new Marca { Id = art.Marca };
                nuevo.Categoria = new Categoria { Id = art.Categoria };
                nuevo.Precio = art.Precio;
                nuevo.Id = id;

                negocio.modificar(nuevo);
                return Request.CreateResponse(HttpStatusCode.OK, "Articulo modificado correctamente.");
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "El Articulo con el ID especificado no existe.");
        }

        // DELETE: api/Articulo/5
        public HttpResponseMessage Delete(int id)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            Articulo buscado = negocio.ObtenerArticulosConImagenes().Find(x => x.Id == id);

            if (buscado != null)
            {
                negocio.eliminar(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Articulo eliminado correctamente.");
            }
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, "El Articulo no existe.");

        }
    }
}
