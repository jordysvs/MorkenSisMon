var urlMasterPrincipal = 'Login.aspx';


$(function () {

    ObtenerPagina();

    Timeout();

    var str_Usuario = null;

    $("#update-btn").click(function () {
        document.location.href = UrlPath + 'Forms/Seguridad/PerfilUsuario.aspx';
        return false;
    });

    $("#password-btn").click(function () {
        document.location.href = UrlPath + 'Forms/Seguridad/CambiarContrasenia.aspx';
        return false;
    });

    $("#lock-btn").click(function () {
        Accion(UrlPath + 'Redireccionar.aspx/LockScreen', {}, Lock);
        return false;
    });

    function Lock(obj_pResultado) {
        str_Usuario = obj_pResultado.ReturnId;
        $("#md-locked").modal('show');
        if (internval_Timeout != null) {
            clearInterval(internval_Timeout);
            internval_Timeout = null;
        }
    }

    $("#signout-btn").click(function () {
        ConfirmJQ('¿Está seguro de cerrar la sesión?', function () {
            Accion(UrlPath + 'Redireccionar.aspx/SignOut', {},
                function () {
                    document.location.href = UrlPath + urlMasterPrincipal;
                });
        });
        return false;
    });

    $("#login-btn").click(function () {
        Accion(UrlPath + 'Redireccionar.aspx/Login', { "str_pUsuario": str_Usuario, "str_pPassword": $('#txtPasswordLocked').val() },
            function () {
                $('#txtPasswordLocked').val('')
                $("#md-locked").modal('hide');
                Timeout();
            });
        return false;
    });

});

//Pagina
function ObtenerPagina() {
    var str_RutaMenu = ObtenerData(UrlPath + "Redireccionar.aspx/ObtenerRutaMenu", {});
    var str_IdModulo = window.sessionStorage.IdModulo;
    var str_Path = window.location.href;
    str_Path = "/" + str_Path.replace(/^(?:\/\/|[^\/]+)*\//, "");
    var obj_Pagina = ObtenerData(UrlPath + "Redireccionar.aspx/ObtenerPagina", {
        "str_pIdModulo": str_IdModulo,
        "str_pPath": str_Path
    });
    if (obj_Pagina != null)
    {
        if (obj_Pagina.Foto != null){
            var str_Url = UrlPath + str_RutaMenu + obj_Pagina.Foto.DescripcionFisica + obj_Pagina.Foto.Extension;
            $("#imgIconoPagina").attr("src", str_Url);
        }
    }
}

//Timeout
var int_Initial = 0;
var int_Timeout = null;
var int_TimeoutBefore = null;
var mmd_Timeout = null;
var internval_Timeout = null;

function Timeout() {
    Accion(UrlPath + 'Redireccionar.aspx/Timeout', {},
        function (obj_pResultado) {
            int_Timeout = obj_pResultado.ReturnId * 60;
            int_Initial = int_Timeout;
            int_TimeoutBefore = 60;
            internval_Timeout = setInterval(TimeoutUp, 990);
        });
}

function TimeoutUp() {
    if ($("#lblTimeoutCount").length > 0)
        $("#lblTimeoutCount").html(int_Initial);

    if (int_Initial == 0) {
        Accion(UrlPath + 'Redireccionar.aspx/SignOut', {},
               function () {
                   document.location.href = UrlPath + urlMasterPrincipal;
               });
    }
    else if (int_Initial <= int_TimeoutBefore) {
        if (mmd_Timeout == null) {
            mmd_Timeout = AlertJQ(4, '', function () {
                clearInterval(internval_Timeout);
                internval_Timeout = null;
                mmd_Timeout = null;
                blinkTitleStop();
                Timeout();
            }, {}, true);
            mmd_Timeout.find('.modal-header h3').html('CIERRE DE SESIÓN DE SEGURIDAD');
            mmd_Timeout.find('.modal-body .text-center').html('<span id="lblTimeoutCount" class="timeout">' + int_Initial + '</span> segundos para cierre de sesión automático');
            mmd_Timeout.find('.modal-footer .btn-aceptar').removeClass('btn-danger');
            mmd_Timeout.find('.modal-footer .btn-aceptar').addClass('btn-default');
            mmd_Timeout.find('.modal-footer .btn-aceptar').html('Permanecer Conectado');

            blinkTitle(document.title, "CIERRE DE SESIÓN DE SEGURIDAD", 800);
        }
    }
    int_Initial--;
}

