using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Morken.SisMon.Entidad
{
	/// <summary>Alerta</summary>
	/// <remarks><list type="bullet">
	/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
	/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>

	[Serializable]
	public class Alerta
	{
		public int? IdAlerta { get; set; }
		public int? IdCanal { get; set; }
		public int? IdSegmento { get; set; }
		public int? CodigoError { get; set; }
		public int? MetroInicialSegmento { get; set; }
		public int? MetroFinalSegmento { get; set; }
		public int? IdTipoAlerta { get; set; }
		public string IdUsuario { get; set; }
		public DateTime? FechaAlerta { get; set; }
		public string FechaAlertaToString { get; set; }
		public DateTime? FechaAsignacion { get; set; }
        public string TiempoDemoraAsignacion { get; set; }
        public decimal? PosicionInicial { get; set; }
		public decimal? PosicionFinal { get; set; }
		public decimal? ValorUmbral { get; set; }
		public decimal? ValorUmbralMaximo { get; set; }
		public int? CantidadGolpes { get; set; }
		public int? CantidadGolpesMaximo { get; set; }
		public int? IdAlertaEstado { get; set; }
		public int? IdOperador { get; set; }
		public DateTime? FechaInforme { get; set; }
        public string TiempoDemoraInforme { get; set; }
		public DateTime? FechaMitigacion { get; set; }
        public string TiempoDemoraMitigacion { get; set; }
		public string Observacion { get; set; }
		public string Estado { get; set; }
		public string UsuarioCreacion { get; set; }
		public DateTime? FechaCreacion { get; set; }
		public string UsuarioModificacion { get; set; }
		public DateTime? FechaModificacion { get; set; }
        public AlertaEstado AlertaEstado { get; set; }
        public TipoAlerta TipoAlerta { get; set; }
        public Kruma.Core.Security.Entity.Usuario Usuario { get; set; }
        public Operador Operador { get; set; }
        public double? CoordenadaLatitud { get; set; }
        public double? CoordenadaLongitud { get; set; }
		public int? CantidadAlertas { get; set; }
    }
}