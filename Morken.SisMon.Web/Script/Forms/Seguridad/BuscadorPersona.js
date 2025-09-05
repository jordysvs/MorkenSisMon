//Atributos
var int_NumPagina = 1;
var int_TamPagina = 5;
var int_PagMostrar = 9;

$(function () {

    $(document).keypress(function (e) {
        var blnMdBuscadorPersona = ($("#mdBuscadorPersona").data('bs.modal') || {}).isShown;
        if (blnMdBuscadorPersona == undefined)
            blnMdBuscadorPersona = false;

        if (blnMdBuscadorPersona)
            if (e.which == 13) {
                $("#btnBuscarBuscadorPersona").click();
                return false;
            }
    });

    $("#btnBuscarBuscadorPersona").click(function () {
        ListarBusquedaPersona(1);
        return false;
    });

    CargarGrillaBuscadorPersona(null);
});

function ObtenerFiltrosBuscadorPersona() {
    var obj_Filtro = new Object();
    obj_Filtro.NombreCompleto = $("#txtNombreBuscadorPersona").val();
    obj_Filtro.IdTipoDocumento = $("#ddlTipoDocumentoBuscadorPersona").val();
    obj_Filtro.NumeroDocumento = $("#txtNroDocumentoBuscadorPersona").val();
    obj_Filtro.IdArea = $("#ddlAreaBuscadorPersona").val();
    obj_Filtro.IdPuesto = $("#ddlPuestoBuscadorPersona").val();
    return obj_Filtro;
}

function ListarBusquedaPersona(int_pNumPagina) {
    int_NumPagina = int_pNumPagina;
    var obj_Filtro = ObtenerFiltrosBuscadorPersona();
    var obj_Data = { "str_pNombreCompleto": obj_Filtro.NombreCompleto, "int_pIdTipoDocumento": obj_Filtro.IdTipoDocumento, "str_pNumeroDocumento": obj_Filtro.NumeroDocumento, "int_pNumPagina": int_pNumPagina, "int_pTamPagina": int_TamPagina };
    AccionDefault(true, UrlPath + "Redireccionar.aspx/ListarPersona", obj_Data, CargarGrillaBuscadorPersona, null, null, null, 1);
}

function CargarGrillaBuscadorPersona(obj_pLista) {

    var obj_TablaPrincipal = $("<table id='tblBuscadorPersona' class='table table-bordered table-hover dt-responsive'></table>");
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
    if (obj_pLista != null) {
        for (var i = 0; i < obj_pLista.Result.length; i++) {
            obj_FilaPrincipal = $("<tr value='" + obj_pLista.Result[i].IdPersona + "' descripcion='" + obj_pLista.Result[i].NombreCompleto + "'></tr>");
            obj_FilaPrincipal.append($("<td style='width:35%'>" + obj_pLista.Result[i].NombreCompleto + "</td>"));
            if (obj_pLista.Result[i].TipoDocumento != null)
                str_TipoDocumento = obj_pLista.Result[i].TipoDocumento.Descripcion;
            obj_FilaPrincipal.append($("<td style='width:20%'>" + str_TipoDocumento + "</td>"));
            obj_FilaPrincipal.append($("<td style='width:30%'>" + obj_pLista.Result[i].NumeroDocumento + "</td>"));
            str_Estado = obj_pLista.Result[i].Estado == 'A' ? "Activo" : "Inactivo";
            obj_FilaPrincipal.append($("<td style='width:15%; text-align:center'>" + str_Estado + "</td>"));
            obj_TBody.append(obj_FilaPrincipal);

            obj_FilaPrincipal.dblclick(
              function () {
                  $(this).addClass('selected');
                  $("#mdBuscadorPersona").find('.modal-footer .btn-aceptar').click();
              });
        }
        obj_TablaPrincipal.append(obj_TBody);

        var int_TotalPaginas = Math.ceil(obj_pLista.Total / int_TamPagina) == 0 ? 1 : Math.ceil(obj_pLista.Total / int_TamPagina);
        $("#divPaginacionInfoBuscadorPersona").html("Página " + int_NumPagina + " de " + int_TotalPaginas);

        $("#divPaginacionBuscadorPersona").unbind();
        $("#divPaginacionBuscadorPersona").bootpag({
            total: int_TotalPaginas,
            page: int_NumPagina,
            maxVisible: obj_pLista.Total == 0 ? 1 : int_PagMostrar
        }).on('page', function (event, num) {
            ListarBusquedaPersona(num);
        });
    }

    $("#divGrillaBuscadorPersona").html('');
    $("#divGrillaBuscadorPersona").append(obj_TablaPrincipal);

    var obj_Tabla = $('#tblBuscadorPersona').dataTable({
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

    $('#tblBuscadorPersona tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            obj_Tabla.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });
}

function ObtenerRegistroBuscadorPersona() {
    var obj_Registro = null;
    var obj_Seleccion = $('#tblBuscadorPersona').find('.selected');
    if (obj_Seleccion.length > 0) {
        obj_Registro = new Object();
        obj_Registro.id = $(obj_Seleccion[0]).attr('value');
        obj_Registro.descripcion = $(obj_Seleccion[0]).attr('descripcion');
    }
    else
        AlertJQ(1, 'Seleccione una persona.');
    return obj_Registro;
}

