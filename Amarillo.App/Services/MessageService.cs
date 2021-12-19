using Amarillo.App.Controllers;
using Amarillo.Core.Interfaces;
using Amarillo.Core.Models;
using Autofac;
using Newtonsoft.Json;
using PhotinoNET;

namespace Amarillo.App.Services;

public class MessageService : IMessageService
{
    private readonly PhotinoWindow _window;

    public MessageService(PhotinoWindow window)
    {
        _window = window;
    }

    public void SendMessage(Message message)
    {
      _window.SendWebMessage(JsonConvert.SerializeObject(message));
    }

    public void ReceiveMessage(IContainer? container, string message)
    {
        var converted = JsonConvert.DeserializeObject<Message>(message);

        switch (converted!.Channel)
        {
            case Channel.Proxy:
                var proxyController = container.Resolve<ProxyController>();
                proxyController.HandleMessage(converted);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}