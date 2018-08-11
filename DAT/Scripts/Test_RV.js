$(document).ready(function () {
    //Registrar el TR de RV
    var x = new moment();

    $('#Submit').on('click', function () {
        var y = new moment();
        var TR = moment.duration(y.diff(x)).as('milliseconds');
        var tiempo = TR.toString();
        $('#RV_TR').val(tiempo);
    });
});