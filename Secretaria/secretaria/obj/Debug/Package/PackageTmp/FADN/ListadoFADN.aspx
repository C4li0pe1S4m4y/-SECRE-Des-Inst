<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoFADN.aspx.cs" Inherits="secretaria.FADN.ListadoFADN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Listado FADN</h2>
    <asp:GridView ID="gvListado" runat="server" OnPageIndexChanging="gvListado_PageIndexChanged1" OnSelectedIndexChanged="gvListado_SelectedIndexChanged" AllowPaging="True" DataKeyNames="numero" CssClass="table table-hover table-responsive" BorderColor="Black">
        <AlternatingRowStyle BackColor="#F0F0F0" />
        <Columns>

            <asp:CommandField ButtonType="Button" HeaderText="Seleccionar"  ControlStyle-CssClass="btn btn-primary" ShowSelectButton="True">
                <ControlStyle CssClass="btn btn-primary "></ControlStyle>

                <HeaderStyle BorderStyle="Inset" HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle BorderStyle="Inset" HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:CommandField>


        </Columns>
        <HeaderStyle BackColor="#0099FF" />
    </asp:GridView>
</asp:Content>
