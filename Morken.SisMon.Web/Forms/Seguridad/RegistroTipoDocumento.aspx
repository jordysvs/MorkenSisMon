<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="RegistroTipoDocumento.aspx.cs" Inherits="Forms_Configuracion_RegistroTipoDocumento" %>

<%@ Register TagPrefix="cc" Namespace="Kruma.Core.Util.Web.Combine.Controls" Assembly="Kruma.Core.Util.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <cc:ScriptCombine ID="ScriptCombiner" runat="server" ScriptHandler="~/Ashx/Combine/ScriptHandler.ashx">
        <Scripts>
            <cc:ScriptReference Path="~/Script/Base/jquery.validate/jquery.validate.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.validate/additional-methods.js" />
            <cc:ScriptReference Path="~/Script/Forms/Seguridad/RegistroTipoDocumento.js" />
        </Scripts>
    </cc:ScriptCombine>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <div class="box box-warning">
                <div class="box-header">
                    <div class="btn-group">
                        <button id="btnGrabar" title="Grabar" class="btnGrabar btn btn-default btn-sm checkbox-toggle"><i class="fa fa-floppy-o"></i></button>
                        <button id="btnCancelar" title="Cancelar" class="btnCancelar btn btn-default btn-sm"><i class="fa fa-arrow-left"></i></button>
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
                        <label for="txtCodigo">Código:</label>
                        <asp:TextBox ID="txtCodigo" runat="server" ClientIDMode="Static" MaxLength="20" CssClass="form-control" placeholder="Ingrese el código" />
                    </div>
                    <div class="form-group">
                        <label for="txtDescripcion">Descripción:</label>
                        <asp:TextBox ID="txtDescripcion" runat="server" ClientIDMode="Static" MaxLength="50" CssClass="form-control" placeholder="Ingrese el nombre" />
                    </div>
                    <div class="form-group">
                        <label for="ddlEstado">Estado:</label>
                        <asp:DropDownList ID="ddlEstado" runat="server" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdIdTipoDocumento" runat="server" ClientIDMode="Static" />
    <div id="vsSummary_dialog" style="display: none">
        <asp:ValidationSummary ID="vsSummary" runat="server" />
    </div>
</asp:Content>




