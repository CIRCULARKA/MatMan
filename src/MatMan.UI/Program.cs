using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace MatMan.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var app = CreateHostBuilder(args).Build();
                app.Run();
            }
            catch (System.Exception e)
            {
                File.WriteAllText("logs.txt", $"[{DateTime.Now}]\n{e.Message}\n\n");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).
                ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.
                        UseIIS().
                        UseContentRoot(Directory.GetCurrentDirectory()).
                        UseIISIntegration().
                        UseStartup<Startup>()
            );
    }
}
