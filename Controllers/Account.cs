using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartHome.Models.Entity;

namespace SmartHome.Controllers.Account
{
    public class Account : BaseController
    {
        private readonly SmartHomeContext _context;

        public Account(SmartHomeContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([Bind("Id,Username,Password")] User userLogin)
        {

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userLogin.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Không tồn tại user");
                return View();
            }
            if (user.Password != userLogin.Password)
            {
                ModelState.AddModelError("", "Sai mật khẩu");
                return View();
            }
            CurrentUser = userLogin.Username;
            return RedirectToAction("index","home");
        }
        [HttpGet]
        public  bool LoginAPI(string name, string pass)
        {

            var user =  _context.Users.FirstOrDefault(u => u.Username == name);
            if (user == null)
            {
                return false;
            }
            if (user.Password != pass)
            {
                return false;
            }
           return true;
        }
        // GET: Account
        public async Task<IActionResult> Index()
        {
            return _context.Users != null ?
                        View(await _context.Users.ToListAsync()) :
                        Problem("Entity set 'SmartHomeContext.Users'  is null.");
        }

        // GET: Account/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password")] User user)
        {
            var newUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if(newUser != null){
                ModelState.AddModelError("","Tài khoản đã tồn tại");
            }
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }

            return View();
        }





        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
