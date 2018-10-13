$(document).ready(function () {
    /*//Centrar Verticalmente el body>div 
    var $Body_alto = $(window).height();
    var $Div_alto = $('body>div').height();
    var $Margen_total = ($Body_alto - $Div_alto) / 2;
    $('body>div').css('margin-top', $Margen_total, 'margin-bottom', $Margen_total);*/
    
    //Centrar Horizontalmente el body>div 
    var $Body_ancho = $(window).width();
    var $Div_ancho = $('body>div').width();
    var $M_total = ($Body_ancho - $Div_ancho) / 2;
    $('body>div').css('margin-left', $M_total, 'margin-right', $M_total);

    // Límite de Tiempo 10 min = 600000ms
    setTimeout(function () {
        $('#Submit').click();
    }, 600000);

    //Registrar el TR de RA
    var x = new moment();
    
    $('#Submit').on('click', function () {
        var y = new moment();
        var TR = moment.duration(y.diff(x)).as('milliseconds');
        var tiempo = TR.toString();
        $('#RA_TR').val(tiempo); 
    });
});