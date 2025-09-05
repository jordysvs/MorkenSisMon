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
	/// <summary>AlertaEstado</summary>
	/// <remarks><list type="bullet">
	/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
	/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>

	public class AlertaEstado
	{
		#region Metodos Públicos

		/// <summary>Listado de AlertaEstado</summary>
		/// <param name="int_pIdAlertaEstado">IdAlertaEstado</param>
		/// <param name="str_pDescripcion">Descripcion</param>
		/// <param name="str_pEstado">Estado</param>
		/// <param name="int_pNumPagina" >Numero de pagina</param>
		/// <param name="int_pTamPagina" >Tamaño de pagina</param>
		/// <returns>Lista de AlertaEstado</returns>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
		public static Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.AlertaEstado> Listar(int? int_pIdAlertaEstado, string str_pDescripcion, string str_pEstado,  int? int_pNumPagina, int? int_pTamPagina)
		{
			Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.AlertaEstado> obj_Lista = new Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.AlertaEstado>();
			obj_Lista.PageNumber = int_pNumPagina;
			obj_Lista.Total = 0;

			DataOperation dop_Operacion = new DataOperation("ListarAlertaEstado");
			dop_Operacion.Parameters.Add(new Parameter("@pIdAlertaEstado", int_pIdAlertaEstado.HasValue ? int_pIdAlertaEstado.Value : (object)DBNull.Value));
			dop_Operacion.Parameters.Add(new Parameter("@pDescripcion", !string.IsNullOrEmpty(str_pDescripcion) ? str_pDescripcion : (object)DBNull.Value));
			dop_Operacion.Parameters.Add(new Parameter("@pEstado", !string.IsNullOrEmpty(str_pEstado) ? str_pEstado : (object)DBNull.Value));
			dop_Operacion.Parameters.Add(new Parameter("@pNumPagina", int_pNumPagina.HasValue ? int_pNumPagina.Value : (object)DBNull.Value));
			dop_Operacion.Parameters.Add(new Parameter("@pTamPagina", int_pTamPagina.HasValue ? int_pTamPagina.Value : (object)DBNull.Value));

			DataTable dt_Resultado = DataManager.ExecuteDataSet(Conexiones.CO_SistemaMonitoreo, dop_Operacion).Tables[0];

			List<Morken.SisMon.Entidad.AlertaEstado> lst_AlertaEstado= new List<Morken.SisMon.Entidad.AlertaEstado>();
			Morken.SisMon.Entidad.AlertaEstado obj_AlertaEstado= new Morken.SisMon.Entidad.AlertaEstado();
			foreach (DataRow obj_Row in dt_Resultado.Rows)
			{
				if (lst_AlertaEstado.Count == 0)
					obj_Lista.Total = (int)obj_Row["Total_Filas"];
				obj_AlertaEstado = new Morken.SisMon.Entidad.AlertaEstado();
                
			obj_AlertaEstado.IdAlertaEstado = obj_Row["IdAlertaEstado"] is DBNull ? null : (int?)obj_Row["IdAlertaEstado"];
			obj_AlertaEstado.Descripcion = obj_Row["Descripcion"] is DBNull ? null : obj_Row["Descripcion"].ToString();
			obj_AlertaEstado.Estado = obj_Row["Estado"] is DBNull ? null : obj_Row["Estado"].ToString();
			obj_AlertaEstado.UsuarioCreacion = obj_Row["UsuarioCreacion"] is DBNull ? null : obj_Row["UsuarioCreacion"].ToString();
			obj_AlertaEstado.FechaCreacion = obj_Row["FechaCreacion"] is DBNull ? null : (DateTime?)obj_Row["FechaCreacion"];
			obj_AlertaEstado.UsuarioModificacion = obj_Row["UsuarioModificacion"] is DBNull ? null : obj_Row["UsuarioModificacion"].ToString();
			obj_AlertaEstado.FechaModificacion = obj_Row["FechaModificacion"] is DBNull ? null : (DateTime?)obj_Row["FechaModificacion"];
			lst_AlertaEstado.Add(obj_AlertaEstado);
			}

			obj_Lista.Result = lst_AlertaEstado;
			return obj_Lista;
		}

		/// <summary>Obtener AlertaEstado</summary>
		/// <param name="int_pIdAlertaEstado">IdAlertaEstado</param>
		/// <returns>Objeto AlertaEstado</returns>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
		public static Morken.SisMon.Entidad.AlertaEstado Obtener(int int_pIdAlertaEstado)
		{
			Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.AlertaEstado> lst_AlertaEstado = Listar(int_pIdAlertaEstado, null, null, null, null);
			return lst_AlertaEstado.Result.Count > 0 ? lst_AlertaEstado.Result[0] : null;
		}

		/// <summary>Insertar AlertaEstado</summary>
		/// <param name="obj_pAlertaEstado">AlertaEstado</param>
		/// <returns>Id de AlertaEstado</returns>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
		public static int Insertar(Morken.SisMon.Entidad.AlertaEstado obj_pAlertaEstado)
		{
			DataOperation dop_Operacion = new DataOperation("InsertarAlertaEstado");

			dop_Operacion.Parameters.Add(new Parameter("@pDescripcion", obj_pAlertaEstado.Descripcion));
			dop_Operacion.Parameters.Add(new Parameter("@pEstado", obj_pAlertaEstado.Estado));
			dop_Operacion.Parameters.Add(new Parameter("@pUsuarioCreacion", obj_pAlertaEstado.UsuarioCreacion));

			Parameter obj_IdAlertaEstado= new Parameter("@pIdAlertaEstado", DbType.Int32);
			obj_IdAlertaEstado.Direction = ParameterDirection.Output;
			dop_Operacion.Parameters.Add(obj_IdAlertaEstado);

			DataManager.ExecuteNonQuery(Conexiones.CO_SistemaMonitoreo, dop_Operacion, false);
			int int_IdAlertaEstado = (int)obj_IdAlertaEstado.Value;
			return int_IdAlertaEstado;
		}

		/// <summary>Actualizar AlertaEstado</summary>
		/// <param name="obj_pAlertaEstado">AlertaEstado</param>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
		public static void Modificar(Morken.SisMon.Entidad.AlertaEstado obj_pAlertaEstado)
		{
			DataOperation dop_Operacion = new DataOperation("ActualizarAlertaEstado");

			dop_Operacion.Parameters.Add(new Parameter("@pIdAlertaEstado", obj_pAlertaEstado.IdAlertaEstado));
			dop_Operacion.Parameters.Add(new Parameter("@pDescripcion", obj_pAlertaEstado.Descripcion));
			dop_Operacion.Parameters.Add(new Parameter("@pEstado", obj_pAlertaEstado.Estado));
            dop_Operacion.Parameters.Add(new Parameter("@pUsuarioModificacion", obj_pAlertaEstado.UsuarioModificacion));

            DataManager.ExecuteNonQuery(Conexiones.CO_SistemaMonitoreo, dop_Operacion, false);
		}

		#endregion
	}
}