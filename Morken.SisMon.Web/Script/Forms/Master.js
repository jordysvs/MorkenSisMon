$(function () {
    if ($("#fullscreen-btn").length > 0) {
        document.getElementById("fullscreen-btn").addEventListener("click", function () {
            if (screenfull.enabled) {
                if (!screenfull.isFullscreen) {
                    window.sessionStorage.isFullscreen = true;
                    screenfull.request();

                }
                else {
                    window.sessionStorage.isFullscreen = false;
                    screenfull.exit();
                }
            }
        })
    }
    setInterval('relojDigital()', 1000);
});

//Reloj
function relojDigital() {
    var crTime = new Date();
    var crHrs = crTime.getHours();
    var crMns = crTime.getMinutes();
    var crScs = crTime.getSeconds();
    crMns = (crMns < 10 ? "0" : "") + crMns;
    crScs = (crScs < 10 ? "0" : "") + crScs;
    var timeOfDay = (crHrs < 12) ? "AM" : "PM";
    crHrs = (crHrs > 12) ? crHrs - 12 : crHrs;
    crHrs = (crHrs == 0) ? 12 : crHrs;
    var crTimeString = crHrs + ":" + crMns + ":" + crScs + " " + timeOfDay;

    $(".reloj").html(crTimeString);
}
