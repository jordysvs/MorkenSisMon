$(function () {
    $("#lnkInicio").click(function (e) {
        document.location.href = UrlPath + "Curso.aspx";
        return false;
    });

    $("#lnkIniciarSesion").click(function (e) {
        document.location.href = UrlPath + "Forms/Extranet/IniciarSesion.aspx";
        return false;
    });

    $("#lnkRegistrarse").click(function (e) {
        document.location.href = UrlPath + "Forms/Extranet/RegistroUsuario.aspx";
        return false;
    });

    $("#lnkPerfilAlumno").click(function (e) {
        document.location.href = UrlPath + "Forms/Extranet_Alumno/Perfil.aspx";
        return false;
    });

    $("#lnkPerfilCliente").click(function (e) {
        document.location.href = UrlPath + "Forms/Extranet_Cliente/Perfil.aspx";
        return false;
    });
});