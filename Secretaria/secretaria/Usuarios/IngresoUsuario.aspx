<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IngresoUsuario.aspx.cs" Inherits="secretaria.Usuarios.IngresoUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Ingreso de Usuario</h2>
    <div class="jumbotron">
        <div class="form-horizontal">
             <div class="form-group">
                
                <label class="col-sm-2">
                    <span style="font-size: medium">Nombre</span>: 
                </label>
                <div class="col-sm-3">
                    <asp:TextBox  ID="txtNombre" CssClass="form-control input" runat="server"  Enabled="true" ></asp:TextBox>
                </div>
                <label class="col-sm-2">
                    <span style="font-size: medium">Usuario</span>: 
                </label>
                <div class="col-sm-3">
                    <asp:TextBox  ID="txtUsuario" CssClass="form-control input" runat="server"   ></asp:TextBox>
                </div>
                 
            </div>
            <div class="form-group">
                
                <label class="col-sm-2">
                    <span style="font-size: medium">Contraseña</span>: 
                </label>
                <div class="col-sm-3">
                    <asp:TextBox  ID="txtcontra" TextMode="Password" CssClass="form-control input" runat="server"   ></asp:TextBox>
                </div>
                <label class="col-sm-2">
                    <span style="font-size: medium">Tipo Usuario</span>: 
                </label>
                <div class="col-sm-3">
                    <asp:DropDownList  ID="ddlTipoUsuario" CssClass="dropdown input-sm" Width="100%"  runat="server"  Enabled="true" ></asp:DropDownList>
                </div>
                 
            </div>
            <div class="form-group">
                <div class="col-sm-4"></div>
                <div class="col-sm-3">
                    <asp:Button ID="btnGuardar" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" runat="server" Width="100%" />
                </div>
            </div>
            <asp:Label ID="lblResultado" runat="server" Visible="false" />
        </div>
    </div>
</asp:Content>