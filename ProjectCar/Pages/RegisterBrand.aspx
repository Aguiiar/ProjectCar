<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterBrand.aspx.cs" Inherits="ProjectCar.Pages.RegisterBrand" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div>


        <h2>Cadastrar Marca</h2>
        <asp:Label runat="server" Text="Marca " AssociatedControlID="txtName" class="form-label fw-semibold" />
        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
        <asp:Button Text="Cadastrar" runat="server" OnClick="Register" CssClass="btn btn-outline-secondary btn-lg mt-4 mb-2" />
        <div class="col-12 ps-0">
            <asp:Label ID="lblMensagem" runat="server" ForeColor="Green" />
        </div>
    </div>
</asp:Content>
