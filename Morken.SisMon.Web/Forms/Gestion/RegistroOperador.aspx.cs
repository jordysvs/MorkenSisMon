using Kruma.Core.Util.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Gestion_RegistroOperador : System.Web.UI.Page
{
    #region Eventos

    /// <summary>
    /// Evento de carga de la pagina
    /// </summary>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>John Castillo</CreadoPor></item>
    /// <item><FecCrea>15/12/2014</FecCrea></item></list></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargaInicial();
        }
    }

    #endregion

    #region Metodos Privados

    /// <summary>
    /// Carga Inicial del formulario
    /// </summary>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>John Castillo</CreadoPor></item>
    /// <item><FecCrea>15/12/2014</FecCrea></item></list></remarks>
    private void CargaInicial()
    {
        int? int_IdOperador = null;

        Master_Default obj_MasterPage = (Master_Default)this.Master;
        if (Request.QueryString["id"] == null)
        {
            obj_MasterPage.TituloPagina = "Agregar Operador";
        }
        else
        {
            obj_MasterPage.TituloPagina = "Modificar Operador";
            int_IdOperador = int.Parse(Request.QueryString["id"]);
            hdIdOperador.Value = int_IdOperador.ToString();
        }

        ddlEstado.DataSource = Kruma.Core.Util.CommonUtil.ListarEstado();
        ddlEstado.DataValueField = "Code";
        ddlEstado.DataTextField = "Description";
        ddlEstado.DataBind();
    }

    #endregion

    #region Metodos Publicos

    /// <summary>
    /// Obtiene los datos de la Operador
    /// </summary>
    /// <param name="int_pIdOperador">Id de la Operador</param>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>Diego Mendoza</CreadoPor></item>
    /// <item><FecCrea>26-03-2018</FecCrea></item></list></remarks>
    [WebMethod]
    public static Morken.SisMon.Entidad.Operador ObtenerOperador(int int_pIdOperador)
    {
        return Morken.SisMon.Negocio.Operador.Obtener(int_pIdOperador);
    }

    /// <summary>
    /// Guarda la informacion de la Operador
    /// </summary>
    /// <param name="str_pOperador">Json Operador</param>
    /// <returns>Resultado del proceso</returns>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>Diego Mendoza</CreadoPor></item>
    /// <item><FecCrea>26-03-2018</FecCrea></item></list></remarks>
    [WebMethod]
    public static ProcessResult GuardarOperador(string str_pOperador)
    {
        JavaScriptSerializer obj_JsSerializer = new JavaScriptSerializer();
        Morken.SisMon.Entidad.Operador obj_Operador = obj_JsSerializer.Deserialize<Morken.SisMon.Entidad.Operador>(str_pOperador);
    
        ProcessResult obj_Resultado = null;
        if (!obj_Operador.IdOperador.HasValue)
        {
            obj_Operador.UsuarioCreacion = Kruma.Core.Security.SecurityManager.Usuario.IdUsuario;
            obj_Operador.UsuarioModificacion = obj_Operador.UsuarioCreacion;
            obj_Resultado = Morken.SisMon.Negocio.Operador.Insertar(obj_Operador);
        }
        else
        {
            obj_Operador.UsuarioModificacion = Kruma.Core.Security.SecurityManager.Usuario.IdUsuario;
            obj_Resultado = Morken.SisMon.Negocio.Operador.Modificar(obj_Operador);
        }
        return obj_Resultado;
    }

    #endregion
}