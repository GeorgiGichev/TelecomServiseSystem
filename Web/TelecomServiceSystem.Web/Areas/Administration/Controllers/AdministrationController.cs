namespace TelecomServiceSystem.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TelecomServiceSystem.Common;
    using TelecomServiceSystem.Services.Data.Teams;
    using TelecomServiceSystem.Web.Controllers;
    using TelecomServiceSystem.Web.ViewModels.Administration.Administration;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
