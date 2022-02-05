namespace FarmersMarket.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations;

    public class Farm
    {
        public Farm()
        {
            this.Products = new HashSet<Product>();
            this.IsDeleted = false;
        }

        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ModelsConstants.RequiredValidationMessage)]
        [StringLength(100, ErrorMessage = ModelsConstants.StringLengthValidationMessage)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ModelsConstants.RequiredValidationMessage)]
        [StringLength(500, ErrorMessage = ModelsConstants.StringLengthValidationMessage)]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ModelsConstants.RequiredValidationMessage)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ModelsConstants.RequiredValidationMessage)]
        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\b\d{3}[-.]?\d{3}[-.]?\d{4}\b", ErrorMessage = ModelsConstants.PhoneNumberValidationMessage)]
        public string PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ModelsConstants.RequiredValidationMessage)]
        [StringLength(80, ErrorMessage = ModelsConstants.StringLengthValidationMessage)]
        public string Address { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ModelsConstants.RequiredValidationMessage)]
        [Url(ErrorMessage = ModelsConstants.UrlValidationMessage)]
        public string ImageUrl { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
