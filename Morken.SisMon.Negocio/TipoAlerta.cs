using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kruma.Core.Util.Common;

namespace Morken.SisMon.Negocio
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
			return Morken.SisMon.Data.TipoAlerta.Listar(int_pIdTipoAlerta, str_pDescripcion, str_pEstado, int_pNumPagina, int_pTamPagina);
		}

		/// <summary>Obtener TipoAlerta</summary>
		/// <param name="int_pIdTipoAlerta">IdTipoAlerta</param>
		/// <returns>Objeto TipoAlerta</returns>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
		public static Morken.SisMon.Entidad.TipoAlerta Obtener(int int_pIdTipoAlerta)
		{
			return Morken.SisMon.Data.TipoAlerta.Obtener(int_pIdTipoAlerta);
		}

		/// <summary>Insertar TipoAlerta</summary>
		/// <param name="obj_pTipoAlerta">TipoAlerta</param>
		/// <returns>Id de TipoAlerta</returns>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
		public static ProcessResult Insertar(Morken.SisMon.Entidad.TipoAlerta obj_pTipoAlerta)
		{
			ProcessResult obj_Resultado = null;
			try
			{
				int int_IdTipoAlerta = Morken.SisMon.Data.TipoAlerta.Insertar(obj_pTipoAlerta);
				obj_Resultado = new ProcessResult(int_IdTipoAlerta);
			}
			catch (Exception obj_pExcepcion)
			{
				obj_Resultado = new ProcessResult(obj_pExcepcion);
			}
			return obj_Resultado;
		}

		/// <summary>Actualizar TipoAlerta</summary>
		/// <param name="obj_pTipoAlerta">TipoAlerta</param>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
		public static ProcessResult Modificar(Morken.SisMon.Entidad.TipoAlerta obj_pTipoAlerta)
		{
			ProcessResult obj_Resultado = null;
			try
			{
				Morken.SisMon.Data.TipoAlerta.Modificar(obj_pTipoAlerta);
				obj_Resultado = new ProcessResult(obj_pTipoAlerta.IdTipoAlerta);
			}
			catch (Exception obj_pExcepcion)
			{
				obj_Resultado = new ProcessResult(obj_pExcepcion);
			}
			return obj_Resultado;
		}

		/// <summary>Modificar Estado TipoAlerta</summary>
		/// <param name="obj_pTipoAlerta">TipoAlerta</param>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
		public static ProcessResult ModificarEstado(Morken.SisMon.Entidad.TipoAlerta obj_pTipoAlerta)
		{
			ProcessResult obj_Resultado = null;
			try
			{
                Morken.SisMon.Entidad.TipoAlerta obj_TipoAlerta = Morken.SisMon.Data.TipoAlerta.Obtener(obj_pTipoAlerta.IdTipoAlerta.Value);
				if(obj_TipoAlerta.Estado == obj_pTipoAlerta.Estado)
				{
					string str_Mensaje = obj_pTipoAlerta.Estado == Morken.SisMon.Entidad.Constante.Estado_Activo ? "El tipo de alerta ya se encuentra activo." : "El tipo de alerta ya se encuentra inactivo.";
					return new ProcessResult(new Exception(str_Mensaje), str_Mensaje);
				}
					obj_TipoAlerta.Estado = obj_pTipoAlerta.Estado;
					obj_TipoAlerta.UsuarioModificacion = obj_pTipoAlerta.UsuarioModificacion;
					Morken.SisMon.Data.TipoAlerta.Modificar(obj_TipoAlerta);

					obj_Resultado = new ProcessResult(obj_pTipoAlerta.IdTipoAlerta);
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