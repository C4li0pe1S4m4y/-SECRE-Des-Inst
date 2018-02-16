<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoAsambleas.aspx.cs" Inherits="secretaria.Asambleas.ListadoAsambleas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Listado de Asambleas</h2>
    <asp:GridView ID="gvListadoA" runat="server" OnSelectedIndexChanged="gvListadoA_SelectedIndexChanged" AllowPaging="True" autogeneratecolumns="false" DataKeyNames="numero" CssClass="table table-hover table-responsive">
    <AlternatingRowStyle BackColor="#F0F0F0" />
        <Columns>

                <asp:CommandField ButtonType="Button" HeaderText="Seleccionar"  ControlStyle-CssClass="btn btn-primary" ShowSelectButton="True">
                    <HeaderStyle BorderStyle="Inset"  HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle BorderStyle="Inset" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:CommandField>
                                
                <asp:boundfield datafield="descripcion" headertext="Descripción"/>
                <asp:boundfield datafield="fecha" headertext="Fecha"/>
                <asp:boundfield datafield="tipoAsamblea" headertext="Tipo de Asamblea"/>
                <asp:boundfield datafield="estado" headertext="Estado"/>
            
        </Columns>
        <HeaderStyle BackColor="#0099FF" />
    </asp:GridView>
</asp:Content>