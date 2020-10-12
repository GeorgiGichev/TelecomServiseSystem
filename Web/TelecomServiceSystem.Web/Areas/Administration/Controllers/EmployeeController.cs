namespace TelecomServiceSystem.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TelecomServiceSystem.Services.Data.Employees;
    using TelecomServiceSystem.Web.ViewModels.Administration.Employees;
    using TelecomServiceSystem.Web.ViewModels.Administration.Search;

    public class EmployeeController : AdministrationController
    {
        private readonly IEmployeesService employeesService;

        public EmployeeController(IEmployeesService employeesService)
        {
            this.employeesService = employeesService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new SearchEmployeeViewModel
            {
                EmployeesList = await this.GetEmployeesAsync(new SearchEmpoloyeeInputModel()),
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SearchEmployeeViewModel model)
        {
            model.EmployeesList = await this.GetEmployeesAsync(model.Input);
            return this.View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await this.employeesService.GetByIdAsync<EditViewModel>(id);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.employeesService.Edit<EditViewModel>(model);

            return this.View(model);
        }

        private async Task<HashSet<SearhEmployeeOutputModel>> GetEmployeesAsync(SearchEmpoloyeeInputModel model)
            => (await this.employeesService.GetBySearchCriteriaAsync<SearhEmployeeOutputModel, SearchEmpoloyeeInputModel>(model)).ToHashSet();
    }
}
