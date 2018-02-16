<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificarUsuario.aspx.cs" Inherits="secretaria.Usuarios.ModificarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Listado Usuarios</h2>
    <div class="jumbotron">
        <div class="form-horizontal">
            <div class="form-group">
            </div>
            <div class="form-group">
                <label class="col-sm-2"><span style="font-size: medium">Usuario</span>: </label>
                <div class="col-sm-4">
                    <asp:Label ID="lblUsuario" Text="" runat="server"></asp:Label>
                </div>
                <label class="col-sm-1">
                    <span style="font-size: medium">Nombre</span>: 
                </label>
                <div class="col-sm-4">
                    <asp:TextBox ID="txtNombre" CssClass="form-control input" runat="server" Enabled="true"></asp:TextBox>
                </div>

            </div>
            <div class="form-group">
                <label class="col-sm-2"><span style="font-size: medium">Contraseña</span>: </label>
                <div class="col-sm-4">
                    <asp:TextBox ID="txtContraseña" TextMode="Password" CssClass="form-control input" runat="server"></asp:TextBox>
                </div>
                <label class="col-sm-1">
                    <span style="font-size: medium">Confirmar</span>: 
                </label>
                <div class="col-sm-4">
                    <asp:TextBox ID="txtContraseñaConfir" TextMode="Password" CssClass="form-control input" runat="server" Enabled="true"></asp:TextBox>
                </div>

            </div>
            <div class="form-group">
                <label class="col-sm-2"><span style="font-size: medium">Estado</span>: </label>
                <div class="col-sm-4">
                    <asp:DropDownList ID="ddlEstado" CssClass="form-control input" runat="server"></asp:DropDownList>
                </div>
                <label class="col-sm-1"><span style="font-size: medium">Tipo</span>: </label>
                <div class="col-sm-4">
                    <asp:DropDownList ID="ddlTipoUsuario" CssClass="form-control input" runat="server"></asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-4">
                </div>
                <div class="col-md-4">
                    <asp:Button ID="btnGuardar" Text="Guardar" OnClick="btnGuardar_Click" CssClass="btn btn-success"  runat="server" Width="100%" />
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblResultado" runat ="server" Visible="false" ForeColor="Red" style="font-size: medium"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
