using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Redireccionar : System.Web.UI.Page
{
    #region Eventos
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string str_Url = FormsAuthentication.LoginUrl;
            if (HttpContext.Current.Request.QueryString.Count > 0)
            {
                if (HttpContext.Current.Request.QueryString["ref"] != null)
                {
                    str_Url = new Kruma.Core.Criptography.CriptographyManager().Dencrypt(HttpUtility.UrlDecode(Request.QueryString["ref"]).Replace(" ", "+"));
                    str_Url = Server.UrlDecode(str_Url);
                }
            }
            Response.Redirect(str_Url);
        }
    }

    #endregion

    #region Metodos Publicos

    #region Menu

    [WebMethod]
    public static List<Kruma.Core.Security.Entity.Grupo> ListarGrupo(string str_pIdModulo)
    {
        List<Kruma.Core.Security.Entity.Grupo> lst_Grupo = null;
        if (Kruma.Core.Security.SecurityManager.Usuario != null)
        {
            Kruma.Core.Security.Entity.Modulo obj_Modulo = (from obj_ModuloItem in Kruma.Core.Security.SecurityManager.Usuario.Modulos
                                                            where obj_ModuloItem.IdModulo == str_pIdModulo
                                                            select obj_ModuloItem).FirstOrDefault();

            if (obj_Modulo != null)
                lst_Grupo = obj_Modulo.Grupos.OrderBy(x => x.Orden).ThenBy(x => x.Orden).ToList();
        }
        return lst_Grupo;
    }

    [WebMethod]
    public static string ObtenerRutaMenu()
    {
        return Kruma.Core.Business.Logical.Parametro.Obtener(
            Kruma.Core.Business.Entity.Constante.Parametro.Modulo,
            Kruma.Core.Business.Entity.Constante.Parametro.Contenido_Menu).Valor;
    }

    [WebMethod]
    public static List<Kruma.Core.Security.Entity.Modulo> ListarModuloPagina(
        string str_pIdModulo,
        string str_pPath)
    {
        List<Kruma.Core.Security.Entity.Modulo> lst_Modulo = Kruma.Core.Security.Logical.Modulo.ListarPorPagina(str_pPath);
        if (str_pIdModulo.TrimEnd() != Kruma.Core.Business.Entity.Constante.Parametro.Modulo)
            lst_Modulo = (from obj_Modulo in lst_Modulo
                          where obj_Modulo.IdModulo == str_pIdModulo
                          select obj_Modulo).ToList();
        return lst_Modulo.OrderBy(x => x.Descripcion).ToList();
    }

    [WebMethod]
    public static Kruma.Core.Security.Entity.GrupoDetalle ObtenerPagina(
        string str_pIdModulo,
        string str_pPath)
    {
        return Kruma.Core.Security.Logical.Modulo.ObtenerPagina(str_pIdModulo, str_pPath);
    }

    #endregion

    #region MasterDefault

    [WebMethod]
    public static Kruma.Core.Util.Common.ProcessResult LockScreen()
    {
        try
        {
            string str_IdUsuario = Kruma.Core.Security.SecurityManager.Usuario.IdUsuario;
            Kruma.Core.Security.SecurityManager.Lock();
            return new Kruma.Core.Util.Common.ProcessResult(str_IdUsuario) { Message = string.Empty };
        }
        catch (Exception ex)
        {
            return new Kruma.Core.Util.Common.ProcessResult(ex);
        }

    }

    [WebMethod]
    public static Kruma.Core.Util.Common.ProcessResult SignOut()
    {
        Kruma.Core.Security.SecurityManager.SignOut();
        return new Kruma.Core.Util.Common.ProcessResult(string.Empty) { Message = string.Empty };
    }

    [WebMethod]
    public static Kruma.Core.Util.Common.ProcessResult Login(string str_pUsuario, string str_pPassword)
    {
        string str_Usuario = str_pUsuario.Trim();
        Kruma.Core.Security.Entity.SecurityResult obj_SecurityResult = Kruma.Core.Security.SecurityManager.Authenticate(str_Usuario, str_pPassword);
        if (obj_SecurityResult.ValidationResult != Kruma.Core.Security.Enum.ValidationResult.Authenticated)
            return new Kruma.Core.Util.Common.ProcessResult(new Exception(obj_SecurityResult.Message), obj_SecurityResult.Message);
        else
            return new Kruma.Core.Util.Common.ProcessResult(str_Usuario) { Message = string.Empty };
    }

    [WebMethod]
    public static Kruma.Core.Util.Common.ProcessResult Timeout()
    {
        Kruma.Core.Util.Common.ProcessResult obj_Resultado = new Kruma.Core.Util.Common.ProcessResult(HttpContext.Current.Session.Timeout.ToString());
        obj_Resultado.Message = string.Empty;
        obj_Resultado.Detail = string.Empty;
        return obj_Resultado;
    }
    #endregion

    #region Buscadores

    [WebMethod]
    public static object ListarPersona(
         string str_pNombreCompleto, int? int_pIdTipoDocumento, string str_pNumeroDocumento, int? int_pNumPagina, int? int_pTamPagina)
    {
        string str_Sistema = Kruma.Core.Security.SecurityManager.Usuario.Sistema;
        if (str_Sistema == Kruma.Core.Security.Entity.Constante.Condicion_Positivo)
            str_Sistema = string.Empty;
        return Kruma.Core.Business.Logical.Persona.Listar(str_pNombreCompleto, int_pIdTipoDocumento, null, str_pNumeroDocumento, str_Sistema, Kruma.Core.Business.Entity.Constante.Estado_Activo, int_pNumPagina, int_pTamPagina);
    }
   
    #endregion

    #endregion
}