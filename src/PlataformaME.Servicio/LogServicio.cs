using System;
using PlataformaMe.Modelos;
using ServiceStack;

namespace PlataformaME.Servicio
{
	public class LogServicio:Service
	{
		public IAlmacenLog Log { get; set; }

		public IAlmacenAuditoria Auditoria { get; set; }


		public QueryResponse<Log> Get(LogConsultar modelo)
		{
			return Log.Consultar(modelo, Request.GetRequestParams()).Result;
		}

		public CrearResponse<Log> Post(LogCrear modelo)
		{
			var auditorias = Auditoria.Consultar(modelo.FechaGreaterThanOrEqualTo, modelo.FechaLessThanOrEqualTo).Result;

			long id = 0;
			auditorias.ForEach(auditoria => {
				id= Log.Crear(auditoria).Result;
				var x = Auditoria.Borrar(auditoria.Id).Result;
			});

			Log log= Log.ConsultarPorId(id).Result;
			return new CrearResponse<Log> { Data = log };

		}
	}
}
