<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="RegistroUsuario.aspx.cs" Inherits="Forms_Seguridad_RegistroUsuario" %>

<%@ Register TagPrefix="cc" Namespace="Kruma.Core.Util.Web.Combine.Controls" Assembly="Kruma.Core.Util.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <cc:ScriptCombine ID="ScriptCombine1" runat="server" ScriptHandler="~/Ashx/Combine/ScriptHandler.ashx" Minify="false" Combine="false">
        <Scripts>
            <cc:ScriptReference Path="~/Script/Base/bootstrap.datetimepicker/bootstrap.datetimepicker.js" />
            <cc:ScriptReference Path="~/Script/Base/bootstrap.datetimepicker/locales/bootstrap.datetimepicker.es.js" />
        </Scripts>
    </cc:ScriptCombine>
    <cc:ScriptCombine ID="ScriptCombiner" runat="server" ScriptHandler="~/Ashx/Combine/ScriptHandler.ashx">
        <Scripts>
            <cc:ScriptReference Path="~/Script/Base/jquery.validate/jquery.validate.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.validate/additional-methods.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.datatables/jquery.dataTables.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.datatables/jquery.datatables.responsive.js" />
            <cc:ScriptReference Path="~/Script/Base/bootstrap.datatables/bootstrap.datatables.js" />
            <cc:ScriptReference Path="~/Script/Base/bootstrap.datatables/bootstrap.datatables.responsive.js" />
            <cc:ScriptReference Path="~/Script/Base/bootstrap.switch/bootstrap.switch.js" />
            <cc:ScriptReference Path="~/Script/Base/icheck/icheck.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.bootpag/jquery.bootpag.min.js" />
            <cc:ScriptReference Path="~/Script/Forms/Seguridad/BuscadorPersona.js" />
            <cc:ScriptReference Path="~/Script/Forms/Seguridad/RegistroUsuario.js" />
        </Scripts>
    </cc:ScriptCombine>
    <cc:CssCombine ID="CSSCombiner" runat="server" CSSHandler="~/Ashx/Combine/CSSHandler.ashx">
        <CSSReferences>
            <cc:CssReference Path="~/Style/Base/jquery.datatables/jquery.datatables.css" />
            <cc:CssReference Path="~/Style/Base/jquery.datatables/jquery.datatables.responsive.css" />
            <cc:CssReference Path="~/Style/Base/icheck/red.css" />
            <cc:CssReference Path="~/Style/Base/bootstrap.switch/bootstrap.switch.min.css" />
            <cc:CssReference Path="~/Style/Base/bootstrap.datetimepicker/bootstrap.datetimepicker.css" />
        </CSSReferences>
    </cc:CssCombine>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <div class="box box-warning">
                <div class="box-header">
                    <div class="btn-group">
                        <button title="Grabar" class="btnGrabar btn btn-default btn-sm checkbox-toggle"><i class="fa fa-floppy-o"></i></button>
                        <button title="Reiniciar Contraseña" class="btnReiniciar btn btn-default btn-sm"><i class="fa fa-unlock-alt"></i></button>
                        <button title="Cancelar" class="btnCancelar btn btn-default btn-sm"><i class="fa fa-arrow-left"></i></button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="smart-widget">
        <div class="smart-widget-inner">
            <ul class="nav nav-tabs tab-style1">
                <li class="active">
                    <a id="lnkTabUsuario" href="#tabUsuario" data-toggle="tab">
                        <span class="icon-wrapper"><i class="glyphicon glyphicon-file"></i></span>
                        Usuario
                    </a>
                </li>
                <li>
                    <a id="lnkTabPerfil" href="#tabPerfil" data-toggle="tab">
                        <span class="icon-wrapper"><i class="fa fa-list-ul"></i></span>
                        Perfil
                    </a>
                </li>
            </ul>
            <div class="smart-widget-body">
                <div class="tab-content">
                    <div class="tab-pane fade in active" id="tabUsuario">
                        <div class="col-lg-3">
                            <div class="block h4"><strong>Usuario</strong></div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="box box box-warning">
                                    <div class="box-body">
                                        <div class="form-group">
                                            <label for="txtCodigo">Código:</label>
                                            <input type="text" id="txtCodigo" name="ctl00$cphBody$txtCodigo" maxlength="20" class="form-control" placeholder="Ingrese el código" />
                                        </div>
                                        <div class="form-group">
                                            <label for="txtUsuarioRed">Usuario de Red:</label>
                                            <input type="text" id="txtUsuarioRed" name="ctl00$cphBody$txtUsuarioRed" maxlength="20" class="form-control" placeholder="Ingrese el usuario de red" />
                                        </div>
                                        <div class="form-group">
                                            <label for="txtPersona">Persona:</label>
                                            <div id="txtPersona" class="input-group">
                                                <input id="txtPersona_input" name="ctl00$cphBody$txtPersona_input" type="text" class="form-control" readonly="readonly" placeholder="Seleccione una persona">
                                                <span id="btnPersona_remove" class="btn btn-default input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                                <span id="btnPersona_find" class="btn btn-default input-group-addon"><span class="fa fa-search"></span></span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="chkFlagExpiracion">Expiración del usuario:</label>
                                            <div class="input-group">
                                                <input type="checkbox" id="chkFlagExpiracion" data-on-text="SI" data-off-text="NO" data-size="mini" data-on-color="danger" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="txtFechaExpiracion">Fecha de expiración del usuario:</label>
                                            <div id="txtFechaExpiracion" class="input-group date datetime">
                                                <input id="txtFechaExpiracion_input" name="txtFechaExpiracion_input" class="form-control" size="16" type="text" value="" placeholder="Ingrese la fecha de expiración de contraseña" />
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-th"></span></span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="ddlEstado">Estado:</label>
                                            <asp:DropDownList ID="ddlEstado" runat="server" ClientIDMode="Static" CssClass="form-control" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="tab-pane fade in" id="tabPerfil">
                        <div class="col-lg-3">
                            <div class="block h4"><strong>Perfil</strong></div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="box box box-warning">
                                    <div class="form-group">
                                        <input type="hidden" id="hdPerfiles" name="ctl00$cphBody$hdPerfiles" />
                                        <div id="divPerfiles" class="col-sm-12">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row auditoria">
        <div class="col-xs-12">
            <div class="box box box-warning">
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-3">
                            <div class="dl-horizontal">
                                <span class="label">Usuario de Creación:</span>
                                <span class="field" id="ddUsuarioCreacion"></span>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="dl-horizontal">
                                <span class="label">Fecha de Creación:</span>
                                <span class="field" id="ddFechaCreacion"></span>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="dl-horizontal">
                                <span class="label">Usuario de Modificación:</span>
                                <span class="field" id="ddUsuarioModificacion"></span>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="dl-horizontal">
                                <span class="label">Fecha de Modificación:</span>
                                <span class="field" id="ddFechaModificacion"></span>
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
                        <button title="Grabar" class="btnGrabar btn btn-default btn-sm checkbox-toggle"><i class="fa fa-floppy-o"></i></button>
                        <button title="Reiniciar Contraseña" class="btnReiniciar btn btn-default btn-sm"><i class="fa fa-unlock-alt"></i></button>
                        <button title="Cancelar" class="btnCancelar btn btn-default btn-sm"><i class="fa fa-arrow-left"></i></button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade colored-header danger" id="mdBuscadorPersona" tabindex="-1" style="display: none;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h3>Persona</h3>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="box box-warning">
                                <div class="box-header">
                                    <h3 class="box-title">Criterios de Búsqueda</h3>
                                    <div class="box-tools pull-right">
                                        <button class="btn btn-default btn-sm" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label for="txtNombreBuscadorPersona">Nombre:</label>
                                                <input type="text" id="txtNombreBuscadorPersona" class="form-control" maxlength="50" placeholder="Ingrese el nombre" />
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label for="ddlTipoDocumentoBuscadorPersona">Tipo Documento:</label>
                                                <asp:DropDownList ID="ddlTipoDocumentoBuscadorPersona" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label for="txtNroDocumentoBuscadorPersona">Documento:</label>
                                                <input type="text" id="txtNroDocumentoBuscadorPersona" class="form-control" maxlength="20" placeholder="ingrese el nro. documento" />
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
                                    <button id="btnBuscarBuscadorPersona" title="Buscar" class="btn btn-default btn-sm"><i class="fa fa-search"></i></button>
                                </div>
                                <div class="box-body">
                                    <div id="divGrillaBuscadorPersona" style="width: 100%">
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-5">
                                            <div id="divPaginacionInfoBuscadorPersona" class="dataTables_info"></div>
                                        </div>
                                        <div class="col-sm-7">
                                            <div id="divPaginacionBuscadorPersona" class="dataTables_paginate paging_simple_numbers"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger btn-flat btn-aceptar">Aceptar</button>
                    <button type="button" class="btn btn-default btn-flat btn-cancelar">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdIdUsuario" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdIdPersona" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdIdAlmacen" runat="server" ClientIDMode="Static" />
</asp:Content>




