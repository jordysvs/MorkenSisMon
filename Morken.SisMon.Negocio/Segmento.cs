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
	/// <summary>Segmento</summary>
	/// <remarks><list type="bullet">
	/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
	/// <item><FecCrea>24-10-2017</FecCrea></item></list></remarks>

	public class Segmento
	{
        #region Metodos Públicos

        /// <summary>Listado de Segmento</summary>
        /// <param name="int_pIdCanal">IdCanal</param>
        /// <param name="int_pIdSegmento">IdSegmento</param>
        /// <param name="str_pDescripcion">Descripcion</param>
        /// <param name="int_pMetroInicial">MetroInicial</param>
        /// <param name="int_pMetroFinal">MetroFinal</param>
        /// <param name="str_pEstado">Estado</param>
        /// <param name="int_pNumPagina" >Numero de pagina</param>
        /// <param name="int_pTamPagina" >Tamaño de pagina</param>
        /// <returns>Lista de Segmento</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
        /// <item><FecCrea>24-10-2017</FecCrea></item></list></remarks>
        public static Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Segmento> Listar(int? int_pIdCanal, int? int_pIdSegmento, string str_pDescripcion, int? int_pMetroInicial, int? int_pMetroFinal, string str_pEstado,  int? int_pNumPagina, int? int_pTamPagina)
		{
			return Morken.SisMon.Data.Segmento.Listar(int_pIdCanal, int_pIdSegmento, str_pDescripcion, int_pMetroInicial, int_pMetroFinal, str_pEstado, int_pNumPagina, int_pTamPagina);
		}

        /// <summary>Obtener Segmento</summary>
        /// <param name="int_pIdCanal">IdCanal</param>
        /// <param name="int_pIdSegmento">IdSegmento</param>
        /// <returns>Objeto Segmento</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
        /// <item><FecCrea>24-10-2017</FecCrea></item></list></remarks>
        public static Morken.SisMon.Entidad.Segmento Obtener(int int_pIdCanal, int int_pIdSegmento)
		{
			return Morken.SisMon.Data.Segmento.Obtener(int_pIdCanal, int_pIdSegmento);
		}

		/// <summary>Insertar Segmento</summary>
		/// <param name="obj_pSegmento">Segmento</param>
		/// <returns>Id de Segmento</returns>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>24-10-2017</FecCrea></item></list></remarks>
		public static ProcessResult Insertar(Morken.SisMon.Entidad.Segmento obj_pSegmento)
		{
			ProcessResult obj_Resultado = null;
			try
			{
				Morken.SisMon.Data.Segmento.Insertar(obj_pSegmento);
				obj_Resultado = new ProcessResult(obj_pSegmento.IdSegmento);
			}
			catch (Exception obj_pExcepcion)
			{
				obj_Resultado = new ProcessResult(obj_pExcepcion);
			}
			return obj_Resultado;
		}

		/// <summary>Actualizar Segmento</summary>
		/// <param name="obj_pSegmento">Segmento</param>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>24-10-2017</FecCrea></item></list></remarks>
		public static ProcessResult Modificar(Morken.SisMon.Entidad.Segmento obj_pSegmento)
		{
			ProcessResult obj_Resultado = null;
			try
			{
				Morken.SisMon.Data.Segmento.Modificar(obj_pSegmento);
				obj_Resultado = new ProcessResult(obj_pSegmento.IdSegmento);
			}
			catch (Exception obj_pExcepcion)
			{
				obj_Resultado = new ProcessResult(obj_pExcepcion);
			}
			return obj_Resultado;
		}

		/// <summary>Modificar Estado Segmento</summary>
		/// <param name="obj_pSegmento">Segmento</param>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>24-10-2017</FecCrea></item></list></remarks>
		public static ProcessResult ModificarEstado(Morken.SisMon.Entidad.Segmento obj_pSegmento)
		{
			ProcessResult obj_Resultado = null;
			try
			{
                Morken.SisMon.Entidad.Segmento obj_Segmento = Morken.SisMon.Data.Segmento.Obtener(obj_pSegmento.IdCanal.Value,  obj_pSegmento.IdSegmento.Value);
				if(obj_Segmento.Estado == obj_pSegmento.Estado)
				{
					string str_Mensaje = obj_pSegmento.Estado == Morken.SisMon.Entidad.Constante.Estado_Activo ? "El segmento ya se encuentra activo." : "El segmento ya se encuentra inactivo.";
					return new ProcessResult(new Exception(str_Mensaje), str_Mensaje);
				}
					obj_Segmento.Estado = obj_pSegmento.Estado;
					obj_Segmento.UsuarioModificacion = obj_pSegmento.UsuarioModificacion;
					Morken.SisMon.Data.Segmento.Modificar(obj_Segmento);

					obj_Resultado = new ProcessResult(obj_pSegmento.IdSegmento);
			}
			catch (Exception obj_pExcepcion)
			{
				obj_Resultado = new ProcessResult(obj_pExcepcion);
			}

			return obj_Resultado;
		}

        public static ProcessResult CargarSegmentos(string str_pRutaConfiguracion)
        {
            ProcessResult obj_Resultado = null;
            try
            {
                if (!string.IsNullOrEmpty(str_pRutaConfiguracion))
                {
                    if (File.Exists(str_pRutaConfiguracion))
                    {
                        XmlDocument xml_Documento = new XmlDocument();
                        xml_Documento.Load(str_pRutaConfiguracion);

                        XmlNodeList lst_Nodos_Canal1= xml_Documento.GetElementsByTagName("ch1");
                        XmlNodeList lst_Segmentos_Canal1 = ((XmlElement)lst_Nodos_Canal1[0]).GetElementsByTagName("threshold");

                        XmlNodeList lst_Nodos_Canal2 = xml_Documento.GetElementsByTagName("ch2");
                        XmlNodeList lst_Segmentos_Canal2 = ((XmlElement)lst_Nodos_Canal2[0]).GetElementsByTagName("threshold");

                        ProcessResult obj_ResultadoSegmento = null;

                        obj_ResultadoSegmento = RecorrerSegmentosXML(lst_Segmentos_Canal1, Morken.SisMon.Entidad.Constante.Canal_1);
                        if (obj_ResultadoSegmento.OperationResult != Kruma.Core.Util.Common.Enum.OperationResult.Success)
                            return obj_ResultadoSegmento;

                        obj_ResultadoSegmento = RecorrerSegmentosXML(lst_Segmentos_Canal2, Morken.SisMon.Entidad.Constante.Canal_2);
                        if (obj_ResultadoSegmento.OperationResult != Kruma.Core.Util.Common.Enum.OperationResult.Success)
                            return obj_ResultadoSegmento;

                        obj_Resultado = new ProcessResult(lst_Segmentos_Canal1.Count + lst_Segmentos_Canal2.Count);
                    }
                    else
                    {
                        obj_Resultado = new ProcessResult(new Exception("El archivo XML para la configuración de los segmentos no existe"));
                    }
                }
            }
            catch (Exception ex)
            {
                obj_Resultado = new ProcessResult(ex);
            }
            return obj_Resultado;
        }

        private static ProcessResult RecorrerSegmentosXML(XmlNodeList lst_pSegmentos, int int_IdCanal)
        {
            ProcessResult obj_Resultado = null;
            try
            {
                foreach (XmlElement xml_Elemento in lst_pSegmentos)
                {
                    Morken.SisMon.Entidad.Segmento obj_Segmento = new Entidad.Segmento();
                    string id = xml_Elemento.GetElementsByTagName("id")[0].InnerText;
                    obj_Segmento.IdCanal = int_IdCanal;
                    obj_Segmento.IdSegmento = int.Parse(xml_Elemento.GetElementsByTagName("id")[0].InnerText);
                    obj_Segmento.MetroInicial = int.Parse(xml_Elemento.GetElementsByTagName("pos_start")[0].InnerText);
                    obj_Segmento.MetroFinal = int.Parse(xml_Elemento.GetElementsByTagName("pos_stop")[0].InnerText);
                    obj_Segmento.Estado = Morken.SisMon.Entidad.Constante.Estado_Activo;

                    if (Morken.SisMon.Data.Segmento.Obtener(obj_Segmento.IdCanal.Value, obj_Segmento.IdSegmento.Value) == null)
                    {
                        obj_Segmento.UsuarioCreacion = Morken.SisMon.Entidad.Constante.Usuario_Sistema;
                        Morken.SisMon.Data.Segmento.Insertar(obj_Segmento);
                    }
                    else
                    {
                        obj_Segmento.UsuarioModificacion = Morken.SisMon.Entidad.Constante.Usuario_Sistema;
                        Morken.SisMon.Data.Segmento.Modificar(obj_Segmento);
                    }
                }
               
                obj_Resultado = new ProcessResult(lst_pSegmentos.Count);
            }
            catch (Exception ex)
            {
                obj_Resultado = new ProcessResult(ex);
            }
            return obj_Resultado;
        }
		#endregion
	}

}