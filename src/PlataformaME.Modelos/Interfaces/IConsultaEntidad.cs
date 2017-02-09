using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PlataformaMe.Modelos
{
	public interface IConsultaEntidad : IDisposable
	{
		
		Task<QueryResponse<From>> ConsultarAsync<From>(IQueryDb<From> modelo,
											Dictionary<string, string> peticion,
											Expression<Func<From, bool>> filtro = null);


		Task<QueryResponse<Into>> ConsultarAsync<From, Into>(IQueryDb<From, Into> modelo,
												  Dictionary<string, string> peticion,
												  Expression<Func<From, bool>> filtro = null);
		
		Task<List<T>> ConsultarAsync<T>(Expression<Func<T, bool>> predicado);

		Task<T> ConsultarPorIdAsync<T>(long id);


		Task<T> ConsultarPorIdAsync<T>(T modelo) where T : IEntidad;

		Task<T> ConsultarElPrimeroAsync<T>(Expression<Func<T, bool>> predicado);


	}
}
