using System;
namespace PlataformaMe.Modelos
{
	public interface ISaveResponse<T> : IServiceResponse
	{
		T Data { get; set; }
	}
}
