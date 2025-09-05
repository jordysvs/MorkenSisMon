using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_Externo : System.Web.UI.MasterPage
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
            CargaInicial();
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
    }
    protected void lbLogOut_Click(object sender, EventArgs e)
    {
        Kruma.Core.Security.SecurityManager.SignOut();
    }
}
