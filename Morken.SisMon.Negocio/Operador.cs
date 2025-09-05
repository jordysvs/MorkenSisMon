using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kruma.Core.Util.Common;
using System.Transactions;

namespace Morken.SisMon.Negocio
{
	/// <summary>Operador</summary>
	/// <remarks><list type="bullet">
	/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
	/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>

	public class Operador
	{
        #region Metodos Públicos

        // <summary>Listado de Operador</summary>
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
        public static Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Operador> Listar(int? int_pIdOperador, int? int_pIdPersona, string str_pNumeroDocumento, string str_pNombreCompleto, string str_pEstado, int? int_pNumPagina, int? int_pTamPagina)
        {
            return Morken.SisMon.Data.Operador.Listar(int_pIdOperador, int_pIdPersona, str_pNumeroDocumento, str_pNombreCompleto, str_pEstado, int_pNumPagina, int_pTamPagina);
		}

		/// <summary>Obtener Operador</summary>
		/// <param name="int_pIdOperador">IdOperador</param>
		/// <returns>Objeto Operador</returns>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
		public static Morken.SisMon.Entidad.Operador Obtener(int int_pIdOperador)
		{
			return Morken.SisMon.Data.Operador.Obtener(int_pIdOperador);
		}

		/// <summary>Insertar Operador</summary>
		/// <param name="obj_pOperador">Operador</param>
		/// <returns>Id de Operador</returns>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
		public static ProcessResult Insertar(Morken.SisMon.Entidad.Operador obj_pOperador)
		{
			ProcessResult obj_Resultado = null;
			try
			{
                //Validación de existencia del operador
                if (Morken.SisMon.Data.Operador.Listar(null, null, obj_pOperador.Persona.NumeroDocumento, null, null, null, null).Result.Count > 0)
                {
                    string str_Mensaje = "Ya existe el operador en el sistema.";
                    return new Kruma.Core.Util.Common.ProcessResult(new Exception(str_Mensaje), str_Mensaje);
                }

                //Transaccion
                using (TransactionScope obj_Transaction = new TransactionScope())
                {
                    //Persona
                    Kruma.Core.Business.Entity.Persona obj_PersonaAModificar = obj_pOperador.Persona;
                    //Verifica si existe la persona
                    System.Collections.Generic.List<Kruma.Core.Business.Entity.Persona> lst_Persona = Kruma.Core.Business.Logical.Persona.Listar(null, obj_pOperador.Persona.IdTipoDocumento, obj_pOperador.Persona.NumeroDocumento, null, null, null, null, null).Result;
                    if (lst_Persona.Count > 0)
                    {
                        //Modificar los datos de la persona
                        obj_PersonaAModificar = lst_Persona[0];
                        obj_PersonaAModificar.Nombres = obj_pOperador.Persona.Nombres;
                        obj_PersonaAModificar.ApellidoPaterno = obj_pOperador.Persona.ApellidoPaterno;
                        obj_PersonaAModificar.ApellidoMaterno = obj_pOperador.Persona.ApellidoMaterno;
                        obj_PersonaAModificar.RazonSocial = obj_pOperador.Persona.RazonSocial;
                        obj_PersonaAModificar.NombreComercial = obj_pOperador.Persona.NombreComercial;
                        obj_PersonaAModificar.Estado = Kruma.Core.Business.Entity.Constante.Estado_Activo;
                        obj_PersonaAModificar.UsuarioModificacion = obj_pOperador.UsuarioCreacion;
                        Kruma.Core.Business.Data.Persona.Modificar(obj_PersonaAModificar);
                        obj_pOperador.IdPersona = obj_PersonaAModificar.IdPersona;
                    }
                    else
                    {
                        //Insercion de los datos de la persona
                        obj_PersonaAModificar.Estado = Kruma.Core.Business.Entity.Constante.Estado_Activo;
                        obj_PersonaAModificar.UsuarioCreacion = obj_pOperador.UsuarioCreacion;
                        obj_PersonaAModificar.UsuarioModificacion = obj_pOperador.UsuarioCreacion;
                        obj_pOperador.IdPersona = Kruma.Core.Business.Data.Persona.Insertar(obj_PersonaAModificar);
                    }

                    if (obj_pOperador.Persona.Telefonos.Count > 0)
                    {
                        //Telefono
                        Kruma.Core.Business.Entity.PersonaTelefono obj_PersonaTelefono = obj_pOperador.Persona.Telefonos[0];
                        Kruma.Core.Business.Entity.PersonaTelefono obj_PersonaTelefonoAModificar = obj_PersonaTelefono;

                        System.Collections.Generic.List<Kruma.Core.Business.Entity.PersonaTelefono> lst_PersonaTelefono =
                            Kruma.Core.Business.Logical.PersonaTelefono.Listar(
                                obj_pOperador.IdPersona, null, null,
                                obj_PersonaTelefono.Numero, null, null, null, null).Result;

                        if (lst_PersonaTelefono.Count > 0)
                        {
                            obj_PersonaTelefonoAModificar = lst_PersonaTelefono[0];
                            obj_PersonaTelefonoAModificar.Principal = Kruma.Core.Business.Entity.Constante.Condicion_Positivo;
                            obj_PersonaTelefonoAModificar.Numero = obj_PersonaTelefono.Numero;
                            obj_PersonaTelefonoAModificar.UsuarioModificacion = obj_PersonaTelefono.UsuarioModificacion;
                            Kruma.Core.Business.Data.PersonaTelefono.Modificar(obj_PersonaTelefonoAModificar);
                            obj_PersonaTelefono.IdTelefono = obj_PersonaTelefonoAModificar.IdTelefono;
                        }
                        else
                        {
                            //Insercion de los datos del telefono
                            obj_PersonaTelefonoAModificar.IdPersona = obj_pOperador.IdPersona;
                            obj_PersonaTelefonoAModificar.IdTelefonoTipo = Kruma.Core.Business.Entity.TelefonoTipo.Trabajo;
                            obj_PersonaTelefonoAModificar.Numero = obj_PersonaTelefono.Numero;
                            obj_PersonaTelefonoAModificar.Principal = Kruma.Core.Business.Entity.Constante.Condicion_Positivo;
                            obj_PersonaTelefonoAModificar.Estado = Kruma.Core.Business.Entity.Constante.Estado_Activo;
                            obj_PersonaTelefonoAModificar.UsuarioCreacion = obj_pOperador.UsuarioCreacion;
                            obj_PersonaTelefonoAModificar.UsuarioModificacion = obj_pOperador.UsuarioCreacion;
                            obj_PersonaTelefono.IdTelefono = Kruma.Core.Business.Data.PersonaTelefono.Insertar(obj_PersonaTelefonoAModificar);
                        }
                    }

                    if (obj_pOperador.Persona.Mails.Count > 0)
                    {
                        //Mail
                        Kruma.Core.Business.Entity.PersonaMail obj_PersonaMail = obj_pOperador.Persona.Mails[0];
                        Kruma.Core.Business.Entity.PersonaMail obj_PersonaMailAModificar = obj_PersonaMail;

                        System.Collections.Generic.List<Kruma.Core.Business.Entity.PersonaMail> lst_PersonaMail =
                            Kruma.Core.Business.Logical.PersonaMail.Listar(
                                obj_pOperador.IdPersona, null, obj_PersonaMail.Mail, null,
                                null, null, null, null).Result;

                        if (lst_PersonaMail.Count > 0)
                        {
                            obj_PersonaMailAModificar = lst_PersonaMail[0];
                            obj_PersonaMailAModificar.Mail = obj_PersonaMail.Mail;
                            obj_PersonaMailAModificar.Principal = Kruma.Core.Business.Entity.Constante.Condicion_Positivo;
                            obj_PersonaMailAModificar.UsuarioModificacion = obj_PersonaMail.UsuarioModificacion;
                            Kruma.Core.Business.Data.PersonaMail.Modificar(obj_PersonaMailAModificar);
                            obj_PersonaMail.IdMail = obj_PersonaMailAModificar.IdMail;
                        }
                        else
                        {
                            //Insercion de los datos del telefono
                            obj_PersonaMailAModificar.IdPersona = obj_pOperador.IdPersona;
                            obj_PersonaMailAModificar.IdMailTipo = Kruma.Core.Business.Entity.MailTipo.Trabajo;
                            obj_PersonaMailAModificar.Mail = obj_PersonaMail.Mail;
                            obj_PersonaMailAModificar.Principal = Kruma.Core.Business.Entity.Constante.Condicion_Positivo;
                            obj_PersonaMailAModificar.Estado = Kruma.Core.Business.Entity.Constante.Estado_Activo;
                            obj_PersonaMailAModificar.UsuarioCreacion = obj_pOperador.UsuarioCreacion;
                            obj_PersonaMailAModificar.UsuarioModificacion = obj_pOperador.UsuarioCreacion;
                            obj_PersonaMail.IdMail = Kruma.Core.Business.Data.PersonaMail.Insertar(obj_PersonaMailAModificar);
                        }
                    }

                    int int_IdOperador = Morken.SisMon.Data.Operador.Insertar(obj_pOperador);

                    //Generacion del Usuario
                    if (int_IdOperador > 0)
                    {
                        string str_IdUsuario = obj_pOperador.Persona.NumeroDocumento;
                        if (Kruma.Core.Security.Data.Usuario.Obtener(str_IdUsuario, null) == null)
                        {
                            //Usuario
                            Kruma.Core.Security.Entity.Usuario obj_Usuario = new Kruma.Core.Security.Entity.Usuario();
                            obj_Usuario.IdUsuario = str_IdUsuario;
                            obj_Usuario.Clave = str_IdUsuario;
                            obj_Usuario.UsuarioRed = string.Empty;
                            obj_Usuario.IdPersona = obj_pOperador.IdPersona;
                            obj_Usuario.FlagExpiracion = Morken.SisMon.Entidad.Constante.Condicion_Negativo;
                            obj_Usuario.FechaExpiracion = null;
                            obj_Usuario.Estado = Morken.SisMon.Entidad.Constante.Estado_Activo;

                            //Parametro de Perfil
                            Kruma.Core.Business.Entity.Parametro obj_Parametro = Kruma.Core.Business.Data.Parametro.Obtener(
                                Kruma.Core.Business.Entity.Constante.Parametro.Modulo,
                                Morken.SisMon.Entidad.Constante.Parametro.Perfil_Operador);

                            //Perfil del Usuario
                            Kruma.Core.Security.Entity.PerfilUsuario obj_PerfilUsuario = new Kruma.Core.Security.Entity.PerfilUsuario();
                            obj_PerfilUsuario.IdUsuario = obj_Usuario.IdUsuario;
                            obj_PerfilUsuario.IdModulo = Morken.SisMon.Entidad.Constante.Parametro.Modulo;
                            obj_PerfilUsuario.IdPerfil = obj_Parametro.Valor;
                            obj_PerfilUsuario.Estado = Morken.SisMon.Entidad.Constante.Estado_Activo;
                            obj_PerfilUsuario.UsuarioCreacion = obj_pOperador.UsuarioCreacion;
                            obj_Usuario.Perfiles.Add(obj_PerfilUsuario);

                            //Generar Usuario
                            obj_Resultado = Kruma.Core.Security.Logical.Usuario.Insertar(obj_Usuario);
                            if (obj_Resultado.OperationResult != Kruma.Core.Util.Common.Enum.OperationResult.Success)
                                return obj_Resultado;
                        }
                    }

                    obj_Resultado = new ProcessResult(int_IdOperador);

                    obj_Transaction.Complete();
                }
			}
			catch (Exception obj_pExcepcion)
			{
				obj_Resultado = new ProcessResult(obj_pExcepcion);
			}
			return obj_Resultado;
		}

		/// <summary>Actualizar Operador</summary>
		/// <param name="obj_pOperador">Operador</param>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
		public static ProcessResult Modificar(Morken.SisMon.Entidad.Operador obj_pOperador)
		{
			ProcessResult obj_Resultado = null;
			try
			{
                //Transaccion
                using (TransactionScope obj_Transaction = new TransactionScope())
                {
                    //Persona
                    Kruma.Core.Business.Entity.Persona obj_PersonaAModificar = Kruma.Core.Business.Data.Persona.Obtener(obj_pOperador.IdPersona);

                    //Modificar los datos de la persona
                    obj_PersonaAModificar.Nombres = obj_pOperador.Persona.Nombres;
                    obj_PersonaAModificar.ApellidoPaterno = obj_pOperador.Persona.ApellidoPaterno;
                    obj_PersonaAModificar.ApellidoMaterno = obj_pOperador.Persona.ApellidoMaterno;
                    obj_PersonaAModificar.RazonSocial = obj_pOperador.Persona.RazonSocial;
                    obj_PersonaAModificar.NombreComercial = obj_pOperador.Persona.NombreComercial;
                    obj_PersonaAModificar.Estado = Kruma.Core.Business.Entity.Constante.Estado_Activo;
                    obj_PersonaAModificar.UsuarioModificacion = obj_pOperador.UsuarioCreacion;
                    Kruma.Core.Business.Data.Persona.Modificar(obj_PersonaAModificar);
                    obj_pOperador.IdPersona = obj_PersonaAModificar.IdPersona;

                    if (obj_pOperador.Persona.Telefonos.Count > 0)
                    {
                        //Telefono
                        Kruma.Core.Business.Entity.PersonaTelefono obj_PersonaTelefono = obj_pOperador.Persona.Telefonos[0];
                        Kruma.Core.Business.Entity.PersonaTelefono obj_PersonaTelefonoAModificar = obj_PersonaTelefono;

                        System.Collections.Generic.List<Kruma.Core.Business.Entity.PersonaTelefono> lst_PersonaTelefono =
                            Kruma.Core.Business.Logical.PersonaTelefono.Listar(
                                obj_pOperador.IdPersona, null, null,
                                obj_PersonaTelefono.Numero, null, null, null, null).Result;

                        if (lst_PersonaTelefono.Count > 0)
                        {
                            obj_PersonaTelefonoAModificar = lst_PersonaTelefono[0];
                            obj_PersonaTelefonoAModificar.Principal = Kruma.Core.Business.Entity.Constante.Condicion_Positivo;
                            obj_PersonaTelefonoAModificar.Numero = obj_PersonaTelefono.Numero;
                            obj_PersonaTelefonoAModificar.UsuarioModificacion = obj_PersonaTelefono.UsuarioModificacion;
                            Kruma.Core.Business.Data.PersonaTelefono.Modificar(obj_PersonaTelefonoAModificar);
                            obj_PersonaTelefono.IdTelefono = obj_PersonaTelefonoAModificar.IdTelefono;
                        }
                        else
                        {
                            //Insercion de los datos del telefono
                            obj_PersonaTelefonoAModificar.IdPersona = obj_pOperador.IdPersona;
                            obj_PersonaTelefonoAModificar.IdTelefonoTipo = Kruma.Core.Business.Entity.TelefonoTipo.Trabajo;
                            obj_PersonaTelefonoAModificar.Numero = obj_PersonaTelefono.Numero;
                            obj_PersonaTelefonoAModificar.Principal = Kruma.Core.Business.Entity.Constante.Condicion_Positivo;
                            obj_PersonaTelefonoAModificar.Estado = Kruma.Core.Business.Entity.Constante.Estado_Activo;
                            obj_PersonaTelefonoAModificar.UsuarioCreacion = obj_pOperador.UsuarioCreacion;
                            obj_PersonaTelefonoAModificar.UsuarioModificacion = obj_pOperador.UsuarioCreacion;
                            obj_PersonaTelefono.IdTelefono = Kruma.Core.Business.Data.PersonaTelefono.Insertar(obj_PersonaTelefonoAModificar);
                        }
                    }

                    if (obj_pOperador.Persona.Mails.Count > 0)
                    {
                        //Mail
                        Kruma.Core.Business.Entity.PersonaMail obj_PersonaMail = obj_pOperador.Persona.Mails[0];
                        Kruma.Core.Business.Entity.PersonaMail obj_PersonaMailAModificar = obj_PersonaMail;

                        System.Collections.Generic.List<Kruma.Core.Business.Entity.PersonaMail> lst_PersonaMail =
                            Kruma.Core.Business.Logical.PersonaMail.Listar(
                                obj_pOperador.IdPersona, null, obj_PersonaMail.Mail, null,
                                null, null, null, null).Result;

                        if (lst_PersonaMail.Count > 0)
                        {
                            obj_PersonaMailAModificar = lst_PersonaMail[0];
                            obj_PersonaMailAModificar.Mail = obj_PersonaMail.Mail;
                            obj_PersonaMailAModificar.Principal = Kruma.Core.Business.Entity.Constante.Condicion_Positivo;
                            obj_PersonaMailAModificar.UsuarioModificacion = obj_PersonaMail.UsuarioModificacion;
                            Kruma.Core.Business.Data.PersonaMail.Modificar(obj_PersonaMailAModificar);
                            obj_PersonaMail.IdMail = obj_PersonaMailAModificar.IdMail;
                        }
                        else
                        {
                            //Insercion de los datos del telefono
                            obj_PersonaMailAModificar.IdPersona = obj_pOperador.IdPersona;
                            obj_PersonaMailAModificar.IdMailTipo = Kruma.Core.Business.Entity.MailTipo.Trabajo;
                            obj_PersonaMailAModificar.Mail = obj_PersonaMail.Mail;
                            obj_PersonaMailAModificar.Principal = Kruma.Core.Business.Entity.Constante.Condicion_Positivo;
                            obj_PersonaMailAModificar.Estado = Kruma.Core.Business.Entity.Constante.Estado_Activo;
                            obj_PersonaMailAModificar.UsuarioCreacion = obj_pOperador.UsuarioCreacion;
                            obj_PersonaMailAModificar.UsuarioModificacion = obj_pOperador.UsuarioCreacion;
                            obj_PersonaMail.IdMail = Kruma.Core.Business.Data.PersonaMail.Insertar(obj_PersonaMailAModificar);
                        }
                    }

                    Morken.SisMon.Data.Operador.Modificar(obj_pOperador);

                    //Generacion del Usuario
                    string str_IdUsuario = obj_pOperador.Persona.NumeroDocumento;
                    if (Kruma.Core.Security.Data.Usuario.Obtener(str_IdUsuario, null) == null)
                    {
                        //Usuario
                        Kruma.Core.Security.Entity.Usuario obj_Usuario = new Kruma.Core.Security.Entity.Usuario();
                        obj_Usuario.IdUsuario = str_IdUsuario;
                        obj_Usuario.Clave = str_IdUsuario;
                        obj_Usuario.UsuarioRed = string.Empty;
                        obj_Usuario.IdPersona = obj_pOperador.IdPersona;
                        obj_Usuario.FlagExpiracion = Morken.SisMon.Entidad.Constante.Condicion_Negativo;
                        obj_Usuario.FechaExpiracion = null;
                        obj_Usuario.Estado = Morken.SisMon.Entidad.Constante.Estado_Activo;

                        //Parametro de Perfil 
                        Kruma.Core.Business.Entity.Parametro obj_Parametro = Kruma.Core.Business.Data.Parametro.Obtener(
                            Kruma.Core.Business.Entity.Constante.Parametro.Modulo,
                            Morken.SisMon.Entidad.Constante.Parametro.Perfil_Operador);

                        //Perfil del Usuario
                        Kruma.Core.Security.Entity.PerfilUsuario obj_PerfilUsuario = new Kruma.Core.Security.Entity.PerfilUsuario();
                        obj_PerfilUsuario.IdUsuario = obj_Usuario.IdUsuario;
                        obj_PerfilUsuario.IdModulo = Morken.SisMon.Entidad.Constante.Parametro.Modulo;
                        obj_PerfilUsuario.IdPerfil = obj_Parametro.Valor;
                        obj_PerfilUsuario.Estado = Morken.SisMon.Entidad.Constante.Estado_Activo;
                        obj_PerfilUsuario.UsuarioCreacion = obj_pOperador.UsuarioCreacion;
                        obj_Usuario.Perfiles.Add(obj_PerfilUsuario);

                        //Generar Usuario
                        obj_Resultado = Kruma.Core.Security.Logical.Usuario.Insertar(obj_Usuario);
                        if (obj_Resultado.OperationResult != Kruma.Core.Util.Common.Enum.OperationResult.Success)
                            return obj_Resultado;
                    }

                    obj_Resultado = new ProcessResult(obj_pOperador.IdOperador);
                    obj_Transaction.Complete();
                }

			}
			catch (Exception obj_pExcepcion)
			{
				obj_Resultado = new ProcessResult(obj_pExcepcion);
			}
			return obj_Resultado;
		}

		/// <summary>Modificar Estado Operador</summary>
		/// <param name="obj_pOperador">Operador</param>
		/// <remarks><list type="bullet">
		/// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
		/// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
		public static ProcessResult ModificarEstado(Morken.SisMon.Entidad.Operador obj_pOperador)
		{
			ProcessResult obj_Resultado = null;
			try
			{
                Morken.SisMon.Entidad.Operador obj_Operador = Morken.SisMon.Data.Operador.Obtener(obj_pOperador.IdOperador.Value);
				if(obj_Operador.Estado == obj_pOperador.Estado)
				{
					string str_Mensaje = obj_pOperador.Estado == Morken.SisMon.Entidad.Constante.Estado_Activo ? "El operador ya se encuentra activo." : "El operador ya se encuentra inactivo.";
					return new ProcessResult(new Exception(str_Mensaje), str_Mensaje);
				}
					obj_Operador.Estado = obj_pOperador.Estado;
					obj_Operador.UsuarioModificacion = obj_pOperador.UsuarioModificacion;
					Morken.SisMon.Data.Operador.Modificar(obj_Operador);

					obj_Resultado = new ProcessResult(obj_pOperador.IdOperador);
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