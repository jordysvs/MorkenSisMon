<%@ Page Title="Monitoreo" Language="C#" MasterPageFile="~/Master/DefaultMap.master" AutoEventWireup="true" CodeFile="Monitoreo.aspx.cs" Inherits="Forms_Gestion_Monitoreo" %>

<%@ Register TagPrefix="cc" Namespace="Kruma.Core.Util.Web.Combine.Controls" Assembly="Kruma.Core.Util.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">

    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAcn850-1Ywf9URJQFHwBWxhyMYruUNTog" type="text/javascript"></script>
    <script src="../../Script/Plugins/geoxml3-master/polys/geoxml3.js" type="text/javascript"></script>
    <script src="../../Script/Plugins/geoxml3-master/ProjectedOverlay.js" type="text/javascript"></script>

    <cc:ScriptCombine ID="ScriptCombiner" runat="server" ScriptHandler="~/Ashx/Combine/ScriptHandler.ashx" Combine="false" Minify="false" AppCacheDuration="0" BrowserCacheDuration="0" Compress="false">
        <Scripts>
            <cc:ScriptReference Path="~/Script/Plugins/jquery.select2/jquery.select2.min.js" />
            <cc:ScriptReference Path="~/Script/Plugins/jquery.select2/i18n/es.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.validate/jquery.validate.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.validate/additional-methods.js" />
            <cc:ScriptReference Path="~/Script/Base/bootstrap.datetimepicker/bootstrap.datetimepicker.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.datatables/jquery.dataTables.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.datatables/jquery.datatables.responsive.js" />
            <cc:ScriptReference Path="~/Script/Base/bootstrap.datatables/bootstrap.datatables.js" />
            <cc:ScriptReference Path="~/Script/Base/bootstrap.datatables/bootstrap.datatables.responsive.js" />
            <cc:ScriptReference Path="~/Script/Base/jquery.bootpag/jquery.bootpag.min.js" />
            <cc:ScriptReference Path="~/Script/Forms/Gestion/Monitoreo.js" />
        </Scripts>
    </cc:ScriptCombine>
    <cc:CssCombine ID="CSSCombiner" runat="server" CSSHandler="~/Ashx/Combine/CSSHandler.ashx">
        <CSSReferences>
            <cc:CssReference Path="~/Style/Plugins/jquery.select2/jquery.select2.min.css" />
            <cc:CssReference Path="~/Style/Base/bootstrap.datetimepicker/bootstrap.datetimepicker.css" />
            <cc:CssReference Path="~/Style/Base/jquery.datatables/jquery.datatables.css" />
            <cc:CssReference Path="~/Style/Base/jquery.datatables/jquery.datatables.responsive.css" />
            <cc:CssReference Path="~/Style/Base/bootstrap.datatables/bootstrap.datatables.css" />
        </CSSReferences>
    </cc:CssCombine>

    <style>
        /*.content-wrapper {
                background-image: url(../../Content/Fondo.png);
                height: 500px;
                width: 100%;
            }*/

        .radar {
            background-image: url(../../Content/Radar.png);
            background-repeat: no-repeat;
            color: white;
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

         .asignado {
            background-image: url(../../Content/Radar.png);
            background-color: red;
            background-repeat: no-repeat;
            background-size: contain;
        }

        .textoArea {
            font-size: 11px;
            color: white;
        }

        .btn-mitigar {
            background-color: forestgreen;
        }

            .btn-mitigar :hover {
                background-color: lightgreen;
            }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <script src="<%= Page.ResolveClientUrl("~/Script/Plugins/ckeditor/ckeditor.js?v=9") %>"></script>
    <div id="map_canvas" style="width: 100%; height: 800px;">
    </div>
</asp:Content>
<asp:Content ID="contentSideBar" ContentPlaceHolderID="cphSideBar" runat="Server">
    <aside id="divLateral" class="control-sidebar control-sidebar-dark control-sidebar control-sidebar-dark control-sidebar-open" style="position: absolute; height: auto;">
        <ul class="nav nav-tabs nav-justified control-sidebar-tabs">
            <li class="active">
                <a href="#control-sidebar-persona-tab" data-toggle="tab" id="lnkInstitucionPlaza">
                    <u>ESTADO</u>
                    <i id="iEstado" class="fa fa-circle"></i>
                </a>
            </li>
        </ul>
        <div class="tab-content">
            <div class="tab-pane active" id="control-sidebar-persona-tab">
                <div>
                    <div class="row">
                        <div class="col-lg-11">
                            <div class="form-group">
                                <label for="ddlTipoAlerta">Tipo de Alerta:</label>
                                <asp:DropDownList ID="ddlTipoAlerta" runat="server" CssClass="form-control" ClientIDMode="Static">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-11">
                            <div class="form-group">
                                <label for="txtHoraInicio">Fecha inicio:</label>
                                <div id="txtHoraInicio" class="input-group date datetime_horario">
                                    <input id="txtHoraInicio_input" name="txtHoraInicio_input" class="form-control" size="16" type="text" placeholder="Ingrese la fecha" />
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-th"></span></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-11">
                            <div class="form-group">
                                <label for="txtHoraFin">Fecha fin:</label>
                                <div id="txtHoraFin" class="input-group date datetime_horario">
                                    <input id="txtHoraFin_input" name="txtHoraFin_input" class="form-control" size="16" type="text" placeholder="Ingrese la fecha" />
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-th"></span></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-11">
                            <span id="btnBuscar" title="Buscar" class="btn btn-danger input-group-btn"><span class="fa fa-search"></span></span>
                        </div>
                    </div>
                    <hr />
                    <div id="divAlertas" style="overflow-y: scroll; overflow-x: hidden; max-height: 300px">
                    </div>
                </div>
            </div>
        </div>
    </aside>
    <div class="modal fade colored-header danger" id="mdEvento" style="display: none;">
        <div class="modal-dialog modal-md">
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
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <label for="txtTipoAlerta">Tipo de Alerta:</label>
                                                <input id="txtTipoAlerta" type="text" class="form-control" readonly="readonly" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <label for="txtAlertaEstado">Estado:</label>
                                                <input id="txtAlertaEstado" type="text" class="form-control" readonly="readonly" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <label for="txtPosicionInicial">Posición Inicial:</label>
                                                <input id="txtPosicionInicial" type="text" class="form-control" readonly="readonly" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <label for="txtPosicionFinal">Posición Final:</label>
                                                <input id="txtPosicionFinal" type="text" class="form-control" readonly="readonly" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label for="txtLatitud">Latitud:</label>
                                                <input type="text" id="txtLatitud" class="form-control" readonly="readonly" />
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label for="txtLongitud">Longitud:</label>
                                                <input type="text" id="txtLongitud" class="form-control" readonly="readonly" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <label for="txtValorUmbral">Valor Umbral:</label>
                                                <input id="txtValorUmbral" type="text" class="form-control" readonly="readonly" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <label for="txtValorUmbralMaximo">Valor Pico:</label>
                                                <input id="txtValorUmbralMaximo" type="text" class="form-control" readonly="readonly" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <label for="txtCantidadGolpes">Cantidad de Golpes:</label>
                                                <input id="txtCantidadGolpes" type="text" class="form-control" readonly="readonly" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <label for="txtCantidadGolpesMaximo">Tolerancia:</label>
                                                <input id="txtCantidadGolpesMaximo" type="text" class="form-control" readonly="readonly" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label for="txtFechaAlerta">Fecha y Hora:</label>
                                                <input type="text" id="txtFechaAlerta" class="form-control" readonly="readonly" />
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label for="ddlOperador">Operador:</label>
                                                <select id="ddlOperador" name="ddlOperador" class="form-control"></select>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divObservacion" class="row" style="display: none;">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <label for="txtObservacion">Observación:</label>
                                                <asp:TextBox ID="txtObservacion" Placeholder="Ingrese la observación" CssClass="form-control richTextView" TextMode="MultiLine" Rows="5" runat="server" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="pull-right">
                            <button id="btnAsignar" type="button" class="btn btn-danger btn-flat btn-aceptar">ASIGNAR</button>
                            <button id="btnMitigar" type="button" class="btn btn-danger btn-flat btn-mitigar">MITIGAR</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade full-color danger" id="mdSistemaStopped" data-keyboard="true" data-backdrop="static" style="display: none;">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h3>Estado de Sistema</h3>
                </div>
                <div class="modal-body">
                        <div  class="text-center">
                            <div class="i-circle danger"><i class="fa fa-times"></i></div>
                            <p>El sistema está detenido</p>
                        </div>
                </div>
                <div class="modal-footer">
                        <button type="button" class="btn btn-flat btn-mono3 btn-silenciar btn-danger" data-dismiss="modal"><i class="fa fa-volume-off" aria-hidden="true"></i></button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade full-color warning" id="mdSistemaPaused" data-keyboard="true" data-backdrop="static" style="display: none;">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h3>Estado de Sistema</h3>
                </div>
                <div class="modal-body">
                        <div  class="text-center">
                            <div class="i-circle warning"><i class="fa fa-warning"></i></div>
                            <p>El sistema está pausado</p>
                        </div>
                </div>
                <div class="modal-footer">
                        <button type="button" class="btn btn-flat btn-mono3 btn-silenciar btn-warning" data-dismiss="modal"><i class="fa fa-volume-off" aria-hidden="true"></i></button>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdTiempoAlerta" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdIdAlerta" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdRangoDias" runat="server" ClientIDMode="Static" />
</asp:Content>
