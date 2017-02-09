using System;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace PlataformaMe.Modelos
{
	//[Alias("auditoria")]
	//[NamedConnection("Auditoria")]
	public class Log : IEntidad, IPertenezcoAuditoria
	{
		[AutoIncrement]
		public int Id { get; set;}

		//[Alias("idauditoria")]
		public int AuditoriaId { get; set; }

		//[Alias("fecah_auditoria")]
		public DateTime? Fecha { get; set; }

		//[Alias("usuario_auditoria")]
		public string Usuario { get; set; }

		//[Alias("tabla_auditoria")]
		public string Tabla { get; set; }

		//[Alias("accion_auditoria")]
		public string Accion { get; set; }

		//[Alias("setencia_auditoria")]
		public string SQL { get; set; }

		/*
		[Alias("ip_auditoria")]
		public string IP  { get; set; }

		[Alias("idtabla_auditoria")]
		public int? TablaId  { get; set; }
        */

	}
}

