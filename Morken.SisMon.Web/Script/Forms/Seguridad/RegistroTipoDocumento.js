//Atributos
var urlPrincipal = 'TipoDocumento.aspx';
var urlEdicion = 'RegistroTipoDocumento.aspx';

$(function () {
    CargarInicial();
    CargarControles();
    ObtenerTipoDocumento();
});

function CargarInicial() {
    jQuery.validator.addMethod('selectEstado', function (value) {
        return (value != '');
    }, "Seleccione el estado");

    $('#frmContent').validate({
        rules: {
            ctl00$cphBody$txtCodigo: { required: true },
            ctl00$cphBody$txtDescripcion: { required: true, },
            ctl00$cphBody$ddlEstado: { selectEstado: true }
        },
        messages: {
            ctl00$cphBody$txtCodigo: { required: 'Ingrese el código' },
            ctl00$cphBody$txtNombre: { required: 'Ingrese la descripción' }
        },
        highlight: function (element) {
            $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
        },
        success: function (element) {
            $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
        }
    });
}

function CargarControles() {
    $("#btnGrabar").click(function () {
        return GuardarTipoDocumento();
    });
    $("#btnCancelar").click(function () {
        ConfirmJQ('¿Está seguro de regresar a la ventana anterior?', function () { document.location.href = urlPrincipal });
        return false;
    });
}

function GuardarTipoDocumento() {

    if ($('#frmContent').valid()) {
        obj_TipoDocumento = new Object();
        obj_TipoDocumento.IdTipoDocumento = $("#hdIdTipoDocumento").val();
        obj_TipoDocumento.Codigo = $("#txtCodigo").val();
        obj_TipoDocumento.Descripcion = $("#txtDescripcion").val();
        obj_TipoDocumento.Estado = $("#ddlEstado").val();
        Accion('RegistroTipoDocumento.aspx/GuardarTipoDocumento', { "str_pTipoDocumento": JSON.stringify(obj_TipoDocumento) }, function () { document.location.href = urlPrincipal })
    }
    return false;
}

function ObtenerTipoDocumento() {
    var int_IdTipoDocumento = $("#hdIdTipoDocumento").val();
    if (int_IdTipoDocumento != '') {
        var obj_TipoDocumento = ObtenerData("RegistroTipoDocumento.aspx/ObtenerTipoDocumento", { "int_pIdTipoDocumento": int_IdTipoDocumento });
        if (obj_TipoDocumento != null) {
            $("#hdIdTipoDocumento").val(obj_TipoDocumento.IdTipoDocumento);
            $("#txtCodigo").val(obj_TipoDocumento.Codigo);
            $("#txtDescripcion").val(obj_TipoDocumento.Descripcion);
            $("#ddlEstado").val(obj_TipoDocumento.Estado);
        }
    }
}
