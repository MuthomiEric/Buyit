using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Buyit.BOL.DTO;
using Buyit.DAL.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BuyIt.Admin.Models.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;

namespace BuyIt.Admin.Controllers
{


    public class ProductsController : Controller
    {
        private readonly IProductRepository _product;
        private readonly ICategoryRepository _category;
        private readonly IHostingEnvironment _environment;

        public ProductsController(IProductRepository product, ICategoryRepository category, IHostingEnvironment environment)
        {
            _product = product;
            _category = category;
            _environment = environment;
        }

        //GET: Products
        public IActionResult Index()
        {
            var buyitDbContext = _product.AllEntities();
            return View(buyitDbContext);
        }

        //GET: Products/Details/5
        public IActionResult Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _product.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_category.AllCategories(), "CategoryId", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddProductVM prod)
        {

            if (ModelState.IsValid)
            {
                string uniqueFileName = FileCheck(prod);
                Product product = new Product
                {
                    Category = prod.Category,
                    InStock = prod.InStock,

                    ImageUrl = uniqueFileName,

                    CategoryId = prod.CategoryId,
                    IsPreffered = true,
                    LongDescription = prod.Description,
                    Name = prod.ProductName,
                    Price = prod.Price,
                    ProductId = Guid.NewGuid(),


                };
                await _product.AddAsync(product);

                return RedirectToAction(nameof(Index));

            }


            ViewData["CategoryId"] = new SelectList(_category.AllCategories(), "CategoryId", "CategoryName", prod.CategoryId);
            return View(prod);
        }
        // GET: Products/Edit/5
        public IActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _product.GetById(id);

            EditProductVM edit = new EditProductVM
            {
                Id = product.ProductId,
                ProductName = product.Name,
                Description = product.LongDescription,
                Price = product.Price,
                ExistingImage = product.ImageUrl,
                CategoryId = product.CategoryId,
                Category = product.Category,
                InStock = product.InStock,



            };

            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_category.AllCategories(), "CategoryId", "CategoryName", product.Category);
            return View(edit);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, EditProductVM product)
        {

            if (ModelState.IsValid)
            {

                Product prod = new Product
                {
                    Name = product.ProductName,
                    InStock = product.InStock,
                    LongDescription = product.Description,
                    Price = product.Price,


                };


                if (product.Photo != null)
                {
                    if (product.ExistingImage != null)
                    {
                        string filep = Path.Combine(_environment.WebRootPath, "images/", product.ExistingImage);
                        System.IO.File.Delete(filep);
                    }
                    prod.ImageUrl = FileCheck(product);
                }
                else
                {
                    prod.ImageUrl = product.ExistingImage;
                }

                prod.CategoryId = product.CategoryId;
                prod.Category = product.Category;

                _product.UpDate(prod);

                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_category.AllCategories(), "CategoryId", "CategoryId", product.CategoryId);
            return View(product);

        }

        //GET: Products/Delete/5
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _product.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {

            _product.Delete(id);

            return RedirectToAction(nameof(Index));
        }


        private string FileCheck(AddProductVM p)
        {
            string uniqueFileName = null;
            if (p.Photo != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "images/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + p.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder + uniqueFileName);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    p.Photo.CopyTo(filestream);
                }


            }

            return uniqueFileName;
        }

    }
}
