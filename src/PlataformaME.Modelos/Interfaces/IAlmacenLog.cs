using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceStack;

namespace PlataformaMe.Modelos
{
	public interface IAlmacenLog
	{
		Task<QueryResponse<Log>> Consultar(LogConsultar modelo, Dictionary<string, string> parametrosPeticion);


		Task<long> Crear(Auditoria auditoria);
		Task<Log> ConsultarPorId(long id);
	}
}
