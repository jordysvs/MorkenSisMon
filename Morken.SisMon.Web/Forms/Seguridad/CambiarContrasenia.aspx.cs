using Kruma.Core.Util.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Seguridad_CambiarContrasenia : System.Web.UI.Page
{
    #region Eventos

    protected void Page_PreInit(object sender, EventArgs e)
    {
        string str_Titulo = "Cambiar Contraseña";
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

    [WebMethod]
    public static ProcessResult ValidarContraseniaUsuario(string str_pCodigo, string str_pCorreo)
    {
        return Kruma.Core.Security.Logical.Usuario.ValidarUsuarioContrasenia(str_pCodigo, str_pCorreo, string.Empty);
    }

    #endregion

}