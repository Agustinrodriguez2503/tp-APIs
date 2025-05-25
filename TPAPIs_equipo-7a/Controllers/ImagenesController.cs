using System;
using System.Collections.Generic;
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
    public class ImagenesController : ApiController
    {
        // PUT: api/Imagenes/5
        //public HttpResponseMessage POST(int id, [FromBody] List<string> listaUrls)
        //{
        //    // 1. Validaciones básicas de entrada (lista no vacía, URLs no vacías)
        //    if (listaUrls == null || !listaUrls.Any())
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "La lista de URLs de imágenes no puede estar vacía.");
        //    }
        //    foreach (var url in listaUrls)
        //    {
        //        if (string.IsNullOrWhiteSpace(url.ToString()))
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Una o más URLs de imagen proporcionadas no son válidas o están vacías.");
        //        }
        //    }

        //    ArticuloNegocio articuloNegocio = new ArticuloNegocio(); // Para verificar si el artículo existe
        //    ImagenesNegocio imagenesNegocio = new ImagenesNegocio(); // Tu negocio de imágenes

        //    try
        //    {
        //        // 2. Verificar si el producto (artículo) existe
        //        Articulo buscado = articuloNegocio.ObtenerArticulosConImagenes().Find(x => x.Id == id);
        //        if (buscado == null)
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"El producto con ID {id} no fue encontrado.");
        //        }

        //        // 3. Llamar al método de ImagenesNegocio que maneja la lista de URLs
        //        imagenesNegocio.agregarListadoUrls(id, listaUrls);

        //        return Request.CreateResponse(HttpStatusCode.OK, $"Imágenes agregadas correctamente al producto ID {id}.");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Loguear la excepción (ex.ToString())
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Ocurrió un error al intentar agregar las imágenes: " + ex.Message);
        //    }
        //}
    }
}
