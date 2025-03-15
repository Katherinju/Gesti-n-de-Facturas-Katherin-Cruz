using Microsoft.AspNetCore.Mvc;
using GestionFacturas.Models;
using System.Linq;

namespace GestionFacturas.Controllers
{
    public class FacturasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacturasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción para listar todas las facturas
        public IActionResult Index()
        {
            var facturas = _context.Facturas.ToList();
            return View(facturas);
        }

        // Acción para mostrar el formulario de creación de una factura
        // GET: Facturas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Facturas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Factura factura)
        {
            if (ModelState.IsValid)
            {
                _context.Facturas.Add(factura);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(factura);
        }


        // Acción para mostrar el formulario de edición de una factura
        // GET: Facturas/Edit/5
        public IActionResult Edit(int id)
        {
            var factura = _context.Facturas.Find(id);
            if (factura == null)
            {
                return NotFound();
            }
            return View(factura);
        }

        // POST: Facturas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Factura factura)
        {
            if (id != factura.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(factura);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "La factura se ha actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(factura);
        } 
        
        //Accion para obtener los detalles de una factura
        
        // GET: Facturas/Details/5
        public IActionResult Details(int id)
        {
            var factura = _context.Facturas.Find(id);
            if (factura == null)
            {
                return NotFound();
            }
            return View(factura);
        }

        // Acción para eliminar una factura
        // GET: Facturas/Delete/5
        public IActionResult Delete(int id)
        {
            var factura = _context.Facturas.Find(id);
            if (factura == null)
            {
                return NotFound();
            }
            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var factura = _context.Facturas.Find(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "La factura se ha eliminado correctamente.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}