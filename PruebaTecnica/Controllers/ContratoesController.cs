using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Context;
using PruebaTecnica.Interfaces;
using PruebaTecnica.Models;

namespace PruebaTecnica.Controllers
{
    public class ContratoesController : Controller
    {
        private readonly IContrato _contrato;

        public ContratoesController(IContrato contrato)
        {
            _contrato = contrato;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _contrato.GetAllContratosAsync();
            return View(list);
        }

        // GET: Contratoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var contrato = await _contrato.ObtenerPorcentajePagoAsync(id.Value);
            if (contrato is null) return NotFound();

            return View(contrato);
        }

        // GET: Contratoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contratoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContratoId,Cliente,TotalCuotas")] Contrato contrato)
        {
            if (!ModelState.IsValid) return View(contrato);

            await _contrato.CreateAsync(contrato);
            return RedirectToAction(nameof(Index));
        }

        // GET: Contratoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();

            var contrato = await _contrato.GetByIdAsync(id.Value);
            if (contrato is null) return NotFound();

            return View(contrato);
        }

        // POST: Contratoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContratoId,Cliente,TotalCuotas")] Contrato contrato)
        {
            if (id != contrato.ContratoId) return NotFound();
            if (!ModelState.IsValid) return View(contrato);

            var ok = await _contrato.UpdateAsync(contrato);
            if (!ok) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // GET: Contratoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            var contrato = await _contrato.GetByIdAsync(id.Value);
            if (contrato is null) return NotFound();

            return View(contrato);
        }

        // POST: Contratoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _contrato.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
