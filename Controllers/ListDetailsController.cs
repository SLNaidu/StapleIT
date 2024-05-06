using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StapleIT.DAL;
using StapleIT.Models;

namespace StapleIT.Controllers
{
    public class ListDetailsController : Controller
    {
        private readonly StapleITContext _context;

        public ListDetailsController(StapleITContext context)
        {
            _context = context;
        }

        // GET: ListDetails
        public async Task<IActionResult> Index()
        {
              return View(await _context.ListDetail.ToListAsync());
        }

        // GET: ListDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ListDetail == null)
            {
                return NotFound();
            }

            var listDetail = await _context.ListDetail
                .FirstOrDefaultAsync(m => m.ListDetailId == id);
            if (listDetail == null)
            {
                return NotFound();
            }

            return View(listDetail);
        }

        // GET: ListDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ListDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListDetailId,Quantity,ListId,ItemId")] ListDetail listDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(listDetail);
        }

        // GET: ListDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ListDetail == null)
            {
                return NotFound();
            }

            var listDetail = await _context.ListDetail.FindAsync(id);
            if (listDetail == null)
            {
                return NotFound();
            }
            return View(listDetail);
        }

        // POST: ListDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ListDetailId,Quantity,ListId,ItemId")] ListDetail listDetail)
        {
            if (id != listDetail.ListDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListDetailExists(listDetail.ListDetailId))
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
            return View(listDetail);
        }

        // GET: ListDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ListDetail == null)
            {
                return NotFound();
            }

            var listDetail = await _context.ListDetail
                .FirstOrDefaultAsync(m => m.ListDetailId == id);
            if (listDetail == null)
            {
                return NotFound();
            }

            return View(listDetail);
        }

        // POST: ListDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ListDetail == null)
            {
                return Problem("Entity set 'StapleITContext.ListDetail'  is null.");
            }
            var listDetail = await _context.ListDetail.FindAsync(id);
            if (listDetail != null)
            {
                _context.ListDetail.Remove(listDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListDetailExists(int id)
        {
          return _context.ListDetail.Any(e => e.ListDetailId == id);
        }


    }
}
