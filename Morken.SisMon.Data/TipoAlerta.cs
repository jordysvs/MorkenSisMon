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
	/// <summary>TipoAlerta</summary>
	/// <remarks><list type="bullet">
	/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
	/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>

	public class TipoAlerta
	{
		#region Metodos Públicos

		/// <summary>Listado de TipoAlerta</summary>
		/// <param name="int_pIdTipoAlerta">IdTipoAlerta</param>
		/// <param name="str_pDescripcion">Descripcion</param>
		/// <param name="str_pEstado">Estado</param>
		/// <param name="int_pNumPagina" >Numero de pagina</param>
		/// <param name="int_pTamPagina" >Tamaño de pagina</param>
		/// <returns>Lista de TipoAlerta</returns>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
		public static Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.TipoAlerta> Listar(int? int_pIdTipoAlerta, string str_pDescripcion, string str_pEstado,  int? int_pNumPagina, int? int_pTamPagina)
		{
			Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.TipoAlerta> obj_Lista = new Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.TipoAlerta>();
			obj_Lista.PageNumber = int_pNumPagina;
			obj_Lista.Total = 0;

			DataOperation dop_Operacion = new DataOperation("ListarTipoAlerta");
			dop_Operacion.Parameters.Add(new Parameter("@pIdTipoAlerta", int_pIdTipoAlerta.HasValue ? int_pIdTipoAlerta.Value : (object)DBNull.Value));
			dop_Operacion.Parameters.Add(new Parameter("@pDescripcion", !string.IsNullOrEmpty(str_pDescripcion) ? str_pDescripcion : (object)DBNull.Value));
			dop_Operacion.Parameters.Add(new Parameter("@pEstado", !string.IsNullOrEmpty(str_pEstado) ? str_pEstado : (object)DBNull.Value));
			dop_Operacion.Parameters.Add(new Parameter("@pNumPagina", int_pNumPagina.HasValue ? int_pNumPagina.Value : (object)DBNull.Value));
			dop_Operacion.Parameters.Add(new Parameter("@pTamPagina", int_pTamPagina.HasValue ? int_pTamPagina.Value : (object)DBNull.Value));

			DataTable dt_Resultado = DataManager.ExecuteDataSet(Conexiones.CO_SistemaMonitoreo, dop_Operacion).Tables[0];

			List<Morken.SisMon.Entidad.TipoAlerta> lst_TipoAlerta= new List<Morken.SisMon.Entidad.TipoAlerta>();
			Morken.SisMon.Entidad.TipoAlerta obj_TipoAlerta= new Morken.SisMon.Entidad.TipoAlerta();
			foreach (DataRow obj_Row in dt_Resultado.Rows)
			{
				if (lst_TipoAlerta.Count == 0)
					obj_Lista.Total = (int)obj_Row["Total_Filas"];
				obj_TipoAlerta = new Morken.SisMon.Entidad.TipoAlerta();
			obj_TipoAlerta.IdTipoAlerta = obj_Row["IdTipoAlerta"] is DBNull ? null : (int?)obj_Row["IdTipoAlerta"];
			obj_TipoAlerta.Descripcion = obj_Row["Descripcion"] is DBNull ? null : obj_Row["Descripcion"].ToString();
			obj_TipoAlerta.Estado = obj_Row["Estado"] is DBNull ? null : obj_Row["Estado"].ToString();
			obj_TipoAlerta.UsuarioCreacion = obj_Row["UsuarioCreacion"] is DBNull ? null : obj_Row["UsuarioCreacion"].ToString();
			obj_TipoAlerta.FechaCreacion = obj_Row["FechaCreacion"] is DBNull ? null : (DateTime?)obj_Row["FechaCreacion"];
			obj_TipoAlerta.UsuarioModificacion = obj_Row["UsuarioModificacion"] is DBNull ? null : obj_Row["UsuarioModificacion"].ToString();
			obj_TipoAlerta.FechaModificacion = obj_Row["FechaModificacion"] is DBNull ? null : (DateTime?)obj_Row["FechaModificacion"];
			lst_TipoAlerta.Add(obj_TipoAlerta);
			}

			obj_Lista.Result = lst_TipoAlerta;
			return obj_Lista;
		}

		/// <summary>Obtener TipoAlerta</summary>
		/// <param name="int_pIdTipoAlerta">IdTipoAlerta</param>
		/// <returns>Objeto TipoAlerta</returns>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
		public static Morken.SisMon.Entidad.TipoAlerta Obtener(int int_pIdTipoAlerta)
		{
			Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.TipoAlerta> lst_TipoAlerta = Listar(int_pIdTipoAlerta, null, null, null, null);
			return lst_TipoAlerta.Result.Count > 0 ? lst_TipoAlerta.Result[0] : null;
		}

		/// <summary>Insertar TipoAlerta</summary>
		/// <param name="obj_pTipoAlerta">TipoAlerta</param>
		/// <returns>Id de TipoAlerta</returns>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
		public static int Insertar(Morken.SisMon.Entidad.TipoAlerta obj_pTipoAlerta)
		{
			DataOperation dop_Operacion = new DataOperation("InsertarTipoAlerta");

			dop_Operacion.Parameters.Add(new Parameter("@pDescripcion", obj_pTipoAlerta.Descripcion));
			dop_Operacion.Parameters.Add(new Parameter("@pEstado", obj_pTipoAlerta.Estado));
			dop_Operacion.Parameters.Add(new Parameter("@pUsuarioCreacion", obj_pTipoAlerta.UsuarioCreacion));

			Parameter obj_IdTipoAlerta= new Parameter("@pIdTipoAlerta", DbType.Int32);
			obj_IdTipoAlerta.Direction = ParameterDirection.Output;
			dop_Operacion.Parameters.Add(obj_IdTipoAlerta);

			DataManager.ExecuteNonQuery(Conexiones.CO_SistemaMonitoreo, dop_Operacion, false);
			int int_IdTipoAlerta = (int)obj_IdTipoAlerta.Value;
			return int_IdTipoAlerta;
		}

		/// <summary>Actualizar TipoAlerta</summary>
		/// <param name="obj_pTipoAlerta">TipoAlerta</param>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
		public static void Modificar(Morken.SisMon.Entidad.TipoAlerta obj_pTipoAlerta)
		{
			DataOperation dop_Operacion = new DataOperation("ActualizarTipoAlerta");

			dop_Operacion.Parameters.Add(new Parameter("@pIdTipoAlerta", obj_pTipoAlerta.IdTipoAlerta));
			dop_Operacion.Parameters.Add(new Parameter("@pDescripcion", obj_pTipoAlerta.Descripcion));
			dop_Operacion.Parameters.Add(new Parameter("@pEstado", obj_pTipoAlerta.Estado));
            dop_Operacion.Parameters.Add(new Parameter("@pUsuarioModificacion", obj_pTipoAlerta.UsuarioModificacion));

            DataManager.ExecuteNonQuery(Conexiones.CO_SistemaMonitoreo, dop_Operacion, false);
		}

		#endregion
	}
}