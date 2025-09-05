<%@ WebHandler Language="C#" Class="FileServerViewHandler" %>

using System;
using System.Web;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Web.Script.Serialization;

public class FileServerViewHandler : IHttpHandler
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
        int int_IdAlmacen = 0;
        int int_IdRegistro = 0;
        int? int_IdDocumento = null;
        string str_IdDocumento = string.Empty;
        //bool bln_Resize = false;
        //int? int_Width = null;
        //int? int_Height = null;

        if (obj_pContext.Request.QueryString.Count > 0)
        {
            //GET
            if (obj_pContext.Request.QueryString["ref"] != null)
            {
                //Encriptado
                string str_Parametro = HttpUtility.UrlDecode(obj_pContext.Request.QueryString["ref"]).Replace(" ", "+");
                str_Parametro = new Kruma.Core.Criptography.CriptographyManager().Dencrypt(str_Parametro);
                string[] arr_Parametro = str_Parametro.Split('|');
                int_IdAlmacen = int.Parse(arr_Parametro[0]);
                int_IdRegistro = int.Parse(arr_Parametro[1]);
                str_IdDocumento = arr_Parametro[2];
                //if (arr_Parametro.Length > 3)
                //    bln_Resize = bool.Parse(arr_Parametro[3]);
                //if (arr_Parametro.Length > 4)
                //    int_Width = int.Parse(arr_Parametro[4]);
                //if (arr_Parametro.Length > 5)
                //    int_Height = int.Parse(arr_Parametro[5]);

            }
            else
            {
                int.TryParse(obj_pContext.Request.QueryString["idAlm"], out int_IdAlmacen);
                int.TryParse(obj_pContext.Request.QueryString["idReg"], out int_IdRegistro);
                str_IdDocumento = obj_pContext.Request.QueryString["idDoc"];
                //if (obj_pContext.Request.QueryString["resize"] != null)
                //    bln_Resize = bool.Parse(obj_pContext.Request.QueryString["resize"]);
                //if (obj_pContext.Request.QueryString["width"] != null)
                //    int_Width = int.Parse(obj_pContext.Request.QueryString["width"]);
                //if (obj_pContext.Request.QueryString["height"] != null)
                //    int_Height = int.Parse(obj_pContext.Request.QueryString["height"]);
            }
        }
        else
        {
            //POST
            int.TryParse(obj_pContext.Request["int_pIdAlmacen"], out int_IdAlmacen);
            int.TryParse(obj_pContext.Request["int_pIdRegistro"], out int_IdRegistro);
            str_IdDocumento = obj_pContext.Request["int_pIdDocumento"];
            //if (obj_pContext.Request.QueryString["bln_pResize"] != null)
            //    bln_Resize = bool.Parse(obj_pContext.Request.QueryString["bln_pResize"]);
            //if (obj_pContext.Request.QueryString["int_pWidth"] != null)
            //    int_Width = int.Parse(obj_pContext.Request.QueryString["int_pWidth"]);
            //if (obj_pContext.Request.QueryString["int_pHeight"] != null)
            //    int_Height = int.Parse(obj_pContext.Request.QueryString["int_pHeight"]);
        }

        Kruma.Core.FileServer.FileServerManager obj_FileServerManager = new Kruma.Core.FileServer.FileServerManager();
        if (!string.IsNullOrEmpty(str_IdDocumento))
            int_IdDocumento = int.Parse(str_IdDocumento);
        else
        {
            List<Kruma.Core.FileServer.Entity.FileUpload> lst_FileUpload = obj_FileServerManager.getFiles(int_IdAlmacen, int_IdRegistro);
            if (lst_FileUpload.Count > 0)
                int_IdDocumento = lst_FileUpload[0].IdDocumento;
        }

        Kruma.Core.FileServer.Entity.FileUpload obj_FileUpload = null;
        if (int_IdDocumento.HasValue)
            obj_FileUpload = obj_FileServerManager.getFileStream(int_IdAlmacen, int_IdRegistro, int_IdDocumento.Value);

        if (obj_FileUpload != null)
        {
            obj_pContext.Response.Clear();
            obj_pContext.Response.ClearHeaders();
            obj_pContext.Response.ClearContent();
            obj_pContext.Response.BufferOutput = true;
            obj_pContext.Response.AddHeader("Content-Disposition", "inline; filename=" + obj_FileUpload.DescripcionLogica);
            obj_pContext.Response.ContentType = MimeMapping.GetMimeMapping(obj_FileUpload.Extension);

            //if (bln_Resize)
            //{
            //    Imazen.LightResize.ResizeJob obj_ResizeJob = new Imazen.LightResize.ResizeJob();
            //    obj_ResizeJob.Width = int_Width;
            //    obj_ResizeJob.Height = int_Height;
            //    obj_ResizeJob.Format = Imazen.LightResize.OutputFormat.Png;
            //    obj_FileUpload.Extension = ".png";
                
            //    MemoryStream ms = null;
            //    using (ms = new MemoryStream(8000))
            //    {
            //        obj_ResizeJob.Build(obj_FileUpload.Stream, ms, Imazen.LightResize.JobOptions.LeaveTargetStreamOpen);
            //       ms.WriteTo(obj_pContext.Response.OutputStream);
            //    }
            //}
            //else
            //{
                obj_FileUpload.Stream.CopyTo(obj_pContext.Response.OutputStream);
            //}
            
            obj_pContext.Response.End();
        }
    }



    public class MimeMapping
    {
        private static Dictionary<string, string> dic_ExtensionMap = new Dictionary<string, string>();

        static MimeMapping()
        {
            dic_ExtensionMap.Add(".323", "text/h323");
            dic_ExtensionMap.Add(".asx", "video/x-ms-asf");
            dic_ExtensionMap.Add(".acx", "application/internet-property-stream");
            dic_ExtensionMap.Add(".ai", "application/postscript");
            dic_ExtensionMap.Add(".aif", "audio/x-aiff");
            dic_ExtensionMap.Add(".aiff", "audio/aiff");
            dic_ExtensionMap.Add(".axs", "application/olescript");
            dic_ExtensionMap.Add(".aifc", "audio/aiff");
            dic_ExtensionMap.Add(".asr", "video/x-ms-asf");
            dic_ExtensionMap.Add(".avi", "video/x-msvideo");
            dic_ExtensionMap.Add(".asf", "video/x-ms-asf");
            dic_ExtensionMap.Add(".au", "audio/basic");
            dic_ExtensionMap.Add(".application", "application/x-ms-application");
            dic_ExtensionMap.Add(".bin", "application/octet-stream");
            dic_ExtensionMap.Add(".bas", "text/plain");
            dic_ExtensionMap.Add(".bcpio", "application/x-bcpio");
            dic_ExtensionMap.Add(".bmp", "image/bmp");
            dic_ExtensionMap.Add(".cdf", "application/x-cdf");
            dic_ExtensionMap.Add(".cat", "application/vndms-pkiseccat");
            dic_ExtensionMap.Add(".crt", "application/x-x509-ca-cert");
            dic_ExtensionMap.Add(".c", "text/plain");
            dic_ExtensionMap.Add(".css", "text/css");
            dic_ExtensionMap.Add(".cer", "application/x-x509-ca-cert");
            dic_ExtensionMap.Add(".crl", "application/pkix-crl");
            dic_ExtensionMap.Add(".cmx", "image/x-cmx");
            dic_ExtensionMap.Add(".csh", "application/x-csh");
            dic_ExtensionMap.Add(".cod", "image/cis-cod");
            dic_ExtensionMap.Add(".cpio", "application/x-cpio");
            dic_ExtensionMap.Add(".clp", "application/x-msclip");
            dic_ExtensionMap.Add(".crd", "application/x-mscardfile");
            dic_ExtensionMap.Add(".deploy", "application/octet-stream");
            dic_ExtensionMap.Add(".dll", "application/x-msdownload");
            dic_ExtensionMap.Add(".dot", "application/msword");
            dic_ExtensionMap.Add(".doc", "application/msword");
            dic_ExtensionMap.Add(".dvi", "application/x-dvi");
            dic_ExtensionMap.Add(".dir", "application/x-director");
            dic_ExtensionMap.Add(".dxr", "application/x-director");
            dic_ExtensionMap.Add(".der", "application/x-x509-ca-cert");
            dic_ExtensionMap.Add(".dib", "image/bmp");
            dic_ExtensionMap.Add(".dcr", "application/x-director");
            dic_ExtensionMap.Add(".disco", "text/xml");
            dic_ExtensionMap.Add(".exe", "application/octet-stream");
            dic_ExtensionMap.Add(".etx", "text/x-setext");
            dic_ExtensionMap.Add(".evy", "application/envoy");
            dic_ExtensionMap.Add(".eml", "message/rfc822");
            dic_ExtensionMap.Add(".eps", "application/postscript");
            dic_ExtensionMap.Add(".flr", "x-world/x-vrml");
            dic_ExtensionMap.Add(".fif", "application/fractals");
            dic_ExtensionMap.Add(".gtar", "application/x-gtar");
            dic_ExtensionMap.Add(".gif", "image/gif");
            dic_ExtensionMap.Add(".gz", "application/x-gzip");
            dic_ExtensionMap.Add(".hta", "application/hta");
            dic_ExtensionMap.Add(".htc", "text/x-component");
            dic_ExtensionMap.Add(".htt", "text/webviewhtml");
            dic_ExtensionMap.Add(".h", "text/plain");
            dic_ExtensionMap.Add(".hdf", "application/x-hdf");
            dic_ExtensionMap.Add(".hlp", "application/winhlp");
            dic_ExtensionMap.Add(".html", "text/html");
            dic_ExtensionMap.Add(".htm", "text/html");
            dic_ExtensionMap.Add(".hqx", "application/mac-binhex40");
            dic_ExtensionMap.Add(".isp", "application/x-internet-signup");
            dic_ExtensionMap.Add(".iii", "application/x-iphone");
            dic_ExtensionMap.Add(".ief", "image/ief");
            dic_ExtensionMap.Add(".ivf", "video/x-ivf");
            dic_ExtensionMap.Add(".ins", "application/x-internet-signup");
            dic_ExtensionMap.Add(".ico", "image/x-icon");
            dic_ExtensionMap.Add(".jpg", "image/jpeg");
            dic_ExtensionMap.Add(".jfif", "image/pjpeg");
            dic_ExtensionMap.Add(".jpe", "image/jpeg");
            dic_ExtensionMap.Add(".jpeg", "image/jpeg");
            dic_ExtensionMap.Add(".js", "application/x-javascript");
            dic_ExtensionMap.Add(".lsx", "video/x-la-asf");
            dic_ExtensionMap.Add(".latex", "application/x-latex");
            dic_ExtensionMap.Add(".lsf", "video/x-la-asf");
            dic_ExtensionMap.Add(".manifest", "application/x-ms-manifest");
            dic_ExtensionMap.Add(".mhtml", "message/rfc822");
            dic_ExtensionMap.Add(".mny", "application/x-msmoney");
            dic_ExtensionMap.Add(".mht", "message/rfc822");
            dic_ExtensionMap.Add(".mid", "audio/mid");
            dic_ExtensionMap.Add(".mpv2", "video/mpeg");
            dic_ExtensionMap.Add(".man", "application/x-troff-man");
            dic_ExtensionMap.Add(".mvb", "application/x-msmediaview");
            dic_ExtensionMap.Add(".mpeg", "video/mpeg");
            dic_ExtensionMap.Add(".m3u", "audio/x-mpegurl");
            dic_ExtensionMap.Add(".mdb", "application/x-msaccess");
            dic_ExtensionMap.Add(".mpp", "application/vnd.ms-project");
            dic_ExtensionMap.Add(".m1v", "video/mpeg");
            dic_ExtensionMap.Add(".mpa", "video/mpeg");
            dic_ExtensionMap.Add(".me", "application/x-troff-me");
            dic_ExtensionMap.Add(".m13", "application/x-msmediaview");
            dic_ExtensionMap.Add(".movie", "video/x-sgi-movie");
            dic_ExtensionMap.Add(".m14", "application/x-msmediaview");
            dic_ExtensionMap.Add(".mpe", "video/mpeg");
            dic_ExtensionMap.Add(".mp2", "video/mpeg");
            dic_ExtensionMap.Add(".mov", "video/quicktime");
            dic_ExtensionMap.Add(".mp3", "audio/mpeg");
            dic_ExtensionMap.Add(".mpg", "video/mpeg");
            dic_ExtensionMap.Add(".ms", "application/x-troff-ms");
            dic_ExtensionMap.Add(".nc", "application/x-netcdf");
            dic_ExtensionMap.Add(".nws", "message/rfc822");
            dic_ExtensionMap.Add(".oda", "application/oda");
            dic_ExtensionMap.Add(".ods", "application/oleobject");
            dic_ExtensionMap.Add(".pmc", "application/x-perfmon");
            dic_ExtensionMap.Add(".p7r", "application/x-pkcs7-certreqresp");
            dic_ExtensionMap.Add(".p7b", "application/x-pkcs7-certificates");
            dic_ExtensionMap.Add(".p7s", "application/pkcs7-signature");
            dic_ExtensionMap.Add(".pmw", "application/x-perfmon");
            dic_ExtensionMap.Add(".ps", "application/postscript");
            dic_ExtensionMap.Add(".p7c", "application/pkcs7-mime");
            dic_ExtensionMap.Add(".pbm", "image/x-portable-bitmap");
            dic_ExtensionMap.Add(".ppm", "image/x-portable-pixmap");
            dic_ExtensionMap.Add(".pub", "application/x-mspublisher");
            dic_ExtensionMap.Add(".pnm", "image/x-portable-anymap");
            dic_ExtensionMap.Add(".pml", "application/x-perfmon");
            dic_ExtensionMap.Add(".p10", "application/pkcs10");
            dic_ExtensionMap.Add(".pfx", "application/x-pkcs12");
            dic_ExtensionMap.Add(".p12", "application/x-pkcs12");
            dic_ExtensionMap.Add(".pdf", "application/pdf");
            dic_ExtensionMap.Add(".pps", "application/vnd.ms-powerpoint");
            dic_ExtensionMap.Add(".p7m", "application/pkcs7-mime");
            dic_ExtensionMap.Add(".pko", "application/vndms-pkipko");
            dic_ExtensionMap.Add(".ppt", "application/vnd.ms-powerpoint");
            dic_ExtensionMap.Add(".pmr", "application/x-perfmon");
            dic_ExtensionMap.Add(".pma", "application/x-perfmon");
            dic_ExtensionMap.Add(".pot", "application/vnd.ms-powerpoint");
            dic_ExtensionMap.Add(".prf", "application/pics-rules");
            dic_ExtensionMap.Add(".pgm", "image/x-portable-graymap");
            dic_ExtensionMap.Add(".qt", "video/quicktime");
            dic_ExtensionMap.Add(".ra", "audio/x-pn-realaudio");
            dic_ExtensionMap.Add(".rgb", "image/x-rgb");
            dic_ExtensionMap.Add(".ram", "audio/x-pn-realaudio");
            dic_ExtensionMap.Add(".rmi", "audio/mid");
            dic_ExtensionMap.Add(".ras", "image/x-cmu-raster");
            dic_ExtensionMap.Add(".roff", "application/x-troff");
            dic_ExtensionMap.Add(".rtf", "application/rtf");
            dic_ExtensionMap.Add(".rtx", "text/richtext");
            dic_ExtensionMap.Add(".sv4crc", "application/x-sv4crc");
            dic_ExtensionMap.Add(".spc", "application/x-pkcs7-certificates");
            dic_ExtensionMap.Add(".setreg", "application/set-registration-initiation");
            dic_ExtensionMap.Add(".snd", "audio/basic");
            dic_ExtensionMap.Add(".stl", "application/vndms-pkistl");
            dic_ExtensionMap.Add(".setpay", "application/set-payment-initiation");
            dic_ExtensionMap.Add(".stm", "text/html");
            dic_ExtensionMap.Add(".shar", "application/x-shar");
            dic_ExtensionMap.Add(".sh", "application/x-sh");
            dic_ExtensionMap.Add(".sit", "application/x-stuffit");
            dic_ExtensionMap.Add(".spl", "application/futuresplash");
            dic_ExtensionMap.Add(".sct", "text/scriptlet");
            dic_ExtensionMap.Add(".scd", "application/x-msschedule");
            dic_ExtensionMap.Add(".sst", "application/vndms-pkicertstore");
            dic_ExtensionMap.Add(".src", "application/x-wais-source");
            dic_ExtensionMap.Add(".sv4cpio", "application/x-sv4cpio");
            dic_ExtensionMap.Add(".tex", "application/x-tex");
            dic_ExtensionMap.Add(".tgz", "application/x-compressed");
            dic_ExtensionMap.Add(".t", "application/x-troff");
            dic_ExtensionMap.Add(".tar", "application/x-tar");
            dic_ExtensionMap.Add(".tr", "application/x-troff");
            dic_ExtensionMap.Add(".tif", "image/tiff");
            dic_ExtensionMap.Add(".txt", "text/plain");
            dic_ExtensionMap.Add(".texinfo", "application/x-texinfo");
            dic_ExtensionMap.Add(".trm", "application/x-msterminal");
            dic_ExtensionMap.Add(".tiff", "image/tiff");
            dic_ExtensionMap.Add(".tcl", "application/x-tcl");
            dic_ExtensionMap.Add(".texi", "application/x-texinfo");
            dic_ExtensionMap.Add(".tsv", "text/tab-separated-values");
            dic_ExtensionMap.Add(".ustar", "application/x-ustar");
            dic_ExtensionMap.Add(".uls", "text/iuls");
            dic_ExtensionMap.Add(".vcf", "text/x-vcard");
            dic_ExtensionMap.Add(".wps", "application/vnd.ms-works");
            dic_ExtensionMap.Add(".wav", "audio/wav");
            dic_ExtensionMap.Add(".wrz", "x-world/x-vrml");
            dic_ExtensionMap.Add(".wri", "application/x-mswrite");
            dic_ExtensionMap.Add(".wks", "application/vnd.ms-works");
            dic_ExtensionMap.Add(".wmf", "application/x-msmetafile");
            dic_ExtensionMap.Add(".wcm", "application/vnd.ms-works");
            dic_ExtensionMap.Add(".wrl", "x-world/x-vrml");
            dic_ExtensionMap.Add(".wdb", "application/vnd.ms-works");
            dic_ExtensionMap.Add(".wsdl", "text/xml");
            dic_ExtensionMap.Add(".xml", "text/xml");
            dic_ExtensionMap.Add(".xlm", "application/vnd.ms-excel");
            dic_ExtensionMap.Add(".xaf", "x-world/x-vrml");
            dic_ExtensionMap.Add(".xla", "application/vnd.ms-excel");
            dic_ExtensionMap.Add(".xls", "application/vnd.ms-excel");
            dic_ExtensionMap.Add(".xof", "x-world/x-vrml");
            dic_ExtensionMap.Add(".xlt", "application/vnd.ms-excel");
            dic_ExtensionMap.Add(".xlc", "application/vnd.ms-excel");
            dic_ExtensionMap.Add(".xsl", "text/xml");
            dic_ExtensionMap.Add(".xbm", "image/x-xbitmap");
            dic_ExtensionMap.Add(".xlw", "application/vnd.ms-excel");
            dic_ExtensionMap.Add(".xpm", "image/x-xpixmap");
            dic_ExtensionMap.Add(".xwd", "image/x-xwindowdump");
            dic_ExtensionMap.Add(".xsd", "text/xml");
            dic_ExtensionMap.Add(".z", "application/x-compress");
            dic_ExtensionMap.Add(".zip", "application/x-zip-compressed");
            dic_ExtensionMap.Add(".*", "application/octet-stream");
        }

        public static string GetMimeMapping(string fileExtension)
        {
            if (dic_ExtensionMap.ContainsKey(fileExtension))
                return dic_ExtensionMap[fileExtension];
            else
                return dic_ExtensionMap[".*"];
        }
    }

    #endregion

}


