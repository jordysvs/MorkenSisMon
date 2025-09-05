<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="Contrasenia.aspx.cs" Inherits="Forms_Seguridad_Contrasenia" %>

<%@ Register TagPrefix="cc" Namespace="Kruma.Core.Util.Web.Combine.Controls" Assembly="Kruma.Core.Util.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <cc:ScriptCombine ID="ScriptCombiner" runat="server" ScriptHandler="~/Ashx/Combine/ScriptHandler.ashx">
        <Scripts>
            <cc:ScriptReference Path="~/Script/Base/jquery.validate/jquery.validate.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.validate/additional-methods.js" />
            <cc:ScriptReference Path="~/Script/Forms/Seguridad/Contrasenia.js" />
        </Scripts>
    </cc:ScriptCombine>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <asp:Panel ID="pnlContrasenia" runat="server">
        <div class="row">
            <div class="col-md-6">
                <div class="box box-warning">
                    <div class="box-header">
                        <div class="btn-group">
                            <button id="btnGrabar" title="Grabar" class="btnGrabar btn btn-default btn-sm checkbox-toggle"><i class="fa fa-floppy-o"></i></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <!-- general form elements -->
                <div class="box box box-warning">
                    <div class="box-body">
                        <div class="form-group">
                            <label for="txtContrasenia">Nueva contraseña:</label>
                            <asp:TextBox ID="txtContrasenia" runat="server" TextMode="Password" ClientIDMode="Static" MaxLength="50" CssClass="form-control" placeholder="Ingrese la contraseña" />
                        </div>
                        <div class="form-group">
                            <label for="txtRepetirContrasenia">Repetir contraseña:</label>
                            <asp:TextBox ID="txtRepetirContrasenia" runat="server" TextMode="Password" ClientIDMode="Static" MaxLength="50" CssClass="form-control" placeholder="Ingrese nuevamente la contraseña" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <div id="divMensaje" runat="server" visible="false" class="row">
        <div class="col-md-6">
            <div class="box box-warning">
                <div class="box-header">
                    <div class="btn-group">
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdIdUsuario" runat="server" ClientIDMode="Static" />
</asp:Content>








