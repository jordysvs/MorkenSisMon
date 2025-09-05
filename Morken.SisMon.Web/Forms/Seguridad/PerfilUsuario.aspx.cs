using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Seguridad_PerfilUsuario : System.Web.UI.Page
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
        lnkEliminarFoto.ClientIDMode = System.Web.UI.ClientIDMode.Static;
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
        obj_MasterPage.TituloPagina = "Perfil del Usuario";
        hdIdPersona.Value = Kruma.Core.Security.SecurityManager.Usuario.IdPersona.ToString();

        ddlTipoDocumento.DataSource = Kruma.Core.Business.Logical.TipoDocumento.Listar(null, null, Kruma.Core.Business.Entity.Constante.Estado_Activo, null, null).Result;
        ddlTipoDocumento.DataValueField = "IdTipoDocumento";
        ddlTipoDocumento.DataTextField = "Descripcion";
        ddlTipoDocumento.DataBind();
        ddlTipoDocumento.Items.Insert(0, new ListItem("--Seleccione--", string.Empty));

        int int_IdAlmacen = int.Parse(
            Kruma.Core.Business.Logical.Parametro.Obtener(
            Kruma.Core.Business.Entity.Constante.Parametro.Modulo,
            Kruma.Core.Business.Entity.Constante.Parametro.Almacen_Temporal).Valor);

        hdIdAlmacen.Value = int_IdAlmacen.ToString();

        string str_UrlImagen = string.Format("{0}{1}", Page.ResolveUrl("~"),
            Kruma.Core.Business.Logical.Parametro.Obtener(
            Kruma.Core.Business.Entity.Constante.Parametro.Modulo,
            "IMAGENPERSONA").Valor);

        hdImagenPersona.Value = str_UrlImagen;

        int int_IdAlmacenPersona = int.Parse(
            Kruma.Core.Business.Logical.Parametro.Obtener(
            Kruma.Core.Business.Entity.Constante.Parametro.Modulo,
            Kruma.Core.Business.Entity.Constante.Parametro.Almacen_Persona).Valor);

        Kruma.Core.FileServer.FileServerManager obj_FileServerManager = new Kruma.Core.FileServer.FileServerManager();
        List<Kruma.Core.FileServer.Entity.FileUpload> lst_Fotos = obj_FileServerManager.getFiles(int_IdAlmacenPersona, Kruma.Core.Security.SecurityManager.Usuario.IdPersona.Value);
        lnkEliminarFoto.Style["display"] = (lst_Fotos.Count > 0) ? "" : "none";

        if (lst_Fotos.Count > 0)
        {
            str_UrlImagen = string.Format("~/Ashx/FileServerViewHandler.ashx?ref={0}",
                new Kruma.Core.Criptography.CriptographyManager().Encrypt(
                string.Format("{0}|{1}|{2}", lst_Fotos[0].IdAlmacen, lst_Fotos[0].IdRegistro, lst_Fotos[0].IdDocumento)
                ));
        }

        imgPerfilUsuarioImagen.ImageUrl = str_UrlImagen;
    }

    #endregion

    #region Metodos Publicos

    /// <summary>
    /// Obtiene los datos de la persona
    /// </summary>
    /// <param name="int_pIdPersona">Id de la persona</param>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>John Castillo</CreadoPor></item>
    /// <item><FecCrea>09/08/2015</FecCrea></item></list></remarks>
    [WebMethod]
    public static Kruma.Core.Business.Entity.Persona ObtenerPersona(int int_pIdPersona)
    {
        return Kruma.Core.Business.Logical.Persona.Obtener(int_pIdPersona);
    }

    ///// <summary>
    ///// Obtiene los datos del empleado
    ///// </summary>
    ///// <param name="int_pIdEmpleado">Id de la persona</param>
    ///// <remarks><list type="bullet">
    ///// <item><CreadoPor>John Castillo</CreadoPor></item>
    ///// <item><FecCrea>09/08/2015</FecCrea></item></list></remarks>
    //[WebMethod]
    //public static Kruma.Core.Business.Entity.Empleado ObtenerEmpleado(int int_pIdPersona)
    //{
    //    List<Kruma.Core.Business.Entity.Empleado> lst_Empleado = Kruma.Core.Business.Logical.Empleado.Listar(int_pIdPersona, null, null, null, null, null, Kruma.Core.Business.Entity.Constante.Estado_Activo, null, null).Result;
    //    if (lst_Empleado.Count > 0)
    //        return lst_Empleado[0];
    //    else return null;
    //}

    /// <summary>
    /// Guarda la informacion de la persona
    /// </summary>
    /// <param name="str_pPersona">Json Persona</param>
    /// <returns>Resultado del proceso</returns>
    /// <remarks><list type="bullet">
    /// <item><CreadoPor>Creado por John Castillo</CreadoPor></item>
    /// <item><FecCrea>09/08/2015</FecCrea></item></list></remarks>
    [WebMethod]
    public static Kruma.Core.Util.Common.ProcessResult GuardarPersona(string str_pPersona)
    {
        JavaScriptSerializer obj_JsSerializer = new JavaScriptSerializer();
        Kruma.Core.Business.Entity.Persona obj_Persona = obj_JsSerializer.Deserialize<Kruma.Core.Business.Entity.Persona>(str_pPersona);

        Kruma.Core.Business.Entity.Persona obj_PersonaAModificar = Kruma.Core.Business.Logical.Persona.Obtener(obj_Persona.IdPersona);
        obj_PersonaAModificar.Nombres = obj_Persona.Nombres;
        obj_PersonaAModificar.ApellidoPaterno = obj_Persona.ApellidoPaterno;
        obj_PersonaAModificar.ApellidoMaterno = obj_Persona.ApellidoMaterno;
        obj_PersonaAModificar.IdTipoDocumento = obj_Persona.IdTipoDocumento;
        obj_PersonaAModificar.NumeroDocumento = obj_Persona.NumeroDocumento;
        obj_PersonaAModificar.Direcciones = obj_Persona.Direcciones;
        obj_PersonaAModificar.Telefonos = obj_Persona.Telefonos;
        obj_PersonaAModificar.Mails = obj_Persona.Mails;
        obj_PersonaAModificar.Foto = obj_Persona.Foto;
        obj_PersonaAModificar.UsuarioCreacion = Kruma.Core.Security.SecurityManager.Usuario.IdUsuario;
        obj_PersonaAModificar.UsuarioModificacion = obj_PersonaAModificar.UsuarioCreacion;

        return Kruma.Core.Business.Logical.Persona.Modificar(obj_PersonaAModificar);
    }


    #endregion
}