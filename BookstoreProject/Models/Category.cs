using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreProject.Models
{
    public class Category
    {
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z_ğüşıöçĞÜŞİÖÇ ]+$", ErrorMessage = "Sadece Harf Giriniz.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} En Az {2} En Fazla {1} Karakter Uzunluğunda Olmalıdır.")]
        [Required(ErrorMessage = "{0} Alan Boş Bırakılamaz")]
        [Display(Name = "İsim")]
        public string Name { get; set; }

        [Display(Name = "Aktiflik")]
        public bool Active { get; set; }
    }
}
