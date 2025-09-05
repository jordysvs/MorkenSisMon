<%@ Page Title="Ver Monitoreo" MasterPageFile="~/Master/DefaultMap.master" Language="C#" AutoEventWireup="true" CodeFile="VerMonitoreo.aspx.cs" Inherits="Forms_Gestion_VerMonitoreo" %>

<%@ Register TagPrefix="cc" Namespace="Kruma.Core.Util.Web.Combine.Controls" Assembly="Kruma.Core.Util.Web" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAcn850-1Ywf9URJQFHwBWxhyMYruUNTog" type="text/javascript"></script>
    <script src="../../Script/Plugins/geoxml3-master/polys/geoxml3.js" type="text/javascript"></script>
    <script src="../../Script/Plugins/geoxml3-master/ProjectedOverlay.js" type="text/javascript"></script>
    <cc:ScriptCombine ID="ScriptCombiner" runat="server" ScriptHandler="~/Ashx/Combine/ScriptHandler.ashx" Combine="false">
        <Scripts>
            <cc:ScriptReference Path="~/Script/Base/bootstrap.datetimepicker/bootstrap.datetimepicker.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.datatables/jquery.dataTables.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.datatables/jquery.datatables.responsive.js" />
            <cc:ScriptReference Path="~/Script/Base/bootstrap.datatables/bootstrap.datatables.js" />
            <cc:ScriptReference Path="~/Script/Base/bootstrap.datatables/bootstrap.datatables.responsive.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.bootpag/jquery.bootpag.min.js" />
            <cc:ScriptReference Path="~/Script/Forms/Gestion/VerMonitoreo.js" />
        </Scripts>
    </cc:ScriptCombine>
    <cc:CssCombine ID="CSSCombiner" runat="server" CSSHandler="~/Ashx/Combine/CSSHandler.ashx">
        <CSSReferences>
            <cc:CssReference Path="~/Style/Base/bootstrap.datetimepicker/bootstrap.datetimepicker.css" />
            <cc:CssReference Path="~/Style/Base/jquery.datatables/jquery.datatables.css" />
            <cc:CssReference Path="~/Style/Base/jquery.datatables/jquery.datatables.responsive.css" />
            <cc:CssReference Path="~/Style/Base/bootstrap.datatables/bootstrap.datatables.css" />
        </CSSReferences>
    </cc:CssCombine>
    <script>
        $(function () {
            //var canvas = document.getElementById("lienzo");
            //if (canvas && canvas.getContext) {
            //    var ctx = canvas.getContext("2d");
            //    if (ctx) {
            //        //var R = 100;
            //        //var X = canvas.width / 2;
            //        //var Y = canvas.height / 2;
            //        ctx.fillStyle = "rgba(255, 255, 255, 0.2)";
            //        ctx.lineCap = "round"
            //        // un angulo de 60deg.
            //        //var rad = (Math.PI / 180) * 60;
            //        ctx.beginPath();
            //        //for (var i = 0; i < 6; i++) {
            //        //    x = X + R * Math.cos(rad * i);
            //        //    y = Y + R * Math.sin(rad * i);
            //        //    console.log("x:" + x);
            //        //    console.log("y:" + y);
            //        //    ctx.lineTo(x, y);
            //        //}
            //        ctx.lineTo(120, 0);
            //        ctx.lineTo(175, 0);
            //        ctx.lineTo(520, 395);
            //        ctx.lineTo(800, 0);
            //        ctx.lineTo(855, 0);
            //        ctx.lineTo(535, 450);
            //        ctx.lineTo(500, 450);
            //        ctx.closePath();
            //        ctx.fill();
            //    }
            //}
        });
    </script>
    <style>
        /*.content-wrapper {
            background-image: url(../../Content/Fondo.png);
            height: 500px;
            width: 100%;
        }*/

        .radar {
            background-image: url(../../Content/Radar.png);
            color: white;
            background-repeat: no-repeat;
            background-size: contain;
        }

        .evento {
            position: absolute;
            border-radius: 50px;
            width: 60px;
            height: 60px;
            background-repeat: no-repeat;
            background-size: contain;
            cursor: pointer;
        }

        .alerta {
            background-image: url(../../Content/Radar.png);
            background-color: red;
            background-repeat: no-repeat;
            background-size: contain;
        }

        .textoArea{
            font-size:11px;
            color:white;
        }

         .btn-mitigar{
             background-color: forestgreen;
         }
         .btn-mitigar :hover{
             background-color: lightgreen;
         }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
      <div id="map_canvas" style="width: 100%; height: 800px;">
    </div>
    <%--<canvas id="lienzo" width="1024" height="800" style="position: absolute;">Su navegador no soporta canvas :( </canvas>
    <div style="position: relative;">
        <div id="radar_1" class="evento radar" style="left: 780px; top: 0px;"><span class="textoArea" style="font-size:11px;color:white:">Campamento N°1</span></div>
    </div>--%>
</asp:Content>
<asp:Content ID="contentSideBar" ContentPlaceHolderID="cphSideBar" runat="Server">
    <aside id="divLateral" class="control-sidebar control-sidebar-dark control-sidebar control-sidebar-dark control-sidebar-open" style="position: absolute; height: auto;">
        <ul class="nav nav-tabs nav-justified control-sidebar-tabs">
            <li class="active">
                <a href="#control-sidebar-persona-tab" data-toggle="tab" id="lnkInstitucionPlaza">
                    <u>ESTADO</u>
                </a>
            </li>
        </ul>
        <div class="tab-content">
            <div class="tab-pane active" id="control-sidebar-persona-tab">
                <div>
                     <div id="divAlertas" style="overflow-y: hidden; overflow-x: hidden; max-height: 500px">
                     </div>
                </div>
            </div>
        </div>
    </aside>
    <div class="modal fade colored-header danger" id="mdEvento" tabindex="-1" style="display: none;">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h3><span style="font-size: 20px; font-weight: bold">Alerta</span></h3>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="box box-warning">
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label for="txtAperturaUsuario">Tipo de Alerta:</label>
                                                <input type="text" id="txtAperturaUsuario" class="form-control" readonly="readonly" value="strain_low" />
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label for="txtAperturaMozo">Estado:</label>
                                                <input type="text" class="form-control" readonly="readonly" value="Running" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label for="txtAperturaFecha">Fecha:</label>
                                                <input type="text" id="txtAperturaFecha" class="form-control" readonly="readonly" value="20/09/2017" />
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label for="txtAperturaMozo">Hora:</label>
                                                <input type="text" id="txtAperturaMozo" class="form-control" readonly="readonly" value="1:00 p.m." />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label for="txtAperturaMozo">Posición Inicial:</label>
                                                <input type="text" class="form-control" readonly="readonly" value="1" />
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label for="txtAperturaMozo">Posición Final:</label>
                                                <input type="text" class="form-control" readonly="readonly" value="3" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <label for="txtObservacion">Observación</label>
                                                <asp:TextBox ID="txtObservacion" Placeholder="Ingrese la observación" CssClass="form-control" TextMode="MultiLine" Rows="2" runat="server" ClientIDMode="Static" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="pull-left">
                            <button type="button" class="btn btn-danger btn-flat btn-aceptar">Aceptar</button>
                        </div>
                        <div class="pull-right">
                            <button type="button" class="btn btn-danger btn-flat btn-mitigar">Mitigar</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hdIdAlerta" runat="server" ClientIDMode="Static" />
</asp:Content>