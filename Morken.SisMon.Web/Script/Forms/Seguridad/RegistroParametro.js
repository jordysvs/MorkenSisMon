//Atributos
var urlPrincipal = 'Parametro.aspx';
var urlEdicion = 'RegistroParametro.aspx';

$(function () {
    cargarInicial();
    cargarControles();
    obtenerParametro();
});

function cargarInicial() {

    $('#frmContent').validate({
        rules: {
            ctl00$cphBody$txtCodigo: { required: true },
            ctl00$cphBody$txtDescripcion: { required: true, },
            ctl00$cphBody$txtValor: { required: true, }
        },
        messages: {
            ctl00$cphBody$txtCodigo: { required: 'Ingrese el código' },
            ctl00$cphBody$txtDescripcion: { required: 'Ingrese la descripción' },
            ctl00$cphBody$txtValor: { required: 'Ingrese el valor' }
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

    $("#txtDescripcion").attr("maxlength", 100);

    $("#txtValor").attr("maxlength", 200);

    $(".btnGrabar").click(function () {
        return guardarParametro();
    });

    $(".btnCancelar").click(function () {
        ConfirmJQ('¿Está seguro de regresar a la ventana anterior?', function () { document.location.href = urlPrincipal });
        return false;
    });

    $("#txtCodigo").focus();
}

function guardarParametro() {
    if ($('#frmContent').valid()) {
        var obj_Parametro = new Object();
        obj_Parametro.IdModulo = $("#hdIdModulo").val();
        obj_Parametro.IdParametro = $("#txtCodigo").val();
        obj_Parametro.Descripcion = $("#txtDescripcion").val();
        obj_Parametro.Valor = $("#txtValor").val();
        Accion('RegistroParametro.aspx/GuardarParametro', { "str_pParametro": JSON.stringify(obj_Parametro) }, function () { document.location.href = urlPrincipal })
    }
    return false;
}

function obtenerParametro() {
    var str_IdModulo = $("#hdIdModulo").val();
    var str_IdParametro = $("#txtCodigo").val();
    if (str_IdParametro != '') {
        var obj_Parametro = ObtenerData("RegistroParametro.aspx/ObtenerParametro", 
            { 
                "str_pIdModulo": str_IdModulo,
                "str_pIdParametro": str_IdParametro 
            });
        if (obj_Parametro != null) {
            $("#txtModulo").val(obj_Parametro.Modulo.Descripcion);
            $("#txtCodigo").val(obj_Parametro.IdParametro);
            $("#txtDescripcion").val(obj_Parametro.Descripcion);
            $("#txtValor").val(obj_Parametro.Valor);

            //Auditoria
            $("#ddUsuarioCreacion").html(obj_Parametro.UsuarioCreacion);
            $("#ddUsuarioModificacion").html(obj_Parametro.UsuarioModificacion);
            $("#ddFechaCreacion").html(ToDateTimeString(obj_Parametro.FechaCreacion));
            $("#ddFechaModificacion").html(ToDateTimeString(obj_Parametro.FechaModificacion));
        }
    }
}
