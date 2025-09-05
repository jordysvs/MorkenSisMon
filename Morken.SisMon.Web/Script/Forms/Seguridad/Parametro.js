//Atributos
var int_NumPagina = 1;
var int_TamPagina = 10;
var int_PagMostrar = 9;
var urlEdicion = 'RegistroParametro.aspx';

$(function () {

    CargarModuloSelect("ddlModulo", true, "--Todos--");

    $("#btnModificar").click(function () {
        Editar();
        return false;
    });

    $("#btnBuscar").click(function () {
        ListarParametro(1);
        return false;
    });

    KeyPressEnter("divFiltro",
        function () {
            $("#btnBuscar").click();
        });

    $("#btnBuscar").focus();

    ListarParametro(1);
});

function ObtenerFiltros() {
    var obj_Filtro = new Object();
    obj_Filtro.IdModulo = null;
    if ($("#ddlModulo").val() != '')
        obj_Filtro.IdModulo = $("#ddlModulo").val();
    obj_Filtro.Descripcion = $("#txtDescripcion").val();
    return obj_Filtro;
}

function ListarParametroResultado(obj_pResultado, int_pNumPagina) {
    ListarParametro(int_pNumPagina);
}

function ListarParametro(int_pNumPagina) {
    int_NumPagina = int_pNumPagina;
    var obj_Filtro = ObtenerFiltros();
    var obj_Data = {
        "str_pIdModulo": obj_Filtro.IdModulo,
        "str_pDescripcion": obj_Filtro.Descripcion,
        "int_pNumPagina": int_pNumPagina,
        "int_pTamPagina": int_TamPagina
    };
    AccionDefault(true, "Parametro.aspx/ListarParametro", obj_Data, CargarGrilla, null, null, null, 1);
}

function CargarGrilla(obj_pLista) {

    var obj_TablaPrincipal = $("<table id='tblParametro' class='table table-bordered table-hover dt-responsive'></table>");
    var obj_THead = $("<thead></thead>");
    var obj_FilaPrincipal = $("<tr></tr>");
    obj_FilaPrincipal.append($("<th style='width:65%'>Descripción</th>"));
    obj_FilaPrincipal.append($("<th style='width:10%'>Módulo</th>"));
    obj_FilaPrincipal.append($("<th style='width:25%'>Valor</th>"));
    obj_THead.append(obj_FilaPrincipal);
    obj_TablaPrincipal.append(obj_THead);

    var obj_TBody = $("<tbody></tbody>");
    for (var i = 0; i < obj_pLista.Result.length; i++) {
        obj_FilaPrincipal = $("<tr idmodulo='" + obj_pLista.Result[i].IdModulo + "' idparametro='" + obj_pLista.Result[i].IdParametro + "'></tr>");
        obj_FilaPrincipal.append($("<td>" + obj_pLista.Result[i].Descripcion + "</td>"));
        obj_FilaPrincipal.append($("<td>" + obj_pLista.Result[i].Modulo.Descripcion + "</td>"));
        obj_FilaPrincipal.append($("<td>" + obj_pLista.Result[i].Valor + "</td>"));
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
        ListarParametro(num);
    });

    $("#divGrilla").html('');
    $("#divGrilla").append(obj_TablaPrincipal);

    var obj_Tabla = $('#tblParametro').dataTable({
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
        "bSort": false,
        "bInfo": false,
        "bAutoWidth": false
    });

    $('#tblParametro tbody').on('click', 'tr', function () {
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
    var obj_Registro = null;
    var obj_Seleccion = $('#tblParametro').find('.selected');
    if (obj_Seleccion.length > 0) {
        obj_Registro = new Object();
        obj_Registro.IdModulo = $(obj_Seleccion[0]).attr('idmodulo');
        obj_Registro.IdParametro = $(obj_Seleccion[0]).attr('idparametro');
    }
    else
        AlertJQ(1, 'Seleccione un parámetro.');
    return obj_Registro;
}

function Editar() {
    var obj_Registro = obtenerIdRegistro();
    if (obj_Registro != null) {
        MostrarMensajeCargando();
        document.location.href = urlEdicion + "?id=" + obj_Registro.IdModulo + "&id2=" + obj_Registro.IdParametro;
    }
}

