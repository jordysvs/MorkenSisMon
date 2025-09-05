using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kruma.Core.Util.Common;

namespace Morken.SisMon.Negocio
{
	/// <summary>Canal</summary>
	/// <remarks><list type="bullet">
	/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
	/// <item><FecCrea>24-10-2017</FecCrea></item></list></remarks>

	public class Canal
	{
		#region Metodos Públicos

		/// <summary>Listado de Canal</summary>
		/// <param name="int_pIdCanal">IdCanal</param>
		/// <param name="str_pDescripcion">Descripcion</param>
		/// <param name="str_pEstado">Estado</param>
		/// <param name="int_pNumPagina" >Numero de pagina</param>
		/// <param name="int_pTamPagina" >Tamaño de pagina</param>
		/// <returns>Lista de Canal</returns>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>24-10-2017</FecCrea></item></list></remarks>
		public static Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Canal> Listar(int? int_pIdCanal, string str_pDescripcion, string str_pEstado,  int? int_pNumPagina, int? int_pTamPagina)
		{
			return Morken.SisMon.Data.Canal.Listar(int_pIdCanal, str_pDescripcion, str_pEstado, int_pNumPagina, int_pTamPagina);
		}

		/// <summary>Obtener Canal</summary>
		/// <param name="int_pIdCanal">IdCanal</param>
		/// <returns>Objeto Canal</returns>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>24-10-2017</FecCrea></item></list></remarks>
		public static Morken.SisMon.Entidad.Canal Obtener(int int_pIdCanal)
		{
			return Morken.SisMon.Data.Canal.Obtener(int_pIdCanal);
		}

		/// <summary>Insertar Canal</summary>
		/// <param name="obj_pCanal">Canal</param>
		/// <returns>Id de Canal</returns>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>24-10-2017</FecCrea></item></list></remarks>
		public static ProcessResult Insertar(Morken.SisMon.Entidad.Canal obj_pCanal)
		{
			ProcessResult obj_Resultado = null;
			try
			{
				int int_IdCanal = Morken.SisMon.Data.Canal.Insertar(obj_pCanal);
				obj_Resultado = new ProcessResult(int_IdCanal);
			}
			catch (Exception obj_pExcepcion)
			{
				obj_Resultado = new ProcessResult(obj_pExcepcion);
			}
			return obj_Resultado;
		}

		/// <summary>Actualizar Canal</summary>
		/// <param name="obj_pCanal">Canal</param>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>24-10-2017</FecCrea></item></list></remarks>
		public static ProcessResult Modificar(Morken.SisMon.Entidad.Canal obj_pCanal)
		{
			ProcessResult obj_Resultado = null;
			try
			{
				Morken.SisMon.Data.Canal.Modificar(obj_pCanal);
				obj_Resultado = new ProcessResult(obj_pCanal.IdCanal);
			}
			catch (Exception obj_pExcepcion)
			{
				obj_Resultado = new ProcessResult(obj_pExcepcion);
			}
			return obj_Resultado;
		}

		/// <summary>Modificar Estado Canal</summary>
		/// <param name="obj_pCanal">Canal</param>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>24-10-2017</FecCrea></item></list></remarks>
		public static ProcessResult ModificarEstado(Morken.SisMon.Entidad.Canal obj_pCanal)
		{
			ProcessResult obj_Resultado = null;
			try
			{
                Morken.SisMon.Entidad.Canal obj_Canal = Morken.SisMon.Data.Canal.Obtener(obj_pCanal.IdCanal.Value);
				if(obj_Canal.Estado == obj_pCanal.Estado)
				{
					string str_Mensaje = obj_pCanal.Estado == Morken.SisMon.Entidad.Constante.Estado_Activo ? "El canal ya se encuentra activo." : "El canal ya se encuentra inactivo.";
					return new ProcessResult(new Exception(str_Mensaje), str_Mensaje);
				}
					obj_Canal.Estado = obj_pCanal.Estado;
					obj_Canal.UsuarioModificacion = obj_pCanal.UsuarioModificacion;
					Morken.SisMon.Data.Canal.Modificar(obj_Canal);

					obj_Resultado = new ProcessResult(obj_pCanal.IdCanal);
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