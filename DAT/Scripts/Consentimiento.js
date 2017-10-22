$(document).ready(function () {
    //Centrar Verticalmente el body>div 
    var $Body_alto = $(window).height();
    var $Div_alto = $('body>div').height();
    var $Margen_total = ($Body_alto - $Div_alto)/2;
    $('body>div').css('margin-top', $Margen_total, 'margin-bottom', $Margen_total);

    /*$('#Sexo').on('click', function () {
        var $valorEmail = $('input[type = "email"]').val();
        if ($valorEmail !== '') {
            var resultado = '';
            var $Existe = $.ajax({
                url: "/Home/ConsultaDuplicado",
                async: false,
                type: "POST",
                data: { Mail: $valorEmail },
                dataType: "string",
                success: function (result) {
                    return result; //muestra el resultado en la alerta
                    }
                });
            resultado = result; 
            alert(resultado);
        }
    });*/
});
