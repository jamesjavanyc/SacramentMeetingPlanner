using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SacramentMeetingPlanner.Data;
using SacramentMeetingPlanner.Models;

namespace SacramentMeetingPlanner.Views
{
    public class TalksController : Controller
    {
        private readonly ProgramContext _context;

        public TalksController(ProgramContext context)
        {
            _context = context;
        }

        // GET: Talks
        public async Task<IActionResult> Index()
        {
              return _context.Talk != null ? 
                          View(await _context.Talk.ToListAsync()) :
                          Problem("Entity set 'ProgramContext.Talk'  is null.");
        }

        // GET: Talks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Talk == null)
            {
                return NotFound();
            }

            var talk = await _context.Talk
                .FirstOrDefaultAsync(m => m.Id == id);
            if (talk == null)
            {
                return NotFound();
            }

            return View(talk);
        }

        // GET: Talks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Talks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MeetingId,Speaker,Topic")] Talk talk)
        {
            if (ModelState.IsValid)
            {
                _context.Add(talk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(talk);
        }

        // GET: Talks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Talk == null)
            {
                return NotFound();
            }

            var talk = await _context.Talk.FindAsync(id);
            if (talk == null)
            {
                return NotFound();
            }
            return View(talk);
        }

        // POST: Talks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MeetingId,Speaker,Topic")] Talk talk)
        {
            if (id != talk.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(talk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TalkExists(talk.Id))
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
            return View(talk);
        }

        // GET: Talks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Talk == null)
            {
                return NotFound();
            }

            var talk = await _context.Talk
                .FirstOrDefaultAsync(m => m.Id == id);
            if (talk == null)
            {
                return NotFound();
            }

            return View(talk);
        }

        // POST: Talks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Talk == null)
            {
                return Problem("Entity set 'ProgramContext.Talk'  is null.");
            }
            var talk = await _context.Talk.FindAsync(id);
            if (talk != null)
            {
                _context.Talk.Remove(talk);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TalkExists(int id)
        {
          return (_context.Talk?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
