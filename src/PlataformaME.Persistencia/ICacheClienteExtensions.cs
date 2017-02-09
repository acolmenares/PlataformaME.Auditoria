using System;
using System.Threading.Tasks;
using PlataformaMe.Modelos;
using ServiceStack;
using ServiceStack.Caching;

namespace PlataformaME.Persistencia
{
	public static class ICacheClientExtensions
	{

		private static readonly double expiresInSeconds = 30;

		public static bool Remove<T>(this ICacheClient cacheClient, T entidad) where T : IEntidad
		{
			var cachekey = entidad.CreateUrn();
			return cacheClient.Remove(cachekey);

		}

		public static bool Remove<T>(this ICacheClient cacheClient, int id) where T : IEntidad
		{
			var cachekey = id.ToUrn<T>();
			return cacheClient.Remove(cachekey);
		}


		public static bool Set<T>(this ICacheClient cacheClient, T entidad) where T : IEntidad
		{
			var cachekey = entidad.CreateUrn();
			return cacheClient.Set(cachekey, entidad, TimeSpan.FromSeconds(expiresInSeconds));
		}


		public static async Task<T> Get<T>(this ICacheClient cacheClient, long id, Func<Task<T>> funcGet) //where T : IEntidad
		{
			var cachekey = id.ToUrn<T>();
			var result = cacheClient.Get<T>(cachekey);
			if (result == null)
			{
				result = await funcGet();
				if (result != null)
				{
					cacheClient.Set(cachekey, result, TimeSpan.FromSeconds(expiresInSeconds));
				}

			}

			return result;
		}

	}
}
