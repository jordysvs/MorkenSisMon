using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Seguridad_Menu : System.Web.UI.Page
{
    #region Eventos

    /// <summary>
    /// Carga inicial de la pagina
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        lnkEliminarFotoGrupo.ClientIDMode = System.Web.UI.ClientIDMode.Static;
        lnkEliminarFotoDetalle.ClientIDMode = System.Web.UI.ClientIDMode.Static;

        if (!IsPostBack)
            CargaInicial();
    }

    #endregion

    #region Metodos Privados

    /// <summary>
    /// Carga inicial del formulario
    /// </summary>
    private void CargaInicial()
    {
        Master_Default obj_MasterPage = (Master_Default)this.Master;
        obj_MasterPage.TituloPagina = "Menu";

        List<Kruma.Core.Util.Common.Entity> lst_Estado = Kruma.Core.Util.CommonUtil.ListarEstado();

        ddlEstado.DataSource = lst_Estado;
        ddlEstado.DataValueField = "Code";
        ddlEstado.DataTextField = "Description";
        ddlEstado.DataBind();

        ddlEstado.Items.Insert(0, new ListItem("--Todos--", string.Empty));
        ddlEstado.SelectedValue = Kruma.Core.Security.Entity.Constante.Estado_Activo;


        ddlEstadoGrupo.DataSource = lst_Estado;
        ddlEstadoGrupo.DataValueField = "Code";
        ddlEstadoGrupo.DataTextField = "Description";
        ddlEstadoGrupo.DataBind();
        ddlEstadoGrupo.Items.Insert(0, new ListItem("--Seleccione--", string.Empty));

        ddlEstadoDetalle.DataSource = lst_Estado;
        ddlEstadoDetalle.DataValueField = "Code";
        ddlEstadoDetalle.DataTextField = "Description";
        ddlEstadoDetalle.DataBind();
        ddlEstadoDetalle.Items.Insert(0, new ListItem("--Seleccione--", string.Empty));

        List<Kruma.Core.Util.Common.Entity> lst_Visible = new List<Kruma.Core.Util.Common.Entity>();
        lst_Visible.Add(new Kruma.Core.Util.Common.Entity()
        {
            Id = Kruma.Core.Security.Entity.Constante.Condicion_Positivo,
            Description = "Visible"
        });
        lst_Visible.Add(new Kruma.Core.Util.Common.Entity()
        {
            Id = Kruma.Core.Security.Entity.Constante.Condicion_Negativo,
            Description = "No Visible"
        });
        ddlVisible.DataSource = lst_Visible;
        ddlVisible.DataValueField = "Id";
        ddlVisible.DataTextField = "Description";
        ddlVisible.DataBind();
        ddlVisible.Items.Insert(0, new ListItem("--Todos--", string.Empty));

        hdIdAlmacen.Value = Kruma.Core.Business.Logical.Parametro.Obtener(
            Kruma.Core.Business.Entity.Constante.Parametro.Modulo,
            Kruma.Core.Business.Entity.Constante.Parametro.Almacen_Temporal).Valor;

        string str_UrlImagen = string.Format("{0}{1}", Page.ResolveUrl("~"),
            Kruma.Core.Business.Logical.Parametro.Obtener(
            Kruma.Core.Business.Entity.Constante.Parametro.Modulo,
            "IMAGENMENU").Valor);
        hdImagenMenu.Value = str_UrlImagen;
        imgImagenGrupo.ImageUrl = str_UrlImagen;
        imgImagenDetalle.ImageUrl = str_UrlImagen;
    }

    #endregion

    #region Metodos Publicos

    [WebMethod]
    public static List<Kruma.Core.Util.Common.FancyTree> ListarGrupo(
        string str_pIdModulo,
        string str_pDescripcion,
        string str_pURL,
        string str_pFlagVisible,
        string str_pEstado)
    {
        string str_IdModulo = str_pIdModulo;
        if (string.IsNullOrEmpty(str_IdModulo))
        {
            foreach (Kruma.Core.Business.Entity.Modulo obj_Modulo in Kruma.Core.Security.SecurityManager.Usuario.ModulosPagina)
                str_IdModulo = string.Format("{0}{1},", str_IdModulo, obj_Modulo.IdModulo);
            if (str_IdModulo.Length > 0)
                str_IdModulo = str_IdModulo.Remove(str_IdModulo.Length - 1, 1);
        }

        List<Kruma.Core.Security.Entity.Grupo> lst_Grupo = Kruma.Core.Security.Logical.Grupo.Listar(str_IdModulo, null, str_pDescripcion, str_pURL, str_pFlagVisible, str_pEstado, null, null).Result;
        List<Kruma.Core.Business.Entity.Modulo> lst_Modulo = Kruma.Core.Business.Logical.Modulo.Listar(str_IdModulo, null, null, Kruma.Core.Business.Entity.Constante.Estado_Activo, null, null).Result;

        List<Kruma.Core.Util.Common.FancyTree> lst_Menu = null;
        Kruma.Core.Util.Common.FancyTree obj_MenuRoot = null;
        Kruma.Core.Util.Common.FancyTree obj_Menu = null;
        List<Kruma.Core.Util.Common.FancyTree> lstResultado = new List<Kruma.Core.Util.Common.FancyTree>();

        foreach (Kruma.Core.Business.Entity.Modulo obj_Modulo in lst_Modulo)
        {
            obj_MenuRoot = new Kruma.Core.Util.Common.FancyTree();
            obj_MenuRoot.title = obj_Modulo.Descripcion;
            obj_MenuRoot.source = obj_Modulo;
            obj_MenuRoot.folder = true;
            obj_MenuRoot.expanded = true;
            lst_Menu = new List<Kruma.Core.Util.Common.FancyTree>();
            foreach (Kruma.Core.Security.Entity.Grupo obj_Grupo in lst_Grupo.Where(x => x.IdModulo == obj_Modulo.IdModulo).ToList())
            {
                obj_Menu = new Kruma.Core.Util.Common.FancyTree();
                obj_Menu.title = obj_Grupo.Descripcion;
                obj_Menu.folder = obj_Grupo.Total_Detalle > 0;
                obj_Menu.lazy = obj_Grupo.Total_Detalle > 0;
                obj_Menu.source = obj_Grupo;
                lst_Menu.Add(obj_Menu);
            }
            obj_MenuRoot.children = lst_Menu;
            lstResultado.Add(obj_MenuRoot);
        }

        return lstResultado.OrderBy(x => ((Kruma.Core.Business.Entity.Modulo)x.source).Descripcion).ToList();
    }

    [WebMethod]
    public static List<Kruma.Core.Util.Common.FancyTree> ListarGrupoDetalle(string str_pIdModulo, string str_pIdGrupo, string str_pIdDetalle, string str_pDescripcion, string str_pURL, string str_pFlagVisible, string str_pEstado)
    {
        List<Kruma.Core.Security.Entity.GrupoDetalle> lst_GrupoDetalle = Kruma.Core.Security.Logical.GrupoDetalle.Listar(str_pIdModulo, str_pIdGrupo, null, str_pDescripcion, str_pIdDetalle, str_pURL, str_pFlagVisible, str_pEstado, null, null).Result;
        List<Kruma.Core.Util.Common.FancyTree> lst_Menu = new List<Kruma.Core.Util.Common.FancyTree>();
        Kruma.Core.Util.Common.FancyTree obj_Menu = null;
        foreach (Kruma.Core.Security.Entity.GrupoDetalle obj_GrupoDetalle in lst_GrupoDetalle)
        {
            obj_Menu = new Kruma.Core.Util.Common.FancyTree();
            //obj_Menu.key = string.Format("D|{0}|{1}", obj_GrupoDetalle.IdGrupo, obj_GrupoDetalle.IdDetalle);
            obj_Menu.title = obj_GrupoDetalle.Descripcion;
            obj_Menu.folder = obj_GrupoDetalle.Total_Detalle > 0;
            obj_Menu.lazy = obj_GrupoDetalle.Total_Detalle > 0;
            obj_Menu.source = obj_GrupoDetalle;
            lst_Menu.Add(obj_Menu);
        }
        return lst_Menu;
    }

    [WebMethod]
    public static Kruma.Core.Security.Entity.Grupo ObtenerGrupo(string str_pIdModulo, string str_pIdGrupo)
    {
        return Kruma.Core.Security.Logical.Grupo.Obtener(str_pIdModulo, str_pIdGrupo);
    }

    [WebMethod]
    public static Kruma.Core.Security.Entity.GrupoDetalle ObtenerDetalle(string str_pIdModulo, string str_pIdGrupo, string str_pIdDetalle)
    {
        return Kruma.Core.Security.Logical.GrupoDetalle.Obtener(str_pIdModulo, str_pIdGrupo, str_pIdDetalle);
    }

    /// <summary>
    /// Guarda la informacion del grupo
    /// </summary>
    /// <param name="str_pGrupo">Json Grupo</param>
    /// <returns>Resultado del proceso</returns>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>Creado por John Castillo</CreadoPor></item>
    /// <item><FecCrea>16/12/2014</FecCrea></item></list></remarks>
    [WebMethod]
    public static Kruma.Core.Util.Common.ProcessResult GuardarGrupo(string str_pGrupo)
    {
        JavaScriptSerializer obj_JsSerializer = new JavaScriptSerializer();
        Kruma.Core.Security.Entity.Grupo obj_Grupo = obj_JsSerializer.Deserialize<Kruma.Core.Security.Entity.Grupo>(str_pGrupo);
        obj_Grupo.UsuarioCreacion = Kruma.Core.Security.SecurityManager.Usuario.IdUsuario;
        Kruma.Core.Util.Common.ProcessResult obj_Resultado = Kruma.Core.Security.Logical.Grupo.Insertar(obj_Grupo);
        return obj_Resultado;
    }

    /// <summary>
    /// Guarda la informacion del grupo
    /// </summary>
    /// <param name="str_pGrupo">Json Grupo</param>
    /// <returns>Resultado del proceso</returns>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>Creado por John Castillo</CreadoPor></item>
    /// <item><FecCrea>16/12/2014</FecCrea></item></list></remarks>
    [WebMethod]
    public static Kruma.Core.Util.Common.ProcessResult ActualizarGrupo(string str_pGrupo)
    {
        JavaScriptSerializer obj_JsSerializer = new JavaScriptSerializer();
        Kruma.Core.Security.Entity.Grupo obj_Grupo = obj_JsSerializer.Deserialize<Kruma.Core.Security.Entity.Grupo>(str_pGrupo);
        obj_Grupo.UsuarioModificacion = Kruma.Core.Security.SecurityManager.Usuario.IdUsuario;
        Kruma.Core.Util.Common.ProcessResult obj_Resultado = Kruma.Core.Security.Logical.Grupo.Modificar(obj_Grupo);
        return obj_Resultado;
    }

    /// <summary>
    /// Guarda la informacion del detalle
    /// </summary>
    /// <param name="str_pDetalle">Json Detalle</param>
    /// <returns>Resultado del proceso</returns>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>Creado por John Castillo</CreadoPor></item>
    /// <item><FecCrea>16/12/2014</FecCrea></item></list></remarks>
    [WebMethod]
    public static Kruma.Core.Util.Common.ProcessResult GuardarDetalle(string str_pDetalle)
    {
        JavaScriptSerializer obj_JsSerializer = new JavaScriptSerializer();
        Kruma.Core.Security.Entity.GrupoDetalle obj_GrupoDetalle = obj_JsSerializer.Deserialize<Kruma.Core.Security.Entity.GrupoDetalle>(str_pDetalle);
        obj_GrupoDetalle.UsuarioCreacion = Kruma.Core.Security.SecurityManager.Usuario.IdUsuario;
        return Kruma.Core.Security.Logical.GrupoDetalle.Insertar(obj_GrupoDetalle);
    }

    /// <summary>
    /// Guarda la informacion del detalle
    /// </summary>
    /// <param name="str_pDetalle">Json Detalle</param>
    /// <returns>Resultado del proceso</returns>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>Creado por John Castillo</CreadoPor></item>
    /// <item><FecCrea>16/12/2014</FecCrea></item></list></remarks>
    [WebMethod]
    public static Kruma.Core.Util.Common.ProcessResult ActualizarDetalle(string str_pDetalle)
    {
        JavaScriptSerializer obj_JsSerializer = new JavaScriptSerializer();
        Kruma.Core.Security.Entity.GrupoDetalle obj_GrupoDetalle = obj_JsSerializer.Deserialize<Kruma.Core.Security.Entity.GrupoDetalle>(str_pDetalle);
        obj_GrupoDetalle.UsuarioCreacion = Kruma.Core.Security.SecurityManager.Usuario.IdUsuario;
        obj_GrupoDetalle.UsuarioModificacion = obj_GrupoDetalle.UsuarioCreacion;
        return Kruma.Core.Security.Logical.GrupoDetalle.Modificar(obj_GrupoDetalle);
    }
    //[WebMethod]
    //public static Kruma.Core.Util.Common.ProcessResult ModificarEstado(
    //    string str_pIdUsuario, string str_pEstado)
    //{
    //    Kruma.Core.Security.Entity.Usuario obj_Usuario = new Kruma.Core.Security.Entity.Usuario();
    //    obj_Usuario.IdUsuario = str_pIdUsuario;
    //    obj_Usuario.Estado = str_pEstado;
    //    return Kruma.Core.Security.Logical.Usuario.ModificarEstado(obj_Usuario);
    //}

    /// <summary>
    /// Guarda el orden del grupo
    /// </summary>
    /// <param name="str_pGrupo">Json Grupo</param>
    /// <returns>Resultado del proceso</returns>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>Creado por John Castillo</CreadoPor></item>
    /// <item><FecCrea>30/07/2015</FecCrea></item></list></remarks>
    [WebMethod]
    public static Kruma.Core.Util.Common.ProcessResult ModificarOrdenGrupo(string lst_pGrupo)
    {
        JavaScriptSerializer obj_JsSerializer = new JavaScriptSerializer();
        List<Kruma.Core.Security.Entity.Grupo> lst_Grupo = obj_JsSerializer.Deserialize<List<Kruma.Core.Security.Entity.Grupo>>(lst_pGrupo);
        foreach (Kruma.Core.Security.Entity.Grupo obj_Grupo in lst_Grupo)
            obj_Grupo.UsuarioCreacion = Kruma.Core.Security.SecurityManager.Usuario.IdUsuario;
        return Kruma.Core.Security.Logical.Grupo.ModificarOrden(lst_Grupo);
    }

    /// <summary>
    /// Guarda el orden del detale del grupo
    /// </summary>
    /// <param name="str_pGrupo">Json Grupo</param>
    /// <returns>Resultado del proceso</returns>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>Creado por John Castillo</CreadoPor></item>
    /// <item><FecCrea>30/07/2015</FecCrea></item></list></remarks>
    [WebMethod]
    public static Kruma.Core.Util.Common.ProcessResult ModificarOrdenGrupoDetalle(string lst_pGrupoDetalle)
    {
        JavaScriptSerializer obj_JsSerializer = new JavaScriptSerializer();
        List<Kruma.Core.Security.Entity.GrupoDetalle> lst_GrupoDetalle = obj_JsSerializer.Deserialize<List<Kruma.Core.Security.Entity.GrupoDetalle>>(lst_pGrupoDetalle);
        foreach (Kruma.Core.Security.Entity.GrupoDetalle obj_Grupo in lst_GrupoDetalle)
            obj_Grupo.UsuarioCreacion = Kruma.Core.Security.SecurityManager.Usuario.IdUsuario;
        return Kruma.Core.Security.Logical.GrupoDetalle.ModificarOrden(lst_GrupoDetalle);
    }

    [WebMethod]
    public static string ObtenerImagenURL(int? int_pIdRegistro, string str_pTipo)
    {
        string str_UrlImagen = string.Empty;
        if (int_pIdRegistro.HasValue)
        {
            int? int_IdAlmacenMenu = null;
            if (str_pTipo == "G")
                int_IdAlmacenMenu = int.Parse(
                    Kruma.Core.Business.Logical.Parametro.Obtener(
                    Kruma.Core.Business.Entity.Constante.Parametro.Modulo,
                    Kruma.Core.Business.Entity.Constante.Parametro.Almacen_Grupo).Valor);
            else
                int_IdAlmacenMenu = int.Parse(
                      Kruma.Core.Business.Logical.Parametro.Obtener(
                    Kruma.Core.Business.Entity.Constante.Parametro.Modulo,
                    Kruma.Core.Business.Entity.Constante.Parametro.Almacen_GrupoDetalle).Valor);

            Kruma.Core.FileServer.FileServerManager obj_FileServerManager = new Kruma.Core.FileServer.FileServerManager();
            List<Kruma.Core.FileServer.Entity.FileUpload> lst_Fotos = obj_FileServerManager.getFiles(int_IdAlmacenMenu.Value, int_pIdRegistro.Value);
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