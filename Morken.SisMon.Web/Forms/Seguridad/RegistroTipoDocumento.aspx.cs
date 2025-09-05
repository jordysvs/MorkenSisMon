using Kruma.Core.Util.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Configuracion_RegistroTipoDocumento : System.Web.UI.Page
{
    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargaInicial();
        }
    }

    #endregion

    #region Metodos Privados

    private void CargaInicial()
    {
        //Titulo
        Master_Default obj_MasterPage = (Master_Default)this.Master;
        if (Request.QueryString["id"] == null)
        {
            obj_MasterPage.TituloPagina = "Agregar Tipo Documento";
        }
        else
        {
            obj_MasterPage.TituloPagina = "Modificar Tipo Documento";
            hdIdTipoDocumento.Value = Request.QueryString["id"];
        }

        ddlEstado.DataSource = Kruma.Core.Util.CommonUtil.ListarEstado();
        ddlEstado.DataValueField = "Code";
        ddlEstado.DataTextField = "Description";
        ddlEstado.DataBind();
    }

    #endregion

    #region Metodos Publicos

    /// <summary>
    /// Obtiene los datos del tipo de documento
    /// </summary>
    /// <param name="int_pIdArea">Id del tipo de documento</param>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>John Castillo</CreadoPor></item>
    /// <item><FecCrea>07/06/2015</FecCrea></item></list></remarks>
    [WebMethod]
    public static Kruma.Core.Business.Entity.TipoDocumento ObtenerTipoDocumento(int int_pIdTipoDocumento)
    {
        return Kruma.Core.Business.Logical.TipoDocumento.Obtener(int_pIdTipoDocumento);
    }

    /// <summary>
    /// Guarda la informacion del tipo de documento
    /// </summary>
    /// <param name="str_pArea">Json Tipo de documento</param>
    /// <returns>Resultado del proceso</returns>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>Creado por John Castillo</CreadoPor></item>
    /// <item><FecCrea>07/06/2015</FecCrea></item></list></remarks>
    [WebMethod]
    public static ProcessResult GuardarTipoDocumento(string str_pTipoDocumento)
    {
        JavaScriptSerializer obj_JsSerializer = new JavaScriptSerializer();
        Kruma.Core.Business.Entity.TipoDocumento obj_TipoDocumento = obj_JsSerializer.Deserialize<Kruma.Core.Business.Entity.TipoDocumento>(str_pTipoDocumento);
        obj_TipoDocumento.UsuarioCreacion = Kruma.Core.Security.SecurityManager.Usuario.IdUsuario;
        obj_TipoDocumento.UsuarioModificacion = obj_TipoDocumento.UsuarioCreacion;

        ProcessResult obj_Resultado = null;
        if (!obj_TipoDocumento.IdTipoDocumento.HasValue)
            obj_Resultado = Kruma.Core.Business.Logical.TipoDocumento.Insertar(obj_TipoDocumento);
        else
            obj_Resultado = Kruma.Core.Business.Logical.TipoDocumento.Modificar(obj_TipoDocumento);
        return obj_Resultado;
    }

    #endregion
}