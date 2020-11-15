namespace TelecomServiceSystem.Web.Pages.Utilities
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;

    public static class NavigationManagerExtensions
    {
        public static async Task NavigateToNewWindowAsync(this NavigationManager navigator, IJSRuntime jsRuntime, string url, string content)
        {
            await jsRuntime.InvokeAsync<object>("NavigationManagerExtensions.openInNewWindow", url, content);
        }
    }
}
