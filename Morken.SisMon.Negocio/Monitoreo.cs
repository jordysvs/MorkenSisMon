using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kruma.Core.Util.Common;
using System.Transactions;

namespace Morken.SisMon.Negocio
{
    public class Monitoreo
    {
        /// <summary>Notificar Monitoreo</summary>
        /// <param name="obj_pPago">Pago</param>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Diego Mendoza Villarreyes</CreadoPor></item>
        /// <item><FecCrea>26-09-2017</FecCrea></item></list></remarks>
        public static Kruma.Core.Util.Common.ProcessResult Notificar(System.Collections.Generic.List<Morken.SisMon.Entidad.Alerta> lst_pAlertas)
        {
            Kruma.Core.Util.Common.ProcessResult obj_Resultado = null;
            try
            {
                //string str_Mensaje = string.Empty;
                string str_Titulo = "Notificación de Alerta";
                Kruma.Core.Business.Entity.Parametro obj_ParametroCorreo = Kruma.Core.Business.Data.Parametro.Obtener(Morken.SisMon.Entidad.Constante.Parametro.Modulo, Morken.SisMon.Entidad.Constante.Parametro.Correo);
                Kruma.Core.Business.Entity.Parametro obj_ParametroSistemaNombre = Kruma.Core.Business.Logical.Parametro.Obtener("CORE", "SISTEMANOMBRE");

                foreach (Morken.SisMon.Entidad.Alerta obj_pAlerta in lst_pAlertas)
                {
                    Morken.SisMon.Entidad.Alerta obj_Alerta = Morken.SisMon.Data.Alerta.Obtener(obj_pAlerta.IdAlerta.Value);
                    Kruma.Core.Notification.Entity.MailEntry obj_MailEntry = new Kruma.Core.Notification.Entity.MailEntry();
                    obj_MailEntry.To = obj_ParametroCorreo.Valor;
                    obj_MailEntry.Subject = str_Titulo;
                    obj_MailEntry.IdTipoCorreo = Kruma.Core.Notification.Enum.MailType.Template;
                    obj_MailEntry.BodyFileName = string.Format("{0}{1}",
                        System.Web.HttpContext.Current.Server.MapPath("~"),
                        "\\Plantillas\\NotificacionMonitoreo\\EventoMonitoreo.htm");
                    obj_MailEntry.isHTML = true;
                    obj_MailEntry.Replacements.Add("<%USUARIO%>", "Administrador");
                    obj_MailEntry.Replacements.Add("<%TIPOALERTA%>", obj_Alerta.TipoAlerta.Descripcion);
                    obj_MailEntry.Replacements.Add("<%ESTADO%>", obj_Alerta.AlertaEstado.Descripcion);
                    obj_MailEntry.Replacements.Add("<%FECHAALERTA%>", obj_Alerta.FechaAlerta.Value.ToString("dd/MM/yyyy"));
                    obj_MailEntry.Replacements.Add("<%POSICIONINICIAL%>", string.Format("{0} m", obj_Alerta.PosicionInicial));
                    obj_MailEntry.Replacements.Add("<%POSICIONFINAL%>", string.Format("{0} m", obj_Alerta.PosicionFinal));
                    obj_MailEntry.Replacements.Add("<%VALORUMBRAL%>", obj_Alerta.ValorUmbral);
                    obj_MailEntry.Replacements.Add("<%VALORMAXUMBRAL%>", obj_Alerta.ValorUmbralMaximo);
                    obj_MailEntry.Replacements.Add("<%CANTIDADGOLPES%>", obj_Alerta.CantidadGolpes);
                    obj_MailEntry.Replacements.Add("<%CANTIDADMAXGOLPES%>", obj_Alerta.CantidadGolpesMaximo);
                    //Mensaje del correo
                    StringBuilder stbMensaje = new StringBuilder();
                    //stbMensaje.Append(str_Mensaje);
                    stbMensaje.Append(string.Format("<br><br>Atentamente.<br><br>{0}", obj_ParametroSistemaNombre.Valor));
                    obj_MailEntry.Replacements.Add("<%FIRMA%>", stbMensaje.ToString());
                    Kruma.Core.Notification.NotificationManager obj_NotificationManager = new Kruma.Core.Notification.NotificationManager("Default");
                    obj_NotificationManager.Send(obj_MailEntry);
                }
                obj_Resultado = new Kruma.Core.Util.Common.ProcessResult(lst_pAlertas.Count);
            }
            catch (Exception obj_pExcepcion)
            {
                obj_Resultado = new Kruma.Core.Util.Common.ProcessResult(obj_pExcepcion);
            }
            return obj_Resultado;
        }

    }
}
