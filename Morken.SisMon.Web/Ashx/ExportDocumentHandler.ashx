<%@ WebHandler Language="C#" Class="ExportDocumentHandler" %>

using System;
using System.Web;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Web.Script.Serialization;

public class ExportDocumentHandler : IHttpHandler
{
    #region Propiedades

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    #endregion

    #region Metodos Publicos

    public void ProcessRequest(HttpContext obj_pContext)
    {
        string str_HTML = string.Empty;
        string str_Nombre = DateTime.Now.ToFileTime().ToString();
        string str_Tipo = "PDF";
        string str_Extension = ".pdf";
        Stream obj_Documento = null;

        if (obj_pContext.Request.Form.Count > 0)
        {
            //HTML
            str_HTML = obj_pContext.Request["str_HTML"];
            str_HTML = HttpUtility.HtmlDecode(str_HTML);
            //Nombre del documento
            if (!string.IsNullOrEmpty(obj_pContext.Request["str_Nombre"]))
                str_Nombre = obj_pContext.Request["str_Nombre"];

            //Tipo de exportacion
            str_Tipo = obj_pContext.Request["str_Tipo"];
            if (str_Tipo == "WORD")
            {
                str_Extension = ".doc";
                obj_Documento = Kruma.Core.Util.Export.HTMLToWORD(str_HTML);
            }
            else
                obj_Documento = Kruma.Core.Util.Export.HTMLToPDF(str_HTML);
        }

        if (obj_Documento != null)
        {
            obj_pContext.Response.Clear();
            obj_pContext.Response.ClearHeaders();
            obj_pContext.Response.ClearContent();
            obj_pContext.Response.BufferOutput = true;
            obj_pContext.Response.AddHeader("Content-Disposition", "inline; filename=" + str_Nombre + str_Extension);
            obj_pContext.Response.ContentType = MimeMapping.GetMimeMapping(str_Extension);
            obj_Documento.CopyTo(obj_pContext.Response.OutputStream);
            obj_pContext.Response.End();
        }
    }

    #endregion
}