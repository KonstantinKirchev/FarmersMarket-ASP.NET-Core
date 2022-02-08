namespace FarmersMarket.Models.ViewModels
{
    using FarmersMarket.Models.EntityModels;
    using FarmersMarket.Models.Infrastructure.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.ComponentModel.DataAnnotations;

    public class ProductViewModel : IMapFrom<Product>
    {
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

        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        [Display(Name = "Farm")]
        public int FarmId { get; set; }

        public IEnumerable<SelectListItem> Farms { get; set; }
    }
}
