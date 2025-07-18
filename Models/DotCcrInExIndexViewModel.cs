using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication2.Models;

namespace WebApplication2.Models
{
    public class DotCcrInExIndexViewModel
    {
        // Filtros
        public string? RutFilter { get; set; }
        public string? AgenteFilter { get; set; }
        public string? PaisFilter { get; set; }
        public string? NombreCcFilter { get; set; }
        public string? AreaFilter { get; set; }
        public string? AreaGestionFilter { get; set; }
        public string? ContratoFilter { get; set; }
        public string? TipoAgenteFilter { get; set; }
        public string? ServicioFilter { get; set; }
        public string? NombreJefaturaFilter { get; set; }
        public string? MesGestionFilter { get; set; }

        // Listas para combos
        public SelectList? PaisCallCenterList { get; set; }
        public SelectList? NombreCallCenterList { get; set; }
        public SelectList? AreaList { get; set; }
        public SelectList? AreaGestionList { get; set; }
        public SelectList? ContratoList { get; set; }
        public SelectList? TiposAgenteList { get; set; }
        public SelectList? ServicioList { get; set; }
        public SelectList? NombreJefaturaList { get; set; }
        public SelectList? MesGestionList { get; set; }

        // Resultados
        public IEnumerable<PersonRecord> Results { get; set; } = new List<PersonRecord>();
    }
}