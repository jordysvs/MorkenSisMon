using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Reporte_ReporteSistema : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CargaInicial();
    }
    #region Metodos Privados

    /// <summary>
    /// Carga Inicial del formulario
    /// </summary>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>Vicente Gonzales Osorio</CreadoPor></item>
    /// <item><FecCrea>30/01/2017</FecCrea></item></list></remarks>
    private void CargaInicial()
    {
        //Titulo
        Master_Default obj_MasterPage = (Master_Default)this.Master;
        obj_MasterPage.TituloPagina = "Reporte de alertas del sistema";

        ddlAlertaEstado.DataSource = Morken.SisMon.Negocio.AlertaEstado.Listar(null, null, Morken.SisMon.Entidad.Constante.Estado_Activo, null, null).Result;
        ddlAlertaEstado.DataValueField = "IdAlertaEstado";
        ddlAlertaEstado.DataTextField = "Descripcion";
        ddlAlertaEstado.DataBind();
        ddlAlertaEstado.Items.Insert(0, new ListItem("--Todos--", string.Empty));

    }
    #endregion

    #region Metodos Publicos

    [WebMethod]
    public static object ListarReporte(int? int_pIdAlertaEstado, DateTime? dt_pFechaAlertaInicio, DateTime? dt_pFechaAlertaFin, int? int_pNumPagina, int? int_pTamPagina)
    {
        return Morken.SisMon.Negocio.Alerta.ListarReporteSistema(int_pIdAlertaEstado, dt_pFechaAlertaInicio, dt_pFechaAlertaFin, int_pNumPagina, int_pTamPagina);
    }

    #endregion

}