using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NhSubJoinTest.Data.Entities;
using System;
using System.Linq;

namespace NhSubJoinTest.Data
{
	public static class DatabaseSetup
	{
		public static IHost EnsureDatabaseCreated(this IHost host)
		{
			Exception error = null;

			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				var cfg = services.GetRequiredService<Configuration>();
				var session = services.GetRequiredService<ISession>();
				var logger = services.GetRequiredService<ILogger<Program>>();

				logger.LogInformation($"Start EnsureDatabaseCreated ASPNETCORE_ENVIRONMENT: {System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}");

				// Ensure Created
				try
				{
					new SchemaExport(cfg).Execute(false, true, false);
				}
				catch (Exception ex)
				{
					logger.LogCritical(ex, "Error whilst creating database");
					error = new Exception("Error whilst creating database", ex);
				}

				// Seed database
				try
				{
					if (!session.Query<Document>().Any())
					{
						using (var tran = session.BeginTransaction())
						{
							try
							{
								var company = new Company("ABC");
								session.Save(company);

								var entities = new Document[]
								{
									new Order(company, "O-20/001", "DIY supplies", 1200),
									new Order(company, "O-20/002", "Office equipment", 800),
									new Order(company, "O-20/003", "Network services", 150),
									new Invoice(company, "I-20/001", "DIY supplies", 1050),
									new Invoice(company, "I-20/002", "Office equipment", 600),
									new Invoice(company, "I-20/003", "Network services", 110),
								};

								foreach (var entity in entities)
								{
									session.Save(entity);
								}

								tran.Commit();
							}
							catch
							{
								tran.Rollback();
								throw;
							}
						}
					}
				}
				catch (Exception ex)
				{
					logger.LogCritical(ex, "Error whilst seeding database");
					error = new Exception("Error whilst seeding database", ex);
				}
			}

			if (error != null)
				throw error;

			return host;
		}
	}
}
