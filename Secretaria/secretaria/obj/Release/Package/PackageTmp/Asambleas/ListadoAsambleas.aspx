<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoAsambleas.aspx.cs" Inherits="secretaria.Asambleas.ListadoAsambleas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Listado de Asambleas</h2>
    <asp:GridView ID="gvListadoA" runat="server" OnSelectedIndexChanged="gvListadoA_SelectedIndexChanged" AllowPaging="False" autogeneratecolumns="false" DataKeyNames="numero" CssClass="table table-hover table-responsive" onrowcommand="opcionesAsamblea_RowCommand" OnRowDataBound="opcionesAsamblea_RowDataBound">  
        <AlternatingRowStyle BackColor="#F0F0F0" />
        <Columns>
                <asp:boundfield datafield="numero" headertext="idAsamblea"/>              
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="btIngresar" runat="server" ControlStyle-CssClass="btn btn-info" CausesValidation="false" 
                            CommandName="Ingresar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            Text="Ingresar" />
                            
                        <asp:Button ID="btListado" runat="server" ControlStyle-CssClass="btn btn-success" CausesValidation="false" 
                            CommandName="Listado" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            Text="Listado" />
                            
                    </ItemTemplate>
                    <ItemStyle Width="18%"/>
                </asp:TemplateField>
                                
                <asp:boundfield datafield="descripcion" headertext="Descripción"/>
                <asp:boundfield datafield="fecha" headertext="Fecha"/>
                <asp:boundfield datafield="tipoAsamblea" headertext="Tipo de Asamblea"/>
                <asp:boundfield datafield="estado" headertext="Estado"/>
            
        </Columns>
        <HeaderStyle BackColor="#0099FF" />
    </asp:GridView>
</asp:Content>