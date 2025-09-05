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
        public static Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Canal> Listar(int? int_pIdCanal, string str_pDescripcion, string str_pEstado, int? int_pNumPagina, int? int_pTamPagina)
        {
            Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Canal> obj_Lista = new Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Canal>();
            obj_Lista.PageNumber = int_pNumPagina;
            obj_Lista.Total = 0;

            DataOperation dop_Operacion = new DataOperation("ListarCanal");
            dop_Operacion.Parameters.Add(new Parameter("@pIdCanal", int_pIdCanal.HasValue ? int_pIdCanal.Value : (object)DBNull.Value));
            dop_Operacion.Parameters.Add(new Parameter("@pDescripcion", !string.IsNullOrEmpty(str_pDescripcion) ? str_pDescripcion : (object)DBNull.Value));
            dop_Operacion.Parameters.Add(new Parameter("@pEstado", !string.IsNullOrEmpty(str_pEstado) ? str_pEstado : (object)DBNull.Value));
            dop_Operacion.Parameters.Add(new Parameter("@pNumPagina", int_pNumPagina.HasValue ? int_pNumPagina.Value : (object)DBNull.Value));
            dop_Operacion.Parameters.Add(new Parameter("@pTamPagina", int_pTamPagina.HasValue ? int_pTamPagina.Value : (object)DBNull.Value));

            DataTable dt_Resultado = DataManager.ExecuteDataSet(Conexiones.CO_SistemaMonitoreo, dop_Operacion).Tables[0];

            List<Morken.SisMon.Entidad.Canal> lst_Canal = new List<Morken.SisMon.Entidad.Canal>();
            Morken.SisMon.Entidad.Canal obj_Canal = new Morken.SisMon.Entidad.Canal();
            foreach (DataRow obj_Row in dt_Resultado.Rows)
            {
                if (lst_Canal.Count == 0)
                    obj_Lista.Total = (int)obj_Row["Total_Filas"];
                obj_Canal = new Morken.SisMon.Entidad.Canal();
                obj_Canal.IdCanal = obj_Row["IdCanal"] is DBNull ? null : (int?)obj_Row["IdCanal"];
                obj_Canal.Descripcion = obj_Row["Descripcion"] is DBNull ? null : obj_Row["Descripcion"].ToString();
                obj_Canal.Inverso = obj_Row["Inverso"] is DBNull ? Kruma.Core.Business.Entity.Constante.Condicion_Negativo : obj_Row["Inverso"].ToString();
                obj_Canal.Estado = obj_Row["Estado"] is DBNull ? null : obj_Row["Estado"].ToString();
                obj_Canal.UsuarioCreacion = obj_Row["UsuarioCreacion"] is DBNull ? null : obj_Row["UsuarioCreacion"].ToString();
                obj_Canal.FechaCreacion = obj_Row["FechaCreacion"] is DBNull ? null : (DateTime?)obj_Row["FechaCreacion"];
                obj_Canal.UsuarioModificacion = obj_Row["UsuarioModificacion"] is DBNull ? null : obj_Row["UsuarioModificacion"].ToString();
                obj_Canal.FechaModificacion = obj_Row["FechaModificacion"] is DBNull ? null : (DateTime?)obj_Row["FechaModificacion"];
                lst_Canal.Add(obj_Canal);
            }

            obj_Lista.Result = lst_Canal;
            return obj_Lista;
        }

        /// <summary>Obtener Canal</summary>
        /// <param name="int_pIdCanal">IdCanal</param>
        /// <returns>Objeto Canal</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
        /// <item><FecCrea>24-10-2017</FecCrea></item></list></remarks>
        public static Morken.SisMon.Entidad.Canal Obtener(int int_pIdCanal)
        {
            Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Canal> lst_Canal = Listar(int_pIdCanal, null, null, null, null);
            return lst_Canal.Result.Count > 0 ? lst_Canal.Result[0] : null;
        }

        /// <summary>Insertar Canal</summary>
        /// <param name="obj_pCanal">Canal</param>
        /// <returns>Id de Canal</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
        /// <item><FecCrea>24-10-2017</FecCrea></item></list></remarks>
        public static int Insertar(Morken.SisMon.Entidad.Canal obj_pCanal)
        {
            DataOperation dop_Operacion = new DataOperation("InsertarCanal");

            dop_Operacion.Parameters.Add(new Parameter("@pDescripcion", obj_pCanal.Descripcion));
            dop_Operacion.Parameters.Add(new Parameter("@pEstado", obj_pCanal.Estado));
            dop_Operacion.Parameters.Add(new Parameter("@pUsuarioCreacion", obj_pCanal.UsuarioCreacion));

            Parameter obj_IdCanal = new Parameter("@pIdCanal", DbType.Int32);
            obj_IdCanal.Direction = ParameterDirection.Output;
            dop_Operacion.Parameters.Add(obj_IdCanal);

            DataManager.ExecuteNonQuery(Conexiones.CO_SistemaMonitoreo, dop_Operacion, false);
            int int_IdCanal = (int)obj_IdCanal.Value;
            return int_IdCanal;
        }

        /// <summary>Actualizar Canal</summary>
        /// <param name="obj_pCanal">Canal</param>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Diego Mendoza</CreadoPor></item>
        /// <item><FecCrea>24-10-2017</FecCrea></item></list></remarks>
        public static void Modificar(Morken.SisMon.Entidad.Canal obj_pCanal)
        {
            DataOperation dop_Operacion = new DataOperation("ActualizarCanal");

            dop_Operacion.Parameters.Add(new Parameter("@pIdCanal", obj_pCanal.IdCanal));
            dop_Operacion.Parameters.Add(new Parameter("@pDescripcion", obj_pCanal.Descripcion));
            dop_Operacion.Parameters.Add(new Parameter("@pEstado", obj_pCanal.Estado));
            dop_Operacion.Parameters.Add(new Parameter("@pUsuarioModificacion", obj_pCanal.UsuarioModificacion));

            DataManager.ExecuteNonQuery(Conexiones.CO_SistemaMonitoreo, dop_Operacion, false);
        }

        #endregion
    }
}