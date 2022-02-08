namespace FarmersMarket.Services
{
    using AutoMapper;
    using FarmersMarket.Data;

    public abstract class Service
    {
        protected readonly FarmersMarketDbContext db;
        protected readonly IMapper mapper;

        protected Service(FarmersMarketDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
    }
}
