<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IngresoAsamblea.aspx.cs" Inherits="secretaria.Asambleas.IngresoAsamblea" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/css/bootstrap.min.css" />
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.4/jquery.min.js"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.5.0/css/bootstrap-datepicker.min.css" />
<script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.5.0/js/bootstrap-datepicker.min.js"></script>

<script type="text/javascript">
        $( document ).ready(function() {
            $('#fechaAsamblea').datepicker({
                autoclose: true,
                format: "dd/mm/yyyy",
                dayNamesMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"],
                dayNamesShort: ["Dom", "Lun", "Mar", "Mie", "Jue", "Vie", "Sab"],
                monthNames: 
                    ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio",
                    "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
                monthNamesShort: 
                    ["Ene", "Feb", "Mar", "Abr", "May", "Jun",
                    "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"]
                
            });
        });
</script>
    <div class="container">
        <h2>Ingreso de Asamblea</h2>      
            <div class="jumbotron">
                <div class="container">
                    <div>
                        <div class="row">
                            <div>
                                <label for="fecha">Fecha:</label>
                            </div>
                            <div class="col-xl-4 col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                <div class="input-group col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <input type="text" class="form-control" id="fechaAsamblea" name="fechaAsamblea" runat="server" ClientIDMode="Static">
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                                <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server"
                                  ControlToValidate="fechaAsamblea"
                                  ErrorMessage="Ingrese una fecha."
                                  ForeColor="Red"
                                  Font-Size="Small">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div>
                        <div class="row">
                            <div>
                                <label for="tipoAsamblea">Tipo de Asamblea:</label>                                
                            </div>
                            
                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">                                    
                                <asp:DropDownList ID="ddlTipoAsamblea" CssClass="form-control input" runat="server"></asp:DropDownList>
                            </div>
                            
                            
                        </div>
                        <div>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server"
                                ControlToValidate="ddlTipoAsamblea"
                                InitialValue="0"
                                ErrorMessage="Ingrese un tipo de asamblea."
                                ForeColor="Red"
                                Font-Size="Small">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div>
                        <div class="row">
                            <div>
                                <label for="descripcion">Descripción:</label>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                <textarea class="form-control" rows="5" id="descripcion" runat="server"></textarea>
                                <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server"
                                  ControlToValidate="descripcion"
                                  ErrorMessage="Ingrese una descripción."
                                  ForeColor="Red"
                                  Font-Size="Small">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div>
                         <div class="form-group row">
                            <div class="col-xs-2">
                                <asp:Button ID="btnGuardar" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" runat="server"/>
                            </div>
                            <div class="col-xs-2">
                                <asp:Button ID="btnCancelar" Text="Cancelar" CssClass="btn btn-default" OnClick="btnCancelar_Click" runat="server"/>
                            </div>
                        </div> 
                    </div>
                </div>
            </div>
            <asp:Label ID="lblResultado" runat="server" Visible="false" />
    </div>
</asp:Content>

