using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreProject.Models
{
    public class Book
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} En Az {2} En Fazla {1} Karakter Uzunluğunda Olmalıdır.")]
        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz.")]
        [Display(Name="İsim")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz.")]
        [Display(Name = "Tanım")]
        public string Description { get; set; }


        [RegularExpression(@"^[1-9][0-9]{0,3}$",ErrorMessage = "{0} En Az 1 ve 9999 arasında olmalıdır.")]
        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz.")]
        [Display(Name = "Fiyat")]
        public int Price { get; set; }

        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "{0} En Az 1 olmalıdır.")]
        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz.")]
        [Display(Name = "Stok")]
        public int Stock { get; set; }

        [RegularExpression(@"^[1-9][0-9]{3}$", ErrorMessage = "{0}'ı 4 Haneli Olmalıdır. ")]
        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz.")]
        [Display(Name = "Basım Yıl")]
        public int PublishYear { get; set; }

        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Display(Name = "Dil")]
        public int LanguageId { get; set; }
        public Language Language { get; set; }

        [Display(Name = "Yazar")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        [Display(Name = "Eklenme Tarihi")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Aktiflik")]
        public bool Active { get; set; }

        [Display(Name = "Ana Fotograf")]
        public string MainPhoto { get; set; }

        [Display(Name = "İkinci Fotograf")]
        public string SecondPhoto { get; set; }

        [Display(Name = "Üçüncü Fotograf")]
        public string ThirdPhoto { get; set; }

        [NotMapped]
        [DisplayName("Ana Fotograf")]
        public IFormFile MainPhotoFile { get; set; }

        [NotMapped]
        [DisplayName("İkinci Fotograf")]
        public IFormFile SecondPhotoFile { get; set; }

        [NotMapped]
        [DisplayName("Üçüncü Fotograf")]
        public IFormFile ThirdPhotoFile { get; set; }
    }
}
