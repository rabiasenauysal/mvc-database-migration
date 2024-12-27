using System.ComponentModel.DataAnnotations;

namespace odevSon.Models
{
    public class Customer : BaseEntity
    {
        [Required(ErrorMessage = "Name alanı boş olamaz.")]
        [StringLength(50, ErrorMessage = "Name en fazla 50 karakter uzunluğunda olabilir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "email alanı boş olamaz.")]
        public string Email { get; set; }
    }
}
