//Atributos
var int_NumPagina = 1;
var int_TamPagina = 10;
var int_PagMostrar = 9;
var urlEdicion = 'RegistroUsuario.aspx';

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
        ListarUsuario(1);
        return false;
    });

    $("#ddlModulo").change(function () {
        CargarPerfilSelect();
    });
    CargarPerfilSelect();

    KeyPressEnter("divFiltro",
        function () {
            $("#btnBuscar").click();
        });

    $("#btnBuscar").focus();

    ListarUsuario(1);
});

function CargarPerfilSelect() {
    var ddlPerfil = $("#ddlPerfil");
    ddlPerfil.html('');
    ddlPerfil.append($("<option value=''>--Todos--</option>"));

    if ($("#ddlModulo").val() != '') {
        var obj_Data = { "str_pIdModulo": $("#ddlModulo").val() };
        AccionDefault(true, "Usuario.aspx/ListarPerfil", obj_Data,
            function (obj_pLista) {
                for (var i = 0; i < obj_pLista.length; i++) {
                    ddlPerfil.append($("<option value='" + obj_pLista[i].IdPerfil + "'>" + obj_pLista[i].Descripcion + "</option>"));
                }
            }
       , null, null, null, 1);
    }
}

function ObtenerFiltros() {
    var obj_Filtro = new Object();
    obj_Filtro.IdUsuario = $("#txtCodigo").val();
    obj_Filtro.NombreCompleto = $("#txtNombre").val();
    obj_Filtro.IdPerfil = $("#ddlPerfil").val();
    obj_Filtro.Estado = $("#ddlEstado").val();
    return obj_Filtro;
}

function ListarUsuarioResultado(obj_pResultado, int_pNumPagina) {
    ListarUsuario(int_pNumPagina);
}

function ListarUsuario(int_pNumPagina) {
    int_NumPagina = int_pNumPagina;
    var obj_Filtro = ObtenerFiltros();
    var obj_Data = { "str_pIdUsuario": obj_Filtro.IdUsuario, "str_pNombreCompleto": obj_Filtro.NombreCompleto, "str_pIdPerfil": obj_Filtro.IdPerfil, "str_pEstado": obj_Filtro.Estado, "int_pNumPagina": int_pNumPagina, "int_pTamPagina": int_TamPagina };
    AccionDefault(true, "Usuario.aspx/ListarUsuario", obj_Data, CargarGrilla, null, null, null, 1);
}

function CargarGrilla(obj_pLista) {

    var obj_TablaPrincipal = $("<table id='tblUsuario' class='table table-bordered table-hover dt-responsive nowrap'></table>");
    var obj_THead = $("<thead></thead>");
    var obj_FilaPrincipal = $("<tr></tr>");
    obj_FilaPrincipal.append($("<th>Código</th>"));
    obj_FilaPrincipal.append($("<th>Nombre</th>"));
    obj_FilaPrincipal.append($("<th>Contraseña</th>"));
    obj_FilaPrincipal.append($("<th>Correo</th>"));
    obj_FilaPrincipal.append($("<th>Estado</th>"));
    obj_THead.append(obj_FilaPrincipal);
    obj_TablaPrincipal.append(obj_THead);

    var str_NombreCompleto = '';
    var str_Mail = '';
    var str_Estado = '';
    var obj_TBody = $("<tbody></tbody>");
    for (var i = 0; i < obj_pLista.Result.length; i++) {
        obj_FilaPrincipal = $("<tr value='" + obj_pLista.Result[i].IdUsuario + "'></tr>");
        obj_FilaPrincipal.append($("<td style='width:15%'>" + obj_pLista.Result[i].IdUsuario + "</td>"));

        if (obj_pLista.Result[i].Persona != null) {
            str_NombreCompleto = obj_pLista.Result[i].Persona.NombreCompleto;

            if (obj_pLista.Result[i].Persona.Mail != null)
                str_Mail = obj_pLista.Result[i].Persona.Mail;
        }
        obj_FilaPrincipal.append($("<td style='width:45%'>" + str_NombreCompleto + "</td>"));
        obj_FilaPrincipal.append($("<td style='width:15%'>" + obj_pLista.Result[i].Clave + "</td>"));
        obj_FilaPrincipal.append($("<td style='width:15%'>" + str_Mail + "</td>"));
        str_Estado = obj_pLista.Result[i].Estado == 'A' ? "Activo" : "Inactivo";
        obj_FilaPrincipal.append($("<td style='width:10%; text-align:center'>" + str_Estado + "</td>"));
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
        ListarUsuario(num);
    });

    $("#divGrilla").html('');
    $("#divGrilla").append(obj_TablaPrincipal);

    var obj_Tabla = $('#tblUsuario').dataTable({
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

    $('#tblUsuario tbody').on('click', 'tr', function () {
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
    var obj_Seleccion = $('#tblUsuario').find('.selected');
    if (obj_Seleccion.length > 0)
        str_Id = $(obj_Seleccion[0]).attr('value');
    else
        AlertJQ(1, 'Seleccione un usuario.');
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
        ConfirmJQ('¿Está seguro de activar el usuario?', ModificarEstado, [str_Id, 'A']);
    }
}

function Inactivar(int_pId) {
    var str_Id = obtenerIdRegistro();
    if (str_Id != null) {
        ConfirmJQ('¿Está seguro de inactivar el usuario?', ModificarEstado, [str_Id, 'I']);
    }
}

function ModificarEstado(str_pId, str_pEstado) {
    var obj_Data = { "str_pIdUsuario": str_pId, "str_pEstado": str_pEstado };
    Accion('Usuario.aspx/ModificarEstado', { "str_pIdUsuario": str_pId, "str_pEstado": str_pEstado }, ListarUsuarioResultado, [1]);
}
