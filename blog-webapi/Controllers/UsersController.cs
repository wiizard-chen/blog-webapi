﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using blog.Models;
using blogModel;
using blog_webapi.Utility;

namespace blog_webapi.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        //不需要带方法名

        private readonly BloggingContext _context;

        public UsersController(BloggingContext context) {
            _context = context;
        }

        // GET: Users
        [HttpGet]
        public async Task<IActionResult> Index() {
            var bloggingContext = _context.Users.Include(r => r.Roles);
            var list = await bloggingContext.ToListAsync();
            list.ForEach(r => r.Roles.ForEach(n => n.User = null));
            
            return Json(list);
            //return View();
            //return Json(new { name = "fuck", sex = "man" });
        }

        // GET: Users/Details/5
        [HttpGet("{id?}")]
        public async Task<IActionResult> Details(int? id) {
            if (id == null)
                return JsonStaticMethod.Nothing();
            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
                return JsonStaticMethod.Nothing();
            return Json(user);
        }

        // GET: Users/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Password,UserMail,CreateDate,Active")] User user) {
            if (ModelState.IsValid) {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (user == null) {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password,UserMail,CreateDate,Active")] User user) {
            if (id != user.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!UserExists(user.Id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null) {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id) {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
