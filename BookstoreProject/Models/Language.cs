using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreProject.Models
{
    public class Language
    {
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z_ğüşıöçĞÜŞİÖÇ ]+$", ErrorMessage = "Sadece Harf Giriniz.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Minimum {2} Karakter Maximum {1} Karakter Girilebilir.")]
        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz")]
        [Display(Name = "İsim")]
        public string Name { get; set; }

        [Display(Name = "Aktiflik")]
        public bool Active { get; set; }
    }
}
