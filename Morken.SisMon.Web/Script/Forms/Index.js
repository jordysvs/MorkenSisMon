//var obj_DashBoard = ObtenerData("Index.aspx/ObtenerDashBoard");

//var gcAtendidos = AmCharts.makeChart("gcAtendidos", {
//    "type": "gauge",
//    "theme": "none",
//    "axes": [{
//        "axisThickness": 1,
//        "axisAlpha": 0.1,
//        "tickAlpha": 0.1,
//        "valueInterval": (
//            Math.floor(obj_DashBoard.TotalTramite / 6) == 0 ?
//            1 : Math.floor(obj_DashBoard.TotalTramite / 6)),
//        "bands": [{
//            "color": "#cc4748",
//            "endValue": Math.ceil(obj_DashBoard.TotalTramite / 3),
//            "startValue": 0
//        },
//        {
//            "color": "#fdd400",
//            "endValue": (Math.ceil(obj_DashBoard.TotalTramite / 3) * 2),
//            "startValue": Math.ceil(obj_DashBoard.TotalTramite / 3)
//        },
//        {
//            "color": "#84b761",
//            "endValue": obj_DashBoard.TotalTramite,
//            "startValue": (Math.ceil(obj_DashBoard.TotalTramite / 3) * 2)
//        }],
//        "bottomTextYOffset": -20,
//        "endValue": obj_DashBoard.TotalTramite
//    }],
//    "arrows": [{}]
//});

//setInterval(AtentidoValue, 2000);

//function AtentidoValue() {
//    var value = obj_DashBoard.TotalTramiteAtendido;
//    gcAtendidos.arrows[0].setValue(value);
//    gcAtendidos.axes[0].setBottomText(value + " Trámites atendidos");
//    $("#gcAtendidosComentario").html("Usted tiene " + obj_DashBoard.TotalTramiteAtendido + " trámite(s) atendido(s):<br>" +
//        "<br>- <b>Trámites pendientes</b>: " + obj_DashBoard.TotalTramitePendiente +
//        "<br>- <b>Trámites atendidos</b>: " + obj_DashBoard.TotalTramiteAtendido +
//        "<br>- <b>Total de trámites</b>: " + obj_DashBoard.TotalTramite
//        );
//}

//$(function () {

//    ListarGraficoTramitePendiente()

//})

//function ListarGraficoTramitePendiente() {

//    $("#gcAtendidosATiempoComentario").html("Usted tiene " + obj_DashBoard.TotalTramiteATiempo + " trámite(s) pendiente(s):<br>" +
//    "<br>- <b>Trámites pendientes</b>: " + obj_DashBoard.TotalTramitePendiente +
//    "<br>- <b>Trámites pendientes a tiempo</b>: " + obj_DashBoard.TotalTramiteATiempo +
//    "<br>- <b>Trámites pendientes fuera de tiempo</b>: " + obj_DashBoard.TotalTramiteFueraTiempo);

//    var obj_data = [];
//    obj_data.push(
//        { label: "Trámites pendientes a tiempo", data: obj_DashBoard.TotalTramiteATiempo, color: "green" },
//        { label: "Trámites pendientes fuera de tiempo", data: obj_DashBoard.TotalTramiteFueraTiempo, color: "red" }
//           );


//    $.plot("#divTramitePendiente", obj_data, {
//        series: {
//            pie: {
//                show: true,
//                radius: 1,
//                innerRadius: 0.5,
//                label: {
//                    show: true,
//                    radius: 2 / 3,
//                    formatter: labelFormatter,
//                    threshold: 0.1
//                }

//            }
//        },
//        legend: {
//            show: true
//        }
//    });
//}

//function labelFormatter(label, series) {
//    return "<div style='font-size:13px; text-align:center; padding:2px; color: #fff; font-weight: 600;'>"
//            + "<br/>"
//            + Math.round(series.percent) + "%</div>";
//}