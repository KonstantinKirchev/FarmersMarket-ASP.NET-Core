namespace FarmersMarket.Models.ViewModels
{
    public class UsersFarmsViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }

        public IEnumerable<FarmViewModel> Farms { get; set; }
    }
}
