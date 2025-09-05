using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Gestion_Monitoreo : System.Web.UI.Page
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
        ddlTipoAlerta.DataSource =  Morken.SisMon.Negocio.TipoAlerta.Listar(null, null, Morken.SisMon.Entidad.Constante.Estado_Activo, null, null).Result;
        ddlTipoAlerta.DataValueField = "IdTipoAlerta";
        ddlTipoAlerta.DataTextField = "Descripcion";
        ddlTipoAlerta.DataBind();
        ddlTipoAlerta.Items.Insert(0, new ListItem("--Todos--", string.Empty));

        hdTiempoAlerta.Value = Kruma.Core.Business.Data.Parametro.Obtener
            (Morken.SisMon.Entidad.Constante.Parametro.Modulo, 
            Morken.SisMon.Entidad.Constante.Parametro.Tiempo_Alerta).Valor;

        string str_RangoDias = Kruma.Core.Business.Data.Parametro.Obtener
          (Morken.SisMon.Entidad.Constante.Parametro.Modulo,
          Morken.SisMon.Entidad.Constante.Parametro.Rango_Dias).Valor;

        str_RangoDias = "-" + str_RangoDias;
        hdRangoDias.Value = str_RangoDias;
    }
    #endregion

    #region Metodos Publicos

    /// <summary>
    /// Lista los pacientes
    /// </summary>
    /// <param name="str_pNombreCliente">Nombre de Cliente</param>
    /// <returns>Notificar</returns>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>Diego Mendoza Villarreyes</CreadoPor></item>
    /// <item><FecCrea>26-09-2017</FecCrea></item></list></remarks>
    //[WebMethod]
    //public static object Notificar()
    //{
    //    return Morken.SisMon.Negocio.Monitoreo.Notificar();
    //}

    [WebMethod]
    public static object CargarAlertas(int int_pCantidadDias)
    {
        return Morken.SisMon.Negocio.Alerta.LeerRutaXML(int_pCantidadDias);
    }

    [WebMethod]
    public static object ObtenerEstado()
    {
        return Kruma.Core.Business.Data.Parametro.Obtener(Kruma.Core.Business.Entity.Constante.Parametro.Modulo, Morken.SisMon.Entidad.Constante.Parametro.Estado_Sistema);
    }

    [WebMethod]
    public static object ObtenerEstado2()
    {
        return Kruma.Core.Business.Data.Parametro.Obtener(Kruma.Core.Business.Entity.Constante.Parametro.Modulo, Morken.SisMon.Entidad.Constante.Parametro.Estado_Sistema);
    }

    [WebMethod]
    public static object ListarAlertas(int int_pCantidadDias, int? int_pIdTipoAlerta, DateTime? dt_pFechaInicial, DateTime? dt_pFechaFinal)
    {
        //return Morken.SisMon.Negocio.Alerta.Listar
        //    (null, null, null, null, int_pIdTipoAlerta, null, DateTime.Now, dt_pHoraInicial, dt_pHoraFinal, null, null, null, null, null, null, null, Morken.SisMon.Entidad.Constante.Estado_Activo, null, null);
        return Morken.SisMon.Negocio.Alerta.ListarAlertasMapa(int_pCantidadDias, int_pIdTipoAlerta, dt_pFechaInicial, dt_pFechaFinal);
    }

    [WebMethod]
    public static object ListarOperador(string str_pNombreCompleto,  int? int_pNumPagina, int? int_pTamPagina)
    {
        return Morken.SisMon.Negocio.Operador.Listar
            (null, null, null, str_pNombreCompleto, Morken.SisMon.Entidad.Constante.Estado_Activo, int_pNumPagina, int_pTamPagina);
    }

    [WebMethod]
    public static object AsignarAlerta(int? int_pIdAlerta, int? int_pIdOperador)
    {
        Morken.SisMon.Entidad.Alerta obj_AlertaAsignada = Morken.SisMon.Data.Alerta.Obtener(int_pIdAlerta.Value);
        obj_AlertaAsignada.IdUsuario = Kruma.Core.Security.SecurityManager.Usuario.IdUsuario;
        obj_AlertaAsignada.IdOperador = int_pIdOperador;
        obj_AlertaAsignada.FechaAsignacion = DateTime.Now;
        obj_AlertaAsignada.UsuarioModificacion = obj_AlertaAsignada.IdUsuario;
        return Morken.SisMon.Negocio.Alerta.Modificar(obj_AlertaAsignada);
    }

    [WebMethod]
    public static object MitigarAlerta(int? int_pIdAlerta)
    {
        //string str_Match = "\"__type\":\"([^\\\"]|\\.)*\",";
        //System.Text.RegularExpressions.Regex obj_Regex = new System.Text.RegularExpressions.Regex(str_Match, System.Text.RegularExpressions.RegexOptions.Singleline);
        //str_pAlerta = obj_Regex.Replace(str_pAlerta, "");
        //JavaScriptSerializer obj_JsSerializer = new JavaScriptSerializer();
        //Morken.SisMon.Entidad.Alerta obj_Alerta = obj_JsSerializer.Deserialize<Morken.SisMon.Entidad.Alerta>(str_pAlerta);
        Morken.SisMon.Entidad.Alerta obj_AlertaAMitigar= Morken.SisMon.Data.Alerta.Obtener(int_pIdAlerta.Value);
        obj_AlertaAMitigar.FechaMitigacion = DateTime.Now;
        obj_AlertaAMitigar.UsuarioModificacion = Kruma.Core.Security.SecurityManager.Usuario.IdUsuario;
        return Morken.SisMon.Negocio.Alerta.Modificar(obj_AlertaAMitigar);
    }

    [WebMethod]
    public static object ObtenerAlerta(int? int_pIdAlerta)
    {
        return Morken.SisMon.Negocio.Alerta.Obtener(int_pIdAlerta.Value);
    }


    #endregion
}