$(document).ready(function () {
    //Registrar el TR de RV
    var x = new moment();

    $('#Submit').on('click', function () {
        var y = new moment();
        var TR = moment.duration(y.diff(x)).as('milliseconds');
        var tiempo = TR.toString();
        $('#RV_TR').val(tiempo);
    });

    // Límite de Tiempo 10 min = 600000ms
    setTimeout(function () {
        $('#Submit').click();
    }, 600000);

    //Centrar Horizontalmente el body>div 
    var $Body_ancho = $(window).width();
    var $table_ancho = $('table').width();
    var $M_total = ($Body_ancho - $table_ancho) / 2;
    $('body>div').css('margin-left', $M_total);
});