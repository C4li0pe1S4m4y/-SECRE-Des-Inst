<%@ Page Title="Control de Asistencia" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ControlAsistenciaSoloLectura.aspx.cs" Inherits="secretaria.Asistencias.ControlAsistenciaSoloLectura" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css">
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="http://code.jquery.com/jquery-latest.js"></script>



    <script type="text/javascript">
        var oldgridcolor;
        var oldfont;
        function SetMouseOver(element){
            oldgridcolor = element.style.backgroundColor;
            oldfont = element.style.fontWeight;
            element.style.backgroundColor = '#D5EEFF';
            element.style.cursor = 'pointer';
            //element.style.fontWeight = '700';
        }
        function SetMouseOut(element){
            //element.style.fontWeight = oldfont;
            element.style.backgroundColor = oldgridcolor;
            element.style.textDecoration = 'none';            
        }

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
            <asp:Label ID="lblId" Text="" runat="server" Style="display: none;"></asp:Label></div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Timer ID="Timer1" runat="server" Interval="600" OnTick="Timer1_Tick"></asp:Timer>
                <div class="form-group row">
                    <div class="col-lg-7 col-md-7 col-sm-12">
                        <div class="jumbotron">

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
                                <div>
                                    <div>

                                        <h2>
                                            <asp:Label ID="lblDescripcion" Text="" runat="server"></asp:Label></h2>
                                        <h3>
                                            <asp:Label ID="lblEstadoAsamblea2" Text="" runat="server"></asp:Label></h3>
                                    </div>
                                    <div>
                                        <h2>
                                            <asp:Label ID="lblHora" Text="" runat="server"></asp:Label></h2>
                                    </div>
                                    <!--
                            <table>
                                <tr>
                                    
                                    <td class="col-lg-2 col-md-2 col-sm-2">
                                        <div class="mylabel">
                                            <input type="checkbox" id="timerCheck" onclick="StartTime()">
                                            <div class="slidinggroove"></div>
                                            <label class="mylabel" for="timerCheck"></label>
                                        </div>
                                    </td>
                                    <td>
                                        <label>Actualizar Auto</label>
                                    </td>
                                </tr>
                            </table>
                            -->
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div>

                    <asp:GridView ID="gvListadoAsistencia" runat="server" 
                        OnSelectedIndexChanged="gvListadoAsistencia_SelectedIndexChanged" AutoGenerateColumns="false" 
                        DataKeyNames="numero" CssClass="table table-responsive" OnRowCommand="opcionesAsistente_RowCommand" 
                        OnRowDataBound="opcionesAsistente_RowDataBound" >
                        <AlternatingRowStyle BackColor="#F0F0F0" />
                        <Columns>
                            <asp:BoundField DataField="numero" HeaderText="idAsistencia" />
                            <asp:BoundField DataField="Estado" HeaderText="Estado">
                                <ItemStyle Width="5%" Font-Size="X-Large" />
                            </asp:BoundField>
                            <asp:BoundField DataField="entrada" HeaderText="Entrada">
                                <ItemStyle Width="5%" Font-Size="X-Large" />
                            </asp:BoundField>
                            <asp:BoundField DataField="salida" HeaderText="Salida">
                                <ItemStyle Width="5%" Font-Size="X-Large" />
                            </asp:BoundField>
                            <asp:BoundField DataField="federacion" HeaderText="FADN">
                                <ItemStyle Width="15%" Font-Size="X-Large" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombreC" HeaderText="Nombre Completo">
                                <ItemStyle Width="20%" Font-Size="X-Large" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion" HeaderText="Cargo">
                                <ItemStyle Width="5%" Font-Size="X-Large" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipoA" HeaderText="Tipo Asistencia">
                                <ItemStyle Width="5%" Font-Size="X-Large" />
                            </asp:BoundField>

                        </Columns>
                        <HeaderStyle BackColor="#0099FF" />
                    </asp:GridView>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
