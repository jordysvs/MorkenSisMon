using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_DefaultLateral : System.Web.UI.MasterPage
{
    #region Propiedades

    public string TituloPagina
    {
        get { return this.lblTitulo.Text; }
        set { this.lblTitulo.Text = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ValidarModulo();
            CargaInicial();
        }
    }
    private void CargaInicial()
    {
        string str_Titulo = string.Empty;
        Kruma.Core.Business.Entity.Parametro obj_Parametro =
            Kruma.Core.Business.Logical.Parametro.Obtener(
            Kruma.Core.Business.Entity.Constante.Parametro.Modulo,
            Kruma.Core.Business.Entity.Constante.Parametro.Sistema_Nombre);

        if (obj_Parametro != null)
            str_Titulo = obj_Parametro.Valor;

        litTituloProyectoAbreviatura.Text = "";
        Title.Text = string.Format("{0} - {1}", str_Titulo, lblTitulo.Text);

        Kruma.Core.Security.Entity.Usuario obj_Usuario = Kruma.Core.Security.SecurityManager.Usuario;
        if (obj_Usuario != null)
        {
            //Información general del usuario
            if (Kruma.Core.Security.SecurityManager.Usuario.Persona != null)
                litUsuario.Text = Kruma.Core.Security.SecurityManager.Usuario.Persona.NombreCompleto;
            litUsuarioPerfil.Text = litUsuario.Text;
            litUsuarioMenu.Text = litUsuario.Text;
            litUsuarioLockScreen.Text = litUsuario.Text;

            //Visualizacion de la Imagen del Perfil
            string str_UrlImagen = string.Format("{0}{1}", Page.ResolveUrl("~"),
                Kruma.Core.Business.Logical.Parametro.Obtener(
                Kruma.Core.Business.Entity.Constante.Parametro.Modulo,
                "IMAGENPERSONA").Valor);

            int int_IdAlmacenPersona = int.Parse(
                Kruma.Core.Business.Logical.Parametro.Obtener(
                Kruma.Core.Business.Entity.Constante.Parametro.Modulo,
                Kruma.Core.Business.Entity.Constante.Parametro.Almacen_Persona).Valor);

            if (obj_Usuario.Persona.Foto != null)
            {
                Kruma.Core.Business.Entity.Parametro obj_ParametroRuta = Kruma.Core.Business.Logical.Parametro.Obtener(
                    Kruma.Core.Business.Entity.Constante.Parametro.Modulo,
                    Kruma.Core.Business.Entity.Constante.Parametro.Contenido_Persona);

                str_UrlImagen = string.Format("{0}{1}{2}{3}",
                    Page.ResolveUrl("~"),
                    obj_ParametroRuta.Valor,
                    obj_Usuario.Persona.Foto.DescripcionFisica,
                    obj_Usuario.Persona.Foto.Extension);
            }

            imgUserImageMenu.ImageUrl = str_UrlImagen;
            imgUserImagePerfil.ImageUrl = str_UrlImagen;
            imgUserImageBar.ImageUrl = str_UrlImagen;
            imgUserLockScreen.ImageUrl = str_UrlImagen;

            //Ultimo Login
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-PE");
            DateTime? dtFechaUltimoLogin = Kruma.Core.Security.SecurityManager.Usuario.FechaUltimoLogin;
            lblUltimoLogin.Text = string.Format("Ultimo inicio de sesión : {0} / {1}",
                dtFechaUltimoLogin.Value.ToLongDateString(),
                dtFechaUltimoLogin.Value.ToLongTimeString());
        }
    }

    private void ValidarModulo()
    {
        StringBuilder stb_Script = new StringBuilder();
        stb_Script.Append("var urlModulo = 'Modulo.aspx';");
        stb_Script.Append("if (window.sessionStorage.IdModulo == undefined) {");
        stb_Script.Append("var str_Url = window.location.href;");
        stb_Script.Append("str_Url = \"/\" + str_Url.replace(/^(?:\\/\\/|[^\\/]+)*\\//, \"\");");
        stb_Script.Append("document.location.href = UrlPath + urlModulo + \"?ReturnUrl=\" + str_Url;");
        stb_Script.Append("}");
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ValidarModulo", stb_Script.ToString(), true);
    }
}
