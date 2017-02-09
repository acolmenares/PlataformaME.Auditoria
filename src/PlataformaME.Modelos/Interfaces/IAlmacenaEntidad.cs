using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaMe.Modelos
{
	public interface IAlmacenaEntidad : IConsultaEntidad, IDisposable
	{
		
		Task<int> ActualizarAsync<T>(T modelo) where T : IEntidad;

		Task<int> ActualizarAsync<T>(T modelo, IList<string> updateonly) where T : IEntidad;

		Task<long> CrearAsync<T>(T modelo) where T : IEntidad;

		Task<int> BorrarAsync<T>(T modelo) where T : class, IEntidad;

		Task<int> BorrarAsync<T>(int id) where T : IEntidad;

		void IniciarTransaccion();
		void FinalizarTransaccion();
		void CancelarTransaccion();

	}
}
