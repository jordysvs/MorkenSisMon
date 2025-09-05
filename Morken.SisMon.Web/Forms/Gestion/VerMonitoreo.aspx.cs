using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Gestion_VerMonitoreo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            CargaInicial();
    }

    #region Metodos Privados

    /// <summary>
    /// Carga inicial del formulario
    /// </summary>
    private void CargaInicial()
    {
        this.Title = "Ver Monitoreo";
        hdIdAlerta.Value = Request.QueryString["id"];
    }
    #endregion

    #region Metodos Publicos

    [WebMethod]
    public static object ObtenerEstado()
    {
        return Kruma.Core.Business.Data.Parametro.Obtener(Kruma.Core.Business.Entity.Constante.Parametro.Modulo, Morken.SisMon.Entidad.Constante.Parametro.Estado_Sistema);
    }

    [WebMethod]
    public static object ObtenerAlerta(int? int_pIdAlerta)
    {
        System.Collections.Generic.List<Morken.SisMon.Entidad.Alerta> lst_Alertas = Morken.SisMon.Data.Alerta.Listar(int_pIdAlerta, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Result;
        lst_Alertas = Morken.SisMon.Negocio.Alerta.CalcularCoordenadas(lst_Alertas);
        if (lst_Alertas.Count > 0)
            return lst_Alertas[0];
        else
            return null;
    }


    #endregion
}