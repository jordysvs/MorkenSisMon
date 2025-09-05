using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Seguridad_RegistroPerfil : System.Web.UI.Page
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
        if (Request.QueryString["id"] == null)
        {
            obj_MasterPage.TituloPagina = "Agregar Perfil";
        }
        else
        {
            obj_MasterPage.TituloPagina = "Modificar Perfil";
            hdIdModulo.Value = Request.QueryString["id"];
            hdIdPerfil.Value = Request.QueryString["id2"];
            txtCodigo.Text = Request.QueryString["id2"];
        }

        ddlEstado.DataSource = Kruma.Core.Util.CommonUtil.ListarEstado();
        ddlEstado.DataValueField = "Code";
        ddlEstado.DataTextField = "Description";
        ddlEstado.DataBind();
    }

    #endregion

    #region Metodos Publicos

    /// <summary>
    /// Obtiene los datos del perfil
    /// </summary>
    /// <param name="str_pIdPerfil">Id del perfil</param>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>John Castillo</CreadoPor></item>
    /// <item><FecCrea>22/07/2015</FecCrea></item></list></remarks>
    [WebMethod]
    public static Kruma.Core.Security.Entity.Perfil ObtenerPerfil(
        string str_pIdModulo,
        string str_pIdPerfil)
    {
        return Kruma.Core.Security.Logical.Perfil.Obtener(str_pIdModulo, str_pIdPerfil);
    }

    /// <summary>
    /// Guarda la informacion del perfil
    /// </summary>
    /// <param name="str_pPerfil">Json Perfil</param>
    /// <returns>Resultado del proceso</returns>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>Creado por John Castillo</CreadoPor></item>
    /// <item><FecCrea>22/07/2015</FecCrea></item></list></remarks>
    [WebMethod]
    public static Kruma.Core.Util.Common.ProcessResult GuardarPerfil(
        string str_pPerfil,
        string str_pIdPerfil)
    {
        JavaScriptSerializer obj_JsSerializer = new JavaScriptSerializer();

        //Auditoria para el perfil
        Kruma.Core.Security.Entity.Perfil obj_Perfil = obj_JsSerializer.Deserialize<Kruma.Core.Security.Entity.Perfil>(str_pPerfil);
        obj_Perfil.UsuarioCreacion = Kruma.Core.Security.SecurityManager.Usuario.IdUsuario;
        obj_Perfil.UsuarioModificacion = obj_Perfil.UsuarioCreacion;

        //Auditoria para los accesos del perfil
        foreach (Kruma.Core.Security.Entity.PerfilGrupoDetalleAcceso obj_Acceso in obj_Perfil.Accesos)
        {
            obj_Acceso.Estado = Kruma.Core.Security.Entity.Constante.Estado_Activo;
            obj_Acceso.UsuarioCreacion = Kruma.Core.Security.SecurityManager.Usuario.IdUsuario;
            obj_Acceso.UsuarioModificacion = obj_Perfil.UsuarioCreacion;
        }

        Kruma.Core.Util.Common.ProcessResult obj_Resultado = null;
        if (string.IsNullOrEmpty(str_pIdPerfil))
            obj_Resultado = Kruma.Core.Security.Logical.Perfil.Insertar(obj_Perfil);
        else
            obj_Resultado = Kruma.Core.Security.Logical.Perfil.Modificar(obj_Perfil);
        return obj_Resultado;
    }

    [WebMethod]
    public static List<Kruma.Core.Util.Common.FancyTree> Listar(
        string str_pIdModulo,
        string str_pIdPerfil)
    {
        List<string> lst_IdModulo = new List<string>();
        lst_IdModulo.Add(str_pIdModulo);
        if (str_pIdModulo.TrimEnd() != Kruma.Core.Business.Entity.Constante.Parametro.Modulo)
            lst_IdModulo.Add(Kruma.Core.Business.Entity.Constante.Parametro.Modulo);

        Kruma.Core.Util.Common.FancyTree obj_MenuRoot = null;
        Kruma.Core.Business.Entity.Modulo obj_Modulo = null;
        List<Kruma.Core.Security.Entity.GrupoDetalle> lst_GrupoDetalle = null;
        List<Kruma.Core.Util.Common.FancyTree> lstResultado = new List<Kruma.Core.Util.Common.FancyTree>();

        foreach (string str_IdModulo in lst_IdModulo)
        {
            obj_Modulo = Kruma.Core.Business.Logical.Modulo.Obtener(str_IdModulo);

            obj_MenuRoot = new Kruma.Core.Util.Common.FancyTree();
            obj_MenuRoot.title = obj_Modulo.Descripcion;
            obj_MenuRoot.folder = true;
            obj_MenuRoot.expanded = true;
            obj_MenuRoot.source = obj_Modulo;
            obj_MenuRoot.hideCheckbox = true;

            lst_GrupoDetalle = Kruma.Core.Security.Logical.PerfilGrupoDetalleAcceso.ListarPorPerfil(str_pIdModulo, str_pIdPerfil, str_IdModulo);

            var lst_Grupo = (from obj_GrupoDetalle in lst_GrupoDetalle
                             select new
                             {
                                 IdModulo = obj_GrupoDetalle.Grupo.IdModulo,
                                 IdGrupo = obj_GrupoDetalle.Grupo.IdGrupo,
                                 Descripcion = obj_GrupoDetalle.Grupo.Descripcion
                             }).Distinct().ToList();


            List<Kruma.Core.Util.Common.FancyTree> lst_Menu = new List<Kruma.Core.Util.Common.FancyTree>();
            Kruma.Core.Util.Common.FancyTree obj_Menu = null;
            foreach (var obj_Grupo in lst_Grupo)
            {
                obj_Menu = new Kruma.Core.Util.Common.FancyTree();
                obj_Menu.title = obj_Grupo.Descripcion;
                //obj_Menu.hideCheckbox = true;
                obj_Menu.folder = true;
                obj_Menu.expanded = true;
                obj_Menu.source = obj_Grupo;
                obj_Menu.children = ListarDetalle(obj_Grupo.IdModulo, obj_Grupo.IdGrupo, null, lst_GrupoDetalle);
                lst_Menu.Add(obj_Menu);
            }
            obj_MenuRoot.children = lst_Menu;
            lstResultado.Add(obj_MenuRoot);
        }

        return lstResultado;
    }

    private static List<Kruma.Core.Util.Common.FancyTree> ListarDetalle(
        string str_pIdModulo,
        string str_pIdGrupo,
        string str_pIdDetalle,
        List<Kruma.Core.Security.Entity.GrupoDetalle> lst_pDetalle
        )
    {
        List<Kruma.Core.Util.Common.FancyTree> lst_Detalle = new List<Kruma.Core.Util.Common.FancyTree>();
        foreach (Kruma.Core.Security.Entity.GrupoDetalle obj_Detalle in (from obj_Item in lst_pDetalle
                                                                         where
                                                                         obj_Item.IdModulo.Equals(str_pIdModulo) &&
                                                                         obj_Item.IdGrupo.Equals(str_pIdGrupo) &&
                                                                         (obj_Item.IdDetallePadre == null ?
                                                                         (obj_Item.IdDetallePadre == null && str_pIdDetalle == null) :
                                                                         obj_Item.IdDetallePadre.Equals(str_pIdDetalle))
                                                                         select obj_Item).ToList())
        {
            Kruma.Core.Util.Common.FancyTree obj_Menu = new Kruma.Core.Util.Common.FancyTree();
            obj_Menu.title = obj_Detalle.Descripcion;
            obj_Menu.selected = obj_Detalle.Seleccion;
            obj_Menu.children = ListarDetalle(obj_Detalle.IdModulo, obj_Detalle.IdGrupo, obj_Detalle.IdDetalle, lst_pDetalle);
            obj_Menu.folder = obj_Menu.children.Count() > 0;
            obj_Menu.source = obj_Detalle;
            lst_Detalle.Add(obj_Menu);
        }
        return lst_Detalle;
    }
    #endregion
}