using System;
using ServiceStack;

namespace PlataformaMe.Modelos
{
	public abstract class Crear<T> : IReturn<CrearResponse<T>>
	{
	}

	public class CrearResponse<T> : ServiceResponse, ICrearResponse<T>
	{
		public T Data
		{
			get; set;
		}

	}
}
