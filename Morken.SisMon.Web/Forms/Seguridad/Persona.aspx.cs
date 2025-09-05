using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Seguridad_Persona : System.Web.UI.Page
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
        obj_MasterPage.TituloPagina = "Persona";

        ddlTipoDocumento.DataSource = Kruma.Core.Business.Logical.TipoDocumento.Listar(null, null, Kruma.Core.Business.Entity.Constante.Estado_Activo, null, null).Result;
        ddlTipoDocumento.DataValueField = "IdTipoDocumento";
        ddlTipoDocumento.DataTextField = "Descripcion";
        ddlTipoDocumento.DataBind();
        ddlTipoDocumento.Items.Insert(0, new ListItem("--Seleccione--", string.Empty));

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
    public static object ListarPersona(
        string str_pNombreCompleto, int? int_pIdTipoDocumento, string str_pNumeroDocumento, string str_pEstado, int? int_pNumPagina, int? int_pTamPagina)
    {
        string str_Sistema = Kruma.Core.Security.SecurityManager.Usuario.Sistema;
        if (str_Sistema == Kruma.Core.Security.Entity.Constante.Condicion_Positivo)
            str_Sistema = string.Empty;
        return Kruma.Core.Business.Logical.Persona.Listar(str_pNombreCompleto, int_pIdTipoDocumento, null, str_pNumeroDocumento, str_Sistema, str_pEstado, int_pNumPagina, int_pTamPagina);
    }

    [WebMethod]
    public static Kruma.Core.Util.Common.ProcessResult ModificarEstado(
        int int_pIdPersona, string str_pEstado)
    {
        Kruma.Core.Business.Entity.Persona obj_Persona = new Kruma.Core.Business.Entity.Persona();
        obj_Persona.IdPersona = int_pIdPersona;
        obj_Persona.Estado = str_pEstado;
        return Kruma.Core.Business.Logical.Persona.ModificarEstado(obj_Persona);
    }

    #endregion
}