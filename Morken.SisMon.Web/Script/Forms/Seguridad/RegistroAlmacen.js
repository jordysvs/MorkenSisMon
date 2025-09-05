//Atributos
var urlPrincipal = 'Almacen.aspx';
var urlEdicion = 'RegistroAlmacen.aspx';

$(function () {
    cargarInicial();
    cargarControles();
    obtenerAlmacen();
});

function cargarInicial() {

    jQuery.validator.addMethod('selectEstado', function (value) {
        return (value != '');
    }, "Seleccione el estado");

    jQuery.validator.addMethod('selectModulo', function (value) {
        return (value != '');
    }, "Seleccione el módulo");

    $('#frmContent').validate({
        rules: {
            ctl00$cphBody$txtDescripcion: { required: true },
            ctl00$cphBody$txtRuta: { required: true, },
            ctl00$cphBody$ddlModulo: { selectModulo: true },
            ctl00$cphBody$ddlEstado: { selectEstado: true }
        },
        messages: {
            ctl00$cphBody$txtDescripcion: { required: 'Ingrese la descripción' },
            ctl00$cphBody$txtRuta: { required: 'Ingrese la ruta' },
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

    CargarModuloSelect("ddlModulo", true, "--Seleccione--");

    $(".btnGrabar").click(function () {
        return guardarAlmacen();
    });

    $(".btnCancelar").click(function () {
        ConfirmJQ('¿Está seguro de regresar a la ventana anterior?', function () { document.location.href = urlPrincipal });
        return false;
    });

    $("#txtDescripcion").focus();
}

function guardarAlmacen() {

    if ($('#frmContent').valid()) {
        obj_Almacen = new Object();
        obj_Almacen.IdAlmacen = $("#hdIdAlmacen").val();
        obj_Almacen.Descripcion = $("#txtDescripcion").val();
        obj_Almacen.TipoAlmacen = $("#ddlTipoAlmacen").val();
        obj_Almacen.Usuario = $("#txtUsuario").val();
        obj_Almacen.Clave = $("#txtClave").val();
        obj_Almacen.Dominio = $("#txtDominio").val();
        obj_Almacen.Ruta = $("#txtRuta").val();
        obj_Almacen.IdModulo = $("#ddlModulo").val();
        obj_Almacen.Estado = $("#ddlEstado").val();

        Accion('RegistroAlmacen.aspx/GuardarAlmacen', { "str_pAlmacen": JSON.stringify(obj_Almacen) },
                  function () { document.location.href = urlPrincipal });
    }
    return false;
}

function obtenerAlmacen() {
    var int_IdAlmacen = $("#hdIdAlmacen").val();
    if (int_IdAlmacen != '') {
        var obj_Almacen = ObtenerData("RegistroAlmacen.aspx/ObtenerAlmacen", { "int_pIdAlmacen": int_IdAlmacen });
        if (obj_Almacen != null) {
            $("#txtDescripcion").val(obj_Almacen.Descripcion);
            $("#txtRuta").val(obj_Almacen.Ruta);
            $("#ddlTipoAlmacen").val(obj_Almacen.TipoAlmacen);
            $("#txtUsuario").val(obj_Almacen.Usuario);
            $("#txtClave").val(obj_Almacen.Clave);
            $("#txtDominio").val(obj_Almacen.Dominio);
            $("#ddlModulo").val(obj_Almacen.IdModulo);
            $("#ddlEstado").val(obj_Almacen.Estado);

            //Auditoria
            $("#ddUsuarioCreacion").html(obj_Almacen.UsuarioCreacion);
            $("#ddUsuarioModificacion").html(obj_Almacen.UsuarioModificacion);
            $("#ddFechaCreacion").html(ToDateTimeString(obj_Almacen.FechaCreacion));
            $("#ddFechaModificacion").html(ToDateTimeString(obj_Almacen.FechaModificacion));
        }
    }
}
