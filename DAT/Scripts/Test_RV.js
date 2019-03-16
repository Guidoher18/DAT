$(document).ready(function () {
    //Registrar el TR de RV
    var x = new moment();

    $('#Submit').on('click', function () {
        var y = new moment();
        var TR = moment.duration(y.diff(x)).as('milliseconds');
        var tiempo = TR.toString();
        $('#RV_TR').val(tiempo);
    });

    // Límite de Tiempo 6 min = 360000ms
    setTimeout(function () {
        $('#Submit').click();
    }, 360000);
});