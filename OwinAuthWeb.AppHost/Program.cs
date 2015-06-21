using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinAuthWeb.AppHost
{
    class Program
    {
        const string URL = "http://localhost:8788/";

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("=> Listening on: {0}", URL);

                using (WebApp.Start<AppStartup>(URL))
                {
                    Console.WriteLine("Press ENTER to stop...");
                    Console.ReadLine();
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Error starting . {0}", ex.ToString());

                if (System.Diagnostics.Debugger.IsAttached)
                {
                    Console.WriteLine("Press ENTER to stop...");
                    Console.ReadLine();
                }
            }
        }
    }
}
