﻿namespace Yumify.Service.DTO.Order
{
    public class OrderItemsDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}