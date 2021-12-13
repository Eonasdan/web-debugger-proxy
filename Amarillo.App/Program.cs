using Photino.Blazor;

namespace Amarillo.App
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            ComponentsDesktop.Run<Startup>("Hello Photino Blazor!"
                , "wwwroot/index.html"
                , x:450
                , y:100
                , width:1000
                , height:900);
        }
    }
}
