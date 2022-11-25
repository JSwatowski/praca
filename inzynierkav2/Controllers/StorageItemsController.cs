using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using inzynierka.Models;
using pracainz.Data;

namespace inzynierkav2.Controllers
{
    public class StorageItemsController : Controller
    {
        private readonly AppDbContext _context;

        public StorageItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: StorageItems
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.StorageItems.Include(s => s.Movie).Include(s => s.Storage);
            return View(await appDbContext.ToListAsync());
        }

        // GET: StorageItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storageItem = await _context.StorageItems
                .Include(s => s.Movie)
                .Include(s => s.Storage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storageItem == null)
            {
                return NotFound();
            }

            return View(storageItem);
        }

        // GET: StorageItems/Create
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id");
            ViewData["StorageId"] = new SelectList(_context.Storages, "Id", "Id");
            return View();
        }

        // POST: StorageItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieId,StorageId")] StorageItem storageItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storageItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", storageItem.MovieId);
            ViewData["StorageId"] = new SelectList(_context.Storages, "Id", "Id", storageItem.StorageId);
            return View(storageItem);
        }

        // GET: StorageItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storageItem = await _context.StorageItems.FindAsync(id);
            if (storageItem == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", storageItem.MovieId);
            ViewData["StorageId"] = new SelectList(_context.Storages, "Id", "Id", storageItem.StorageId);
            return View(storageItem);
        }

        // POST: StorageItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,StorageId")] StorageItem storageItem)
        {
            if (id != storageItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storageItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StorageItemExists(storageItem.Id))
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
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", storageItem.MovieId);
            ViewData["StorageId"] = new SelectList(_context.Storages, "Id", "Id", storageItem.StorageId);
            return View(storageItem);
        }

        // GET: StorageItems/Delete/5
        /*
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storageItem = await _context.StorageItems
                .Include(s => s.Movie)
                .Include(s => s.Storage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storageItem == null)
            {
                return NotFound();
            }

            return View(storageItem);
        }*/

        // POST: StorageItems/Delete/5

        public async Task<IActionResult> Delete(int id)
        {
            var storageItem = await _context.StorageItems.FindAsync(id);
            _context.StorageItems.Remove(storageItem);

            var storage = await _context.Storages.FindAsync(storageItem.StorageId);
            _context.Storages.Remove(storage);
            await _context.SaveChangesAsync();
            return RedirectToAction("StorageView", "Storages");
        }

        private bool StorageItemExists(int id)
        {
            return _context.StorageItems.Any(e => e.Id == id);
        }
    }
}
