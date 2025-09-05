using Kruma.Core.Util.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Seguridad_RegistroParametro : System.Web.UI.Page
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
        //Titulo
        Master_Default obj_MasterPage = (Master_Default)this.Master;
        obj_MasterPage.TituloPagina = "Modificar Parámetro";

        //Id del parametro
        hdIdModulo.Value = Request.QueryString["id"];
        txtCodigo.Text = Request.QueryString["id2"];
    }

    #endregion

    #region Metodos Publicos

    /// <summary>
    /// Obtiene los datos del parametro
    /// </summary>
    /// <param name="int_pIdParametro">Id del parametro</param>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>John Castillo</CreadoPor></item>
    /// <item><FecCrea>15/12/2014</FecCrea></item></list></remarks>
    [WebMethod]
    public static Kruma.Core.Business.Entity.Parametro ObtenerParametro(
        string str_pIdModulo,
        string str_pIdParametro)
    {
        return Kruma.Core.Business.Logical.Parametro.Obtener(
            str_pIdModulo,
            str_pIdParametro);
    }

    /// <summary>
    /// Guarda la informacion del parametro
    /// </summary>
    /// <param name="str_pProducto">Json Parametro</param>
    /// <returns>Resultado del proceso</returns>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>Creado por John Castillo</CreadoPor></item>
    /// <item><FecCrea>04/11/2014</FecCrea></item></list></remarks>
    [WebMethod]
    public static ProcessResult GuardarParametro(
        string str_pParametro)
    {
        JavaScriptSerializer obj_JsSerializer = new JavaScriptSerializer();
        Kruma.Core.Business.Entity.Parametro obj_Parametro = obj_JsSerializer.Deserialize<Kruma.Core.Business.Entity.Parametro>(str_pParametro);
        obj_Parametro.UsuarioCreacion = Kruma.Core.Security.SecurityManager.Usuario.IdUsuario;
        obj_Parametro.UsuarioModificacion = obj_Parametro.UsuarioCreacion;

        obj_Parametro.Estado = Kruma.Core.Business.Entity.Constante.Estado_Activo;
        ProcessResult obj_Resultado = Kruma.Core.Business.Logical.Parametro.Modificar(obj_Parametro);
        return obj_Resultado;
    }

    #endregion
}