//Atributos
var urlPrincipal = 'Perfil.aspx';
var urlEdicion = 'RegistroPerfil.aspx';

$(function () {
    cargarInicial();
    obtenerPerfil();
    cargarControles();
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
            ctl00$cphBody$txtCodigo: { required: true },
            ctl00$cphBody$txtDescripcion: { required: true, },
            ctl00$cphBody$ddlEstado: { selectEstado: true }
        },
        messages: {
            ctl00$cphBody$txtCodigo: { required: 'Ingrese el código' },
            ctl00$cphBody$txtDescripcion: { required: 'Ingrese la descripción' },
            ctl00$cphBody$ddlModulo: { selectModulo: true },
        },
        highlight: function (element) {
            $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
        },
        success: function (element) {
            $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
        }
    });

    CargarModuloSelect("ddlModulo", false, null);
}

function cargarControles() {

    $(".btnGrabar").click(function () {
        return guardarPerfil();
    });

    $(".btnCancelar").click(function () {
        ConfirmJQ('¿Está seguro de regresar a la ventana anterior?', function () { document.location.href = urlPrincipal });
        return false;
    });

    $("#ddlModulo").change(function () {
        ListarMenu();
        return false;
    });

    $("#txtCodigo").focus();

    InicializarMenu();
}

function ListarMenu() {
    $("#tree").data("ui-fancytree").getTree().reload({
        url: "RegistroPerfil.aspx/Listar",
        data: JSON.stringify({
            "str_pIdModulo": $('#ddlModulo').val(),
            "str_pIdPerfil": $('#hdIdPerfil').val()
        }),
        cache: false
    });
}

function guardarPerfil() {

    if ($('#frmContent').valid()) {
        var str_IdPerfil = $("#hdIdPerfil").val();
        obj_Perfil = new Object();
        obj_Perfil.IdPerfil = $("#txtCodigo").val();
        obj_Perfil.Descripcion = $("#txtDescripcion").val();
        obj_Perfil.IdModulo = $("#ddlModulo").val();
        obj_Perfil.Estado = $("#ddlEstado").val();

        obj_Perfil.Accesos = [];
        var arrDetalle = $("#tree").fancytree('getTree').getSelectedNodes();
        for (var i = 0; i < arrDetalle.length; i++) {
            if (arrDetalle[i].data.source.hasOwnProperty('IdDetalle')) {
                obj_Detalle = new Object();
                obj_Detalle.IdModulo = obj_Perfil.IdModulo;
                obj_Detalle.IdPerfil = obj_Perfil.IdPerfil;
                obj_Detalle.IdModuloGrupo = arrDetalle[i].data.source.IdModulo;
                obj_Detalle.IdGrupo = arrDetalle[i].data.source.IdGrupo;
                obj_Detalle.IdDetalle = arrDetalle[i].data.source.IdDetalle;
                obj_Perfil.Accesos.push(obj_Detalle);
            }
        }

        Accion('RegistroPerfil.aspx/GuardarPerfil', { "str_pPerfil": JSON.stringify(obj_Perfil), "str_pIdPerfil": str_IdPerfil },
                  function () { document.location.href = urlPrincipal });
    }
    return false;
}

function obtenerPerfil() {
    var str_IdModulo = $("#hdIdModulo").val();
    var str_IdPerfil = $("#hdIdPerfil").val();
    if (str_IdPerfil != '') {
        var obj_Perfil = ObtenerData("RegistroPerfil.aspx/ObtenerPerfil", {
            "str_pIdModulo": str_IdModulo,
            "str_pIdPerfil": str_IdPerfil
        });
        if (obj_Perfil != null) {
            $("#txtCodigo").val(obj_Perfil.IdPerfil);
            $("#txtDescripcion").val(obj_Perfil.Descripcion);
            $("#ddlModulo").val(obj_Perfil.IdModulo);
            $("#ddlEstado").val(obj_Perfil.Estado);

            //Auditoria
            $("#ddUsuarioCreacion").html(obj_Perfil.UsuarioCreacion);
            $("#ddUsuarioModificacion").html(obj_Perfil.UsuarioModificacion);
            $("#ddFechaCreacion").html(ToDateTimeString(obj_Perfil.FechaCreacion));
            $("#ddFechaModificacion").html(ToDateTimeString(obj_Perfil.FechaModificacion));

            $("#txtCodigo").prop('readonly', true);
            $("#ddlModulo").prop('disabled', true);
        }
    }
}

function InicializarMenu() {

    glyph_opts = {
        map: {
            doc: "glyphicon glyphicon-file",
            docOpen: "glyphicon glyphicon-file",
            checkbox: "glyphicon glyphicon-unchecked",
            checkboxSelected: "glyphicon glyphicon-check",
            checkboxUnknown: "glyphicon glyphicon-share",
            dragHelper: "glyphicon glyphicon-play",
            dropMarker: "glyphicon glyphicon-arrow-right",
            error: "glyphicon glyphicon-warning-sign",
            expanderClosed: "glyphicon glyphicon-plus-sign",
            expanderLazy: "glyphicon glyphicon-plus-sign",
            // expanderLazy: "glyphicon glyphicon-expand",
            expanderOpen: "glyphicon glyphicon-minus-sign",
            // expanderOpen: "glyphicon glyphicon-collapse-down",
            folder: "glyphicon glyphicon-folder-close",
            folderOpen: "glyphicon glyphicon-folder-open",
            loading: "glyphicon glyphicon-refresh"
        }
    };

    $("#tree").fancytree({
        extensions: ["glyph", "wide"],
        checkbox: true,
        glyph: glyph_opts,
        selectMode: 3,
        ajax: { type: "POST", contentType: "application/json" },
        source: {
            url: "RegistroPerfil.aspx/Listar",
            data: JSON.stringify({
                "str_pIdModulo": $('#ddlModulo').val(),
                "str_pIdPerfil": $('#hdIdPerfil').val()
            }), cache: false
        },
        wide: {
            iconWidth: "1em",     // Adjust this if @fancy-icon-width != "16px"
            iconSpacing: "0.5em", // Adjust this if @fancy-icon-spacing != "3px"
            levelOfs: "1.5em"     // Adjust this if ul padding != "16px"
        },

    });

}