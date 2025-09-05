<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="Menu.aspx.cs" Inherits="Forms_Seguridad_Menu" %>

<%@ Register TagPrefix="cc" Namespace="Kruma.Core.Util.Web.Combine.Controls" Assembly="Kruma.Core.Util.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <cc:ScriptCombine ID="ScriptCombiner" runat="server" ScriptHandler="~/Ashx/Combine/ScriptHandler.ashx">
        <Scripts>
            <cc:ScriptReference Path="~/Script/Base/jquery.validate/jquery.validate.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.validate/additional-methods.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.ui/jquery.ui.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.fancytree/jquery.fancytree.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.fancytree/jquery.fancytree.dnd.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.fancytree/jquery.fancytree.edit.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.fancytree/jquery.fancytree.glyph.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.fancytree/jquery.fancytree.table.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.fancytree/jquery.fancytree.wide.js" />
            <cc:ScriptReference Path="~/Script/Forms/FileServer/FileServer.js" />
            <cc:ScriptReference Path="~/Script/Forms/Seguridad/Menu.js" />
        </Scripts>
    </cc:ScriptCombine>
    <cc:CSSCombine ID="CSSCombiner" runat="server" CSSHandler="~/Ashx/Combine/CSSHandler.ashx">
        <CSSReferences>
            <cc:CSSReference Path="~/Style/Base/jquery.fancytree/jquery.fancytree.css" />
            <cc:CSSReference Path="~/Style/Forms/Seguridad/Menu.css" />
        </CSSReferences>
    </cc:CSSCombine>
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
                                <label for="ddlModulo">Módulo:</label>
                                <asp:DropDownList ID="ddlModulo" runat="server" CssClass="form-control" ClientIDMode="Static" />
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="txtDescripcion">Descripción:</label>
                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="20" placeholder="Ingrese la descripción" />
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="txtURL">URL:</label>
                                <asp:TextBox ID="txtURL" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="500" placeholder="Ingrese la URL" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="ddlVisible">Visible:</label>
                                <asp:DropDownList ID="ddlVisible" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label for="ddlEstado">Estado:</label>
                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-4">
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
                    </div>
                    <!-- /.btn-group -->
                    <button id="btnBuscar" title="Buscar" class="btn btn-default btn-sm"><i class="fa fa-search"></i></button>
                    <!-- /.pull-right -->
                </div>
                <div class="box-body">

                    <table id="treetable" class="table table-condensed table-hover table-striped">
                        <colgroup>
                            <col style="width: 50%"></col>
                            <col style="width: 30%"></col>
                            <col style="width: 10%"></col>
                            <col style="width: 10%"></col>
                        </colgroup>
                        <thead>
                            <tr>
                                <th>Menú</th>
                                <th>URL</th>
                                <th>Visible</th>
                                <th>Estado</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade colored-header danger" id="mdGrupo" tabindex="-1" style="display: none;">
        <div class="modal-dialog">
            <div id="divGrupoModal" class="modal-content">
                <div class="modal-header">
                    <h3>Grupo</h3>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <label for="txtCodigoGrupo">Código:</label>
                                    <asp:TextBox ID="txtCodigoGrupo" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="8" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <label for="txtDescripcionGrupo">Descripción:</label>
                                    <asp:TextBox ID="txtDescripcionGrupo" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="100" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <div class="user-header" style="text-align: center;">
                                            <asp:Image CssClass="user-image" Style="max-height: 128px;" alt="" ID="imgImagenGrupo" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label for="fileUploadGrupo">Foto:</label>
                                        <input id="fileUploadGrupo" name="fileUploadGrupo" type="file" />
                                        <a id="lnkEliminarFotoGrupo" runat="server" href="#">Eliminar Foto</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <label for="ddlEstadoGrupo">Estado:</label>
                                    <asp:DropDownList ID="ddlEstadoGrupo" runat="server" ClientIDMode="Static" CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                        <div id="divAuditoriaGrupo" class="row auditoria">
                            <div class="col-xs-12">
                                <div class="box box box-warning">
                                    <div class="box-body">
                                        <div class="row">
                                            <div class="col-xs-6">
                                                <div class="dl-horizontal">
                                                    <span class="label">Usuario de Creación:</span>
                                                    <span class="field" id="ddUsuarioCreacionGrupo"></span>
                                                </div>
                                            </div>
                                            <div class="col-xs-6">
                                                <div class="dl-horizontal">
                                                    <span class="label">Fecha de Creación:</span>
                                                    <span class="field" id="ddFechaCreacionGrupo"></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-6">
                                                <div class="dl-horizontal">
                                                    <span class="label">Usuario de Modificación:</span>
                                                    <span class="field" id="ddUsuarioModificacionGrupo"></span>
                                                </div>
                                            </div>
                                            <div class="col-xs-6">
                                                <div class="dl-horizontal">
                                                    <span class="label">Fecha de Modificación:</span>
                                                    <span class="field" id="ddFechaModificacionGrupo"></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger btn-flat btn-aceptar">Aceptar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade colored-header danger" id="mdDetalle" tabindex="-1" style="display: none;">
        <div class="modal-dialog">
            <div id="divGrupoDetalleModal" class="modal-content">
                <div class="modal-header">
                    <h3>Rechazar Trámite</h3>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <label for="txtCodigoDetalle">Código:</label>
                                    <asp:TextBox ID="txtCodigoDetalle" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="8" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <label for="txtDescripcionDetalle">Descripción:</label>
                                    <asp:TextBox ID="txtDescripcionDetalle" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="50" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <label for="txtUrlDetalle">Url:</label>
                                    <asp:TextBox ID="txtUrlDetalle" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="500" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <div class="checkbox">
                                        <label>
                                            <input type="checkbox" id="chkVisibleDetalle" />Visible
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <div class="user-header" style="text-align: center;">
                                            <asp:Image CssClass="user-image" Style="max-height: 128px;" alt="" ID="imgImagenDetalle" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label for="fileUploadDetalle">Foto:</label>
                                        <input id="fileUploadDetalle" name="fileUploadDetalle" type="file" />
                                        <a id="lnkEliminarFotoDetalle" runat="server" href="#">Eliminar Foto</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <label for="ddlEstadoDetalle">Estado:</label>
                                    <asp:DropDownList ID="ddlEstadoDetalle" runat="server" ClientIDMode="Static" CssClass="form-control" />
                                </div>
                            </div>
                        </div>

                        <div id="divAuditoriaDetalle" class="row auditoria">
                            <div class="col-xs-12">
                                <div class="box box box-warning">
                                    <div class="box-body">
                                        <div class="row">
                                            <div class="col-xs-6">
                                                <div class="dl-horizontal">
                                                    <span class="label">Usuario de Creación:</span>
                                                    <span class="field" id="ddUsuarioCreacionDetalle"></span>
                                                </div>
                                            </div>
                                            <div class="col-xs-6">
                                                <div class="dl-horizontal">
                                                    <span class="label">Fecha de Creación:</span>
                                                    <span class="field" id="ddFechaCreacionDetalle"></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-6">
                                                <div class="dl-horizontal">
                                                    <span class="label">Usuario de Modificación:</span>
                                                    <span class="field" id="ddUsuarioModificacionDetalle"></span>
                                                </div>
                                            </div>
                                            <div class="col-xs-6">
                                                <div class="dl-horizontal">
                                                    <span class="label">Fecha de Modificación:</span>
                                                    <span class="field" id="ddFechaModificacionDetalle"></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger btn-flat btn-aceptar">Aceptar</button>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdIdModulo" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdIdGrupo" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdIdDetalle" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdIdDetallePadre" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdOrden" runat="server" ClientIDMode="Static" />
    <!--FileServer-->
    <asp:HiddenField ID="hdIdAlmacen" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdIdImagenGrupo" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdIdImagenDetalle" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdIdDocumento" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdImagenMenu" runat="server" ClientIDMode="Static" />

</asp:Content>



