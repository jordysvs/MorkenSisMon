using Kruma.Core.Util.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Seguridad_RegistroDispositivo : System.Web.UI.Page
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
    /// <item><FecCrea>22/07/2015</FecCrea></item></list></remarks>
    private void CargaInicial()
    {
        //Titulo
        Master_Default obj_MasterPage = (Master_Default)this.Master;
        if (Request.QueryString["id"] == null)
        {
            obj_MasterPage.TituloPagina = "Agregar Dispositivo";
        }
        else
        {
            obj_MasterPage.TituloPagina = "Modificar Dispositivo";
            hdIdDispositivo.Value = Request.QueryString["id"];
        }

        ddlEstado.DataSource = Kruma.Core.Util.CommonUtil.ListarEstado();
        ddlEstado.DataValueField = "Code";
        ddlEstado.DataTextField = "Description";
        ddlEstado.DataBind();
    }

    #endregion

    #region Metodos Publicos

    /// <summary>
    /// Obtiene los datos del dispositivo
    /// </summary>
    /// <param name="str_pIdDispositivo">Id del dispositivo</param>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>John Castillo</CreadoPor></item>
    /// <item><FecCrea>22/07/2015</FecCrea></item></list></remarks>
    [WebMethod]
    public static Kruma.Core.Security.Entity.Dispositivo ObtenerDispositivo(int int_pIdDispositivo)
    {
        return Kruma.Core.Security.Logical.Dispositivo.Obtener(int_pIdDispositivo);
    }

    /// <summary>
    /// Guarda la informacion del dispositivo
    /// </summary>
    /// <param name="str_pDispositivo">Json Dispositivo</param>
    /// <returns>Resultado del proceso</returns>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>Creado por John Castillo</CreadoPor></item>
    /// <item><FecCrea>22/07/2015</FecCrea></item></list></remarks>
    [WebMethod]
    public static ProcessResult GuardarDispositivo(string str_pDispositivo)
    {
        JavaScriptSerializer obj_JsSerializer = new JavaScriptSerializer();
        Kruma.Core.Security.Entity.Dispositivo obj_Dispositivo = obj_JsSerializer.Deserialize<Kruma.Core.Security.Entity.Dispositivo>(str_pDispositivo);
        obj_Dispositivo.UsuarioCreacion = Kruma.Core.Security.SecurityManager.Usuario.IdUsuario;
        obj_Dispositivo.UsuarioModificacion = obj_Dispositivo.UsuarioCreacion;

        ProcessResult obj_Resultado = null;
        if (!obj_Dispositivo.IdDispositivo.HasValue)
            obj_Resultado = Kruma.Core.Security.Logical.Dispositivo.Insertar(obj_Dispositivo);
        else
            obj_Resultado = Kruma.Core.Security.Logical.Dispositivo.Modificar(obj_Dispositivo);
        return obj_Resultado;
    }

    #endregion
}