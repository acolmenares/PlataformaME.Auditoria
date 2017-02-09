using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Caching;
using PlataformaMe.Modelos;

namespace PlataformaME.Persistencia
{
	public class AlmacenaEntidad : ConsultaEntidad, IAlmacenaEntidad
	{

		public AlmacenaEntidad(IDbConnectionFactory dbConnectionFactory, 
		                       IAutoQueryDb autoQuery, 
		                       ICacheClient cacheClient,
		                       string connectionName=null) 
			: base(dbConnectionFactory, autoQuery, cacheClient, connectionName)
		{
		}


		public Task<int> ActualizarAsync<T>(T modelo) where T : IEntidad
		{

			cacheClient.Remove<T>(modelo);
			var result = Execute(async cn =>
		   {
			   var id = modelo.Id;
			   var u = await cn.UpdateAsync<T>(modelo, q => q.Id == id);
			   var modeloActualizado = await cn.SingleByIdAsync<T>(modelo.Id);
			   cacheClient.Set(modeloActualizado);
			   return u;
		   });
			return result;
		}


	

		public Task<int> ActualizarAsync<T>(T modelo, IList<string> updateonly) where T : IEntidad
		{
			cacheClient.Remove(modelo);
			var result = Execute(async cn =>
		   {
			   var id = modelo.Id;
			   var r = await cn.UpdateOnlyAsync<T>(modelo, onlyFields: updateonly.ToArray(), where: q => q.Id == id);
			   var modeloActualizado = await cn.SingleByIdAsync<T>(modelo.Id);
			   cacheClient.Set(modeloActualizado);
			   return r;
		   });
			return result;
		}




		public Task<int> BorrarAsync<T>(int id) where T : IEntidad
		{

			cacheClient.Remove<T>(id);
			var result = Execute(async cn =>
		  {
			  return await cn.DeleteByIdAsync<T>(id);
		  });
			return result;
		}


		public Task<int> BorrarAsync<T>(T modelo) where T : class, IEntidad
		{
			var id = modelo.Id;
			return BorrarAsync<T>(id);
		}


			

		public Task<long> CrearAsync<T>(T modelo) where T : IEntidad
		{
			var result = Execute(async cn => await cn.InsertAsync(modelo, true));

			modelo.Id = (int)result.Result;
			cacheClient.Set(modelo);
			return result;
		}

	}
}
