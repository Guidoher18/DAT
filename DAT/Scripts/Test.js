$(document).ready(function () {
    //Centrar Verticalmente el body>div 
    var $Body_alto = $(window).height();
    var $Div_alto = $('body>div').height();
    var $Margen_total = ($Body_alto - $Div_alto) / 2;
    $('body>div').css('margin-top', $Margen_total, 'margin-bottom', $Margen_total);
    
    //Centrar Horizontalmente el body>div 
    var $Body_ancho = $(window).width();
    var $Div_ancho = $('body>div').width();
    var $M_total = ($Body_ancho - $Div_ancho) / 2;
    $('body>div').css('margin-left', $M_total, 'margin-right', $M_total);

    // Límite de Tiempo 15 min = 900000ms
    setTimeout(function () {
        $('#Submit').click();
    } ,900000);

    $('#Siguiente').click(function () {
        //Obtengo la Respuesta Elegida
        var $Letra_elegida = $("input[type=radio][name=opcion]:checked").val();
        
        //Obtengo los numeros de la serie 1, 2, 3, 4, etc.
        var $Ejercicio = parseInt($('#Ejercicio').html());
        var $Resultado = $Ejercicio + 1;

        if ($Resultado !== 18) {
            //Función que Setea la Respuesta en el Input Oculto y pasa al Prox. #Ejercicio e #Imagen img
            var $Cambio = function (a, b) {
                $('#' + a + $Ejercicio).val($Letra_elegida);
                $("input[type=radio][name=opcion]:checked").prop("checked", false);
                $('#Ejercicio').replaceWith('<h2 id="Ejercicio">' + b + $Resultado + '</h2>');
                $('#Imagen img').replaceWith('<img src="../../dat_img/RA/' + b + $Resultado + '.jpg"/>');
            };

            if ($Resultado < 10) {
                $Cambio(0, 0);
            }
            else if ($Resultado === 10) {
                $Cambio(0, '');
            }
            else {
                if ($Resultado === 17) {
                    $('#Siguiente').prop("value", "Enviar");
                }
                $Cambio('', '');
            }
        }
        else
        {
            $('#' + $Ejercicio).val($Letra_elegida);
            $('#Submit').click();
        }
        

        
    });
});