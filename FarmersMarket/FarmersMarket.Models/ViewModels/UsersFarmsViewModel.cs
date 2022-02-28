namespace FarmersMarket.Models.ViewModels
{
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class UsersFarmsViewModel
    {
        public string UserId { get; set; }

        public IEnumerable<SelectListItem> Users { get; set; }

        public int FarmId { get; set; }

        public IEnumerable<SelectListItem> Farms { get; set; }
    }
}
