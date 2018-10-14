$(document).ready(function () {
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