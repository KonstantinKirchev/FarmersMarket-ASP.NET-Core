namespace FarmersMarket.Models.ViewModels
{
    using FarmersMarket.Models.EntityModels;
    using FarmersMarket.Models.Infrastructure.Mapping;

    public class ShoppingCartProductViewModel : IMapFrom<ShoppingCartProduct>
    {
        public int ShoppingCartId { get; set; }

        public int ProductId { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

        public Product Product { get; set; }

        public int Units { get; set; }
    }
}
