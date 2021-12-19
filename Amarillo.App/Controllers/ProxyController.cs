using Amarillo.Core;
using Amarillo.Core.Interfaces;
using Amarillo.Core.Models;
using Amarillo.Core.Services;
using Titanium.Web.Proxy.EventArguments;

namespace Amarillo.App.Controllers;

public class ProxyController : IDisposable
{
    private readonly IMessageService _messageService;
    private readonly WebProxyService _webProxyService;
    private int _entryCount;
    private readonly List<Log> _entries = new();
    private const string From = "Proxy";

    public ProxyController(IMessageService messageService, WebProxyService webProxyService)
    {
        _messageService = messageService;
        _webProxyService = webProxyService;
        _webProxyService.ProxyServer.BeforeRequest += OnRequest;
        _webProxyService.ProxyServer.BeforeResponse += OnResponse;
        _webProxyService.ProxyServer.ServerCertificateValidationCallback += OnCertificateValidation;
        _webProxyService.ProxyServer.ClientCertificateSelectionCallback += OnCertificateSelection;
    }

    public void Start()
    {
        _webProxyService.Start();
    }

    public void Stop()
    {
        _webProxyService.Stop();
    }

    public async Task OnRequest(object sender, SessionEventArgs e)
    {
        Console.WriteLine(e.HttpClient.Request.Url);

        // read request headers
        var requestHeaders = e.HttpClient.Request.Headers;

        var method = e.HttpClient.Request.Method.ToUpper();
        /*if (method is "POST" or "PUT" or "PATCH")
        {
            // Get/Set request body bytes
            var bodyBytes = await e.GetRequestBody();
            e.SetRequestBody(bodyBytes);

            // Get/Set request body as string
            var bodyString = await e.GetRequestBodyAsString();
            e.SetRequestBodyString(bodyString);

            // store request
            // so that you can find it from response handler
            e.UserData = e.HttpClient.Request;
        }*/
        _entryCount++;

        e.UserData = _entryCount;

        var log = new Log { Id = _entryCount, Method = method, Url = e.HttpClient.Request.Url};

        _entries.Add(log);
        _messageService.SendMessage(new Message
        {
            Channel = Channel.Proxy,
            Body = log
        });
    }

// Modify response
    public async Task OnResponse(object sender, SessionEventArgs e)
    {
        Console.WriteLine(e.HttpClient.Request.Url);
        // read response headers
        /*var responseHeaders = e.HttpClient.Response.Headers;

        //if (!e.ProxySession.Request.Host.Equals("medeczane.sgk.gov.tr")) return;
        if (e.HttpClient.Request.Method is "GET" or "POST")
        {
            if (e.HttpClient.Response.StatusCode == 200)
            {
                if (e.HttpClient.Response.ContentType != null &&
                    e.HttpClient.Response.ContentType.Trim().ToLower().Contains("text/html"))
                {
                    var bodyBytes = await e.GetResponseBody();
                    e.SetResponseBody(bodyBytes);

                    var body = await e.GetResponseBodyAsString();
                    e.SetResponseBodyString(body);
                }
            }
        }

        if (e.UserData != null)
        {
            // access request from UserData property where we stored it in RequestHandler
            var request = (Request)e.UserData;
        }*/

        var log = _entries.FirstOrDefault(x => x.Id == (int)e.UserData);
        log.Result = e.HttpClient.Response.StatusCode;
        //StateHasChanged();
    }

    // Allows overriding default certificate validation logic
    public Task OnCertificateValidation(object sender, CertificateValidationEventArgs e)
    {
        // set IsValid to true/false based on Certificate Errors
        if (e.SslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
            e.IsValid = true;

        return Task.CompletedTask;
    }

    // Allows overriding default client certificate selection logic during mutual authentication
    public Task OnCertificateSelection(object sender, CertificateSelectionEventArgs e)
    {
        // set e.clientCertificate to override
        return Task.CompletedTask;
    }


    public void Dispose()
    {
        _webProxyService.ProxyServer.BeforeRequest -= OnRequest;
        _webProxyService.ProxyServer.BeforeResponse -= OnResponse;
        _webProxyService.ProxyServer.ServerCertificateValidationCallback -= OnCertificateValidation;
        _webProxyService.ProxyServer.ClientCertificateSelectionCallback -= OnCertificateSelection;
        Stop();
    }

    public void HandleMessage(Message message)
    {
        if (message.Command != null)
        {
            switch (message.Command.ToLower())
            {
                case "start":
                    Start();
                    break;
                case "stop":
                    Stop();
                    break;
            }
        }
    }
}

public class Log
{
    public int Id { get; set; }

    public string Url { get; set; }

    public int Result { get; set; }

    public string Method { get; set; }

    public string RemoteAddress { get; set; }

}