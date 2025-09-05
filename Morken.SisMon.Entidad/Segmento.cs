using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Morken.SisMon.Entidad
{
	/// <summary>Segmento</summary>
	/// <remarks><list type="bullet">
	/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
	/// <item><FecCrea>24-10-2017</FecCrea></item></list></remarks>

	[Serializable]
	public class Segmento
	{
		public int? IdCanal { get; set; }
		public int? IdSegmento { get; set; }
		public string Descripcion { get; set; }
		public int? MetroInicial { get; set; }
		public int? MetroFinal { get; set; }
		public string Estado { get; set; }
		public string UsuarioCreacion { get; set; }
		public DateTime? FechaCreacion { get; set; }
		public string UsuarioModificacion { get; set; }
		public DateTime? FechaModificacion { get; set; }
	}
}