namespace FarmersMarket.Models.ViewModels
{
    using FarmersMarket.Models.EntityModels;
    using FarmersMarket.Models.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;
    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ModelsConstants.RequiredValidationMessage)]
        [StringLength(100, ErrorMessage = ModelsConstants.StringLengthValidationMessage)]
        public string Name { get; set; }
    }
}
