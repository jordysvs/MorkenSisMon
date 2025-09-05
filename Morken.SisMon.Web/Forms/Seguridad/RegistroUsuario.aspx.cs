using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Seguridad_RegistroUsuario : System.Web.UI.Page
{
    #region Eventos

    /// <summary>
    /// Evento de carga de la pagina
    /// </summary>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>John Castillo</CreadoPor></item>
    /// <item><FecCrea>15/12/2014</FecCrea></item></list></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargaInicial();
        }
    }

    #endregion

    #region Metodos Privados

    /// <summary>
    /// Carga Inicial del formulario
    /// </summary>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>John Castillo</CreadoPor></item>
    /// <item><FecCrea>15/12/2014</FecCrea></item></list></remarks>
    private void CargaInicial()
    {
        //Titulo
        Master_Default obj_MasterPage = (Master_Default)this.Master;
        if (Request.QueryString["id"] == null)
        {
            obj_MasterPage.TituloPagina = "Agregar Usuario";
        }
        else
        {
            obj_MasterPage.TituloPagina = "Modificar Usuario";
            hdIdUsuario.Value = Request.QueryString["id"];
        }

        ddlEstado.DataSource = Kruma.Core.Util.CommonUtil.ListarEstado();
        ddlEstado.DataValueField = "Code";
        ddlEstado.DataTextField = "Description";
        ddlEstado.DataBind();

        //Buscador Empleado
        ddlTipoDocumentoBuscadorPersona.DataSource = Kruma.Core.Business.Logical.TipoDocumento.Listar(null, null, Kruma.Core.Business.Entity.Constante.Estado_Activo, null, null).Result;
        ddlTipoDocumentoBuscadorPersona.DataValueField = "IdTipoDocumento";
        ddlTipoDocumentoBuscadorPersona.DataTextField = "Descripcion";
        ddlTipoDocumentoBuscadorPersona.DataBind();
        ddlTipoDocumentoBuscadorPersona.Items.Insert(0, new ListItem("--Seleccione--", string.Empty));

    }

    #endregion

    #region Metodos Publicos

    /// <summary>
    /// Obtiene los datos del usuario
    /// </summary>
    /// <param name="str_pIdUsuario">Id del usuario</param>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>John Castillo</CreadoPor></item>
    /// <item><FecCrea>16/12/2014</FecCrea></item></list></remarks>
    [WebMethod]
    public static Kruma.Core.Security.Entity.Usuario ObtenerUsuario(string str_pIdUsuario)
    {
        return Kruma.Core.Security.Logical.Usuario.Obtener(str_pIdUsuario, null);
    }

    /// <summary>
    /// Obtiene los perfiles del usuario
    /// </summary>
    /// <param name="str_pIdPerfil">Id del perfil</param>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>John Castillo</CreadoPor></item>
    /// <item><FecCrea>31/07/2015</FecCrea></item></list></remarks>
    [WebMethod]
    public static List<Kruma.Core.Security.Entity.PerfilUsuario> ListarPerfil(string str_pIdUsuario)
    {
        string str_Sistema = Kruma.Core.Security.SecurityManager.Usuario.Sistema;
        if (str_Sistema == Kruma.Core.Security.Entity.Constante.Condicion_Positivo)
            str_Sistema = string.Empty;

        string str_IdModulo = null;
        foreach (Kruma.Core.Business.Entity.Modulo obj_Modulo in Kruma.Core.Security.SecurityManager.Usuario.ModulosPagina)
            str_IdModulo = string.Format("{0}{1},", str_IdModulo, obj_Modulo.IdModulo);
        if (str_IdModulo.Length > 0)
            str_IdModulo = str_IdModulo.Remove(str_IdModulo.Length - 1, 1);

        List<Kruma.Core.Security.Entity.Perfil> lst_Perfil = Kruma.Core.Security.Logical.Perfil.Listar(str_IdModulo, null, null, str_Sistema, Kruma.Core.Security.Entity.Constante.Estado_Activo, null, null).Result;
        List<Kruma.Core.Security.Entity.PerfilUsuario> lst_PerfilUsuario = Kruma.Core.Security.Logical.PerfilUsuario.Listar(str_pIdUsuario, null, null, Kruma.Core.Security.Entity.Constante.Estado_Activo);

        List<Kruma.Core.Security.Entity.PerfilUsuario> lst_Resultado = new List<Kruma.Core.Security.Entity.PerfilUsuario>();
        Kruma.Core.Security.Entity.PerfilUsuario obj_PerfilUsuarioResultado = null;

        foreach (Kruma.Core.Security.Entity.Perfil obj_Perfil in lst_Perfil)
        {
            obj_PerfilUsuarioResultado = (from obj_PerfilUsuario in lst_PerfilUsuario
                                          where
                                          obj_PerfilUsuario.IdModulo == obj_Perfil.IdModulo &&
                                          obj_PerfilUsuario.IdPerfil == obj_Perfil.IdPerfil
                                          select obj_PerfilUsuario).FirstOrDefault();

            if (obj_PerfilUsuarioResultado == null)
            {
                obj_PerfilUsuarioResultado = new Kruma.Core.Security.Entity.PerfilUsuario();
                obj_PerfilUsuarioResultado.IdUsuario = str_pIdUsuario;
                obj_PerfilUsuarioResultado.IdPerfil = obj_Perfil.IdPerfil;
                obj_PerfilUsuarioResultado.FlagExpiracion = Kruma.Core.Security.Entity.Constante.Condicion_Negativo;
                obj_PerfilUsuarioResultado.FechaExpiracion = null;
                obj_PerfilUsuarioResultado.Estado = Kruma.Core.Security.Entity.Constante.Estado_Activo;
            }
            else
                obj_PerfilUsuarioResultado.Seleccion = true;

            obj_PerfilUsuarioResultado.Perfil = obj_Perfil;
            lst_Resultado.Add(obj_PerfilUsuarioResultado);
        }
        return lst_Resultado.OrderBy(x => x.Perfil.Modulo.Descripcion).ThenBy(x => x.Perfil.Descripcion).ToList();
    }

    /// <summary>
    /// Guarda la informacion del usuario
    /// </summary>
    /// <param name="str_pUsuario">Json Usuario</param>
    /// <returns>Resultado del proceso</returns>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>Creado por John Castillo</CreadoPor></item>
    /// <item><FecCrea>16/12/2014</FecCrea></item></list></remarks>
    [WebMethod]
    public static Kruma.Core.Util.Common.ProcessResult GuardarUsuario(string str_pUsuario, string str_pIdUsuario)
    {
        JavaScriptSerializer obj_JsSerializer = new JavaScriptSerializer();
        Kruma.Core.Security.Entity.Usuario obj_Usuario = obj_JsSerializer.Deserialize<Kruma.Core.Security.Entity.Usuario>(str_pUsuario);

        //Auditoria del usuario
        obj_Usuario.UsuarioCreacion = Kruma.Core.Security.SecurityManager.Usuario.IdUsuario;
        obj_Usuario.UsuarioModificacion = obj_Usuario.UsuarioCreacion;

        //Auditoria de los perfiles del usuario
        foreach (Kruma.Core.Security.Entity.PerfilUsuario obj_PerfilUsuario in obj_Usuario.Perfiles)
        {
            obj_PerfilUsuario.Estado = Kruma.Core.Security.Entity.Constante.Estado_Activo;
            obj_PerfilUsuario.UsuarioCreacion = Kruma.Core.Security.SecurityManager.Usuario.IdUsuario;
            obj_PerfilUsuario.UsuarioModificacion = obj_Usuario.UsuarioCreacion;
        }

        Kruma.Core.Util.Common.ProcessResult obj_Resultado = null;
        if (string.IsNullOrEmpty(str_pIdUsuario))
            obj_Resultado = Kruma.Core.Security.Logical.Usuario.Insertar(obj_Usuario);
        else
            obj_Resultado = Kruma.Core.Security.Logical.Usuario.Modificar(obj_Usuario);
        return obj_Resultado;
    }

    /// <summary>
    /// Reiniciar contransenia
    /// </summary>
    /// <param name="str_pIdUsuario">Id del usuario</param>
    /// <returns>Resultado del proceso</returns>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>Creado por John Castillo</CreadoPor></item>
    /// <item><FecCrea>06/08/2015</FecCrea></item></list></remarks>
    [WebMethod]
    public static Kruma.Core.Util.Common.ProcessResult ReiniciarContrasenia(string str_pIdUsuario)
    {
        return Kruma.Core.Security.Logical.Usuario.GenerarContrasenia(str_pIdUsuario, Kruma.Core.Security.SecurityManager.Usuario.IdUsuario);
    }

    #endregion
}