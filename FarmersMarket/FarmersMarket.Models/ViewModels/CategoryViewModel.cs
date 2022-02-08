namespace FarmersMarket.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ModelsConstants.RequiredValidationMessage)]
        [StringLength(100, ErrorMessage = ModelsConstants.StringLengthValidationMessage)]
        public string Name { get; set; }
    }
}
