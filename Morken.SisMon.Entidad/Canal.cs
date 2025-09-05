using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Morken.SisMon.Entidad
{
    /// <summary>Canal</summary>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
    /// <item><FecCrea>24-10-2017</FecCrea></item></list></remarks>

    [Serializable]
    public class Canal
    {
        public Canal()
        {
            path_list = new List<Entidad.path_class>();
        }
        public int? IdCanal { get; set; }
        public string Descripcion { get; set; }
        public string Inverso { get; set; }
        public string Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public System.Collections.Generic.List<path_class> path_list { get; set; }
    }
}