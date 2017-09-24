using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using blog.Models;
using blogModel;

namespace blog_webapi.Controllers
{
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        private readonly BloggingContext _context;

        public RolesController(BloggingContext context) {
            _context = context;
        }

        // GET: Roles
        [HttpGet]
        public async Task<IActionResult> Index() {
            var bloggingContext = _context.Roles.Include(r => r.User);
            var list = await bloggingContext.ToListAsync();
            list.ForEach(r => r.User.Roles = null);
            JsonResult jsonResult = new JsonResult(bloggingContext.ToList());
            return jsonResult;
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var role = await _context.Roles
                .Include(r => r.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (role == null) {
                return NotFound();
            }

            return View(role);
        }

        // GET: Roles/Create
        public IActionResult Create() {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoleName,UserId")] Role role) {
            if (ModelState.IsValid) {
                _context.Add(role);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", role.UserId);
            return View(role);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var role = await _context.Roles.SingleOrDefaultAsync(m => m.Id == id);
            if (role == null) {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", role.UserId);
            return View(role);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoleName,UserId")] Role role) {
            if (id != role.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(role);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!RoleExists(role.Id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", role.UserId);
            return View(role);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var role = await _context.Roles
                .Include(r => r.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (role == null) {
                return NotFound();
            }

            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var role = await _context.Roles.SingleOrDefaultAsync(m => m.Id == id);
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(int id) {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
