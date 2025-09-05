<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<%@ Register TagPrefix="cc" Namespace="Kruma.Core.Util.Web.Combine.Controls" Assembly="Kruma.Core.Util.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <cc:ScriptCombine ID="ScriptCombiner" runat="server" ScriptHandler="~/Ashx/Combine/ScriptHandler.ashx">
        <Scripts>
            <cc:ScriptReference Path="~/Script/Forms/Index.js" />
        </Scripts>
    </cc:ScriptCombine>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">

    <div class="row">
        <div class="col-xs-12">
            <div class="box box-warning" style="background-color: transparent!important; box-shadow: none!important;">
                <div class="box-body" style="text-align: center;">
                    <br />
                    <br />
                    <br />
                    <br />
                    <img src="<%= Page.ResolveClientUrl("~/Images/Base/logoCliente.png")%>" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

