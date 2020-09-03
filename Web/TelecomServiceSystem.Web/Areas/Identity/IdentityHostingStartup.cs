using System;

using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(TelecomServiceSystem.Web.Areas.Identity.IdentityHostingStartup))]

namespace TelecomServiceSystem.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
