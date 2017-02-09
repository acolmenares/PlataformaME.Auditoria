using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Caching;
using PlataformaMe.Modelos;

namespace PlataformaME.Persistencia
{
	public class ConsultaEntidad : IConsultaEntidad
	{
		IAutoQueryDb autoQuery;
		readonly IDbConnection conexion;
		IDbTransaction transaccion = null;
		protected readonly ICacheClient cacheClient;

		public ConsultaEntidad(IDbConnectionFactory dbConnectionFactory, IAutoQueryDb autoQuery, ICacheClient cacheClient, string connectionName=null)
		{
			this.cacheClient = cacheClient;

			this.autoQuery = autoQuery;
			try
			{
				conexion = connectionName.IsNullOrEmpty() ? dbConnectionFactory.Open() : dbConnectionFactory.Open(connectionName);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Consulta Entidad {0}", ex.Message);
			}
		}


		public Task<T> ConsultarPorIdAsync<T>(long id)
		{
			return cacheClient.Get<T>(id, () => Execute(async cn =>
			{
				var r = await cn.SingleByIdAsync<T>(id);
				return r == null ? typeof(T).CreateInstance<T>() : r;
			}));
		}


		public Task<T> ConsultarPorIdAsync<T>(T modelo) where T : IEntidad
		{
			var id = modelo.Id;
			return ConsultarPorIdAsync<T>(id);
		}


		public Task<QueryResponse<From>> ConsultarAsync<From>(IQueryDb<From> modelo,
												   Dictionary<string, string> peticion,
												   Expression<Func<From, bool>> filtro = null)
		{
			var q = autoQuery.CreateQuery(modelo, peticion);
			if (filtro != null) q = q.And(filtro);
			var r = autoQuery.Execute(modelo, q);
			return r.InTask();
		}



		public Task<QueryResponse<Into>> ConsultarAsync<From, Into>(IQueryDb<From, Into> modelo,
														 Dictionary<string, string> peticion,
														 Expression<Func<From, bool>> filtro = null)
		{

			var q = autoQuery.CreateQuery(modelo, peticion);
			if (filtro != null) q = q.And(filtro);
			var r = autoQuery.Execute(modelo, q);
			return r.InTask();
		}



		public Task<List<T>> ConsultarAsync<T>(Expression<Func<T, bool>> predicado)
		{
			return Execute(async cn => await cn.SelectAsync<T>(predicado));
		}


		public Task<T> ConsultarElPrimeroAsync<T>(Expression<Func<T, bool>> predicado)
		{
			return Execute(async cn =>
			{
				var r = await cn.SingleAsync(predicado);
				return r == null ? typeof(T).CreateInstance<T>() : r;
			});
		}


		public void Dispose()
		{
			Console.WriteLine("ConsultaEntidad: Repositorio Principal Dispose");
			Rollback();
			Execute(con => con.Dispose());
		}


		protected void Execute(Action<IDbConnection> acciones)
		{
			acciones(conexion);
		}

		protected T Execute<T>(Func<IDbConnection, T> acciones)
		{
			return acciones(conexion);
		}


		private void Rollback()
		{
			if (transaccion != null)
			{
				transaccion.Rollback();
				transaccion.Dispose();
				transaccion = null;
			}
		}

		private SqlExpression<From> CrearSqlExpression<From>(Func<From, bool> predicado)
		{
			var qr = CrearSqlExpression<From>();

			return qr;
		}

		private SqlExpression<From> CrearSqlExpression<From>()
		{
			return conexion.From<From>();
		}

		public void IniciarTransaccion()
		{
			if (transaccion == null)
			{
				Execute(con => transaccion = con.OpenTransaction());
			}
		}

		public void FinalizarTransaccion()
		{
			if (transaccion != null)
			{
				transaccion.Commit();
				transaccion.Dispose();
				transaccion = null;
			}
		}

		public void CancelarTransaccion()
		{
			Rollback();
		}

	}
}


