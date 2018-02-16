<%@ Page Title="Control de Asistencia" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ControlAsistencia.aspx.cs" Inherits="secretaria.Asistencias.ControlAsistencia" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" >
<script src="http://code.jquery.com/jquery-1.9.1.js"></script>
<script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>


    <script type="text/javascript">        
        $(function () {
            var $ddl = $("select[name$=ddlAsistente]");
            $ddl.select2();            
            $ddl.focus();
            $ddl.bind("change keyup", function () {
                loadSubjects($("select option:selected").val());
                $ddlSub.fadeIn("slow");
            });
        });
    </script>
    <script >

       //resto del script si lo hay

      @if(ViewBag.Message != ""){

          alert('@ViewBag.Message');

       }

    </script>
    <!-- <meta http-equiv="refresh" content="5" /> -->

<div class="form-group">
    <h2>CONTROL DE ASISTENCIA<asp:Label ID="lblControlAsistencia" runat="server"></asp:Label></h2>
    <div class="jumbotron">       
        <div class="row"><h2><asp:Label ID="lblDescripcion" Text="" runat="server"></asp:Label></h2></div>
        <div class="row"><asp:Label ID="lblId" Text="" runat="server" style="display:none;"></asp:Label></div>
    </div>
    <div class="form-group row">
        <div class="col-lg-7 col-md-7 col-sm-12">
            <div class="jumbotron">
                <div class="form-group col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <asp:label id="lblA" runat="server">Asistente:</asp:label>
                    <asp:DropDownList ID="ddlAsistente" AutoPostBack="true" CssClass="form-control input" runat="server" Width="280px" OnSelectedIndexChanged="ddlAsistenteOnSelectIndex"></asp:DropDownList>             
                </div>
                <div class="form-group col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <asp:label id="lblTA" runat="server">Tipo de Asistencia:</asp:label>
                    <asp:DropDownList ID="ddlTipoAsistencia" CssClass="form-control input" runat="server"></asp:DropDownList>
                    <asp:Button ID="btnAgregar" Text="Agregar" CssClass="btn btn-success" OnClick="btnAgregar_Click" runat="server"/>                    
                </div>
                <div class="form-group col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="form-group row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label>ASISTENTES</label>
                                <h1><asp:Label ID="lblTotalAsistentes" class="label label-primary" Text="" runat="server"></asp:Label></h1>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label>RETIRADOS</label>
                                <h1><asp:Label ID="lblTotalRetirados" class="label label-default" Text="" runat="server"></asp:Label></h1>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label>ASAMBLEISTAS</label>
                                <h1><asp:Label ID="lblTotalFederados" class="label label-success" Text="" runat="server"></asp:Label></h1>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-5 col-md-5 col-sm-12">
            <div class="jumbotron">
                <div>
                     
                        <div class="col-lg-12 col-md-12 col-sm-12">
                            <h1><asp:Button id="btReporte" type="button" class="btn-lg btn-info col-lg-12 col-md-12 col-sm-12" Text="GENERAR REPORTE" OnClick="btReporte_Click" runat="server"/></h1>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12">
                            <h1><asp:Button id="btIniciarQuorum" type="button" class="btn-lg btn-success col-lg-12 col-md-12 col-sm-12" Text="INICIAR QUORUM" OnClick="btIniciarQuorum_Click" OnClientClick="return confirm('¿Desea iniciar el Quórum?');" runat="server"/></h1>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12">
                            <h1><asp:Button id="btFinalizarQuorum2" type="button" class="btn-lg btn-danger col-lg-12 col-md-12 col-sm-12" Text="FINALIZAR QUORUM" OnClick="btFinalizarQuorum_Click" OnClientClick="return confirm('¿Desea finalizar el Quórum?');" runat="server"/></h1>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12">
                            <h1><asp:Button id="btIniciarQuorumF" type="button" class="btn-lg btn-warning col-lg-12 col-md-12 col-sm-12" Text="INICIAR QUORUM" OnClick="btIniciarQuorum_Click" OnClientClick="return confirm('¿Desea iniciar el Quórum?');" runat="server"/></h1>
                        </div> 
                        <div>
                            <div>
                                <h3><asp:Label id="lblEstadoAsamblea2" Text="" runat="server"></asp:Label></h3>                            
                            </div>
                            <div>
                                <h2><asp:Label id="lblHora" text="" runat="server"></asp:Label></h2>
                            </div>
                        </div>  
                </div>
            </div>
        </div>
    </div>

    <div>
        <asp:GridView ID="gvListadoAsistencia" runat="server" OnSelectedIndexChanged="gvListadoAsistencia_SelectedIndexChanged" AllowPaging="False" autogeneratecolumns="false" DataKeyNames="numero" CssClass="table table-hover table-responsive" onrowcommand="opcionesAsistente_RowCommand" OnRowDataBound="opcionesAsistente_RowDataBound">
            <AlternatingRowStyle BackColor="#F0F0F0" />
            <Columns>         
                <asp:boundfield datafield="numero" headertext="idAsistencia"/>                
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="btIngresar" runat="server" ControlStyle-CssClass="btn btn-success" CausesValidation="false" 
                            CommandName="Ingresar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            Text="Ingresar"
                            OnClientClick="return confirm('¿Desea realziar esta operación?');"/>
                        <asp:Button ID="btRetirar" runat="server" ControlStyle-CssClass="btn btn-warning" CausesValidation="false" 
                            CommandName="Retirar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            Text="Retirar"
                            OnClientClick="return confirm('¿Desea realziar esta operación?');"/>
                        <asp:Button ID="btQuitar" runat="server" ControlStyle-CssClass="btn btn-danger" CausesValidation="false" 
                            CommandName="Quitar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            Text="Quitar" 
                            OnClientClick="return confirm('¿Desea realziar esta operación?');"/>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:boundfield datafield="Estado" headertext="Estado"/>               
                <asp:boundfield datafield="entrada" headertext="Entrada"/>
                <asp:boundfield datafield="salida" headertext="Salida"/>
                <asp:boundfield datafield="nombreC" headertext="Nombre Completo"/>
                <asp:boundfield datafield="descripcion" headertext="Cargo"/>
                <asp:boundfield datafield="tipoA" headertext="Tipo Asistencia"/>
                <asp:boundfield datafield="federacion" headertext="FADN"/>
            </Columns>
             <HeaderStyle BackColor="#0099FF" />
        </asp:GridView>
    </div>
</div>
</asp:Content>
