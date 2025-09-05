var int_NumPagina = 1;
var int_TamPagina = 10;
var int_PagMostrar = 9;

$(function () {
    CargarInicial();
    CargarControles();
});

function CargarInicial() {
}

function CargarControles() {

    $(".datetime").datetimepicker({
        language: 'es',
        format: 'dd/mm/yyyy',
        pickerPosition: "bottom-left",
        autoclose: true,
        minView: 2,
        maxView: 4
    });

    $(".datetime").each(function () {
        $(this).data("datetimepicker").setStartDate((new Date(1900, 0, 1, 0, 0, 0, 0)));
        //$(this).find('input').attr('readonly', 'readonly');
    })

    $(".datetimeinput").mask("99/99/9999");

    $("#btnBuscar").click(function () {
        ListarReporte(1);
        return false;
    });

    $("#btnExportar").click(function () {
        Exportar();
        return false;
    });

    ListarReporte(1);

    KeyPressEnter("divFiltro",
     function () {
         $("#btnBuscar").click();
     });
    $("#btnBuscar").focus();
}

function ObtenerFiltros() {

    var obj_Filtro = new Object();
    obj_Filtro.IdAlertaEstado = $("#ddlAlertaEstado").val();

    obj_Filtro.FechaInicio = null;
    obj_Filtro.FechaInicioToString = '';
    if ($("#txtFechaInicio_input").val() != '') {
        obj_Filtro.FechaInicio = $("#txtFechaInicio").data("datetimepicker").getDate();
        obj_Filtro.FechaInicioToString = $("#txtFechaInicio_input").val();
    }

    obj_Filtro.FechaFin = null;
    obj_Filtro.FechaFinToString = '';
    if ($("#txtFechaFin_input").val() != '') {
        obj_Filtro.FechaFin = $("#txtFechaFin").data("datetimepicker").getDate();
        obj_Filtro.FechaFinToString = $("#txtFechaFin_input").val();
    }

    return obj_Filtro;
}

function ListarReporte(int_pNumPagina) {
    int_NumPagina = int_pNumPagina;
    var obj_Filtro = ObtenerFiltros();
    var obj_Data = {
        "int_pIdAlertaEstado": obj_Filtro.IdAlertaEstado,
        "dt_pFechaAlertaInicio": obj_Filtro.FechaInicio,
        "dt_pFechaAlertaFin": obj_Filtro.FechaFin,
        "int_pNumPagina": int_NumPagina,
        "int_pTamPagina": int_TamPagina,
    };

    AccionDefault(true, "ReporteSistema.aspx/ListarReporte", obj_Data, CargarReporte, null, null, null, 1);
}

function CargarReporte(obj_pLista) {
    var str_Ruta = UrlPath + 'Forms/Gestion/VerMonitoreo.aspx';

    var obj_TablaPrincipal = $("<table id='tblReporte' class='table table-bordered table-hover dt-responsive'></table>");
    var obj_THead = $("<thead></thead>");
    var obj_FilaPrincipal = $("<tr></tr>");
    obj_FilaPrincipal.append($("<th style='width:10%'>Código Error</th>"));
    obj_FilaPrincipal.append($("<th style='width:10%'>Fecha Alerta</th>"));
    obj_FilaPrincipal.append($("<th style='width:15%'>Observación</th>"));
    obj_FilaPrincipal.append($("<th style='width:10%'>Estado</th>"));
    obj_THead.append(obj_FilaPrincipal);
    obj_TablaPrincipal.append(obj_THead);

    var obj_TBody = $("<tbody></tbody>");
    for (var i = 0; i < obj_pLista.Result.length; i++) {
        obj_FilaPrincipal = $("<tr></tr>");
        obj_FilaPrincipal.append($("<td align='center'>" + obj_pLista.Result[i].CodigoError + "</td>"));
        obj_FilaPrincipal.append($("<td align='center'>" + ToDateTimeString(obj_pLista.Result[i].FechaAlerta) + "</td>"));
        obj_FilaPrincipal.append($("<td align='center'>" + obj_pLista.Result[i].Observacion + "</td>"));
        obj_FilaPrincipal.append($("<td align='center'>" + obj_pLista.Result[i].AlertaEstado.Descripcion + "</td>"));
        obj_TBody.append(obj_FilaPrincipal);
    }

    obj_TablaPrincipal.append(obj_TBody);

    var int_TotalPaginas = Math.ceil(obj_pLista.Total / int_TamPagina) == 0 ? 1 : Math.ceil(obj_pLista.Total / int_TamPagina);
    $("#divPaginacionInfo").html("Página " + int_NumPagina + " de " + int_TotalPaginas);

    $("#divPaginacion").unbind();
    $("#divPaginacion").bootpag({
        total: int_TotalPaginas,
        page: int_NumPagina,
        maxVisible: obj_pLista.Total == 0 ? 1 : int_PagMostrar
        //maxVisible: 5 == 0 ? 1 : int_PagMostrar
    }).on('page', function (event, num) {
        ListarReporte(num);
    });

    $("#divGrilla").html('');
    $("#divGrilla").append(obj_TablaPrincipal);

    var obj_Tabla = $('#tblReporte').dataTable({
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
        "bAutoWidth": true
    });
}

function Exportar() {
    var obj_Filtro = ObtenerFiltros();
    $('body').prepend("<form id='excelForm' method='post' action='../../Ashx/Reportes/ReporteSistemaExcelHandler.ashx'>" +
    "<input type='hidden' name='int_pIdAlertaEstado' value='" + obj_Filtro.IdAlertaEstado + "' >" +
    "<input type='hidden' name='dt_pFechaAlertaInicio' value='" + obj_Filtro.FechaInicioToString + "' >" +
    "<input type='hidden' name='dt_pFechaAlertaFin' value='" + obj_Filtro.FechaFinToString + "' >" +
    "</form>");
    $('#excelForm').submit();
    $("#excelForm").remove();
}

