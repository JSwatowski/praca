using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inzynierka.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pracainz.Data;
using pracainz.Models;

namespace inzynierka.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _service;

        public CategoriesController(ICategoriesService service)
        {
            _service = service;
        }

     
        public async Task <IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name")] Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            await _service.AddAsync(category);
            return RedirectToAction(nameof(Index));
        }

        //GET: Categories/Details/1

        public async Task<IActionResult> Details(int id)
        {
            var categoryDeatails = await _service.GetByIdAsync(id);

            if (categoryDeatails == null) return View("NotFound");
            return View(categoryDeatails);
        }

        //Cagtegory/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var categoryDeatails = await _service.GetByIdAsync(id);

            if (categoryDeatails == null) return View("NotFound");
            return View(categoryDeatails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            await _service.UpdateAsync(id,category);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var categoryDeatails = await _service.GetByIdAsync(id);

            if (categoryDeatails == null) return View("NotFound");
            return View(categoryDeatails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryDetails = await _service.GetByIdAsync(id);
            if (categoryDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
          
            return RedirectToAction(nameof(Index));
        }
    }
}
