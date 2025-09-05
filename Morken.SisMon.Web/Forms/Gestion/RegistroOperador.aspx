<%@ Page Language="C#"  MasterPageFile="~/Master/Default.master"  AutoEventWireup="true" CodeFile="RegistroOperador.aspx.cs" Inherits="Forms_Gestion_RegistroOperador" %>

<%@ Register TagPrefix="cc" Namespace="Kruma.Core.Util.Web.Combine.Controls" Assembly="Kruma.Core.Util.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <cc:ScriptCombine ID="ScriptCombiner" runat="server" ScriptHandler="~/Ashx/Combine/ScriptHandler.ashx" Combine="false">
        <Scripts>
            <cc:ScriptReference Path="~/Script/Base/jquery.validate/jquery.validate.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.validate/additional-methods.js" />
            <cc:ScriptReference Path="~/Script/Forms/FileServer/FileServer.js" />
            <cc:ScriptReference Path="~/Script/Forms/Gestion/RegistroOperador.js" />
        </Scripts>
    </cc:ScriptCombine>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
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
    <div class="row">
        <div class="col-xs-12">
            <div class="box box box-warning">
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label for="txtNombre">Nombre:</label>
                                <asp:TextBox ID="txtNombre" runat="server" ClientIDMode="Static" MaxLength="50" CssClass="form-control" placeholder="Ingrese el nombre" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label for="txtApellidoPaterno">Apellido Paterno:</label>
                                <asp:TextBox ID="txtApellidoPaterno" runat="server" ClientIDMode="Static" MaxLength="50" CssClass="form-control" placeholder="Ingrese el apellido paterno" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label for="txtApellidoMaterno">Apellido Materno:</label>
                                <asp:TextBox ID="txtApellidoMaterno" runat="server" ClientIDMode="Static" MaxLength="50" CssClass="form-control" placeholder="Ingrese el apellido materno" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label for="txtCorreo">Correo:</label>
                                <asp:TextBox ID="txtCorreo" runat="server" ClientIDMode="Static" MaxLength="50" CssClass="form-control" placeholder="Ingrese el correo" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label for="ddlTipoDocumento">Tipo Documento:</label>
                                <asp:DropDownList ID="ddlTipoDocumento" runat="server" ClientIDMode="Static" CssClass="form-control" >
                                    <asp:ListItem Text="--Seleccione--" Value="" />
                                    <asp:ListItem Text="DNI" Value="1" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label for="txtNroDocumento">Nro Documento:</label>
                                <asp:TextBox ID="txtNroDocumento" runat="server" CssClass="form-control soloNumeros" ClientIDMode="Static" MaxLength="8" placeholder="Ingrese el número de documento" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label for="txtCelular">Celular:</label>
                                <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control soloNumeros" ClientIDMode="Static" MaxLength="20" placeholder="Ingrese el celular" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label for="ddlEstado">Estado:</label>
                                <asp:DropDownList ID="ddlEstado" runat="server" ClientIDMode="Static" CssClass="form-control" />
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
                        <button title="Cancelar" class="btnCancelar btn btn-default btn-sm"><i class="fa fa-arrow-left"></i></button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdIdPersona" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdIdOperador" runat="server" ClientIDMode="Static" />
</asp:Content>