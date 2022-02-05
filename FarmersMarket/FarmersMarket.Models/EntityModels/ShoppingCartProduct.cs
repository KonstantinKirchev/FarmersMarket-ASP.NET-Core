namespace FarmersMarket.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ShoppingCartProduct
    {
        public ShoppingCartProduct()
        {
            this.Units = 1;
        }

        [Key, Column(Order = 0)]
        public int ShoppingCartId { get; set; }

        [Key, Column(Order = 1)]
        public int ProductId { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }

        public virtual Product Product { get; set; }

        public int Units { get; set; }
    }
}
