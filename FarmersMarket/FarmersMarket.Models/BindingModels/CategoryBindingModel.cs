namespace FarmersMarket.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    public class CategoryBindingModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ModelsConstants.RequiredValidationMessage)]
        [StringLength(100, ErrorMessage = ModelsConstants.StringLengthValidationMessage)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}
