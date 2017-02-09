using System;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace PlataformaMe.Modelos
{
	//[NamedConnection("Monitoreo")]
	[Alias("auditoria")]
	public class Auditoria : IEntidad, IPertenezcoMonitoreo
	{

		[Alias("idauditoria")]
		public int Id { get; set; }

		[Alias("fecha_auditoria")]
		public DateTime? Fecha { get; set; }

		[Alias("usuario_auditoria")]
		public string Usuario { get; set; }

		[Alias("tabla_auditoria")]
		public string Tabla { get; set; }

		[Alias("accion_uaditoria")]
		public string Accion { get; set; }

		[Alias("setencia_auditoria")]
		public string SQL { get; set; }

		/*
		[Alias("ip_auditoria")]
		public string IP  { get; set; }

		[Alias("idtabla_auditoria")]
		public int? TablaId  { get; set; }
        */

	}
}

