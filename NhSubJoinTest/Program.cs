using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NhSubJoinTest.Data;

namespace NhSubJoinTest
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args)
				.Build()
				.EnsureDatabaseCreated()
				.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
