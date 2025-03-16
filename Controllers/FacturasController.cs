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
        public IActionResult Create(Factura model)
        {
            if (ModelState.IsValid)
            {
                // Si el usuario no ingresó una fecha, asigna una por defecto
                if (model.FechaVencimiento == default(DateTime))
                {
                    model.FechaVencimiento = DateTime.Now.AddDays(30); // Ejemplo: 30 días después de hoy
                }

                _context.Facturas.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // Acción para mostrar el formulario de edición de una factura
        public IActionResult Edit(Factura factura)
        {
            Console.WriteLine($"Factura ID: {factura.Id}, NúmeroFactura: {factura.NumeroFactura}, Fecha: {factura.FechaVencimiento}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState tiene errores:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"  - {error.ErrorMessage}");
                }
                return View(factura);
            }

            var facturaExistente = _context.Facturas.Find(factura.Id);
            if (facturaExistente == null)
            {
                return NotFound();
            }

            facturaExistente.NumeroFactura = factura.NumeroFactura;
            facturaExistente.FechaVencimiento = factura.FechaVencimiento;

            _context.SaveChanges();
            TempData["SuccessMessage"] = "Factura actualizada correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // Acción para mostrar los detalles de una factura
        public IActionResult Details(int id)
        {
            var factura = _context.Facturas.Find(id);
            if (factura == null)
            {
                return NotFound();
            }
            return View(factura);
        }

        // Acción para mostrar la confirmación de eliminación de una factura
        public IActionResult Delete(int id)
        {
            var factura = _context.Facturas.Find(id);
            if (factura == null)
            {
                return NotFound();
            }
            return View(factura);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Console.WriteLine($"ID recibido: {id}"); // <-- Agrega esto para depuración

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