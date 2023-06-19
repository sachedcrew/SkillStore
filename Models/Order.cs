using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace SklStore.Models
{
    public class Order
    {
        [BindNever]
        public int OrderID { get; set; }
        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [Required(ErrorMessage = "Proszę podać imię i nazwisko.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Proszę podać pierwszy wiersz adresu.")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę miasta.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Proszę kod pocztowy.")]
        public string Zip { get; set; }

        [BindNever]
        public bool Shipped { get; set; }

    }
}
