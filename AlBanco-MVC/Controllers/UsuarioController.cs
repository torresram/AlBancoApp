
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlBanco_MVC.Models;
using AlBanco_MVC.Data;

public class UsuarioController : Controller
{
    private readonly AlBancoDbContext _context;

    public UsuarioController(AlBancoDbContext context)
    {
        _context = context;
    }

    // GET: USUARIOS
    public async Task<IActionResult> MiPerfil(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Create");
        }

        var usuario = await _context.Usuarios
            .Include(u => u.Cancha)
            .ThenInclude(c => c.Zona)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    // GET: USUARIOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(m => m.Id == id);
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    // GET: USUARIOS/Create
    public async Task<IActionResult> Create()
    {
        await LoadCanchasAsync();
        return View();
    }

    // POST: USUARIOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RegistroUsuarioVM usuario)
    {
        bool whatsappExists = await _context.Usuarios
            .AnyAsync(u => u.WhatsApp == usuario.WhatsApp);

        if(whatsappExists)
        {
            ModelState.AddModelError("WhatsApp", "El número de WhatsApp ya está registrado.");
        }

        if (ModelState.IsValid)
        {
            var nuevoUsuario = new Usuario
            {
                Nombre = usuario.Nombre,
                WhatsApp = usuario.WhatsApp,
                Activo = usuario.Activo,
                FechaAlta = DateTime.Now,
                CanchaId = usuario.CanchaId
            };

            _context.Add(nuevoUsuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MiPerfil), new { id = nuevoUsuario.Id });
        }

        await LoadCanchasAsync(usuario.CanchaId);
        return View(usuario);
    }

    private async Task LoadCanchasAsync(int? selectedCanchaId = null)
    {
        var canchas = await _context.Canchas
            .OrderBy(c => c.Nombre)
            .ToListAsync();

        ViewData["CanchaId"] = new SelectList(canchas, "Id", "Nombre", selectedCanchaId);
    }

    // GET: USUARIOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }
        return View(usuario);
    }

    // POST: USUARIOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nombre,WhatsApp,Activo,FechaAlta,CanchaId,Cancha,ConvocatoriasCreadas,Confirmaciones")] Usuario usuario)
    {
        if (id != usuario.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(usuario.Id))
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
        return View(usuario);
    }

    // GET: USUARIOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(m => m.Id == id);
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    // POST: USUARIOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool UsuarioExists(int? id)
    {
        return _context.Usuarios.Any(e => e.Id == id);
    }
}
