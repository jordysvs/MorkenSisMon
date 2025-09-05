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
	/// <summary>Operador</summary>
	/// <remarks><list type="bullet">
	/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
	/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>

	public class Operador
	{
        #region Metodos Públicos

        /// <summary>Listado de Operador</summary>
        /// <param name="int_pIdOperador">IdOperador</param>
        /// <param name="int_pIdPersona">IdPersona</param>
        /// <param name="str_pNumeroDocumento">NumeroDocumento</param>
        /// <param name="str_pNombreCompleto">NombreCompleto</param>
        /// <param name="str_pEstado">Estado</param>
        /// <param name="int_pNumPagina" >Numero de pagina</param>
        /// <param name="int_pTamPagina" >Tamaño de pagina</param>
        /// <returns>Lista de Operador</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
        /// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
        public static Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Operador> Listar(int? int_pIdOperador, int? int_pIdPersona, string str_pNumeroDocumento, string  str_pNombreCompleto, string str_pEstado,  int? int_pNumPagina, int? int_pTamPagina)
		{
			Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Operador> obj_Lista = new Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Operador>();
			obj_Lista.PageNumber = int_pNumPagina;
			obj_Lista.Total = 0;

			DataOperation dop_Operacion = new DataOperation("ListarOperador");
			dop_Operacion.Parameters.Add(new Parameter("@pIdOperador", int_pIdOperador.HasValue ? int_pIdOperador.Value : (object)DBNull.Value));
			dop_Operacion.Parameters.Add(new Parameter("@pIdPersona", int_pIdPersona.HasValue ? int_pIdPersona.Value : (object)DBNull.Value));
			dop_Operacion.Parameters.Add(new Parameter("@pNumeroDocumento", !string.IsNullOrEmpty(str_pNumeroDocumento) ? str_pNumeroDocumento : (object)DBNull.Value));
			dop_Operacion.Parameters.Add(new Parameter("@pNombreCompleto", !string.IsNullOrEmpty(str_pNombreCompleto) ? str_pNombreCompleto : (object)DBNull.Value));
			dop_Operacion.Parameters.Add(new Parameter("@pEstado", !string.IsNullOrEmpty(str_pEstado) ? str_pEstado : (object)DBNull.Value));
			dop_Operacion.Parameters.Add(new Parameter("@pNumPagina", int_pNumPagina.HasValue ? int_pNumPagina.Value : (object)DBNull.Value));
			dop_Operacion.Parameters.Add(new Parameter("@pTamPagina", int_pTamPagina.HasValue ? int_pTamPagina.Value : (object)DBNull.Value));

			DataTable dt_Resultado = DataManager.ExecuteDataSet(Conexiones.CO_SistemaMonitoreo, dop_Operacion).Tables[0];

			List<Morken.SisMon.Entidad.Operador> lst_Operador= new List<Morken.SisMon.Entidad.Operador>();
			Morken.SisMon.Entidad.Operador obj_Operador= new Morken.SisMon.Entidad.Operador();
            foreach (DataRow obj_Row in dt_Resultado.Rows)
            {
                if (lst_Operador.Count == 0)
                    obj_Lista.Total = (int)obj_Row["Total_Filas"];
                obj_Operador = new Morken.SisMon.Entidad.Operador();
                obj_Operador.IdOperador = obj_Row["IdOperador"] is DBNull ? null : (int?)obj_Row["IdOperador"];
                obj_Operador.IdPersona = obj_Row["IdPersona"] is DBNull ? null : (int?)obj_Row["IdPersona"];
                obj_Operador.Estado = obj_Row["Estado"] is DBNull ? null : obj_Row["Estado"].ToString();
                obj_Operador.UsuarioCreacion = obj_Row["UsuarioCreacion"] is DBNull ? null : obj_Row["UsuarioCreacion"].ToString();
                obj_Operador.FechaCreacion = obj_Row["FechaCreacion"] is DBNull ? null : (DateTime?)obj_Row["FechaCreacion"];
                obj_Operador.UsuarioModificacion = obj_Row["UsuarioModificacion"] is DBNull ? null : obj_Row["UsuarioModificacion"].ToString();
                obj_Operador.FechaModificacion = obj_Row["FechaModificacion"] is DBNull ? null : (DateTime?)obj_Row["FechaModificacion"];

                obj_Operador.Persona = new Kruma.Core.Business.Entity.Persona();
                obj_Operador.Persona.IdPersona = obj_Row["CorePersona_IdPersona"] is DBNull ? null : (int?)obj_Row["CorePersona_IdPersona"];
                obj_Operador.Persona.IdTipoDocumento = obj_Row["CorePersona_IdTipoDocumento"] is DBNull ? null : (int?)obj_Row["CorePersona_IdTipoDocumento"];
                obj_Operador.Persona.NumeroDocumento = obj_Row["CorePersona_NumeroDocumento"] is DBNull ? null : obj_Row["CorePersona_NumeroDocumento"].ToString();
                obj_Operador.Persona.Nombres = obj_Row["CorePersona_Nombres"] is DBNull ? null : obj_Row["CorePersona_Nombres"].ToString();
                obj_Operador.Persona.ApellidoPaterno = obj_Row["CorePersona_ApellidoPaterno"] is DBNull ? null : obj_Row["CorePersona_ApellidoPaterno"].ToString();
                obj_Operador.Persona.ApellidoMaterno = obj_Row["CorePersona_ApellidoMaterno"] is DBNull ? null : obj_Row["CorePersona_ApellidoMaterno"].ToString();

                obj_Operador.Persona.TipoDocumento = new Kruma.Core.Business.Entity.TipoDocumento();
                obj_Operador.Persona.TipoDocumento.IdTipoDocumento = obj_Row["TipoDocumento_IdTipoDocumento"] is DBNull ? null : (int?)obj_Row["TipoDocumento_IdTipoDocumento"];
                obj_Operador.Persona.TipoDocumento.Descripcion = obj_Row["TipoDocumento_Descripcion"] is DBNull ? null : obj_Row["TipoDocumento_Descripcion"].ToString();

                if(!(obj_Row["PersonaMail_IdMail"] is DBNull))
                {
                    obj_Operador.Persona.Mails = new List<Kruma.Core.Business.Entity.PersonaMail>();
                    Kruma.Core.Business.Entity.PersonaMail  obj_Mail = new Kruma.Core.Business.Entity.PersonaMail();
                    obj_Mail.IdMail = obj_Row["PersonaMail_IdMail"] is DBNull ? null : (int?)obj_Row["PersonaMail_IdMail"];
                    obj_Mail.Mail = obj_Row["PersonaMail_Mail"] is DBNull ? null : obj_Row["PersonaMail_Mail"].ToString();
                    obj_Operador.Persona.Mail = obj_Row["PersonaMail_Mail"] is DBNull ? null : obj_Row["PersonaMail_Mail"].ToString();
                    obj_Operador.Persona.Mails.Add(obj_Mail);
                }

                if (!(obj_Row["PersonaTelefono_IdTelefono"] is DBNull))
                {
                    obj_Operador.Persona.Telefonos = new List<Kruma.Core.Business.Entity.PersonaTelefono>();
                    Kruma.Core.Business.Entity.PersonaTelefono obj_Telefono= new Kruma.Core.Business.Entity.PersonaTelefono();
                    obj_Telefono.IdTelefono = obj_Row["PersonaTelefono_IdTelefono"] is DBNull ? null : (int?)obj_Row["PersonaTelefono_IdTelefono"];
                    obj_Telefono.Numero = obj_Row["PersonaTelefono_Numero"] is DBNull ? null : obj_Row["PersonaTelefono_Numero"].ToString();
                    obj_Operador.Persona.Numero = obj_Row["PersonaTelefono_Numero"] is DBNull ? null : obj_Row["PersonaTelefono_Numero"].ToString();
                    obj_Operador.Persona.Telefonos.Add(obj_Telefono);
                }

                lst_Operador.Add(obj_Operador);
			}

			obj_Lista.Result = lst_Operador;
			return obj_Lista;
		}

		/// <summary>Obtener Operador</summary>
		/// <param name="int_pIdOperador">IdOperador</param>
		/// <returns>Objeto Operador</returns>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
		public static Morken.SisMon.Entidad.Operador Obtener(int int_pIdOperador)
		{
			Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Operador> lst_Operador = Listar(int_pIdOperador,null,  null, null, null, null, null);
			return lst_Operador.Result.Count > 0 ? lst_Operador.Result[0] : null;
		}

		/// <summary>Insertar Operador</summary>
		/// <param name="obj_pOperador">Operador</param>
		/// <returns>Id de Operador</returns>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
		public static int Insertar(Morken.SisMon.Entidad.Operador obj_pOperador)
		{
			DataOperation dop_Operacion = new DataOperation("InsertarOperador");

			dop_Operacion.Parameters.Add(new Parameter("@pIdPersona", obj_pOperador.IdPersona));
			dop_Operacion.Parameters.Add(new Parameter("@pEstado", obj_pOperador.Estado));
			dop_Operacion.Parameters.Add(new Parameter("@pUsuarioCreacion", obj_pOperador.UsuarioCreacion));

			Parameter obj_IdOperador= new Parameter("@pIdOperador", DbType.Int32);
			obj_IdOperador.Direction = ParameterDirection.Output;
			dop_Operacion.Parameters.Add(obj_IdOperador);

			DataManager.ExecuteNonQuery(Conexiones.CO_SistemaMonitoreo, dop_Operacion, false);
			int int_IdOperador = (int)obj_IdOperador.Value;
			return int_IdOperador;
		}

		/// <summary>Actualizar Operador</summary>
		/// <param name="obj_pOperador">Operador</param>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
		public static void Modificar(Morken.SisMon.Entidad.Operador obj_pOperador)
		{
			DataOperation dop_Operacion = new DataOperation("ActualizarOperador");

			dop_Operacion.Parameters.Add(new Parameter("@pIdOperador", obj_pOperador.IdOperador));
			dop_Operacion.Parameters.Add(new Parameter("@pIdPersona", obj_pOperador.IdPersona));
			dop_Operacion.Parameters.Add(new Parameter("@pEstado", obj_pOperador.Estado));
            dop_Operacion.Parameters.Add(new Parameter("@pUsuarioModificacion", obj_pOperador.UsuarioModificacion));

            DataManager.ExecuteNonQuery(Conexiones.CO_SistemaMonitoreo, dop_Operacion, false);
		}

		#endregion
	}
}