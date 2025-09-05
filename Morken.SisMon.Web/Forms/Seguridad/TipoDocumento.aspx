<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="TipoDocumento.aspx.cs" Inherits="Forms_Configuracion_TipoDocumento" %>

<%@ Register TagPrefix="cc" Namespace="Kruma.Core.Util.Web.Combine.Controls" Assembly="Kruma.Core.Util.Web" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphHeader" runat="Server">
    <cc:ScriptCombine ID="ScriptCombiner" runat="server" ScriptHandler="~/Ashx/Combine/ScriptHandler.ashx">
        <Scripts>
            <cc:ScriptReference Path="~/Script/Base/jquery.datatables/jquery.dataTables.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.datatables/jquery.datatables.responsive.js" />
            <cc:ScriptReference Path="~/Script/Base/bootstrap.datatables/bootstrap.datatables.js" />
            <cc:ScriptReference Path="~/Script/Base/bootstrap.datatables/bootstrap.datatables.responsive.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.bootpag/jquery.bootpag.min.js" />
            <cc:ScriptReference Path="~/Script/Forms/Seguridad/TipoDocumento.js" />
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
        <div class="col-xs-12">
            <div class="box box-warning">

                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label for="txtCodigo">Código:</label>
                                <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="20" placeholder="Ingrese el código" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label for="txtDescripcion">Descripcion:</label>
                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="50" placeholder="Ingrese la descripción" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                        </div>
                        <div class="col-lg-6">
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
                    <!-- Check all button -->
                    <div class="btn-group">
                        <button id="btnAgregar" title="Agregar" class="btn btn-default btn-sm checkbox-toggle"><i class="fa fa-file-o"></i></button>
                        <button id="btnModificar" title="Modificar" class="btn btn-default btn-sm"><i class="fa fa-edit"></i></button>
                        <button id="btnActivar" title="Activar" class="btn btn-default btn-sm"><i class="glyphicon glyphicon-floppy-saved"></i></button>
                        <button id="btnInactivar" title="Inactivar" class="btn btn-default btn-sm"><i class="glyphicon glyphicon-floppy-remove"></i></button>
                    </div>
                    <!-- /.btn-group -->
                    <button id="btnBuscar" title="Buscar" class="btn btn-default btn-sm"><i class="fa fa-search"></i></button>
                    <!-- /.pull-right -->
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



