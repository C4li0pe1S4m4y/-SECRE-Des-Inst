<%@ Page Title="Control de Asistencia" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ControlAsistencia.aspx.cs" Inherits="secretaria.Asistencias.ControlAsistencia" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css">
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
    <script>

        //resto del script si lo hay

      @if(ViewBag.Message != ""){

          alert('@ViewBag.Message');

      }

    </script>
    <!-- <meta http-equiv="refresh" content="5" /> -->

    <div class="form-group">

        <div class="row">
            <h2>
                <asp:Label ID="lblDescripcion" Text="" runat="server"></asp:Label></h2>
        </div>
        <div class="row">
            <asp:Label ID="lblId" Text="" runat="server" Style="display: none;"></asp:Label>
        </div>

        <asp:UpdatePanel ID="uPanel" runat="server">
            <ContentTemplate>


                <div class="form-group row">
                    <div class="col-lg-7 col-md-7 col-sm-12">
                        <div class="jumbotron">
                            <table class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <tr>
                                    <td>
                                        <div>
                                            <asp:Label ID="lblFadn" runat="server">FADN:</asp:Label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlFadn" AutoPostBack="true" CssClass="form-control input" runat="server" Width="280px" OnSelectedIndexChanged="ddlFadnOnSelectIndex"></asp:DropDownList>
                                        </div>
                                        <div>
                                            <asp:RequiredFieldValidator ID="rfv1" runat="server"
                                                ControlToValidate="ddlFadn"
                                                InitialValue="0"
                                                ErrorMessage="Seleccione una federación."
                                                ForeColor="Red"
                                                Font-Size="Small">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:Label ID="lblA" runat="server">Asistente:</asp:Label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlAsistente" AutoPostBack="true" CssClass="form-control input" runat="server" Width="280px" OnSelectedIndexChanged="ddlAsistenteOnSelectIndex"></asp:DropDownList>
                                        </div>
                                        <div>
                                            <asp:RequiredFieldValidator ID="rfv2" runat="server"
                                                ControlToValidate="ddlAsistente"
                                                InitialValue="0"
                                                ErrorMessage="Seleccione un dirigente."
                                                ForeColor="Red"
                                                Font-Size="Small">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                            <asp:Label ID="lblTA" runat="server">Tipo de Asistencia:</asp:Label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlTipoAsistencia" CssClass="form-control input" runat="server"></asp:DropDownList>
                                        </div>
                                        <div>
                                            <asp:RequiredFieldValidator ID="rfv3" runat="server"
                                                ControlToValidate="ddlTipoAsistencia"
                                                InitialValue="0"
                                                ErrorMessage="Seleccione el tipo de asistencia."
                                                ForeColor="Red"
                                                Font-Size="Small">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                    <td class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <asp:Button ID="btnAgregar" Text="Agregar" CssClass="btn btn-success col-lg-6 col-md-6 col-sm-6 col-xs-6" OnClick="btnAgregar_Click" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <div class="form-group col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="form-group row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label>ASISTENTES</label>
                                            <h1>
                                                <asp:Label ID="lblTotalAsistentes" class="label label-primary" Text="" runat="server"></asp:Label></h1>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label>RETIRADOS</label>
                                            <h1>
                                                <asp:Label ID="lblTotalRetirados" class="label label-default" Text="" runat="server"></asp:Label></h1>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label>ASAMBLEISTAS</label>
                                            <h1>
                                                <asp:Label ID="lblTotalFederados" class="label label-success" Text="" runat="server"></asp:Label></h1>
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
                                    <h1>
                                        <asp:Button ID="btReporte" type="button" class="btn-lg btn-info col-lg-12 col-md-12 col-sm-12" Text="GENERAR REPORTE" OnClick="btReporte_Click" runat="server" /></h1>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <h1>
                                        <asp:Button ID="btIniciarQuorum" type="button" class="btn-lg btn-success col-lg-12 col-md-12 col-sm-12" Text="INICIAR ASAMBLEA" OnClick="btIniciarQuorum_Click" OnClientClick="return confirm('¿Desea iniciar el Quórum?');" runat="server" /></h1>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <h1>
                                        <asp:Button ID="btFinalizarQuorum2" type="button" class="btn-lg btn-danger col-lg-12 col-md-12 col-sm-12" Text="FINALIZAR ASAMBLEA" OnClick="btFinalizarQuorum_Click" OnClientClick="return confirm('¿Desea finalizar el Quórum?');" runat="server" /></h1>
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <h1>
                                        <asp:Button ID="btIniciarQuorumF" type="button" class="btn-lg btn-warning col-lg-12 col-md-12 col-sm-12" Text="INICIAR ASAMBLEA" OnClick="btIniciarQuorum_Click" OnClientClick="return confirm('¿Desea iniciar el Quórum?');" runat="server" /></h1>
                                </div>
                                <div>
                                    <div>
                                        <h3>
                                            <asp:Label ID="lblEstadoAsamblea2" Text="" runat="server"></asp:Label></h3>
                                    </div>
                                    <div>
                                        <h2>
                                            <asp:Label ID="lblHora" Text="" runat="server"></asp:Label></h2>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div>
                    <asp:GridView ID="gvListadoAsistencia" runat="server" OnSelectedIndexChanged="gvListadoAsistencia_SelectedIndexChanged" AllowPaging="False" AutoGenerateColumns="false" DataKeyNames="numero" CssClass="table table-hover table-responsive" OnRowCommand="opcionesAsistente_RowCommand" OnRowDataBound="opcionesAsistente_RowDataBound">
                        <AlternatingRowStyle BackColor="#F0F0F0" />
                        <Columns>
                            <asp:BoundField DataField="numero" HeaderText="idAsistencia" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="btIngresar" runat="server" ControlStyle-CssClass="btn btn-success" CausesValidation="false"
                                        CommandName="Ingresar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                        Text="Ingresar"
                                        OnClientClick="return confirm('¿Desea realziar esta operación?');" />
                                    <asp:Button ID="btRetirar" runat="server" ControlStyle-CssClass="btn btn-warning" CausesValidation="false"
                                        CommandName="Retirar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                        Text="Retirar"
                                        OnClientClick="return confirm('¿Desea realziar esta operación?');" />
                                    <asp:Button ID="btQuitar" runat="server" ControlStyle-CssClass="btn btn-danger" CausesValidation="false"
                                        CommandName="Quitar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                        Text="Quitar"
                                        OnClientClick="return confirm('¿Desea realziar esta operación?');" />
                                </ItemTemplate>
                                <ItemStyle Width="18%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Estado" HeaderText="Estado">
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="entrada" HeaderText="Entrada">
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="salida" HeaderText="Salida">
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="federacion" HeaderText="FADN">
                                <ItemStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombreC" HeaderText="Nombre Completo">
                                <ItemStyle Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion" HeaderText="Cargo">
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipoA" HeaderText="Tipo Asistencia">
                                <ItemStyle Width="5%" />
                            </asp:BoundField>

                        </Columns>
                        <HeaderStyle BackColor="#0099FF" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btReporte" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
