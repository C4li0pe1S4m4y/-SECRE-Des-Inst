<%@ Page Title="Control de Asistencia" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ControlAsistenciaSoloLectura.aspx.cs" Inherits="secretaria.Asistencias.ControlAsistenciaSoloLectura" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" >
<script src="http://code.jquery.com/jquery-1.9.1.js"></script>
<script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
<script src="http://code.jquery.com/jquery-latest.js"></script>

    <style>
        .mylabel{
            position: relative;
            display: block;
            width: 60px;
            height: 30px;
            margin-bottom: 15px;
        }

        .mylabel input{
            display: none;
        }

        .slidinggroove{
            position: absolute;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            background: #ababab;
            border-radius: 20px;
            transition: all 0.3s ease;
        }

        .slidinggroove:after{
            position: absolute;
            content: "";
            width: 28px;
            height: 28px;
            border-radius: 50px;
            background: #fff;
            top: 1px;
            left: 1px;
            transition: all 0.3s ease;
        }

        input:checked + .slidinggroove{
            background: #5fcf80;
        }

        input:checked + .slidinggroove:after{
            transform: translateX(30px);
        }

        .labelterm{
            position: relative;
            top: 5px;
            margin-left: 65px;
        }
    </style>

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

    
        <div class="row"><asp:Label ID="lblId" Text="" runat="server" style="display:none;"></asp:Label></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <asp:Timer ID="Timer1" runat="server" Interval="3600" ontick="Timer1_Tick"></asp:Timer>
    <div class="form-group row">
        <div class="col-lg-7 col-md-7 col-sm-12">
            <div class="jumbotron">
                
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
                        <div>
                            <div>
  
                                <h2><asp:Label ID="lblDescripcion" Text="" runat="server"></asp:Label></h2>  
                                <h3><asp:Label id="lblEstadoAsamblea2" Text="" runat="server"></asp:Label></h3>                            
                            </div>
                            <div>
                                <h2><asp:Label id="lblHora" text="" runat="server"></asp:Label></h2>
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
        
            <asp:GridView ID="gvListadoAsistencia" runat="server" OnSelectedIndexChanged="gvListadoAsistencia_SelectedIndexChanged" AllowPaging="False" autogeneratecolumns="false" DataKeyNames="numero" CssClass="table table-hover table-responsive" onrowcommand="opcionesAsistente_RowCommand" OnRowDataBound="opcionesAsistente_RowDataBound">
                <AlternatingRowStyle BackColor="#F0F0F0" />
                <Columns>         
                    <asp:boundfield datafield="numero" headertext="idAsistencia"/>                
                    <asp:boundfield datafield="Estado" headertext="Estado"><ItemStyle Width="5%"/> </asp:BoundField>            
                    <asp:boundfield datafield="entrada" headertext="Entrada"><ItemStyle Width="5%"/> </asp:BoundField>
                    <asp:boundfield datafield="salida" headertext="Salida"><ItemStyle Width="5%"/> </asp:BoundField>
                    <asp:boundfield datafield="federacion" headertext="FADN"><ItemStyle Width="15%"/> </asp:BoundField>
                    <asp:boundfield datafield="nombreC" headertext="Nombre Completo"><ItemStyle Width="20%"/> </asp:BoundField>
                    <asp:boundfield datafield="descripcion" headertext="Cargo"><ItemStyle Width="5%"/> </asp:BoundField>
                    <asp:boundfield datafield="tipoA" headertext="Tipo Asistencia"><ItemStyle Width="5%"/> </asp:BoundField>
                
                </Columns>
                 <HeaderStyle BackColor="#0099FF" />
            </asp:GridView>
        
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</div>
</asp:Content>
