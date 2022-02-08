namespace FarmersMarket.Services
{
    using FarmersMarket.Data;

    public abstract class Service
    {
        public FarmersMarketDbContext db;

        protected Service(FarmersMarketDbContext db)
        {
            this.db = db;
        }

        //protected Service(FarmersMarketDbContext db, User user)
        //    : this(db)
        //{
        //    this.UserProfile = user;
        //}

        //public FarmersMarketDbContext db { get; private set; }

        //public User UserProfile { get; set; }

        //protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        //{
        //    if (requestContext.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        var username = requestContext.HttpContext.User.Identity.Name;
        //        var user = this.db.Users.All().FirstOrDefault(u => u.UserName == username);
        //        this.UserProfile = user;
        //    }
        //    return base.BeginExecute(requestContext, callback, state);
        //}
    }
}
