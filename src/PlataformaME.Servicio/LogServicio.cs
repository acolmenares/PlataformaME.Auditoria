using System;
using PlataformaMe.Modelos;
using ServiceStack;

namespace PlataformaME.Servicio
{
	public class LogServicio:Service
	{
		public IAlmacenLog Log { get; set; }

		public IAlmacenAuditoria Auditoria { get; set; }


		public QueryResponse<Auditoria> Get(AuditoriaConsultar modelo)
		{
			return Log.Consultar(modelo, Request.GetRequestParams()).Result;
		}


	}
}
