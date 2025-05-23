using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using dominio;
using negocio;

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
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Articulo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Articulo/5
        public void Delete(int id)
        {
        }
    }
}
