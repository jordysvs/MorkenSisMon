using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Morken.SisMon.Entidad
{
	/// <summary>TipoAlerta</summary>
	/// <remarks><list type="bullet">
	/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
	/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>

	[Serializable]
	public class TipoAlerta
	{
		public int? IdTipoAlerta { get; set; }
		public string Descripcion { get; set; }
		public string Estado { get; set; }
		public string UsuarioCreacion { get; set; }
		public DateTime? FechaCreacion { get; set; }
		public string UsuarioModificacion { get; set; }
		public DateTime? FechaModificacion { get; set; }
	}
}