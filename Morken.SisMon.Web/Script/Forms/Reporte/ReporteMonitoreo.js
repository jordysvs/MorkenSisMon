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

    $('#chkEventoSinAlerta').bootstrapSwitch();
    
    $('.icheck').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });

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
    obj_Filtro.IdTipoAlerta = $("#ddlTipoAlerta").val();


    if ($("#chkEventoSinAlerta").prop('checked')) {
        obj_Filtro.PosicionInicial = -1;
        obj_Filtro.PosicionFinal = -1;
        obj_Filtro.PosicionInicialString = '-1';
        obj_Filtro.PosicionFinalString = '-1';

    } else {
        obj_Filtro.PosicionInicial = null;
        obj_Filtro.PosicionInicialString = ''
        obj_Filtro.PosicionFinal = null;
        obj_Filtro.PosicionFinalString = '';
    }

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
        "int_pIdTipoAlerta": obj_Filtro.IdTipoAlerta,
        "int_pPosicionInicial":obj_Filtro.PosicionInicial,
        "int_pPosicionFinal": obj_Filtro.PosicionFinal,
        "dt_pFechaAlertaInicio": obj_Filtro.FechaInicio,
        "dt_pFechaAlertaFin": obj_Filtro.FechaFin,
        "int_pNumPagina": int_NumPagina,
        "int_pTamPagina": int_TamPagina,
    };

    AccionDefault(true, "ReporteMonitoreo.aspx/ListarReporte", obj_Data, CargarReporte, null, null, null, 1);

    obj_DataGraficos = {
        "int_pIdTipoAlerta": obj_Filtro.IdTipoAlerta,
        "int_pPosicionInicial": obj_Filtro.PosicionInicial,
        "int_pPosicionFinal": obj_Filtro.PosicionFinal,
        "dt_pFechaAlertaInicio": obj_Filtro.FechaInicio,
        "dt_pFechaAlertaFin": obj_Filtro.FechaFin,
        "int_pNumPagina": null,
        "int_pTamPagina": null,
    };
    AccionDefault(true, "ReporteMonitoreo.aspx/ListarReporte", obj_DataGraficos, ListarGraficoAlertasTipo, null, null, null, 1);
    AccionDefault(true, "ReporteMonitoreo.aspx/ListarEventosDiarios", obj_DataGraficos, ListarGraficoAlertasDiarias, null, null, null, 1);
}

function CargarReporte(obj_pLista) {
    var str_Ruta = UrlPath + 'Forms/Gestion/VerMonitoreo.aspx?id=';

    var obj_TablaPrincipal = $("<table id='tblReporte' class='table table-bordered table-hover dt-responsive'></table>");
    var obj_THead = $("<thead></thead>");
    var obj_FilaPrincipal = $("<tr></tr>");
    obj_FilaPrincipal.append($("<th style='width:5%'></th>"));
    obj_FilaPrincipal.append($("<th style='width:8%'>Tipo de Alerta</th>"));
    obj_FilaPrincipal.append($("<th style='width:10%'>Estado</th>"));
    obj_FilaPrincipal.append($("<th style='width:5%'>Posición Inicial</th>"));
    obj_FilaPrincipal.append($("<th style='width:5%'>Posición Final</th>"));
    obj_FilaPrincipal.append($("<th style='width:5%'>Valor Umbral</th>"));
    obj_FilaPrincipal.append($("<th style='width:5%'>Valor Pico</th>"));
    obj_FilaPrincipal.append($("<th style='width:5%'>Cantidad de Golpes</th>"));
    obj_FilaPrincipal.append($("<th style='width:5%'>Tolerancia</th>"));
    obj_FilaPrincipal.append($("<th style='width:8%'>Supervisor </th>"));
    obj_FilaPrincipal.append($("<th style='width:10%'>Fecha Alerta</th>"));
    obj_FilaPrincipal.append($("<th style='width:8%'>Tpo. Demora Asignación</th>"));
    obj_FilaPrincipal.append($("<th style='width:8%'>Tpo. Demora Informe</th>"));
    obj_FilaPrincipal.append($("<th style='width:10%'>Fecha Mitigación</th>"));
    obj_FilaPrincipal.append($("<th style='width:8%'>Tpo. Mitigación</th>"));
    obj_FilaPrincipal.append($("<th style='width:8%'>Usuario Mitigación</th>"));
    obj_THead.append(obj_FilaPrincipal);
    obj_TablaPrincipal.append(obj_THead);

    var obj_TBody = $("<tbody></tbody>");
    for (var i = 0; i < obj_pLista.Result.length; i++) {
        var str_Usuario = '';
        if (obj_pLista.Result[i].IdUsuario != null)
            str_Usuario = obj_pLista.Result[i].Usuario.Persona.NombreCompleto;

        var str_Operador = '';
        if (obj_pLista.Result[i].IdOperador != null)
            str_Operador = obj_pLista.Result[i].Operador.Persona.NombreCompleto;

        var str_TiempoAsignacion = '';
        if (obj_pLista.Result[i].TiempoDemoraAsignacion != null)
            str_TiempoAsignacion = obj_pLista.Result[i].TiempoDemoraAsignacion + ' hora(s)';

        var str_TiempoInforme = '';
        if (obj_pLista.Result[i].TiempoDemoraInforme != null)
            str_TiempoInforme = obj_pLista.Result[i].TiempoDemoraInforme + ' hora(s)';

        var str_TiempoMitigacion = '';
        if (obj_pLista.Result[i].TiempoDemoraMitigacion != null)
            str_TiempoMitigacion = obj_pLista.Result[i].TiempoDemoraMitigacion + ' hora(s)';

        var str_FechaMitigacion = '';
        if (obj_pLista.Result[i].FechaMitigacion != null)
            str_FechaMitigacion = ToDateTimeString(obj_pLista.Result[i].FechaMitigacion);

        obj_FilaPrincipal = $("<tr></tr>");
        obj_FilaPrincipal.append($("<td align='center'><a href='" + str_Ruta + obj_pLista.Result[i].IdAlerta + "' target='_blank' class='btn btn-default btn-sm btnVer'><i class='fa fa-eye' ></i></a></td>"));
        obj_FilaPrincipal.append($("<td align='center'>" + obj_pLista.Result[i].TipoAlerta.Descripcion + "</td>"));
        obj_FilaPrincipal.append($("<td align='center'>" + obj_pLista.Result[i].AlertaEstado.Descripcion + "</td>"));
        obj_FilaPrincipal.append($("<td align='center'>" + obj_pLista.Result[i].PosicionInicial + "</td>"));
        obj_FilaPrincipal.append($("<td align='center'>" + obj_pLista.Result[i].PosicionFinal + "</td>"));
        obj_FilaPrincipal.append($("<td align='center'>" + (obj_pLista.Result[i].ValorUmbral != null ? obj_pLista.Result[i].ValorUmbral : '') + "</td>"));
        obj_FilaPrincipal.append($("<td align='center'>" + (obj_pLista.Result[i].ValorUmbralMaximo != null ? obj_pLista.Result[i].ValorUmbralMaximo : '') + "</td>"));
        obj_FilaPrincipal.append($("<td align='center'>" + (obj_pLista.Result[i].CantidadGolpes != null ? obj_pLista.Result[i].CantidadGolpes : '') + "</td>"));
        obj_FilaPrincipal.append($("<td align='center'>" + (obj_pLista.Result[i].CantidadGolpesMaximo != null ? obj_pLista.Result[i].CantidadGolpesMaximo : '') + "</td>"));
        obj_FilaPrincipal.append($("<td align='center'>" + str_Usuario + "</td>"));
        obj_FilaPrincipal.append($("<td align='center'>" + ToDateTimeString(obj_pLista.Result[i].FechaAlerta) + "</td>"));
        obj_FilaPrincipal.append($("<td align='center'>" + str_TiempoAsignacion + "</td>"));
        obj_FilaPrincipal.append($("<td align='center'>" + str_TiempoInforme + "</td>"));
        obj_FilaPrincipal.append($("<td align='center'>" + str_FechaMitigacion + "</td>"));
        obj_FilaPrincipal.append($("<td align='center'>" + str_TiempoMitigacion + "</td>"));
        obj_FilaPrincipal.append($("<td align='center'>" + str_Operador + "</td>"));
        obj_TBody.append(obj_FilaPrincipal);
    }

    obj_TablaPrincipal.append(obj_TBody);

    var int_TotalPaginas = Math.ceil(obj_pLista.Total / int_TamPagina) == 0 ? 1 : Math.ceil(obj_pLista.Total / int_TamPagina);
    //var int_TotalPaginas = Math.ceil(5 / int_TamPagina) == 0 ? 1 : Math.ceil(5 / int_TamPagina);
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

function ListarGraficoAlertasTipo(obj_pLista) {

    var lst_Lista = obj_pLista.Result;

    var obj_Serie1 = [];
    var obj_Serie2 = [];
    var obj_Descripcion = [];

    var int_Low = 0;
    var int_High = 0;
    for (var j = 0; j < lst_Lista.length; j++) {
        if (lst_Lista[j].IdTipoAlerta == $('#hdIdStrainHigh').val())
            int_High++;
        else if (lst_Lista[j].IdTipoAlerta == $('#hdIdStrainLow').val())
            int_Low++;

        //int_Low += lst_Lista[j].Adjudicado;
        //int_High += lst_Lista[j].Vacante;
    }
    obj_Serie1.push([0, int_Low]);
    obj_Serie2.push([1, int_High]);

    obj_Descripcion.push([0, "Straing_Low"]);
    obj_Descripcion.push([1, "Strain_High"]);

    var Serie1 = {
        label: "Straing_Low",
        color: "#20C31D",
        data: obj_Serie1,
        lines: { show: false },
        bars: { show: true, barWidth: 1, align: 'center' }
    }

    var Serie2 = {
        label: "Strain_High",
        color: "#7FD4FF",
        data: obj_Serie2,
        lines: { show: false },
        bars: { show: true, barWidth: 1, align: 'center' }
    }

    somePlot = $.plot("#divGraficoAlertas", [Serie1, Serie2], {
        grid: {
            hoverable: true,
            borderWidth: 1,
            borderColor: "#f3f3f3",
            tickColor: "#f3f3f3"
        },
        series: {
            stack: 1,
            bars: {
                show: true,
                barWidth: 1,
                align: "center"
            },
            valueLabels:
        {
            show: true,
            yoffset: 0,
            align: 'center',
            valign: 'above',
            font: "8pt Arial",
            fontcolor: 'black',
        }
        },
        xaxis: {
            mode: "categories",
            ticks: obj_Descripcion
        },
        tooltip: true,
        tooltipOpts: {
            content: '%s: %y'
        },
        legend: { show: true, noColumns: 2, container: $("#divLegendAlertas"), backgroundColor: 'white' }
    });
}

function ListarGraficoAlertasDiarias(obj_pLista) {
    var lst_Lista = obj_pLista;

    var obj_Serie1 = [];
    var obj_Serie2 = [];
    var obj_Serie3 = [];
    var obj_Serie4 = [];
    var obj_Serie5 = [];
    var obj_Descripcion = [];

    var int_PrimerDia = 0;
    var int_SegundoDia = 0;
    var int_TercerDia = 0;
    var int_CuartoDia = 0;
    var int_QuintoDia = 0;

    var obj_DataDefecto = [];
    obj_DataDefecto.push([0, 0]);

    var Serie1 = {
        label: '',
        color: "#20C31D",
        data: obj_DataDefecto,
        lines: { show: false },
        bars: { show: true, barWidth: 1, align: 'center' }
    };
    var Serie2 = {
        label: '',
        color: "#7FD4FF",
        data: obj_DataDefecto,
        lines: { show: false },
        bars: { show: true, barWidth: 1, align: 'center' }
    };
    var Serie3 = {
        label: '',
        color: "#20C31D",
        data: obj_DataDefecto,
        lines: { show: false },
        bars: { show: true, barWidth: 1, align: 'center' }
    };
    var Serie4 = {
        label: '',
        color: "#7FD4FF",
        data: obj_DataDefecto,
        lines: { show: false },
        bars: { show: true, barWidth: 1, align: 'center' }
    };
    var Serie5 = {
        label: '',
        color: "#20C31D",
        data: obj_DataDefecto,
        lines: { show: false },
        bars: { show: true, barWidth: 1, align: 'center' }
    };
    for (var i = 0; i < lst_Lista.length; i++) {
        var str_FechaAlerta = lst_Lista[i].FechaAlertaToString;
        if (i == 0) {
            obj_Descripcion.push([i, str_FechaAlerta]);
            int_PrimerDia = lst_Lista[i].CantidadAlertas;
            obj_Serie1.push([0, int_PrimerDia]);
            Serie1 = {
                label: str_FechaAlerta,
                color: "#20C31D",
                data: obj_Serie1,
                lines: { show: false },
                bars: { show: true, barWidth: 1, align: 'center' }
            }
        } else if (i == 1) {
            obj_Descripcion.push([i, str_FechaAlerta]);
            int_SegundoDia = lst_Lista[i].CantidadAlertas;
            obj_Serie2.push([i, int_SegundoDia]);
            Serie2 = {
                label: str_FechaAlerta,
                color: "#7FD4FF",
                data: obj_Serie2,
                lines: { show: false },
                bars: { show: true, barWidth: 1, align: 'center' }
            }
        } else if (i == 2) {
            obj_Descripcion.push([i, str_FechaAlerta]);
            int_TercerDia = lst_Lista[i].CantidadAlertas;
            obj_Serie3.push([i, int_TercerDia]);
            Serie3 = {
                label: str_FechaAlerta,
                color: "#20C31D",
                data: obj_Serie3,
                lines: { show: false },
                bars: { show: true, barWidth: 1, align: 'center' }
            }
        } else if (i == 3) {
            obj_Descripcion.push([i, str_FechaAlerta]);
            int_CuartoDia = lst_Lista[i].CantidadAlertas;
            obj_Serie4.push([i, int_CuartoDia]);
            Serie4 = {
                label: str_FechaAlerta,
                color: "#7FD4FF",
                data: obj_Serie4,
                lines: { show: false },
                bars: { show: true, barWidth: 1, align: 'center' }
            }
        } else if (i == 4) {
            obj_Descripcion.push([i, str_FechaAlerta]);
            int_QuintoDia = lst_Lista[i].CantidadAlertas;
            obj_Serie5.push([i, int_QuintoDia]);
            Serie5 = {
                label: str_FechaAlerta,
                color: "#20C31D",
                data: obj_Serie5,
                lines: { show: false },
                bars: { show: true, barWidth: 1, align: 'center' }
            }
        }
    }
    //obj_Serie1.push([0, int_PrimerDia]);
    //obj_Serie2.push([1, int_SegundoDia]);

    //obj_Descripcion.push([0, "20/09/2017"]);
    //obj_Descripcion.push([1, "21/09/2017"]);

    //var Serie1 = {
    //    label: "20/09/2017",
    //    color: "#20C31D",
    //    data: obj_Serie1,
    //    lines: { show: false },
    //    bars: { show: true, barWidth: 1, align: 'center' }
    //}  

    //var Serie2 = {
    //    label: "21/09/2017",
    //    color: "#7FD4FF",
    //    data: obj_Serie2,
    //    lines: { show: false },
    //    bars: { show: true, barWidth: 1, align: 'center' }
    //}

    somePlot = $.plot("#divGraficoAlertasDiarias", [Serie1, Serie2, Serie3, Serie4, Serie5], {
        grid: {
            hoverable: true,
            borderWidth: 1,
            borderColor: "#f3f3f3",
            tickColor: "#f3f3f3"
        },
        series: {
            stack: 1,
            bars: {
                show: true,
                barWidth: 1,
                align: "center"
            },
            valueLabels:
        {
            show: true,
            yoffset: 0,
            align: 'center',
            valign: 'above',
            font: "8pt Arial",
            fontcolor: 'black',
        }
        },
        xaxis: {
            mode: "categories",
            ticks: obj_Descripcion
        },
        tooltip: true,
        tooltipOpts: {
            content: '%s: %y'
        },
        legend: { show: true, noColumns: 5, container: $("#divLegendAlertasDiarias"), backgroundColor: 'white' }
    });
}

function Exportar() {
    var obj_Filtro = ObtenerFiltros();
    $('body').prepend("<form id='excelForm' method='post' action='../../Ashx/Reportes/ReporteMonitoreoExcelHandler.ashx'>" +
    "<input type='hidden' name='int_pIdTipoAlerta' value='" + obj_Filtro.IdTipoAlerta + "' >" +
    "<input type='hidden' name='int_pPosicionInicial' value='" + obj_Filtro.PosicionInicialString + "' >" +
    "<input type='hidden' name='int_pPosicionFinal' value='" + obj_Filtro.PosicionFinalString + "' >" +
    "<input type='hidden' name='dt_pFechaAlertaInicio' value='" + obj_Filtro.FechaInicioToString + "' >" +
    "<input type='hidden' name='dt_pFechaAlertaFin' value='" + obj_Filtro.FechaFinToString + "' >" +
    "</form>");
    $('#excelForm').submit();
    $("#excelForm").remove();
}

