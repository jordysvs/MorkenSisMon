using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Gestion_MonitoreoOperador : System.Web.UI.Page
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
        this.Title = "Monitoreo";
        int? int_IdPersona = Kruma.Core.Security.SecurityManager.Usuario.IdPersona;
        if (int_IdPersona.HasValue)
        {
            System.Collections.Generic.List<Morken.SisMon.Entidad.Operador> lst_Operadores = Morken.SisMon.Data.Operador.Listar(null, int_IdPersona, null, null, null, null, null).Result;
            if(lst_Operadores.Count > 0)
                hdIdOperador.Value = lst_Operadores[0].IdOperador.ToString();
        }

        ddlTipoAlerta.DataSource = Morken.SisMon.Negocio.TipoAlerta.Listar(null, null, Morken.SisMon.Entidad.Constante.Estado_Activo, null, null).Result;
        ddlTipoAlerta.DataValueField = "IdTipoAlerta";
        ddlTipoAlerta.DataTextField = "Descripcion";
        ddlTipoAlerta.DataBind();
        ddlTipoAlerta.Items.Insert(0, new ListItem("--Todos--", string.Empty));

        hdTiempoAlerta.Value = Kruma.Core.Business.Data.Parametro.Obtener
          (Morken.SisMon.Entidad.Constante.Parametro.Modulo,
          Morken.SisMon.Entidad.Constante.Parametro.Tiempo_Alerta).Valor;

    }
    #endregion

    #region Metodos Publicos

    [WebMethod]
    public static object CargarAlertas(int? int_pIdOperador)
    {
        return Morken.SisMon.Negocio.Alerta.ListarAlertasOperador(int_pIdOperador);
    }

    [WebMethod]
    public static object ListarAlertas(int? int_pIdOperador, int? int_pIdTipoAlerta, DateTime? dt_pHoraInicial, DateTime? dt_pHoraFinal)
    {
        return Morken.SisMon.Negocio.Alerta.Listar
            (null, null, null,null, int_pIdTipoAlerta, null,null, dt_pHoraInicial, dt_pHoraFinal, null, null, null, int_pIdOperador, null, null, null, Morken.SisMon.Entidad.Constante.Estado_Activo, null, null);
    }

    [WebMethod]
    public static object ObtenerAlerta(int? int_pIdAlerta)
    {
        return Morken.SisMon.Negocio.Alerta.Obtener(int_pIdAlerta.Value);
    }


    //[WebMethod]
    //public static object ObtenerEstado()
    //{
    //    return Morken.SisMon.Negocio.Alerta.obj_gSemaforo;
    //}


    [WebMethod]
    public static object InformarAlerta(int? int_pIdAlerta, string str_pObservacion)
    {
        Morken.SisMon.Entidad.Alerta obj_AlertaAInformar = Morken.SisMon.Data.Alerta.Obtener(int_pIdAlerta.Value);
        obj_AlertaAInformar.Observacion = str_pObservacion;
        obj_AlertaAInformar.FechaInforme = DateTime.Now;
        obj_AlertaAInformar.UsuarioModificacion = Kruma.Core.Security.SecurityManager.Usuario.IdUsuario;
        return Morken.SisMon.Negocio.Alerta.Modificar(obj_AlertaAInformar);
    }

    //[WebMethod]
    //public static object Notificar()
    //{
    //    return Morken.SisMon.Negocio.Monitoreo.Notificar();
    //}

    //[WebMethod]
    //public static object MostrarRutaXML()
    //{
    //    return Morken.SisMon.Negocio.Alerta.LeerRutaXML();
    //}


    #endregion
}