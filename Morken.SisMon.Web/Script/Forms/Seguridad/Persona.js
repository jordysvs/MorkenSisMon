//Atributos
var int_NumPagina = 1;
var int_TamPagina = 10;
var int_PagMostrar = 9;
var urlEdicion = 'RegistroPersona.aspx';

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
        ListarPersona(1);
        return false;
    });

    KeyPressEnter("divFiltro",
        function () {
            $("#btnBuscar").click();
        });

    $("#btnBuscar").focus();

    ListarPersona(1);
});

function ObtenerFiltros() {
    var obj_Filtro = new Object();
    obj_Filtro.NombreCompleto = $("#txtNombre").val();
    obj_Filtro.IdTipoDocumento = $("#ddlTipoDocumento").val();
    obj_Filtro.NumeroDocumento = $("#txtNroDocumento").val();
    obj_Filtro.Estado = $("#ddlEstado").val();
    return obj_Filtro;
}

function ListarPersonaResultado(obj_pResultado, int_pNumPagina) {
    ListarPersona(int_pNumPagina);
}

function ListarPersona(int_pNumPagina) {
    int_NumPagina = int_pNumPagina;
    var obj_Filtro = ObtenerFiltros();
    var obj_Data = { "str_pNombreCompleto": obj_Filtro.NombreCompleto, "int_pIdTipoDocumento": obj_Filtro.IdTipoDocumento, "str_pNumeroDocumento": obj_Filtro.NumeroDocumento, "str_pEstado": obj_Filtro.Estado, "int_pNumPagina": int_pNumPagina, "int_pTamPagina": int_TamPagina };
    AccionDefault(true, "Persona.aspx/ListarPersona", obj_Data, CargarGrilla, null, null, null, 1);
}

function CargarGrilla(obj_pLista) {

    var obj_TablaPrincipal = $("<table id='tblPersona' class='table table-bordered table-hover dt-responsive'></table>");
    var obj_THead = $("<thead></thead>");
    var obj_FilaPrincipal = $("<tr></tr>");
    obj_FilaPrincipal.append($("<th>Nombre</th>"));
    obj_FilaPrincipal.append($("<th>Tipo Documento</th>"));
    obj_FilaPrincipal.append($("<th>Nro. Documento</th>"));
    obj_FilaPrincipal.append($("<th>Estado</th>"));
    obj_THead.append(obj_FilaPrincipal);
    obj_TablaPrincipal.append(obj_THead);

    var str_TipoDocumento = '';
    var str_Estado = '';
    var obj_TBody = $("<tbody></tbody>");
    for (var i = 0; i < obj_pLista.Result.length; i++) {
        obj_FilaPrincipal = $("<tr value='" + obj_pLista.Result[i].IdPersona + "'></tr>");
        obj_FilaPrincipal.append($("<td style='width:30%'>" + obj_pLista.Result[i].NombreCompleto + "</td>"));
        if (obj_pLista.Result[i].TipoDocumento != null)
            str_TipoDocumento = obj_pLista.Result[i].TipoDocumento.Descripcion;
        obj_FilaPrincipal.append($("<td style='width:15%'>" + str_TipoDocumento + "</td>"));
        obj_FilaPrincipal.append($("<td style='width:15%'>" + obj_pLista.Result[i].NumeroDocumento + "</td>"));
        str_Estado = obj_pLista.Result[i].Estado == 'A' ? "Activo" : "Inactivo";
        obj_FilaPrincipal.append($("<td style='width:15%; text-align:center'>" + str_Estado + "</td>"));
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
        ListarPersona(num);
    });

    $("#divGrilla").html('');
    $("#divGrilla").append(obj_TablaPrincipal);

    var obj_Tabla = $('#tblPersona').dataTable({
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

    $('#tblPersona tbody').on('click', 'tr', function () {
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
    $(obj_AnchorPag).attr('onclick', 'ListarPersona(' + (int_pNumPagina) + ');');
    $(obj_AnchorPag).text(str_pTexto);
    return obj_AnchorPag;
}

function obtenerIdRegistro() {
    var str_Id = null;
    var obj_Seleccion = $('#tblPersona').find('.selected');
    if (obj_Seleccion.length > 0)
        str_Id = $(obj_Seleccion[0]).attr('value');
    else
        AlertJQ(1, 'Seleccione una persona.');
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
        ConfirmJQ('¿Está seguro de activar la persona?', ModificarEstado, [str_Id, 'A']);
    }
}

function Inactivar(int_pId) {
    var str_Id = obtenerIdRegistro();
    if (str_Id != null) {
        ConfirmJQ('¿Está seguro de inactivar la persona?', ModificarEstado, [str_Id, 'I']);
    }
}

function ModificarEstado(str_pId, str_pEstado) {
    Accion('Persona.aspx/ModificarEstado', { "int_pIdPersona": str_pId, "str_pEstado": str_pEstado }, ListarPersonaResultado, [1]);
}
