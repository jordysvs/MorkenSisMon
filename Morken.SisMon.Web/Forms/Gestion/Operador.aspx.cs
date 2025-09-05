using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Gestion_Operador : System.Web.UI.Page
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
        obj_MasterPage.TituloPagina = "Operador";

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
    public static object ListarOperador(
        string str_pNumeroDocumento, string str_pNombreCompleto, string str_pEstado, int? int_pNumPagina, int? int_pTamPagina)
    {
        return Morken.SisMon.Negocio.Operador.Listar(null, null, str_pNumeroDocumento, str_pNombreCompleto, str_pEstado,  int_pNumPagina,  int_pTamPagina);
    }

    [WebMethod]
    public static Kruma.Core.Util.Common.ProcessResult ModificarEstado(
        int int_pIdOperador, string str_pEstado)
    {
        Morken.SisMon.Entidad.Operador obj_Operador = new Morken.SisMon.Entidad.Operador();
        obj_Operador.IdOperador = int_pIdOperador;
        obj_Operador.Estado = str_pEstado;
        return Morken.SisMon.Negocio.Operador.ModificarEstado(obj_Operador);
    }

    #endregion
}