using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morken.SisMon.Negocio
{
    public static class Util
    {
        //Clase
        public static IDisposable XMLCredenciales()
        {
            string str_Credencial = Kruma.Core.Business.Logical.Parametro.Obtener(
            Morken.SisMon.Entidad.Constante.Parametro.Modulo,
            Morken.SisMon.Entidad.Constante.Parametro.XML_Credenciales
            ).Valor;

            if (str_Credencial == Morken.SisMon.Entidad.Constante.Condicion_Positivo)
            {
                int int_IdAlmacen = int.Parse(Kruma.Core.Business.Logical.Parametro.Obtener(
                  Morken.SisMon.Entidad.Constante.Parametro.Modulo,
                  Morken.SisMon.Entidad.Constante.Parametro.Almacen_XML).Valor);
                Kruma.Core.FileServer.Entity.Almacen obj_Almacen = Kruma.Core.FileServer.Logical.Almacen.Obtener(int_IdAlmacen);

                return (SimpleImpersonation.Impersonation.LogonUser(obj_Almacen.Dominio, obj_Almacen.Usuario, obj_Almacen.Clave, SimpleImpersonation.LogonType.NewCredentials));
            }
            else
                return new XMLSinCredenciales();
        }

        public class XMLSinCredenciales : IDisposable
        {
            public void Dispose()
            {
            }
        }

        public static int CalcularCantidadDias(DateTime dtm_FechaDesde, DateTime dtm_FechaHasta)
        {
            TimeSpan ts = dtm_FechaHasta - dtm_FechaDesde;
            return ts.Days;
        }
        public static int CalcularCantidadMeses(DateTime dtm_FechaDesde, DateTime dtm_FechaHasta)
        {
            return Math.Abs((dtm_FechaDesde.Month - dtm_FechaHasta.Month) + 12 * (dtm_FechaDesde.Year - dtm_FechaHasta.Year));
        }

        public static int CalcularCantidadHoras(DateTime dtm_FechaDesde, DateTime dtm_FechaHasta)
        {
            TimeSpan ts = dtm_FechaHasta - dtm_FechaDesde;
            return (int) ts.TotalHours;
        }
    }
}
