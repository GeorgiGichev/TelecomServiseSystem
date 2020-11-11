namespace TelecomServiceSystem.Services.Cron
{
    using System.Threading.Tasks;

    using TelecomServiceSystem.Services.Data.Teams;

    public class InstalSlotsGeneratorJob
    {
        public const string CronSchedule = "59 12 31 12 *";
        private readonly ITeamsService teamsService;

        public InstalSlotsGeneratorJob(ITeamsService teamsService)
        {
            this.teamsService = teamsService;
        }

        public async Task GenerateSlots()
        {
            await this.teamsService.AddSlotsToTeams();
        }
    }
}
