namespace FarmersMarket.Web.Areas.Manager.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Manager")]
    [Area("Manager")]
    public class BaseManagerController : Controller
    {
    }
}