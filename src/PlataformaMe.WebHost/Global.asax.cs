using System.Web;

namespace PlataformaMe.WebHost
{
	public class Global : HttpApplication
	{
		protected void Application_Start()
		{
			var appHost = new AppHost();
			appHost.Init();
		}
	}
}
