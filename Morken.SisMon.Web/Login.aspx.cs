using Kruma.Core.Security.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    { 

    }

    protected void lbkContrasenia_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Forms/Seguridad/CambiarContrasenia.aspx", true);
    }
    protected void lkbIniciar_Click(object sender, EventArgs e)
    {
        ProcesarLogin();
    }
    private void ProcesarLogin()
    {
        string str_User = txtUsuario.Value.Trim();
        string str_Password = txtPassword.Value;
        Kruma.Core.Security.Entity.SecurityResult obj_SecurityResult = Kruma.Core.Security.SecurityManager.AuthenticateRedirect(str_User, str_Password);
        if (obj_SecurityResult.ValidationResult != ValidationResult.Authenticated)
        {
            divMensajeError.Visible = true;
            divMensajeError.InnerText = obj_SecurityResult.Message;
        }

    }
}