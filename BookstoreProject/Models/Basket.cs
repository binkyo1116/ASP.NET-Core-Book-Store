using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreProject.Models
{
    public class Basket
    {
        [Display(Name = "Sipariş Numarası")]
        public int Id { get; set; }

        [Display(Name = "Kullanıcı")]
        [ForeignKey("UserDetails")]
        public string UserDetailsId { get; set; }

        public UserDetails UserDetails { get; set; }

        [Display(Name = "Durum")]
        public string Status { get; set; }

        [Display(Name = "Aktiflik")]
        public bool Active { get; set; }
    }
}
