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
    public class AdminController : Controller
    {
        #region Properties
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constructor
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Index
        /*
         * Admin paneli index sayfasini geri dondurur.
         */
        public IActionResult Index()
        { 
            return View();
        }
        #endregion

        #region OrderList
        /*
         * Bu fonksiyon varolan siparislerin listesini kullanicilarla birlestirerek dondurur.
         * Burada siparisleri, sepetin durumunun KARGO ve aktifliginin false olmasindan anliyoruz.
         */
        public async Task<IActionResult> OrderList()
        {
            List<Basket> basket = await _context.Baskets
                .Where(b => b.Active == false && b.Status == "KARGO")
                .Include(b => b.UserDetails)
                .ToListAsync();

            return View(basket);
        }
        #endregion

        #region BasketDetails
        /*
         * Bu fonksiyon parametre olarak aldigi sepet idsine gore sepetteki urunleri kitap kitap kategorisi ve yazariyla birlestirerek ekrana dondurur.
         */
        public async Task<IActionResult> BasketDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<BasketItem> basketItems = await _context.BasketItems
                .Where(b => b.BasketId == id)
                .Include(b => b.Book)
                .Include(b => b.Book.Category)
                .Include(b => b.Book.Author)
                .ToListAsync();

            return View(basketItems);
        }
        #endregion

        #region ChangeSituation
        /*
         * Bu fonksiyon admin panelinden siparis kargolandi onayi gelince siparisin(sepetin) durumunu TAMAMLANDI yapar.
         */
        public async Task<IActionResult> ChangeSituation(int id)
        {
            Basket basket = _context.Baskets.Where(x => x.Id == id).FirstOrDefault();

            if (basket != null)
            {
                basket.Status = "TAMAMLANDI";
                _context.Update(basket);
                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }

            return RedirectToAction(nameof(OrderList));
        }
        #endregion

    }
}
