using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Seguridad_Modulo : System.Web.UI.Page
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
        obj_MasterPage.TituloPagina = "Módulo";

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
    public static object ListarModulo(
        string str_pCodigo, string str_pDescripcion, string str_pEstado,
        int? int_pNumPagina, int? int_pTamPagina)
    {
        return Kruma.Core.Business.Logical.Modulo.Listar(null,str_pCodigo, str_pDescripcion, str_pEstado, int_pNumPagina, int_pTamPagina);
    }

    [WebMethod]
    public static Kruma.Core.Util.Common.ProcessResult ModificarEstado(
        string str_pIdModulo, string str_pEstado)
    {
        Kruma.Core.Business.Entity.Modulo obj_Modulo = new Kruma.Core.Business.Entity.Modulo();
        obj_Modulo.IdModulo = str_pIdModulo;
        obj_Modulo.Estado = str_pEstado;
        return Kruma.Core.Business.Logical.Modulo.ModificarEstado(obj_Modulo);
    }

    #endregion
}