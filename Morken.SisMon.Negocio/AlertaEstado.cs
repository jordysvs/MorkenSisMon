using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kruma.Core.Util.Common;

namespace Morken.SisMon.Negocio
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
			return Morken.SisMon.Data.AlertaEstado.Listar(int_pIdAlertaEstado, str_pDescripcion, str_pEstado, int_pNumPagina, int_pTamPagina);
		}

		/// <summary>Obtener AlertaEstado</summary>
		/// <param name="int_pIdAlertaEstado">IdAlertaEstado</param>
		/// <returns>Objeto AlertaEstado</returns>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
		public static Morken.SisMon.Entidad.AlertaEstado Obtener(int int_pIdAlertaEstado)
		{
			return Morken.SisMon.Data.AlertaEstado.Obtener(int_pIdAlertaEstado);
		}

		/// <summary>Insertar AlertaEstado</summary>
		/// <param name="obj_pAlertaEstado">AlertaEstado</param>
		/// <returns>Id de AlertaEstado</returns>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
		public static ProcessResult Insertar(Morken.SisMon.Entidad.AlertaEstado obj_pAlertaEstado)
		{
			ProcessResult obj_Resultado = null;
			try
			{
				int int_IdAlertaEstado = Morken.SisMon.Data.AlertaEstado.Insertar(obj_pAlertaEstado);
				obj_Resultado = new ProcessResult(int_IdAlertaEstado);
			}
			catch (Exception obj_pExcepcion)
			{
				obj_Resultado = new ProcessResult(obj_pExcepcion);
			}
			return obj_Resultado;
		}

		/// <summary>Actualizar AlertaEstado</summary>
		/// <param name="obj_pAlertaEstado">AlertaEstado</param>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
		public static ProcessResult Modificar(Morken.SisMon.Entidad.AlertaEstado obj_pAlertaEstado)
		{
			ProcessResult obj_Resultado = null;
			try
			{
				Morken.SisMon.Data.AlertaEstado.Modificar(obj_pAlertaEstado);
				obj_Resultado = new ProcessResult(obj_pAlertaEstado.IdAlertaEstado);
			}
			catch (Exception obj_pExcepcion)
			{
				obj_Resultado = new ProcessResult(obj_pExcepcion);
			}
			return obj_Resultado;
		}

		/// <summary>Modificar Estado AlertaEstado</summary>
		/// <param name="obj_pAlertaEstado">AlertaEstado</param>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
		public static ProcessResult ModificarEstado(Morken.SisMon.Entidad.AlertaEstado obj_pAlertaEstado)
		{
			ProcessResult obj_Resultado = null;
			try
			{
                Morken.SisMon.Entidad.AlertaEstado obj_AlertaEstado = Morken.SisMon.Data.AlertaEstado.Obtener(obj_pAlertaEstado.IdAlertaEstado.Value);
				if(obj_AlertaEstado.Estado == obj_pAlertaEstado.Estado)
				{
					string str_Mensaje = obj_pAlertaEstado.Estado == Morken.SisMon.Entidad.Constante.Estado_Activo ? "El estado ya se encuentra activo." : "El estado ya se encuentra inactivo.";
					return new ProcessResult(new Exception(str_Mensaje), str_Mensaje);
				}
					obj_AlertaEstado.Estado = obj_pAlertaEstado.Estado;
					obj_AlertaEstado.UsuarioModificacion = obj_pAlertaEstado.UsuarioModificacion;
					Morken.SisMon.Data.AlertaEstado.Modificar(obj_AlertaEstado);

					obj_Resultado = new ProcessResult(obj_pAlertaEstado.IdAlertaEstado);
			}
			catch (Exception obj_pExcepcion)
			{
				obj_Resultado = new ProcessResult(obj_pExcepcion);
			}

			return obj_Resultado;
		}

		#endregion
	}
}