<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DirigentesFADN.aspx.cs" Inherits="secretaria.Dirigentes.DirigentesFADN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <script type="text/javascript">
        function abrirModal(tipo) {
            $(tipo).modal('show');
        }
        function cerrarModal(tipo) {
            $(tipo).modal('hide');
        }
        function irTab(tipo) {
            $('.nav-tabs a[href="' + tipo + '"]').tab('show')
        }
    </script>
    <link href="content/bootstrap.css" rel="stylesheet" type="text/css" media="screen" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <h4>Seleccionar FADN</h4>
                <div class="row">
                    <div class="col-sm-4" style="padding:0px">
                        <asp:DropDownList ID="ddl_fadn" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-sm-2" style="padding:0px">
                        <asp:Button ID="btn_buscar" runat="server" Text="Mostrar" CssClass="btn btn-primary" OnClick="btn_buscar_Click" />
                    </div>
                </div>
                <div class="row" style="margin-top:10px">
                    <div class="col-sm-12">
                        <asp:Panel ID="panel_junta" runat="server" CssClass="panel " Visible="false">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-xs-6">
                                        <asp:Label ID="Label1" runat="server" Text="FADN" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="col-xs-6">
                                        <asp:Label ID="Label2" runat="server" Text="Correo" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <asp:Label ID="lbl_fadn" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="col-xs-6">
                                        <asp:Label ID="lbl_correo" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="row" style="margin-top:10px">
                                    <div class="col-xs-6">
                                        <asp:Label ID="Label5" runat="server" Text="Comité Ejecutivo" CssClass="h4"></asp:Label>
                                    </div>
                                </div>
                                <div class="row" style="margin-top:10px">
                                    <div class="col-sm-12">
                                        <asp:GridView ID="gv_fadn" runat="server" CssClass="table table-hover table-responsive" OnSelectedIndexChanged="gv_fadn_SelectedIndexChanged" DataKeyNames="ID">
                                            <Columns>
                                                <asp:CommandField ButtonType="Button" HeaderText="Seleccionar"  ControlStyle-CssClass="btn btn-primary" ShowSelectButton="True">
                                                    <HeaderStyle BorderStyle="Inset"  HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle BorderStyle="Inset" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:CommandField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        
                        <asp:Panel ID="panel_vacantes" runat="server" CssClass="panel panel-default" Visible="false">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <asp:Label ID="Label3" runat="server" Text="Vacantes" CssClass="h4"></asp:Label>
                                    </div>
                                </div>
                                <div class="row" style="margin-top:10px">
                                    <div class="col-xs-12">
                                        <asp:DropDownList ID="ddl_vacantes" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row" style="margin-top:10px">
                                    <div class="col-xs-12">
                                        <asp:LinkButton ID="lbtn_ingresar" runat="server" CssClass="btn btn-success" OnClick="lbtn_ingresar_Click">Ingresar
                                            <span class="glyphicon glyphicon-arrow-up" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12">
                                        <asp:Label ID="Label23" runat="server" Text="Suspendidos" CssClass="h4"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12">
                                        <asp:GridView ID="gv_suspendidos" runat="server" CssClass="table table-hover table-responsive" DataKeyNames="ID,COD" OnSelectedIndexChanged="gv_suspendidos_SelectedIndexChanged">
                                            <Columns>
                                                <asp:CommandField ButtonType="Button" HeaderText="Quitar Suspension"  ControlStyle-CssClass="btn btn-primary" ShowSelectButton="True">
                                                    <HeaderStyle BorderStyle="Inset"  HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle BorderStyle="Inset" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:CommandField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12">
                        <asp:Panel ID="panel_opciones_dirigente" runat="server" CssClass="panel panel-default" Visible="false">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <asp:Label ID="Label20" runat="server" Text="Opciones de dirigente" CssClass="h4"></asp:Label>
                                    </div>
                                </div>
                                <div class="row" style="margin-top:10px">
                                    <div class="col-xs-4">
                                        <asp:LinkButton ID="lbtn_cambiar" runat="server" CssClass="btn btn-success" OnClick="lbtn_cambiar_Click">Modificar
                                            <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-xs-4">
                                        <asp:LinkButton ID="lbtn_baja" runat="server" CssClass="btn btn-danger" OnClick="lbtn_baja_Click">Dar de baja
                                            <span class="glyphicon glyphicon-remove-circle" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row" style="margin-top:10px">
                                    <asp:Panel ID="panel_estado" runat="server" Visible="false" CssClass="panel">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <asp:Label ID="Label4" runat="server" Text="Actualizar Estado" CssClass="h4"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <asp:DropDownList ID="ddl_estado" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_estado_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-4">
                                                    <asp:Label ID="lbl_acta" runat="server" Text="Acta"></asp:Label>
                                                </div>
                                                <div class="col-xs-4">
                                                    <asp:Label ID="lbl_inicio_susp" runat="server" Text="Inicio Suspensión" Visible="false"></asp:Label>
                                                </div>
                                                <div class="col-xs-4">
                                                    <asp:Label ID="lbl_fin_susp" runat="server" Text="Fin Suspensión" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-4">
                                                    <asp:TextBox ID="tb_acta_baja" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-xs-4">
                                                    <asp:TextBox ID="tb_fecha_inicio_susp" runat="server" CssClass="form-control" TextMode="Date" Visible="false"></asp:TextBox>
                                                </div>
                                                <div class="col-xs-4">
                                                    <asp:TextBox ID="tb_fecha_fin_susp" runat="server" CssClass="form-control" TextMode="Date" Visible="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <asp:Label ID="lbl_fecha_acta_baja" runat="server" Text="Fecha Acta"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <asp:TextBox ID="tb_fecha_acta_baja" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <asp:Label ID="lbl_motivo" runat="server" Text="Motivo:" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <asp:TextBox ID="tb_motivo" runat="server" TextMode="MultiLine" Visible="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <asp:Button ID="btn_cambio_estado" runat="server" Text="Cambiar Estado" CssClass="btn btn-primary" OnClick="btn_cambio_estado_Click"/>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
     <div class="modal open" id="myModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <asp:Label ID="lbl_titulo_modal" runat="server" Text="" CssClass="h4 modal-title"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                    <ul class="nav nav-tabs" role="tablist" id="myTabs">
                        <li role="presentation" class="active"><a href="#dirigente" aria-controls="dirigente" role="tab" data-toggle="tab">Dirigente</a></li>
                        <li role="presentation"><a href="#comite" aria-controls="comite" role="tab" data-toggle="tab">Comité</a></li>
                        </ul>
                        <!-- Tab panes -->
                        <div class="tab-content">
                            <div role="tabpanel" class="tab-pane active" id="dirigente">
                                <div class="row">
                                    <div class="col-xs-6">
                                        <asp:Label ID="Label6" runat="server" Text="Nombres"></asp:Label>
                                    </div>
                                    <div class="col-xs-6">
                                        <asp:Label ID="Label7" runat="server" Text="Apellidos"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="tb_nombres" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="tb_apellidos" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <asp:Label ID="Label8" runat="server" Text="CUI (Dpi)"></asp:Label>
                                    </div>
                                    <div class="col-xs-6">
                                        <asp:Label ID="Label9" runat="server" Text="Extendido en"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="tb_dpi" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-6">
                                        <asp:DropDownList ID="ddl_dpto" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <asp:Label ID="Label10" runat="server" Text="NIT"></asp:Label>
                                    </div>
                                    <div class="col-xs-6">
                                        <asp:Label ID="Label14" runat="server" Text="Correo electrónico"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="tb_nit" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="tb_correo" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                                    </div>
                                </div>
                               
                                <div class="row">
                                    
                                </div>
                                <div class="row" style="margin-top:10px">
                                    <div class="col-xs-12">
                                        <asp:Button ID="btn_siguiente" runat="server" Text="Siguiente" CssClass="btn btn-primary" OnClick="btn_siguiente_Click" Visible="false"/>
                                    </div>
                                </div>
                                <div class="row" style="margin-top:10px">
                                    <div class="col-xs-12">
                                        <asp:Button ID="btn_actualizar_dir" runat="server" Text="Actualizar Datos" CssClass="btn btn-primary" Visible="false" OnClick="btn_actualizar_dir_Click"/>
                                    </div>
                                </div>
                            </div>
                            <div role="tabpanel" class="tab-pane" id="comite">
                                <div class="row">
                                    <div class="col-xs-6">
                                        <asp:Label ID="Label28" runat="server" Text="Estado"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <asp:DropDownList ID="ddl_estado_comite" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <asp:Label ID="Label15" runat="server" Text="Periodo(años)"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-3">
                                        <asp:TextBox ID="tb_periodo" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <asp:Label ID="Label16" runat="server" Text="Fecha Inicio"></asp:Label>
                                    </div>
                                    <div class="col-xs-6">
                                        <asp:Label ID="Label17" runat="server" Text="Fecha Final"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="tb_fechainicio" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="tb_fechafinal" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <asp:Label ID="Label18" runat="server" Text="Acuerdo"></asp:Label>
                                    </div>
                                    <div class="col-xs-6">
                                        <asp:Label ID="Label19" runat="server" Text="Fecha Acuerdo"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="tb_acuerdo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="tb_fecha_acuerdo" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <asp:Label ID="Label24" runat="server" Text="Finiquito"></asp:Label>
                                    </div>
                                    <div class="col-xs-6">
                                        <asp:Label ID="Label25" runat="server" Text="Fecha Finiquito"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="tb_finiquito" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="tb_fecha_finiquito" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <asp:Label ID="Label21" runat="server" Text="Acreditación"></asp:Label>
                                    </div>
                                    <div class="col-xs-6">
                                        <asp:Label ID="Label22" runat="server" Text="Fecha Acreditación"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="tb_acreditacion" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="tb_fecha_acreditacion" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <asp:Label ID="Label26" runat="server" Text="Acta Toma de Posesión"></asp:Label>
                                    </div>
                                    <div class="col-xs-6">
                                        <asp:Label ID="Label27" runat="server" Text="Fecha Toma de Posesión"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="tb_acta_tomap" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="tb_fecha_acta_tomap" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <asp:Label ID="Label11" runat="server" Text="No. Resolución TEDEFE"></asp:Label>
                                    </div>
                                    <div class="col-xs-6">
                                        <asp:Label ID="Label29" runat="server" Text="Fecha Resolución TEDEFE"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="tb_no_tedefe" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="tb_fecha_tedefe" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row" style="margin-top:10px">
                                    <div class="col-xs-6">
                                        <asp:Button ID="btn_anterior" runat="server" Text="Anterior" CssClass="btn btn-primary" OnClick="btn_anterior_Click" Visible="false" />
                                    </div>
                                    <div class="col-xs-6">
                                        <asp:Button ID="btn_aplicar_dirigente" runat="server" Text="Aplicar" CssClass="btn btn-primary" OnClick="btn_aplicar_dirigente_Click" Visible="false"/>
                                    </div>
                                </div>
                                <div class="row" style="margin-top:10px">
                                    <div class="col-xs-12">
                                        <asp:Button ID="btn_actualizar_comite" runat="server" Text="Actualizar Datos" CssClass="btn btn-primary" Visible="false" OnClick="btn_actualizar_comite_Click"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
