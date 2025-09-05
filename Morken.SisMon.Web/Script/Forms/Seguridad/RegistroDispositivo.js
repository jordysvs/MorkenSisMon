//Atributos
var urlPrincipal = 'Dispositivo.aspx';
var urlEdicion = 'RegistroDispositivo.aspx';

$(function () {
    cargarInicial();
    cargarControles();
    obtenerDispositivo();
});

function cargarInicial() {

    jQuery.validator.addMethod('selectEstado', function (value) {
        return (value != '');
    }, "Seleccione el estado");

    $('#frmContent').validate({
        rules: {
            ctl00$cphBody$txtIp: { required: true },
            ctl00$cphBody$txtNombre: { required: true, },
            ctl00$cphBody$ddlEstado: { selectEstado: true }
        },
        messages: {
            ctl00$cphBody$txtIp: { required: 'Ingrese la Ip' },
            ctl00$cphBody$txtNombre: { required: 'Ingrese el host name' },
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
        return guardarDispositivo();
    });

    $(".btnCancelar").click(function () {
        ConfirmJQ('¿Está seguro de regresar a la ventana anterior?', function () { document.location.href = urlPrincipal });
        return false;
    });

    $("#txtIp").focus();
}

function guardarDispositivo() {

    if ($('#frmContent').valid()) {
        obj_Dispositivo = new Object();
        obj_Dispositivo.IdDispositivo = $("#hdIdDispositivo").val();
        obj_Dispositivo.Ip = $("#txtIp").val();
        obj_Dispositivo.Nombre = $("#txtNombre").val();
        obj_Dispositivo.Estado = $("#ddlEstado").val();

        Accion('RegistroDispositivo.aspx/GuardarDispositivo', { "str_pDispositivo": JSON.stringify(obj_Dispositivo) },
                  function () { document.location.href = urlPrincipal });
    }
    return false;
}

function obtenerDispositivo() {
    var int_IdDipositivo = $("#hdIdDispositivo").val();
    if (int_IdDipositivo != '') {
        var obj_Dipositivo = ObtenerData("RegistroDispositivo.aspx/ObtenerDispositivo", { "int_pIdDispositivo": int_IdDipositivo });
        if (obj_Dipositivo != null) {
            $("#txtIp").val(obj_Dipositivo.Ip);
            $("#txtNombre").val(obj_Dipositivo.Nombre);
            $("#ddlEstado").val(obj_Dipositivo.Estado);

            //Auditoria
            $("#ddUsuarioCreacion").html(obj_Dipositivo.UsuarioCreacion);
            $("#ddUsuarioModificacion").html(obj_Dipositivo.UsuarioModificacion);
            $("#ddFechaCreacion").html(ToDateTimeString(obj_Dipositivo.FechaCreacion));
            $("#ddFechaModificacion").html(ToDateTimeString(obj_Dipositivo.FechaModificacion));
        }
    }
}
