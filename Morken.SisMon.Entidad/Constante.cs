using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morken.SisMon.Entidad
{
    public class Constante
    {
        public const string Estado_Activo = "A";
        public const string Estado_Inactivo = "I";
        public const string Condicion_Positivo = "S";
        public const string Condicion_Negativo = "N";
        public const string AlertaEstado_LogRunning = "running";
        public const string AlertaEstado_LogStopped= "stopped";
        public const string AlertaEstado_LogPaused= "paused";
        public const int AlertaEstado_Running = 1;
        public const int AlertaEstado_Stopped = 2;
        public const int AlertaEstado_Paused = 3;
        public const string TipoAlerta_LogStrainHigh = "strain_high";
        public const string TipoAlerta_LogStrainLow = "strain_low";
        public const string TipoAlerta_LogTemperatureHigh = "temperature_high";
        public const string TipoAlerta_LogTemperatureLow = "temperature_low";
        public const int TipoAlerta_StraingHigh = 1;
        public const int TipoAlerta_StraingLow= 2;
        public const int TipoAlerta_TemperatureHigh = 3;
        public const int TipoAlerta_TemperatureLow = 4;
        public const int Canal_1 = 1;
        public const int Canal_2 = 2;
        public const string Usuario_Sistema = "SYSTEM";
        public const int Indicador_Todos = -1;
        public const string Estado_Running = "R";
        public const string Estado_Paused= "P";
        public const string Estado_Stopped = "S";

        public class Parametro
        {
            public const string Correo = "CORREO";
            public const string RutaXML = "RUTAXML";
            public const string XML_Credenciales = "XMLCREDENCIAL";
            public const string Estado_Sistema = "ESTADOSISTEMA";
            public const string Tiempo_Alerta = "TIEMPOALERTA";
            public const string Rango_Dias = "RANGODIAS";

            public const string Almacen_XML = "ALMACENXML";

            public const string Perfil_Administrador = "PERFILADMINISTRADOR";
            public const string Pagina_Administrador = "PAGINAADMIN";
            public const string Pagina_Operador= "PAGINAOPERADOR";
            public const string Perfil_Operador = "PERFILOPERADOR";
            
            public const string Modulo = "SISMON";
        }
    }
}
