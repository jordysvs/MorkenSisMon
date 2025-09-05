<%@ Page Language="C#" MasterPageFile="~/Master/Default.master"  AutoEventWireup="true" CodeFile="ReporteSistema.aspx.cs" Inherits="Forms_Reporte_ReporteSistema" %>

<%@ Register TagPrefix="cc" Namespace="Kruma.Core.Util.Web.Combine.Controls" Assembly="Kruma.Core.Util.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <cc:ScriptCombine ID="ScriptCombiner" runat="server" ScriptHandler="~/Ashx/Combine/ScriptHandler.ashx" Combine="false">
        <Scripts>
            <cc:ScriptReference Path="~/Script/Base/jquery.validate/jquery.validate.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.validate/additional-methods.js" />
            <cc:ScriptReference Path="~/Script/Base/bootstrap.datetimepicker/bootstrap.datetimepicker.js" />
            <cc:ScriptReference Path="~/Script/Base/bootstrap.datetimepicker/locales/bootstrap.datetimepicker.es.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.datatables/jquery.dataTables.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.datatables/jquery.datatables.responsive.js" />
            <cc:ScriptReference Path="~/Script/Base/bootstrap.datatables/bootstrap.datatables.js" />
            <cc:ScriptReference Path="~/Script/Base/bootstrap.datatables/bootstrap.datatables.responsive.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.bootpag/jquery.bootpag.min.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.maskedinput/jquery.maskedinput.js" />
            <cc:ScriptReference Path="~/Script/Forms/Reporte/ReporteSistema.js" />
        </Scripts>
    </cc:ScriptCombine>
    <cc:CssCombine ID="CSSCombiner" runat="server" CSSHandler="~/Ashx/Combine/CSSHandler.ashx">
        <CSSReferences>
            <cc:CssReference Path="~/Style/Base/jquery.datatables/jquery.datatables.css" />
            <cc:CssReference Path="~/Style/Base/jquery.datatables/jquery.datatables.responsive.css" />
            <cc:CssReference Path="~/Style/Base/bootstrap.datatables/bootstrap.datatables.css" />
            <cc:CssReference Path="~/Style/Base/bootstrap.datetimepicker/bootstrap.datetimepicker.css" />
        </CSSReferences>
    </cc:CssCombine>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <div class="box box-warning collapsed-box">
                <div class="box-header">
                    <h3 class="box-title">Criterios de Búsqueda</h3>
                    <div class="box-tools pull-right">
                        <button class="btn btn-default btn-sm" data-widget="collapse"><i class="fa fa-plus"></i></button>
                    </div>
                </div>
                <div id="divFiltro" class="box-body">
                    <div class="row">
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="ddlAlertaEstado">Estado:</label>
                                <asp:DropDownList ID="ddlAlertaEstado" runat="server" CssClass="form-control" ClientIDMode="Static">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="txtFechaInicio">Fecha inicio:</label>
                                <div id="txtFechaInicio" class="input-group date datetime">
                                    <input id="txtFechaInicio_input" name="txtFechaInicio_input" class="form-control datetimeinput" size="16" type="text" value="" placeholder="Ingrese la fecha inicial" />
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-th"></span></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="txtFechaFin">Fecha fin:</label>
                                <div id="txtFechaFin" class="input-group date datetime">
                                    <input id="txtFechaFin_input" name="txtFechaFin_input" class="form-control datetimeinput" size="16" type="text" value="" placeholder="Ingrese la fecha final" />
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-th"></span></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-12">
            <div class="box box-warning">
                <div class="box-body">
                    <div class="box-header">
                        <!-- Check all button -->
                        <div class="btn-group">
                            <button id="btnBuscar" title="Generar" class="btn btn-default btn-sm"><i class="fa fa-cog"></i></button>
                            <button id="btnExportar" title="Exportar" class="btn btn-default btn-sm checkbox-toggle"><i class="fa fa-file-excel-o"></i></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <div class="box box-warning">
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div id="divGrilla" style="width: 100%">
                            </div>
                            <div class="row">
                                <div class="col-sm-5">
                                    <div id="divPaginacionInfo" class="dataTables_info"></div>
                                </div>
                                <div class="col-sm-7">
                                    <div id="divPaginacion" class="dataTables_paginate paging_simple_numbers"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
