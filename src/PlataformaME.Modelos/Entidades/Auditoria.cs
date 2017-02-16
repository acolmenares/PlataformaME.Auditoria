using System;
using ServiceStack.DataAnnotations;

namespace PlataformaMe.Modelos
{
	//[NamedConnection("Monitoreo")]
	[Alias("auditoria")]
	public class Auditoria : IEntidad  //, IPertenezcoAuditoria
	{

		[Alias("idauditoria")]
		public int Id { get; set; }

		[Alias("fecha_auditoria")]
		public DateTime? Fecha { get; set; }

		[Alias("usuario_auditoria")]
		public string Usuario { get; set; }

		[Alias("tabla_auditoria")]
		public string Tabla { get; set; }

		[Alias("accion_auditoria")]
		public string Accion { get; set; }

		[Alias("sentencia_auditoria")]
		public string SQL { get; set; }

		[Alias("direccionip_auditoria")]
		public string IP { get; set; }

		//[Alias("idtabla_auditoria")]
		//public int? TablaId  { get; set; }

	}
}

