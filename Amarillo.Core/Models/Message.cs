using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Amarillo.Core.Models;

public class Message
{
    [JsonConverter(typeof(StringEnumConverter))]
    public Channel Channel { get; set; }

    public object? Body { get; set; }

    public string? Command { get; set; }
}

public enum Channel
{
    Proxy
}