var str_IdModulo = null;
var str_Descripcion = null;
var str_URL = null;
var str_FlagVisible = null;
var str_Estado = null;

$(function () {
    cargarInicial();
    InicializarGrupo();
    InicializarDetalle();
    InicializarMenu();
});

function cargarInicial() {

    jQuery.validator.addMethod('selectEstado', function (value) {
        return (value != '');
    }, "Seleccione el estado");

    var settings = $('#frmContent').validate().settings;
    $.extend(settings, {
        highlight: function (element) {
            $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
        },
        success: function (element) {
            $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
        }
    });

    $("#fileUploadGrupo").fileserver(
    {
        idAlmacen: $("#hdIdAlmacen").val(),
        idRegistro: -1,
        autoUpload: true,
        replace: true,
        resize: true,
        width: 16,
        height: 16,
        successMessage: false,
        success: function (obj_pResultado) {
            $("#lnkEliminarFotoGrupo").css('display', '');
            $("#hdIdDocumento").val(obj_pResultado.ReturnId);
            VerArchivoElemento('imgImagenGrupo', $("#hdIdAlmacen").val(), -1, obj_pResultado.ReturnId);
        }
    });

    $("#fileUploadDetalle").fileserver(
    {
        idAlmacen: $("#hdIdAlmacen").val(),
        idRegistro: -1,
        autoUpload: true,
        replace: true,
        resize: true,
        width: 16,
        height: 16,
        successMessage: false,
        success: function (obj_pResultado) {
            $("#lnkEliminarFotoDetalle").css('display', '');
            $("#hdIdDocumento").val(obj_pResultado.ReturnId);
            VerArchivoElemento('imgImagenDetalle', $("#hdIdAlmacen").val(), -1, obj_pResultado.ReturnId);
        }
    });

    $("#lnkEliminarFotoGrupo").click(function () {
        ConfirmJQ('¿Está seguro de eliminar la foto?', EliminarFotoGrupo);
    });

    $("#lnkEliminarFotoDetalle").click(function () {
        ConfirmJQ('¿Está seguro de eliminar la foto?', EliminarFotoDetalle);
    });

    CargarModuloSelect("ddlModulo", true, "--Todos--");

    $("#btnAgregar").click(function () {
        Agregar();
        return false;
    });

    $("#btnModificar").click(function () {
        Editar();
        return false;
    });

    $("#btnBuscar").click(function () {
        ListarMenu();
        return false;
    });

    KeyPressEnter("divFiltro",
    function () {
        $("#btnBuscar").click();
    });

    $("#btnBuscar").focus();
};

function EstablecerFiltros() {
    str_IdModulo = null;
    if ($("#ddlModulo").val() != '')
        str_IdModulo = $("#ddlModulo").val();
    str_Descripcion = $("#txtDescripcion").val();
    str_URL = $("#txtURL").val();
    str_FlagVisible = null;
    if ($("#ddlVisible").val() != '')
        str_FlagVisible = $("#ddlVisible").val();
    str_Estado = null;
    if ($("#ddlEstado").val() != '')
        str_Estado = $("#ddlEstado").val();
}

function ListarMenu() {
    EstablecerFiltros();
    $("#treetable").data("ui-fancytree").getTree().reload({
        url: "Menu.aspx/ListarGrupo",
        data: JSON.stringify({
            "str_pIdModulo": str_IdModulo,
            "str_pDescripcion": str_Descripcion,
            "str_pURL": str_URL,
            "str_pFlagVisible": str_FlagVisible,
            "str_pEstado": str_Estado
        }),
        cache: false
    });
}

function InicializarMenu() {

    EstablecerFiltros();

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

    $("#treetable").fancytree({
        extensions: ["dnd", "edit", "glyph", "table"],
        checkbox: false,
        dnd: {
            preventVoidMoves: true, // Prevent dropping nodes 'before self', etc.
            preventRecursiveMoves: true, // Prevent dropping nodes on own descendants
            autoExpandMS: 400,
            draggable: {
                scroll: false,
                revert: "invalid"
            },

            dragStart: function (node, data) {
                return true;
            },
            dragEnter: function (node, data) {
                if (node.data.source == null || data.otherNode.data.source == null)
                    return false;
                else {
                    if (node.data.source.IdModulo != data.otherNode.data.source.IdModulo)
                        return false;
                    if (node.data.source.hasOwnProperty('IdDetalle') == data.otherNode.data.source.hasOwnProperty('IdDetalle'))
                        return ["before", "after"];
                        //if (node.data.source.hasOwnProperty('IdDetalle') != data.otherNode.data.source.hasOwnProperty('IdDetalle'))
                        //    return true;
                    else return false;
                }
            },
            dragDrop: function (node, data) {
                ConfirmJQ("¿Esta seguro de mover la opción del menú?", function () {
                    data.otherNode.moveTo(node, data.hitMode);
                    var lstOrden = [];
                    var bln_IdDetalle = false;
                    var obj_Detalle = null;
                    var i = 0;
                    var obj_NodePadre = node.getParent();
                    obj_NodePadre.visit(function (node) {
                        if (node.data.source.hasOwnProperty('IdDetalle')) {
                            obj_Detalle = new Object();
                            obj_Detalle.IdModulo = obj_NodePadre.data.source.IdModulo;
                            obj_Detalle.IdGrupo = obj_NodePadre.data.source.IdGrupo;
                            obj_Detalle.IdGrupoAnterior = node.data.source.IdGrupo;
                            obj_Detalle.IdDetalle = node.data.source.IdDetalle;
                            obj_Detalle.IdDetallePadre = null;
                            obj_Detalle.Orden = i + 1;
                            if (obj_NodePadre.data.source != null)
                                if (obj_NodePadre.data.source.hasOwnProperty('IdDetalle'))
                                    obj_Detalle.IdDetallePadre = obj_NodePadre.data.source.IdDetalle;
                            lstOrden[i] = obj_Detalle;

                            node.data.source.IdModulo = obj_Detalle.IdModulo;
                            node.data.source.IdGrupo = obj_Detalle.IdGrupo;
                            node.data.source.IdDetallePadre = obj_Detalle.IdDetallePadre;
                            bln_IdDetalle = true;
                        }
                        else {
                            obj_Detalle = new Object();
                            obj_Detalle.IdModulo = node.data.source.IdModulo;
                            obj_Detalle.IdGrupo = node.data.source.IdGrupo;
                            obj_Detalle.Orden = i + 1;
                            lstOrden[i] = obj_Detalle;
                        }
                        i++;
                    });
                    if (lstOrden.length > 0) {
                        if (bln_IdDetalle)
                            Accion("Menu.aspx/ModificarOrdenGrupoDetalle", { "lst_pGrupoDetalle": JSON.stringify(lstOrden) });
                        else
                            Accion("Menu.aspx/ModificarOrdenGrupo", { "lst_pGrupo": JSON.stringify(lstOrden) });
                    }
                });
            },
            dragStop: function (node, data) {

            }
        },
        glyph: glyph_opts,
        selectMode: 1,
        ajax: { type: "POST", contentType: "application/json" },
        source: {
            url: "Menu.aspx/ListarGrupo",
            data: JSON.stringify({
                "str_pIdModulo": str_IdModulo,
                "str_pDescripcion": str_Descripcion,
                "str_pURL": str_URL,
                "str_pFlagVisible": str_FlagVisible,
                "str_pEstado": str_Estado
            }),
            cache: false
        },
        lazyLoad: function (event, data) {
            var str_IdModulo = data.node.data.source.IdModulo;
            var str_IdGrupo = data.node.data.source.IdGrupo;
            var str_IdDetalle = null;
            if (data.node.data.source.hasOwnProperty('IdDetalle'))
                str_IdDetalle = data.node.data.source.IdDetalle;
            data.result = {
                url: "Menu.aspx/ListarGrupoDetalle",
                data: JSON.stringify({
                    "str_pIdModulo": str_IdModulo,
                    "str_pIdGrupo": str_IdGrupo,
                    "str_pIdDetalle": str_IdDetalle,
                    "str_pDescripcion": str_Descripcion,
                    "str_pURL": str_URL,
                    "str_pFlagVisible": str_FlagVisible,
                    "str_pEstado": str_Estado
                }),
                debugDelay: 1000
            };
        },
        table: {
            nodeColumnIdx: 0
        },
        renderColumns: function (event, data) {
            var node = data.node,
              $tdList = $(node.tr).find(">td");
            $tdList.eq(1).text(
                data.node.data.source != null ?
                (data.node.data.source.hasOwnProperty('URL') ? node.data.source.URL : '') : '');
            $tdList.eq(2).html(
               data.node.data.source != null ?
               (data.node.data.source.hasOwnProperty('FlagVisible') ?
               (node.data.source.FlagVisible == 'S' ?
               "<input type='checkbox' checked disabled />" :
               "<input type='checkbox' disabled />") : '') : '');
            $tdList.eq(3).text(
                data.node.data.source != null ?
                (node.data.source.Estado == 'A' ? 'Activo' : 'Inactivo') : '');
        }
    });

}

function MostrarVentanaGrupo(str_pTitle) {

    var obj_modal = $("#mdGrupo").modal('show', {
        backdrop: true,
        keyboard: false,
    });

    obj_modal.find('.modal-header h3').text(str_pTitle);

    $('#mdGrupo').on('shown.bs.modal', function () {
        if (!$("#txtCodigoGrupo").prop('readonly'))
            $("#txtCodigoGrupo").focus();
        else
            $("#txtDescripcionGrupo").focus();
    });
}

function MostrarVentanaDetalle(str_pTitle) {
    var obj_modal = $("#mdDetalle").modal('show', {
        backdrop: true,
        keyboard: false,
    });

    obj_modal.find('.modal-header h3').text(str_pTitle);

    $('#mdDetalle').on('shown.bs.modal', function () {
        if (!$("#txtCodigoDetalle").prop('readonly'))
            $("#txtCodigoDetalle").focus();
        else
            $("#txtDescripcionDetalle").focus();
    });
}

function ObtenerOpcion() {
    var bln_Seleccion = true;
    var obj_Seleccion = $("#treetable").fancytree("getActiveNode");
    if (obj_Seleccion != null)
        if (obj_Seleccion.data.source != null)
            bln_Seleccion = false;
    if (bln_Seleccion) {
        obj_Seleccion = null;
        AlertJQ(1, 'Seleccione una opción.');
    }
    return obj_Seleccion;
}

function Agregar() {
    var obj_node = $("#treetable").fancytree("getActiveNode");
    var bln_Grupo = true;
    if (obj_node != null) {
        $("#hdIdModulo").val(obj_node.data.source.IdModulo);
        $("#hdIdGrupo").val('');
        $("#hdIdDetalle").val('');
        if (obj_node.data.source.hasOwnProperty('IdGrupo')) {
            $("#hdIdGrupo").val(obj_node.data.source.IdGrupo);
            if (obj_node.data.source.hasOwnProperty('IdDetalle')) {
                $("#hdIdDetallePadre").val(obj_node.data.source.IdDetalle);
            }
            $("#txtCodigoDetalle").prop('readonly', false);
            $("#txtCodigoDetalle").val('');
            $("#txtDescripcionDetalle").val('');
            $("#txtUrlDetalle").val('');
            $("#hdOrden").val('');
            $("#chkVisibleDetalle").prop('checked', false);
            $("#ddlEstadoDetalle").val('');
            $("#ddUsuarioCreacionDetalle").html('');
            $("#ddFechaCreacionDetalle").html('');
            $("#ddUsuarioModificacionDetalle").html('');
            $("#ddFechaModificacionDetalle").html('');

            $("#lnkEliminarFotoDetalle").css('display', 'none');
            $("#imgImagenDetalle").attr("src", $("#hdImagenMenu").val());
            MostrarVentanaDetalle('Agregar Detalle');
            bln_Grupo = false;
        }
        //}
        if (bln_Grupo) {
            $("#txtCodigoGrupo").prop('readonly', false);
            $("#txtCodigoGrupo").val('');
            $("#txtDescripcionGrupo").val('');
            $("#hdOrden").val('');
            $("#ddlEstadoGrupo").val('');
            $("#ddUsuarioCreacionGrupo").html('');
            $("#ddFechaCreacionGrupo").html('');
            $("#ddUsuarioModificacionGrupo").html('');
            $("#ddFechaModificacionGrupo").html('');

            $("#lnkEliminarFotoGrupo").css('display', 'none');
            $("#imgImagenGrupo").attr("src", $("#hdImagenMenu").val());
            MostrarVentanaGrupo('Agregar Grupo');
        }
    }
}

function Editar() {
    var obj_node = ObtenerOpcion();
    if (obj_node != null) {
        $("#hdIdModulo").val(obj_node.data.source.IdModulo);
        $("#hdIdGrupo").val(obj_node.data.source.IdGrupo);
        $("#hdIdDetalle").val('');
        if (obj_node.data.source.hasOwnProperty('IdDetalle')) {
            $("#hdIdDetalle").val(obj_node.data.source.IdDetalle);
            ObtenerGrupoDetalle();
        }
        else {
            ObtenerGrupo();
        }
    }
}

//Grupo

function ObtenerGrupo() {
    var str_IdModulo = $("#hdIdModulo").val();
    var str_IdGrupo = $("#hdIdGrupo").val();
    if (str_IdGrupo != '') {
        var obj_Grupo = ObtenerData("Menu.aspx/ObtenerGrupo", {
            "str_pIdModulo": str_IdModulo,
            "str_pIdGrupo": str_IdGrupo
        });
        if (obj_Grupo != null) {
            $("#txtCodigoGrupo").prop('readonly', true);
            $("#txtCodigoGrupo").val(obj_Grupo.IdGrupo);
            $("#txtDescripcionGrupo").val(obj_Grupo.Descripcion);
            $("#hdOrden").val(obj_Grupo.Orden);
            $("#hdIdImagenGrupo").val(obj_Grupo.IdImagen);
            $("#ddlEstadoGrupo").val(obj_Grupo.Estado);

            AccionValor('Menu.aspx/ObtenerImagenURL', { "int_pIdRegistro": obj_Grupo.IdImagen, "str_pTipo": "G" },
                function (str_pUrl) {
                    $("#lnkEliminarFotoGrupo").css('display', '');

                    if (str_pUrl == '') {
                        str_pUrl = $("#hdImagenMenu").val();
                        $("#lnkEliminarFotoGrupo").css('display', 'none');
                    }
                    $("#imgImagenGrupo").attr("src", str_pUrl);
                });

            //Auditoria
            $("#ddUsuarioCreacionGrupo").html(obj_Grupo.UsuarioCreacion);
            $("#ddUsuarioModificacionGrupo").html(obj_Grupo.UsuarioModificacion);
            $("#ddFechaCreacionGrupo").html(ToDateTimeString(obj_Grupo.FechaCreacion));
            $("#ddFechaModificacionGrupo").html(ToDateTimeString(obj_Grupo.FechaModificacion));

            MostrarVentanaGrupo('Modificar Grupo');
        }
    }
}

function InicializarGrupo() {
    $("#mdGrupo").find('.modal-footer .btn-aceptar').click(function () {
        GuardarGrupo();
    });

    KeyPressEnter("mdGrupo",
        function () {
            $("#mdGrupo").find('.modal-footer .btn-aceptar').click();
        });

    $("#txtCodigoGrupo").val('');
    $("#txtDescripcionGrupo").val('');
}

function ValidacionGrupo() {
    var settings = $('#frmContent').validate().settings;
    $('.form-group').removeClass('has-error').removeClass('has-success');
    $.extend(settings, {
        rules: {
            ctl00$cphBody$txtCodigoGrupo: { required: true },
            ctl00$cphBody$txtDescripcionGrupo: { required: true },
            ctl00$cphBody$ddlEstadoGrupo: { selectEstado: true }
        },
        messages: {
            ctl00$cphBody$txtCodigoGrupo: { required: 'Ingrese el codigo del grupo' },
            ctl00$cphBody$txtDescripcionGrupo: { required: 'Ingrese la descripción del grupo' }
        }
    });
}

function GuardarGrupo() {
    ValidacionGrupo();
    if ($('#frmContent').valid()) {

        obj_Grupo = new Object();
        obj_Grupo.IdModulo = $("#hdIdModulo").val();
        obj_Grupo.IdGrupo = $("#txtCodigoGrupo").val();
        obj_Grupo.Descripcion = $("#txtDescripcionGrupo").val();
        obj_Grupo.Orden = $("#hdOrden").val();
        obj_Grupo.IdImagen = $("#hdIdImagenGrupo").val();
        obj_Grupo.Estado = $("#ddlEstadoGrupo").val();

        //Eliminacion de la imagen
        var str_Ruta = $('#hdImagenMenu').val();
        if (str_Ruta == $("#imgImagenGrupo").attr('src')) {
            obj_Grupo.Foto = new Object();
            obj_Grupo.Foto.IdAlmacen = $("#hdIdAlmacen").val();
            obj_Grupo.Foto.IdRegistro = -1;
            obj_Grupo.Foto.IdDocumento = null;
        }

        //Anexar Imagen
        if ($("#hdIdDocumento").val() != '') {
            obj_Grupo.Foto = new Object();
            obj_Grupo.Foto.IdAlmacen = $("#hdIdAlmacen").val();
            obj_Grupo.Foto.IdRegistro = -1;
            obj_Grupo.Foto.IdDocumento = $("#hdIdDocumento").val();
        }

        var str_Url = null;
        if ($("#hdIdGrupo").val() == '')
            var str_Url = 'Menu.aspx/GuardarGrupo';
        else
            var str_Url = 'Menu.aspx/ActualizarGrupo';
        Accion(str_Url, { "str_pGrupo": JSON.stringify(obj_Grupo) }, ListarMenu);
        $("#mdGrupo").modal('hide');
    }
    return false;
}

function EliminarFotoGrupo() {
    $("#hdIdDocumento").val('');
    var str_Ruta = $('#hdImagenMenu').val();
    $("#imgImagenGrupo").attr('src', str_Ruta);
    $("#lnkEliminarFotoGrupo").css('display', 'none');
}

//Detalle

function InicializarDetalle() {
    $("#mdDetalle").find('.modal-footer .btn-aceptar').click(function () {
        GuardarDetalle();
    });

    KeyPressEnter("mdDetalle",
       function () {
           $("#mdDetalle").find('.modal-footer .btn-aceptar').click();
       });

    $("#txtCodigoDetalle").val('');
    $("#txtDescripcionDetalle").val('');
    $("#txtUrlDetalle").val('');
}

function ObtenerGrupoDetalle() {
    var str_IdModulo = $("#hdIdModulo").val();
    var str_IdGrupo = $("#hdIdGrupo").val();
    var str_IdDetalle = $("#hdIdDetalle").val();
    if (str_IdDetalle != '') {
        var obj_GrupoDetalle = ObtenerData("Menu.aspx/ObtenerDetalle", {
            "str_pIdModulo": str_IdModulo,
            "str_pIdGrupo": str_IdGrupo,
            "str_pIdDetalle": str_IdDetalle
        });
        if (obj_GrupoDetalle != null) {
            $("#txtCodigoDetalle").prop('readonly', true);
            $("#txtCodigoDetalle").val(obj_GrupoDetalle.IdDetalle);
            $("#txtDescripcionDetalle").val(obj_GrupoDetalle.Descripcion);
            $("#txtUrlDetalle").val(obj_GrupoDetalle.URL);
            $("#hdOrden").val(obj_GrupoDetalle.Orden);
            $("#chkVisibleDetalle").prop('checked', obj_GrupoDetalle.FlagVisible == 'S');
            $("#ddlEstadoDetalle").val(obj_GrupoDetalle.Estado);
            $("#hdIdImagenDetalle").val(obj_GrupoDetalle.IdImagen);
            $("#hdIdDetallePadre").val('');
            if (obj_GrupoDetalle.IdDetallePadre != null)
                $("#hdIdDetallePadre").val(obj_GrupoDetalle.IdDetallePadre);

            AccionValor('Menu.aspx/ObtenerImagenURL', { "int_pIdRegistro": obj_GrupoDetalle.IdImagen, "str_pTipo": "D" },
            function (str_pUrl) {
                $("#lnkEliminarFotoDetalle").css('display', '');

                if (str_pUrl == '') {
                    str_pUrl = $("#hdImagenMenu").val();
                    $("#lnkEliminarFotoDetalle").css('display', 'none');
                }
                $("#imgImagenDetalle").attr("src", str_pUrl);
            });

            //Auditoria
            $("#ddUsuarioCreacionDetalle").html(obj_GrupoDetalle.UsuarioCreacion);
            $("#ddUsuarioModificacionDetalle").html(obj_GrupoDetalle.UsuarioModificacion);
            $("#ddFechaCreacionDetalle").html(ToDateTimeString(obj_GrupoDetalle.FechaCreacion));
            $("#ddFechaModificacionDetalle").html(ToDateTimeString(obj_GrupoDetalle.FechaModificacion));

            MostrarVentanaDetalle('Modificar Detalle');
        }
    }
}

function ValidacionDetalle() {
    var settings = $('#frmContent').validate().settings;
    $('.form-group').removeClass('has-error').removeClass('has-success');
    $.extend(settings, {
        rules: {
            ctl00$cphBody$txtCodigoDetalle: { required: true },
            ctl00$cphBody$txtDescripcionDetalle: { required: true },
            //ctl00$cphBody$txtUrlDetalle: { required: true },
            ctl00$cphBody$ddlEstadoDetalle: { selectEstado: true }
        },
        messages: {
            ctl00$cphBody$txtCodigoDetalle: { required: 'Ingrese el codigo del detalle' },
            ctl00$cphBody$txtDescripcionDetalle: { required: 'Ingrese la descripción del detalle' },
            //ctl00$cphBody$txtUrlDetalle: { required: 'Ingrese la url del detalle' }
        }
    });
}

function GuardarDetalle() {
    ValidacionDetalle();
    if ($('#frmContent').valid()) {

        obj_Detalle = new Object();
        obj_Detalle.IdModulo = $("#hdIdModulo").val();
        obj_Detalle.IdGrupo = $("#hdIdGrupo").val();
        obj_Detalle.IdDetalle = $("#txtCodigoDetalle").val();
        obj_Detalle.Descripcion = $("#txtDescripcionDetalle").val();
        obj_Detalle.IdImagen = $("#hdIdImagenDetalle").val();
        obj_Detalle.IdDetallePadre = null;
        if ($("#hdIdDetallePadre").val() != '')
            obj_Detalle.IdDetallePadre = $("#hdIdDetallePadre").val();
        obj_Detalle.FlagVisible = $("#chkVisibleDetalle").prop('checked') ? 'S' : 'N';
        obj_Detalle.URL = $("#txtUrlDetalle").val();
        obj_Detalle.Orden = $("#hdOrden").val();
        obj_Detalle.Estado = $("#ddlEstadoDetalle").val();

        //Eliminacion de la imagen
        var str_Ruta = $('#hdImagenMenu').val();
        if (str_Ruta == $("#imgImagenDetalle").attr('src')) {
            obj_Detalle.Foto = new Object();
            obj_Detalle.Foto.IdAlmacen = $("#hdIdAlmacen").val();
            obj_Detalle.Foto.IdRegistro = -1;
            obj_Detalle.Foto.IdDocumento = null;
        }

        //Anexar Imagen
        if ($("#hdIdDocumento").val() != '') {
            obj_Detalle.Foto = new Object();
            obj_Detalle.Foto.IdAlmacen = $("#hdIdAlmacen").val();
            obj_Detalle.Foto.IdRegistro = -1;
            obj_Detalle.Foto.IdDocumento = $("#hdIdDocumento").val();
        }

        var str_Url = null;
        if ($("#hdIdDetalle").val() == '')
            var str_Url = 'Menu.aspx/GuardarDetalle';
        else
            var str_Url = 'Menu.aspx/ActualizarDetalle';
        Accion(str_Url, { "str_pDetalle": JSON.stringify(obj_Detalle) }, ListarMenu);
        $("#mdDetalle").modal('hide');
    }
    return false;
}

function EliminarFotoDetalle() {
    $("#hdIdDocumento").val('');
    var str_Ruta = $('#hdImagenMenu').val();
    $("#imgImagenDetalle").attr('src', str_Ruta);
    $("#lnkEliminarFotoDetalle").css('display', 'none');
}
