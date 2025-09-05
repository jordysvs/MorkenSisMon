using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Error : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        string str_Titulo = "Error en el sistema";
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
        CargaInicial();
    }

    private void CargaInicial()
    {
        string str_Titulo = "Kruma";
        Kruma.Core.Business.Entity.Parametro obj_Parametro =
            Kruma.Core.Business.Logical.Parametro.Obtener(
            Kruma.Core.Business.Entity.Constante.Parametro.Modulo,
            Kruma.Core.Business.Entity.Constante.Parametro.Sistema_Nombre);

        if (obj_Parametro != null)
            str_Titulo = obj_Parametro.Valor;

        Exception obj_Excepcion = Server.GetLastError();
        RegistrarError(str_Titulo, obj_Excepcion);
        RegistrarLog(str_Titulo, obj_Excepcion);

        Server.ClearError();
    }

    private void RegistrarError(string str_pTitulo, Exception obj_pExcepcion)
    {
        string str_Error = "Se produjo un error en el sistema.";

        if (obj_pExcepcion != null)
        {
            if (obj_pExcepcion.InnerException != null)
            {
                lblInnerTrace.Text = obj_pExcepcion.InnerException.StackTrace;
                pnlInnerErrorPanel.Visible = Request.IsLocal;
                lblInnerMessage.Text = obj_pExcepcion.InnerException.Message;
            }
        }
        else
            obj_pExcepcion = new Exception(str_Error, obj_pExcepcion);

        if (Request.IsLocal)
            pnlExErrorPanel.Visible = true;

        lblExMessage.Text = obj_pExcepcion.Message;
        lblExTrace.Text = obj_pExcepcion.StackTrace;
    }

    private void RegistrarLog(string str_pTitulo, Exception obj_pExcepcion)
    {
        //Log
        if (obj_pExcepcion != null)
        {
            Kruma.Core.Log.LogManager.Write("Exceptions", string.Format("{0} - Message", str_pTitulo), obj_pExcepcion.Message);
            Kruma.Core.Log.LogManager.Write("Exceptions", string.Format("{0} - StackTrace", str_pTitulo), obj_pExcepcion.StackTrace);
            if (obj_pExcepcion.InnerException != null)
            {
                Kruma.Core.Log.LogManager.Write("Exceptions", string.Format("{0} - InnerException-Message", str_pTitulo), obj_pExcepcion.InnerException.Message);
                Kruma.Core.Log.LogManager.Write("Exceptions", string.Format("{0} - InnerException-StackTrace", str_pTitulo), obj_pExcepcion.InnerException.StackTrace);
            }
        }

    }
}