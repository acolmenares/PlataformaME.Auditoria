using ServiceStack;

namespace PlataformaMe.Modelos
{
	[NamedConnection("Auditoria")]
	public class AuditoriaConsultar:QueryDb<Auditoria>
	{
	}
}
