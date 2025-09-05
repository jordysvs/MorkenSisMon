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
        public static Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Alerta> Listar(int? int_pIdAlerta, int? int_pIdCanal, int? int_pIdSegmento, int? int_pCodigoError, int? int_pIdTipoAlerta, string str_pIdUsuario, DateTime? dt_pFechaAlerta, DateTime? dt_pHoraInicial, DateTime? dt_pHoraFinal, decimal? dec_pPosicionInicial, decimal? dec_pPosicionFinal, int? int_pIdAlertaEstado, int? int_pIdOperador, DateTime? dt_pFechaInforme, DateTime? dt_pFechaMitigacion, string str_pObservacion, string str_pEstado, int? int_pNumPagina, int? int_pTamPagina)
        {
            Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Alerta> obj_Lista = new Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Alerta>();
            obj_Lista.PageNumber = int_pNumPagina;
            obj_Lista.Total = 0;

            DataOperation dop_Operacion = new DataOperation("ListarAlerta");
            dop_Operacion.Parameters.Add(new Parameter("@pIdAlerta", int_pIdAlerta.HasValue ? int_pIdAlerta.Value : (object)DBNull.Value));
            dop_Operacion.Parameters.Add(new Parameter("@pIdCanal", int_pIdCanal.HasValue ? int_pIdCanal.Value : (object)DBNull.Value));
            dop_Operacion.Parameters.Add(new Parameter("@pIdSegmento", int_pIdSegmento.HasValue ? int_pIdSegmento.Value : (object)DBNull.Value));
            dop_Operacion.Parameters.Add(new Parameter("@pCodigoError", int_pCodigoError.HasValue ? int_pCodigoError.Value : (object)DBNull.Value));
            dop_Operacion.Parameters.Add(new Parameter("@pIdTipoAlerta", int_pIdTipoAlerta.HasValue ? int_pIdTipoAlerta.Value : (object)DBNull.Value));
            dop_Operacion.Parameters.Add(new Parameter("@pIdUsuario", !string.IsNullOrEmpty(str_pIdUsuario) ? str_pIdUsuario : (object)DBNull.Value));

            Parameter dtm_FechaAlerta = new Parameter("@pFechaAlerta", DbType.DateTime);
            dtm_FechaAlerta.Direction = ParameterDirection.Input;
            dtm_FechaAlerta.Value = dt_pFechaAlerta.HasValue ? dt_pFechaAlerta.Value : (object)DBNull.Value;
            dop_Operacion.Parameters.Add(dtm_FechaAlerta);

            Parameter dtm_HoraInicial = new Parameter("@pHoraInicial", DbType.DateTime);
            dtm_HoraInicial.Direction = ParameterDirection.Input;
            dtm_HoraInicial.Value = dt_pHoraInicial.HasValue ? dt_pHoraInicial.Value : (object)DBNull.Value;
            dop_Operacion.Parameters.Add(dtm_HoraInicial);

            Parameter dtm_HoraFinal = new Parameter("@pHoraFinal", DbType.DateTime);
            dtm_HoraFinal.Direction = ParameterDirection.Input;
            dtm_HoraFinal.Value = dt_pHoraFinal.HasValue ? dt_pHoraFinal.Value : (object)DBNull.Value;
            dop_Operacion.Parameters.Add(dtm_HoraFinal);

            dop_Operacion.Parameters.Add(new Parameter("@pPosicionInicial", dec_pPosicionInicial.HasValue ? dec_pPosicionInicial.Value : (object)DBNull.Value));
            dop_Operacion.Parameters.Add(new Parameter("@pPosicionFinal", dec_pPosicionFinal.HasValue ? dec_pPosicionFinal.Value : (object)DBNull.Value));
            dop_Operacion.Parameters.Add(new Parameter("@pIdAlertaEstado", int_pIdAlertaEstado.HasValue ? int_pIdAlertaEstado.Value : (object)DBNull.Value));
            dop_Operacion.Parameters.Add(new Parameter("@pIdOperador", int_pIdOperador.HasValue ? int_pIdOperador.Value : (object)DBNull.Value));

            Parameter dtm_FechaInforme = new Parameter("@pFechaInforme", DbType.DateTime);
            dtm_FechaInforme.Direction = ParameterDirection.Input;
            dtm_FechaInforme.Value = dt_pFechaInforme.HasValue ? dt_pFechaInforme.Value : (object)DBNull.Value;
            dop_Operacion.Parameters.Add(dtm_FechaInforme);

            Parameter dtm_FechaMitigacion = new Parameter("@pFechaMitigacion", DbType.DateTime);
            dtm_FechaMitigacion.Direction = ParameterDirection.Input;
            dtm_FechaMitigacion.Value = dt_pFechaMitigacion.HasValue ? dt_pFechaMitigacion.Value : (object)DBNull.Value;
            dop_Operacion.Parameters.Add(dtm_FechaMitigacion);

            dop_Operacion.Parameters.Add(new Parameter("@pObservacion", !string.IsNullOrEmpty(str_pObservacion) ? str_pObservacion : (object)DBNull.Value));
            dop_Operacion.Parameters.Add(new Parameter("@pEstado", !string.IsNullOrEmpty(str_pEstado) ? str_pEstado : (object)DBNull.Value));
            dop_Operacion.Parameters.Add(new Parameter("@pNumPagina", int_pNumPagina.HasValue ? int_pNumPagina.Value : (object)DBNull.Value));
            dop_Operacion.Parameters.Add(new Parameter("@pTamPagina", int_pTamPagina.HasValue ? int_pTamPagina.Value : (object)DBNull.Value));

            DataTable dt_Resultado = DataManager.ExecuteDataSet(Conexiones.CO_SistemaMonitoreo, dop_Operacion).Tables[0];

            List<Morken.SisMon.Entidad.Alerta> lst_Alerta = new List<Morken.SisMon.Entidad.Alerta>();
            Morken.SisMon.Entidad.Alerta obj_Alerta = new Morken.SisMon.Entidad.Alerta();
            foreach (DataRow obj_Row in dt_Resultado.Rows)
            {
                if (lst_Alerta.Count == 0)
                    obj_Lista.Total = (int)obj_Row["Total_Filas"];
                obj_Alerta = new Morken.SisMon.Entidad.Alerta();
                obj_Alerta.IdAlerta = obj_Row["IdAlerta"] is DBNull ? null : (int?)obj_Row["IdAlerta"];
                obj_Alerta.IdCanal = obj_Row["IdCanal"] is DBNull ? null : (int?)obj_Row["IdCanal"];
                obj_Alerta.IdSegmento = obj_Row["IdSegmento"] is DBNull ? null : (int?)obj_Row["IdSegmento"];
                obj_Alerta.CodigoError = obj_Row["CodigoError"] is DBNull ? null : (int?)obj_Row["CodigoError"];
                obj_Alerta.MetroInicialSegmento = obj_Row["MetroInicialSegmento"] is DBNull ? null : (int?)obj_Row["MetroInicialSegmento"];
                obj_Alerta.MetroFinalSegmento = obj_Row["MetroFinalSegmento"] is DBNull ? null : (int?)obj_Row["MetroFinalSegmento"];
                obj_Alerta.IdTipoAlerta = obj_Row["IdTipoAlerta"] is DBNull ? null : (int?)obj_Row["IdTipoAlerta"];
                obj_Alerta.IdUsuario = obj_Row["IdUsuario"] is DBNull ? null : obj_Row["IdUsuario"].ToString();
                obj_Alerta.FechaAlerta = obj_Row["FechaAlerta"] is DBNull ? null : (DateTime?)obj_Row["FechaAlerta"];
                obj_Alerta.FechaAsignacion = obj_Row["FechaAsignacion"] is DBNull ? null : (DateTime?)obj_Row["FechaAsignacion"];
                obj_Alerta.PosicionInicial = obj_Row["PosicionInicial"] is DBNull ? null : (decimal?)obj_Row["PosicionInicial"];
                obj_Alerta.PosicionFinal = obj_Row["PosicionFinal"] is DBNull ? null : (decimal?)obj_Row["PosicionFinal"];
                obj_Alerta.ValorUmbral = obj_Row["ValorUmbral"] is DBNull ? null : (decimal?)obj_Row["ValorUmbral"];
                obj_Alerta.ValorUmbralMaximo = obj_Row["ValorUmbralMaximo"] is DBNull ? null : (decimal?)obj_Row["ValorUmbralMaximo"];
                obj_Alerta.CantidadGolpes = obj_Row["CantidadGolpes"] is DBNull ? null : (int?)obj_Row["CantidadGolpes"];
                obj_Alerta.CantidadGolpesMaximo = obj_Row["CantidadGolpesMaximo"] is DBNull ? null : (int?)obj_Row["CantidadGolpesMaximo"];
                obj_Alerta.IdAlertaEstado = obj_Row["IdAlertaEstado"] is DBNull ? null : (int?)obj_Row["IdAlertaEstado"];
                obj_Alerta.IdOperador = obj_Row["IdOperador"] is DBNull ? null : (int?)obj_Row["IdOperador"];
                obj_Alerta.FechaInforme = obj_Row["FechaInforme"] is DBNull ? null : (DateTime?)obj_Row["FechaInforme"];
                obj_Alerta.FechaMitigacion = obj_Row["FechaMitigacion"] is DBNull ? null : (DateTime?)obj_Row["FechaMitigacion"];
                obj_Alerta.Observacion = obj_Row["Observacion"] is DBNull ? null : obj_Row["Observacion"].ToString();
                obj_Alerta.Estado = obj_Row["Estado"] is DBNull ? null : obj_Row["Estado"].ToString();
                obj_Alerta.UsuarioCreacion = obj_Row["UsuarioCreacion"] is DBNull ? null : obj_Row["UsuarioCreacion"].ToString();
                obj_Alerta.FechaCreacion = obj_Row["FechaCreacion"] is DBNull ? null : (DateTime?)obj_Row["FechaCreacion"];
                obj_Alerta.UsuarioModificacion = obj_Row["UsuarioModificacion"] is DBNull ? null : obj_Row["UsuarioModificacion"].ToString();
                obj_Alerta.FechaModificacion = obj_Row["FechaModificacion"] is DBNull ? null : (DateTime?)obj_Row["FechaModificacion"];

                obj_Alerta.TipoAlerta = new Entidad.TipoAlerta();
                obj_Alerta.TipoAlerta.IdTipoAlerta = obj_Row["TipoAlerta_IdTipoAlerta"] is DBNull ? null : (int?)obj_Row["TipoAlerta_IdTipoAlerta"];
                obj_Alerta.TipoAlerta.Descripcion = obj_Row["TipoAlerta_Descripcion"] is DBNull ? null : obj_Row["TipoAlerta_Descripcion"].ToString();

                obj_Alerta.AlertaEstado = new Entidad.AlertaEstado();
                obj_Alerta.AlertaEstado.IdAlertaEstado = obj_Row["AlertaEstado_IdAlertaEstado"] is DBNull ? null : (int?)obj_Row["AlertaEstado_IdAlertaEstado"];
                obj_Alerta.AlertaEstado.Descripcion = obj_Row["AlertaEstado_Descripcion"] is DBNull ? null : obj_Row["AlertaEstado_Descripcion"].ToString();

                if (!(obj_Row["IdUsuario"] is DBNull))
                {
                    obj_Alerta.Usuario = new Kruma.Core.Security.Entity.Usuario();
                    obj_Alerta.Usuario.IdUsuario = obj_Row["SecurityUsuario_IdUsuario"] is DBNull ? null : obj_Row["SecurityUsuario_IdUsuario"].ToString();
                    obj_Alerta.Usuario.IdPersona = obj_Row["SecurityUsuario_IdPersona"] is DBNull ? null : (int?)obj_Row["SecurityUsuario_IdPersona"];
                    obj_Alerta.Usuario.Persona = new Kruma.Core.Business.Entity.Persona();
                    obj_Alerta.Usuario.Persona.IdPersona = obj_Row["CorePersonaUsuario_IdPersona"] is DBNull ? null : (int?)obj_Row["CorePersonaUsuario_IdPersona"];
                    obj_Alerta.Usuario.Persona.Nombres = obj_Row["CorePersonaUsuario_Nombres"] is DBNull ? null : obj_Row["CorePersonaUsuario_Nombres"].ToString();
                    obj_Alerta.Usuario.Persona.ApellidoPaterno = obj_Row["CorePersonaUsuario_ApellidoPaterno"] is DBNull ? null : obj_Row["CorePersonaUsuario_ApellidoPaterno"].ToString();
                    obj_Alerta.Usuario.Persona.ApellidoMaterno = obj_Row["CorePersonaUsuario_ApellidoMaterno"] is DBNull ? null : obj_Row["CorePersonaUsuario_ApellidoMaterno"].ToString();
                }

                if (!(obj_Row["IdOperador"] is DBNull))
                {
                    obj_Alerta.Operador = new Entidad.Operador();
                    obj_Alerta.Operador.IdOperador = obj_Row["Operador_IdOperador"] is DBNull ? null : (int?)obj_Row["Operador_IdOperador"];
                    obj_Alerta.Operador.IdPersona = obj_Row["Operador_IdPersona"] is DBNull ? null : (int?)obj_Row["Operador_IdPersona"];
                    obj_Alerta.Operador.Persona = new Kruma.Core.Business.Entity.Persona();
                    obj_Alerta.Operador.Persona.IdPersona = obj_Row["CorePersonaOperador_IdPersona"] is DBNull ? null : (int?)obj_Row["CorePersonaOperador_IdPersona"];
                    obj_Alerta.Operador.Persona.Nombres = obj_Row["CorePersonaOperador_Nombres"] is DBNull ? null : obj_Row["CorePersonaOperador_Nombres"].ToString();
                    obj_Alerta.Operador.Persona.ApellidoPaterno = obj_Row["CorePersonaOperador_ApellidoPaterno"] is DBNull ? null : obj_Row["CorePersonaOperador_ApellidoPaterno"].ToString();
                    obj_Alerta.Operador.Persona.ApellidoMaterno = obj_Row["CorePersonaOperador_ApellidoMaterno"] is DBNull ? null : obj_Row["CorePersonaOperador_ApellidoMaterno"].ToString();
                }

                lst_Alerta.Add(obj_Alerta);
            }

            obj_Lista.Result = lst_Alerta;
            return obj_Lista;
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
            Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Alerta> lst_Alerta = Listar(int_pIdAlerta, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
            return lst_Alerta.Result.Count > 0 ? lst_Alerta.Result[0] : null;
        }

        /// <summary>Insertar Alerta</summary>
        /// <param name="obj_pAlerta">Alerta</param>
        /// <returns>Id de Alerta</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
        /// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
        public static int Insertar(Morken.SisMon.Entidad.Alerta obj_pAlerta)
        {
            DataOperation dop_Operacion = new DataOperation("InsertarAlerta");
            dop_Operacion.Parameters.Add(new Parameter("@pIdTipoAlerta", obj_pAlerta.IdTipoAlerta));
            dop_Operacion.Parameters.Add(new Parameter("@pIdCanal", obj_pAlerta.IdCanal));
            dop_Operacion.Parameters.Add(new Parameter("@pIdSegmento", obj_pAlerta.IdSegmento));
            dop_Operacion.Parameters.Add(new Parameter("@pCodigoError", obj_pAlerta.CodigoError));
            dop_Operacion.Parameters.Add(new Parameter("@pMetroInicialSegmento", obj_pAlerta.MetroInicialSegmento));
            dop_Operacion.Parameters.Add(new Parameter("@pMetroFinalSegmento", obj_pAlerta.MetroFinalSegmento));
            dop_Operacion.Parameters.Add(new Parameter("@pIdUsuario", obj_pAlerta.IdUsuario));
            Parameter dtm_FechaAlerta = new Parameter("@pFechaAlerta", DbType.DateTime);
            dtm_FechaAlerta.Direction = ParameterDirection.Input;
            dtm_FechaAlerta.Value = obj_pAlerta.FechaAlerta.HasValue ? obj_pAlerta.FechaAlerta.Value : (object)DBNull.Value;
            dop_Operacion.Parameters.Add(dtm_FechaAlerta);
            Parameter dtm_FechaAsignacion = new Parameter("@pFechaAsignacion", DbType.DateTime);
            dtm_FechaAsignacion.Direction = ParameterDirection.Input;
            dtm_FechaAsignacion.Value = obj_pAlerta.FechaAsignacion.HasValue ? obj_pAlerta.FechaAsignacion.Value : (object)DBNull.Value;
            dop_Operacion.Parameters.Add(dtm_FechaAsignacion);
            dop_Operacion.Parameters.Add(new Parameter("@pPosicionInicial", obj_pAlerta.PosicionInicial));
            dop_Operacion.Parameters.Add(new Parameter("@pPosicionFinal", obj_pAlerta.PosicionFinal));
            dop_Operacion.Parameters.Add(new Parameter("@pValorUmbral", obj_pAlerta.ValorUmbral));
            dop_Operacion.Parameters.Add(new Parameter("@pValorUmbralMaximo", obj_pAlerta.ValorUmbralMaximo));
            dop_Operacion.Parameters.Add(new Parameter("@pCantidadGolpes", obj_pAlerta.CantidadGolpes));
            dop_Operacion.Parameters.Add(new Parameter("@pCantidadGolpesMaximo", obj_pAlerta.CantidadGolpesMaximo));
            dop_Operacion.Parameters.Add(new Parameter("@pIdAlertaEstado", obj_pAlerta.IdAlertaEstado));
            dop_Operacion.Parameters.Add(new Parameter("@pIdOperador", obj_pAlerta.IdOperador));
            Parameter dtm_FechaInforme = new Parameter("@pFechaInforme", DbType.DateTime);
            dtm_FechaInforme.Direction = ParameterDirection.Input;
            dtm_FechaInforme.Value = obj_pAlerta.FechaInforme.HasValue ? obj_pAlerta.FechaInforme.Value : (object)DBNull.Value;
            dop_Operacion.Parameters.Add(dtm_FechaInforme);
            Parameter dtm_FechaMitigacion = new Parameter("@pFechaMitigacion", DbType.DateTime);
            dtm_FechaMitigacion.Direction = ParameterDirection.Input;
            dtm_FechaMitigacion.Value = obj_pAlerta.FechaMitigacion.HasValue ? obj_pAlerta.FechaMitigacion.Value : (object)DBNull.Value;
            dop_Operacion.Parameters.Add(dtm_FechaMitigacion);
            dop_Operacion.Parameters.Add(new Parameter("@pObservacion", obj_pAlerta.Observacion));
            dop_Operacion.Parameters.Add(new Parameter("@pEstado", obj_pAlerta.Estado));
            dop_Operacion.Parameters.Add(new Parameter("@pUsuarioCreacion", obj_pAlerta.UsuarioCreacion));

            Parameter obj_IdAlerta = new Parameter("@pIdAlerta", DbType.Int32);
            obj_IdAlerta.Direction = ParameterDirection.Output;
            dop_Operacion.Parameters.Add(obj_IdAlerta);

            DataManager.ExecuteNonQuery(Conexiones.CO_SistemaMonitoreo, dop_Operacion, false);
            int int_IdAlerta = (int)obj_IdAlerta.Value;
            return int_IdAlerta;
        }

        /// <summary>Actualizar Alerta</summary>
        /// <param name="obj_pAlerta">Alerta</param>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
        /// <item><FecCrea>06-10-2017</FecCrea></item></list></remarks>
        public static void Modificar(Morken.SisMon.Entidad.Alerta obj_pAlerta)
        {
            DataOperation dop_Operacion = new DataOperation("ActualizarAlerta");

            //Falta arreglar los parámetros de fecha
            dop_Operacion.Parameters.Add(new Parameter("@pIdAlerta", obj_pAlerta.IdAlerta));
            dop_Operacion.Parameters.Add(new Parameter("@pIdCanal", obj_pAlerta.IdCanal));
            dop_Operacion.Parameters.Add(new Parameter("@pIdSegmento", obj_pAlerta.IdSegmento));
            dop_Operacion.Parameters.Add(new Parameter("@pCodigoError", obj_pAlerta.CodigoError));
            dop_Operacion.Parameters.Add(new Parameter("@pMetroInicialSegmento", obj_pAlerta.MetroInicialSegmento));
            dop_Operacion.Parameters.Add(new Parameter("@pMetroFinalSegmento", obj_pAlerta.MetroFinalSegmento));
            dop_Operacion.Parameters.Add(new Parameter("@pIdTipoAlerta", obj_pAlerta.IdTipoAlerta));
            dop_Operacion.Parameters.Add(new Parameter("@pIdUsuario", obj_pAlerta.IdUsuario));
            Parameter dtm_FechaAlerta = new Parameter("@pFechaAlerta", DbType.DateTime);
            dtm_FechaAlerta.Direction = ParameterDirection.Input;
            dtm_FechaAlerta.Value = obj_pAlerta.FechaAlerta.HasValue ? obj_pAlerta.FechaAlerta.Value : (object)DBNull.Value;
            dop_Operacion.Parameters.Add(dtm_FechaAlerta);
            Parameter dtm_FechaAsignacion = new Parameter("@pFechaAsignacion", DbType.DateTime);
            dtm_FechaAsignacion.Direction = ParameterDirection.Input;
            dtm_FechaAsignacion.Value = obj_pAlerta.FechaAsignacion.HasValue ? obj_pAlerta.FechaAsignacion.Value : (object)DBNull.Value;
            dop_Operacion.Parameters.Add(dtm_FechaAsignacion);
            dop_Operacion.Parameters.Add(new Parameter("@pPosicionInicial", obj_pAlerta.PosicionInicial));
            dop_Operacion.Parameters.Add(new Parameter("@pPosicionFinal", obj_pAlerta.PosicionFinal));
            dop_Operacion.Parameters.Add(new Parameter("@pValorUmbral", obj_pAlerta.ValorUmbral));
            dop_Operacion.Parameters.Add(new Parameter("@pValorUmbralMaximo", obj_pAlerta.ValorUmbralMaximo));
            dop_Operacion.Parameters.Add(new Parameter("@pCantidadGolpes", obj_pAlerta.CantidadGolpes));
            dop_Operacion.Parameters.Add(new Parameter("@pCantidadGolpesMaximo", obj_pAlerta.CantidadGolpesMaximo));
            dop_Operacion.Parameters.Add(new Parameter("@pIdAlertaEstado", obj_pAlerta.IdAlertaEstado));
            dop_Operacion.Parameters.Add(new Parameter("@pIdOperador", obj_pAlerta.IdOperador));
            Parameter dtm_FechaInforme = new Parameter("@pFechaInforme", DbType.DateTime);
            dtm_FechaInforme.Direction = ParameterDirection.Input;
            dtm_FechaInforme.Value = obj_pAlerta.FechaInforme.HasValue ? obj_pAlerta.FechaInforme.Value : (object)DBNull.Value;
            dop_Operacion.Parameters.Add(dtm_FechaInforme);
            Parameter dtm_FechaMitigacion = new Parameter("@pFechaMitigacion", DbType.DateTime);
            dtm_FechaMitigacion.Direction = ParameterDirection.Input;
            dtm_FechaMitigacion.Value = obj_pAlerta.FechaMitigacion.HasValue ? obj_pAlerta.FechaMitigacion.Value : (object)DBNull.Value;
            dop_Operacion.Parameters.Add(dtm_FechaMitigacion);
            dop_Operacion.Parameters.Add(new Parameter("@pObservacion", obj_pAlerta.Observacion));
            dop_Operacion.Parameters.Add(new Parameter("@pEstado", obj_pAlerta.Estado));
            dop_Operacion.Parameters.Add(new Parameter("@pUsuarioModificacion", obj_pAlerta.UsuarioModificacion));

            DataManager.ExecuteNonQuery(Conexiones.CO_SistemaMonitoreo, dop_Operacion, false);
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
            Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Alerta> obj_Lista = new Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Alerta>();
            obj_Lista.PageNumber = int_pNumPagina;
            obj_Lista.Total = 0;

            DataOperation dop_Operacion = new DataOperation("ListarReporteMonitoreo");
            dop_Operacion.Parameters.Add(new Parameter("@pIdTipoAlerta", int_pIdTipoAlerta.HasValue ? int_pIdTipoAlerta.Value : (object)DBNull.Value));

            dop_Operacion.Parameters.Add(new Parameter("@pPosicionInicial", int_pPosicionInicial.HasValue ? int_pPosicionInicial.Value : (object)DBNull.Value));
            dop_Operacion.Parameters.Add(new Parameter("@pPosicionFinal", int_pPosicionFinal.HasValue ? int_pPosicionFinal.Value : (object)DBNull.Value));

            Parameter dtm_FechaAlertaInicial = new Parameter("@pFechaAlertaInicio", DbType.DateTime);
            dtm_FechaAlertaInicial.Direction = ParameterDirection.Input;
            dtm_FechaAlertaInicial.Value = dt_pFechaAlertaInicio.HasValue ? dt_pFechaAlertaInicio.Value : (object)DBNull.Value;
            dop_Operacion.Parameters.Add(dtm_FechaAlertaInicial);

            Parameter dtm_FechaAlertaFinal = new Parameter("@pFechaAlertaFin", DbType.DateTime);
            dtm_FechaAlertaFinal.Direction = ParameterDirection.Input;
            dtm_FechaAlertaFinal.Value = dt_pFechaAlertaFin.HasValue ? dt_pFechaAlertaFin.Value : (object)DBNull.Value;
            dop_Operacion.Parameters.Add(dtm_FechaAlertaFinal);

            dop_Operacion.Parameters.Add(new Parameter("@pNumPagina", int_pNumPagina.HasValue ? int_pNumPagina.Value : (object)DBNull.Value));
            dop_Operacion.Parameters.Add(new Parameter("@pTamPagina", int_pTamPagina.HasValue ? int_pTamPagina.Value : (object)DBNull.Value));

            DataTable dt_Resultado = DataManager.ExecuteDataSet(Conexiones.CO_SistemaMonitoreo, dop_Operacion).Tables[0];

            List<Morken.SisMon.Entidad.Alerta> lst_Alerta = new List<Morken.SisMon.Entidad.Alerta>();
            Morken.SisMon.Entidad.Alerta obj_Alerta = new Morken.SisMon.Entidad.Alerta();
            foreach (DataRow obj_Row in dt_Resultado.Rows)
            {
                if (lst_Alerta.Count == 0)
                    obj_Lista.Total = (int)obj_Row["Total_Filas"];
                obj_Alerta = new Morken.SisMon.Entidad.Alerta();
                obj_Alerta.IdAlerta = obj_Row["IdAlerta"] is DBNull ? null : (int?)obj_Row["IdAlerta"];
                obj_Alerta.IdCanal = obj_Row["IdCanal"] is DBNull ? null : (int?)obj_Row["IdCanal"];
                obj_Alerta.IdSegmento = obj_Row["IdSegmento"] is DBNull ? null : (int?)obj_Row["IdSegmento"];
                //obj_Alerta.CodigoError = obj_Row["CodigoError"] is DBNull ? null : (int?)obj_Row["CodigoError"];
                obj_Alerta.MetroInicialSegmento = obj_Row["MetroInicialSegmento"] is DBNull ? null : (int?)obj_Row["MetroInicialSegmento"];
                obj_Alerta.MetroFinalSegmento = obj_Row["MetroFinalSegmento"] is DBNull ? null : (int?)obj_Row["MetroFinalSegmento"];
                obj_Alerta.IdTipoAlerta = obj_Row["IdTipoAlerta"] is DBNull ? null : (int?)obj_Row["IdTipoAlerta"];
                obj_Alerta.IdUsuario = obj_Row["IdUsuario"] is DBNull ? null : obj_Row["IdUsuario"].ToString();
                obj_Alerta.FechaAlerta = obj_Row["FechaAlerta"] is DBNull ? null : (DateTime?)obj_Row["FechaAlerta"];
                obj_Alerta.FechaAsignacion = obj_Row["FechaAsignacion"] is DBNull ? null : (DateTime?)obj_Row["FechaAsignacion"];
                obj_Alerta.PosicionInicial = obj_Row["PosicionInicial"] is DBNull ? null : (decimal?)obj_Row["PosicionInicial"];
                obj_Alerta.PosicionFinal = obj_Row["PosicionFinal"] is DBNull ? null : (decimal?)obj_Row["PosicionFinal"];
                obj_Alerta.ValorUmbral = obj_Row["ValorUmbral"] is DBNull ? null : (decimal?)obj_Row["ValorUmbral"];
                obj_Alerta.ValorUmbralMaximo = obj_Row["ValorUmbralMaximo"] is DBNull ? null : (decimal?)obj_Row["ValorUmbralMaximo"];
                obj_Alerta.CantidadGolpes = obj_Row["CantidadGolpes"] is DBNull ? null : (int?)obj_Row["CantidadGolpes"];
                obj_Alerta.CantidadGolpesMaximo = obj_Row["CantidadGolpesMaximo"] is DBNull ? null : (int?)obj_Row["CantidadGolpesMaximo"];
                obj_Alerta.IdAlertaEstado = obj_Row["IdAlertaEstado"] is DBNull ? null : (int?)obj_Row["IdAlertaEstado"];
                obj_Alerta.IdOperador = obj_Row["IdOperador"] is DBNull ? null : (int?)obj_Row["IdOperador"];
                obj_Alerta.FechaInforme = obj_Row["FechaInforme"] is DBNull ? null : (DateTime?)obj_Row["FechaInforme"];
                obj_Alerta.FechaMitigacion = obj_Row["FechaMitigacion"] is DBNull ? null : (DateTime?)obj_Row["FechaMitigacion"];
                obj_Alerta.Observacion = obj_Row["Observacion"] is DBNull ? null : obj_Row["Observacion"].ToString();
                obj_Alerta.Estado = obj_Row["Estado"] is DBNull ? null : obj_Row["Estado"].ToString();
                obj_Alerta.UsuarioCreacion = obj_Row["UsuarioCreacion"] is DBNull ? null : obj_Row["UsuarioCreacion"].ToString();
                obj_Alerta.FechaCreacion = obj_Row["FechaCreacion"] is DBNull ? null : (DateTime?)obj_Row["FechaCreacion"];
                obj_Alerta.UsuarioModificacion = obj_Row["UsuarioModificacion"] is DBNull ? null : obj_Row["UsuarioModificacion"].ToString();
                obj_Alerta.FechaModificacion = obj_Row["FechaModificacion"] is DBNull ? null : (DateTime?)obj_Row["FechaModificacion"];

                obj_Alerta.TiempoDemoraAsignacion = obj_Row["TiempoDemoraAsignacion"] is DBNull ? null : obj_Row["TiempoDemoraAsignacion"].ToString();
                obj_Alerta.TiempoDemoraInforme = obj_Row["TiempoDemoraInforme"] is DBNull ? null : obj_Row["TiempoDemoraInforme"].ToString();
                obj_Alerta.TiempoDemoraMitigacion = obj_Row["TiempoDemoraMitigacion"] is DBNull ? null : obj_Row["TiempoDemoraMitigacion"].ToString();

                obj_Alerta.TipoAlerta = new Entidad.TipoAlerta();
                obj_Alerta.TipoAlerta.IdTipoAlerta = obj_Row["TipoAlerta_IdTipoAlerta"] is DBNull ? null : (int?)obj_Row["TipoAlerta_IdTipoAlerta"];
                obj_Alerta.TipoAlerta.Descripcion = obj_Row["TipoAlerta_Descripcion"] is DBNull ? null : obj_Row["TipoAlerta_Descripcion"].ToString();

                obj_Alerta.AlertaEstado = new Entidad.AlertaEstado();
                obj_Alerta.AlertaEstado.IdAlertaEstado = obj_Row["AlertaEstado_IdAlertaEstado"] is DBNull ? null : (int?)obj_Row["AlertaEstado_IdAlertaEstado"];
                obj_Alerta.AlertaEstado.Descripcion = obj_Row["AlertaEstado_Descripcion"] is DBNull ? null : obj_Row["AlertaEstado_Descripcion"].ToString();

                if (!(obj_Row["IdUsuario"] is DBNull))
                {
                    obj_Alerta.Usuario = new Kruma.Core.Security.Entity.Usuario();
                    obj_Alerta.Usuario.IdUsuario = obj_Row["SecurityUsuario_IdUsuario"] is DBNull ? null : obj_Row["SecurityUsuario_IdUsuario"].ToString();
                    obj_Alerta.Usuario.IdPersona = obj_Row["SecurityUsuario_IdPersona"] is DBNull ? null : (int?)obj_Row["SecurityUsuario_IdPersona"];
                    obj_Alerta.Usuario.Persona = new Kruma.Core.Business.Entity.Persona();
                    obj_Alerta.Usuario.Persona.IdPersona = obj_Row["CorePersonaUsuario_IdPersona"] is DBNull ? null : (int?)obj_Row["CorePersonaUsuario_IdPersona"];
                    obj_Alerta.Usuario.Persona.Nombres = obj_Row["CorePersonaUsuario_Nombres"] is DBNull ? null : obj_Row["CorePersonaUsuario_Nombres"].ToString();
                    obj_Alerta.Usuario.Persona.ApellidoPaterno = obj_Row["CorePersonaUsuario_ApellidoPaterno"] is DBNull ? null : obj_Row["CorePersonaUsuario_ApellidoPaterno"].ToString();
                    obj_Alerta.Usuario.Persona.ApellidoMaterno = obj_Row["CorePersonaUsuario_ApellidoMaterno"] is DBNull ? null : obj_Row["CorePersonaUsuario_ApellidoMaterno"].ToString();
                }

                if (!(obj_Row["IdOperador"] is DBNull))
                {
                    obj_Alerta.Operador = new Entidad.Operador();
                    obj_Alerta.Operador.IdOperador = obj_Row["Operador_IdOperador"] is DBNull ? null : (int?)obj_Row["Operador_IdOperador"];
                    obj_Alerta.Operador.IdPersona = obj_Row["Operador_IdPersona"] is DBNull ? null : (int?)obj_Row["Operador_IdPersona"];
                    obj_Alerta.Operador.Persona = new Kruma.Core.Business.Entity.Persona();
                    obj_Alerta.Operador.Persona.IdPersona = obj_Row["CorePersonaOperador_IdPersona"] is DBNull ? null : (int?)obj_Row["CorePersonaOperador_IdPersona"];
                    obj_Alerta.Operador.Persona.Nombres = obj_Row["CorePersonaOperador_Nombres"] is DBNull ? null : obj_Row["CorePersonaOperador_Nombres"].ToString();
                    obj_Alerta.Operador.Persona.ApellidoPaterno = obj_Row["CorePersonaOperador_ApellidoPaterno"] is DBNull ? null : obj_Row["CorePersonaOperador_ApellidoPaterno"].ToString();
                    obj_Alerta.Operador.Persona.ApellidoMaterno = obj_Row["CorePersonaOperador_ApellidoMaterno"] is DBNull ? null : obj_Row["CorePersonaOperador_ApellidoMaterno"].ToString();
                }


                lst_Alerta.Add(obj_Alerta);
            }

            obj_Lista.Result = lst_Alerta;
            return obj_Lista;
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
            DataOperation dop_Operacion = new DataOperation("ListarEventosDiarios");
            dop_Operacion.Parameters.Add(new Parameter("@pIdTipoAlerta", int_pIdTipoAlerta.HasValue ? int_pIdTipoAlerta.Value : (object)DBNull.Value));


            dop_Operacion.Parameters.Add(new Parameter("@pPosicionInicial", int_pPosicionInicial.HasValue ? int_pPosicionInicial.Value : (object)DBNull.Value));
            dop_Operacion.Parameters.Add(new Parameter("@pPosicionFinal", int_pPosicionFinal.HasValue ? int_pPosicionFinal.Value : (object)DBNull.Value));

            Parameter dtm_FechaAlertaInicial = new Parameter("@pFechaAlertaInicio", DbType.DateTime);
            dtm_FechaAlertaInicial.Direction = ParameterDirection.Input;
            dtm_FechaAlertaInicial.Value = dt_pFechaAlertaInicio.HasValue ? dt_pFechaAlertaInicio.Value : (object)DBNull.Value;
            dop_Operacion.Parameters.Add(dtm_FechaAlertaInicial);

            Parameter dtm_FechaAlertaFinal = new Parameter("@pFechaAlertaFin", DbType.DateTime);
            dtm_FechaAlertaFinal.Direction = ParameterDirection.Input;
            dtm_FechaAlertaFinal.Value = dt_pFechaAlertaFin.HasValue ? dt_pFechaAlertaFin.Value : (object)DBNull.Value;
            dop_Operacion.Parameters.Add(dtm_FechaAlertaFinal);

            DataTable dt_Resultado = DataManager.ExecuteDataSet(Conexiones.CO_SistemaMonitoreo, dop_Operacion).Tables[0];

            List<Morken.SisMon.Entidad.Alerta> lst_Alerta = new List<Morken.SisMon.Entidad.Alerta>();
            Morken.SisMon.Entidad.Alerta obj_Alerta = new Morken.SisMon.Entidad.Alerta();
            foreach (DataRow obj_Row in dt_Resultado.Rows)
            {
                obj_Alerta = new Morken.SisMon.Entidad.Alerta();
                obj_Alerta.FechaAlertaToString = obj_Row["FechaAlerta"] is DBNull ? null : obj_Row["FechaAlerta"].ToString();
                obj_Alerta.CantidadAlertas = obj_Row["CantidadAlertas"] is DBNull ? null : (int?)obj_Row["CantidadAlertas"];
                lst_Alerta.Add(obj_Alerta);
            }

            return lst_Alerta;
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
            Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Alerta> obj_Lista = new Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Alerta>();
            obj_Lista.PageNumber = int_pNumPagina;
            obj_Lista.Total = 0;

            DataOperation dop_Operacion = new DataOperation("ListarReporteSistema");
            dop_Operacion.Parameters.Add(new Parameter("@pIdAlertaEstado", int_pIdAlertaEstado.HasValue ? int_pIdAlertaEstado.Value : (object)DBNull.Value));

            Parameter dtm_FechaAlertaInicial = new Parameter("@pFechaAlertaInicio", DbType.DateTime);
            dtm_FechaAlertaInicial.Direction = ParameterDirection.Input;
            dtm_FechaAlertaInicial.Value = dt_pFechaAlertaInicio.HasValue ? dt_pFechaAlertaInicio.Value : (object)DBNull.Value;
            dop_Operacion.Parameters.Add(dtm_FechaAlertaInicial);

            Parameter dtm_FechaAlertaFinal = new Parameter("@pFechaAlertaFin", DbType.DateTime);
            dtm_FechaAlertaFinal.Direction = ParameterDirection.Input;
            dtm_FechaAlertaFinal.Value = dt_pFechaAlertaFin.HasValue ? dt_pFechaAlertaFin.Value : (object)DBNull.Value;
            dop_Operacion.Parameters.Add(dtm_FechaAlertaFinal);

            dop_Operacion.Parameters.Add(new Parameter("@pNumPagina", int_pNumPagina.HasValue ? int_pNumPagina.Value : (object)DBNull.Value));
            dop_Operacion.Parameters.Add(new Parameter("@pTamPagina", int_pTamPagina.HasValue ? int_pTamPagina.Value : (object)DBNull.Value));

            DataTable dt_Resultado = DataManager.ExecuteDataSet(Conexiones.CO_SistemaMonitoreo, dop_Operacion).Tables[0];

            List<Morken.SisMon.Entidad.Alerta> lst_Alerta = new List<Morken.SisMon.Entidad.Alerta>();
            Morken.SisMon.Entidad.Alerta obj_Alerta = new Morken.SisMon.Entidad.Alerta();
            foreach (DataRow obj_Row in dt_Resultado.Rows)
            {
                if (lst_Alerta.Count == 0)
                    obj_Lista.Total = (int)obj_Row["Total_Filas"];

                obj_Alerta = new Morken.SisMon.Entidad.Alerta();
                obj_Alerta.IdAlerta = obj_Row["IdAlerta"] is DBNull ? null : (int?)obj_Row["IdAlerta"];
                obj_Alerta.CodigoError = obj_Row["CodigoError"] is DBNull ? null : (int?)obj_Row["CodigoError"];
                obj_Alerta.FechaAlerta = obj_Row["FechaAlerta"] is DBNull ? null : (DateTime?)obj_Row["FechaAlerta"];
                obj_Alerta.IdAlertaEstado = obj_Row["IdAlertaEstado"] is DBNull ? null : (int?)obj_Row["IdAlertaEstado"];
                obj_Alerta.Observacion = obj_Row["Observacion"] is DBNull ? null : obj_Row["Observacion"].ToString();
                obj_Alerta.Estado = obj_Row["Estado"] is DBNull ? null : obj_Row["Estado"].ToString();
                obj_Alerta.UsuarioCreacion = obj_Row["UsuarioCreacion"] is DBNull ? null : obj_Row["UsuarioCreacion"].ToString();
                obj_Alerta.FechaCreacion = obj_Row["FechaCreacion"] is DBNull ? null : (DateTime?)obj_Row["FechaCreacion"];
                obj_Alerta.UsuarioModificacion = obj_Row["UsuarioModificacion"] is DBNull ? null : obj_Row["UsuarioModificacion"].ToString();
                obj_Alerta.FechaModificacion = obj_Row["FechaModificacion"] is DBNull ? null : (DateTime?)obj_Row["FechaModificacion"];

                obj_Alerta.AlertaEstado = new Entidad.AlertaEstado();
                obj_Alerta.AlertaEstado.IdAlertaEstado = obj_Row["AlertaEstado_IdAlertaEstado"] is DBNull ? null : (int?)obj_Row["AlertaEstado_IdAlertaEstado"];
                obj_Alerta.AlertaEstado.Descripcion = obj_Row["AlertaEstado_Descripcion"] is DBNull ? null : obj_Row["AlertaEstado_Descripcion"].ToString();

                lst_Alerta.Add(obj_Alerta);
            }
            obj_Lista.Result = lst_Alerta;
            return obj_Lista;
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
            DataOperation dop_Operacion = new DataOperation("ListarAlertasMapa");
            dop_Operacion.Parameters.Add(new Parameter("@pCantidadDias", int_pCantidadDias));
            dop_Operacion.Parameters.Add(new Parameter("@pIdTipoAlerta", int_pIdTipoAlerta.HasValue ? int_pIdTipoAlerta.Value : (object)DBNull.Value));

            Parameter dtm_FechaAlertaInicial = new Parameter("@pFechaInicial", DbType.DateTime);
            dtm_FechaAlertaInicial.Direction = ParameterDirection.Input;
            dtm_FechaAlertaInicial.Value = dt_pFechaAlertaInicio.HasValue ? dt_pFechaAlertaInicio.Value : (object)DBNull.Value;
            dop_Operacion.Parameters.Add(dtm_FechaAlertaInicial);

            Parameter dtm_FechaAlertaFinal = new Parameter("@pFechaFinal", DbType.DateTime);
            dtm_FechaAlertaFinal.Direction = ParameterDirection.Input;
            dtm_FechaAlertaFinal.Value = dt_pFechaAlertaFin.HasValue ? dt_pFechaAlertaFin.Value : (object)DBNull.Value;
            dop_Operacion.Parameters.Add(dtm_FechaAlertaFinal);

            DataTable dt_Resultado = DataManager.ExecuteDataSet(Conexiones.CO_SistemaMonitoreo, dop_Operacion).Tables[0];

            List<Morken.SisMon.Entidad.Alerta> lst_Alerta = new List<Morken.SisMon.Entidad.Alerta>();
            Morken.SisMon.Entidad.Alerta obj_Alerta = new Morken.SisMon.Entidad.Alerta();
            foreach (DataRow obj_Row in dt_Resultado.Rows)
            {
                obj_Alerta = new Morken.SisMon.Entidad.Alerta();
                obj_Alerta.IdAlerta = obj_Row["IdAlerta"] is DBNull ? null : (int?)obj_Row["IdAlerta"];
                obj_Alerta.IdCanal = obj_Row["IdCanal"] is DBNull ? null : (int?)obj_Row["IdCanal"];
                obj_Alerta.IdSegmento = obj_Row["IdSegmento"] is DBNull ? null : (int?)obj_Row["IdSegmento"];
                obj_Alerta.CodigoError = obj_Row["CodigoError"] is DBNull ? null : (int?)obj_Row["CodigoError"];
                obj_Alerta.MetroInicialSegmento = obj_Row["MetroInicialSegmento"] is DBNull ? null : (int?)obj_Row["MetroInicialSegmento"];
                obj_Alerta.MetroFinalSegmento = obj_Row["MetroFinalSegmento"] is DBNull ? null : (int?)obj_Row["MetroFinalSegmento"];
                obj_Alerta.IdTipoAlerta = obj_Row["IdTipoAlerta"] is DBNull ? null : (int?)obj_Row["IdTipoAlerta"];
                obj_Alerta.IdUsuario = obj_Row["IdUsuario"] is DBNull ? null : obj_Row["IdUsuario"].ToString();
                obj_Alerta.FechaAlerta = obj_Row["FechaAlerta"] is DBNull ? null : (DateTime?)obj_Row["FechaAlerta"];
                obj_Alerta.FechaAsignacion = obj_Row["FechaAsignacion"] is DBNull ? null : (DateTime?)obj_Row["FechaAsignacion"];
                obj_Alerta.PosicionInicial = obj_Row["PosicionInicial"] is DBNull ? null : (decimal?)obj_Row["PosicionInicial"];
                obj_Alerta.PosicionFinal = obj_Row["PosicionFinal"] is DBNull ? null : (decimal?)obj_Row["PosicionFinal"];
                obj_Alerta.ValorUmbral = obj_Row["ValorUmbral"] is DBNull ? null : (decimal?)obj_Row["ValorUmbral"];
                obj_Alerta.ValorUmbralMaximo = obj_Row["ValorUmbralMaximo"] is DBNull ? null : (decimal?)obj_Row["ValorUmbralMaximo"];
                obj_Alerta.CantidadGolpes = obj_Row["CantidadGolpes"] is DBNull ? null : (int?)obj_Row["CantidadGolpes"];
                obj_Alerta.CantidadGolpesMaximo = obj_Row["CantidadGolpesMaximo"] is DBNull ? null : (int?)obj_Row["CantidadGolpesMaximo"];
                obj_Alerta.IdAlertaEstado = obj_Row["IdAlertaEstado"] is DBNull ? null : (int?)obj_Row["IdAlertaEstado"];
                obj_Alerta.IdOperador = obj_Row["IdOperador"] is DBNull ? null : (int?)obj_Row["IdOperador"];
                obj_Alerta.FechaInforme = obj_Row["FechaInforme"] is DBNull ? null : (DateTime?)obj_Row["FechaInforme"];
                obj_Alerta.FechaMitigacion = obj_Row["FechaMitigacion"] is DBNull ? null : (DateTime?)obj_Row["FechaMitigacion"];
                obj_Alerta.Observacion = obj_Row["Observacion"] is DBNull ? null : obj_Row["Observacion"].ToString();
                obj_Alerta.Estado = obj_Row["Estado"] is DBNull ? null : obj_Row["Estado"].ToString();
                obj_Alerta.UsuarioCreacion = obj_Row["UsuarioCreacion"] is DBNull ? null : obj_Row["UsuarioCreacion"].ToString();
                obj_Alerta.FechaCreacion = obj_Row["FechaCreacion"] is DBNull ? null : (DateTime?)obj_Row["FechaCreacion"];
                obj_Alerta.UsuarioModificacion = obj_Row["UsuarioModificacion"] is DBNull ? null : obj_Row["UsuarioModificacion"].ToString();
                obj_Alerta.FechaModificacion = obj_Row["FechaModificacion"] is DBNull ? null : (DateTime?)obj_Row["FechaModificacion"];

                obj_Alerta.TipoAlerta = new Entidad.TipoAlerta();
                obj_Alerta.TipoAlerta.IdTipoAlerta = obj_Row["TipoAlerta_IdTipoAlerta"] is DBNull ? null : (int?)obj_Row["TipoAlerta_IdTipoAlerta"];
                obj_Alerta.TipoAlerta.Descripcion = obj_Row["TipoAlerta_Descripcion"] is DBNull ? null : obj_Row["TipoAlerta_Descripcion"].ToString();

                obj_Alerta.AlertaEstado = new Entidad.AlertaEstado();
                obj_Alerta.AlertaEstado.IdAlertaEstado = obj_Row["AlertaEstado_IdAlertaEstado"] is DBNull ? null : (int?)obj_Row["AlertaEstado_IdAlertaEstado"];
                obj_Alerta.AlertaEstado.Descripcion = obj_Row["AlertaEstado_Descripcion"] is DBNull ? null : obj_Row["AlertaEstado_Descripcion"].ToString();

                if (!(obj_Row["IdOperador"] is DBNull))
                {
                    obj_Alerta.Operador = new Entidad.Operador();
                    obj_Alerta.Operador.IdOperador = obj_Row["Operador_IdOperador"] is DBNull ? null : (int?)obj_Row["Operador_IdOperador"];
                    obj_Alerta.Operador.IdPersona = obj_Row["Operador_IdPersona"] is DBNull ? null : (int?)obj_Row["Operador_IdPersona"];
                    obj_Alerta.Operador.Persona = new Kruma.Core.Business.Entity.Persona();
                    obj_Alerta.Operador.Persona.IdPersona = obj_Row["CorePersonaOperador_IdPersona"] is DBNull ? null : (int?)obj_Row["CorePersonaOperador_IdPersona"];
                    obj_Alerta.Operador.Persona.Nombres = obj_Row["CorePersonaOperador_Nombres"] is DBNull ? null : obj_Row["CorePersonaOperador_Nombres"].ToString();
                    obj_Alerta.Operador.Persona.ApellidoPaterno = obj_Row["CorePersonaOperador_ApellidoPaterno"] is DBNull ? null : obj_Row["CorePersonaOperador_ApellidoPaterno"].ToString();
                    obj_Alerta.Operador.Persona.ApellidoMaterno = obj_Row["CorePersonaOperador_ApellidoMaterno"] is DBNull ? null : obj_Row["CorePersonaOperador_ApellidoMaterno"].ToString();
                }

                lst_Alerta.Add(obj_Alerta);
            }
            
            return lst_Alerta;
        }
        #endregion
    }
}