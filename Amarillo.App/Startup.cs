using Amarillo.App.Core;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Photino.Blazor;
using PhotinoNET;

namespace Amarillo.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new WebProxy());
        }

        [UsedImplicitly]
        public void Configure(DesktopApplicationBuilder app)
        {
            app.AddComponent<App>("app");
            var window = app.Services.GetService<PhotinoWindow>();
            var webProxy = app.Services.GetService<WebProxy>();
            window!.WindowClosingHandler += (sender, args) =>
            {
                if (webProxy!.ProxyServer.ProxyRunning)
                    webProxy.ProxyServer.Stop();
                return false;
            };
        }
    }
}