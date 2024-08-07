using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ECommerce.API
{
    // Uygulamanın giriş noktası olan Program sınıfı
    public class Program
    {
        // Ana metot, uygulamanın çalışmaya başlamasını sağlar
        public static void Main(string[] args)
        {
            // Uygulama host'unu oluşturur ve çalıştırır
            CreateHostBuilder(args).Build().Run();
        }

        // Host oluşturucusunu yapılandıran metod
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            // Varsayılan yapılandırmaları kullanarak bir host builder oluşturur
            Host.CreateDefaultBuilder(args)
                // Web uygulaması yapılandırmalarını varsayılan olarak ayarlar
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Startup sınıfını kullanarak web uygulamasının yapılandırmasını sağlar
                    webBuilder.UseStartup<Startup>();
                });
    }
}
