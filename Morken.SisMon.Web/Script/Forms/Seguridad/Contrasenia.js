//Atributos
var urlPrincipal = '../../Login.aspx';

$(function () {
    cargarInicial();
    cargarControles();
});

function cargarInicial() {

    jQuery.validator.addMethod('Contrasenia', function (value) {
        return ($("#txtContrasenia").val() == $("#txtRepetirContrasenia").val());
    }, 'Las contraseñas deben ser iguales');

    $('#frmContent').validate({
        rules: {
            ctl00$cphBody$txtContrasenia: { required: true },
            ctl00$cphBody$txtRepetirContrasenia: { required: true, Contrasenia: true }
        },
        messages: {
            ctl00$cphBody$txtContrasenia: { required: 'Ingrese la contraseña' },
            ctl00$cphBody$txtRepetirContrasenia: { required: 'Ingrese nuevamente la contraseña' }
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

    $("#btnGrabar").click(function () {
        return guardarContrasenia();
    });

    $("#txtContrasenia").focus();
}

function guardarContrasenia() {
    if ($('#frmContent').valid()) {
        var str_IdUsuario = $("#hdIdUsuario").val();
        var str_Contrasenia = $("#txtContrasenia").val();
        Accion('Contrasenia.aspx/CambiarContrasenia', { "str_pIdUsuario": str_IdUsuario, "str_pContrasenia": str_Contrasenia }, function () { document.location.href = urlPrincipal })
    }
    return false;
}

