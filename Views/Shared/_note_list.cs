using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NoteApplication.Models;

namespace NoteApplication.Views.Shared
{
    public class _note_list : Controller
    {
        private readonly NoteContext _context;

        public _note_list(NoteContext context)
        {
            _context = context;
        }

        // GET: _note_list
        public async Task<IActionResult> Index()
        {
              return _context.Notes != null ? 
                          View(await _context.Notes.ToListAsync()) :
                          Problem("Entity set 'NoteContext.Notes'  is null.");
        }

        // GET: _note_list/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Notes == null)
            {
                return NotFound();
            }

            var notes = await _context.Notes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notes == null)
            {
                return NotFound();
            }

            return View(notes);
        }

        // GET: _note_list/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: _note_list/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TextNote,DateNote")] Notes notes)
        {
            if (ModelState.IsValid)
            {
                notes.Id = Guid.NewGuid();
                _context.Add(notes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notes);
        }

        // GET: _note_list/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Notes == null)
            {
                return NotFound();
            }

            var notes = await _context.Notes.FindAsync(id);
            if (notes == null)
            {
                return NotFound();
            }
            return View(notes);
        }

        // POST: _note_list/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,TextNote,DateNote")] Notes notes)
        {
            if (id != notes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotesExists(notes.Id))
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
            return View(notes);
        }

        // GET: _note_list/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Notes == null)
            {
                return NotFound();
            }

            var notes = await _context.Notes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notes == null)
            {
                return NotFound();
            }

            return View(notes);
        }

        // POST: _note_list/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Notes == null)
            {
                return Problem("Entity set 'NoteContext.Notes'  is null.");
            }
            var notes = await _context.Notes.FindAsync(id);
            if (notes != null)
            {
                _context.Notes.Remove(notes);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotesExists(Guid id)
        {
          return (_context.Notes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
