using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookstoreProject.Data;
using BookstoreProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookstoreProject.Controllers
{
    // Sadece Admin rolu tanimli olan kullanicilarin bu controllerda islem yapabilmesi icin eklenmistir.
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        #region Properties
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constructor
        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Index
        /*
         * Kategori listesini veritabanindan ceker ve geri dondurur.
         */
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }
        #endregion

        #region Details
        /*
         * Details fonksiyonu verilen kategori idsine gore kategoriyi bulur ve geri dondurur
         */
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        #endregion

        #region Create GET
        /*
         * Create fonksiyonu kategori olusturmak icin cagrildiginda Create.cshtml viewini dondurur.
         */
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region Create POST
        /*
         * Create fonksiyonu parametre olarak alinan modele gore kategoriyi veritabanina ekler.
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Active")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.Active = true;
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        #endregion

        #region Edit GET
        /*
         * Kategori idsine gore kategori nesnesini veritabanindan bulur ve geri dondurur.
         */
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        #endregion

        #region Edit POST
        /*
         * Edit fonksiyonu disaridan aldigi kategori modeline gore kategoriyi gunceller.
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Active")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
        #endregion

        #region Delete GET
        /*
         * Bu fonksiyon parametre olarak alinan ide gore kategoriyi bulur ve delete sayfasi dondurur.
         */
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        #endregion

        #region Delete POST
        /*
         * DeleteConfirmed fonksiyonu parametre olarak aldigi id bilgisine gore kategorinin aktiflik durumunu aktifse pasif pasifse aktif yapar.
         */
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Category category = _context.Categories.Where(x => x.Id == id).FirstOrDefault();

            if (category != null)
            {
                category.Active = !category.Active;
                _context.Update(category);
                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region CategoryExists
        /*
         * bu fonksiyon kategorinin varligini kontrol eder
         */
        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
        #endregion
    }
}
