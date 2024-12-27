using System.ComponentModel.DataAnnotations;

namespace odevSon.Models
{
    namespace ProductOrderManagement.Models
    {
        public class Order : BaseEntity
        {
            public int ProductId { get; set; }


            [Range(1, int.MaxValue, ErrorMessage = "Quantity en az 1 olmalıdır.")]
            public int Quantity { get; set; }
            public DateTime OrderDate { get; set; }

            public Product? Product { get; set; }
        }
    }

}
