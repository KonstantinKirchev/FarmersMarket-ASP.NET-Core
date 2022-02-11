namespace FarmersMarket.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class ProductBindingModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ModelsConstants.RequiredValidationMessage)]
        [StringLength(100, ErrorMessage = ModelsConstants.StringLengthValidationMessage)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ModelsConstants.RequiredValidationMessage)]
        [StringLength(500, ErrorMessage = ModelsConstants.StringLengthValidationMessage)]
        public string Description { get; set; }

        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Invalid Target Price; Maximum Two Decimal Points.")]
        [Range(0, 9999.99, ErrorMessage = "Price should be between {1} and {2}.")]
        public decimal Price { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ModelsConstants.RequiredValidationMessage)]
        [StringLength(10, ErrorMessage = ModelsConstants.StringLengthValidationMessage)]
        public string Unit { get; set; }

        [Range(0, 9999.99, ErrorMessage = "Quantity should be between {1} and {2}.")]
        public int Quantity { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ModelsConstants.RequiredValidationMessage)]
        [Url(ErrorMessage = ModelsConstants.UrlValidationMessage)]
        public string ImageUrl { get; set; }

        public bool IsDeleted { get; set; }

        public int CategoryId { get; set; }

        public int FarmId { get; set; }
    }
}
