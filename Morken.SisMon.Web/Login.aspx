<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Login.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register TagPrefix="cc" Namespace="Kruma.Core.Util.Web.Combine.Controls" Assembly="Kruma.Core.Util.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <cc:ScriptCombine ID="ScriptCombiner" runat="server" ScriptHandler="~/Ashx/Combine/ScriptHandler.ashx">
        <Scripts>
            <cc:ScriptReference Path="~/Script/Forms/Login.js" />
        </Scripts>
    </cc:ScriptCombine>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <fieldset>
        <p>
            <label for="login-username">Usuario</label>
            <input id="txtUsuario" runat="server" type="text" class="round full-width-input" autofocus />
        </p>
        <p>
            <label for="login-password">Contraseña</label>
            <input id="txtPassword" runat="server" type="password" class="round full-width-input" />
        </p>
        <p>
            Yo
            <asp:LinkButton ID="lbkContrasenia" runat="server" ClientIDMode="Static" OnClientClick="MostrarMensajeCargando();" OnClick="lbkContrasenia_Click">olvidé mi contraseña</asp:LinkButton>.
        </p>
        <asp:LinkButton ID="lkbIniciar" runat="server" ClientIDMode="Static" CssClass="button round blue image-right ic-right-arrow" OnClientClick="MostrarMensajeCargando();" OnClick="lkbIniciar_Click">INICIAR</asp:LinkButton>
    </fieldset>
    <br />
    <div class="information-box round">Ingrese su usuario y contraseña y haga clic en el botón "INICIAR" para continuar.</div>
    <div id="divMensajeError" runat="server" visible="false" class="error-box round"></div>
</asp:Content>


