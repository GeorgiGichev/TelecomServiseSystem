namespace TelecomServiceSystem.Web.ViewModels.Slots
{
    using System;

    using TelecomServiceSystem.Data.Models;
    using TelecomServiceSystem.Services.Mapping;

    public class SlotViewModel : IMapFrom<InstalationSlot>
    {
        public int Id { get; set; }

        public DateTime StartingTime { get; set; }

        public DateTime EndingTime { get; set; }

        public string FullInfo => $"{this.StartingTime.ToShortDateString()}   ({this.StartingTime.ToString("HH:mm")} - {this.EndingTime.ToString("HH:mm")})";
    }
}
