$(function () {
    $(document).keypress(function (e) {
        if (e.which == 13) {
            MostrarMensajeCargando();
            eval($('#lkbIniciar').attr('href'));
        }
    });
});