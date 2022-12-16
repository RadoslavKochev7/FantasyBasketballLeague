using FantasyBasketballLeague.Areas.Admin.Constants;
using FantasyBasketballLeague.Infrastructure.Data.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyBasketballLeague.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleConstants.Administrator)]
    [Route("Admin/[controller]/[Action]/{id?}")]
    [Area(AdminConstants.AreaName)]
    public class BaseController : Controller
    {
    }
}
