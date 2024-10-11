using System.ComponentModel.DataAnnotations;

namespace Yumify.Service.DTO.Cart
{
    public class CreateUpdateCartDto
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        public List<CartItemsDto> Items { get; set; }= new List<CartItemsDto>();

    }
}
