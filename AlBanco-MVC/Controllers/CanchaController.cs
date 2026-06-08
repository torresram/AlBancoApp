
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlBanco_MVC.Models;
using AlBanco_MVC.Data;

public class CanchaController : Controller
{
    private readonly AlBancoDbContext _context;

    public CanchaController(AlBancoDbContext context)
    {
        _context = context;
    }

    // GET: CANCHAS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Canchas.ToListAsync());
    }

    // GET: CANCHAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cancha = await _context.Canchas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (cancha == null)
        {
            return NotFound();
        }

        return View(cancha);
    }

    // GET: CANCHAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CANCHAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Direccion,ZonaId,Zona,CodigoQR,Usuarios,Convocatorias")] Cancha cancha)
    {
        if (ModelState.IsValid)
        {
            _context.Add(cancha);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(cancha);
    }

    // GET: CANCHAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cancha = await _context.Canchas.FindAsync(id);
        if (cancha == null)
        {
            return NotFound();
        }
        return View(cancha);
    }

    // POST: CANCHAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nombre,Direccion,ZonaId,Zona,CodigoQR,Usuarios,Convocatorias")] Cancha cancha)
    {
        if (id != cancha.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(cancha);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CanchaExists(cancha.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(cancha);
    }

    // GET: CANCHAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cancha = await _context.Canchas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (cancha == null)
        {
            return NotFound();
        }

        return View(cancha);
    }

    // POST: CANCHAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var cancha = await _context.Canchas.FindAsync(id);
        if (cancha != null)
        {
            _context.Canchas.Remove(cancha);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CanchaExists(int? id)
    {
        return _context.Canchas.Any(e => e.Id == id);
    }
}
