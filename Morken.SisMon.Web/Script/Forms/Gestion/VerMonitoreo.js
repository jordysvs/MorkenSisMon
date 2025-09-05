var obj_gTimer = null;
var int_gIndiceRadar = 1;
var map = null;

function initMap() {
    var mapOptions = {
        center: new google.maps.LatLng(28.670106, 77.214455),
        zoom: 9, // Zoom Level
        mapTypeId: 'satellite'
    };

    map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
    ListarKML();
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
    CargarControles();
    //CargarEventos();
    ObtenerAlerta();
});


function CargarControles() {
    $('.evento').click(function () {
        $('#lnkLateral').click();
    });

    $('#mdEvento').find('.modal-footer .btn-aceptar').click(function () {
        AlertJQ(3, 'La alerta debe ser mitigada lo más pronto posible.', function () {
            $("#mdEvento").modal('hide');
        }, {}, true);
    });

    $('#mdEvento').find('.modal-footer .btn-mitigar').click(function () {
        AlertJQ(2, 'Se realizó correctamente el proceso.', function () {
            $("#mdEvento").modal('hide');
        }, {}, true)
    });
    google.maps.event.addDomListener(window, 'load', initMap);
}

function CargarEventos() {
    //Limpia el timer
    if (obj_gTimer != null)
        clearTimeout(obj_gTimer);

    setTimeout(Alertar, 8000);
}

function Alertar() {
    if (int_gIndiceRadar == 4)
        int_gIndiceRadar = 1;

    var int_IndiceAnterior = (int_gIndiceRadar - 1 == 0) ? 3 : int_gIndiceRadar - 1;

    $('#radar_' + int_gIndiceRadar).removeClass('radar');
    $('#radar_' + int_gIndiceRadar).addClass('alerta');
    $('#radar_' + int_IndiceAnterior).removeClass('alerta');
    $('#radar_' + int_IndiceAnterior).addClass('radar');

    int_gIndiceRadar++;
    Parpadear();
    //AccionDefault(false, 'Monitoreo.aspx/Notificar', {}, function () { Parpadear(); }, null, null, null, 1);
}

function Parpadear() {
    $('.alerta').fadeIn(500).delay(500).fadeOut(500, Parpadear);
}


function ObtenerAlerta() {
    AccionDefault(true, 'VerMonitoreo.aspx/ObtenerAlerta', { 'int_pIdAlerta': $('#hdIdAlerta').val() },
        function (obj_Alerta) {
            if (obj_Alerta != null) {
                var div_Alertas = $('#divAlertas');

                var str_Usuario = '';
                if (obj_Alerta.IdUsuario != null)
                    str_Usuario = obj_Alerta.Usuario.Persona.NombreCompleto;

                var str_Operador = '';
                if (obj_Alerta.IdOperador != null)
                    str_Operador = obj_Alerta.Operador.Persona.NombreCompleto;

                var str_FechaAsignacion = '';
                if (obj_Alerta.FechaAsignacion != null)
                    str_FechaAsignacion = ToDateTimeString(obj_Alerta.FechaAsignacion);

                var str_FechaInforme = '';
                if (obj_Alerta.FechaInforme != null)
                    str_FechaInforme = ToDateTimeString(obj_Alerta.FechaInforme);

                var str_FechaMitigacion = '';
                if (obj_Alerta.FechaMitigacion != null)
                    str_FechaMitigacion = ToDateTimeString(obj_Alerta.FechaMitigacion);

                var str_HTML = '';
                str_HTML += '<hr style="color: white;" />';
                str_HTML += '<div class="row">';
                str_HTML += '<div class="col-lg-12">';
                str_HTML += '<div class="form-group">';
                str_HTML += '<label>Fecha:</label>&nbsp;';
                str_HTML += '<span>' + ToDateTimeString(obj_Alerta.FechaAlerta) + '</span>';
                str_HTML += '</div>';
                str_HTML += '<div class="form-group">';
                str_HTML += '<label>Estado:</label>&nbsp;&nbsp;';
                str_HTML += '<span>' + obj_Alerta.AlertaEstado.Descripcion + '</span>';
                str_HTML += '</div>';
                str_HTML += '<div class="form-group">';
                str_HTML += '<label>Tipo de Alerta:</label>&nbsp;';
                str_HTML += '<span>' + obj_Alerta.TipoAlerta.Descripcion + '</span>';
                str_HTML += '</div>';
                str_HTML += '<div class="form-group">';
                str_HTML += '<label>Pos. Inicial:</label>&nbsp;&nbsp;';
                str_HTML += '<span>' + obj_Alerta.PosicionInicial + '</span>';
                str_HTML += '</div>';
                str_HTML += '<div class="form-group">';
                str_HTML += '<label>Pos. Final:</label>&nbsp;&nbsp;';
                str_HTML += '<span>' + obj_Alerta.PosicionFinal + '</span>';
                str_HTML += '</div>';
                str_HTML += '<div class="form-group">';
                str_HTML += '<label>Valor Umbral:</label>&nbsp;&nbsp;';
                str_HTML += '<span>' + (obj_Alerta.ValorUmbral != null ? obj_Alerta.ValorUmbral : '') + '</span>';
                str_HTML += '</div>';
                str_HTML += '<div class="form-group">';
                str_HTML += '<label>Valor Max. Umbral:</label>&nbsp;&nbsp;';
                str_HTML += '<span>' + (obj_Alerta.ValorUmbralMaximo != null ? obj_Alerta.ValorUmbral : '') + '</span>';
                str_HTML += '</div>';
                str_HTML += '<div class="form-group">';
                str_HTML += '<label>Cant. Golpes:</label>&nbsp;&nbsp;';
                str_HTML += '<span>' + (obj_Alerta.CantidadGolpes != null ? obj_Alerta.ValorUmbral : '') + '</span>';
                str_HTML += '</div>';
                str_HTML += '<div class="form-group">';
                str_HTML += '<label>Cant. Max. Golpes:</label>&nbsp;&nbsp;';
                str_HTML += '<span>' + (obj_Alerta.CantidadGolpesMaximo != null ? obj_Alerta.ValorUmbral : '') + '</span>';
                str_HTML += '</div>';
                str_HTML += '<div class="form-group">';
                str_HTML += '<label>Latitud:</label>&nbsp;&nbsp;';
                str_HTML += '<span>' + obj_Alerta.CoordenadaLatitud + '</span>';
                str_HTML += '</div>';
                str_HTML += '<div class="form-group">';
                str_HTML += '<label>Longitud:</label>&nbsp;&nbsp;';
                str_HTML += '<span>' + obj_Alerta.CoordenadaLongitud + '</span>';
                str_HTML += '</div>';
                str_HTML += '<div class="form-group">';
                str_HTML += '<label>Supervisor:</label>&nbsp;&nbsp;';
                str_HTML += '<span>' + str_Usuario + '</span>';
                str_HTML += '</div>';
                str_HTML += '<div class="form-group">';
                str_HTML += '<label>Fecha Asignación:</label>&nbsp;&nbsp;';
                str_HTML += '<span>' + str_FechaAsignacion + '</span>';
                str_HTML += '</div>';
                str_HTML += '<div class="form-group">';
                str_HTML += '<label>Operador:</label>&nbsp;&nbsp;';
                str_HTML += '<span>' + str_Operador + '</span>';
                str_HTML += '</div>';
                str_HTML += '<div class="form-group">';
                str_HTML += '<label>Fecha Informe:</label>&nbsp;&nbsp;';
                str_HTML += '<span>' + str_FechaInforme + '</span>';
                str_HTML += '</div>';
                str_HTML += '<div class="form-group">';
                str_HTML += '<label>Fecha Mitigación:</label>&nbsp;&nbsp;';
                str_HTML += '<span>' + str_FechaMitigacion + '</span>';
                str_HTML += '</div>';
                str_HTML += '<div class="form-group">';
                str_HTML += '<label>Observación:</label><br>';
                str_HTML += '<span>' + (obj_Alerta.Observacion == null ? '' : obj_Alerta.Observacion) + '</span>';
                str_HTML += '</div>';
                str_HTML += '</div>';
                str_HTML += '</div>';
                str_HTML += '<hr style="color: white;" />';

                div_Alertas.append($(str_HTML));

                var str_RutaIconoAlerta = UrlPath + 'Content/Radar.png';
                var bounds = new google.maps.LatLngBounds(new google.maps.LatLng(parseFloat(obj_Alerta.CoordenadaLatitud), parseFloat(obj_Alerta.CoordenadaLongitud)));
                overlay = new USGSOverlay(bounds, str_RutaIconoAlerta, map, 'radar', obj_Alerta.IdAlerta, parseFloat(obj_Alerta.CoordenadaLatitud), parseFloat(obj_Alerta.CoordenadaLongitud));
            }
        }, null, null, null, 1)
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

    var div = document.createElement('div');
    div.style.borderStyle = 'none';
    div.style.borderWidth = '0px';
    div.style.position = 'absolute';
    $(div).addClass(this.tipo);
    $(div).addClass('asignado');
    $(div).css('border-radius', '50px');
    $(div).attr('id', this.id);
    $(div).attr('lat', this.lat);
    $(div).attr('log', this.log);

    this.div_ = div;

    if ($(div).hasClass('alerta')) {
        $(div).fadeIn(500).delay(500).fadeOut(500, Parpadear);
    }

    //InicializarEventos();
    // Add the element to the "overlayLayer" pane.
    var panes = this.getPanes();
    panes.overlayMouseTarget.appendChild(div);
};

USGSOverlay.prototype.draw = function () {
    var overlayProjection = this.getProjection();
    var sw = overlayProjection.fromLatLngToDivPixel(this.bounds_.getSouthWest());
    var ne = overlayProjection.fromLatLngToDivPixel(this.bounds_.getNorthEast());
    var div = this.div_;
    div.style.left = sw.x + 'px';
    div.style.top = ne.y + 'px';
    div.style.width = 50 + 'px';
    div.style.height = 50 + 'px';
};

USGSOverlay.prototype.onRemove = function () {
    this.div_.parentNode.removeChild(this.div_);
    this.div_ = null;
};