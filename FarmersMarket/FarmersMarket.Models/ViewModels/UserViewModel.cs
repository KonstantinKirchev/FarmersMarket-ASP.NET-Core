namespace FarmersMarket.Models.ViewModels
{
    using FarmersMarket.Models.EntityModels;
    using FarmersMarket.Models.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class UserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ModelsConstants.RequiredValidationMessage)]
        [StringLength(100, ErrorMessage = ModelsConstants.StringLengthValidationMessage)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ModelsConstants.RequiredValidationMessage)]
        [StringLength(80, ErrorMessage = ModelsConstants.StringLengthValidationMessage)]
        public string Address { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ModelsConstants.RequiredValidationMessage)]
        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\b\d{3}[-.]?\d{3}[-.]?\d{4}\b", ErrorMessage = ModelsConstants.PhoneNumberValidationMessage)]
        public string PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ModelsConstants.RequiredValidationMessage)]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Image Url")]
        public string? ImageUrl { get; set; }
    }
}