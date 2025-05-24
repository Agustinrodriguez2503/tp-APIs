using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls.WebParts;
using dominio;
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
        public void Post([FromBody]ArticuloDto art)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo nuevo = new Articulo();
            
            nuevo.Nombre = art.Nombre;
            nuevo.Codigo = art.Codigo;
            nuevo.Descripcion = art.Descripcion;
            nuevo.Marca.Id = art.Marca;
            nuevo.Categoria.Id = art.Categoria;
            nuevo.Precio = art.Precio;

            negocio.agregar(nuevo);

        }

        // PUT: api/Articulo/5
        //MODIFICAR
        public void Put(int id, [FromBody]ArticuloDto art)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo nuevo = new Articulo();

            nuevo.Nombre = art.Nombre;
            nuevo.Codigo = art.Codigo;
            nuevo.Descripcion = art.Descripcion;
            nuevo.Marca = new Marca { Id = art.Marca };
            nuevo.Categoria = new Categoria { Id = art.Categoria };
            nuevo.Precio = art.Precio;
            nuevo.Id = id;

            negocio.modificar(nuevo);

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
            }else
                return Request.CreateResponse(HttpStatusCode.BadRequest, "El Articulo no existe.");

        }
    }
}
