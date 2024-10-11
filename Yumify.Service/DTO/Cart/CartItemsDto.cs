using System.ComponentModel.DataAnnotations;

namespace Yumify.Service.DTO.Cart
{
    public class CartItemsDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage ="Price must be more than zero.")]
        public decimal Price { get; set; }

        [Required]
        public string PictureUrl { get; set; } = null!;

        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="One quantity must be at least.")]
        public int Quantity { get; set; }

        [Required]
        public string Brand { get; set; } = null!;

        [Required]
        public string Category { get; set; } = null!;
    }
}