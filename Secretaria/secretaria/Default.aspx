<%@ Page Title="Home Page"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="secretaria._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        
        <h2>¡Bienvenido! <asp:Label ID="lblUsuario" runat="server"></asp:Label> </h2> 
        <p>Sistema para el manejo de nombramientos a federaciones, para la Secretaria General.</p>
    </div>

</asp:Content>
