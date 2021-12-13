using System.Net;
using System.Text;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;

namespace Amarillo.App.Core;

public class WebProxy
{
    public readonly ProxyServer ProxyServer;
    public ExplicitProxyEndPoint ExplicitEndPoint { get; set; }

    public WebProxy()
    {
        ProxyServer = new ProxyServer();

        ExplicitEndPoint = new ExplicitProxyEndPoint(IPAddress.Any, 8000, true)
        {
            // Use self-issued generic certificate on all https requests
            // Optimizes performance by not creating a certificate for each https-enabled domain
            // Useful when certificate trust is not required by proxy clients
            //GenericCertificate = new X509Certificate2(Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "genericcert.pfx"), "password")
        };

        // Fired when a CONNECT request is received
        ExplicitEndPoint.BeforeTunnelConnectRequest += onBeforeTunnelConnectRequest;

        // An explicit endpoint is where the client knows about the existence of a proxy
        // So client sends request in a proxy friendly manner
        ProxyServer.AddEndPoint(ExplicitEndPoint);
    }

    public void Start()
    {
        ProxyServer.Start();
        foreach (var endPoint in ProxyServer.ProxyEndPoints)
            Console.WriteLine("Listening on '{0}' endpoint at Ip {1} and port: {2} ",
                endPoint.GetType().Name, endPoint.IpAddress, endPoint.Port);

// Only explicit proxies can be set as system proxy!
        ProxyServer.SetAsSystemHttpProxy(ExplicitEndPoint);
        ProxyServer.SetAsSystemHttpsProxy(ExplicitEndPoint);
    }

    private static Task onBeforeTunnelConnectRequest(object sender, TunnelConnectSessionEventArgs e)
    {
        string hostname = e.HttpClient.Request.RequestUri.Host;
        e.GetState().PipelineInfo.AppendLine(nameof(onBeforeTunnelConnectRequest) + ":" + hostname);
        Console.WriteLine("Tunnel to: " + hostname);

        var clientLocalIp = e.ClientLocalEndPoint.Address;
        if (!clientLocalIp.Equals(IPAddress.Loopback) && !clientLocalIp.Equals(IPAddress.IPv6Loopback))
        {
            e.HttpClient.UpStreamEndPoint = new IPEndPoint(clientLocalIp, 0);
        }

        return Task.CompletedTask;
    }

    public void Stop()
    {
        //ProxyServer.BeforeRequest;
        for (var i = ProxyServer.ProxyEndPoints.Count - 1; i >= 0; i--)
        {
        }

        ExplicitEndPoint.BeforeTunnelConnectRequest -= onBeforeTunnelConnectRequest;
        ProxyServer.Stop();
        //ProxyServer.Dispose();
    }
}

public static class ProxyEventArgsBaseExtensions
{
    public static SampleClientState GetState(this ProxyEventArgsBase args)
    {
        if (args.ClientUserData == null)
        {
            args.ClientUserData = new SampleClientState();
        }

        return (SampleClientState)args.ClientUserData;
    }
}

public class SampleClientState
{
    public StringBuilder PipelineInfo { get; } = new();
}