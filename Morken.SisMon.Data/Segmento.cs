using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Kruma.Core.Data;
using Kruma.Core.Data.Entity;

namespace Morken.SisMon.Data
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
			Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Segmento> obj_Lista = new Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Segmento>();
			obj_Lista.PageNumber = int_pNumPagina;
			obj_Lista.Total = 0;

			DataOperation dop_Operacion = new DataOperation("ListarSegmento");
			dop_Operacion.Parameters.Add(new Parameter("@pIdCanal", int_pIdCanal.HasValue ? int_pIdCanal.Value : (object)DBNull.Value));
			dop_Operacion.Parameters.Add(new Parameter("@pIdSegmento", int_pIdSegmento.HasValue ? int_pIdSegmento.Value : (object)DBNull.Value));
			dop_Operacion.Parameters.Add(new Parameter("@pDescripcion", !string.IsNullOrEmpty(str_pDescripcion) ? str_pDescripcion : (object)DBNull.Value));
			dop_Operacion.Parameters.Add(new Parameter("@pMetroInicial", int_pMetroInicial.HasValue ? int_pMetroInicial.Value : (object)DBNull.Value));
			dop_Operacion.Parameters.Add(new Parameter("@pMetroFinal", int_pMetroFinal.HasValue ? int_pMetroFinal.Value : (object)DBNull.Value));
			dop_Operacion.Parameters.Add(new Parameter("@pEstado", !string.IsNullOrEmpty(str_pEstado) ? str_pEstado : (object)DBNull.Value));
			dop_Operacion.Parameters.Add(new Parameter("@pNumPagina", int_pNumPagina.HasValue ? int_pNumPagina.Value : (object)DBNull.Value));
			dop_Operacion.Parameters.Add(new Parameter("@pTamPagina", int_pTamPagina.HasValue ? int_pTamPagina.Value : (object)DBNull.Value));

			DataTable dt_Resultado = DataManager.ExecuteDataSet(Conexiones.CO_SistemaMonitoreo, dop_Operacion).Tables[0];

			List<Morken.SisMon.Entidad.Segmento> lst_Segmento= new List<Morken.SisMon.Entidad.Segmento>();
			Morken.SisMon.Entidad.Segmento obj_Segmento= new Morken.SisMon.Entidad.Segmento();
			foreach (DataRow obj_Row in dt_Resultado.Rows)
			{
				if (lst_Segmento.Count == 0)
					obj_Lista.Total = (int)obj_Row["Total_Filas"];
				obj_Segmento = new Morken.SisMon.Entidad.Segmento();
			    obj_Segmento.IdCanal = obj_Row["IdCanal"] is DBNull ? null : (int?)obj_Row["IdCanal"];
			    obj_Segmento.IdSegmento = obj_Row["IdSegmento"] is DBNull ? null : (int?)obj_Row["IdSegmento"];
			    obj_Segmento.Descripcion = obj_Row["Descripcion"] is DBNull ? null : obj_Row["Descripcion"].ToString();
			    obj_Segmento.MetroInicial = obj_Row["MetroInicial"] is DBNull ? null : (int?)obj_Row["MetroInicial"];
			    obj_Segmento.MetroFinal = obj_Row["MetroFinal"] is DBNull ? null : (int?)obj_Row["MetroFinal"];
			    obj_Segmento.Estado = obj_Row["Estado"] is DBNull ? null : obj_Row["Estado"].ToString();
			    obj_Segmento.UsuarioCreacion = obj_Row["UsuarioCreacion"] is DBNull ? null : obj_Row["UsuarioCreacion"].ToString();
			    obj_Segmento.FechaCreacion = obj_Row["FechaCreacion"] is DBNull ? null : (DateTime?)obj_Row["FechaCreacion"];
			    obj_Segmento.UsuarioModificacion = obj_Row["UsuarioModificacion"] is DBNull ? null : obj_Row["UsuarioModificacion"].ToString();
			    obj_Segmento.FechaModificacion = obj_Row["FechaModificacion"] is DBNull ? null : (DateTime?)obj_Row["FechaModificacion"];
			    lst_Segmento.Add(obj_Segmento);
			}

			obj_Lista.Result = lst_Segmento;
			return obj_Lista;
		}

		/// <summary>Obtener Segmento</summary>
		/// <param name="int_pIdSegmento">IdSegmento</param>
		/// <returns>Objeto Segmento</returns>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>24-10-2017</FecCrea></item></list></remarks>
		public static Morken.SisMon.Entidad.Segmento Obtener(int int_pIdCanal, int int_pIdSegmento)
		{
			Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Segmento> lst_Segmento = Listar(int_pIdCanal, int_pIdSegmento, null, null, null, null, null, null);
			return lst_Segmento.Result.Count > 0 ? lst_Segmento.Result[0] : null;
		}

		/// <summary>Insertar Segmento</summary>
		/// <param name="obj_pSegmento">Segmento</param>
		/// <returns>Id de Segmento</returns>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>24-10-2017</FecCrea></item></list></remarks>
		public static void Insertar(Morken.SisMon.Entidad.Segmento obj_pSegmento)
		{
			DataOperation dop_Operacion = new DataOperation("InsertarSegmento");

			dop_Operacion.Parameters.Add(new Parameter("@pIdCanal", obj_pSegmento.IdCanal));
			dop_Operacion.Parameters.Add(new Parameter("@pIdSegmento", obj_pSegmento.IdSegmento));
			dop_Operacion.Parameters.Add(new Parameter("@pDescripcion", obj_pSegmento.Descripcion));
			dop_Operacion.Parameters.Add(new Parameter("@pMetroInicial", obj_pSegmento.MetroInicial));
			dop_Operacion.Parameters.Add(new Parameter("@pMetroFinal", obj_pSegmento.MetroFinal));
			dop_Operacion.Parameters.Add(new Parameter("@pEstado", obj_pSegmento.Estado));
			dop_Operacion.Parameters.Add(new Parameter("@pUsuarioCreacion", obj_pSegmento.UsuarioCreacion));

			//Parameter obj_IdSegmento= new Parameter("@pIdSegmento", DbType.Int32);
			//obj_IdSegmento.Direction = ParameterDirection.Output;
			//dop_Operacion.Parameters.Add(obj_IdSegmento);

			DataManager.ExecuteNonQuery(Conexiones.CO_SistemaMonitoreo, dop_Operacion, false);
			//int int_IdSegmento = (int)obj_IdSegmento.Value;
			//return int_IdSegmento;
		}

		/// <summary>Actualizar Segmento</summary>
		/// <param name="obj_pSegmento">Segmento</param>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>24-10-2017</FecCrea></item></list></remarks>
		public static void Modificar(Morken.SisMon.Entidad.Segmento obj_pSegmento)
		{
			DataOperation dop_Operacion = new DataOperation("ActualizarSegmento");

			dop_Operacion.Parameters.Add(new Parameter("@pIdCanal", obj_pSegmento.IdCanal));
			dop_Operacion.Parameters.Add(new Parameter("@pIdSegmento", obj_pSegmento.IdSegmento));
			dop_Operacion.Parameters.Add(new Parameter("@pDescripcion", obj_pSegmento.Descripcion));
			dop_Operacion.Parameters.Add(new Parameter("@pMetroInicial", obj_pSegmento.MetroInicial));
			dop_Operacion.Parameters.Add(new Parameter("@pMetroFinal", obj_pSegmento.MetroFinal));
			dop_Operacion.Parameters.Add(new Parameter("@pEstado", obj_pSegmento.Estado));
            dop_Operacion.Parameters.Add(new Parameter("@pUsuarioModificacion", obj_pSegmento.UsuarioModificacion));

            DataManager.ExecuteNonQuery(Conexiones.CO_SistemaMonitoreo, dop_Operacion, false);
		}

		#endregion
	}
}