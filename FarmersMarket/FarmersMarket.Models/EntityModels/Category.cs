namespace FarmersMarket.Models.EntityModels
{
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
            this.IsDeleted = false;
        }

        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ModelsConstants.RequiredValidationMessage)]
        [StringLength(100, ErrorMessage = ModelsConstants.StringLengthValidationMessage)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
