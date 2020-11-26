namespace TelecomServiceSystem.Web.Hubs
{
    using System.Threading.Tasks;

    public interface ITaskHub
    {
        Task ReceiveTasksUpdate();
    }
}
