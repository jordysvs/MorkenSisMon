//Atributos
var urlPrincipal = 'Operador.aspx';
var urlEdicion = 'RegistroOperador.aspx';

$(function () {
    cargarInicial();
    cargarControles();
    obtenerOperador();
});

function cargarInicial() {
    jQuery.validator.addMethod('Correo', function (value) {
        if (value == '')
            return true;
        else
            return /\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/.test(value);
    }, 'Ingrese correctamente el correo');

    jQuery.validator.addMethod('selectEstado', function (value) {
        return (value != '');
    }, "Seleccione el estado");

    $('#frmContent').validate({
        rules: {
            ctl00$cphBody$txtNombre: { required: true, },
            ctl00$cphBody$txtApellidoPaterno: { required: true, },
            ctl00$cphBody$txtApellidoMaterno: { required: true, },
            ctl00$cphBody$txtCorreo: { required: true, Correo: true },
            ctl00$cphBody$ddlEstado: { selectEstado: true }
        },
        messages: {
            ctl00$cphBody$txtNombre: { required: 'Ingrese el nombre' },
            ctl00$cphBody$txtApellidoPaterno: { required: 'Ingrese el apellido paterno' },
            ctl00$cphBody$txtApellidoMaterno: { required: 'Ingrese el apellido materno' },
        },
        highlight: function (element) {
            $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
        },
        success: function (element) {
            $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
        }
    });
}

function cargarControles() {

    $(".btnGrabar").click(function () {
        return guardarOperador();
    });

    $(".btnCancelar").click(function () {
        ConfirmJQ('¿Está seguro de regresar a la ventana anterior?', function () { document.location.href = urlPrincipal });
        return false;
    });

    $("#txtNombre").focus();
}

function guardarOperador() {
    if ($('#frmContent').valid()) {
        var obj_Operador = new Object();
        obj_Operador.IdOperador = $("#hdIdOperador").val();
        obj_Operador.IdPersona = $("#hdIdPersona").val();

        obj_Operador.Persona = new Object();
        obj_Operador.Persona.IdPersona = $("#hdIdPersona").val();
        obj_Operador.Persona.Nombres = $("#txtNombre").val();
        obj_Operador.Persona.ApellidoPaterno = $("#txtApellidoPaterno").val();
        obj_Operador.Persona.ApellidoMaterno = $("#txtApellidoMaterno").val();
        obj_Operador.Persona.IdTipoDocumento = $("#ddlTipoDocumento").val();
        obj_Operador.Persona.NumeroDocumento = $("#txtNroDocumento").val();
        obj_Operador.Estado = $("#ddlEstado").val();

        //Mail
        obj_Operador.Persona.Mails = []
        var obj_Mail = new Object();
        obj_Mail.Mail = $("#txtCorreo").val();
        obj_Operador.Persona.Mails.push(obj_Mail);

        //Telefono
        obj_Operador.Persona.Telefonos = [];
        var obj_Telefono = new Object();
        obj_Telefono.Numero = $("#txtCelular").val();
        obj_Operador.Persona.Telefonos.push(obj_Telefono);

        Accion('RegistroOperador.aspx/GuardarOperador', { "str_pOperador": JSON.stringify(obj_Operador) },
            function () { document.location.href = urlPrincipal; });
    }
    return false;
}

function obtenerOperador() {
    var int_IdOperador = $("#hdIdOperador").val();
    if (int_IdOperador != '') {
        AccionDefault(true, "RegistroOperador.aspx/ObtenerOperador", { "int_pIdOperador": int_IdOperador },
            function (obj_Operador) {
                if (obj_Operador != null) {
                    $("#hdIdPersona").val(obj_Operador.IdPersona);
                    $("#txtNombre").val(obj_Operador.Persona.Nombres);
                    $("#txtApellidoPaterno").val(obj_Operador.Persona.ApellidoPaterno);
                    $("#txtApellidoMaterno").val(obj_Operador.Persona.ApellidoMaterno);
                    $("#ddlTipoDocumento").val(obj_Operador.Persona.IdTipoDocumento);
                    $("#txtNroDocumento").val(obj_Operador.Persona.NumeroDocumento);
                    $("#txtCelular").val(obj_Operador.Persona.Numero);
                    $("#txtCorreo").val(obj_Operador.Persona.Mail);
                    $("#ddlEstado").val(obj_Operador.Estado);

                    //Auditoria
                    $("#ddUsuarioCreacion").html(obj_Operador.UsuarioCreacion);
                    $("#ddUsuarioModificacion").html(obj_Operador.UsuarioModificacion);
                    $("#ddFechaCreacion").html(ToDateTimeString(obj_Operador.FechaCreacion));
                    $("#ddFechaModificacion").html(ToDateTimeString(obj_Operador.FechaModificacion));
                }
        }, null, null, null, 1); 
    }
}
