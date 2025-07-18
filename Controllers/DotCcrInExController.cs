using WebApplication2.Data;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication2.Data;

namespace WebApplication2.Controllers
{
    public class DotCcrInExController : Controller
    {
        private readonly AppDbContext _context;

        public DotCcrInExController(AppDbContext context) => _context = context;

        /* ------------------------- LIST + FILTROS ------------------------- */
        public async Task<IActionResult> Index(
            string rutFilter, string agenteFilter,
            string paisFilter, string nombreCcFilter,
            string areaFilter, string areaGestionFilter,
            string contratoFilter, string tipoAgenteFilter,
            string servicioFilter, string nombreJefaturaFilter,
            string mesGestionFilter)
        {
            var q = _context.TbBaseDotCcrInEx.AsQueryable();

            if (!string.IsNullOrWhiteSpace(rutFilter)) q = q.Where(x => x.RutDni.Contains(rutFilter));
            if (!string.IsNullOrWhiteSpace(agenteFilter)) q = q.Where(x => x.Agente!.Contains(agenteFilter));
            if (!string.IsNullOrWhiteSpace(paisFilter)) q = q.Where(x => x.PaisCallCenter == paisFilter);
            if (!string.IsNullOrWhiteSpace(nombreCcFilter)) q = q.Where(x => x.NombreCallCenter == nombreCcFilter);
            if (!string.IsNullOrWhiteSpace(areaFilter)) q = q.Where(x => x.Area == areaFilter);
            if (!string.IsNullOrWhiteSpace(areaGestionFilter)) q = q.Where(x => x.AreaGestion == areaGestionFilter);
            if (!string.IsNullOrWhiteSpace(contratoFilter) &&
                double.TryParse(contratoFilter, out var c)) q = q.Where(x => x.Contrato == c);
            if (!string.IsNullOrWhiteSpace(tipoAgenteFilter)) q = q.Where(x => x.TiposDeAgente == tipoAgenteFilter);
            if (!string.IsNullOrWhiteSpace(servicioFilter)) q = q.Where(x => x.Servicio == servicioFilter);
            if (!string.IsNullOrWhiteSpace(nombreJefaturaFilter)) q = q.Where(x => x.NombreJefatura == nombreJefaturaFilter);
            if (!string.IsNullOrWhiteSpace(mesGestionFilter)) q = q.Where(x => x.MesGestion == mesGestionFilter);

            var model = new DotCcrInExIndexViewModel
            {
                RutFilter = rutFilter,
                AgenteFilter = agenteFilter,
                PaisFilter = paisFilter,
                NombreCcFilter = nombreCcFilter,
                AreaFilter = areaFilter,
                AreaGestionFilter = areaGestionFilter,
                ContratoFilter = contratoFilter,
                TipoAgenteFilter = tipoAgenteFilter,
                ServicioFilter = servicioFilter,
                NombreJefaturaFilter = nombreJefaturaFilter,
                MesGestionFilter = mesGestionFilter,
                Results = await q.AsNoTracking().ToListAsync(),
                PaisCallCenterList = new SelectList(await DistinctAsync(x => x.PaisCallCenter)),
                NombreCallCenterList = new SelectList(await DistinctAsync(x => x.NombreCallCenter)),
                AreaList = new SelectList(await DistinctAsync(x => x.Area)),
                AreaGestionList = new SelectList(await DistinctAsync(x => x.AreaGestion)),
                ContratoList = new SelectList((await DistinctAsync(x => x.Contrato)).Select(v => v?.ToString())),
                TiposAgenteList = new SelectList(await DistinctAsync(x => x.TiposDeAgente)),
                ServicioList = new SelectList(await DistinctAsync(x => x.Servicio)),
                NombreJefaturaList = new SelectList(await DistinctAsync(x => x.NombreJefatura)),
                MesGestionList = GetMeses()
            };

            return View(model);
        }

        /* ---------------------------- CREATE ------------------------------ */
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            ViewBag.MesGestionList = GetMeses();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonRecord pr)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            await LoadDropdownsAsync();
            ViewBag.MesGestionList = GetMeses();
            return View(pr);
        }

        /* ----------------------------- EDIT ------------------------------- */
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();
            var pr = await _context.TbBaseDotCcrInEx.FindAsync(id);
            if (pr == null) return NotFound();

            await LoadDropdownsAsync();
            ViewBag.MesGestionList = GetMeses();
            return View(pr);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, PersonRecord pr)
        {
            if (id != pr.RutDni) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pr);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) when (!PersonExists(pr.RutDni))
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            await LoadDropdownsAsync();
            ViewBag.MesGestionList = GetMeses();
            return View(pr);
        }

        /* -------------------------- HELPERS ------------------------------- */
        private bool PersonExists(string id) =>
            _context.TbBaseDotCcrInEx.Any(e => e.RutDni == id);

        private async Task LoadDropdownsAsync()
        {
            ViewBag.PaisCallCenterList = new SelectList(await DistinctAsync(x => x.PaisCallCenter));
            ViewBag.NombreCallCenterList = new SelectList(await DistinctAsync(x => x.NombreCallCenter));
            ViewBag.AreaList = new SelectList(await DistinctAsync(x => x.Area));
            ViewBag.AreaGestionList = new SelectList(await DistinctAsync(x => x.AreaGestion));
            ViewBag.ContratoList = new SelectList((await DistinctAsync(x => x.Contrato))
                                                          .Select(v => v?.ToString()));
            ViewBag.TiposAgenteList = new SelectList(await DistinctAsync(x => x.TiposDeAgente));
            ViewBag.ServicioList = new SelectList(await DistinctAsync(x => x.Servicio));
            ViewBag.NombreJefaturaList = new SelectList(await DistinctAsync(x => x.NombreJefatura));
            ViewBag.DVList = new SelectList(await DistinctAsync(x => x.DV));
            ViewBag.ClasificaCargoList = new SelectList(await DistinctAsync(x => x.ClasificaCargo));
            ViewBag.CargoList = new SelectList(await DistinctAsync(x => x.Cargo));
        }

        private async Task<List<T?>> DistinctAsync<T>(Expression<Func<PersonRecord, T?>> selector)
        {
            return await _context.TbBaseDotCcrInEx
                                 .Select(selector)      // IQueryable<T?>
                                 .Distinct()
                                 .OrderBy(x => x)
                                 .ToListAsync();
        }

        private SelectList GetMeses() =>
            new(new[] { "Enero","Febrero","Marzo","Abril","Mayo","Junio",
                        "Julio","Agosto","Septiembre","Octubre","Noviembre","Diciembre" });
    }
}
