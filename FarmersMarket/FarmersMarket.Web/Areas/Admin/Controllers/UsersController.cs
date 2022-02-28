namespace FarmersMarket.Web.Areas.Admin.Controllers
{
    using FarmersMarket.Models.ViewModels;
    using FarmersMarket.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
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

        [HttpGet]
        public IActionResult AssignManagerToFarm()
        {
            IEnumerable<UserViewModel> usersModel = usersService.GetAllUsers();
            IEnumerable<FarmViewModel> farmsModel = farmsService.GetAllFarms();

            IEnumerable<SelectListItem> users = usersModel.Select(u => new SelectListItem()
            {
                Text = u.Email,
                Value = u.Id
            });

            IEnumerable<SelectListItem> farms = farmsModel.Select(f => new SelectListItem()
            {
                Text = f.Name,
                Value = f.Id.ToString()
            });

            UsersFarmsViewModel viewModel = new UsersFarmsViewModel()
            {
                Users = users,
                Farms = farms
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AssignManagerToFarm(string UserId, int FarmId)
        {
            usersService.AssignManagerToFarm(UserId, FarmId);

            return RedirectToAction("Index");
        }
    }
}
