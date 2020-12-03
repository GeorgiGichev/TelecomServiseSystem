namespace TelecomServiceSystem.Web.Areas.Engineering.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TelecomServiceSystem.Common;
    using TelecomServiceSystem.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.EngineerRoleName)]
    [Area("Engineering")]
    public class EngineeringBaseController : BaseController
    {
    }
}
