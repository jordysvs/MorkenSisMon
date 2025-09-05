<%@ WebHandler Language="C#" Class="ReporteSistemaExcelHandler" %>

using System;
using System.Web;
using System.IO;
using NPOI.HSSF.UserModel;

public class ReporteSistemaExcelHandler : IHttpHandler {

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
        string str_Titulo = string.Format("ReporteSistema_{0}", DateTime.Now.ToString("ddMMyyyy_HHmm"));
        
        int? int_pIdAlertaEstado = null;
        if (!string.IsNullOrEmpty(obj_pContext.Request["int_pIdAlertaEstado"]))
            int_pIdAlertaEstado = int.Parse(obj_pContext.Request["int_pIdAlertaEstado"].ToString());

        DateTime? dt_pFechaAlertaInicio = null;
        if (!string.IsNullOrEmpty(obj_pContext.Request["dt_pFechaAlertaInicio"]))
            dt_pFechaAlertaInicio = DateTime.Parse(obj_pContext.Request["dt_pFechaAlertaInicio"].ToString());

        DateTime? dt_pFechaAlertaFin = null;
        if (!string.IsNullOrEmpty(obj_pContext.Request["dt_pFechaAlertaFin"]))
            dt_pFechaAlertaFin = DateTime.Parse(obj_pContext.Request["dt_pFechaAlertaFin"].ToString());

        Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Alerta> lst_Lista = Morken.SisMon.Negocio.Alerta.ListarReporteSistema(
           int_pIdAlertaEstado, dt_pFechaAlertaInicio, dt_pFechaAlertaFin, null, null);

        MemoryStream obj_Stream = GenerarExcel(str_Titulo, lst_Lista);
        if (obj_Stream != null)
        {
            obj_pContext.Response.Clear();
            obj_pContext.Response.ClearHeaders();
            obj_pContext.Response.ClearContent();
            obj_pContext.Response.AddHeader("content-disposition", "attachment; filename=" + str_Titulo + ".xls");
            obj_pContext.Response.ContentType = "application/msexcel";
            obj_pContext.Response.BinaryWrite(obj_Stream.GetBuffer());
        }
        obj_pContext.Response.End();


        //System.Text.StringBuilder stb_Reporte = new System.Text.StringBuilder();
        //stb_Reporte.Append("<table>");
        //stb_Reporte.Append("<tr><td colspan='2' style='font-size:18px; font-weight:bold;'>Reporte de Monitoreo</td><td></td></tr>");
        //stb_Reporte.Append("<tr><td></td></tr>");
        //stb_Reporte.Append(string.Format("<tr><td>Fecha:</td><td style='text-align:left;'>{0}</td></tr>", DateTime.Now.ToString("dd/MM/yyyy HH:mm")));
        //stb_Reporte.Append("<tr><td></td></tr>");
        //stb_Reporte.Append("</table>");
        //stb_Reporte.Append("<table style='width:100%' border='1' rules='all' cellspacing='0'>");
        //stb_Reporte.Append("<tr style='background-color:red; color:white;'>");
        //stb_Reporte.Append("<th>Código Error</th>");
        //stb_Reporte.Append("<th>Fecha Alerta</th>");
        //stb_Reporte.Append("<th>Observación</th>");
        //stb_Reporte.Append("<th>Estado</th>");
        //stb_Reporte.Append("</tr>");

        //if (lst_Lista.Result.Count > 0)
        //{
        //    for (int i = 0; i < lst_Lista.Result.Count; i++)
        //    {
        //        stb_Reporte.Append("<tr>");
        //        stb_Reporte.AppendFormat("<td>{0}</td>", lst_Lista.Result[i].CodigoError);
        //        stb_Reporte.AppendFormat("<td>{0}</td>", lst_Lista.Result[i].FechaAlerta.Value.ToString("dd/MM/yyyy  HH:mm"));
        //        stb_Reporte.AppendFormat("<td>{0}</td>", lst_Lista.Result[i].Observacion);
        //        stb_Reporte.AppendFormat("<td>{0}</td>", lst_Lista.Result[i].AlertaEstado.Descripcion);
        //    }
        //}
        //stb_Reporte.Append("</table>");
        //string date = DateTime.Now.ToFileTime().ToString();
        //obj_pContext.Response.Clear();
        //obj_pContext.Response.Buffer = true;
        //obj_pContext.Response.AddHeader("content-disposition", "attachment; filename=ReporteSistema_" + date + ".xls");
        //obj_pContext.Response.ContentType = "application/msexcel";
        //obj_pContext.Response.ContentEncoding = System.Text.Encoding.Default;

        //System.IO.StringWriter objSW = new System.IO.StringWriter();
        //System.Web.UI.HtmlTextWriter objHTW = new System.Web.UI.HtmlTextWriter(objSW);

        //obj_pContext.Response.Write(stb_Reporte.ToString());
        //obj_pContext.Response.Flush();
        //obj_pContext.Response.End();
    }

    private MemoryStream GenerarExcel(string str_pName,  Kruma.Core.Util.Common.List<Morken.SisMon.Entidad.Alerta> lst_pLista)
    {
        MemoryStream obj_MemoryStream = null;
        HSSFWorkbook obj_WorkBook = null;
        try
        {

            obj_WorkBook = new HSSFWorkbook();

            HSSFSheet obj_Sheet = (HSSFSheet)obj_WorkBook.CreateSheet(str_pName);
            HSSFRow obj_Fila = null;
            HSSFCell obj_Celda = null;
            int int_Fila = 5;

            short int_IndiceColor = 45;
            HSSFPalette obj_Paleta = obj_WorkBook.GetCustomPalette();
            obj_Paleta.SetColorAtIndex(int_IndiceColor, (byte)32, (byte)84, (byte)132);

            string str_FontName = "Calibri";

            //Titulo
            HSSFCellStyle obj_EstiloTitulo = (HSSFCellStyle)obj_WorkBook.CreateCellStyle();
            HSSFFont obj_FontTitulo = (HSSFFont)obj_WorkBook.CreateFont();
            obj_FontTitulo.FontName = str_FontName;
            obj_FontTitulo.IsBold = true;
            obj_FontTitulo.FontHeightInPoints = 16;
            obj_EstiloTitulo.SetFont(obj_FontTitulo);

            //Fecha Titulo
            HSSFCellStyle obj_EstiloFechaTitulo = (HSSFCellStyle)obj_WorkBook.CreateCellStyle();
            HSSFFont obj_FontFechaTitulo = (HSSFFont)obj_WorkBook.CreateFont();
            obj_FontFechaTitulo.FontName = str_FontName;
            obj_FontFechaTitulo.IsBold = true;
            obj_EstiloFechaTitulo.SetFont(obj_FontFechaTitulo);

            //Fecha
            HSSFCellStyle obj_EstiloFecha = (HSSFCellStyle)obj_WorkBook.CreateCellStyle();
            HSSFFont obj_FontFecha = (HSSFFont)obj_WorkBook.CreateFont();
            obj_FontFecha.FontName = str_FontName;
            obj_FontFecha.IsBold = false;
            obj_EstiloFecha.SetFont(obj_FontFecha);

            HSSFCellStyle obj_EstiloCabeceraGrilla = (HSSFCellStyle)obj_WorkBook.CreateCellStyle();
            obj_EstiloCabeceraGrilla.FillForegroundColor = obj_Paleta.GetColor(int_IndiceColor).Indexed;
            obj_EstiloCabeceraGrilla.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;
            obj_EstiloCabeceraGrilla.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            obj_EstiloCabeceraGrilla.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            obj_EstiloCabeceraGrilla.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            obj_EstiloCabeceraGrilla.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            obj_EstiloCabeceraGrilla.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            obj_EstiloCabeceraGrilla.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            HSSFFont obj_FontCabeceraGrilla = (HSSFFont)obj_WorkBook.CreateFont();
            obj_FontCabeceraGrilla.Color = new NPOI.HSSF.Util.HSSFColor.White().Indexed;
            obj_FontCabeceraGrilla.FontName = str_FontName;
            obj_FontCabeceraGrilla.IsBold = true;
            obj_EstiloCabeceraGrilla.SetFont(obj_FontCabeceraGrilla);

            HSSFFont obj_FontDetalleGrilla = (HSSFFont)obj_WorkBook.CreateFont();
            obj_FontDetalleGrilla.FontName = str_FontName;

            HSSFCellStyle obj_EstiloDetalleGrillaLeft = (HSSFCellStyle)obj_WorkBook.CreateCellStyle();
            obj_EstiloDetalleGrillaLeft.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Top;
            obj_EstiloDetalleGrillaLeft.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
            obj_EstiloDetalleGrillaLeft.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            obj_EstiloDetalleGrillaLeft.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            obj_EstiloDetalleGrillaLeft.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            obj_EstiloDetalleGrillaLeft.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            obj_EstiloDetalleGrillaLeft.SetFont(obj_FontDetalleGrilla);

            HSSFCellStyle obj_EstiloDetalleGrillaCenter = (HSSFCellStyle)obj_WorkBook.CreateCellStyle();
            obj_EstiloDetalleGrillaCenter.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Top;
            obj_EstiloDetalleGrillaCenter.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            obj_EstiloDetalleGrillaCenter.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            obj_EstiloDetalleGrillaCenter.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            obj_EstiloDetalleGrillaCenter.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            obj_EstiloDetalleGrillaCenter.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            obj_EstiloDetalleGrillaCenter.SetFont(obj_FontDetalleGrilla);

            HSSFCellStyle obj_EstiloDetalleGrillaRight = (HSSFCellStyle)obj_WorkBook.CreateCellStyle();
            obj_EstiloDetalleGrillaRight.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Top;
            obj_EstiloDetalleGrillaRight.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
            obj_EstiloDetalleGrillaRight.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            obj_EstiloDetalleGrillaRight.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            obj_EstiloDetalleGrillaRight.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            obj_EstiloDetalleGrillaRight.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            obj_EstiloDetalleGrillaRight.SetFont(obj_FontDetalleGrilla);

            //Titulo
            obj_Fila = (HSSFRow)obj_Sheet.CreateRow(0);
            obj_Celda = (HSSFCell)obj_Fila.CreateCell(0);
            obj_Celda.SetCellValue("REPORTE DE SISTEMA");
            obj_Celda.CellStyle = obj_EstiloTitulo;

            NPOI.SS.Util.CellRangeAddress obj_CeldaTitulo = new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 2);
            obj_Sheet.AddMergedRegion(obj_CeldaTitulo);

            //Fecha
            obj_Fila = (HSSFRow)obj_Sheet.CreateRow(2);
            obj_Celda = (HSSFCell)obj_Fila.CreateCell(0);
            obj_Celda.SetCellValue("Fecha:");
            obj_Celda.CellStyle = obj_EstiloFechaTitulo;

            obj_Celda = (HSSFCell)obj_Fila.CreateCell(1);
            obj_Celda.SetCellValue(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            obj_Celda.CellStyle = obj_EstiloFecha;

            //Cabecera Grilla
            obj_Fila = (HSSFRow)obj_Sheet.CreateRow(4);
            obj_Celda = (HSSFCell)obj_Fila.CreateCell(0);
            obj_Celda.SetCellValue("Código Error");
            obj_Celda.CellStyle = obj_EstiloCabeceraGrilla;

            obj_Celda = (HSSFCell)obj_Fila.CreateCell(1);
            obj_Celda.SetCellValue("Fecha Alerta");
            obj_Celda.CellStyle = obj_EstiloCabeceraGrilla;

            obj_Celda = (HSSFCell)obj_Fila.CreateCell(2);
            obj_Celda.SetCellValue("Observación");
            obj_Celda.CellStyle = obj_EstiloCabeceraGrilla;

            obj_Celda = (HSSFCell)obj_Fila.CreateCell(3);
            obj_Celda.SetCellValue("Estado");
            obj_Celda.CellStyle = obj_EstiloCabeceraGrilla;

            for (int i = 0; i < lst_pLista.Result.Count; i++)
            {
                obj_Fila = (HSSFRow)obj_Sheet.CreateRow(int_Fila++);
                obj_Celda = (HSSFCell)obj_Fila.CreateCell(0);
                obj_Celda.SetCellValue(lst_pLista.Result[i].CodigoError.ToString());
                obj_Celda.CellStyle = obj_EstiloDetalleGrillaLeft;

                obj_Celda = (HSSFCell)obj_Fila.CreateCell(1);
                obj_Celda.SetCellValue(lst_pLista.Result[i].FechaAlerta.Value.ToString("dd/MM/yyyy  HH:mm"));
                obj_Celda.CellStyle = obj_EstiloDetalleGrillaLeft;
                obj_Celda.CellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;

                obj_Celda = (HSSFCell)obj_Fila.CreateCell(2);
                obj_Celda.SetCellValue(lst_pLista.Result[i].Observacion);
                obj_Celda.CellStyle = obj_EstiloDetalleGrillaLeft;

                obj_Celda = (HSSFCell)obj_Fila.CreateCell(3);
                obj_Celda.SetCellValue(lst_pLista.Result[i].AlertaEstado.Descripcion);
                obj_Celda.CellStyle = obj_EstiloDetalleGrillaLeft;
            }

            for (int i = 0; i < 14; i++)
                obj_Sheet.AutoSizeColumn((short)i);

            obj_MemoryStream = new MemoryStream();
            obj_WorkBook.Write(obj_MemoryStream);
        }
        catch (Exception ex)
        {
            obj_MemoryStream = null;
        }
        finally
        {
            if (obj_WorkBook != null)
                obj_WorkBook.Close();
        }
        return obj_MemoryStream;
    }

    #endregion
}