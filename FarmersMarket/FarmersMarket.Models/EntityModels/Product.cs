namespace FarmersMarket.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        public Product()
        {
            this.ShoppingCartProducts = new HashSet<ShoppingCartProduct>();
            this.IsDeleted = false;
        }

        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ModelsConstants.RequiredValidationMessage)]
        [StringLength(100, ErrorMessage = ModelsConstants.StringLengthValidationMessage)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ModelsConstants.RequiredValidationMessage)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long and no more than {1}.", MinimumLength = 6)]
        public string Description { get; set; }

        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Invalid Target Price; Maximum Two Decimal Points.")]
        [Range(0, 9999.99, ErrorMessage = "Price should be between {1} and {2}.")]
        public decimal Price { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ModelsConstants.RequiredValidationMessage)]
        [StringLength(10, ErrorMessage = ModelsConstants.StringLengthValidationMessage)]
        public string Quantity { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ModelsConstants.RequiredValidationMessage)]
        [Url(ErrorMessage = ModelsConstants.UrlValidationMessage)]
        public string ImageUrl { get; set; }

        public bool IsDeleted { get; set; }

        public int OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public virtual Farm Owner { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<ShoppingCartProduct> ShoppingCartProducts { get; set; }
    }
}
