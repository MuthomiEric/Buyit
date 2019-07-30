using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Buyit.BOL.DTO;
using Buyit.DAL.Services.Interfaces;
using System.Linq;

namespace Buyit.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _context;

        public CategoriesController(ICategoryRepository context)
        {
            _context = context;
        }

        // GET: Categories
        public IActionResult Index()
        {
            var items = _context.AllCategories();

            return View(items);
        }

        // GET: Categories/Details/5
        //public async Task<IActionResult> Details(Guid id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var category = await _context.GetByIdAsync(id);

        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(category);
        //}

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.CategoryId = Guid.NewGuid();
                _context.AddAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public IActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _context.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.UpDate(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        //public IActionResult Delete(Guid id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var category = _context.GetByIdAsync(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(category);
        //}

        //// POST: Categories/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteConfirmed(Guid id)
        //{

        //    _context.Delete(id);

        //    return RedirectToAction(nameof(Index));
        //}

        private bool CategoryExists(Guid id)
        {
            return _context.AllCategories().Any(e => e.CategoryId == id);
        }
    }
}
