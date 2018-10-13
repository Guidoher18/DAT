$(document).ready(function () {
    //Registrar el TR de RM
    var x = new moment();

    $('#Submit').on('click', function () {
        var y = new moment();
        var TR = moment.duration(y.diff(x)).as('milliseconds');
        var tiempo = TR.toString();
        $('#RM_TR').val(tiempo);
    });

    // Límite de Tiempo 15 min = 900000ms
    setTimeout(function () {
        $('#Submit').click();
    }, 900000);

    //Centrar Horizontalmente el body>div 
    var $Body_ancho = $(window).width();
    var $Div_ancho = $('body>div').width();
    var $M_total = ($Body_ancho - $Div_ancho) / 2;
    $('body>div').css('margin-left', $M_total, 'margin-right', $M_total);
});