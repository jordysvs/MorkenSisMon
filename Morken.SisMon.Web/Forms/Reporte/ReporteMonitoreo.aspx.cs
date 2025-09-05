using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Reportes_ReporteMonitoreo : System.Web.UI.Page
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
        obj_MasterPage.TituloPagina = "Reporte de Monitoreo";

        ddlTipoAlerta.DataSource = Morken.SisMon.Negocio.TipoAlerta.Listar(null, null, Morken.SisMon.Entidad.Constante.Estado_Activo, null, null).Result;
        ddlTipoAlerta.DataValueField = "IdTipoAlerta";
        ddlTipoAlerta.DataTextField = "Descripcion";
        ddlTipoAlerta.DataBind();
        ddlTipoAlerta.Items.Insert(0, new ListItem("--Todos--", string.Empty));

        hdIdStrainHigh.Value = Morken.SisMon.Entidad.Constante.TipoAlerta_StraingHigh.ToString();
        hdIdStrainLow.Value = Morken.SisMon.Entidad.Constante.TipoAlerta_StraingLow.ToString();
    }
    #endregion

    #region Metodos Publicos

    [WebMethod]
    public static object ListarReporte(int? int_pIdTipoAlerta, int? int_pPosicionInicial, int? int_pPosicionFinal, DateTime? dt_pFechaAlertaInicio, DateTime? dt_pFechaAlertaFin, int? int_pNumPagina, int? int_pTamPagina)
    {
        return Morken.SisMon.Negocio.Alerta.ListarReporteMonitoreo(int_pIdTipoAlerta, int_pPosicionInicial, int_pPosicionFinal, dt_pFechaAlertaInicio, dt_pFechaAlertaFin, int_pNumPagina, int_pTamPagina);
    }

    [WebMethod]
    public static object ListarEventosDiarios(int? int_pIdTipoAlerta, int? int_pPosicionInicial, int? int_pPosicionFinal, DateTime? dt_pFechaAlertaInicio, DateTime? dt_pFechaAlertaFin)
    {
        return Morken.SisMon.Negocio.Alerta.ListarEventosDiarios(int_pIdTipoAlerta, int_pPosicionInicial, int_pPosicionFinal, dt_pFechaAlertaInicio, dt_pFechaAlertaFin);
    }

    #endregion

}