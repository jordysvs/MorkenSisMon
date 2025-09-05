using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index : System.Web.UI.Page
{
    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        CargaInicial();
    }

    #endregion

    #region Metodos Publicos

    //[WebMethod]
    //public static Ugel.TraDoc.Entidad.DashBoard ObtenerDashBoard()
    //{
    //    return Ugel.TraDoc.Negocio.DashBoard.ListarDashBoard(Kruma.Core.Security.SecurityManager.Usuario.IdEmpleado);
    //}

    private void CargaInicial()
    {
        Master_Default obj_MasterPage = (Master_Default)this.Master;
        obj_MasterPage.TituloPagina = "Inicio";


    }
    #endregion
}