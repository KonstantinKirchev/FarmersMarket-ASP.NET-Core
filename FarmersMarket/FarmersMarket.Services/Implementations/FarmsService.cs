namespace FarmersMarket.Services.Implementations
{
    using AutoMapper;
    using FarmersMarket.Data;
    using FarmersMarket.Models.BindingModels;
    using FarmersMarket.Models.EntityModels;
    using FarmersMarket.Models.ViewModels;
    using FarmersMarket.Services.Interfaces;

    public class FarmsService : Service, IFarmsService
    {
        private readonly IMapper mapper;

        public FarmsService(FarmersMarketDbContext db, IMapper mapper)
            : base(db)
        {
            this.mapper = mapper;
        }

        public IEnumerable<FarmViewModel> GetAllFarms()
        {
            IEnumerable<Farm> farms = this.db.Farms.Where(f => f.IsDeleted == false).OrderBy(f => f.Id);
            IEnumerable<FarmViewModel> viewModels = this.mapper.Map<IEnumerable<Farm>, IEnumerable<FarmViewModel>>(farms);
            return viewModels;
        }

        public void CreateNewFarm(FarmBindingModel model)
        {
            Farm? existingFarm = this.db.Farms.FirstOrDefault(f => f.Name == model.Name);

            if (existingFarm == null)
            {
                var farm = new Farm()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Address = model.Address,
                    Email = model.Email,
                    ImageUrl = model.ImageUrl,
                    PhoneNumber = model.PhoneNumber
                };

                this.db.Farms.Add(farm);
            }
            else
            {
                existingFarm.IsDeleted = false;
                existingFarm.Description = model.Description;
                existingFarm.Address = model.Address;
                existingFarm.Email = model.Email;
                existingFarm.ImageUrl = model.ImageUrl;
                existingFarm.PhoneNumber = model.PhoneNumber;
            }

            this.db.SaveChanges();
        }

        public FarmViewModel? GetEditFarm(int id)
        {
            Farm? farm = this.db.Farms.Find(id);

            if (farm == null || farm.IsDeleted == true)
            {
                return null;
            }

            FarmViewModel viewModel = this.mapper.Map<Farm, FarmViewModel>(farm);

            return viewModel;
        }

        public void EditFarm(FarmBindingModel model)
        {
            Farm? farm = this.db.Farms.Find(model.Id);

            if (farm != null && farm.IsDeleted == false)
            {
                farm.Name = model.Name;
                farm.Description = model.Description;
                farm.ImageUrl = model.ImageUrl;
                farm.Address = model.Address;
                farm.Email = model.Email;
                farm.PhoneNumber = model.PhoneNumber;

                this.db.SaveChanges();
            }
            
        }

        public FarmViewModel? GetDeleteFarm(int? id)
        {
            Farm? farm = this.db.Farms.Find(id);

            if (farm == null || farm.IsDeleted == true)
            {
                return null;
            }

            FarmViewModel viewModel = this.mapper.Map<Farm, FarmViewModel>(farm);

            return viewModel;
        }

        public void DeleteFarm(int id)
        {
            Farm? farm = this.db.Farms.Find(id);
            if (farm == null) return;
            farm.IsDeleted = true;
            this.db.SaveChanges();
        }
    }
}
