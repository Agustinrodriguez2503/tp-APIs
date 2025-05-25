using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using dominio;

namespace TPAPIs_equipo_7a.Areas.HelpPage.Models
{
    public class ArticuloDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Código es obligatorio y no puede estar vacío.")]
        [StringLength(50)]
        public string Codigo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nombre es obligatorio y no puede estar vacío.")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La Descripción es obligatoria y no puede estar vacía.")]
        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El Id de Marca es obligatorio y no puede estar vacio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El Id de Marca no es válido.")]
        public int Marca { get; set; }

        [Required(ErrorMessage = "El Id de Categoria es obligatorio y no puede estar vacio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El Id de Categoría no es válido.")]
        public int Categoria { get; set; }

        [Required(ErrorMessage = "El Precio es obligatorio y no puede ser cero.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El Precio debe ser mayor que cero.")]
        public decimal Precio { get; set; }
    }
}