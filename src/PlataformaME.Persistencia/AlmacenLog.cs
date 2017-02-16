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


		public async Task<QueryResponse<Auditoria>> Consultar(AuditoriaConsultar modelo, Dictionary<string, string> parametrosPeticion)
		{
			return await almacen.ConsultarAsync(modelo, parametrosPeticion);
		}


	}
}
