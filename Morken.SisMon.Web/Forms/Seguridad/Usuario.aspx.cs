using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Seguridad_Usuario : System.Web.UI.Page
{
    #region Eventos

    /// <summary>
    /// Carga inicial de la pagina
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
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
        obj_MasterPage.TituloPagina = "Usuario";

        ddlEstado.DataSource = Kruma.Core.Util.CommonUtil.ListarEstado();
        ddlEstado.DataValueField = "Code";
        ddlEstado.DataTextField = "Description";
        ddlEstado.DataBind();

        ddlEstado.Items.Insert(0, new ListItem("--Todos--", string.Empty));
        ddlEstado.SelectedValue = Kruma.Core.Business.Entity.Constante.Estado_Activo;
    }

    #endregion

    #region Metodos Publicos

    [WebMethod]
    public static Kruma.Core.Util.Common.List<Kruma.Core.Security.Entity.Usuario> ListarUsuario(
        string str_pIdUsuario, string str_pNombreCompleto, string str_pIdPerfil, string str_pEstado,
        int? int_pNumPagina, int? int_pTamPagina)
    {
        string str_Sistema = Kruma.Core.Security.SecurityManager.Usuario.Sistema;
        if (str_Sistema == Kruma.Core.Security.Entity.Constante.Condicion_Positivo)
            str_Sistema = string.Empty;

        Kruma.Core.Criptography.CriptographyManager obj_CriptographyManager = new Kruma.Core.Criptography.CriptographyManager();
        Kruma.Core.Util.Common.List<Kruma.Core.Security.Entity.Usuario> lst_Usuario = Kruma.Core.Security.Logical.Usuario.Listar(str_pIdUsuario, str_pNombreCompleto, str_pIdPerfil, null, str_Sistema, str_pEstado, int_pNumPagina, int_pTamPagina);
        foreach (Kruma.Core.Security.Entity.Usuario obj_Usuario in lst_Usuario.Result)
            obj_Usuario.Clave = obj_CriptographyManager.Dencrypt(obj_Usuario.Clave);
        return lst_Usuario;
    }

    [WebMethod]
    public static List<Kruma.Core.Security.Entity.Perfil> ListarPerfil(
        string str_pIdModulo)
    {
        string str_Sistema = Kruma.Core.Security.SecurityManager.Usuario.Sistema;
        if (str_Sistema == Kruma.Core.Security.Entity.Constante.Condicion_Positivo)
            str_Sistema = string.Empty;
        return Kruma.Core.Security.Logical.Perfil.Listar(str_pIdModulo, null, null, str_Sistema, null, null, null).Result;
    }

    [WebMethod]
    public static Kruma.Core.Util.Common.ProcessResult ModificarEstado(
        string str_pIdUsuario, string str_pEstado)
    {
        Kruma.Core.Security.Entity.Usuario obj_Usuario = new Kruma.Core.Security.Entity.Usuario();
        obj_Usuario.IdUsuario = str_pIdUsuario;
        obj_Usuario.Estado = str_pEstado;
        return Kruma.Core.Security.Logical.Usuario.ModificarEstado(obj_Usuario);
    }

    #endregion
}