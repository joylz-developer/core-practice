using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using core_practice.Database;
using core_practice.Models;

namespace core_practice.Controllers {
  public class CategoryProductsController : Controller {
    private readonly BaseContext _context;

    public CategoryProductsController(BaseContext context) {
      _context = context;
    }

    // GET: CategoryProducts
    public async Task<IActionResult> Index() {
      return View(await _context.CategoryProducts.ToListAsync());
    }

    // GET: CategoryProducts/Details/5
    public async Task<IActionResult> Details(int? id) {
      if (id == null) {
        return NotFound();
      }

      var categoryProduct = await _context.CategoryProducts
                .FirstOrDefaultAsync(m => m.Id == id);
      if (categoryProduct == null) {
        return NotFound();
      }

      return View(categoryProduct);
    }

    // GET: CategoryProducts/Create
    public IActionResult Create() {
      return View();
    }

    // POST: CategoryProducts/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] CategoryProduct categoryProduct) {
      if (ModelState.IsValid) {
        var resAdd = _context.Add(categoryProduct);
        var resSave = await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(categoryProduct);
    }

    // GET: CategoryProducts/Edit/5
    public async Task<IActionResult> Edit(int? id) {
      if (id == null) {
        return NotFound();
      }

      var categoryProduct = await _context.CategoryProducts.FindAsync(id);
      if (categoryProduct == null) {
        return NotFound();
      }
      return View(categoryProduct);
    }

    // POST: CategoryProducts/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CategoryProduct categoryProduct) {
      if (id != categoryProduct.Id) {
        return NotFound();
      }

      if (ModelState.IsValid) {
        try {
          _context.Update(categoryProduct);
          await _context.SaveChangesAsync();
        } catch (DbUpdateConcurrencyException) {
          if (!CategoryProductExists(categoryProduct.Id)) {
            return NotFound();
          } else {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(categoryProduct);
    }

    // GET: CategoryProducts/Delete/5
    public async Task<IActionResult> Delete(int? id) {
      if (id == null) {
        return NotFound();
      }

      var categoryProduct = await _context.CategoryProducts
                .FirstOrDefaultAsync(m => m.Id == id);
      if (categoryProduct == null) {
        return NotFound();
      }

      return View(categoryProduct);
    }

    // POST: CategoryProducts/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
      var categoryProduct = await _context.CategoryProducts.FindAsync(id);
      _context.CategoryProducts.Remove(categoryProduct);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool CategoryProductExists(int id) {
      return _context.CategoryProducts.Any(e => e.Id == id);
    }
  }
}
