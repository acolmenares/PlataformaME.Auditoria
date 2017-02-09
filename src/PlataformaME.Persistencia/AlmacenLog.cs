using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaMe.Modelos;
using ServiceStack;

namespace PlataformaME.Persistencia
{
	public class AlmacenLog:IAlmacenLog
	{
		readonly IAlmacenaEntidad almacen;
		public AlmacenLog(IAlmacenaEntidad almacen)
		{
			this.almacen = almacen;
		}


		public async Task<QueryResponse<Log>> Consultar(LogConsultar modelo, Dictionary<string, string> parametrosPeticion)
		{
			return await almacen.ConsultarAsync(modelo, parametrosPeticion);
		}

		public async Task<Log> ConsultarPorId(long id)
		{
			return  await almacen.ConsultarPorIdAsync<Log>(id);

			throw new NotImplementedException();
		}

		public async Task<long> Crear(Auditoria auditoria)
		{
			var log = new Log();
			log.PopulateWith(auditoria);
			log.AuditoriaId = auditoria.Id;
			log.Id = 0;

			return await almacen.CrearAsync(log);


		}
	}
}
