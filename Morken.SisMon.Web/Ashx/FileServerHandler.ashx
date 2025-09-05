<%@ WebHandler Language="C#" Class="FileServerHandler" %>

using System;
using System.Web;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Web.Script.Serialization;

public class FileServerHandler : IHttpHandler
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
        Kruma.Core.Util.Common.ProcessResult obj_ProcessResult = null;
        try
        {
            int? int_IdDocumento = null;
            int int_IdAlmacen = int.Parse(obj_pContext.Request["int_pIdAlmacen"]);
            int int_IdRegistro = int.Parse(obj_pContext.Request["int_pIdRegistro"]);
            bool bln_Replace = bool.Parse(obj_pContext.Request["bln_pReplace"]);

            if (obj_pContext.Request.Files.Count > 0)
            {
                string str_FileName;
                var obj_File = obj_pContext.Request.Files[0];

                Kruma.Core.FileServer.FileServerManager obj_FileServerManager = new Kruma.Core.FileServer.FileServerManager();
                if (bln_Replace)
                {
                    List<Kruma.Core.FileServer.Entity.FileUpload> lstFotos = obj_FileServerManager.getFiles(int_IdAlmacen, int_IdRegistro);
                    foreach (Kruma.Core.FileServer.Entity.FileUpload obj_Foto in lstFotos)
                        obj_FileServerManager.deleteFile(obj_Foto.IdDocumento.Value, obj_Foto.IdAlmacen.Value, obj_Foto.IdRegistro.Value, Kruma.Core.Security.SecurityManager.Usuario.IdUsuario);
                }

                if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                {
                    string[] files = obj_File.FileName.Split(new char[] { '\\' });
                    str_FileName = files[files.Length - 1];
                }
                else
                    str_FileName = obj_File.FileName;

                int_IdDocumento = obj_FileServerManager.Upload(obj_pContext.Request.Files[0].InputStream, int_IdAlmacen, int_IdRegistro, str_FileName, Kruma.Core.Security.SecurityManager.Usuario.IdUsuario);
            }
            obj_ProcessResult = new Kruma.Core.Util.Common.ProcessResult(int_IdDocumento);
        }
        catch (Exception obj_pExcepcion)
        {
            obj_ProcessResult = new Kruma.Core.Util.Common.ProcessResult(obj_pExcepcion);
        }
        obj_pContext.Response.Write(new JavaScriptSerializer().Serialize(obj_ProcessResult));
    }

    #endregion
}