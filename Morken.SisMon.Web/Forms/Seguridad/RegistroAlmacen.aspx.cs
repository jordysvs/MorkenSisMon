using Kruma.Core.Util.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Seguridad_RegistroAlmacen : System.Web.UI.Page
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
            obj_MasterPage.TituloPagina = "Agregar Almacen";
        }
        else
        {
            obj_MasterPage.TituloPagina = "Modificar Almacen";
            hdIdAlmacen.Value = Request.QueryString["id"];
        }

        ddlEstado.DataSource = Kruma.Core.Util.CommonUtil.ListarEstado();
        ddlEstado.DataValueField = "Code";
        ddlEstado.DataTextField = "Description";
        ddlEstado.DataBind();
    }

    #endregion

    #region Metodos Publicos

    /// <summary>
    /// Obtiene los datos del almacen de documentos
    /// </summary>
    /// <param name="str_pIdAlmacen">Id del almacen</param>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>John Castillo</CreadoPor></item>
    /// <item><FecCrea>22/07/2015</FecCrea></item></list></remarks>
    [WebMethod]
    public static Kruma.Core.FileServer.Entity.Almacen ObtenerAlmacen(int int_pIdAlmacen)
    {
        return Kruma.Core.FileServer.Logical.Almacen.Obtener(int_pIdAlmacen);
    }

    /// <summary>
    /// Guarda la informacion del almacen
    /// </summary>
    /// <param name="str_pAlmacen">Json Almacen</param>
    /// <returns>Resultado del proceso</returns>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>Creado por John Castillo</CreadoPor></item>
    /// <item><FecCrea>22/07/2015</FecCrea></item></list></remarks>
    [WebMethod]
    public static ProcessResult GuardarAlmacen(string str_pAlmacen)
    {
        JavaScriptSerializer obj_JsSerializer = new JavaScriptSerializer();
        Kruma.Core.FileServer.Entity.Almacen obj_Almacen = obj_JsSerializer.Deserialize<Kruma.Core.FileServer.Entity.Almacen>(str_pAlmacen);
        obj_Almacen.UsuarioCreacion = Kruma.Core.Security.SecurityManager.Usuario.IdUsuario;
        obj_Almacen.UsuarioModificacion = obj_Almacen.UsuarioCreacion;

        ProcessResult obj_Resultado = null;
        if (!obj_Almacen.IdAlmacen.HasValue)
            obj_Resultado = Kruma.Core.FileServer.Logical.Almacen.Insertar(obj_Almacen);
        else
            obj_Resultado = Kruma.Core.FileServer.Logical.Almacen.Modificar(obj_Almacen);
        return obj_Resultado;
    }

    #endregion
}