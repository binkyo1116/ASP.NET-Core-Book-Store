using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookstoreProject.Data;
using BookstoreProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookstoreProject.Controllers
{
    // Sadece Admin rolu tanimli olan kullanicilarin bu controllerda islem yapabilmesi icin eklenmistir.
    [Authorize(Roles = "Admin")]
    public class LanguagesController : Controller
    {
        #region Properties
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constructor
        public LanguagesController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Index
        /*
         * Bu fonksiyon veritabanında bulunan dillerin hepsini liste halinde viewa model olarak aktarir.
         */
        public async Task<IActionResult> Index()
        {
            return View(await _context.Languages.ToListAsync());
        }
        #endregion

        #region Details
        /*
         * Details fonksiyonu parametre olarak aldigi dil idsine gore dili veritabanından alir ve model olarak dondurur. 
         */
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = await _context.Languages
                .FirstOrDefaultAsync(m => m.Id == id);

            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }
        #endregion

        #region Create GET
        /*
         * Languages > Create.cshtml viewini dondurur.
         */
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region Create POST
        /*
         * Parametre olarak aldigi dil modelini veritabanına yeni kayit olarak kaydeder.
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Active")] Language language)
        {
            if (ModelState.IsValid)
            {
                language.Active = true;
                _context.Add(language);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(language);
        }
        #endregion

        #region Edit GET
        /*
         * Disaridan alinan dil idsine gore veritabanindaki dili bulur ve viewa model olarak aktarir. 
         */
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = await _context.Languages.FindAsync(id);

            if (language == null)
            {
                return NotFound();
            }
            return View(language);
        }
        #endregion

        #region  Edit POST
        /*
         * Disaridan alinan dil modeline gore dili gunceller ve guncellenen dili viewa model olarak aktarir.
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Active")] Language language)
        {
            if (id != language.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(language);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LanguageExists(language.Id))
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
            return View(language);
        }
        #endregion

        #region Delete GET
        /*
         * Delete fonksiyonu aldigi dil idsine gore dili viewa aktarip dondurur.
         */
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = await _context.Languages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }
        #endregion

        #region Delete POST
        /*
         * Delete fonksiyonu aldigi dil id'sine gore dilin o anki aktiflik durumunu degistirir.
         */
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Language language = _context.Languages.Where(x => x.Id == id).FirstOrDefault();

            if (language != null)
            {
                language.Active = !language.Active;
                _context.Update(language);
                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region LanguageExists 
        /*
         * Dilin varligini kontrol etmek amaciyla yazilmis fonksiyondur.
         */
        private bool LanguageExists(int id)
        {
            return _context.Languages.Any(e => e.Id == id);
        }
        #endregion
    }
}
