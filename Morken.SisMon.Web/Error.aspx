<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <!-- general form elements -->
            <div class="box box box-warning">
                <div class="box-body">
                    <div class="form-group">
                        <label></label>
                        <asp:Label ID="lblExMessage" runat="server" CssClass="mensajeError" />
                    </div>
                    <div class="form-group">
                        <label></label>
                        <asp:Panel ID="pnlExErrorPanel" runat="server" Visible="false">
                            <pre style="text-align: left;">
                                <asp:Label ID="lblExTrace" runat="server"/>
                                </pre>
                        </asp:Panel>
                    </div>
                    <div class="form-group">
                        <label></label>
                        <asp:Panel ID="pnlInnerErrorPanel" runat="server" Visible="false">
                            <p>
                                <b>Inner Error Message:</b><br />
                                <asp:Label ID="lblInnerMessage" runat="server" CssClass="mensajeError" /><br />
                            </p>
                            <pre style="text-align: left;">
                                        <asp:Label ID="lblInnerTrace" runat="server" />
                                        </pre>
                        </asp:Panel>
                    </div>
                    <div class="form-group">
                        <label></label>
                        <p>Regrese a la <a href='<%= Page.ResolveClientUrl("~/Index.aspx") %>'>Página Principal</a></p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
