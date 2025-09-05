$(function () {
    ListarModulo();
});

function ListarModulo() {
    var obj_Data = { "str_pPath": GetUrlParameter("ReturnUrl") };
    AccionDefault(true, "Modulo.aspx/ListarModulo", obj_Data, CargarGrilla, null, null, null, 1);
}

function CargarGrilla(obj_pLista) {

    var str_RutaModuloDefecto = ObtenerData(UrlPath + "Modulo.aspx/ObtenerRutaModuloDefecto", {});
    var str_RutaModulo = ObtenerData(UrlPath + "Modulo.aspx/ObtenerRutaModulo", {});
    var divModulo = $("#divModulo");

    var obj_Row = null;
    var obj_Modulo = null;
    var obj_Enlace = null;

    for (var i = 0; i < obj_pLista.length; i++) {
        if (i == 0 || i % 3 == 0) {
            obj_Row = $("<div class='row'></div>");
            divModulo.append(obj_Row);
        }
        obj_Modulo = $("<div class='col-lg-4' align='center'></div>");
        obj_Enlace = $("<a href='#' idmodulo='" + obj_pLista[i].IdModulo + "'></a>");
        obj_Enlace.append(ObtenerModulo(obj_pLista[i], str_RutaModuloDefecto, str_RutaModulo));
        obj_Modulo.append(obj_Enlace);
        obj_Row.append(obj_Modulo);

        $(obj_Enlace).click(function () {
            window.sessionStorage.IdModulo = $(this).attr('idmodulo');
            var str_Url = GetUrlParameter("ReturnUrl");
            if (str_Url == null)
                str_Url = 'Index.aspx';
            document.location.href = str_Url;
        });
    }
}

function ObtenerModulo(obj_pModulo, str_pRutaModuloDefecto, str_pRutaModulo) {
    var int_IdAlmacen = $("#hdIdAlmacen").val();
    var obj_Tabla = $("<table style='height:200px;width:200px;'></table>");
    var obj_Fila = null;

    if (obj_pModulo.Foto != null)
        obj_Fila = $("<tr><td align='center'><img style='max-height:150px;' alt='' onerror='this.onerror=null;this.src=\"" + str_pRutaModuloDefecto + "\";' src='" + UrlPath + str_pRutaModulo + obj_pModulo.Foto.DescripcionFisica + obj_pModulo.Foto.Extension + "' />");
    else
        obj_Fila = $("<tr><td align='center'><img style='max-height:150px;' alt='' src='" + str_pRutaModuloDefecto + "'></img></td></tr>");

    obj_Tabla.append(obj_Fila);
    var obj_Fila = $("<tr><td align='center'>" + obj_pModulo.Descripcion + "</td></tr>");
    obj_Tabla.append(obj_Fila);
    return obj_Tabla;
}