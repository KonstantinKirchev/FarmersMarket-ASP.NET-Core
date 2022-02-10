namespace FarmersMarket.Models.ViewModels
{
    using FarmersMarket.Models.EntityModels;
    using System.ComponentModel.DataAnnotations;

    public class ShoppingCartViewModel
    {
        public ShoppingCartViewModel()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 99999.99, ErrorMessage = "Price should be between {1} and {2}.")]
        public decimal TotalPrice { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
