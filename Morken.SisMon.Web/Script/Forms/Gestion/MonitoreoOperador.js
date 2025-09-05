var obj_gTimer = null;
var int_gIndiceRadar = 1;
var obj_Audio = null;

var markers = [];
var map = null;
function initMap() {
    var mapOptions = {
        center: new google.maps.LatLng(28.670106, 77.214455),
        zoom: 9, // Zoom Level
        mapTypeId: 'satellite',
    };

    map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
    ListarKML();
    CargarAlertas();
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
    //Alertar();
});

function CargaInicial() {

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
    //CargarSegmentos();
    $(".datetime_horario").datetimepicker({
        language: 'eS',
        format: 'dd/mm/yyyy',
        pickerPosition: "bottom-left",
        autoclose: true,
        minView: 3,
        maxView: 3,
        startView: 2,
        endDate: new Date()
    });

    $("#txtHoraInicio_input").prop('readonly', true);
    $("#txtHoraFin_input").prop('readonly', true);

    $('#btnBuscar').click(function () {
        ListarAlertas();
        return false;
    });

    //$('.alerta').click(function () {
    //    //obj_Audio.pause();
    //    //obj_Audio.currentTime = 0;
    //    var str_IdAlerta = $(this).attr('id');
    //    $('#hdIdAlerta').val(str_IdAlerta);
    //    $('#mdEvento').modal('show');
    //});

    //$('#mdEvento').find('.modal-footer .btn-aceptar').click(function () {
    //    AlertJQ(3, 'La alerta debe ser mitigada lo más pronto posible.', function () {
    //        $("#mdEvento").modal('hide');
    //    }, {}, true);
    //});


    $('#mdEvento').find('.modal-footer .btn-no').click(function () {
        var str_IdAlerta = $('#hdIdAlerta').val();
        Informar(str_IdAlerta, '');
    });

    $('#mdEvento').find('.modal-footer .btn-si').click(function () {
        ValidarInforme();
        if ($('#frmContent').valid()) {
            var str_IdAlerta = $('#hdIdAlerta').val();
            var str_Observacion = CKEDITOR.instances.txtObservacion.getData();
            Informar(str_IdAlerta, str_Observacion);
        }
        //AlertJQ(2, 'Se realizó correctamente el proceso.', function () {
        //    $('#' + str_IdAlerta).removeClass('alerta');
        //    $('#' + str_IdAlerta).addClass('radar');
        //    $('#' + str_IdAlerta).finish();
        //    $('#' + str_IdAlerta).css('display', 'block');
        //    $("#mdEvento").modal('hide');
        //}, {}, true)
    });

    RichText();
    //ListarAlertas();
    obj_Audio = new Audio('../../Content/alarm.wav');
    obj_Audio.addEventListener('ended', function () {
        this.currentTime = 0;
        this.play();
    }, false);
    google.maps.event.addDomListener(window, 'load', initMap);
}

function ValidarInforme() {
    var settings = $('#frmContent').validate().settings;
    $('.form-group').removeClass('has-error').removeClass('has-success');
    $.extend(settings, {
        ignore: "",
        rules: {
            ctl00$cphBody$txtObservacion: {
                required: function (element) {
                    return (CKEDITOR.instances.txtObservacion.getData() == '');
                }
            },
        },
        messages: {
            ctl00$cphBody$txtObservacion: { required: 'Ingrese la observación' },
        }
    });
}

//function ObtenerEstado() {
//    var obj_Semaforo = ObtenerData('MonitoreoOperador.aspx/ObtenerEstado', {});
//    if (obj_Semaforo != null) {
//        var str_Estado = 'text-green';
//        //if (obj_Semaforo.Running)
//        //    str_Estado += 'green';
//        if(obj_Semaforo.Paused)
//            str_Estado = 'text-yellow';
//        else if(obj_Semaforo.Stopped)
//            str_Estado = 'text-red';

//        $('#iEstado').addClass(str_Estado);
//    }
//}

function IniciarTimer() {
    var int_Tiempo = parseInt($('#hdTiempoAlerta').val());
    obj_gTimer = setTimeout(CargarAlertas, int_Tiempo);
}

function CargarEventos() {
    //Limpia el timer
    if (obj_gTimer != null)
        clearTimeout(obj_gTimer);

    setTimeout(Alertar, 100);
}

function Alertar() {
    $('#radar_1').addClass('alerta');
    $('#radar_2').addClass('alerta');

    $('.alerta').click(function () {
        //obj_Audio.pause();
        //obj_Audio.currentTime = 0;
        var str_IdAlerta = $(this).attr('id');
        $('#hdIdAlerta').val(str_IdAlerta);
        CKEDITOR.instances.txtObservacion.setData('');
        $('#mdEvento').modal('show');
    });

    //obj_Audio.play();
    //AccionDefault(false, 'Monitoreo.aspx/Notificar', {}, function () { Parpadear(); }, null, null, null, 1);
    Parpadear();
}

function Parpadear() {
    $('.alerta').fadeIn(500).delay(500).fadeOut(500, Parpadear);
}
function InicializarEventos() {
    //$(".radar").click(function () {
    //    var str_IdAlerta = $(this).attr('id');
    //    $('#mdEvento').modal('show');
    //});
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
    $('.alerta').fadeIn(500).delay(500).fadeOut(500, Parpadear);
}
function CargarAlertas() {
    AccionDefault(true, "MonitoreoOperador.aspx/CargarAlertas", { 'int_pIdOperador': $('#hdIdOperador').val() },
        function (lst_vAlertas) {
            if (lst_vAlertas.length > 0) {
                var str_RutaIconoAlerta = UrlPath + 'Content/Radar.png';
                var bln_ExistenAlertas = false;
                for (var i = 0; i < lst_vAlertas.length; i++) {
                    var bounds = new google.maps.LatLngBounds(new google.maps.LatLng(parseFloat(lst_vAlertas[i].CoordenadaLatitud), parseFloat(lst_vAlertas[i].CoordenadaLongitud)));
                    if (lst_vAlertas[i].Observacion == null) {
                        overlay = new USGSOverlay(bounds, str_RutaIconoAlerta, map, 'alerta', lst_vAlertas[i].IdAlerta, parseFloat(lst_vAlertas[i].CoordenadaLatitud), parseFloat(lst_vAlertas[i].CoordenadaLongitud));
                        bln_ExistenAlertas = true;
                    }
                    else {
                        if (lst_vAlertas[i].FechaMitigacion == null)
                            overlay = new USGSOverlay(bounds, str_RutaIconoAlerta, map, 'radar', lst_vAlertas[i].IdAlerta, parseFloat(lst_vAlertas[i].CoordenadaLatitud), parseFloat(lst_vAlertas[i].CoordenadaLongitud));
                        else
                            overlay = new USGSOverlay(bounds, str_RutaIconoAlerta, map, 'mitigada', lst_vAlertas[i].IdAlerta, parseFloat(lst_vAlertas[i].CoordenadaLatitud), parseFloat(lst_vAlertas[i].CoordenadaLongitud));
                    }
                }

                if (bln_ExistenAlertas)
                    obj_Audio.play();

            }
            //InicializarEventos();
            ListarAlertas();
            IniciarTimer();
        }, null, null, null, 1, false, false);
}

function ListarAlertas() {
    AccionDefault(true, "MonitoreoOperador.aspx/ListarAlertas",
        {
            'int_pIdOperador': $('#hdIdOperador').val(),
            'int_pIdTipoAlerta': $('#ddlTipoAlerta').val(),
            'dt_pHoraInicial': $('#txtHoraInicio_input').val() != '' ? $('#txtHoraInicio').data("datetimepicker").getDate() : null,
            'dt_pHoraFinal': $('#txtHoraFin_input').val() != '' ? $('#txtHoraFin').data("datetimepicker").getDate() : null
        },
        function (lst_Alertas) {
            var lst_vAlertas = lst_Alertas.Result;

            var div_Alertas = $('#divAlertas');
            div_Alertas.empty();
            var str_HTML = '';
            if (lst_vAlertas.length > 0) {
                for (var i = 0; i < lst_vAlertas.length; i++) {
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
                }
                div_Alertas.append($(str_HTML));
            }
        }, null, null, null, 1)
}

function ObtenerAlerta(int_pIdAlerta) {
    AccionDefault(true, 'MonitoreoOperador.aspx/ObtenerAlerta', { 'int_pIdAlerta': int_pIdAlerta },
        function (obj_pAlerta) {
            if (obj_pAlerta != null) {
                $('#txtTipoAlerta').val(obj_pAlerta.TipoAlerta.Descripcion);
                $('#txtAlertaEstado').val(obj_pAlerta.AlertaEstado.Descripcion);
                $('#txtPosicionInicial').val(obj_pAlerta.PosicionInicial + ' m');
                $('#txtPosicionFinal').val(obj_pAlerta.PosicionFinal + ' m');
                $('#txtFechaAlerta').val(ToDateTimeString(obj_pAlerta.FechaAlerta));
                $('#txtValorUmbral').val(obj_pAlerta.ValorUmbral);
                $('#txtValorUmbralMaximo').val(obj_pAlerta.ValorUmbralMaximo);
                $('#txtCantidadGolpes').val(obj_pAlerta.CantidadGolpes);
                $('#txtCantidadGolpesMaximo').val(obj_pAlerta.CantidadGolpesMaximo);
                if (obj_pAlerta.Observacion != null)
                    CKEDITOR.instances.txtObservacion.setData(obj_pAlerta.Observacion);
                else
                    CKEDITOR.instances.txtObservacion.setData('');

                if (obj_pAlerta.FechaInforme != null)
                    $('.btn-informar').css('display', 'none');
                else
                    $('.btn-informar').css('display', 'block');

                $('#mdEvento').modal('show');
            }
        }, null, null, null, 1)
}

function Informar(int_pIdAlerta, str_pObservacion) {
    Accion('MonitoreoOperador.aspx/InformarAlerta',
        {
            'int_pIdAlerta': int_pIdAlerta,
            'str_pObservacion': str_pObservacion
        },
        function (obj_pResultado) {
            var int_IdAlerta = obj_pResultado.ReturnId;
            //$('#' + int_IdAlerta).removeClass('alerta');
            $("#" + int_IdAlerta).toggleClass('alerta', false);
            $("#" + int_IdAlerta).finish();
            $("#" + int_IdAlerta).toggleClass('radar', true);
            //$('#' + int_IdAlerta).addClass('radar');
            $("#" + int_IdAlerta).css('display', 'block');
            $("#mdEvento").modal('hide');
            //InicializarEventos();
        });
}

function DestroyCkeditor() {
    var hEd = CKEDITOR.instances['txtObservacion'];
    if (hEd) {
        CKEDITOR.instances.txtObservacion.destroy()
    }
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
        if (this.tipo != 'mitigada') {
            div = document.createElement('div');
            div.style.borderStyle = 'none';
            div.style.borderWidth = '0px';
            div.style.position = 'absolute';
            if (this.tipo == 'radar')
                div.style.zIndex = '99';
            else
                div.style.zIndex = '100';

            $(div).addClass(this.tipo);
            $(div).addClass('asignado');
            $(div).css('border-radius', '50px');
            $(div).attr('id', this.id);
            $(div).attr('lat', this.lat);
            $(div).attr('log', this.log);
            if ($(div).hasClass('alerta')) {
                $(div).fadeIn(500).delay(500).fadeOut(500, Parpadear);
            }

            //InicializarEventos();
            // Add the element to the "overlayLayer" pane.
            var panes = this.getPanes();
            panes.overlayMouseTarget.appendChild(div);
        }
    } else {
        div = $("#" + int_Id)[0];
        if (this.tipo == 'mitigada') {
            $(div).removeClass('alerta');
            $(div).removeClass('asignado');
            $(div).removeClass('radar');
            $(div).finish();
            $(div).css('display', 'none');
        } else if (this.tipo == 'radar')
            div.style.zIndex = '99';
        else if (this.tipo == 'alerta')
            div.style.zIndex = '100';
    }

    this.div_ = div;
    $(div).unbind('click');
    $(div).on('click', function () {
        var str_IdAlerta = $(this).attr('id');
        var str_Latitud = $(this).attr('lat');
        var str_Longitud = $(this).attr('log');
        if ($(this).hasClass('alerta')) {
            if ($('#txtObservacion').hasClass('richTextView'))
                $('#txtObservacion').removeClass('richTextView');

            if (!$('#txtObservacion').hasClass('richText'))
                $('#txtObservacion').addClass('richText');

            DestroyCkeditor();
            RichText();
            CKEDITOR.instances.txtObservacion.setData('');
            $('#hdIdAlerta').val(str_IdAlerta);
            $('#txtLatitud').val(str_Latitud);
            $('#txtLongitud').val(str_Longitud);
            ObtenerAlerta(str_IdAlerta);
            $('#btnAsignar').css('display', 'block');
            $('#btnMitigar').css('display', 'none');
            $('#ddlOperador').prop('disabled', false);
            $('#divObservacion').css('display', 'none');
            obj_Audio.pause();
            obj_Audio.currentTime = 0;
            //$('#mdEvento').modal('show');
        }
        else if ($(this).hasClass('radar')) {
            if ($('#txtObservacion').hasClass('richText'))
                $('#txtObservacion').removeClass('richText');

            if (!$('#txtObservacion').hasClass('richTextView'))
                $('#txtObservacion').addClass('richTextView');

            DestroyCkeditor();
            RichTextView();
            CKEDITOR.instances.txtObservacion.setData('');
            $('#hdIdAlerta').val(str_IdAlerta);
            $('#txtLatitud').val(str_Latitud);
            $('#txtLongitud').val(str_Longitud);
            ObtenerAlerta(str_IdAlerta);
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
        div.style.width = 50 + 'px';
        div.style.height = 50 + 'px';
    }
};

USGSOverlay.prototype.onRemove = function () {
    this.div_.parentNode.removeChild(this.div_);
    this.div_ = null;
};