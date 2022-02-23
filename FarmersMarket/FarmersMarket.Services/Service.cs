namespace FarmersMarket.Services
{
    using AutoMapper;
    using FarmersMarket.Data.UnitOfWork;

    public abstract class Service
    {
        protected readonly IFarmersMarketData db;
        protected readonly IMapper mapper;

        protected Service(IFarmersMarketData db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
    }
}
