using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Kruma.Core.Util.Common;

namespace Morken.SisMon.Negocio
{
    /// <summary>Alerta</summary>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
    /// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>

    public class Alerta
    {
        #region Metodos Públicos

        /// <summary>Listado de Alerta</summary>
        /// <param name="int_pIdAlerta">IdAlerta</param>
        /// <param name="int_pIdTipoAlerta">IdTipoAlerta</param>
        /// <param name="int_pIdUsuario">IdUsuario</param>
        /// <param name="dt_pFechaAlerta">FechaAlerta</param>
        /// <param name="dt_pHoraInicial">HoraInicial</param>
        /// <param name="dt_pHoraFinal">HoraFinal</param>
        /// <param name="dec_pPosicionInicial">PosicionInicial</param>
        /// <param name="dec_pPosicionFinal">PosicionFinal</param>
        /// <param name="int_pIdAlertaEstado">IdAlertaEstado</param>
        /// <param name="int_pIdOperador">IdOperador</param>
        /// <param name="dt_pFechaInforme">FechaInforme</param>
        /// <param name="dt_pFechaMitigacion">FechaMitigacion</param>
        /// <param name="str_pObservacion">Observacion</param>
        /// <param name="str_pEstado">Estado</param>
        /// <param name="int_pNumPagina" >Numero de pagina</param>
        /// <param name="int_pTamPagina" >Tamaño de pagina</param>
        /// <returns>Lista de Alerta</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
        /// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
        public static Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Alerta> Listar(int? int_pIdAlerta, int? int_pIdCanal, int? int_pIdSegmento,int? int_pCodigoError, int? int_pIdTipoAlerta, string str_pIdUsuario, DateTime? dt_pFechaAlerta, DateTime? dt_pHoraInicial, DateTime? dt_pHoraFinal, decimal? dec_pPosicionInicial, decimal? dec_pPosicionFinal, int? int_pIdAlertaEstado, int? int_pIdOperador, DateTime? dt_pFechaInforme, DateTime? dt_pFechaMitigacion, string str_pObservacion, string str_pEstado, int? int_pNumPagina, int? int_pTamPagina)
        {
            return Morken.SisMon.Data.Alerta.Listar(int_pIdAlerta, int_pIdCanal, int_pIdSegmento, int_pCodigoError, int_pIdTipoAlerta, str_pIdUsuario, dt_pFechaAlerta, dt_pHoraInicial, dt_pHoraFinal, dec_pPosicionInicial, dec_pPosicionFinal, int_pIdAlertaEstado, int_pIdOperador, dt_pFechaInforme, dt_pFechaMitigacion, str_pObservacion, str_pEstado, int_pNumPagina, int_pTamPagina);
        }

        /// <summary>Obtener Alerta</summary>
        /// <param name="int_pIdLocal">IdLocal</param>
        /// <param name="int_pIdUbicacion">IdUbicacion</param>
        /// <param name="int_pIdAlerta">IdAlerta</param>
        /// <param name="int_pIdAlerta">IdAlerta</param>
        /// <returns>Objeto Alerta</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
        /// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
        public static Morken.SisMon.Entidad.Alerta Obtener(int int_pIdAlerta)
        {
            return Morken.SisMon.Data.Alerta.Obtener(int_pIdAlerta);
        }

        /// <summary>Insertar Alerta</summary>
        /// <param name="obj_pAlerta">Alerta</param>
        /// <returns>Id de Alerta</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
        /// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
        public static ProcessResult Insertar(Morken.SisMon.Entidad.Alerta obj_pAlerta)
        {
            ProcessResult obj_Resultado = null;
            try
            {
                int int_IdAlerta = Morken.SisMon.Data.Alerta.Insertar(obj_pAlerta);
                obj_Resultado = new ProcessResult(int_IdAlerta);
            }
            catch (Exception obj_pExcepcion)
            {
                obj_Resultado = new ProcessResult(obj_pExcepcion);
            }
            return obj_Resultado;
        }

        /// <summary>Actualizar Alerta</summary>
        /// <param name="obj_pAlerta">Alerta</param>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
        /// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
        public static ProcessResult Modificar(Morken.SisMon.Entidad.Alerta obj_pAlerta)
        {
            ProcessResult obj_Resultado = null;
            try
            {
                Morken.SisMon.Data.Alerta.Modificar(obj_pAlerta);
                obj_Resultado = new ProcessResult(obj_pAlerta.IdAlerta);
            }
            catch (Exception obj_pExcepcion)
            {
                obj_Resultado = new ProcessResult(obj_pExcepcion);
            }
            return obj_Resultado;
        }

        /// <summary>Modificar Estado Alerta</summary>
        /// <param name="obj_pAlerta">Alerta</param>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
        /// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
        public static ProcessResult ModificarEstado(Morken.SisMon.Entidad.Alerta obj_pAlerta)
        {
            ProcessResult obj_Resultado = null;
            try
            {
                Morken.SisMon.Entidad.Alerta obj_Alerta = Morken.SisMon.Data.Alerta.Obtener(obj_pAlerta.IdAlerta.Value);
                if (obj_Alerta.Estado == obj_pAlerta.Estado)
                {
                    string str_Mensaje = obj_pAlerta.Estado == Morken.SisMon.Entidad.Constante.Estado_Activo ? "La alerta ya se encuentra activa." : "La alerta ya se encuentra inactiva.";
                    return new ProcessResult(new Exception(str_Mensaje), str_Mensaje);
                }
                obj_Alerta.Estado = obj_pAlerta.Estado;
                obj_Alerta.UsuarioModificacion = obj_pAlerta.UsuarioModificacion;
                Morken.SisMon.Data.Alerta.Modificar(obj_Alerta);

                obj_Resultado = new ProcessResult(obj_pAlerta.IdAlerta);
            }
            catch (Exception obj_pExcepcion)
            {
                obj_Resultado = new ProcessResult(obj_pExcepcion);
            }

            return obj_Resultado;
        }

        /// <summary>Listado de Reporte de Monitoreo</summary>
        /// <param name="int_pIdTipoAlerta">IdTipoAlerta</param>
        /// <param name="dt_pFechaAlertaInicio">HoraInicial</param>
        /// <param name="dt_pFechaAlertaFin">HoraFinal</param>
        /// <param name="int_pNumPagina" >Numero de pagina</param>
        /// <param name="int_pTamPagina" >Tamaño de pagina</param>
        /// <returns>Lista de Alerta</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
        /// <item><FecCrea>02-11-2017</FecCrea></item></list></remarks>
        public static Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Alerta> ListarReporteMonitoreo(int? int_pIdTipoAlerta, int? int_pPosicionInicial, int? int_pPosicionFinal, DateTime? dt_pFechaAlertaInicio, DateTime? dt_pFechaAlertaFin, int? int_pNumPagina, int? int_pTamPagina)
        {
            return Morken.SisMon.Data.Alerta.ListarReporteMonitoreo(int_pIdTipoAlerta, int_pPosicionInicial,  int_pPosicionFinal, dt_pFechaAlertaInicio, dt_pFechaAlertaFin, int_pNumPagina, int_pTamPagina);
        }

        /// <summary>Listado de Eventos diarios</summary>
        /// <param name="int_pIdTipoAlerta">IdTipoAlerta</param>
        /// <param name="dt_pFechaAlertaInicio">HoraInicial</param>
        /// <param name="dt_pFechaAlertaFin">HoraFinal</param>
        /// <returns>Lista de Alerta</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
        /// <item><FecCrea>13-11-2017</FecCrea></item></list></remarks>
        public static System.Collections.Generic.List<Morken.SisMon.Entidad.Alerta> ListarEventosDiarios(int? int_pIdTipoAlerta, int? int_pPosicionInicial, int? int_pPosicionFinal, DateTime? dt_pFechaAlertaInicio, DateTime? dt_pFechaAlertaFin)
        {
            return Morken.SisMon.Data.Alerta.ListarEventosDiarios(int_pIdTipoAlerta, int_pPosicionInicial, int_pPosicionFinal, dt_pFechaAlertaInicio, dt_pFechaAlertaFin);
        }

        /// <summary>Leer la ruta del log en el XML</summary>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
        /// <item><FecCrea>20-10-2017</FecCrea></item></list></remarks>
        public static ProcessResult LeerRutaXML(int int_pCantidadDias)
        {
            ProcessResult obj_Resultado = null;
            string str_RutaSegmento = string.Empty;
            string str_RutaLog = string.Empty;
            try
            {
                //string str_RutaXML = "C:\\Ruta.xml";

                //Obteniendo el parámetro con el nombre del archivo XML
                Kruma.Core.Business.Entity.Parametro obj_Parametro =
                    Kruma.Core.Business.Data.Parametro.Obtener(
                         Morken.SisMon.Entidad.Constante.Parametro.Modulo,
                        Morken.SisMon.Entidad.Constante.Parametro.RutaXML);
                if (obj_Parametro != null)
                {
                    string str_RutaXML = string.Empty;

                    //Obteniendo el nombre del archivo XML
                    string str_Archivo = obj_Parametro.Valor;

                    //Obteniendo la ruta del archivo XML
                    int int_IdAlmacen = int.Parse(Kruma.Core.Business.Logical.Parametro.Obtener(Morken.SisMon.Entidad.Constante.Parametro.Modulo, Morken.SisMon.Entidad.Constante.Parametro.Almacen_XML).Valor);
                    Kruma.Core.FileServer.Entity.Almacen obj_Almacen = Kruma.Core.FileServer.Logical.Almacen.Obtener(int_IdAlmacen);

                    using (Util.XMLCredenciales())
                    {
                        str_RutaXML = obj_Almacen.Ruta + str_Archivo;
                        if (File.Exists(str_RutaXML))
                        {
                            XmlDocument xml_Documento = new XmlDocument();
                            xml_Documento.Load(str_RutaXML);

                            //Obteniendo los nodos con la configuración de los segmentos
                            XmlNodeList lst_Nodos_Segmento = xml_Documento.GetElementsByTagName("auto_msr_cfg_path");

                            //Recorriendo la lista de los nodos para obtener la ruta del XML
                            foreach (XmlElement xml_Nodo in lst_Nodos_Segmento)
                                str_RutaSegmento = xml_Nodo.InnerText;

                            //Obteniedo los nodos con la configuración de las alarmas
                            XmlNodeList lst_Nodos_Log = xml_Documento.GetElementsByTagName("alarms_log_path");

                            //Recorriendo la lista de los nodos para obtener la ruta del XML
                            foreach (XmlElement xml_Nodo in lst_Nodos_Log)
                                str_RutaLog = xml_Nodo.InnerText;

                            ProcessResult obj_ResultadoSegmento = Morken.SisMon.Negocio.Segmento.CargarSegmentos(str_RutaSegmento);
                            if (obj_ResultadoSegmento.OperationResult != Kruma.Core.Util.Common.Enum.OperationResult.Success)
                                return obj_ResultadoSegmento;

                            //obj_Resultado = obj_ResultadoSegmento;
                            obj_Resultado = LeerLog(str_RutaLog, int_pCantidadDias);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //str_RutaLog = "Excepción: " + e.ToString();
                obj_Resultado = new ProcessResult(e);
            }

            return obj_Resultado;
        }

        /// <summary>Lee los archivos log</summary>
        /// <param name="str_pRutaLog">Ruta del Log</param>
        /// <param name="int_pCantidadDias">Rango de días</param>
        /// <returns>Resultado de proceso</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
        /// <item><FecCrea>13-11-2017</FecCrea></item></list></remarks>
        public static ProcessResult LeerLog(string str_pRutaLog, int int_pCantidadDias)
        {
            ProcessResult obj_Resultado = null;
            System.Collections.Generic.List<Morken.SisMon.Entidad.Alerta> lst_Alertas = new System.Collections.Generic.List<Entidad.Alerta>();
            bool bln_Running = false;
            bool bln_Paused= false;
            bool bln_Stopped= false;
            try
            {
                if (!string.IsNullOrEmpty(str_pRutaLog))
                {
                    var directorio = new DirectoryInfo(str_pRutaLog);
                    if (directorio.Exists)
                    {
                        var ultimo_archivo = (from f in directorio.GetFiles()
                                              where f.Extension == ".LOG" || f.Extension == ".log"
                                              orderby f.LastWriteTime descending
                                              select f).FirstOrDefault();
                        if (ultimo_archivo != null)
                        {
                            using (StreamReader lector = new StreamReader(ultimo_archivo.FullName))
                            {
                                char char_Separador = '|';
                                while (lector.Peek() > -1)
                                {
                                    string linea = lector.ReadLine();
                                    if (!string.IsNullOrEmpty(linea))
                                    {
                                        string[] arr_Lineas = linea.Split(char_Separador);
                                        Morken.SisMon.Entidad.Alerta obj_Alerta = null;

                                        /*
                                        0 | timestamp: UTC time run status: Indicates if the monitoring is currently ongoing.  
                                            / marca de tiempo: estado de ejecución de tiempo UTC: indica si la supervisión está actualmente en curso.
                                        1 | run status: Indicates if the monitoring is currently ongoing. Possible values: “running”, “stopped”, “paused” 
                                            / estado de ejecución: indica si la supervisión está actualmente en curso. Valores posibles: "en ejecución", "parado", "en pausa"
                                        2 | channel number: number of the fiber channel the threshold refers to (as configured by the user)  
                                            / número de canal de fibra al que se refiere el umbral (según lo configurado por el usuario)
                                        3 | threshold id: id of the threshold as configured by the user. Can be used to identify the threshold. 
                                            / Identificación del umbral según lo configurado por el usuario. Puede usarse para identificar el umbral.
                                        4 | alarm type: type of alarm. Possible values “strain_high”, “strain_low”  
                                            / tipo de alarma Valores posibles "strain_high", "strain_low"
                                        5 | position start: value in meters. Start position of the alarm along the fiber, starting from the instrument.  
                                            / valor en metros Inicie la posición de la alarma a lo largo de la fibra, comenzando por el instrumento.
                                        6 | position stop: value in meters. Stop position of the alarm along the fiber, starting from the instrument. 
                                            /valor en metros Detenga la posición de la alarma a lo largo de la fibra, comenzando por el instrumento.
                                        7 | Threshold value: value of the threshold as configured in the alarms configuration file. Microstrains or degrees Celsius. 
                                            / Valor del umbral como se configuró en el archivo de configuración de alarmas. Microstrains o grados Celsius.
                                        8 | Peak value:  peak value in the threshold range. Set even if threshold was not exceeded. Microstrains or degree Celsius. 
                                            / Valor máximo en el rango del umbral. Establecido incluso si no se excedió el umbral. Microstrains o grado Celsius.
                                        9 | number of hits – total: total number of threshold hits. 
                                            / número total de aciertos de umbral.
                                        10 | number of hits – over tolerance: total number of threshold hits which exceed the configured width threshold.  
                                            / número total de aciertos de umbral que exceden el umbral de ancho configurado.
                                        */
                                        // Old / 2017 - 03 - 06T13: 15:05 | running | ALM_MSR | 1 | 4 | strain_low | -1 | -1 | 0 | 0 
                                        //2017 - 05 - 03T11: 54:46Z | running | ALM_MSR | 1 | 1 | strain_high | 37.5 | 52 | 70 | 221 | 2 | 1 
                                        //2017 - 05 - 03T11: 54:46Z | running | ALM_MSR | 1 | 2 | strain_high | 38 | 47 | 160 | 221 | 3 | 2
                                        if (arr_Lineas.Length > 0)
                                        {
                                            if (arr_Lineas.Length >= 6)
                                            {
                                                int? int_IdCanal = int.Parse(arr_Lineas[3].ToString().Trim());
                                                int? int_IdSegmento = int.Parse(arr_Lineas[4].ToString().Trim());

                                                //Verificando si el canal existe
                                                if (Morken.SisMon.Data.Canal.Obtener(int_IdCanal.Value) == null)
                                                    continue;

                                                Morken.SisMon.Entidad.Segmento obj_Segmento = null;

                                                obj_Segmento = Morken.SisMon.Data.Segmento.Obtener(int_IdCanal.Value, int_IdSegmento.Value);
                                                //Verificando si el segmento existe
                                                if (obj_Segmento == null)
                                                    continue;

                                                DateTime dt_FechaAlerta = XmlConvert.ToDateTime(arr_Lineas[0].ToString().Trim(), XmlDateTimeSerializationMode.Utc);
                                                //DateTime dt_FechaAlerta = DateTime.Parse(arr_Lineas[0].ToString().Trim());
                                                decimal dec_PosicionInicial = decimal.Parse(arr_Lineas[6].ToString().Trim());
                                                decimal dec_PosicionFinal = decimal.Parse(arr_Lineas[7].ToString().Trim());
                                                int? int_IdTipoAlerta = null;
                                                if (arr_Lineas[5].ToString().Trim() == Morken.SisMon.Entidad.Constante.TipoAlerta_LogStrainHigh)
                                                    int_IdTipoAlerta = Morken.SisMon.Entidad.Constante.TipoAlerta_StraingHigh;
                                                else if (arr_Lineas[5].ToString().Trim() == Morken.SisMon.Entidad.Constante.TipoAlerta_LogStrainLow)
                                                    int_IdTipoAlerta = Morken.SisMon.Entidad.Constante.TipoAlerta_StraingLow;
                                                else if (arr_Lineas[5].ToString().Trim() == Morken.SisMon.Entidad.Constante.TipoAlerta_LogTemperatureHigh)
                                                    int_IdTipoAlerta = Morken.SisMon.Entidad.Constante.TipoAlerta_TemperatureHigh;
                                                else if (arr_Lineas[5].ToString().Trim() == Morken.SisMon.Entidad.Constante.TipoAlerta_LogTemperatureLow)
                                                    int_IdTipoAlerta = Morken.SisMon.Entidad.Constante.TipoAlerta_TemperatureLow;

                                                //Listando las alertas encontradas
                                                System.Collections.Generic.List<Morken.SisMon.Entidad.Alerta> lst_AlertasEncontradas = Morken.SisMon.Data.Alerta.Listar
                                                        (null, int_IdCanal, int_IdSegmento, null, int_IdTipoAlerta, null, null, null, null, 
                                                        dec_PosicionInicial, dec_PosicionFinal, null, null, null, null, null, Morken.SisMon.Entidad.Constante.Estado_Activo, null, null).Result;

                                                //Validando duplicidad de las alertas
                                                if(lst_AlertasEncontradas.Where(x => x.FechaAlerta == dt_FechaAlerta).ToList().Count > 0)
                                                    continue;

                                                //if(arr_Lineas[2].ToString() == "")
                                                obj_Alerta = new Entidad.Alerta();
                                                obj_Alerta.IdCanal = int_IdCanal;
                                                obj_Alerta.IdSegmento = int_IdSegmento;
                                                obj_Alerta.FechaAlerta = dt_FechaAlerta;
                                                obj_Alerta.IdTipoAlerta = int_IdTipoAlerta;
                                                if (arr_Lineas[1].ToString().Trim() == Morken.SisMon.Entidad.Constante.AlertaEstado_LogRunning)
                                                {
                                                    obj_Alerta.IdAlertaEstado = Morken.SisMon.Entidad.Constante.AlertaEstado_Running;
                                                    bln_Running = true;
                                                    bln_Stopped = false;
                                                    bln_Paused = false;
                                                }
                                                else if (arr_Lineas[1].ToString().Trim() == Morken.SisMon.Entidad.Constante.AlertaEstado_LogStopped)
                                                {
                                                    obj_Alerta.IdAlertaEstado = Morken.SisMon.Entidad.Constante.AlertaEstado_Stopped;
                                                    bln_Stopped  = true;
                                                    bln_Running = false;
                                                    bln_Paused = false;
                                                }
                                                else if (arr_Lineas[1].ToString().Trim() == Morken.SisMon.Entidad.Constante.AlertaEstado_LogPaused)
                                                {
                                                    obj_Alerta.IdAlertaEstado = Morken.SisMon.Entidad.Constante.AlertaEstado_Paused;
                                                    bln_Paused = true;
                                                    bln_Running = false;
                                                    bln_Stopped = false;
                                                }

                                                obj_Alerta.MetroInicialSegmento = obj_Segmento.MetroInicial;
                                                obj_Alerta.MetroFinalSegmento = obj_Segmento.MetroFinal;
                                                obj_Alerta.PosicionInicial = dec_PosicionInicial;
                                                obj_Alerta.PosicionFinal = dec_PosicionFinal;
                                                obj_Alerta.ValorUmbral = decimal.Parse(arr_Lineas[8].ToString().Trim());
                                                obj_Alerta.ValorUmbralMaximo = decimal.Parse(arr_Lineas[9].ToString().Trim());
                                                obj_Alerta.CantidadGolpes = int.Parse(arr_Lineas[10].ToString().Trim());
                                                obj_Alerta.CantidadGolpesMaximo = int.Parse(arr_Lineas[11].ToString().Trim());
                                                obj_Alerta.Estado = Morken.SisMon.Entidad.Constante.Estado_Activo;
                                                obj_Alerta.UsuarioCreacion = Morken.SisMon.Entidad.Constante.Usuario_Sistema;
                                                obj_Alerta.IdAlerta = Morken.SisMon.Data.Alerta.Insertar(obj_Alerta);
                                                //lst_Alertas.Add(obj_Alerta);
                                            }
                                            //2017 - 03 - 06T13: 14:45 | paused | ALM_SYS | -4 | network error connecting to instrument
                                            else if (arr_Lineas.Length == 5)
                                            {
                                                int? int_AlertaEstado = null;
                                                bln_Stopped = false;
                                                bln_Paused = false;
                                                bln_Running = false;
                                                if (arr_Lineas[1].ToString().Trim() == Morken.SisMon.Entidad.Constante.AlertaEstado_LogRunning)
                                                    int_AlertaEstado = Morken.SisMon.Entidad.Constante.AlertaEstado_Running;
                                                else if (arr_Lineas[1].ToString().Trim() == Morken.SisMon.Entidad.Constante.AlertaEstado_LogStopped)
                                                    int_AlertaEstado = Morken.SisMon.Entidad.Constante.AlertaEstado_Stopped;
                                                else if (arr_Lineas[1].ToString().Trim() == Morken.SisMon.Entidad.Constante.AlertaEstado_LogPaused)
                                                    int_AlertaEstado = Morken.SisMon.Entidad.Constante.AlertaEstado_Paused;

                                                DateTime dt_FechaAlerta = DateTime.Parse(arr_Lineas[0].ToString().Trim());
                                                int int_CodigoError = int.Parse(arr_Lineas[3].ToString().Trim());

                                                //Listando las alertas encontradas
                                                System.Collections.Generic.List<Morken.SisMon.Entidad.Alerta> lst_AlertasEncontradas = Morken.SisMon.Data.Alerta.Listar
                                                        (null, null, null, int_CodigoError, null, null, null, null, null,
                                                        null, null, int_AlertaEstado, null, null, null, null, Morken.SisMon.Entidad.Constante.Estado_Activo, null, null).Result;

                                                //Validando duplicidad de las alertas
                                                if (lst_AlertasEncontradas.Where(x => x.FechaAlerta == dt_FechaAlerta).ToList().Count > 0)
                                                    continue;

                                                if (int_AlertaEstado == Morken.SisMon.Entidad.Constante.AlertaEstado_Running)
                                                    bln_Running = true;
                                                else if (int_AlertaEstado == Morken.SisMon.Entidad.Constante.AlertaEstado_Stopped)
                                                    bln_Stopped = true;
                                                else if(int_AlertaEstado == Morken.SisMon.Entidad.Constante.AlertaEstado_Paused)
                                                    bln_Paused = true;

                                                obj_Alerta = new Entidad.Alerta();
                                                obj_Alerta.FechaAlerta = dt_FechaAlerta;
                                                obj_Alerta.CodigoError = int_CodigoError;
                                                obj_Alerta.IdAlertaEstado = int_AlertaEstado;
                                                obj_Alerta.Observacion = arr_Lineas[4].ToString().Trim();
                                             
                                                obj_Alerta.Estado = Morken.SisMon.Entidad.Constante.Estado_Activo;
                                                obj_Alerta.UsuarioCreacion = Morken.SisMon.Entidad.Constante.Usuario_Sistema;
                                                obj_Alerta.IdAlerta = Morken.SisMon.Data.Alerta.Insertar(obj_Alerta);
                                            }
                                        }
                                    }
                                }

                                //System.Collections.Generic.List<Morken.SisMon.Entidad.Alerta> lst_vAlertas = new System.Collections.Generic.List<Entidad.Alerta>();

                                //Obteniendo las alertas que tienen informe del operador
                                //System.Collections.Generic.List<Morken.SisMon.Entidad.Alerta> lst_AlertasInformadas = Data.Alerta.Listar(null, null, null, null, null, null, null, null, null, null, null, null, Morken.SisMon.Entidad.Constante.Indicador_Todos, null, null, null, Morken.SisMon.Entidad.Constante.Estado_Activo, null, null).Result;
                                //Agregando las alertas informadas al listado general para el administrador
                                //foreach (Morken.SisMon.Entidad.Alerta obj_AlertaInformada in lst_AlertasInformadas)
                                //    lst_vAlertas.Add(obj_AlertaInformada);

                                //Agregando las alertas del log al listado general para el administrador
                                //foreach (Morken.SisMon.Entidad.Alerta obj_AlertaInformada in lst_Alertas)
                                //    lst_vAlertas.Add(obj_AlertaInformada);

                                //Obteniendo la lista de las alertas que no han sido mitigadas en el rango de días
                                System.Collections.Generic.List<Morken.SisMon.Entidad.Alerta> lst_vAlertas = Data.Alerta.ListarAlertasMapa(int_pCantidadDias, null, null, null);

                                lst_vAlertas = CalcularCoordenadas(lst_vAlertas);

                                obj_Resultado = new ProcessResult(lst_vAlertas.Count);
                                obj_Resultado.Objeto = lst_vAlertas;
                                
                                //Obteniendo el estado del sistema
                                ObtenerEstadoMonitoreo(bln_Running, bln_Paused, bln_Stopped);

                                //Notificando las alertas
                                Morken.SisMon.Negocio.Monitoreo.Notificar(lst_Alertas);
                            }
                        }
                        else
                        {
                            obj_Resultado = new ProcessResult(new Exception("No existe ningún archivo LOG en la carpeta."));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                obj_Resultado = new ProcessResult(ex);
            }
            return obj_Resultado;
        }

        /// <summary>Actualiza el estado del sistema según el archivo LOG</summary>
        /// <param name="bln_pRunning">Sistema corriendo</param>
        /// <param name="bln_pPaused">Sistema pausado</param>
        /// <param name="bln_pStopped">Sistema detenido</param>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
        /// <item><FecCrea>13-11-2017</FecCrea></item></list></remarks>
        private static void ObtenerEstadoMonitoreo(bool bln_pRunning, bool bln_pPaused, bool bln_pStopped)
        {
            Kruma.Core.Business.Entity.Parametro obj_SistemaEstado = Kruma.Core.Business.Data.Parametro.Obtener
                (Kruma.Core.Business.Entity.Constante.Parametro.Modulo, Morken.SisMon.Entidad.Constante.Parametro.Estado_Sistema);

            if (bln_pRunning)
                obj_SistemaEstado.Valor = Morken.SisMon.Entidad.Constante.Estado_Running;
            else if (bln_pPaused)
                obj_SistemaEstado.Valor = Morken.SisMon.Entidad.Constante.Estado_Paused;
            else if (bln_pStopped)
                obj_SistemaEstado.Valor = Morken.SisMon.Entidad.Constante.Estado_Stopped;

            Kruma.Core.Business.Data.Parametro.Modificar(obj_SistemaEstado);
        }

        /// <summary>Calcula las coordenadas de las alertas según el archivo KML</summary>
        /// <param name="lst_pAlertas">Listado de alertas</param>
        /// <returns>Resultado de proceso</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
        /// <item><FecCrea>13-11-2017</FecCrea></item></list></remarks>
        public static System.Collections.Generic.List<Morken.SisMon.Entidad.Alerta> CalcularCoordenadas(System.Collections.Generic.List<Morken.SisMon.Entidad.Alerta> lst_pAlertas)
        {
            //Se obtiene el documento KML
            Morken.SisMon.Negocio.KmlReader obj_KmlReader = new Morken.SisMon.Negocio.KmlReader();
            obj_KmlReader.CargarMapa();

            //Se obtiene los canales
            System.Collections.Generic.List<Morken.SisMon.Entidad.Canal> lst_Canal = Morken.SisMon.Negocio.Canal.Listar(null, null, Morken.SisMon.Entidad.Constante.Estado_Activo, null, null).Result;
            foreach (Morken.SisMon.Entidad.Canal obj_Canal in lst_Canal)
            {
                obj_Canal.path_list = obj_KmlReader.path_list.Where(x => x.Nombre.ToLower().Contains(obj_Canal.Descripcion.ToLower())).ToList();
                //Si la direccion del canal es de derecha a izquierda
                if (obj_Canal.Inverso == Kruma.Core.Business.Entity.Constante.Condicion_Positivo)
                {
                    obj_Canal.path_list = obj_Canal.path_list.OrderByDescending(x => x.Orden).ToList();
                    foreach (Morken.SisMon.Entidad.path_class obj_Item in obj_Canal.path_list)
                        obj_Item.Coordenadas = obj_Item.Coordenadas.OrderByDescending(x => x.orden).ToArray();
                }
            }

            //Se obtiene las coordenadas de la alerta
            Morken.SisMon.Entidad.Canal obj_CanalAlerta = null;
            foreach (Morken.SisMon.Entidad.Alerta obj_Alerta in lst_pAlertas)
            {
                //Se obtiene el canal de la alerta
                obj_CanalAlerta = lst_Canal.Where(x => x.IdCanal == obj_Alerta.IdCanal).FirstOrDefault();
                if (obj_CanalAlerta != null)
                {
                    double int_PuntoMedioAlerta = (double) (obj_Alerta.PosicionInicial.Value + obj_Alerta.PosicionFinal.Value) / 2;
                    double dbl_AcumuladorMetro = 0000.0000;
                    bool bln_TieneCoordenadas = obj_Alerta.CoordenadaLongitud.HasValue && obj_Alerta.CoordenadaLatitud.HasValue;

                    for (int i = 0; i < obj_CanalAlerta.path_list.Count; i++)
                    {
                        if (!bln_TieneCoordenadas)
                        {
                            for (int j = 0; j < obj_CanalAlerta.path_list[i].Coordenadas.Length; j++)
                            {
                                double? dbl_SiguienteCoordenadaLongitud = null;
                                double? dbl_SiguienteCoordenadaLatitud = null;

                                //Verificando si existe el siguiente en el arreglo
                                if ((object)obj_CanalAlerta.path_list[i].Coordenadas[j + 1] != null)
                                {
                                    dbl_SiguienteCoordenadaLongitud = obj_CanalAlerta.path_list[i].Coordenadas[j + 1].lon;
                                    dbl_SiguienteCoordenadaLatitud = obj_CanalAlerta.path_list[i].Coordenadas[j + 1].lat;
                                }
                                else
                                {
                                    //En el caso de que no exista el siguiente, pasar al siguiente del path_list
                                    if (obj_CanalAlerta.path_list[i + 1] != null)
                                    {
                                        //Verificando si contiene coordenadas
                                        if (obj_CanalAlerta.path_list[i + 1].Coordenadas.Count() > 0)
                                        {
                                            dbl_SiguienteCoordenadaLongitud = obj_CanalAlerta.path_list[i + 1].Coordenadas[0].lon;
                                            dbl_SiguienteCoordenadaLatitud = obj_CanalAlerta.path_list[i + 1].Coordenadas[0].lat;
                                        }
                                    }
                                }
                                double dbl_DistanciaCoordenadas = Morken.SisMon.Entidad.path_class.getDistance
                                        (obj_CanalAlerta.path_list[i].Coordenadas[j].lon, obj_CanalAlerta.path_list[i].Coordenadas[j].lat,
                                       dbl_SiguienteCoordenadaLongitud.Value, dbl_SiguienteCoordenadaLatitud.Value);

                                if (!bln_TieneCoordenadas)
                                {
                                    if ((dbl_AcumuladorMetro + dbl_DistanciaCoordenadas) >= int_PuntoMedioAlerta)
                                    {
                                        //Hallando la proporción
                                        double dbl_ProporcionCoordenada = int_PuntoMedioAlerta / (dbl_AcumuladorMetro + dbl_DistanciaCoordenadas);

                                        //Hallando las coordenadas
                                        obj_Alerta.CoordenadaLatitud = (obj_CanalAlerta.path_list[i].Coordenadas[j].lat + (dbl_ProporcionCoordenada * dbl_SiguienteCoordenadaLatitud)) / (1 + dbl_ProporcionCoordenada);
                                        obj_Alerta.CoordenadaLongitud = (obj_CanalAlerta.path_list[i].Coordenadas[j].lon + (dbl_SiguienteCoordenadaLongitud * dbl_ProporcionCoordenada)) / (1 + dbl_ProporcionCoordenada);
                                        bln_TieneCoordenadas = true;
                                        break;
                                    }
                                    else
                                        dbl_AcumuladorMetro += dbl_DistanciaCoordenadas;
                                }
                                else
                                    break;
                            }
                        }
                        else
                            break;
                    }
                }

            }
            return lst_pAlertas;
        }


        /// <summary>Lista las alertas del operador </summary>
        /// <param name="int_pIdOperador">Id del Operador</param>
        /// <returns>Resultado de proceso</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
        /// <item><FecCrea>13-11-2017</FecCrea></item></list></remarks>
        public static System.Collections.Generic.List<Morken.SisMon.Entidad.Alerta> ListarAlertasOperador(int? int_pIdOperador)
        {
            System.Collections.Generic.List<Morken.SisMon.Entidad.Alerta> lst_Alertas =
                Morken.SisMon.Data.Alerta.Listar(null, null, null, null, null, null, null, null, null, null, null, null, int_pIdOperador, null, null, null, Morken.SisMon.Entidad.Constante.Estado_Activo, null, null).Result;

            //Verificar luego esta parte
            //lst_Alertas = lst_Alertas.Where(x => !x.FechaMitigacion.HasValue).ToList();
            return CalcularCoordenadas(lst_Alertas);
        }

        /// <summary>Listado de Reporte de alertas del sistem</summary>
        /// <param name="int_pIdAlertaEstado">IdTipoAlerta</param>
        /// <param name="dt_pFechaAlertaInicio">HoraInicial</param>
        /// <param name="dt_pFechaAlertaFin">HoraFinal</param>
        /// <param name="int_pNumPagina" >Numero de pagina</param>
        /// <param name="int_pTamPagina" >Tamaño de pagina</param>
        /// <returns>Lista de Alerta</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
        /// <item><FecCrea>02-11-2017</FecCrea></item></list></remarks>
        public static Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Alerta> ListarReporteSistema(int? int_pIdAlertaEstado, DateTime? dt_pFechaAlertaInicio, DateTime? dt_pFechaAlertaFin, int? int_pNumPagina, int? int_pTamPagina)
        {
            return Morken.SisMon.Data.Alerta.ListarReporteSistema(int_pIdAlertaEstado, dt_pFechaAlertaInicio, dt_pFechaAlertaFin, int_pNumPagina, int_pTamPagina);
        }

        /// <summary>Listado de Alerta</summary>
        /// <param name="int_pCantidadDias" >Rango de días</param>
        /// <param name="int_pIdTipoAlerta" >Id de Tipo de alerta</param>
        /// <param name="dt_pFechaAlertaInicio" >Fecha Inicial</param>
        /// <param name="dt_pFechaAlertaFin" >Fecha Final</param>
        /// <returns>Lista de Alerta para el mapa de monitoreo</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
        /// <item><FecCrea>23-03-2018</FecCrea></item></list></remarks>
        public static System.Collections.Generic.List<Morken.SisMon.Entidad.Alerta> ListarAlertasMapa(int int_pCantidadDias, int? int_pIdTipoAlerta, DateTime? dt_pFechaAlertaInicio, DateTime? dt_pFechaAlertaFin)
        {
            return Morken.SisMon.Data.Alerta.ListarAlertasMapa(int_pCantidadDias, int_pIdTipoAlerta, dt_pFechaAlertaInicio, dt_pFechaAlertaFin);
        }

        #endregion
    }
}
