//Atributos
var urlPrincipal = 'Persona.aspx';
var urlEdicion = 'RegistroPersona.aspx';

$(function () {
    cargarInicial();
    cargarControles();
    obtenerPersona();
});

function cargarInicial() {

    jQuery.validator.addMethod('CorreoUsuario', function (value) {
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
            ctl00$cphBody$txtCorreo: { required: false, CorreoUsuario: true },
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

    $("#fileUpload").fileserver(
    {
        idAlmacen: $("#hdIdAlmacen").val(),
        idRegistro: -1,
        autoUpload: true,
        replace: true,
        successMessage: false,
        success: function (obj_pResultado) {
            $("#lnkEliminarFoto").css('display', '');
            $("#hdIdDocumento").val(obj_pResultado.ReturnId);
            VerArchivoElemento('imgPerfilUsuarioImagen', $("#hdIdAlmacen").val(), -1, obj_pResultado.ReturnId);
        }
    });

    $(".btnGrabar").click(function () {
        return guardarPersona();
    });

    $(".btnCancelar").click(function () {
        ConfirmJQ('¿Está seguro de regresar a la ventana anterior?', function () { document.location.href = urlPrincipal });
        return false;
    });

    $("#lnkEliminarFoto").click(function () {
        ConfirmJQ('¿Está seguro de eliminar la foto?', eliminarFoto);
    });

    $("#txtNombre").focus();
}

function guardarPersona() {

    if ($('#frmContent').valid()) {

        obj_Persona = new Object();
        obj_Persona.IdPersona = $("#hdIdPersona").val();
        obj_Persona.Nombres = $("#txtNombre").val();
        obj_Persona.ApellidoPaterno = $("#txtApellidoPaterno").val();
        obj_Persona.ApellidoMaterno = $("#txtApellidoMaterno").val();
        obj_Persona.IdTipoDocumento = $("#ddlTipoDocumento").val();
        obj_Persona.NumeroDocumento = $("#txtNroDocumento").val();
        obj_Persona.Estado = $("#ddlEstado").val();

        //Mail
        obj_Persona.Mails = []
        var obj_Mail = new Object();
        obj_Mail.Mail = $("#txtCorreo").val();
        obj_Persona.Mails.push(obj_Mail);

        //Telefono
        obj_Persona.Telefonos = [];
        var obj_Telefono = new Object();
        obj_Telefono.Numero = $("#txtCelular").val();
        obj_Persona.Telefonos.push(obj_Telefono);

        //Eliminacion de la imagen
        var str_Ruta = $('#hdImagenPersona').val();
        if (str_Ruta == $("#imgPerfilUsuarioImagen").attr('src')) {
            obj_Persona.Foto = new Object();
            obj_Persona.Foto.IdAlmacen = $("#hdIdAlmacen").val();
            obj_Persona.Foto.IdRegistro = -1;
            obj_Persona.Foto.IdDocumento = null;
        }

        //Anexar Imagen
        if ($("#hdIdDocumento").val() != '') {
            obj_Persona.Foto = new Object();
            obj_Persona.Foto.IdAlmacen = $("#hdIdAlmacen").val();
            obj_Persona.Foto.IdRegistro = -1;
            obj_Persona.Foto.IdDocumento = $("#hdIdDocumento").val();
        }

        Accion('RegistroPersona.aspx/GuardarPersona', { "str_pPersona": JSON.stringify(obj_Persona) },
            function () { document.location.href = urlPrincipal; });
    }
    return false;
}

function eliminarFoto() {
    $("#hdIdDocumento").val('');
    var str_Ruta = $('#hdImagenPersona').val();
    $("#imgPerfilUsuarioImagen").attr('src', str_Ruta);
    $("#lnkEliminarFoto").css('display', 'none');
}

function obtenerPersona() {
    var int_IdPersona = $("#hdIdPersona").val();
    if (int_IdPersona != '') {
        var obj_Persona = ObtenerData("RegistroPersona.aspx/ObtenerPersona", { "int_pIdPersona": int_IdPersona });
        if (obj_Persona != null) {
            $("#txtNombre").val(obj_Persona.Nombres);
            $("#txtApellidoPaterno").val(obj_Persona.ApellidoPaterno);
            $("#txtApellidoMaterno").val(obj_Persona.ApellidoMaterno);
            $("#ddlTipoDocumento").val(obj_Persona.IdTipoDocumento);
            $("#txtNroDocumento").val(obj_Persona.NumeroDocumento);
            $("#txtCelular").val(obj_Persona.Numero);
            $("#txtCorreo").val(obj_Persona.Mail);
            $("#ddlEstado").val(obj_Persona.Estado);

            //Auditoria
            $("#ddUsuarioCreacion").html(obj_Persona.UsuarioCreacion);
            $("#ddUsuarioModificacion").html(obj_Persona.UsuarioModificacion);
            $("#ddFechaCreacion").html(ToDateTimeString(obj_Persona.FechaCreacion));
            $("#ddFechaModificacion").html(ToDateTimeString(obj_Persona.FechaModificacion));
        }
    }
}
