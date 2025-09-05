using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modulo : System.Web.UI.Page
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
        Master_Externo obj_MasterPage = (Master_Externo)this.Master;
        obj_MasterPage.TituloPagina = "Módulos";
        if (!IsPostBack)
            ValidarModulo();

    }

    private void ValidarModulo()
    {
        Kruma.Core.Security.Entity.Usuario obj_Usuario = Kruma.Core.Security.SecurityManager.Usuario;
        if (obj_Usuario != null)
        {
            string str_Url = HttpContext.Current.Request.QueryString["ReturnUrl"];
            int int_Cantidad = 0;
            if (string.IsNullOrEmpty(str_Url))
                int_Cantidad = Kruma.Core.Security.SecurityManager.Usuario.Modulos.Count;
            else
                int_Cantidad = Kruma.Core.Security.Logical.Modulo.ListarPorPagina(str_Url).Count;

            if (int_Cantidad == 1)
            {
                StringBuilder stb_Script = new StringBuilder();
                stb_Script.Append(string.Format("window.sessionStorage.IdModulo = '{0}';", obj_Usuario.Modulos[0].IdModulo));
                if (string.IsNullOrEmpty(str_Url))
                {
                    str_Url = "Index.aspx";

                    System.Collections.Generic.List<Kruma.Core.Security.Entity.PerfilUsuario> lst_Perfiles = Kruma.Core.Security.Data.PerfilUsuario.Listar(obj_Usuario.IdUsuario, null, null, Kruma.Core.Business.Entity.Constante.Estado_Activo);
                    Kruma.Core.Business.Entity.Parametro obj_PerfilAdmin = Kruma.Core.Business.Data.Parametro.Obtener(Kruma.Core.Business.Entity.Constante.Parametro.Modulo, Morken.SisMon.Entidad.Constante.Parametro.Perfil_Administrador);
                    Kruma.Core.Business.Entity.Parametro obj_ParametroPaginaAdmin= Kruma.Core.Business.Data.Parametro.Obtener(Kruma.Core.Business.Entity.Constante.Parametro.Modulo, Morken.SisMon.Entidad.Constante.Parametro.Pagina_Administrador);
                    Kruma.Core.Business.Entity.Parametro obj_PerfilOperador = Kruma.Core.Business.Data.Parametro.Obtener(Kruma.Core.Business.Entity.Constante.Parametro.Modulo, Morken.SisMon.Entidad.Constante.Parametro.Perfil_Operador);
                    Kruma.Core.Business.Entity.Parametro obj_ParametroPaginaOperador= Kruma.Core.Business.Data.Parametro.Obtener(Kruma.Core.Business.Entity.Constante.Parametro.Modulo, Morken.SisMon.Entidad.Constante.Parametro.Pagina_Operador);

                    foreach (Kruma.Core.Security.Entity.PerfilUsuario obj_PerfilUsuario in lst_Perfiles)
                    {
                        if (obj_PerfilUsuario.IdPerfil == obj_PerfilOperador.Valor)
                            str_Url = obj_ParametroPaginaOperador.Valor;

                        if (obj_PerfilUsuario.IdPerfil == obj_PerfilAdmin.Valor)
                            str_Url = obj_ParametroPaginaAdmin.Valor;
                    }
                }
                stb_Script.Append(string.Format("document.location.href = '{0}';", str_Url));
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RedireccionarModulo", stb_Script.ToString(), true);
            }
        }
    }
    #endregion

    #region Metodos Publicos

    [WebMethod]
    public static List<Kruma.Core.Security.Entity.Modulo> ListarModulo(string str_pPath)
    {
        List<Kruma.Core.Security.Entity.Modulo> lst_Modulo = null;
        if (string.IsNullOrEmpty(str_pPath))
            lst_Modulo = Kruma.Core.Security.SecurityManager.Usuario.Modulos;
        else
            lst_Modulo = Kruma.Core.Security.Logical.Modulo.ListarPorPagina(str_pPath);
        return lst_Modulo.OrderBy(x => x.Descripcion).ToList();
    }

    [WebMethod]
    public static string ObtenerRutaModulo()
    {
        return Kruma.Core.Business.Logical.Parametro.Obtener(
            Kruma.Core.Business.Entity.Constante.Parametro.Modulo,
            Kruma.Core.Business.Entity.Constante.Parametro.Contenido_Modulo).Valor;
    }

    [WebMethod]
    public static string ObtenerRutaModuloDefecto()
    {
        Page obj_Page = (Page)HttpContext.Current.Handler;
        return string.Format("{0}{1}", obj_Page.ResolveUrl("~"),
         Kruma.Core.Business.Logical.Parametro.Obtener(
         Kruma.Core.Business.Entity.Constante.Parametro.Modulo,
     "IMAGENMENU").Valor);
    }

    #endregion
}