<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DirigentesCompletos.aspx.cs" Inherits="secretaria.ReportesSistema.DirigentesCompletos" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h2>Listado de Dirigentes </h2>
    <div class="panel-default">
        <div class="row" style="margin-top: 10px">

            <div class="col-xs-4">
                <asp:Label ID="Label1" runat="server" Text="Seleccionar FADN"></asp:Label>
                <asp:DropDownList ID="ddl_fadn" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddl_fadn_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-xs-4">
                <asp:Label ID="Label4" runat="server" Text="Seleccionar Dirigente"></asp:Label>
                <asp:DropDownList ID="ddl_dirigente" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddl_dirigente_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <div class="row" style="margin-top: 10px">
            <div class="col-xs-4">
                <asp:Label ID="Label2" runat="server" Text="Fecha Inicio"></asp:Label>
                <asp:TextBox ID="tb_fecha_inicio" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>
            <div class="col-xs-4">
                <asp:Label ID="Label3" runat="server" Text="Fecha Fin"></asp:Label>
                <asp:TextBox ID="tb_fecha_fin" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>
        </div>
        <div class="row" style="margin-top: 10px">
            <div class="col-xs-8">
                <asp:CheckBoxList ID="cblFiltros" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="cblFiltros_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="1">Acuerdo </asp:ListItem>
                    <asp:ListItem Selected="True" Value="2">Acreditacion</asp:ListItem>
                    <asp:ListItem Selected="True" Value="3">Finiquito</asp:ListItem>
                    <asp:ListItem Selected="True" Value="4">Acta de Posesión</asp:ListItem>
                    <asp:ListItem Selected="True" Value="5">DPI</asp:ListItem>
                    <asp:ListItem Selected="True" Value="6">NIT</asp:ListItem>
                </asp:CheckBoxList>
            </div>
        </div>
        <div class="row" style="margin-top: 10px">
            <div class="col-xs-4">

                <asp:Button ID="btnBusqueda" runat="server" Text="Buscar Por Fecha" CssClass="btn btn-info" OnClick="btnBusqueda_Click" Style="margin-left: 26" />
            </div>
        </div>
        <div class="row">
            <div style="width: 100%; height: 100%;">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="100%">
                    <LocalReport ReportPath="Reportes\RPDirigentesActivos.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dbsecretariaConnectionString2 %>" ProviderName="<%$ ConnectionStrings:dbsecretariaConnectionString2.ProviderName %>" SelectCommand="SELECT f.nombre, d.Nombres, d.Apellidos AS Nombres, d.Estado, t.descripcion, c.Fecha_inicio, c.Fecha_final, c.Acuerdo_cej, c.Fecha_acuerdo, c.Acreditacion_cdag, c.Fecha_Acreditacion, c.no_finiquito, c.fecha_finiquito, c.acta_posesion, c.fecha_posesion, c.fecha_recepcion, d.DPI, d.NIT FROM sg_comite_ejecutivo c INNER JOIN sg_dirigente d ON c.id_dirigente = d.idDirigente INNER JOIN sg_fadn f ON f.id_fand = c.id_fadn INNER JOIN sg_tipo_dirigente t ON t.idTipo_dirigente = d.Tipo_dirigente WHERE (c.Estado_Comite > 0) ORDER BY f.nombre, d.Tipo_dirigente"></asp:SqlDataSource>
            </div>

        </div>
    </div>
</asp:Content>
