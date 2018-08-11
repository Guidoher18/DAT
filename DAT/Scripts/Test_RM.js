$(document).ready(function () {
    //Registrar el TR de RM
    var x = new moment();

    $('#Submit').on('click', function () {
        var y = new moment();
        var TR = moment.duration(y.diff(x)).as('milliseconds');
        var tiempo = TR.toString();
        $('#RM_TR').val(tiempo);
    });
});