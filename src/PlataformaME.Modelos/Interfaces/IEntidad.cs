using ServiceStack.Model;

namespace PlataformaMe.Modelos
{

	public interface IEntidad : IHasId<int>
	{
		new int Id { get; set; }
	}
}
