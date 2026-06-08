
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlBanco_MVC.Models;
using AlBanco_MVC.Data;

public class ConvocatoriaController : Controller
{
    private readonly AlBancoDbContext _context;

    public ConvocatoriaController(AlBancoDbContext context)
    {
        _context = context;
    }

    // GET: CONVOCATORIAS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Convocatorias.ToListAsync());
    }

    // GET: CONVOCATORIAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var convocatoria = await _context.Convocatorias
            .FirstOrDefaultAsync(m => m.Id == id);
        if (convocatoria == null)
        {
            return NotFound();
        }

        return View(convocatoria);
    }

    // GET: CONVOCATORIAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CONVOCATORIAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,FechaPartido,HoraPartido,JugadoresNecesarios,PrecioPorJugador,Observaciones,Estado,FechaCreacion,CanchaId,Cancha,UsuarioId,Usuario,Confirmaciones")] Convocatoria convocatoria)
    {
        if (ModelState.IsValid)
        {
            _context.Add(convocatoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(convocatoria);
    }

    // GET: CONVOCATORIAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var convocatoria = await _context.Convocatorias.FindAsync(id);
        if (convocatoria == null)
        {
            return NotFound();
        }
        return View(convocatoria);
    }

    // POST: CONVOCATORIAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,FechaPartido,HoraPartido,JugadoresNecesarios,PrecioPorJugador,Observaciones,Estado,FechaCreacion,CanchaId,Cancha,UsuarioId,Usuario,Confirmaciones")] Convocatoria convocatoria)
    {
        if (id != convocatoria.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(convocatoria);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConvocatoriaExists(convocatoria.Id))
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
        return View(convocatoria);
    }

    // GET: CONVOCATORIAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var convocatoria = await _context.Convocatorias
            .FirstOrDefaultAsync(m => m.Id == id);
        if (convocatoria == null)
        {
            return NotFound();
        }

        return View(convocatoria);
    }

    // POST: CONVOCATORIAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var convocatoria = await _context.Convocatorias.FindAsync(id);
        if (convocatoria != null)
        {
            _context.Convocatorias.Remove(convocatoria);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ConvocatoriaExists(int? id)
    {
        return _context.Convocatorias.Any(e => e.Id == id);
    }
}
