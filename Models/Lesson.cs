using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SklStore.Models
{
    public class Lesson
    {
        public long LessonID { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę lekcji.")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Proszę podać opis.")]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "ProduktID")]
        public long ProductID { get; set; }

        [ForeignKey("ProductID")]
        public Product Product { get; set; }
    }
}