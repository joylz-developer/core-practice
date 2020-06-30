using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using core_practice.Database;
using core_practice.Models;
using core_practice.Code;
using System.Globalization;

namespace core_practice.Controllers {
  public class PurchasesController : Controller {
    private readonly BaseContext _context;

    public PurchasesController(BaseContext context) {
      _context = context;
    }

    // GET: Purchases
    public async Task<IActionResult> Index() {
      var baseContext = _context.Purchases.Include(p => p.Product).Include(p => p.User);
      return View(await baseContext.ToListAsync());
    }

    // GET: Purchases/Details/5
    public async Task<IActionResult> Details(int? id) {
      if (id == null) {
        return NotFound();
      }

      var purchase = await _context.Purchases
                .Include(p => p.Product)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
      if (purchase == null) {
        return NotFound();
      }

      return View(purchase);
    }

    // GET: Purchases/Create
    public IActionResult Create() {
      ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
      ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
      ViewData["Status"] = new SelectList(Enum.GetValues(typeof(EStatusPurchase)).Cast<EStatusPurchase>().ToList());
      ViewData["Datatime"] = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-ddTHH:mm");
      return View();
    }

    // POST: Purchases/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Date,Status,UserId,ProductId")] Purchase purchase) {
      if (ModelState.IsValid) {
        _context.Add(purchase);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", purchase.ProductId);
      ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", purchase.UserId);
      ViewData["Status"] = new SelectList(Enum.GetValues(typeof(EStatusPurchase)).Cast<EStatusPurchase>().ToList(), purchase.Status);
      return View(purchase);
    }

    // GET: Purchases/Edit/5
    public async Task<IActionResult> Edit(int? id) {
      if (id == null) {
        return NotFound();
      }

      var purchase = await _context.Purchases.FindAsync(id);
      if (purchase == null) {
        return NotFound();
      }
      ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", purchase.ProductId);
      ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", purchase.UserId);
      ViewData["Status"] = new SelectList(Enum.GetValues(typeof(EStatusPurchase)).Cast<EStatusPurchase>().ToList(), purchase.Status);
      return View(purchase);
    }

    // POST: Purchases/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Status,UserId,ProductId")] Purchase purchase) {
      if (id != purchase.Id) {
        return NotFound();
      }

      if (ModelState.IsValid) {
        try {
          _context.Update(purchase);
          await _context.SaveChangesAsync();
        } catch (DbUpdateConcurrencyException) {
          if (!PurchaseExists(purchase.Id)) {
            return NotFound();
          } else {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", purchase.ProductId);
      ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", purchase.UserId);
      ViewData["Status"] = new SelectList(Enum.GetValues(typeof(EStatusPurchase)).Cast<EStatusPurchase>().ToList(), purchase.Status);
      return View(purchase);
    }

    // GET: Purchases/Delete/5
    public async Task<IActionResult> Delete(int? id) {
      if (id == null) {
        return NotFound();
      }

      var purchase = await _context.Purchases
                .Include(p => p.Product)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
      if (purchase == null) {
        return NotFound();
      }

      return View(purchase);
    }

    // POST: Purchases/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
      var purchase = await _context.Purchases.FindAsync(id);
      _context.Purchases.Remove(purchase);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool PurchaseExists(int id) {
      return _context.Purchases.Any(e => e.Id == id);
    }
  }
}
