//Atributos
var int_NumPagina = 1;
var int_TamPagina = 10;
var int_PagMostrar = 9;
var urlEdicion = 'RegistroPerfil.aspx';

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
        ListarPerfil(1);
        return false;
    });

    KeyPressEnter("divFiltro",
        function () {
            $("#btnBuscar").click();
        });

    $("#btnBuscar").focus();

    ListarPerfil(1);
});

function ObtenerFiltros() {
    var obj_Filtro = new Object();
    obj_Filtro.IdModulo = null;
    if ($("#ddlModulo").val() != '')
        obj_Filtro.IdModulo = $("#ddlModulo").val();
    obj_Filtro.IdPerfil = $("#txtCodigo").val();
    obj_Filtro.Descripcion = $("#txtDescripcion").val();
    obj_Filtro.Estado = $("#ddlEstado").val();
    return obj_Filtro;
}

function ListarPerfilResultado(obj_pResultado, int_pNumPagina) {
    ListarPerfil(int_pNumPagina);
}

function ListarPerfil(int_pNumPagina) {
    int_NumPagina = int_pNumPagina;
    var obj_Filtro = ObtenerFiltros();
    var obj_Data = {
        "str_pIdModulo": obj_Filtro.IdModulo,
        "str_pIdPerfil": obj_Filtro.IdPerfil,
        "str_pDescripcion": obj_Filtro.Descripcion,
        "str_pEstado": obj_Filtro.Estado,
        "int_pNumPagina": int_pNumPagina,
        "int_pTamPagina": int_TamPagina
    };
    AccionDefault(true, "Perfil.aspx/ListarPerfil", obj_Data, CargarGrilla, null, null, null, 1);
}

function CargarGrilla(obj_pLista) {

    var obj_TablaPrincipal = $("<table id='tblPerfil' class='table table-bordered table-hover dt-responsive'></table>");
    var obj_THead = $("<thead></thead>");
    var obj_FilaPrincipal = $("<tr></tr>");
    obj_FilaPrincipal.append($("<th style='width:20%'>Código</th>"));
    obj_FilaPrincipal.append($("<th style='width:45%'>Descripción</th>"));
    obj_FilaPrincipal.append($("<th style='width:20%'>Módulo</th>"));
    obj_FilaPrincipal.append($("<th>Estado</th>"));
    obj_THead.append(obj_FilaPrincipal);
    obj_TablaPrincipal.append(obj_THead);

    var str_Estado = '';
    var obj_TBody = $("<tbody></tbody>");
    for (var i = 0; i < obj_pLista.Result.length; i++) {
        obj_FilaPrincipal = $("<tr idmodulo='" + obj_pLista.Result[i].IdModulo + "' idperfil='" + obj_pLista.Result[i].IdPerfil + "'></tr>");
        obj_FilaPrincipal.append($("<td>" + obj_pLista.Result[i].IdPerfil + "</td>"));
        obj_FilaPrincipal.append($("<td>" + obj_pLista.Result[i].Descripcion + "</td>"));
        obj_FilaPrincipal.append($("<td>" + obj_pLista.Result[i].Modulo.Descripcion + "</td>"));
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
        ListarPerfil(num);
    });

    $("#divGrilla").html('');
    $("#divGrilla").append(obj_TablaPrincipal);

    var obj_Tabla = $('#tblPerfil').dataTable({
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

    $('#tblPerfil tbody').on('click', 'tr', function () {
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
    var obj_Seleccion = $('#tblPerfil').find('.selected');
    if (obj_Seleccion.length > 0) {
        obj_Registro = new Object();
        obj_Registro.IdModulo = $(obj_Seleccion[0]).attr('idmodulo');
        obj_Registro.IdPerfil = $(obj_Seleccion[0]).attr('idperfil');
    }
    else
        AlertJQ(1, 'Seleccione un perfil.');
    return obj_Registro;
}

function Editar() {
    var obj_Registro = obtenerIdRegistro();
    if (obj_Registro != null) {
        MostrarMensajeCargando();
        document.location.href = urlEdicion + "?id=" + obj_Registro.IdModulo + "&id2=" + obj_Registro.IdPerfil;
    }
}

function Activar() {
    var obj_Registro = obtenerIdRegistro();
    if (obj_Registro != null) {
        ConfirmJQ('¿Está seguro de activar el perfil?', ModificarEstado, [obj_Registro, 'A']);
    }
}

function Inactivar(int_pId) {
    var obj_Registro = obtenerIdRegistro();
    if (obj_Registro != null) {
        ConfirmJQ('¿Está seguro de inactivar el perfil?', ModificarEstado, [obj_Registro, 'I']);
    }
}

function ModificarEstado(obj_pRegistro, str_pEstado) {
    Accion('Perfil.aspx/ModificarEstado', {
        "str_pIdModulo": obj_pRegistro.IdModulo,
        "str_pIdPerfil": obj_pRegistro.IdPerfil,
        "str_pEstado": str_pEstado
    }, ListarPerfilResultado, [1]);
}
