
using System.ComponentModel.DataAnnotations;


namespace Kinosal.Models
{
    public class Kino
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? Genre { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? Director { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [StringLength(250, MinimumLength = 20, ErrorMessage = "Длина строки должна быть от 20 до 500 символов")]
        public string? Description { get; set; }

       
        public string? Poster { get; set; }

    }
}
