using ServiceStack;

namespace PlataformaMe.Modelos
{
	[NamedConnection("Auditoria")]
	public class LogConsultar:QueryDb<Log>
	{
		
	}
}
