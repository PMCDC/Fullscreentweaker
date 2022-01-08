using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FT.Core.Services;

namespace FT.WinClient
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfigureServices(services);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var mainFrm = serviceProvider.GetRequiredService<MainForm>();
                Application.Run(mainFrm);
            }

        }

        static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProcessInteractorService, ProcessInteractorService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<MainForm>();
        }
    }
}
