using Kruma.Core.Util.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Seguridad_RegistroModulo : System.Web.UI.Page
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
        lnkEliminarFoto.ClientIDMode = System.Web.UI.ClientIDMode.Static;
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
        Master_Default obj_MasterPage = (Master_Default)this.Master;
        if (Request.QueryString["id"] == null)
        {
            obj_MasterPage.TituloPagina = "Agregar Módulo";
            lnkEliminarFoto.Style["display"] = "none";
        }
        else
        {
            obj_MasterPage.TituloPagina = "Modificar Módulo";
            hdIdModulo.Value = Request.QueryString["id"];
        }

        ddlEstado.DataSource = Kruma.Core.Util.CommonUtil.ListarEstado();
        ddlEstado.DataValueField = "Code";
        ddlEstado.DataTextField = "Description";
        ddlEstado.DataBind();

        int int_IdAlmacen = int.Parse(Kruma.Core.Business.Logical.Parametro.Obtener(
            Kruma.Core.Business.Entity.Constante.Parametro.Modulo,
            Kruma.Core.Business.Entity.Constante.Parametro.Almacen_Temporal).Valor);

        hdIdAlmacen.Value = int_IdAlmacen.ToString();

        //Imagen 
        string str_UrlImagen = string.Format("{0}{1}", Page.ResolveUrl("~"),
            Kruma.Core.Business.Logical.Parametro.Obtener(
            Kruma.Core.Business.Entity.Constante.Parametro.Modulo,
            "IMAGENMENU").Valor);

        hdImagenModulo.Value = str_UrlImagen;
        imgModulo.ImageUrl = str_UrlImagen;
    }

    #endregion

    #region Metodos Publicos

    /// <summary>
    /// Obtiene los datos del modulo
    /// </summary>
    /// <param name="int_pIdPersona">Id del modulo</param>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>John Castillo</CreadoPor></item>
    /// <item><FecCrea>14/04/2016</FecCrea></item></list></remarks>
    [WebMethod]
    public static Kruma.Core.Business.Entity.Modulo ObtenerModulo(string str_pIdModulo)
    {
        return Kruma.Core.Business.Logical.Modulo.Obtener(str_pIdModulo);
    }

    /// <summary>
    /// Guarda la informacion del modulo
    /// </summary>
    /// <param name="str_pModulo">Json Modulo</param>
    /// <returns>Resultado del proceso</returns>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>Creado por John Castillo</CreadoPor></item>
    /// <item><FecCrea>14/04/2016</FecCrea></item></list></remarks>
    [WebMethod]
    public static ProcessResult GuardarModulo(string str_pModulo, string str_pIdModulo)
    {
        JavaScriptSerializer obj_JsSerializer = new JavaScriptSerializer();
        Kruma.Core.Business.Entity.Modulo obj_Modulo = obj_JsSerializer.Deserialize<Kruma.Core.Business.Entity.Modulo>(str_pModulo);
        obj_Modulo.UsuarioCreacion = Kruma.Core.Security.SecurityManager.Usuario.IdUsuario;
        obj_Modulo.UsuarioModificacion = obj_Modulo.UsuarioCreacion;

        ProcessResult obj_Resultado = null;
        if (string.IsNullOrEmpty(str_pIdModulo))
            obj_Resultado = Kruma.Core.Business.Logical.Modulo.Insertar(obj_Modulo);
        else
            obj_Resultado = Kruma.Core.Business.Logical.Modulo.Modificar(obj_Modulo);
        return obj_Resultado;
    }

    [WebMethod]
    public static string ObtenerImagenURL(int? int_pIdRegistro)
    {
        string str_UrlImagen = string.Empty;
        if (int_pIdRegistro.HasValue)
        {
            int? int_IdAlmacenModulo =
                int_IdAlmacenModulo = int.Parse(
                      Kruma.Core.Business.Logical.Parametro.Obtener(
                    Kruma.Core.Business.Entity.Constante.Parametro.Modulo,
                    Kruma.Core.Business.Entity.Constante.Parametro.Almacen_Modulo).Valor);

            Kruma.Core.FileServer.FileServerManager obj_FileServerManager = new Kruma.Core.FileServer.FileServerManager();
            System.Collections.Generic.List<Kruma.Core.FileServer.Entity.FileUpload> lst_Fotos = obj_FileServerManager.getFiles(int_IdAlmacenModulo.Value, int_pIdRegistro.Value);
            if (lst_Fotos.Count > 0)
            {
                str_UrlImagen = string.Format("../../Ashx/FileServerViewHandler.ashx?ref={0}",
                    new Kruma.Core.Criptography.CriptographyManager().Encrypt(
                    string.Format("{0}|{1}|{2}", lst_Fotos[0].IdAlmacen, lst_Fotos[0].IdRegistro, lst_Fotos[0].IdDocumento)
                    ));
            }
        }
        return str_UrlImagen;
    }

    #endregion
}