using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Seguridad_Dispositivo : System.Web.UI.Page
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
        obj_MasterPage.TituloPagina = "Dispositivo";

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
    public static object ListarDispositivo(
        string str_pIp, string str_pNombre, string str_pEstado,
        int? int_pNumPagina, int? int_pTamPagina)
    {
        return Kruma.Core.Security.Logical.Dispositivo.Listar(str_pIp, str_pNombre, str_pEstado, int_pNumPagina, int_pTamPagina);
    }

    [WebMethod]
    public static Kruma.Core.Util.Common.ProcessResult ModificarEstado(
        int int_pIdDispositivo, string str_pEstado)
    {
        Kruma.Core.Security.Entity.Dispositivo obj_Dispositivo = new Kruma.Core.Security.Entity.Dispositivo();
        obj_Dispositivo.IdDispositivo = int_pIdDispositivo;
        obj_Dispositivo.Estado = str_pEstado;
        return Kruma.Core.Security.Logical.Dispositivo.ModificarEstado(obj_Dispositivo);
    }

    #endregion
}