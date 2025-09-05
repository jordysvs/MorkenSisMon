//Atributos
var urlPrincipal = 'Modulo.aspx';
var urlEdicion = 'RegistroModulo.aspx';

$(function () {
    CargarInicial();
    CargarControles();
    ObtenerModulo();
});

function CargarInicial() {

    jQuery.validator.addMethod('selectEstado', function (value) {
        return (value != '');
    }, "Seleccione el estado");

    $('#frmContent').validate({
        rules: {
            ctl00$cphBody$txtCodigo: { required: true, },
            ctl00$cphBody$txtDescripcion: { required: true, },
            ctl00$cphBody$ddlEstado: { selectEstado: true }
        },
        messages: {
            ctl00$cphBody$txtCodigo: { required: 'Ingrese el código' },
            ctl00$cphBody$txtDescripcion: { required: 'Ingrese la descripción' },
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
            VerArchivoElemento('imgModulo', $("#hdIdAlmacen").val(), -1, obj_pResultado.ReturnId);
        }
    });

    $(".btnGrabar").click(function () {
        return GuardarModulo();
    });

    $(".btnCancelar").click(function () {
        ConfirmJQ('¿Está seguro de regresar a la ventana anterior?', function () { document.location.href = urlPrincipal });
        return false;
    });

    $("#lnkEliminarFoto").click(function () {
        ConfirmJQ('¿Está seguro de eliminar la foto?', eliminarFoto);
    });

    $("#txtCodigo").focus();
}

function GuardarModulo() {

    if ($('#frmContent').valid()) {

        obj_Modulo = new Object();
        obj_Modulo.IdModulo = $("#txtCodigo").val();
        obj_Modulo.Descripcion = $("#txtDescripcion").val();
        obj_Modulo.IdImagen = $("#hdIdImagen").val();
        obj_Modulo.Estado = $("#ddlEstado").val();

        //Eliminacion de la imagen
        var str_Ruta = $('#hdImagenModulo').val();
        if (str_Ruta == $("#imgModulo").attr('src')) {
            obj_Modulo.Foto = new Object();
            obj_Modulo.Foto.IdAlmacen = $("#hdIdAlmacen").val();
            obj_Modulo.Foto.IdRegistro = -1;
            obj_Modulo.Foto.IdDocumento = null;
        }

        //Anexar Imagen
        if ($("#hdIdDocumento").val() != '') {
            obj_Modulo.Foto = new Object();
            obj_Modulo.Foto.IdAlmacen = $("#hdIdAlmacen").val();
            obj_Modulo.Foto.IdRegistro = -1;
            obj_Modulo.Foto.IdDocumento = $("#hdIdDocumento").val();
        }

        Accion('RegistroModulo.aspx/GuardarModulo', {
            "str_pModulo": JSON.stringify(obj_Modulo),
            "str_pIdModulo": $("#hdIdModulo").val()
        },
            function () { document.location.href = urlPrincipal; });
    }
    return false;
}

function EliminarFoto() {
    $("#hdIdDocumento").val('');
    var str_Ruta = $('#hdImagenModulo').val();
    $("#imgModulo").attr('src', str_Ruta);
    $("#lnkEliminarFoto").css('display', 'none');
}

function ObtenerModulo() {
    var str_IdModulo = $("#hdIdModulo").val();
    if (str_IdModulo != '') {
        var obj_Modulo = ObtenerData("RegistroModulo.aspx/ObtenerModulo", { "str_pIdModulo": str_IdModulo });
        if (obj_Modulo != null) {
            $("#txtCodigo").prop('readonly', true);
            $("#txtCodigo").val(obj_Modulo.IdModulo);
            $("#txtDescripcion").val(obj_Modulo.Descripcion);
            $("#hdIdImagen").val(obj_Modulo.IdImagen);
            $("#ddlEstado").val(obj_Modulo.Estado);

            //Auditoria
            $("#ddUsuarioCreacion").html(obj_Modulo.UsuarioCreacion);
            $("#ddUsuarioModificacion").html(obj_Modulo.UsuarioModificacion);
            $("#ddFechaCreacion").html(ToDateTimeString(obj_Modulo.FechaCreacion));
            $("#ddFechaModificacion").html(ToDateTimeString(obj_Modulo.FechaModificacion));

            AccionValor('RegistroModulo.aspx/ObtenerImagenURL', { "int_pIdRegistro": obj_Modulo.IdImagen },
            function (str_pUrl) {
                $("#lnkEliminarFoto").css('display', '');

                if (str_pUrl == '') {
                    str_pUrl = $("#hdImagenModulo").val();
                    $("#lnkEliminarFoto").css('display', 'none');
                }
                $("#imgModulo").attr("src", str_pUrl);
            });
        }
    }
}
