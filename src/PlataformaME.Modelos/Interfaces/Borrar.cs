using System;
using ServiceStack;

namespace PlataformaMe.Modelos
{
	public abstract class Borrar<T> : IReturn<BorrarResponse>
	{
		public int Id { get; set; }
	}

	public class BorrarResponse : ServiceResponse, IBorrarResponse
	{
	}
}
