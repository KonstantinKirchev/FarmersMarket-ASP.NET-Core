namespace FarmersMarket.Models.ViewModels
{
    using FarmersMarket.Models.EntityModels;

    public class ShoppingCartProductViewModel
    {
        public int ShoppingCartId { get; set; }

        public int ProductId { get; set; }

        public ShoppingCart? ShoppingCart { get; set; }

        public Product? Product { get; set; }

        public int Units { get; set; }
    }
}
