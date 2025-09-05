//Atributos
var int_NumPagina = 1;
var int_TamPagina = 10;
var int_PagMostrar = 9;
var urlEdicion = 'RegistroModulo.aspx';

$(function () {

    $("#btnAgregar").click(function () {
        MostrarMensajeCargando();
        document.location.href = urlEdicion;
        return false;
    });

    $("#btnModificar").click(function () {
        Editar();
        return false;
    });

    $("#btnActivar").click(function () {
        Activar();
        return false;
    });

    $("#btnInactivar").click(function () {
        Inactivar();
        return false;
    });

    $("#btnBuscar").click(function () {
        ListarModulo(1);
        return false;
    });

    KeyPressEnter("divFiltro",
        function () {
            $("#btnBuscar").click();
        });

    $("#btnBuscar").focus();

    ListarModulo(1);
});

function ObtenerFiltros() {
    var obj_Filtro = new Object();
    obj_Filtro.Codigo = $("#txtCodigo").val();
    obj_Filtro.Descripcion = $("#txtDescripcion").val();
    obj_Filtro.Estado = $("#ddlEstado").val();
    return obj_Filtro;
}

function ListarModulo(int_pNumPagina) {
    int_NumPagina = int_pNumPagina;
    var obj_Filtro = ObtenerFiltros();
    var obj_Data = {
        "str_pCodigo": obj_Filtro.Codigo,
        "str_pDescripcion": obj_Filtro.Descripcion,
        "str_pEstado": obj_Filtro.Estado,
        "int_pNumPagina": int_pNumPagina,
        "int_pTamPagina": int_TamPagina
    };
    AccionDefault(true, "Modulo.aspx/ListarModulo", obj_Data, CargarGrilla, null, null, null, 1);
}

function CargarGrilla(obj_pLista) {

    var obj_TablaPrincipal = $("<table id='tblModulo' class='table table-bordered table-hover dt-responsive'></table>");
    var obj_THead = $("<thead></thead>");
    var obj_FilaPrincipal = $("<tr></tr>");
    obj_FilaPrincipal.append($("<th style='width:15%'>Código</th>"));
    obj_FilaPrincipal.append($("<th style='width:70%'>Descripción</th>"));
    obj_FilaPrincipal.append($("<th style='width:15%; text-align:center'>Estado</th>"));
    obj_THead.append(obj_FilaPrincipal);
    obj_TablaPrincipal.append(obj_THead);

    var str_Estado = '';
    var obj_TBody = $("<tbody></tbody>");
    for (var i = 0; i < obj_pLista.Result.length; i++) {
        obj_FilaPrincipal = $("<tr value='" + obj_pLista.Result[i].IdModulo + "'></tr>");
        obj_FilaPrincipal.append($("<td>" + obj_pLista.Result[i].IdModulo + "</td>"));
        obj_FilaPrincipal.append($("<td>" + obj_pLista.Result[i].Descripcion + "</td>"));
        str_Estado = obj_pLista.Result[i].Estado == 'A' ? "Activo" : "Inactivo";
        obj_FilaPrincipal.append($("<td>" + str_Estado + "</td>"));
        obj_TBody.append(obj_FilaPrincipal);

        obj_FilaPrincipal.dblclick(
        function () {
            $(this).addClass('selected');
            Editar();
        });
    }
    obj_TablaPrincipal.append(obj_TBody);

    var int_TotalPaginas = Math.ceil(obj_pLista.Total / int_TamPagina) == 0 ? 1 : Math.ceil(obj_pLista.Total / int_TamPagina);
    $("#divPaginacionInfo").html("Página " + int_NumPagina + " de " + int_TotalPaginas);

    $("#divPaginacion").unbind();
    $("#divPaginacion").bootpag({
        total: int_TotalPaginas,
        page: int_NumPagina,
        maxVisible: obj_pLista.Total == 0 ? 1 : int_PagMostrar
    }).on('page', function (event, num) {
        ListarModulo(num);
    });

    $("#divGrilla").html('');
    $("#divGrilla").append(obj_TablaPrincipal);

    var obj_Tabla = $('#tblModulo').dataTable({
        "language": {
            "lengthMenu": "Mostrar _MENU_ registros por página",
            "zeroRecords": "No se encontraron registros",
            "info": "Página _PAGE_ de _PAGES_",
            "infoEmpty": "",
            "infoFiltered": "(Filtrado hasta _MAX_ total registros)"
        },
        "processing": false,
        "bPaginate": false,
        "bLengthChange": false,
        "bFilter": false,
        "bSort": true,
        "bInfo": false,
        "bAutoWidth": false
    });

    $('#tblModulo tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            obj_Tabla.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });
}

function CargarPaginacion(obj_pLista) {
    var int_vNumPagina = int_NumPagina;
    var int_vPagMostrar = int_PagMostrar;
    var int_TotalRegistros = obj_pLista.Total;

    var obj_Ul = $("<ul class='pagination'></ul>");
    var obj_Li = $("<li class='paginate_button'></li>");
    obj_Ul.append(obj_Li);
    obj_Li.append(CrearNumeroPagina(int_vNumPagina, int_vNumPagina));

    while ((int_TotalRegistros - ((int_NumPagina - 1) * int_TamPagina) - int_TamPagina) > 0) {
        int_TotalRegistros = (int_TotalRegistros - int_TamPagina);
        int_vNumPagina++;
        int_vPagMostrar--;
        if (int_vPagMostrar > 0)
            obj_Li.append(CrearNumeroPagina(int_vNumPagina, int_vNumPagina));
        else {
            if (int_TotalRegistros > 0) {
                obj_Li.append(CrearNumeroPagina(int_vNumPagina, ">"));
                break;
            }
        }
    }

    int_vNumPagina = int_NumPagina;
    while ((int_TotalRegistros - int_TamPagina) > 0) {
        int_TotalRegistros = (int_TotalRegistros - int_TamPagina);
        int_vNumPagina--;
        int_vPagMostrar--;
        if (int_vPagMostrar > 0)
            obj_Li.prepend(CrearNumeroPagina(int_vNumPagina, int_vNumPagina));
    }
    if (int_vNumPagina > 1)
        obj_Li.prepend(CrearNumeroPagina(int_vNumPagina, "<"));

    var int_TotalPaginas = Math.ceil(obj_pLista.Total / int_TamPagina);
    int_TotalPaginas = int_TotalPaginas == 0 ? 1 : int_TotalPaginas;
    $("#divPaginacionInfo").html("Página " + int_NumPagina + " de " + int_TotalPaginas);

    return obj_Ul;
}

function CrearNumeroPagina(int_pNumPagina, str_pTexto) {
    var obj_AnchorPag = $("<a></a>");
    $(obj_AnchorPag).attr('href', '#');
    $(obj_AnchorPag).attr('onclick', 'ListarModulo(' + (int_pNumPagina) + ');');
    $(obj_AnchorPag).text(str_pTexto);
    return obj_AnchorPag;
}

function obtenerIdRegistro() {
    var str_Id = null;
    var obj_Seleccion = $('#tblModulo').find('.selected');
    if (obj_Seleccion.length > 0)
        str_Id = $(obj_Seleccion[0]).attr('value');
    else
        AlertJQ(1, 'Seleccione un módulo.');
    return str_Id;
}

function Editar() {
    var str_Id = obtenerIdRegistro();
    if (str_Id != null) {
        MostrarMensajeCargando();
        document.location.href = urlEdicion + "?id=" + str_Id;
    }
}

function Activar() {
    var str_Id = obtenerIdRegistro();
    if (str_Id != null) {
        ConfirmJQ('¿Está seguro de activar el módulo?', ModificarEstado, [str_Id, 'A']);
    }
}

function Inactivar(int_pId) {
    var str_Id = obtenerIdRegistro();
    if (str_Id != null) {
        ConfirmJQ('¿Está seguro de inactivar el módulo?', ModificarEstado, [str_Id, 'I']);
    }
}

function ModificarEstado(str_pId, str_pEstado) {
    Accion('Modulo.aspx/ModificarEstado', {
        "str_pIdModulo": str_pId,
        "str_pEstado": str_pEstado
    }, function () {
        ListarModulo(1);
    });
}
