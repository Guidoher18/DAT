$(document).ready(function(){
    var audio = document.getElementById("audio");

    var Pintar = function(a){              //Pinta el bloque de rojo #FF0000
        $(a).addClass("rojo");};

    var Despintar = function(a){           //Deja el bloque en blanco #FFF
       $(a).removeClass("rojo");};

    var Clic = function(Selector){         //Función que permite el registro de la respuesta del usuario
        //Registro la respuesta
        var Id = $(Selector).attr("id").substr(1);
        var Input = $('#Respuesta').val();
        var Suma = Input + Id;
        $('#Respuesta').val(Suma); 
        
        //Se pinta Rojo por 325 ms y luego se despinta
        Pintar(Selector);
        setTimeout(Despintar, 325, Selector);};
        
    //Cuando hago clic en un bloque, ejecuto la función Clic
    $('div[class="bloques"]').on('click',function(){
        Clic(this);});

var Secuencia = function(y){                //Reproduce el Beep antes de mostrar la secuencia
    //audio.play();
    setTimeout(function(){
        Mostrar(y);    
    },3000)};

var Mostrar = function(y){                  //Función que permite mostrar la secuencia de bloques
    var Bloques = [];                       //y: String con los items ej.: '4, 5, 6, 7'
    Bloques = y.split(",");                 //Toma los items y los separa en la lista Bloques
    var Serie = Bloques.length;             //Serie: Cantidad de items en la serie 
    var Parametros = [];
    
    for (i = 0; i<Bloques.length; i++)      //Toma los items de Bloques y les suma a c/u el prefijo 'b#' para luego utilizarlo como parámetro
    {
        var a = '#b' + Bloques[i];
        Parametros.push(a);
    }                                       //Obtengo Parametros = ['b#4', 'b#5', 'b#6', 'b#7']
    
    //Dependiendo de la cant de items de la Serie, varían los parámetros que le paso a la Función A
    if (Serie == 2)
    {
        A(Serie, Parametros[0], Parametros[1]);
    }
    else if (Serie == 3)
    {
        A(Serie, Parametros[0], Parametros[1], Parametros[2]);
    }
    else if (Serie == 4)
    {
        A(Serie, Parametros[0], Parametros[1], Parametros[2], Parametros[3]);
    }
    else if (Serie == 5)
    {
        A(Serie, Parametros[0], Parametros[1], Parametros[2], Parametros[3], Parametros[4]);
    }
    else if (Serie == 6)
    {
        A(Serie, Parametros[0], Parametros[1], Parametros[2], Parametros[3], Parametros[4], Parametros[5]);
    }
    else if (Serie == 7)
    {
        A(Serie, Parametros[0], Parametros[1], Parametros[2], Parametros[3], Parametros[4], Parametros[5], Parametros[6]);
    }
    else if (Serie == 8)
    {
        A(Serie, Parametros[0], Parametros[1], Parametros[2], Parametros[3], Parametros[4], Parametros[5], Parametros[6], Parametros[7]);
    }
    else if (Serie == 9)
    {
        A(Serie, Parametros[0], Parametros[1], Parametros[2], Parametros[3], Parametros[4], Parametros[5], Parametros[6], Parametros[7], Parametros[8]);
    }};

//Funciones encadenadas que permiten mostrar la secuencia de hasta nueve bloques al estilo del juego "Simon Says"
var A = function (Serie, a, b, c, d, e, f, g, h, i) {
    Pintar(a);
    setTimeout(function () {
        Despintar(a);
        B(Serie, b, c, d, e, f, g, h, i);
    }, 1000);};

var B = function (Serie, b, c, d, e, f, g, h, i) {
    Pintar(b);
    setTimeout(function () {
        Despintar(b);
        if (Serie > 2) {
            C(Serie, c, d, e, f, g, h, i);
        }
    }, 1000);};

var C = function (Serie, c, d, e, f, g, h, i) {
    Pintar(c);
    setTimeout(function () {
        Despintar(c);
        if (Serie > 3) {
            D(Serie, d, e, f, g, h, i);
        }
    }, 1000);};

var D = function (Serie, d, e, f, g, h, i) {
    Pintar(d);
    setTimeout(function () {
        Despintar(d);
        if (Serie > 4) {
            E(Serie, e, f, g, h, i);
        }
    }, 1000);};

var E = function (Serie, e, f, g, h, i) {
    Pintar(e);
    setTimeout(function () {
        Despintar(e);
        if (Serie > 5) {
            F(Serie, f, g, h, i);
        }
    }, 1000);};

var F = function (Serie, f, g, h, i) {
    Pintar(f);
    setTimeout(function () {
        Despintar(f);
        if (Serie > 6) {
            G(Serie, g, h, i);
        }
    }, 1000);};

var G = function (Serie, g, h, i) {
    Pintar(g);
    setTimeout(function () {
        Despintar(g);
        if (Serie > 7) {
            H(Serie, h, i);
        }
    }, 1000);};

var H = function (Serie, h, i) {
    Pintar(h);
    setTimeout(function () {
        Despintar(h);
        if (Serie > 8) {
            I(i);
        }
    }, 1000);};

var I = function (i) {    
    Pintar(i);
    setTimeout(function(){
        Despintar(i);
        },1000);
    };


function update() {
  $('#clock').html(moment().format('mm:ss'));
}

setInterval(update, 1000);


{
    $('div[class="bloques"]').css('display', 'none');
    $('#Instructivo').html('<div id="Instruccion"><p class="Instruccion">A continuación vas a ver unos bloques en los que aparecerán cuadrados rojos, de a uno por vez.<p><p class="Instruccion">Tu objetivo es retener la ubicación de cada cuadrado rojo en el mismo orden en el que fueron presentados.<p><p class="Instruccion">Luego aparecerá signo de interrogación y deberás indicar la ubicación de cada cuadrado rojo, en el mismo orden en el que aparecieron.<p><a class="btn btn-info" id="Entendido">Entendido</a></div>');
    $('#Entendido').on('click', function () {
        $('#Instruccion').html('<p class="Instruccion" style="margin-top:calc((98vh - 48px)/2)">Vamos a hacer unos ensayos de prueba<p>');
        setTimeout(function () {
            $('#Instruccion').css('display', 'none');
            $('div[class="bloques"]').css('display', 'block');
            setTimeout(function () {
                Secuencia("8,9");
                setTimeout(function () {
                    $('div[class="bloques"]').css('display', 'none');
                }, 3000);
            }, 1000);
        }, 2000);
    });
}
 
 

//var x = "4,5,6,7,3,8"
//Secuencia(x);
});     











