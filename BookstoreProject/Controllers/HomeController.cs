using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookstoreProject.Models;
using Microsoft.AspNetCore.Authorization;
using BookstoreProject.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace BookstoreProject.Controllers
{
    public class HomeController : Controller
    {
        #region Properties
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserDetails> _userManager;
        private readonly IStringLocalizer<HomeController> _localizer;
        #endregion

        #region Constructor
        public HomeController(ApplicationDbContext context, UserManager<UserDetails> userManager, IStringLocalizer<HomeController> localizer)
        {
            _context = context;
            _userManager = userManager;
            _localizer = localizer;
        }
        #endregion

        #region Index
        /*
         * Index fonksiyonu eklenme tarihini gore yeni olan kitaplarin ilk 10 tanesini alir.
         */
        public async Task<IActionResult> Index()
        {
            List<Book> _kitaplar = await _context.Books.OrderByDescending(x => x.CreateDate).Take(10).ToListAsync();
            return View(_kitaplar);
        }
        #endregion

        #region BookDetails
        /*
         * BookDetails fonksiyonu aldigi id parametresine gore kitabı bulur.
         * Ayni zamanda rastgele olarak kitaplar arasindan 4 tane kitap secer
         * Tum bu kitaplari viewa aktarip geri doner.
         */
        public async Task<IActionResult> BookDetails(int searchId)
        {
            List<Book> kitaplar = await _context.Books.OrderBy(u => Guid.NewGuid()).Where(u => u.Id != searchId).Take(4).ToListAsync();

            Book _kitap = _context.Books.Where(x => x.Id == searchId).FirstOrDefault();
            kitaplar.Add(_kitap);
            if (_kitap == null)
                return RedirectToAction("ErrorPage", "Home", new { statusCode = 901 });
            else
                return View(kitaplar);
        }
        #endregion

        #region SearchBooks
        /*
         * SearchBooks fonksiyonu gelen arama parametresine gore uyusan kitaplari bulur ve geri dondurur.
         */
        public async Task<IActionResult> SearchBooks(string searchItem)
        {
            List<Book> kitaplar = await _context.Books.Where(x => x.Name.ToLower().Contains(searchItem.ToLower())).ToListAsync();
            return View(kitaplar);
        }
        #endregion

        #region AddBookToBasket
        /*
         * Bu fonksiyon parametre olarak alinan kitabi kullanicinin sepetine ekler.
         * Kullaniciya ait aktif bir sepet yoksa once sepet olusturur sonra kitabı sepete ekler.
         * Kullaniciya ait bir sepet varsa o sepete kitap eklenir.
         */
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddBookToBasket(int bookId)
        {
            var basket = await _context.Baskets
                .Where(x => x.UserDetailsId == User.FindFirstValue(ClaimTypes.NameIdentifier) && x.Active == true)
                .FirstOrDefaultAsync();

            Book currentBook = await _context.Books
                .Where(x => x.Id == bookId && x.Active == true)
                .FirstOrDefaultAsync();

            if (currentBook == null) return NotFound();

            if (basket != null)
            {
                BasketItem newBasketItem = new BasketItem
                {
                    BasketId = basket.Id,
                    Book = currentBook,
                    Active = true
                };

                _context.Add(newBasketItem);
                await _context.SaveChangesAsync();
            }
            else
            {
                Basket newBasket = new Basket
                {
                    Status = "YENI",
                    Active = true,
                    UserDetails = await _userManager.GetUserAsync(User)
                };

                _context.Add(newBasket);
                _context.SaveChanges();

                BasketItem newBasketItem = new BasketItem
                {
                    BasketId = newBasket.Id,
                    Book = currentBook,
                    Active = true
                };

                _context.Add(newBasketItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Baskets");
        }
        #endregion

        #region MyOrders
        /*
         * Kullaniciya ait sepeti dondurur.
         */
        [Authorize]
        public async Task<IActionResult> MyOrder()
        {
            List<Basket> baskets = await _context.Baskets
                .Where(x => x.UserDetailsId == User.FindFirstValue(ClaimTypes.NameIdentifier) && x.Active == false)
                .ToListAsync();

            return View(baskets);
        }
        #endregion

        #region CalcelOrder
        /*
         * Siparislerim sayfasinda verilen siparisi iptal etmek icin bu fonksiyon cagrilir.
         * Cagrildiginda siparis idsine gore sepetin statusunu IPTAL e ceker.
         */
        [Authorize]
        public async Task<IActionResult> CancelOrder(int? orderId)
        {
            if (orderId == null)
            {
                return NotFound();
            }

            Basket basket = await _context.Baskets
                .Where(x => x.Id == orderId)
                .FirstOrDefaultAsync();

            if (basket == null)
            {
                TempData["HataMesaj"] = _localizer["ErrorMessage3"];
            }
            else
            {
                basket.Status = "IPTAL";
                basket.Active = false;
                _context.Update(basket);
                await _context.SaveChangesAsync();
            }

            List<Basket> basketList = await _context.Baskets
                .Where(x => x.UserDetailsId == User.FindFirstValue(ClaimTypes.NameIdentifier) && x.Active == false)
                .ToListAsync();

            return View("MyOrder", basketList);
        }
        #endregion

        #region OrderDetails
        /*
         * Sepet sayfasinda sepetin detaylarini dondurmek icin cagrilir.
         * Sepet idsi ile sepetin icindeki itemlari bulur ve geri dondurur.
         */
        [Authorize]
        public async Task<IActionResult> OrderDetails(int? orderId)
        {
            if (orderId == null)
            {
                return NotFound();
            }

            List<BasketItem> basketItems = await _context.BasketItems
                .Where(b => b.BasketId == orderId && b.Active == true)
                .Include(b => b.Book)
                .Include(b => b.Book.Category)
                .Include(b => b.Book.Author)
                .ToListAsync();

            return View(basketItems);
        }
        #endregion

        #region LangSetting
        /*
         * Dil seciminde bu fonksiyon cagrilir.
         * Secilen dili 10 gunlugune cookieye kaydeder.
         */
        [HttpPost]
        public IActionResult LangSetting(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(10) }
                );
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region ErrorPage with statusCode
        /*
         * ErrorPage fonksiyonu gelen statusCode una gore hata mesaji doldurup sayfayi geri dondurur
         */
        [Route("Home/ErrorPage/{statusCode}")]
        public IActionResult ErrorPage(int statusCode)
        {
            switch (statusCode)
            {
                case 901:
                    ViewBag.ErrorMessage = _localizer["ErrorMessage1"];
                    break;
                default:
                    ViewBag.ErrorMessage = _localizer["ErrorMessage2"];
                    break;
            }
            return View();
        }
        #endregion

        #region ErrorPage with ReturnUrl
        /*
         * Bu fonksiyon accessdenied hatasinda dondurulur gitmek istedigi sayfa bulunmadi hatasi doner.
         */
        public IActionResult ErrorPage(string ReturnUrl)
        {
            ViewBag.ErrorMessage = _localizer["ErrorMessage2"] + ": " + ReturnUrl;
            return View();
        }
        #endregion

        #region Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}
