namespace FarmersMarket.Services.Interfaces
{
    using FarmersMarket.Models.BindingModels;
    using FarmersMarket.Models.ViewModels;

    public interface IFarmsService
    {
        IEnumerable<FarmViewModel> GetAllFarms();
        void CreateNewFarm(FarmBindingModel model);
        FarmViewModel? GetEditFarm(int id);
        void EditFarm(FarmBindingModel model);
        FarmViewModel? GetDeleteFarm(int? id);
        void DeleteFarm(int id);
    }
}
