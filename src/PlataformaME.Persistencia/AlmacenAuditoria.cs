using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PlataformaMe.Modelos;
using ServiceStack.OrmLite;

namespace PlataformaME.Persistencia
{
	public class AlmacenAuditoria:IAlmacenAuditoria
	{
		readonly IAlmacenaEntidad almacen;
		public AlmacenAuditoria(IAlmacenaEntidad almacen)
		{
			this.almacen = almacen;
		}

		public async Task<List<Auditoria>> Consultar(DateTime? fechaGreaterThanOrEqualTo, DateTime? fechaLessThanOrEqualTo  )
		{
			Expression<Func<Auditoria, bool>> predicado = PredicateBuilder.True<Auditoria>();
			if (fechaGreaterThanOrEqualTo.HasValue)
			{
				predicado = PredicateBuilder.And(predicado, q => q.Fecha >= fechaGreaterThanOrEqualTo.Value);
			}

			if (fechaLessThanOrEqualTo.HasValue)
			{
				predicado = PredicateBuilder.And(predicado, q => q.Fecha >= fechaLessThanOrEqualTo.Value);
			}

			return await almacen.ConsultarAsync(predicado);
		}


		public async Task<int> Borrar(int id)
		{
			return await almacen.BorrarAsync<Auditoria>(id);
		}

	}
}
