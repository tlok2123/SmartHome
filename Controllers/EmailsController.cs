using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Mail.Extend;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartHome.Extend;
using SmartHome.Models.Entity;

namespace SmartHome.Controllers
{
    public class EmailsController : Controller
    {
        private readonly SmartHomeContext _context;
        private readonly ISendMailService _sendmailservice;
        public EmailsController(SmartHomeContext context, ISendMailService sendmailservice = null)
        {
            _context = context;
            _sendmailservice = sendmailservice;
        }

        // GET: Emails
        public async Task<IActionResult> Index()
        {
            return _context.Emails != null ?
                        View(await _context.Emails.ToListAsync()) :
                        Problem("Entity set 'SmartHomeContext.Emails'  is null.");
        }

        // GET: Emails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Emails == null)
            {
                return NotFound();
            }

            var email = await _context.Emails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (email == null)
            {
                return NotFound();
            }

            return View(email);
        }

        // GET: Emails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Emails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Gmail")] Email email)
        {
            if (ModelState.IsValid)
            {
                _context.Add(email);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(email);
        }

        // GET: Emails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Emails == null)
            {
                return NotFound();
            }

            var email = await _context.Emails.FindAsync(id);
            if (email == null)
            {
                return NotFound();
            }
            return View(email);
        }

        // POST: Emails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Gmail")] Email email)
        {
            if (id != email.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(email);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailExists(email.Id))
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
            return View(email);
        }

        // GET: Emails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Emails == null)
            {
                return NotFound();
            }

            var email = await _context.Emails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (email == null)
            {
                return NotFound();
            }

            return View(email);
        }

        // POST: Emails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Emails == null)
            {
                return Problem("Entity set 'SmartHomeContext.Emails'  is null.");
            }
            var email = await _context.Emails.FindAsync(id);
            if (email != null)
            {
                _context.Emails.Remove(email);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> MySendMail()
        {
            var newNotice = new Notice(){
                thongBao = "Thông báo mới từ toà nhà"
            };
            return View(newNotice);

        }
        [HttpPost]
        public async Task<IActionResult> MySendMail(Notice thongBaoMoi)
        {
            var listMail = _context.Emails.ToList();
            foreach (var mail in listMail)
            {
                MailContent mailMontent = new MailContent
                {
                    To = mail.Gmail,
                    Subject = "Thư gửi từ ban quản lý",
                    Body = thongBaoMoi.thongBao
                };
                await _sendmailservice.SendMail(mailMontent);
            }
            return RedirectToAction("Index");
        }


        private bool EmailExists(int id)
        {
            return (_context.Emails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
