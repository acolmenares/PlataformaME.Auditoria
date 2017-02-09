using System;
using Funq;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.OrmLite;
using ServiceStack.Logging;
using PlataformaME.Servicio;
using PlataformaME.Persistencia;
using PlataformaMe.Modelos;
using ServiceStack.Admin;

namespace PlataformaMe.WebHost
{
	public class AppHost : AppHostBase
	{
		readonly string MONITOREO = "Monitoreo";
		readonly string AUDITORIA = "Auditoria";

		public AppHost() : base("AUDITORIA PLATAFORMA ME API", typeof(LogServicio).Assembly) { }

		public override void Configure(Container container)
		{
			SetConfig(new HostConfig
			{
				DebugMode = true,
				HandlerFactoryPath = "aud-api",
				GlobalResponseHeaders =
					{
						{ "Access-Control-Allow-Origin", "*" },
						{ "Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS, PATCH" },
					},
				DefaultContentType = "json"
			});

			LogManager.LogFactory = new ConsoleLogFactory(debugEnabled: true); ;
			ConfigureRoutes();


			Plugins.Add(new CorsFeature());
			Plugins.Add(new SessionFeature()); // TODO : PONER REDIS AQUI!

			Plugins.Add(new AuthFeature(
				() => new AuthUserSession(),
				new IAuthProvider[] { new CredentialsAuthProvider() })

			{ IncludeAssignRoleServices = false });

			Plugins.Add(new AutoQueryFeature { MaxLimit = 100 });
			Plugins.Add(new AdminFeature());


			var appSettings = new AppSettings();

			var connectionFactory = MySqlConnectionFactory(appSettings);
			container.Register<IDbConnectionFactory>(connectionFactory);

			container.Register<ICacheClient>(new MemoryCacheClient { FlushOnDispose = false });
			container.Register<IUserAuthRepository>(c => new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>(), MONITOREO));


			container.Register<IAlmacenLog>(
				c =>
				new AlmacenLog(
					new AlmacenaEntidad(c.Resolve<IDbConnectionFactory>(),
										c.Resolve<IAutoQueryDb>(),
										c.Resolve<ICacheClient>(), AUDITORIA))).ReusedWithin(ReuseScope.Request);

			container.Register<IAlmacenAuditoria>(
				c =>
					 new AlmacenAuditoria(
					new AlmacenaEntidad(c.Resolve<IDbConnectionFactory>(),
										c.Resolve<IAutoQueryDb>(),
										c.Resolve<ICacheClient>(), MONITOREO))).ReusedWithin(ReuseScope.Request);


			CrearTablaseEnMonitoreo(connectionFactory);
			CrearTablasEnAuditoria(connectionFactory);


		}

		void ConfigureRoutes()
		{

			Routes.Add<LogConsultar>("/log/consultar", ApplyTo.Get);
			Routes.Add<LogCrear>("/log/crear", ApplyTo.Post);

		}

		OrmLiteConnectionFactory MySqlConnectionFactory(AppSettings appSettings)
		{

			var strConexionAuditoria = appSettings.Get("CONEXION_BD_AUDITORIA", Environment.GetEnvironmentVariable("CONEXION_BD_AUDITORIA"));
			var strConexionMonitoreo = appSettings.Get("CONEXION_BD_MONITOREO", Environment.GetEnvironmentVariable("CONEXION_BD_MONITOREO"));

			var dbfactory = new OrmLiteConnectionFactory(strConexionAuditoria, MySqlDialect.Provider)
			{
				ConnectionFilter = x => new ProfiledDbConnection(x, Profiler.Current)
			};
			dbfactory.RegisterConnection(AUDITORIA, strConexionAuditoria, MySqlDialect.Provider);
			dbfactory.RegisterConnection(MONITOREO, strConexionMonitoreo, MySqlDialect.Provider);
			return dbfactory;
		}


		void CrearTablasEnAuditoria(OrmLiteConnectionFactory dbfactory)
		{
			var overwrite = false;
			using (var con = dbfactory.Open(AUDITORIA))
			{

				con.CreateTable<Log>(overwrite);
			}
		}


		void CrearTablaseEnMonitoreo(OrmLiteConnectionFactory dbfactory)
		{
			var overwrite = false;
			using (var con = dbfactory.Open(MONITOREO))
			{
				con.CreateTable<Auditoria>(overwrite);
			}
		}

	}
}
