<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CarManagement.aspx.cs" Inherits="ProjectCar.Pages.CarManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="col-12">
    <div class="row">
      <div class="col-12 col-md-4 lh-lg">
        <h2 class="h1">Cadastrar Carro</h2>
        <asp:Label Text="Modelo" runat="server" AssociatedControlID="txtModel" class="form-label fw-semibold" />
        <asp:TextBox ID="txtModel" runat="server" CssClass="form-control" />
       
        <asp:Label Text="Fabricação" runat="server" AssociatedControlID="txtManufacture" class="form-label fw-semibold" />
        <asp:TextBox ID="txtManufacture" runat="server" TextMode="Date" CssClass="form-control" />
        <asp:Label Text="Potência" runat="server" AssociatedControlID="txtPower" class="form-label fw-semibold" />
        <asp:TextBox ID="txtPower" runat="server" CssClass="form-control" />
        <label class="fw-semibold">Turbo</label>
        <div class="form-check ps-0">
          <asp:RadioButton ID="rbtnYes" GroupName="Turbo" runat="server" />
          <asp:Label runat="server" AssociatedControlID="rbtnYes" CssClass="form-check-label fw-semibold">Sim</asp:Label>
        </div>
        <div class="form-check ps-0">
          <asp:RadioButton ID="rbtnNo" GroupName="Turbo" runat="server" />
          <asp:Label runat="server" AssociatedControlID="rbtnNo" class="form-check-label fw-semibold">Não</asp:Label>
        </div>
        <asp:Label Text="Marcas" runat="server" AssociatedControlID="ddlBrands" CssClass="fw-semibold" />
        <asp:DropDownList ID="ddlBrands" runat="server" CssClass="form-select form-select" />
        <asp:Button Text="Cadastrar" runat="server" OnClick="RegisterEditCar" CssClass="btn btn-outline-secondary btn-lg mt-4 mb-2" />
        <div>
          <asp:Label ID="lblMensagem" runat="server" ForeColor="Green" />
        </div>
      </div>
      
    
      <div class="col-sm-12 col-md-8">
      <asp:Panel ID="tableCars" runat="server" Visible="false">
       <div class="table-responsive">
          <table class="table table-striped">
            <thead>
              <tr>
                <th scope="col">Id</th>
                <th scope="col">Modelo</th>
                <th scope="col">Fabricação</th>
                <th scope="col">Potência</th>
                <th scope="col">Turbo</th>
                <th scope="col">Marca</th>
              </tr>
            </thead>
            <tbody>
<asp:Repeater ID="repeaterCars" runat="server" OnItemCommand="EditDelete">
    <ItemTemplate>
       
                  <tr>
                    <td><%# Eval("Id") %></td>
                    <td><%# Eval("Model") %></td>
                    <td><%# Eval("Manufacture", "{0:dd/MM/yyyy}") %></td>
                    <td><%# Eval("Power") %></td>
                    <td><%# Eval("TurboTexto") %></td>
                    <td><%# Eval("MarcaNome") %></td>
                      <td>
                          <asp:Button ID="btnDelete" runat="server" Text="Excluir" CommandName="Deletar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('Deseja mesmo deletar?');" />
                      </td>
                      <td>
                           <asp:Button runat="server" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' Text="Editar" CssClass="btn btn-warning btn-sm" />
                      </td>
                  </tr>
                </ItemTemplate>
              </asp:Repeater>
            </tbody>
          </table>
           </div>
          </asp:Panel>
        </div>

    </div>
  </div>
</asp:Content>
