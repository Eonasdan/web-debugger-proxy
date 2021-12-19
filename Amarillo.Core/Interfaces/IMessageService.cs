using Amarillo.Core.Models;
using Autofac;

namespace Amarillo.Core.Interfaces;

public interface IMessageService
{
    void SendMessage(Message message);
    void ReceiveMessage(IContainer? container, string message);
}