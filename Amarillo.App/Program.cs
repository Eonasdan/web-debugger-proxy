using Amarillo.App.Services;
using Amarillo.Core;
using Amarillo.Core.Interfaces;
using Amarillo.Core.Services;
using Autofac;
using PhotinoNET;

namespace Amarillo.App
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            try
            {
                var container = ConfigureContainer();
                var start = container.Resolve<Startup>();
                start.Container = container;
                start.Run(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Startup>().AsSelf();
            //builder.RegisterType<WebProxy>().As<IWebProxy>();
            builder.RegisterType<WebProxyService>().AsSelf();
            builder.RegisterType<MessageService>().AsImplementedInterfaces();
            builder.RegisterType<Controllers.ProxyController>().AsSelf();

            var window = new PhotinoWindow();
            //window.Load("wwwroot/index.html");

            builder.RegisterInstance(window).As<PhotinoWindow>();

            return builder.Build();
        }
    }

    internal class Startup
    {
        private readonly PhotinoWindow _window;
        private readonly WebProxyService _webProxyService;
        private readonly IMessageService _messageService;
        public IContainer? Container;

        public Startup(PhotinoWindow window, WebProxyService webProxyService, IMessageService messageService)
        {
            _window = window;
            _webProxyService = webProxyService;
            _messageService = messageService;
        }

        public void Run(string[] args)
        {
            _window
#if debug
                    .Load("http://localhost:5000")
# else
                .Load("wwwroot/index.html")
#endif
                .RegisterWebMessageReceivedHandler((sender, message) =>
                {
                    _messageService.ReceiveMessage(Container, message);
                });

            _window.WindowClosingHandler += (_, _) =>
            {
                if (_webProxyService!.ProxyServer.ProxyRunning)
                    _webProxyService.ProxyServer.Stop();
                return false;
            };

            _window.WaitForClose(); // Starts the application event loop
        }
    }
}