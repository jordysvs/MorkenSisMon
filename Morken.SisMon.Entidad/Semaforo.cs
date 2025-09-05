using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morken.SisMon.Entidad
{
    public class Semaforo
    {
        public bool Running { get; set; }
        public bool Stopped { get; set; }
        public bool Paused { get; set; }
    }
}
