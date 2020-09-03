namespace TelecomServiceSystem.Data.Models
{
    using TelecomServiceSystem.Data.Common.Models;

    public class ServiseNumber : BaseDeletableModel<int>
    {
        public ServiseNumber()
        {
            this.IsFree = true;
        }

        public string Number { get; set; }

        public bool IsFree { get; set; }
    }
}
