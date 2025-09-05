using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Seguridad_Perfil : System.Web.UI.Page
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
        obj_MasterPage.TituloPagina = "Perfil";

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
    public static object ListarPerfil(
        string str_pIdModulo,
        string str_pIdPerfil,
        string str_pDescripcion,
        string str_pEstado,
        int? int_pNumPagina,
        int? int_pTamPagina)
    {
        string str_Sistema = Kruma.Core.Security.SecurityManager.Usuario.Sistema;
        if (str_Sistema == Kruma.Core.Security.Entity.Constante.Condicion_Positivo)
            str_Sistema = string.Empty;

        string str_IdModulo = str_pIdModulo;
        if (string.IsNullOrEmpty(str_IdModulo))
        {
            foreach (Kruma.Core.Business.Entity.Modulo obj_Modulo in Kruma.Core.Security.SecurityManager.Usuario.ModulosPagina)
                str_IdModulo = string.Format("{0}{1},", str_IdModulo, obj_Modulo.IdModulo);
            if (str_IdModulo.Length > 0)
                str_IdModulo = str_IdModulo.Remove(str_IdModulo.Length - 1, 1);
        }

        return Kruma.Core.Security.Logical.Perfil.Listar(str_IdModulo, str_pIdPerfil, str_pDescripcion, str_Sistema, str_pEstado, int_pNumPagina, int_pTamPagina);
    }

    [WebMethod]
    public static Kruma.Core.Util.Common.ProcessResult ModificarEstado(
        string str_pIdModulo,
        string str_pIdPerfil,
        string str_pEstado)
    {
        Kruma.Core.Security.Entity.Perfil obj_Perfil = new Kruma.Core.Security.Entity.Perfil();
        obj_Perfil.IdModulo = str_pIdModulo;
        obj_Perfil.IdPerfil = str_pIdPerfil;
        obj_Perfil.Estado = str_pEstado;
        return Kruma.Core.Security.Logical.Perfil.ModificarEstado(obj_Perfil);
    }

    #endregion
}