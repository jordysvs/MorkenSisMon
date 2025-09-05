<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="RegistroPerfil.aspx.cs" Inherits="Forms_Seguridad_RegistroPerfil" %>

<%@ Register TagPrefix="cc" Namespace="Kruma.Core.Util.Web.Combine.Controls" Assembly="Kruma.Core.Util.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <cc:ScriptCombine ID="ScriptCombiner" runat="server" ScriptHandler="~/Ashx/Combine/ScriptHandler.ashx">
        <Scripts>
            <cc:ScriptReference Path="~/Script/Base/jquery.validate/jquery.validate.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.validate/additional-methods.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.ui/jquery.ui.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.fancytree/jquery.fancytree.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.fancytree/jquery.fancytree.glyph.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.fancytree/jquery.fancytree.wide.js" />
            <cc:ScriptReference Path="~/Script/Forms/Seguridad/RegistroPerfil.js" />
        </Scripts>
    </cc:ScriptCombine>
    <cc:CSSCombine ID="CSSCombiner" runat="server" CSSHandler="~/Ashx/Combine/CSSHandler.ashx">
        <CSSReferences>
            <cc:CSSReference Path="~/Style/Base/jquery.fancytree/jquery.fancytree.css" />
        </CSSReferences>
    </cc:CSSCombine>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-warning">
                <div class="box-header">
                    <div class="btn-group">
                        <button title="Grabar" class="btnGrabar btn btn-default btn-sm checkbox-toggle"><i class="fa fa-floppy-o"></i></button>
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
                    <a id="lnkTabDocumento" href="#tabPerfil" data-toggle="tab">
                        <span class="icon-wrapper"><i class="glyphicon glyphicon-file"></i></span>
                        Perfil
                    </a>
                </li>
                <li>
                    <a id="lnkTabComentario" href="#tabMenu" data-toggle="tab">
                        <span class="icon-wrapper"><i class="fa fa-list-ul"></i></span>
                        Menú
                    </a>
                </li>
            </ul>
            <div class="smart-widget-body">
                <div class="tab-content">
                    <div class="tab-pane fade in active" id="tabPerfil">
                        <div class="col-lg-3">
                            <div class="block h4"><strong>Perfil</strong></div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="box box box-warning">
                                    <div class="box-body">
                                        <div class="form-group">
                                            <label for="txtCodigo">Código:</label>
                                            <asp:TextBox ID="txtCodigo" runat="server" ClientIDMode="Static" MaxLength="20" CssClass="form-control" placeholder="Ingrese el código" />
                                        </div>
                                        <div class="form-group">
                                            <label for="txtDescripcion">Descripción:</label>
                                            <asp:TextBox ID="txtDescripcion" runat="server" ClientIDMode="Static" MaxLength="50" CssClass="form-control" placeholder="Ingrese la descripción" />
                                        </div>
                                        <div class="form-group">
                                            <label for="ddlModulo">Modulo:</label>
                                            <asp:DropDownList ID="ddlModulo" runat="server" ClientIDMode="Static" CssClass="form-control" />
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
                    <div class="tab-pane fade in" id="tabMenu">
                        <div class="col-lg-3">
                            <div class="block h4"><strong>Menú</strong></div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="box box box-warning">
                                    <div class="box-body">
                                        <div id="tree">
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
        <div class="col-md-12">
            <div class="box box-warning">
                <div class="box-header">
                    <div class="btn-group">
                        <button title="Grabar" class="btnGrabar btn btn-default btn-sm checkbox-toggle"><i class="fa fa-floppy-o"></i></button>
                        <button title="Cancelar" class="btnCancelar btn btn-default btn-sm"><i class="fa fa-arrow-left"></i></button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdIdModulo" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdIdPerfil" runat="server" ClientIDMode="Static" />
</asp:Content>




