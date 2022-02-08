namespace FarmersMarket.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class BaseAdminController : Controller
    {
    }
}