namespace FarmersMarket.Web.Areas.Admin.Controllers
{
    using FarmersMarket.Models.ViewModels;
    using FarmersMarket.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using X.PagedList;

    public class UsersController : BaseAdminController
    {
        private readonly IUsersService usersService;
        private readonly IFarmsService farmsService;

        public UsersController(IUsersService usersService, IFarmsService farmsService)
        {
            this.usersService = usersService;
            this.farmsService = farmsService;
        }

        [HttpGet]
        public IActionResult Index(int? page)
        {
            IEnumerable<UserViewModel> users = usersService.GetAllUsers();

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(users.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> AddUserToRoleManager(string id)
        {
            await usersService.AddUserToRoleManager(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> RemoveUserFromRoleManager(string id)
        {
            await usersService.RemoveUserFromRoleManager(id);
            return RedirectToAction("Index");
        }
    }
}
