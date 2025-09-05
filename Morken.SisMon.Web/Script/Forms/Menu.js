$(function () {
    cargarMenu();
});

//Menu
function cargarMenu() {

    var IdAlmacenGrupo = $("#hdIdAlmacenGrupoMaster").val()
    var IdAlmacenGrupoDetalle = $("#hdIdAlmacenGrupoDetalleMaster").val();

    var ulMenu = $('<ul></ul>');
    var liGrupo = null;
    var aGrupo = null;
    var iGrupo = null;
    var iGrupoIcono = null;
    var spanGrupo = null;
    var ulGrupo = null;

    var str_IdModulo = null;
    if (window.sessionStorage.IdModulo != undefined)
        str_IdModulo = window.sessionStorage.IdModulo;

    var str_RutaMenu = ObtenerData(UrlPath + "Redireccionar.aspx/ObtenerRutaMenu", {});
    var lstGrupo = ObtenerData(UrlPath + "Redireccionar.aspx/ListarGrupo", { "str_pIdModulo": str_IdModulo });
    if (lstGrupo != null) {
        for (var i = 0; i < lstGrupo.length; i++) {
            liGrupo = $("<li class='treeview' style='display:block!important;'></li>");
            aGrupo = $("<a href='#'></a>");
            if (lstGrupo[i].Foto != null)
                iGrupo = $("<i id='G" + lstGrupo[i].IdImagen + "' class='fa fa-link' style='display:none;'></i><img class='iconGrupo' alt='' onerror='this.onerror=null;this.src = \"\";$(\"#G" + lstGrupo[i].IdImagen + "\").css(\"display\",\"\");$(this).css(\"display\",\"none\");' src='" + UrlPath + str_RutaMenu + lstGrupo[i].Foto.DescripcionFisica + lstGrupo[i].Foto.Extension + "' />");
            else
                iGrupo = $("<i id='G" + lstGrupo[i].IdImagen + "' class='fa fa-link'></i>");

            iGrupoIcono = $("<i class='fa fa-angle-left pull-right'></i>");
            spanGrupo = $('<span></span>');
            spanGrupo.text(lstGrupo[i].Descripcion);
            aGrupo.append(iGrupo);
            aGrupo.append(spanGrupo);
            aGrupo.append(iGrupoIcono);
            liGrupo.append(aGrupo);

            if (lstGrupo[i].Detalle.length > 0) {
                ulGrupo = $("<ul class='treeview-menu'></ul>");
                cargarMenuDetalle(IdAlmacenGrupoDetalle, ulGrupo, lstGrupo[i].Detalle, str_RutaMenu);
                liGrupo.append(ulGrupo);
            }
            ulMenu.append(liGrupo);
        }
        $(".sidebar-menu").html(ulMenu.html());
    }
}

function cargarMenuDetalle(int_pIdAlmacen, obj_pUl, lst_pGrupoDetalle, str_pRutaMenu) {
    for (var i = 0; i < lst_pGrupoDetalle.length; i++) {
        if (lst_pGrupoDetalle[i].FlagVisible == 'S') {
            var liGrupoDetalle = $('<li ></li>');
            var aGrupoDetalle = $('<a ></a>');
            var iGrupoDetalle = null;
            if (lst_pGrupoDetalle[i].Foto != null)
                iGrupoDetalle = $("<i id='D" + lst_pGrupoDetalle[i].IdImagen + "' class='fa fa-circle-o' style='display:none;'></i><img class='iconGrupo' alt='' onerror='this.onerror=null;this.src = \"\";$(\"#D" + lst_pGrupoDetalle[i].IdImagen + "\").css(\"display\",\"\");$(this).css(\"display\",\"none\");' src='" + UrlPath + str_pRutaMenu + lst_pGrupoDetalle[i].Foto.DescripcionFisica + lst_pGrupoDetalle[i].Foto.Extension + "' />");
            else
                iGrupoDetalle = $("<i id='D" + lst_pGrupoDetalle[i].IdImagen + "' class='fa fa-circle-o'></i>");

            aGrupoDetalle.append(iGrupoDetalle);
            aGrupoDetalle.append(lst_pGrupoDetalle[i].Descripcion);
            aGrupoDetalle.attr('href', UrlPath + lst_pGrupoDetalle[i].URL);
            liGrupoDetalle.append(aGrupoDetalle);

            if (lst_pGrupoDetalle[i].Detalle.length > 0) {
                var ulGrupoDetalle = $("<ul class='treeview-menu'></ul>");
                cargarMenuDetalle(int_pIdAlmacen, ulGrupoDetalle, lst_pGrupoDetalle[i].Detalle);
                if (ulGrupoDetalle.children().length > 0)
                    liGrupoDetalle.append(ulGrupoDetalle);
            }
            obj_pUl.append(liGrupoDetalle);
        }
    }

}
