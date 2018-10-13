//Centrar Horizontalmente el body>div 
var $Body_ancho = $(window).width();
var $table_ancho = $('table').width();
var $M_total = ($Body_ancho - $table_ancho) / 2;
$('table').css('margin-left', $M_total, 'margin-right', $M_total);