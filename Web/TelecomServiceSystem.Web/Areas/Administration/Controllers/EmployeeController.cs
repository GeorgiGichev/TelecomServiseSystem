﻿namespace TelecomServiceSystem.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TelecomServiceSystem.Services.Data.Employees;
    using TelecomServiceSystem.Services.Data.Teams;
    using TelecomServiceSystem.Web.Controllers;
    using TelecomServiceSystem.Web.ViewModels.Administration.Administration;
    using TelecomServiceSystem.Web.ViewModels.Administration.Employees;
    using TelecomServiceSystem.Web.ViewModels.Administration.Search;

    public class EmployeeController : AdministrationController
    {
        private readonly IEmployeesService employeesService;
        private readonly ITeamsService teamsService;

        public EmployeeController(IEmployeesService employeesService, ITeamsService teamsService)
        {
            this.employeesService = employeesService;
            this.teamsService = teamsService;
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
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

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

        
        public async Task<IActionResult> AllFreeTeams(string employeeId, int cityId)
        {
            var model = new TeamAllViewModel
            {
                CityId = cityId,
                EmployeeId = employeeId,
                Teams = await this.teamsService.GetByCityId<TeamViewModel>(cityId) as ICollection<TeamViewModel>,
            };

            return this.View(model);
        }

        
        public async Task<IActionResult> AddToTeam(int teamId, string employeeId)
        {
            await this.teamsService.AddEmployee(teamId, employeeId);
            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> CreateTeam(string employeeId, int cityId)
        {
            await this.teamsService.CreateAsync(employeeId, cityId);

            return this.Redirect($"/Administration/Employee/AllFreeTeams?employeeId={employeeId}&cityId={cityId}");
        }
    }
}
