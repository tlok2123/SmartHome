using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartHome.Models.Entity;

namespace SmartHome.Controllers
{
    public class SecurityLogsController : Controller
    {
        private readonly SmartHomeContext _context;

        public SecurityLogsController(SmartHomeContext context)
        {
            _context = context;
        }

        // GET: SecurityLogs
        public async Task<IActionResult> Index()
        {
              return _context.SecurityLogs != null ? 
                          View(await _context.SecurityLogs.Take(20).ToListAsync()) :
                          Problem("Entity set 'SmartHomeContext.SecurityLogs'  is null.");
        }

        // GET: SecurityLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SecurityLogs == null)
            {
                return NotFound();
            }

            var securityLog = await _context.SecurityLogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (securityLog == null)
            {
                return NotFound();
            }

            return View(securityLog);
        }

        // GET: SecurityLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SecurityLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DeviceName,Time,Temperature,Humidity,Water,Gas,Light,State")] SecurityLog securityLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(securityLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(securityLog);
        }

        // GET: SecurityLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SecurityLogs == null)
            {
                return NotFound();
            }

            var securityLog = await _context.SecurityLogs.FindAsync(id);
            if (securityLog == null)
            {
                return NotFound();
            }
            return View(securityLog);
        }

        // POST: SecurityLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DeviceName,Time,Temperature,Humidity,Water,Gas,Light,State")] SecurityLog securityLog)
        {
            if (id != securityLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(securityLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecurityLogExists(securityLog.Id))
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
            return View(securityLog);
        }

        // GET: SecurityLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SecurityLogs == null)
            {
                return NotFound();
            }

            var securityLog = await _context.SecurityLogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (securityLog == null)
            {
                return NotFound();
            }

            return View(securityLog);
        }

        // POST: SecurityLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SecurityLogs == null)
            {
                return Problem("Entity set 'SmartHomeContext.SecurityLogs'  is null.");
            }
            var securityLog = await _context.SecurityLogs.FindAsync(id);
            if (securityLog != null)
            {
                _context.SecurityLogs.Remove(securityLog);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SecurityLogExists(int id)
        {
          return (_context.SecurityLogs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
