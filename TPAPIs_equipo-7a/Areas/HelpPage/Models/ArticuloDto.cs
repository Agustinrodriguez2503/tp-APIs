﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using dominio;

namespace TPAPIs_equipo_7a.Areas.HelpPage.Models
{
    public class ArticuloDto
    {

        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Marca { get; set; }
        public int Categoria { get; set; }
        public decimal Precio { get; set; }
    }
}