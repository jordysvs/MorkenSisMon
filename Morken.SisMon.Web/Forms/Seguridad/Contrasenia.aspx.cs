using Kruma.Core.Util.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Seguridad_Contrasenia : System.Web.UI.Page
{
    #region Eventos

    protected void Page_PreInit(object sender, EventArgs e)
    {
        string str_Titulo = "Contraseña";
        if (Kruma.Core.Security.SecurityManager.Usuario != null)
        {
            this.MasterPageFile = "~/Master/Default.master";
            Master_Default obj_MasterPage = (Master_Default)this.Master;
            obj_MasterPage.TituloPagina = str_Titulo;
        }
        else
        {
            this.MasterPageFile = "~/Master/Externo.master";
            Master_Externo obj_MasterPage = (Master_Externo)this.Master;
            obj_MasterPage.TituloPagina = str_Titulo;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargaInicial();
        }
    }

    [WebMethod]
    public static ProcessResult CambiarContrasenia(string str_pIdUsuario, string str_pContrasenia)
    {
        Kruma.Core.Security.Entity.Usuario obj_Usuario = Kruma.Core.Security.Logical.Usuario.Obtener(str_pIdUsuario, null);
        obj_Usuario.Clave = str_pContrasenia;
        obj_Usuario.UsuarioCreacion = obj_Usuario.IdUsuario;
        obj_Usuario.UsuarioModificacion = obj_Usuario.UsuarioCreacion;
        return Kruma.Core.Security.Logical.Usuario.ModificarClave(obj_Usuario);
    }

    #endregion

    #region Metodos Publicos

    private void CargaInicial()
    {
        pnlContrasenia.Visible = false;
        if (Request.QueryString.Count <= 0)
        {
            lblMensaje.Text = "Se requiere del envio de un correo electrónico para el cambio de la contraseña.";
            divMensaje.Visible = true;
            return;
        }
        else
        {
            string str_IdUsuario = HttpUtility.UrlDecode(Request.QueryString["ref"]).Replace(" ", "+");
            str_IdUsuario = new Kruma.Core.Criptography.CriptographyManager().Dencrypt(str_IdUsuario);
            Kruma.Core.Util.Common.ProcessResult obj_ProcessResult = Kruma.Core.Security.Logical.Usuario.ValidarOlvidoContrasenia(str_IdUsuario);
            if (obj_ProcessResult.OperationResult != Kruma.Core.Util.Common.Enum.OperationResult.Success)
            {
                lblMensaje.Text = obj_ProcessResult.Detail;
                divMensaje.Visible = true;
            }
            else
            {
                pnlContrasenia.Visible = true;
                hdIdUsuario.Value = str_IdUsuario;
            }
        }
    }

    #endregion

}