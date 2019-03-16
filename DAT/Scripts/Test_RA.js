$(document).ready(function () {
    // Límite de Tiempo 6 min = 360000ms
    setTimeout(function () {
        $('#Submit').click();
    }, 360000);

    //Registrar el TR de RA
    var x = new moment();
    
    $('#Submit').on('click', function () {
        var y = new moment();
        var TR = moment.duration(y.diff(x)).as('milliseconds');
        var tiempo = TR.toString();
        $('#RA_TR').val(tiempo); 
    });
});