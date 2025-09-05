//Atributos
var urlPrincipal = 'Usuario.aspx';
var urlEdicion = 'RegistroUsuario.aspx';

$(function () {
    CargarInicial();
    CargarControles();
    ObtenerUsuario();
    ListarPerfilUsuario();
});

function CargarInicial() {

    jQuery.validator.addMethod('selectEstado', function (value) {
        return (value != '');
    }, "Seleccione el estado");

    jQuery.validator.addMethod('selectPersona', function (value) {
        return ($("#hdIdPersona").val() != '');
    }, "Seleccione la persona");

    jQuery.validator.addMethod('selectPerfil', function (value) {
        return ($("#divPerfiles .checkbox").find("input:checked").length > 0);
    }, "Seleccione al menos un perfil");

}

function CargarControles() {

    var settings = $('#frmContent').validate().settings;
    $.extend(settings, {
        highlight: function (element) {
            $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
        },
        success: function (element) {
            $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
        }
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
        $(this).find('input').attr('readonly', 'readonly');
    })

    /*Switch*/
    $('#chkFlagExpiracion').bootstrapSwitch();
    $('#chkFlagExpiracion').on('switchChange.bootstrapSwitch', function (event, state) {
        HabilitarFechaExpiracion();
    });

    $(".btnGrabar").click(function () {
        return GuardarUsuario();
    });

    $(".btnCancelar").click(function () {
        ConfirmJQ('¿Está seguro de regresar a la ventana anterior?', function () { document.location.href = urlPrincipal });
        return false;
    });

    $(".btnReiniciar").css('display', 'none');

    $(".btnReiniciar").click(function () {
        ConfirmJQ('¿Está seguro de reiniciar la contraseña?',
            function () {
                Accion('RegistroUsuario.aspx/ReiniciarContrasenia', { "str_pIdUsuario": $("#hdIdUsuario").val() });
            });
        return false;
    });

    //$("#chkFlagExpiracion").change(function () {
    //    HabilitarFechaExpiracion();
    //});

    //Busqueda Persona
    $("#btnPersona_find").click(function () {
        var obj_modal = $("#mdBuscadorPersona").modal('show', {
            backdrop: true,
            keyboard: false,
        });

        $('#mdBuscadorPersona').on('shown.bs.modal', function () {
            $("#txtNombreBuscadorPersona").focus();
        });

        ListarBusquedaPersona(1);
    });

    $("#btnPersona_remove").click(function () {
        $("#hdIdPersona").val('');
        $("#txtPersona_input").val('');
    });

    $("#mdBuscadorPersona").find('.modal-footer .btn-aceptar').click(function () {
        var obj_Registro = ObtenerRegistroBuscadorPersona();
        if (obj_Registro != null) {
            $("#hdIdPersona").val(obj_Registro.id);
            $("#txtPersona_input").val(obj_Registro.descripcion);
            $("#mdBuscadorPersona").modal('hide');
        }
    });

    $("#mdBuscadorPersona").find('.modal-footer .btn-cancelar').click(function () {
        $("#mdBuscadorPersona").modal('hide');
    });

    KeyPressEnter("mdBuscadorPersona",
     function () {
         $("#mdBuscadorPersona").find('.modal-footer .btn-aceptar').click();
     });

    $("#txtCodigo").focus();
}

function ListarPerfilUsuario() {
    var obj_PerfilUsuario = ObtenerData("RegistroUsuario.aspx/ListarPerfil", { "str_pIdUsuario": $("#hdIdUsuario").val() });
    var divPerfiles = $("#divPerfiles");
    var str_checked = '';
    for (var i = 0; i < obj_PerfilUsuario.length; i++) {
        str_checked = '';
        if (obj_PerfilUsuario[i].Seleccion)
            str_checked = 'checked';
        divPerfiles.append("<div class='checkbox'><label><input type='checkbox' class='icheck' idmodulo='" + obj_PerfilUsuario[i].Perfil.Modulo.IdModulo + "' idperfil='" + obj_PerfilUsuario[i].IdPerfil + "' " + str_checked + " />" + obj_PerfilUsuario[i].Perfil.Modulo.Descripcion + " - " + obj_PerfilUsuario[i].Perfil.Descripcion + "</label></div>");
    }

    $('.icheck').iCheck({
        checkboxClass: 'icheckbox_flat-red'
    });
}

function HabilitarFechaExpiracion() {
    $('#txtFechaExpiracion_input').prop('readonly', !$('#chkFlagExpiracion').prop('checked'));
}

function ValidacionUsuario() {
    var settings = $('#frmContent').validate().settings;
    $('.form-group').removeClass('has-error').removeClass('has-success');
    $.extend(settings, {
        ignore: "",
        rules: {
            ctl00$cphBody$txtCodigo: { required: true },
            ctl00$cphBody$txtPersona_input: { selectPersona: true },
            txtFechaExpiracion_input: { required: $("#chkFlagExpiracion").prop('checked') },
            ctl00$cphBody$ddlEstado: { selectEstado: true },
            ctl00$cphBody$hdPerfiles: { selectPerfil: true }
        },
        messages: {
            ctl00$cphBody$txtCodigo: { required: 'Ingrese el código' },
            txtFechaExpiracion_input: { required: 'Ingrese la fecha de expiración' }
        },
    });

}

function GuardarUsuario() {
    //Validacion
    $("#txtPersona_input").prop('readonly', false);
    ValidacionUsuario();
    var blnValid = $('#frmContent').valid();
    $("#txtPersona_input").prop('readonly', true);

    if (blnValid) {
        var obj_Usuario = new Object();
        obj_Usuario.IdUsuario = $("#txtCodigo").val();
        obj_Usuario.UsuarioRed = $("#txtUsuarioRed").val();
        obj_Usuario.IdPersona = $("#hdIdPersona").val();
        obj_Usuario.FlagExpiracion = $("#chkFlagExpiracion").prop('checked') ? 'S' : 'N';
        obj_Usuario.FechaExpiracion = null;
        if ($("#chkFlagExpiracion").prop('checked'))
            obj_Usuario.FechaExpiracion = $("#txtFechaExpiracion").data("datetimepicker").getDate();
        obj_Usuario.Estado = $("#ddlEstado").val();

        var obj_PerfilUsuario = null;
        obj_Usuario.Perfiles = [];
        var lstPerfiles = $("#divPerfiles .checkbox").find("input:checked");
        for (var i = 0; i < lstPerfiles.length; i++) {
            obj_PerfilUsuario = new Object();
            obj_PerfilUsuario.IdUsuario = obj_Usuario.IdUsuario;
            obj_PerfilUsuario.IdModulo = $(lstPerfiles[i]).attr('idmodulo');
            obj_PerfilUsuario.IdPerfil = $(lstPerfiles[i]).attr('idperfil');
            obj_Usuario.Perfiles.push(obj_PerfilUsuario);
        }

        Accion('RegistroUsuario.aspx/GuardarUsuario', { "str_pUsuario": JSON.stringify(obj_Usuario), "str_pIdUsuario": $("#hdIdUsuario").val() }, function () { document.location.href = urlPrincipal });
    }
    else
        $('#frmContent').validate().focusInvalid();
    return false;
}

function ObtenerUsuario() {
    var str_IdUsuario = $("#hdIdUsuario").val();
    if (str_IdUsuario != '') {
        var obj_Usuario = ObtenerData("RegistroUsuario.aspx/ObtenerUsuario", { "str_pIdUsuario": str_IdUsuario });
        if (obj_Usuario != null) {
            $("#txtCodigo").attr('readonly', 'readonly');
            $("#txtCodigo").val(obj_Usuario.IdUsuario);
            $("#txtUsuarioRed").val(obj_Usuario.UsuarioRed);
            $("#hdIdPersona").val(obj_Usuario.IdPersona);
            $("#txtPersona_input").val(obj_Usuario.Persona.NombreCompleto);
            $("#chkFlagExpiracion").prop('checked', obj_Usuario.FlagExpiracion == 'S');

            if (obj_Usuario.FechaExpiracion != null) {
                obj_ParsedDate = new Date(parseInt(obj_Usuario.FechaExpiracion.substr(6)));
                $("#txtFechaExpiracion").data("datetimepicker").setDate(new Date(obj_ParsedDate.getFullYear(), obj_ParsedDate.getMonth(), obj_ParsedDate.getDate(), 00, 01));
            }
            $("#ddlEstado").val(obj_Usuario.Estado);

            //Auditoria
            $("#ddUsuarioCreacion").html(obj_Usuario.UsuarioCreacion);
            $("#ddUsuarioModificacion").html(obj_Usuario.UsuarioModificacion);
            $("#ddFechaCreacion").html(ToDateTimeString(obj_Usuario.FechaCreacion));
            $("#ddFechaModificacion").html(ToDateTimeString(obj_Usuario.FechaModificacion));

            $(".btnReiniciar").css('display', '');
            HabilitarFechaExpiracion();

            $("#txtUsuarioRed").focus();
        }
    }
}
