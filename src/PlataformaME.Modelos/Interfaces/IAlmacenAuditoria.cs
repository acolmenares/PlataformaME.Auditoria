using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaMe.Modelos
{
	public interface IAlmacenAuditoria
	{
		Task<List<Auditoria>> Consultar(DateTime? fechaGreaterThanOrEqualTo, DateTime? fechaLessThanOrEqualTo);

		Task<int> Borrar(int id);
	}
}
