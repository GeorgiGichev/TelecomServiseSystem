namespace TelecomServiceSystem.Web.Hubs
{
    using Microsoft.AspNetCore.SignalR;

    public class TaskHub : Hub<ITaskHub>
    {
        public void Send()
        {
            this.Clients.All.ReceiveTasksUpdate();
        }
    }
}
