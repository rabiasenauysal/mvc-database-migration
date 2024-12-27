using System.ComponentModel.DataAnnotations;

namespace odevSon.Models
{
    public class Product : BaseEntity
    {
        [Required(ErrorMessage = "Name alanı boş olamaz.")]
        [StringLength(50, ErrorMessage = "Name en fazla 50 karakter uzunluğunda olabilir.")]
        public string Name { get; set; }


        [Range(0, double.MaxValue, ErrorMessage = "Price sıfırdan küçük olamaz.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock alanı boş olamaz.")]
        public int Stock { get; set; }
    }
}
