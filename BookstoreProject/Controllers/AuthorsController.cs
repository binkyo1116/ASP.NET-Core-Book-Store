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
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace BookstoreProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AuthorsController : Controller
    {
        #region Properties
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constructor
        public AuthorsController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Index
        /*
         * Yazarlar listesini veritabanindan ceker ve geri dondurur.
         */
        public async Task<IActionResult> Index()
        {
            return View(await _context.Authors.ToListAsync());
        }
        #endregion

        #region Details
        /*
         * Details fonksiyonu verilen yazar idsine gore yazari bulur ve geri dondurur
         */
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }
        #endregion

        #region Create GET
        /*
         * Create fonksiyonu yazar olusturmak icin cagrildiginda Create.cshtml viewini dondurur.
         */
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region Create POST
        /*
         * Create fonksiyonu parametre olarak alinan modele gore yazari veritabanina ekler.
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Active")] Author author)
        {
            if (ModelState.IsValid)
            {
                author.Active = true;
                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }
        #endregion

        #region Edit GET
        /*
         * Yazar idsine gore yazar nesnesini veritabanindan bulur ve geri dondurur.
         */
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }
        #endregion

        #region Edit POST
        /*
         * Edit fonksiyonu disaridan aldigi yazar modeline gore yazari gunceller.
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Active")] Author author)
        {
            
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Id))
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
            return View(author);
        }
        #endregion

        #region Delete GET
        /*
         * Bu fonksiyon parametre olarak alinan ide gore yazari bulur ve delete sayfasi dondurur.
         */
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }
        #endregion

        #region Delete POST
        /*
         * DeleteConfirmed fonksiyonu parametre olarak aldigi id bilgisine gore yazarin aktiflik durumunu aktifse pasif pasifse aktif yapar.
         */
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Author author = _context.Authors.Where(x => x.Id == id).FirstOrDefault();

            if (author != null)
            {
                author.Active = !author.Active;
                _context.Update(author);
                await _context.SaveChangesAsync();

            }
            else
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region AuthorExists
        /*
         * bu fonksiyon yazarin varligini kontrol eder
         */
        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
        #endregion
    }
}
