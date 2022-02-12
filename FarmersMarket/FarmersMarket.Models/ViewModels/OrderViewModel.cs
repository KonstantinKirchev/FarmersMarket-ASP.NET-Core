namespace FarmersMarket.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using EntityModels;
    using Enums;
    using FarmersMarket.Models.Infrastructure.Mapping;

    public class OrderViewModel : IMapFrom<ShoppingCart>
    {
        public int Id { get; set; }

        public OrderStatus Status { get; set; }

        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 99999.99, ErrorMessage = "Price should be between {1} and {2}.")]
        public decimal TotalPrice { get; set; }

        public DateTime? DateOfOrder { get; set; }

        public DateTime? DateOfDelivery { get; set; }

        public User Owner { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}