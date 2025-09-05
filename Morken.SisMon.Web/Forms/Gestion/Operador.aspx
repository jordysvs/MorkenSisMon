<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/Default.master"  CodeFile="Operador.aspx.cs" Inherits="Forms_Gestion_Operador" %>

<%@ Register TagPrefix="cc" Namespace="Kruma.Core.Util.Web.Combine.Controls" Assembly="Kruma.Core.Util.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <cc:ScriptCombine ID="ScriptCombiner" runat="server" ScriptHandler="~/Ashx/Combine/ScriptHandler.ashx" Combine="false">
        <Scripts>
            <cc:ScriptReference Path="~/Script/Base/jquery.datatables/jquery.dataTables.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.datatables/jquery.datatables.responsive.js" />
            <cc:ScriptReference Path="~/Script/Base/bootstrap.datatables/bootstrap.datatables.js" />
            <cc:ScriptReference Path="~/Script/Base/bootstrap.datatables/bootstrap.datatables.responsive.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.bootpag/jquery.bootpag.min.js" />
            <cc:ScriptReference Path="~/Script/Forms/Gestion/Operador.js" />
        </Scripts>
    </cc:ScriptCombine>
    <cc:CssCombine ID="CSSCombiner" runat="server" CSSHandler="~/Ashx/Combine/CSSHandler.ashx">
        <CSSReferences>
            <cc:CssReference Path="~/Style/Base/jquery.datatables/jquery.datatables.css" />
            <cc:CssReference Path="~/Style/Base/jquery.datatables/jquery.datatables.responsive.css" />
            <cc:CssReference Path="~/Style/Base/bootstrap.datatables/bootstrap.datatables.css" />
        </CSSReferences>
    </cc:CssCombine>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <div class="row">
        <div id="divFiltro" class="col-xs-12">
            <div class="box box-warning collapsed-box">
                <div class="box-header">
                    <h3 class="box-title">Criterios de Búsqueda</h3>
                    <div class="box-tools pull-right">
                        <button class="btn btn-default btn-sm" data-widget="collapse"><i class="fa fa-plus"></i></button>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="txtNroDocumento">Documento:</label>
                                <asp:TextBox ID="txtNroDocumento" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="20" placeholder="Ingrese el nro. documento" />
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="txtNombre">Nombre:</label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="20" placeholder="Ingrese el nombre" />
                            </div>
                        </div>
                         <div class="col-lg-4">
                            <div class="form-group">
                                <label for="ddlEstado">Estado:</label>
                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
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
                <div class="box-header">
                    <div class="btn-group">
                        <button id="btnAgregar" title="Agregar" class="btn btn-default btn-sm checkbox-toggle"><i class="fa fa-file-o"></i></button>
                        <button id="btnModificar" title="Modificar" class="btn btn-default btn-sm"><i class="fa fa-edit"></i></button>
                        <button id="btnActivar" title="Activar" class="btn btn-default btn-sm"><i class="glyphicon glyphicon-floppy-saved"></i></button>
                        <button id="btnInactivar" title="Inactivar" class="btn btn-default btn-sm"><i class="glyphicon glyphicon-floppy-remove"></i></button>
                    </div>
                    <button id="btnBuscar" title="Buscar" class="btn btn-default btn-sm"><i class="fa fa-search"></i></button>
                </div>
                <div class="box-body">
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
</asp:Content>