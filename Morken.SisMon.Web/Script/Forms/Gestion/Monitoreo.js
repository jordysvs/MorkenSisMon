var obj_gTimer = null;
var obj_Audio = null;
var obj_gAudioSistema = null;
var markers = [];
var map = null;
var obj_gEstado = null;
var str_gEstadoAnterior = '';

function initMap() {
    var mapOptions = {
        center: new google.maps.LatLng(28.670106, 77.214455),
        zoom: 9, // Zoom Level
        mapTypeId: 'satellite'
    };

    map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
    ListarKML();
    CargarAlertas();
}

function IniciarTimer() {
    var int_Tiempo = parseInt($('#hdTiempoAlerta').val());
    obj_gTimer = setTimeout(CargarAlertas, int_Tiempo);
}

function ListarKML() {
    if (map) {
        var geoXml = new geoXML3.parser({
            map: map,
            singleInfoWindow: true,
        });
        geoXml.parse('../../Content/Mapa.kml');
    }
}
$(function () {
    CargaInicial();
    CargarControles();
    //CargarAlertas();
    //CargarEventos();
    RichTextView();
});

function CargaInicial() {
    jQuery.validator.addMethod('selectOperador', function (value) {
        return (value != null);
    }, "Seleccione el operador");

    var settings = $('#frmContent').validate().settings;
    $.extend(settings, {
        highlight: function (element) {
            $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
        },
        success: function (element) {
            $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
        }
    });
}

function CargarControles() {
    //var canvas = document.getElementById("lienzo");
    //canvas.addEventListener("mousedown", ObtenerCoords, false);
    var str_RangoDias = $('#hdRangoDias').val();
    str_RangoDias += 'd';
    $(".datetime_horario").datetimepicker({
        language: 'eS',
        format: 'dd/mm/yyyy',
        pickerPosition: "bottom-left",
        autoclose: true,
        minView: 3,
        maxView: 3,
        startView: 2,
        startDate: str_RangoDias,
        endDate: new Date(),
    });

    $("#txtHoraInicio_input").prop('readonly', true);
    $("#txtHoraFin_input").prop('readonly', true);

    $('#mdEvento').find('.modal-footer .btn-aceptar').click(function () {
        ValidarAsignacion();
        if ($('#frmContent').valid()) {
            var int_pIdAlerta = $('#hdIdAlerta').val();
            var int_pIdOperador = $('#ddlOperador').val();
            Asignar(int_pIdAlerta, int_pIdOperador);
            //InicializarEventos();
        }
    });

    $('#mdEvento').find('.modal-footer .btn-mitigar').click(function () {
        var str_IdAlerta = $('#hdIdAlerta').val();
        Mitigar(str_IdAlerta);
    });

    $('#btnBuscar').click(function () {
        ListarAlertas();
        return false;
    });

    InicializarOperadorBuscador(null);
    //InicializarEventos();

    //ListarAlertas();
    obj_Audio = new Audio(UrlPath + 'Content/alarm.wav');
    obj_Audio.addEventListener('ended', function () {
        this.currentTime = 0;
        this.play();
    }, false);

    obj_gAudioSistema = new Audio(UrlPath + 'Content/alarma_sistema.wav');
    obj_gAudioSistema.addEventListener('ended', function () {
        this.currentTime = 0;
        this.play();
    }, false);

    $('.btn-silenciar').click(function () {
        obj_gAudioSistema.pause();
        obj_gAudioSistema.currentTime = 0;
        return false;
    });

    google.maps.event.addDomListener(window, 'load', initMap);
}

function ValidarAsignacion() {
    var settings = $('#frmContent').validate().settings;
    $('.form-group').removeClass('has-error').removeClass('has-success');
    $.extend(settings, {
        rules: {
            ddlOperador: { selectOperador: true },
        },
        messages: {}
    });
}

//function CargarEventos() {
//    //Limpia el timer
//    if (obj_gTimer != null)
//        clearTimeout(obj_gTimer);

//    setTimeout(Alertar, 8000);
//}

function ObtenerEstado() {
    obj_gEstado = ObtenerData('Monitoreo.aspx/ObtenerEstado', {});
    if (obj_gEstado != null) {
        var str_ClassEstado = '';
        if (obj_gEstado.Valor != str_gEstadoAnterior) {
            if ($('#mdSistemaStopped').is(':visible')) {
                $('#mdSistemaStopped').modal('hide');
            }
            if ($('#mdSistemaPaused').is(':visible')) {
                $('#mdSistemaPaused').modal('hide');
            }

            switch (obj_gEstado.Valor) {
                case 'R': str_ClassEstado = 'text-green'; break;
                case 'P': str_ClassEstado = 'text-yellow'; break;
                case 'S': str_ClassEstado = 'text-red'; break;
                default: str_ClassEstado = '';
            }

            $('#iEstado').removeClass('text-green');
            $('#iEstado').removeClass('text-yellow');
            $('#iEstado').removeClass('text-red');
            $('#iEstado').addClass(str_ClassEstado);

            if (obj_gEstado.Valor != 'R') {
                if (obj_gEstado.Valor == 'P') {
                    $('#mdSistemaPaused').modal('show');
                } else if (obj_gEstado.Valor == 'S') {
                    $('#mdSistemaStopped').modal('show');
                }
                obj_Audio.pause();
                obj_Audio.currentTime = 0;
                obj_gAudioSistema.play();
            } else {
                obj_gAudioSistema.pause();
                obj_gAudioSistema.currentTime = 0;
            }

            str_gEstadoAnterior = obj_gEstado.Valor;
        }
    }
}
function Alertar() {

    $('#radar_1').removeClass('radar');
    $('#radar_1').addClass('alerta');
    obj_Audio.play();

    AccionDefault(false, 'Monitoreo.aspx/Notificar', {}, function () { Parpadear(); }, null, null, null, 1);
    //Parpadear();
    InicializarEventos();
}

function Parpadear() {
    $('.alerta').fadeIn(500).delay(500).fadeOut(500, Parpadear);
}

function InicializarOperadorBuscador(obj_pData) {
    $("#ddlOperador").select2({
        data: obj_pData,
        language: "es",
        multiple: false,
        width: '100%',
        allowClear: true,
        minimumInputLength: 0,
        placeholder: "Ingrese el nombre del operador",
        escapeMarkup: function (markup) { return markup; },
        ajax: {
            url: "Monitoreo.aspx/ListarOperador",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            delay: 250,
            type: "POST",
            data: function (params) {
                return JSON.stringify({
                    str_pNombreCompleto: (params.term || '').trim(),
                    int_pNumPagina: params.page || 1,
                    int_pTamPagina: 10
                });
            },
            processResults: function (data, params) {
                var lst_Results = [];
                $.each(data.d.Result, function (i, v) {
                    var obj_Resultado = {};
                    obj_Resultado.id = v.IdOperador;
                    obj_Resultado.nombre = v.Persona.NombreCompleto;
                    lst_Results.push(obj_Resultado);
                })
                params.page = params.page || 1;
                return {
                    results: lst_Results,
                    pagination: {
                        more: (params.page * 10) < data.d.Total
                    }
                };
            },
            cache: true
        },
        templateResult: function (option) {
            if (option.loading) { return option.text; }
            return $("<span>" + option.nombre + "</span>");

        },
        templateSelection: function (option) {
            if (option.id == '') { return option.text; }
            return option.nombre;
        }
    });
}

function InicializarEventos() {
    //if ($('.alerta').length > 0) {
    //    obj_Audio.play();
    //}

    $(".radar").click(function () {
        var str_IdAlerta = $(this).attr('id');
        $('#btnAsignar').css('display', 'block');
        $('#btnMitigar').css('display', 'none');
        $('#ddlOperador').prop('disabled', false);
        $('#divObservacion').css('display', 'block');
        $('#ddlOperador').prop('disabled', false);
        $('#mdEvento').modal('show');
    });
    $('.alerta').click(function () {
        var str_IdAlerta = $(this).attr('id');
        $('#hdIdAlerta').val(str_IdAlerta);
        $('#btnAsignar').css('display', 'block');
        $('#btnMitigar').css('display', 'none');
        $('#ddlOperador').prop('disabled', false);
        $('#divObservacion').css('display', 'none');
        obj_Audio.pause();
        obj_Audio.currentTime = 0;
        $('#mdEvento').modal('show');
    });

    $(".asignado").click(function () {
        var str_IdAlerta = $(this).attr('id');
        $('#hdIdAlerta').val(str_IdAlerta);
        $('#btnAsignar').css('display', 'none');
        $('#btnMitigar').css('display', 'block');
        ObtenerAlerta(str_IdAlerta);
        $('#divObservacion').css('display', 'block');
        $('#mdEvento').modal('show');
    });
    $('.alerta').fadeIn(500).delay(500).fadeOut(500, Parpadear);
    //CKEDITOR.instances.txtObservacion.setData('En proceso de <b>mitigación</b>. Se observó que hubo un leve movimiento en la <b>fibra</b>.');
}

function CargarAlertas() {
    AccionDefault(true, "Monitoreo.aspx/CargarAlertas", { 'int_pCantidadDias': $('#hdRangoDias').val() },
        function (obj_pResultado) {
            if (obj_pResultado.OperationResult != 1) {
                AlertJQ(4, obj_pResultado.Message);
            } else {
                if (obj_pResultado.Objeto.length > 0) {
                    var lst_vAlertas = obj_pResultado.Objeto;
                    var bln_ExistenAlertas = false;

                    var str_RutaIconoAlerta = UrlPath + 'Content/Radar.png';
                    for (var i = 0; i < lst_vAlertas.length; i++) {
                        var bounds = new google.maps.LatLngBounds(new google.maps.LatLng(parseFloat(lst_vAlertas[i].CoordenadaLatitud), parseFloat(lst_vAlertas[i].CoordenadaLongitud)));
                        if (lst_vAlertas[i].IdOperador == null) {
                            bln_ExistenAlertas = true;
                            overlay = new USGSOverlay(bounds, str_RutaIconoAlerta, map, 'alerta', lst_vAlertas[i].IdAlerta, parseFloat(lst_vAlertas[i].CoordenadaLatitud), parseFloat(lst_vAlertas[i].CoordenadaLongitud));
                        }
                        else {
                            if (lst_vAlertas[i].FechaInforme == null)
                                overlay = new USGSOverlay(bounds, str_RutaIconoAlerta, map, 'asignado', lst_vAlertas[i].IdAlerta, parseFloat(lst_vAlertas[i].CoordenadaLatitud), parseFloat(lst_vAlertas[i].CoordenadaLongitud));
                            else
                                overlay = new USGSOverlay(bounds, str_RutaIconoAlerta, map, 'radar', lst_vAlertas[i].IdAlerta, parseFloat(lst_vAlertas[i].CoordenadaLatitud), parseFloat(lst_vAlertas[i].CoordenadaLongitud));
                        }
                    }

                    if (bln_ExistenAlertas)
                        obj_Audio.play();
                }
                //InicializarEventos();
                ListarAlertas();
                ObtenerEstado();
                IniciarTimer();
            }
        }, null, null, null, 1, false, false);
}

function ListarAlertas() {
    AccionDefault(true, "Monitoreo.aspx/ListarAlertas",
        {
            'int_pCantidadDias': $('#hdRangoDias').val(),
            'int_pIdTipoAlerta': $('#ddlTipoAlerta').val(),
            'dt_pFechaInicial': $('#txtHoraInicio_input').val() != '' ? $('#txtHoraInicio').data("datetimepicker").getDate() : null,
            'dt_pFechaFinal': $('#txtHoraFin_input').val() != '' ? $('#txtHoraFin').data("datetimepicker").getDate() : null
        },
        function (lst_Alertas) {
            var lst_vAlertas = lst_Alertas;

            var div_Alertas = $('#divAlertas');
            div_Alertas.empty();

            var str_HTML = '';
            if (lst_vAlertas.length > 0) {
                for (var i = 0; i < lst_vAlertas.length; i++) {
                    if (lst_vAlertas[i].CodigoError == null) {
                        str_HTML += '<div class="row">';
                        str_HTML += '<div class="col-lg-12">';
                        str_HTML += '<div class="form-group">';
                        str_HTML += '<label>Fecha:</label>&nbsp;';
                        str_HTML += '<span>' + ToDateTimeString(lst_vAlertas[i].FechaAlerta) + '</span>';
                        str_HTML += '</div>';
                        str_HTML += '<div class="form-group">';
                        str_HTML += '<label>Estado:</label>&nbsp;&nbsp;';
                        str_HTML += '<span>' + lst_vAlertas[i].AlertaEstado.Descripcion + '</span>';
                        str_HTML += '</div>';
                        str_HTML += '<div class="form-group">';
                        str_HTML += '<label>Tipo de Alerta:</label>&nbsp;';
                        str_HTML += '<span>' + lst_vAlertas[i].TipoAlerta.Descripcion + '</span>';
                        str_HTML += '</div>';
                        str_HTML += '<div class="form-group">';
                        str_HTML += '<label>Pos. Inicial:</label>&nbsp;&nbsp;';
                        str_HTML += '<span>' + lst_vAlertas[i].PosicionInicial + '</span>';
                        str_HTML += '</div>';
                        str_HTML += '<div class="form-group">';
                        str_HTML += '<label>Pos. Final:</label>&nbsp;&nbsp;';
                        str_HTML += '<span>' + lst_vAlertas[i].PosicionFinal + '</span>';
                        str_HTML += '</div>';
                        str_HTML += '</div>';
                        str_HTML += '</div>';
                        str_HTML += '<hr style="color: white;" />';
                    } else {
                        str_HTML += '<div class="row">';
                        str_HTML += '<div class="col-lg-12">';
                        str_HTML += '<div class="form-group">';
                        str_HTML += '<label>Fecha:</label>&nbsp;';
                        str_HTML += '<span>' + ToDateTimeString(lst_vAlertas[i].FechaAlerta) + '</span>';
                        str_HTML += '</div>';
                        str_HTML += '<div class="form-group">';
                        str_HTML += '<label>Código Error:</label>&nbsp;';
                        str_HTML += '<span>' + lst_vAlertas[i].CodigoError + '</span>';
                        str_HTML += '</div>';
                        str_HTML += '<div class="form-group">';
                        str_HTML += '<label>Estado:</label>&nbsp;&nbsp;';
                        str_HTML += '<span>' + lst_vAlertas[i].AlertaEstado.Descripcion + '</span>';
                        str_HTML += '</div>';
                        str_HTML += '</div>';
                        str_HTML += '</div>';
                        str_HTML += '<hr style="color: white;" />';
                    }
                }
                div_Alertas.append($(str_HTML));
            }
        }, null, null, null, 1)
}

function ObtenerAlerta(int_pIdAlerta) {
    AccionDefault(true, 'Monitoreo.aspx/ObtenerAlerta', { 'int_pIdAlerta': int_pIdAlerta },
        function (obj_pAlerta) {
            if (obj_pAlerta != null) {
                $('#txtTipoAlerta').val(obj_pAlerta.TipoAlerta.Descripcion);
                $('#txtAlertaEstado').val(obj_pAlerta.AlertaEstado.Descripcion);
                $('#txtPosicionInicial').val(obj_pAlerta.PosicionInicial + 'm');
                $('#txtPosicionFinal').val(obj_pAlerta.PosicionFinal + 'm');
                $('#txtFechaAlerta').val(ToDateTimeString(obj_pAlerta.FechaAlerta));
                $('#txtValorUmbral').val(obj_pAlerta.ValorUmbral);
                $('#txtValorUmbralMaximo').val(obj_pAlerta.ValorUmbralMaximo);
                $('#txtCantidadGolpes').val(obj_pAlerta.CantidadGolpes);
                $('#txtCantidadGolpesMaximo').val(obj_pAlerta.CantidadGolpesMaximo);

                $('#divObservacion').css('display', 'none');
                if (obj_pAlerta.IdOperador != null) {
                    //$('#btnAsignar').css('display', 'none');
                    var lst_Operador = [];
                    var obj = new Object();
                    obj.id = obj_pAlerta.IdOperador;
                    obj.nombre = obj_pAlerta.Operador.Persona.NombreCompleto;
                    lst_Operador.push(obj);
                    InicializarOperadorBuscador(lst_Operador);
                    //$('#ddlOperador').prop('disabled', true);
                    CKEDITOR.instances.txtObservacion.setData(obj_pAlerta.Observacion);
                } else {
                    InicializarOperadorBuscador(null);
                    CKEDITOR.instances.txtObservacion.setData('');
                    $('#btnAsignar').css('display', 'block');
                }

                if (obj_pAlerta.FechaInforme != null) {
                    $('#btnAsignar').css('display', 'none');
                    $('#divObservacion').css('display', 'block');
                    $('#ddlOperador').prop('disabled', true);
                    if (obj_pAlerta.FechaMitigacion != null)
                        $('#btnMitigar').css('display', 'none');
                    else
                        $('#btnMitigar').css('display', 'block');
                }
                else
                    $('#btnMitigar').css('display', 'none');

                $('#mdEvento').modal('show');
            }
        }, null, null, null, 1)
}

function Asignar(int_pIdAlerta, int_pIdOperador) {
    Accion('Monitoreo.aspx/AsignarAlerta', { 'int_pIdAlerta': int_pIdAlerta, 'int_pIdOperador': int_pIdOperador },
        function () {
            var str_IdAlerta = $('#hdIdAlerta').val();
            var obj = $('#' + str_IdAlerta);
            $(obj).addClass('asignado');
            $(obj).addClass('radar');
            //$('#' + str_IdAlerta).toggleClass('alerta');
            $(obj).css('zIndex', '99');
            $(obj).removeClass('alerta');
            $(obj).finish();
            $(obj).css('display', 'block');
            $('#divObservacion').css('display', 'none');
            $("#mdEvento").modal('hide');
            //InicializarEventos();
        });
}

function Mitigar(int_pIdAlerta) {
    Accion('Monitoreo.aspx/MitigarAlerta',
        {
            'int_pIdAlerta': int_pIdAlerta,
        },
        function () {
            //$("#" + int_pIdAlerta).removeClass('asignado');
            var obj = $('#' + int_pIdAlerta);
            $(obj).toggleClass('asignado', false);
            $(obj).finish();
            $(obj).css('display', 'none');
            $("#divObservacion").css('display', 'none');
            $("#mdEvento").modal('hide');
            //InicializarEventos();
            ListarAlertas();
        });
}

function AddMarker(obj_pPosition, obj_pMap, str_pTitle) {
    //setMapClean();

    //var str_Latitud = obj_pPosition.lat();
    //var str_Longitud = obj_pPosition.lng();

    var marker = new google.maps.Marker({
        position: obj_pPosition,
        map: obj_pMap,
        icon: '../../Content/Radar.png',
        title: str_pTitle
    });
    obj_pMap.panTo(obj_pPosition);
    markers.push(marker);
    google.maps.event.addListener(marker, 'click', function () { $('#mdEvento').modal('show'); });
    //$("#txtLatitud").val(str_Latitud);
    //$("#txtLongitud").val(str_Longitud);
}

USGSOverlay.prototype = new google.maps.OverlayView();
/** @constructor */
function USGSOverlay(bounds, image, map, tipo, id, lat, log) {
    // Initialize all properties.
    this.id = id;
    this.tipo = tipo;
    this.lat = lat;
    this.log = log;
    this.bounds_ = bounds;
    this.image_ = image;
    this.map_ = map;
    this.div_ = null;
    this.setMap(map);
}

/**
 * onAdd is called when the map's panes are ready and the overlay has been
 * added to the map.
 */
USGSOverlay.prototype.onAdd = function () {
    var int_Id = this.id;
    var div = null;
    if ($("#" + int_Id).length < 1) {
        div = document.createElement('div');
        div.style.borderStyle = 'none';
        div.style.borderWidth = '0px';
        div.style.position = 'absolute';
        div.style.zIndex = '100';
        $(div).addClass(this.tipo);
        $(div).css('border-radius', '50px');
        if (this.tipo == 'asignado') {
            $(div).addClass('radar');
            div.style.zIndex = '99';
        } else
            div.style.zIndex = '100';

        $(div).attr('id', this.id);
        $(div).attr('lat', this.lat);
        $(div).attr('log', this.log);

        // Create the img element and attach it to the div.
        //var img = document.createElement('img');
        //img.src = this.image_;
        //img.style.width = '100%';
        //img.style.height = '100%';
        //img.style.position = 'absolute';
        //div.appendChild(img);

        if ($(div).hasClass('alerta')) {
            $(div).fadeIn(500).delay(500).fadeOut(500, Parpadear);
        }

        //InicializarEventos();
        // Add the element to the "overlayLayer" pane.
        var panes = this.getPanes();
        panes.overlayMouseTarget.appendChild(div);
    } else {
        div = $("#" + int_Id)[0];
        if (this.tipo == 'asignado') {
            div.style.zIndex = '99';
            if ($(div).hasClass('alerta')) {
                $(div).removeClass('alerta');
                $(div).finish();
            }
            if ($(div).hasClass('radar') == false)
                $(div).addClass('radar');

            $(div).css('display', 'block');
        } else {
            if (this.tipo == 'radar') {
                if ($(div).hasClass('alerta')) {
                    $(div).removeClass('alerta');
                    $(div).finish();
                }
                if ($(div).hasClass('asignado')) 
                    $(div).removeClass('asignado');
                
                if ($(div).hasClass('radar') == false)
                    $(div).addClass('radar');
            }
            div.style.zIndex = '100';
            //$(div).removeClass('alerta');
        }

    }

    this.div_ = div;
    $(div).unbind('click');
    $(div).on('click', function () {
        var str_IdAlerta = $(this).attr('id');
        var str_Latitud = $(this).attr('lat');
        var str_Longitud = $(this).attr('log');
        $('#hdIdAlerta').val(str_IdAlerta);
        $('#txtLatitud').val(str_Latitud);
        $('#txtLongitud').val(str_Longitud);
        $("#ddlOperador").empty().trigger('change');
        $('#ddlOperador').prop('disabled', false);
        if ($(this).hasClass('alerta')) {
            ObtenerAlerta(str_IdAlerta);
            $('#btnAsignar').css('display', 'block');
            $('#btnMitigar').css('display', 'none');
            //$('#ddlOperador').prop('disabled', false);
            $('#divObservacion').css('display', 'none');
            obj_Audio.pause();
            obj_Audio.currentTime = 0;
        } else if ($(this).hasClass('asignado')) {
            ObtenerAlerta(str_IdAlerta);
            //$('#ddlOperador').prop('disabled', true);
            //$('#divObservacion').css('display', 'block');
        } else if ($(this).hasClass('radar')) {
            ObtenerAlerta(str_IdAlerta);
            $('#btnAsignar').css('display', 'block');
            $('#btnMitigar').css('display', 'none');
            $('#ddlOperador').prop('disabled', false);
            $('#divObservacion').css('display', 'block');
        }
    });
};

USGSOverlay.prototype.draw = function () {
    var overlayProjection = this.getProjection();
    var sw = overlayProjection.fromLatLngToDivPixel(this.bounds_.getSouthWest());
    var ne = overlayProjection.fromLatLngToDivPixel(this.bounds_.getNorthEast());
    var div = this.div_;
    if (div != null) {
        div.style.left = sw.x + 'px';
        div.style.top = ne.y + 'px';
        div.style.width = 30 + 'px';
        div.style.height = 30 + 'px';
    }
};

USGSOverlay.prototype.onRemove = function () {
    this.div_.parentNode.removeChild(this.div_);
    this.div_ = null;
};