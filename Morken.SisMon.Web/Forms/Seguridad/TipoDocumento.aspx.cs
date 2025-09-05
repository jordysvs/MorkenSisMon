using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Configuracion_TipoDocumento : System.Web.UI.Page
{
    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            CargaInicial();
    }

    #endregion

    #region Metodos Privados

    private void CargaInicial()
    {
        Master_Default obj_MasterPage = (Master_Default)this.Master;
        obj_MasterPage.TituloPagina = "Tipo de Documento";

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
    public static object ListarTipoDocumento(
        string str_pCodigo, string str_pDescripcion, string str_pEstado, int? int_pNumPagina, int? int_pTamPagina)
    {
        return Kruma.Core.Business.Logical.TipoDocumento.Listar(str_pCodigo, str_pDescripcion, str_pEstado, int_pNumPagina, int_pTamPagina);
    }

    [WebMethod]
    public static Kruma.Core.Util.Common.ProcessResult ModificarEstado(
        int int_pIdTipoDocumento, string str_pEstado)
    {
        Kruma.Core.Business.Entity.TipoDocumento obj_TipoDocumento = new Kruma.Core.Business.Entity.TipoDocumento();
        obj_TipoDocumento.IdTipoDocumento = int_pIdTipoDocumento;
        obj_TipoDocumento.Estado = str_pEstado;
        return Kruma.Core.Business.Logical.TipoDocumento.ModificarEstado(obj_TipoDocumento);
    }

    #endregion
}