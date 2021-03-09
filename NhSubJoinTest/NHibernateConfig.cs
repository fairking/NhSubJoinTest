using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using NHibernate.SimpleMapping;
using System.Diagnostics;
using System.Reflection;

namespace NhSubJoinTest.StartupExtensions
{
	public static class NHibernateConfig
	{
		public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
		{
			var cfg = new Configuration().DataBaseIntegration(db =>
			{
				db.ConnectionString = connectionString;
				db.Dialect<NHibernate.Dialect.SQLiteDialect>();
			});
			cfg.SetProperty("show_sql", "true");
			cfg.SetProperty("format_sql", "true");
			cfg.SetNamingStrategy(ImprovedNamingStrategy.Instance);

			var mapper = new ModelMapper();

			// Uncomment it to use SimpleMapping instead of MapByCode
			//var simpleMappingMaps = MapScanner.Scan(new[] { Assembly.GetExecutingAssembly() });
			//mapper.AddMappings(simpleMappingMaps);

			// Uncomment it to use MapByCode instead of SimpleMapping
			var nhTypes = Assembly.GetExecutingAssembly().GetExportedTypes();
			mapper.AddMappings(nhTypes);

			cfg.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

			services.AddSingleton(cfg);
			services.AddSingleton(s => s.GetRequiredService<Configuration>().BuildSessionFactory());
			services.AddScoped(s => s.GetRequiredService<ISessionFactory>().OpenSession());

			return services;
		}
	}
}
