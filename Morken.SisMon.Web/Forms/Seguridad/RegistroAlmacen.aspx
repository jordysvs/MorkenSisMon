<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="RegistroAlmacen.aspx.cs" Inherits="Forms_Seguridad_RegistroAlmacen" %>

<%@ Register TagPrefix="cc" Namespace="Kruma.Core.Util.Web.Combine.Controls" Assembly="Kruma.Core.Util.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <cc:ScriptCombine ID="ScriptCombiner" runat="server" ScriptHandler="~/Ashx/Combine/ScriptHandler.ashx">
        <Scripts>
            <cc:ScriptReference Path="~/Script/Base/jquery.validate/jquery.validate.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.validate/additional-methods.js" />
            <cc:ScriptReference Path="~/Script/Forms/Seguridad/RegistroAlmacen.js" />
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
                    <div class="form-group">
                        <label for="txtCodigo">Descripción:</label>
                        <asp:TextBox ID="txtDescripcion" runat="server" ClientIDMode="Static" MaxLength="50" CssClass="form-control" placeholder="Ingrese la descripción" />
                    </div>
                    <div class="form-group">
                        <label for="txtNombre">Ruta:</label>
                        <asp:TextBox ID="txtRuta" runat="server" ClientIDMode="Static" MaxLength="200" CssClass="form-control" placeholder="Ingrese la ruta" />
                    </div>
                    <div class="form-group">
                        <label for="ddlTipoAlmacen">Tipo Almacen:</label>
                        <asp:DropDownList ID="ddlTipoAlmacen" runat="server" CssClass="form-control" ClientIDMode="Static">
                            <asp:ListItem Text="Carpeta" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Ftp" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="txtUsuario">Usuario:</label>
                        <asp:TextBox ID="txtUsuario" runat="server" ClientIDMode="Static" MaxLength="50" CssClass="form-control" placeholder="Ingrese el usuario" />
                    </div>
                    <div class="form-group">
                        <label for="txtClave">Clave:</label>
                        <asp:TextBox ID="txtClave" runat="server" ClientIDMode="Static" MaxLength="50" CssClass="form-control" placeholder="Ingrese la clave" />
                    </div>
                    <div class="form-group">
                        <label for="txtDominio">Dominio:</label>
                        <asp:TextBox ID="txtDominio" runat="server" ClientIDMode="Static" MaxLength="50" CssClass="form-control" placeholder="Ingrese el dominio" />
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
    <asp:HiddenField ID="hdIdAlmacen" runat="server" ClientIDMode="Static" />
</asp:Content>




