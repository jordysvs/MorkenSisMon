//Atributos
var int_NumPagina = 1;
var int_TamPagina = 10;
var int_PagMostrar = 9;
var urlEdicion = 'RegistroAlmacen.aspx';

$(function () {

    CargarModuloSelect("ddlModulo", true, "--Todos--");

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
        ListarAlmacen(1);
        return false;
    });

    KeyPressEnter("divFiltro",
        function () {
            $("#btnBuscar").click();
        });

    $("#btnBuscar").focus();

    ListarAlmacen(1);
});

function ObtenerFiltros() {
    var obj_Filtro = new Object();
    obj_Filtro.IdModulo = null;
    if ($("#ddlModulo").val() != '')
        obj_Filtro.IdModulo = $("#ddlModulo").val();
    obj_Filtro.Descripcion = $("#txtDescripcion").val();
    obj_Filtro.Estado = $("#ddlEstado").val();
    return obj_Filtro;
}

function ListarAlmacenResultado(obj_pResultado, int_pNumPagina) {
    ListarAlmacen(int_pNumPagina);
}

function ListarAlmacen(int_pNumPagina) {
    int_NumPagina = int_pNumPagina;
    var obj_Filtro = ObtenerFiltros();
    var obj_Data = {
        "str_pIdModulo": obj_Filtro.IdModulo,
        "str_pDescripcion": obj_Filtro.Descripcion,
        "str_pEstado": obj_Filtro.Estado,
        "int_pNumPagina": int_pNumPagina,
        "int_pTamPagina": int_TamPagina
    };
    AccionDefault(true, "Almacen.aspx/ListarAlmacen", obj_Data, CargarGrilla, null, null, null, 1);
}

function CargarGrilla(obj_pLista) {

    var obj_TablaPrincipal = $("<table id='tblAlmacen' class='table table-bordered table-hover dt-responsive'></table>");
    var obj_THead = $("<thead></thead>");
    var obj_FilaPrincipal = $("<tr></tr>");
    obj_FilaPrincipal.append($("<th style='width:5%'>Id</th>"));
    obj_FilaPrincipal.append($("<th style='width:25%'>Descripción</th>"));
    obj_FilaPrincipal.append($("<th style='width:35%'>Ruta</th>"));
    obj_FilaPrincipal.append($("<th style='width:20%'>Módulo</th>"));
    obj_FilaPrincipal.append($("<th style='width:15%; text-align:center'>Estado</th>"));
    obj_THead.append(obj_FilaPrincipal);
    obj_TablaPrincipal.append(obj_THead);

    var str_Estado = '';
    var obj_TBody = $("<tbody></tbody>");
    for (var i = 0; i < obj_pLista.Result.length; i++) {
        obj_FilaPrincipal = $("<tr value='" + obj_pLista.Result[i].IdAlmacen + "'></tr>");
        obj_FilaPrincipal.append($("<td>" + obj_pLista.Result[i].IdAlmacen + "</td>"));
        obj_FilaPrincipal.append($("<td>" + obj_pLista.Result[i].Descripcion + "</td>"));
        obj_FilaPrincipal.append($("<td>" + obj_pLista.Result[i].Ruta + "</td>"));
        obj_FilaPrincipal.append($("<td>" + obj_pLista.Result[i].ModuloDescripcion + "</td>"));
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
        ListarAlmacen(num);
    });

    $("#divGrilla").html('');
    $("#divGrilla").append(obj_TablaPrincipal);

    var obj_Tabla = $('#tblAlmacen').dataTable({
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

    $('#tblAlmacen tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            obj_Tabla.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });
}

function obtenerIdRegistro() {
    var str_Id = null;
    var obj_Seleccion = $('#tblAlmacen').find('.selected');
    if (obj_Seleccion.length > 0)
        str_Id = $(obj_Seleccion[0]).attr('value');
    else
        AlertJQ(1, 'Seleccione un almacén.');
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
        ConfirmJQ('¿Está seguro de activar el almacén?', ModificarEstado, [str_Id, 'A']);
    }
}

function Inactivar(int_pId) {
    var str_Id = obtenerIdRegistro();
    if (str_Id != null) {
        ConfirmJQ('¿Está seguro de inactivar el almacén?', ModificarEstado, [str_Id, 'I']);
    }
}

function ModificarEstado(str_pId, str_pEstado) {
    Accion('Almacen.aspx/ModificarEstado', { "int_pIdAlmacen": str_pId, "str_pEstado": str_pEstado }, ListarAlmacenResultado, [1]);
}
