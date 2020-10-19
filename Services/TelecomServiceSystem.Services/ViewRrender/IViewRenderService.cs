namespace TelecomServiceSystem.Services.ViewRrender
{
    using System.Threading.Tasks;

    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }
}
