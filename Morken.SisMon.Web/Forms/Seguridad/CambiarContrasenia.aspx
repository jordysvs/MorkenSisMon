<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="CambiarContrasenia.aspx.cs" Inherits="Forms_Seguridad_CambiarContrasenia" %>

<%@ Register TagPrefix="cc" Namespace="Kruma.Core.Util.Web.Combine.Controls" Assembly="Kruma.Core.Util.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <cc:ScriptCombine ID="ScriptCombiner" runat="server" ScriptHandler="~/Ashx/Combine/ScriptHandler.ashx">
        <Scripts>
            <cc:ScriptReference Path="~/Script/Base/jquery.validate/jquery.validate.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.validate/additional-methods.js" />
            <cc:ScriptReference Path="~/Script/Forms/Seguridad/CambiarContrasenia.js" />
        </Scripts>
    </cc:ScriptCombine>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="box box-warning">
                <div class="box-header">
                    <div class="btn-group">
                        <button id="btnValidar" title="Validar" class="btnValidar btn btn-default btn-sm"><i class="fa fa-unlock-alt"></i></button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="box box box-warning">
                <div class="box-body">
                    <div class="form-group">
                        <label for="txtCodigo">Código de usuario:</label>
                        <asp:TextBox ID="txtCodigo" runat="server" ClientIDMode="Static" MaxLength="20" CssClass="form-control" placeholder="Ingrese el código" />
                    </div>
                    <div class="form-group">
                        <label for="txtCorreo">Correo del usuario:</label>
                        <asp:TextBox ID="txtCorreo" runat="server" ClientIDMode="Static" MaxLength="50" CssClass="form-control" placeholder="Ingrese el correo" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>










