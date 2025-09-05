$(function () {
    cargarInicial();
    cargarControles();
});

function cargarInicial() {

    jQuery.validator.addMethod('CorreoUsuario', function (value) {
        return value.match(/\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/g);
    }, 'Ingrese correctamente el correo');

    $('#frmContent').validate({
        rules: {
            ctl00$cphBody$txtCodigo: { required: true },
            ctl00$cphBody$txtCorreo: { required: true, CorreoUsuario: true },
        },
        messages: {
            ctl00$cphBody$txtCodigo: { required: 'Ingrese el código' },
            ctl00$cphBody$txtCorreo: {
                required: 'Ingrese el correo'
            },
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

    $("#btnValidar").click(function () {
        return ValidarUsuario();
    });

    $("#txtCodigo").focus();
}

function ValidarUsuario() {
    if ($('#frmContent').valid()) {
        var str_Codigo = $("#txtCodigo").val();
        var str_Correo = $("#txtCorreo").val();
        Accion('CambiarContrasenia.aspx/ValidarContraseniaUsuario', { "str_pCodigo": str_Codigo, "str_pCorreo": str_Correo })
    }
    return false;
}
