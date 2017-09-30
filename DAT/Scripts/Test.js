$(document).ready(function () {
    $('#Siguiente').click(function () {
        //Obtengo la Respuesta Elegida
        var $Letra_elegida = $("input[type=radio][name=opcion]:checked").val();
        
        //Obtengo los numeros de la serie 1, 2, 3, 4, etc.
        var $Ejercicio = parseInt($('#Ejercicio').html());
        var $Resultado = $Ejercicio + 1;

        if ($Resultado != 18) {
            //Función que Setea la Respuesta en el Input Oculto y pasa al Prox. #Ejercicio e #Imagen img
            var $Cambio = (function (a, b) {
                $('#' + a + $Ejercicio).val($Letra_elegida);
                $('#Ejercicio').replaceWith('<h2 id="Ejercicio">' + b + $Resultado + '</h2>');
                $('#Imagen img').replaceWith('<img src="../../dat_img/RA/' + b + $Resultado + '.jpg"/>');
            });

            if ($Resultado < 10) {
                $Cambio(0, 0);
            }
            else if ($Resultado === 10) {
                $Cambio(0, '');
            }
            else {
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