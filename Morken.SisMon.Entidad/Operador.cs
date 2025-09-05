using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Morken.SisMon.Entidad
{
	/// <summary>Operador</summary>
	/// <remarks><list type="bullet">
	/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
	/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>

	[Serializable]
	public class Operador
	{
		public int? IdOperador { get; set; }
		public int? IdPersona { get; set; }
		public string Estado { get; set; }
		public string UsuarioCreacion { get; set; }
		public DateTime? FechaCreacion { get; set; }
		public string UsuarioModificacion { get; set; }
		public DateTime? FechaModificacion { get; set; }
        public Kruma.Core.Business.Entity.Persona Persona { get; set; }
    }
}