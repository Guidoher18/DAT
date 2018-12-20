$(document).ready(function(){
    var audio = document.getElementById("audio");
    
    //Variables Generales para el Test de las Series ppdd
    var Respuesta = '';      //Almacena la respuesta del Sujeto
    
    var Modalidad = '';      //'rojo', Corsi Simple | 'azul', Corsi con Interferencia

    var Habilitar_Clic = 0;  //Permite habilitar el $('div[class="bloques"]').on('click'... sólo cuando responde el sujeto

    var Numero_de_Tarea = 0; //Permite seguir el Itinerario
    var Serie = 0;
    
    var Conteo_de_Clic_en_Bloque = 0;
    
    var Errores = 0;
    var Respuesta1 = "";
    var Respuesta2 = "";
    var Respuesta3 = "";
    
    var Puntaje = 0;
    var Datos_Brutos = "";

    //Variables para los Tiempos de Reacción
    var TRa = null;
    var TRb = null;
    var TRc = null;
    var TRd = null;

    //Variables Específicas de Corsi con Interferencia
    var Combinacion_Poligonos = [] // Array que contiene los nombres de las variables de los tres polìgonos a mostrar
    var Poligono_Elegido='';
    var Habilitar_Clic_Poligono = 0;

    var Poligono_Respuesta1 = "";
    var Poligono_Respuesta2 = "";
    var Poligono_Respuesta3 = "";

    //Cuando hago clic en un bloque, ejecuto la función Clic
    $('div[class="bloques"]').on('click',function(){
        if (Habilitar_Clic == 1){
            Clic(this);
            Conteo_de_Clic_en_Bloque +=1;
            if (Conteo_de_Clic_en_Bloque == Serie){
                Habilitar_Clic = 0;
                if (Modalidad == 'rojo'){
                    setTimeout(function(){
                        Itinerario_CS(Numero_de_Tarea);
                    }, 1500);
                }
                else{
                    setTimeout(function(){
                        Itinerario_CI(Numero_de_Tarea);
                    }, 1500);
                }    
            }
        }
        else {
            return false;
        }
    });
  
    //Cuando hago clic en el polígono registro el Polígono Elegido. 
    $('.Poligonos').on('click', function(){  
        if (Habilitar_Clic_Poligono == 1){  
            var z = $(this).attr('id');
            Poligono_Elegido = $('#' + z + '>polygon').attr('id');
            $('#' + Poligono_Elegido).attr('stroke','#002060');
            $('#' + Poligono_Elegido).attr('stroke-width','2');
            Habilitar_Clic_Poligono = 0;
            $('polygon').css('cursor', 'default');
        }
        else{
            return false;
        }
    });

    var Pintar = function(a){                   //Pinta el bloque de rojo #FF0000 o azul #0000FF
        $(a).addClass(Modalidad); 
    };

    var Despintar = function(a){                //Deja el bloque en blanco #FFF
        $(a).removeClass(Modalidad); 
    };

    var Clic = function(Selector){              //Permite el registro de la respuesta del usuario
        
        //Registro la respuesta
        var Id = $(Selector).attr("id").substr(1);
        Respuesta = Respuesta + Id;
        
        //Se pinta Rojo por 325 ms y luego se despinta
        Pintar(Selector);
        setTimeout(Despintar, 325, Selector);
    };

    //Secuencia, Obtener_Parametros, Mostrar, A, B, C, D, E, F, G, H, I y Pregunta: suena el beep y luego se muestra la secuencia de bloques de a uno por vez cada 1seg. Luego aparece el signo de ? y se muestran nuevamente los bloques para que el sujeto elija 
    var Secuencia = function(y){                //Reproduce el Beep antes de f(x)Mostrar la secuencia
        Habilitar_Clic = 0;
        audio.play();
        setTimeout(function(){
            Mostrar(y);    
        },3000)};

    var Obtener_Parametros = function(g){       //g: String con los items ej.: '4, 5, 6, 7'
        var Bloques = [];                      
        Bloques = g.toString().split(",");                 //Toma los items y los separa en la lista Bloques
        var Parametros = [];
        
        for (i = 0; i<Bloques.length; i++)      //Toma los items de Bloques y les suma a c/u el prefijo 'b#'
        {
            var a = '#b' + Bloques[i];
            Parametros.push(a);
        }                                       
        return Parametros;
    };                                          //Devuelve Parametros = ['b#4', 'b#5', 'b#6', 'b#7']

    var Mostrar = function(y){                  //Obtiene la lista de bloques a mostrar y lo pasa a la f(x)A

        var Parametros = Obtener_Parametros(y); 
        Serie = Parametros.length;              //Serie: Cantidad de items en la serie 

        //Dependiendo de la cant de items de la Serie, varían los parámetros que le paso a la Función A
        switch(Serie){
            case 2: A(Serie, Parametros[0], Parametros[1]);
            break;
            
            case 3: A(Serie, Parametros[0], Parametros[1], Parametros[2]);
            break;
            
            case 4: A(Serie, Parametros[0], Parametros[1], Parametros[2], Parametros[3]);
            break;
            
            case 5: A(Serie, Parametros[0], Parametros[1], Parametros[2], Parametros[3], Parametros[4]);
            break;
            
            case 6: A(Serie, Parametros[0], Parametros[1], Parametros[2], Parametros[3], Parametros[4], Parametros[5]);
            break;
            
            case 7: A(Serie, Parametros[0], Parametros[1], Parametros[2], Parametros[3], Parametros[4], Parametros[5], Parametros[6]);
            break;
            
            case 8: A(Serie, Parametros[0], Parametros[1], Parametros[2], Parametros[3], Parametros[4], Parametros[5], Parametros[6], Parametros[7]);
            break;
            
            case 9: A(Serie, Parametros[0], Parametros[1], Parametros[2], Parametros[3], Parametros[4], Parametros[5], Parametros[6], Parametros[7], Parametros[8]);
            break;
        };
    };

    var Pregunta_Demorada = function(){
        setTimeout(function(){
            Pregunta();
        },1000);
    };

    //Funciones encadenadas que permiten mostrar la secuencia de hasta nueve bloques al estilo del juego "Simon Says"
    var A = function (Serie, a, b, c, d, e, f, g, h, i) {
        Pintar(a);
        setTimeout(function () {
            Despintar(a);
            switch(Serie){
                case 1: 
                break;
                default: B(Serie, b, c, d, e, f, g, h, i);
                break;
            }
        }, 1000);};

    var B = function (Serie, b, c, d, e, f, g, h, i) {
        Pintar(b);
        setTimeout(function () {
            Despintar(b);
            switch(Serie){
                case 2: Pregunta_Demorada();
                break;
                default: C(Serie, c, d, e, f, g, h, i);
            }
        }, 1000);};

    var C = function (Serie, c, d, e, f, g, h, i) {
        Pintar(c);
        setTimeout(function () {
            Despintar(c);
            switch(Serie){
                case 3: Pregunta_Demorada();
                break;
                default: D(Serie, d, e, f, g, h, i);
            }
        }, 1000);};

    var D = function (Serie, d, e, f, g, h, i) {
        Pintar(d);
        setTimeout(function () {
            Despintar(d);
            switch(Serie){
                case 4: Pregunta_Demorada();
                break;
                default: E(Serie, e, f, g, h, i);
            }
        }, 1000);};

    var E = function (Serie, e, f, g, h, i) {
        Pintar(e);
        setTimeout(function () {
            Despintar(e);
            switch(Serie){
                case 5: Pregunta_Demorada();
                break;
                default: F(Serie, f, g, h, i);
            }
        }, 1000);};

    var F = function (Serie, f, g, h, i) {
        Pintar(f);
        setTimeout(function () {
            Despintar(f);
            switch(Serie){
                case 6: Pregunta_Demorada();
                break;
                default: G(Serie, g, h, i);
            }
        }, 1000);};

    var G = function (Serie, g, h, i) {
        Pintar(g);
        setTimeout(function () {
            Despintar(g);
            switch(Serie){
                case 7: Pregunta_Demorada();
                break;
                default: H(Serie, h, i);
            }
        }, 1000);};

    var H = function (Serie, h, i) {
        Pintar(h);
        setTimeout(function () {
            Despintar(h);
            switch(Serie){
                case 8: Pregunta_Demorada();
                break;
                default: I(i);
            }
        }, 1000);};

    var I = function (i) {    
        Pintar(i);
        setTimeout(function(){
            Despintar(i);
            switch(Serie){
                case 9: Pregunta_Demorada();
                break;
            }
         },1000);
    }

    //Luego de la Secuencia de Bloques muestra el ? o Polígonos + ?
    var Pregunta = function(){                              
        if (Modalidad == 'rojo')
        {      
          $('#Instructivo').css('display', 'none');
          $('div[class="bloques"]').css('display', 'none');
          $('#Pregunta').css('display', 'block');
          setTimeout(function(){
              $('#Pregunta').css('display', 'none');
              $('div[class="bloques"]').css('display', 'block');
              Conteo_de_Clic_en_Bloque = 0;
              Habilitar_Clic = 1;
          },1000);
        }
        else{
            $('#Instructivo').css('display', 'none');
            $('div[class="bloques"]').css('display', 'none');
            $('#Poligono').css('display', 'block');
            Crear_Poligono(Combinacion_Poligonos);
            setTimeout(function(){
                $('#Poligono').css('display', 'none');
                $('#Pregunta').css('display', 'block');
                setTimeout(function(){
                    $('#Pregunta').css('display', 'none');
                    $('div[class="bloques"]').css('display', 'block');
                    $('polygon').css('cursor', 'pointer');
                    Conteo_de_Clic_en_Bloque = 0;
                    Habilitar_Clic = 1;
                },1000);
            },19000);
        }
    };
    
    //A es un Array que contiene los Polígonos a mostrar
    var Crear_Poligono = function(A){ 
        $('#PA').html(A[0]);
        $('#PB').html(A[1]);
        $('#PC').html(A[2]);
        Habilitar_Clic_Poligono = 1;
    };
        
    var Consigna_Boton = function(Consigna, a, b){ 
        //a: elementos de html con consigna 
        //b: función a la que redirige el boton
        //c: parámetros de la función anterior                 
        $('div[class="bloques"]').css('display', 'none');
        $('#Instructivo').html('<table><tr><td><p>'+ Consigna + '</p><p><a class="btn btn-info" id="Entendido">Entendido</a></p></td></tr></table>');
        $('#Instructivo').css('display', 'block');
        $('#Entendido').on('click', function(){
            Consigna_Secuencia(a, b);
        });
    };

    var Consigna_Secuencia = function(Consigna, Secuencia_de_Bloques){
        $('#Instructivo>table').remove();
        $('div[class="bloques"]').css('display', 'none');
        $('#Instructivo').html('<p>' + Consigna + '</p>');
        $('#Instructivo').css('display', 'block');
            setTimeout(function () {
                $('#Instructivo').css('display', 'none');
                $('div[class="bloques"]').css('display', 'block');
                Secuencia(Secuencia_de_Bloques);
            }, 4000);
    };

    var Consigna_Respuesta = function(Consigna, Secuencia_de_Bloques){
        Habilitar_Clic = 0;
        $('div[class="bloques"]').css('display', 'none');
        $('#Instructivo').html('<p>'+ Consigna + '</p>');
        $('#Instructivo').css('display', 'block');
            setTimeout(function () {
                $('#Instructivo').css('display', 'none');
                var Parametros = Obtener_Parametros(Secuencia_de_Bloques);
                
                for (i = 0; i<Parametros.length; i++){
                    $(Parametros[i]).html('<p>'+ (i+1) +'</p>');
                }
                
                $('div[class="bloques"]').css('display', 'block');
            }, 4000);            
    };

    //Corrige las respuestas
    var Correccion = function(a,b){  
        if(a != b)
        {
            Errores +=1;
        }
        else
        {
            Puntaje +=1;
        }
    };

    //Almacena las respuestas de la serie CS
    var Guardar_Datos_Brutos_CS = function(a, b, c){       
        Datos_Brutos = Datos_Brutos + "[Serie " + a.length + ": " + a + ", "+ b + ", " + c + "] ";    
    };

    var SalidaCS = function(){
        //Almaceno los valores en los Inputs
        $('#Respuesta_CS').val(Datos_Brutos);
        $('#Puntaje_CS').val(Puntaje);
        TRb = new moment();
        var Duration = moment.duration(TRb.diff(TRa)).as('milliseconds');
        $('#TR_CS').val(Duration.toString());

        //Reinicio las Variables 
        Habilitar_Clic = 0;

        Numero_de_Tarea = 1;  
        Serie = 0;
        
        Conteo_de_Clic_en_Bloque = 0;
        
        Errores = 0;
        Respuesta1 = "";
        Respuesta2 = "";
        Respuesta3 = "";
        
        Puntaje = 0;
        Datos_Brutos = "";

        Itinerario_CI(1);
    };

    var Poligono_Respuesta = function(Poligono, Secuencia_de_Bloques){

        Habilitar_Clic = 0;
        $('div[class="bloques"]').css('display', 'none');
        $('#Poligono_Respuesta').css('display', 'block');
        var a = '';
        
        switch(Poligono){
            case 'A': a = 'el de la izquierda,';
            $('#PRA>p').html('X');
            break;
            
            case 'B': a = 'el del medio,';
            $('#PRB>p').html('X');
            break;
            
            case 'C': a = 'el de la derecha,';
            $('#PRC>p').html('X');
            break;
        }
        
        $('#PRp1').html('El polígono más complejo era ' + a);
        $('#PRp2').html('y la serie de cuadrados azules fue la siguiente:'); 
            
            setTimeout(function () {
                $('#Poligono_Respuesta').css('display', 'none');
                $('#PRA>p, #PRB>p, #PRC>p').html('');
                var Parametros = Obtener_Parametros(Secuencia_de_Bloques);
                
                for (i = 0; i<Parametros.length; i++){
                    $(Parametros[i]).html('<p>'+ (i+1) +'</p>');
                }
                
                $('div[class="bloques"]').css('display', 'block');
            }, 5000); 
    };

    //Almacena las respuestas de la serie CI
    var Guardar_Datos_Brutos_CI = function(a, pa, b, pb, c, pc){       
        Datos_Brutos = Datos_Brutos + "[Serie " + a.length + ": " + a + " - " + pa +', '+ b + " - " + pb +', '+ c + " - " + pc +'] ';
    };

    var SalidaCI = function(){
        //Almaceno los valores en los Inputs
            $('#Respuesta_CI').val(Datos_Brutos);
            $('#Puntaje_CI').val(Puntaje);
            TRd = new moment();
            var Duration = moment.duration(TRd.diff(TRc)).as('milliseconds');
            $('#TR_CI').val(Duration.toString());
            $('#Submit').click();
    };

    var Itinerario_CS = function (a){
        TRa = new moment();
        Modalidad = 'rojo';
        switch (a){
            case 1: Consigna_Boton('A continuación vas a ver unos bloques en los que aparecerán cuadrados rojos, de a uno por vez.<br>Tu objetivo es retener la ubicación de cada cuadrado rojo, en el mismo orden en el que fueron presentados.<br>Luego aparecerá un signo de interrogación y deberás indicar la ubicación de cada cuadrado rojo, en el mismo orden en el que aparecieron.','Vamos a hacer unos ensayos de prueba.', '8,9');
            Numero_de_Tarea +=1;
            break;
            
            case 2:Consigna_Respuesta('El orden en el que aparecieron los cuadrados rojos fue el siguiente:', '8,9');
            Numero_de_Tarea +=1;
            setTimeout(function(){
                $('div[class="bloques"]>p').remove();
                Itinerario_CS(Numero_de_Tarea);
            },8000);
            break;

            case 3: Consigna_Secuencia('Vamos a probar con uno de tres cuadrados rojos', '2,4,1');
            Respuesta = '';
            Numero_de_Tarea +=1;
            break;

            case 4: Consigna_Respuesta('El orden en el que aparecieron los cuadrados rojos fue el siguiente:', '2,4,1');
            Numero_de_Tarea +=1;
            setTimeout(function(){
                $('div[class="bloques"]>p').remove();
                Itinerario_CS(Numero_de_Tarea);
            },8000);
            break;

            //Serie 3
            case 5: Consigna_Boton('Esa fue la práctica.<br> Ahora vamos a empezar con la tarea.','Vamos a arrancar con las series de tres cuadrados rojos', '5,2,4');
            Respuesta = '';
            Numero_de_Tarea +=1;
            break; 

            case 6: Respuesta1 = Respuesta;     //Almaceno la Respuesta
            Correccion(Respuesta1,'524');       //Corrijo
            Respuesta = '';                     //Vacio el input type text
            Secuencia('9,2,6');
            Numero_de_Tarea +=1;
            break; 

            case 7: Respuesta2 = Respuesta;
            Correccion(Respuesta2, '926');
            Respuesta = '';
            Secuencia ('3,8,5');
            Numero_de_Tarea +=1;
            break; 

            //Serie 4
            case 8:Respuesta3 = Respuesta;

            Correccion(Respuesta3, '385');
            Guardar_Datos_Brutos_CS(Respuesta1, Respuesta2, Respuesta3);
            
                if (Errores <2){
                    Respuesta = '';
                    
                    Errores = 0;
                    Respuesta1 = "";
                    Respuesta2 = "";
                    Respuesta3 = "";

                    Consigna_Secuencia('Vamos a arrancar con las series de cuatro cuadrados rojos', '2,9,6,4');
                    Respuesta = '';
                    Numero_de_Tarea +=1;
                    break
                ;}
                
                else{
                    SalidaCS();
                    break;
                } 
            
            case 9: Respuesta1 = Respuesta;    
            Correccion(Respuesta1,'2964');                 
            Respuesta = '';                       
            Secuencia('6,1,3,2');
            Numero_de_Tarea +=1;
            break; 

            case 10: Respuesta2 = Respuesta;
            Correccion(Respuesta2, '6132');
            Respuesta = '';
            Secuencia ('4,8,2,3');
            Numero_de_Tarea +=1;
            break;

            //Serie 5
            case 11:Respuesta3 = Respuesta;

            Correccion(Respuesta3, '4823');
            Guardar_Datos_Brutos_CS(Respuesta1, Respuesta2, Respuesta3);
            
                if (Errores <2){
                    Respuesta = '';
                    
                    Errores = 0;
                    Respuesta1 = "";
                    Respuesta2 = "";
                    Respuesta3 = "";

                    Consigna_Secuencia('Vamos a arrancar con las series de cinco cuadrados rojos', '3,9,8,7,1');
                    Respuesta = '';
                    Numero_de_Tarea +=1;
                    break
                ;}
                
                else{
                    SalidaCS();
                    break;
                } 
            
            case 12: Respuesta1 = Respuesta;    
            Correccion(Respuesta1,'39871');                 
            Respuesta = '';                       
            Secuencia('6,9,7,4,8');
            Numero_de_Tarea +=1;
            break; 

            case 13: Respuesta2 = Respuesta;
            Correccion(Respuesta2, '69748');
            Respuesta = '';
            Secuencia ('8,7,9,2,4');
            Numero_de_Tarea +=1;
            break; 

            //Serie 6
            case 14:Respuesta3 = Respuesta;

            Correccion(Respuesta3, '87924');
            Guardar_Datos_Brutos_CS(Respuesta1, Respuesta2, Respuesta3);
            
                if (Errores <2){
                    Respuesta = '';
                    
                    Errores = 0;
                    Respuesta1 = "";
                    Respuesta2 = "";
                    Respuesta3 = "";

                    Consigna_Secuencia('Vamos a arrancar con series de seis cuadrados rojos', '6,8,7,9,5,4');
                    Respuesta = '';
                    Numero_de_Tarea +=1;
                    break
                ;}
                
                else{
                    SalidaCS();
                    break;
                } 
            
            case 15: Respuesta1 = Respuesta;    
            Correccion(Respuesta1,'687954');                 
            Respuesta = '';                       
            Secuencia('7,8,6,4,5,3');
            Numero_de_Tarea +=1;
            break; 

            case 16: Respuesta2 = Respuesta;
            Correccion(Respuesta2, '786453');
            Respuesta = '';
            Secuencia('7,6,9,5,8,4');
            Numero_de_Tarea +=1;
            break; 
            
            //Serie 7
            case 17:Respuesta3 = Respuesta;

            Correccion(Respuesta3, '769584');
            Guardar_Datos_Brutos_CS(Respuesta1, Respuesta2, Respuesta3);
            
                if (Errores <2){
                    Respuesta = '';
                    
                    Errores = 0;
                    Respuesta1 = "";
                    Respuesta2 = "";
                    Respuesta3 = "";

                    Consigna_Secuencia('Vamos a arrancar con series de siete cuadrados rojos', '2,9,1,7,3,8,4');
                    Respuesta = '';
                    Numero_de_Tarea +=1;
                    break
                ;}
                
                else{
                    SalidaCS();
                    break;
                } 
            
            case 18: Respuesta1 = Respuesta;    
            Correccion(Respuesta1,'2917384');                 
            Respuesta = '';                       
            Secuencia('6,9,4,5,8,1,7');
            Numero_de_Tarea +=1;
            break; 

            case 19: Respuesta2 = Respuesta;
            Correccion(Respuesta2, '6945817');
            Respuesta = '';
            Secuencia('1,5,6,2,4,9,3');
            Numero_de_Tarea +=1;
            break; 
            
            //Serie 8
            case 20:Respuesta3 = Respuesta;

            Correccion(Respuesta3, '1562493');
            Guardar_Datos_Brutos_CS(Respuesta1, Respuesta2, Respuesta3);
            
                if (Errores <2){
                    Respuesta = '';
                    
                    Errores = 0;
                    Respuesta1 = "";
                    Respuesta2 = "";
                    Respuesta3 = "";

                    Consigna_Secuencia('Vamos a arrancar con series de ocho cuadrados rojos', '3,8,9,7,1,2,4,6');
                    Respuesta = '';
                    Numero_de_Tarea +=1;
                    break
                ;}
                
                else{
                    SalidaCS();
                    break;
                } 
            
            case 21: Respuesta1 = Respuesta;    
            Correccion(Respuesta1,'38971246');                 
            Respuesta = '';                       
            Secuencia('4,5,3,9,2,1,6,8');
            Numero_de_Tarea +=1;
            break; 

            case 22: Respuesta2 = Respuesta;
            Correccion(Respuesta2, '45392168');
            Respuesta = '';
            Secuencia('2,1,4,5,8,3,6,9');
            Numero_de_Tarea +=1;
            break; 
            
            //Serie 9
            case 23:Respuesta3 = Respuesta;

            Correccion(Respuesta3, '21458369');
            Guardar_Datos_Brutos_CS(Respuesta1, Respuesta2, Respuesta3);
            
                if (Errores <2){
                    Respuesta = '';
                    
                    Errores = 0;
                    Respuesta1 = "";
                    Respuesta2 = "";
                    Respuesta3 = "";

                    Consigna_Secuencia('Vamos a arrancar con series de nueve cuadrados rojos', '3,9,8,6,4,1,2,7,5');
                    Respuesta = '';
                    Numero_de_Tarea +=1;
                    break
                ;}
                
                else{
                    SalidaCS();
                    break;
                } 
            
            case 24: Respuesta1 = Respuesta;    
            Correccion(Respuesta1,'398641275');                 
            Respuesta = '';                       
            Secuencia('5,7,9,8,1,2,6,4,3');
            Numero_de_Tarea +=1;
            break; 

            case 25: Respuesta2 = Respuesta;
            Correccion(Respuesta2, '579812643');
            Respuesta = '';
            Secuencia('9,2,6,4,5,7,1,8,3');
            Numero_de_Tarea +=1;
            break; 

            case 26: Respuesta3 = Respuesta;
            Correccion(Respuesta3, '926457183');
            Guardar_Datos_Brutos_CS(Respuesta1, Respuesta2, Respuesta3);
            Numero_de_Tarea = 0;
            SalidaCS();
            break; 
        }

    };

    var Itinerario_CI = function(a){
        TRc = new moment();
        Modalidad = 'azul';
        switch(a){
            case 1: Consigna_Boton('A continuación vas a ver unos bloques en los que aparecerán cuadrados azules, de a uno por vez.<br> Luego aparecerán tres polígonos de diferente complejidad, y vas a tener que decidir cuál es el más complejo (el que tiene más lados). <br> Finalmente vas a ver un signo de pregunta. En ese momento vas a tener que indicar la ubicación de cada cuadrado azul en el mismo orden en el que fueron presentados.','Vamos a hacer unos ensayos de prueba' ,'4,3');
            Combinacion_Poligonos = [P7, P10, P8];
            Numero_de_Tarea +=1;
            break;
            
            case 2:Poligono_Respuesta('B', '4,3');
            Numero_de_Tarea +=1;
            setTimeout(function(){
                $('div[class="bloques"]>p').remove();
                Itinerario_CI(Numero_de_Tarea);
            },9000);
            break;

            case 3: Consigna_Secuencia('Vamos a hacer uno más de prueba', '8,6,4');
            Respuesta = '';
            Combinacion_Poligonos = [P7, P10, P12];
            Numero_de_Tarea +=1;
            break;

            case 4: Poligono_Respuesta('C', '8,6,4');
            Numero_de_Tarea +=1;
            setTimeout(function(){
                $('div[class="bloques"]>p').remove();
                Itinerario_CI(Numero_de_Tarea);
            },9000);
            break;
            
            //Serie 2
            case 5: Consigna_Boton('Esa fue la práctica.<br> Ahora vamos a empezar con la tarea.','Vamos a arrancar con las series de dos cuadrados azules', '8,6');
            Respuesta = '';
            Combinacion_Poligonos = [P6, P12, P8];
            Numero_de_Tarea +=1;
            break; 

            case 6: Respuesta1 = Respuesta;              //Almaceno la Respuesta
            Poligono_Respuesta1 = Poligono_Elegido;      //Almaceno el Poligono Elegido 
            Correccion(Respuesta1,'86');                 //Corrijo
            Respuesta = '';                              //Vacio el input type text
            Poligono_Elegido = '';                       //Vacio la variable del Poligono Elegido
            
            Secuencia('5,2');
            Combinacion_Poligonos= [P10, P5, P7];
            Numero_de_Tarea +=1;
            break; 

            case 7: Respuesta2 = Respuesta;
            Poligono_Respuesta2 = Poligono_Elegido;
            Correccion(Respuesta2, '52');
            Respuesta = '';
            Poligono_Elegido = '';   
            
            Secuencia ('9,1');
            Combinacion_Poligonos= [P12, P7, P10];
            Numero_de_Tarea +=1;
            break; 

            //Serie 3
            case 8:Respuesta3 = Respuesta;
            Poligono_Respuesta3 = Poligono_Elegido;
            Correccion(Respuesta3, '91');
            Guardar_Datos_Brutos_CI(Respuesta1, Poligono_Respuesta1, Respuesta2, Poligono_Respuesta2, Respuesta3, Poligono_Respuesta3);
            
                if (Errores <2){
                    Respuesta = '';
                    
                    Errores = 0;
                    Respuesta1 = "";
                    Respuesta2 = "";
                    Respuesta3 = "";
                    Poligono_Respuesta1 = "";
                    Poligono_Respuesta2 = "";
                    Poligono_Respuesta3 = "";

                    Consigna_Secuencia('Vamos a arrancar con las series de tres cuadrados azules', '1,3,8');
                    Respuesta = '';
                    Poligono_Elegido = '';
                    Combinacion_Poligonos= [P7, P10, P8];
                    Numero_de_Tarea +=1;
                    break
                ;}
                
                else{
                    SalidaCI();
                    break;
                } 
            
            case 9: Respuesta1 = Respuesta;  
            Poligono_Respuesta1 = Poligono_Elegido;      
            Correccion(Respuesta1,'138');                
            Respuesta = '';                     
            Poligono_Elegido = '';                        
                
            Secuencia('2,1,4');
            Combinacion_Poligonos= [P8, P5, P6];
            Numero_de_Tarea +=1;
            break; 
    
            case 10: Respuesta2 = Respuesta;
            Poligono_Respuesta2 = Poligono_Elegido;
            Correccion(Respuesta2, '214');
            Respuesta = '';
            Poligono_Elegido = '';   
                
            Secuencia ('7,5,6');
            Combinacion_Poligonos= [P5, P7, P10];
            Numero_de_Tarea +=1;
            break; 
            
            //Serie 4
            case 11:Respuesta3 = Respuesta;
            Poligono_Respuesta3 = Poligono_Elegido;
            Correccion(Respuesta3, '756');
            Guardar_Datos_Brutos_CI(Respuesta1, Poligono_Respuesta1, Respuesta2, Poligono_Respuesta2, Respuesta3, Poligono_Respuesta3);
        
                if (Errores <2){
                    Respuesta = '';
                    
                    Errores = 0;
                    Respuesta1 = "";
                    Respuesta2 = "";
                    Respuesta3 = "";
                    Poligono_Respuesta1 = "";
                    Poligono_Respuesta2 = "";
                    Poligono_Respuesta3 = "";

                    Consigna_Secuencia('Vamos a arrancar con series de cuatro cuadrados azules.', '3,7,8,2');
                    Respuesta = '';
                    Poligono_Elegido = '';
                    Combinacion_Poligonos= [P10, P8, P12];
                    Numero_de_Tarea +=1;
                    break
                ;}
                
                else{
                    SalidaCI();
                    break;
                } 
        
            case 12: Respuesta1 = Respuesta;  
            Poligono_Respuesta1 = Poligono_Elegido;      
            Correccion(Respuesta1,'3782');                
            Respuesta = '';                     
            Poligono_Elegido = '';                        
                
            Secuencia('4,5,2,6');
            Combinacion_Poligonos= [P7, P10, P8];
            Numero_de_Tarea +=1;
            break; 

            case 13: Respuesta2 = Respuesta;
            Poligono_Respuesta2 = Poligono_Elegido;
            Correccion(Respuesta2, '4526');
            Respuesta = '';
            Poligono_Elegido = '';   
                
            Secuencia ('9,4,3,8');
            Combinacion_Poligonos= [P12, P5, P10];
            Numero_de_Tarea +=1;
            break; 

            //Serie 5
            case 14:Respuesta3 = Respuesta;
            Poligono_Respuesta3 = Poligono_Elegido;
            Correccion(Respuesta3, '9438');
            Guardar_Datos_Brutos_CI(Respuesta1, Poligono_Respuesta1, Respuesta2, Poligono_Respuesta2, Respuesta3, Poligono_Respuesta3);
            
                if (Errores <2){
                    Respuesta = '';
                    
                    Errores = 0;
                    Respuesta1 = "";
                    Respuesta2 = "";
                    Respuesta3 = "";
                    Poligono_Respuesta1 = "";
                    Poligono_Respuesta2 = "";
                    Poligono_Respuesta3 = "";

                    Consigna_Secuencia('Vamos a arrancar con series de cinco cuadrados azules.', '9,2,5,1,6');
                    Respuesta = '';
                    Poligono_Elegido = '';
                    Combinacion_Poligonos= [P10, P8, P7];
                    Numero_de_Tarea +=1;
                    break
                ;}
                
                else{
                    SalidaCI();
                    break;
                } 
            
            case 15: Respuesta1 = Respuesta;  
            Poligono_Respuesta1 = Poligono_Elegido;      
            Correccion(Respuesta1,'92516');                
            Respuesta = '';                     
            Poligono_Elegido = '';                        
                
            Secuencia('1,3,5,8,7');
            Combinacion_Poligonos= [P5, P8, P6];
            Numero_de_Tarea +=1;
            break; 
    
            case 16: Respuesta2 = Respuesta;
            Poligono_Respuesta2 = Poligono_Elegido;
            Correccion(Respuesta2, '13587');
            Respuesta = '';
            Poligono_Elegido = '';   
                
            Secuencia ('4,6,9,7,3');
            Combinacion_Poligonos= [P10, P8, P6];
            Numero_de_Tarea +=1;
            break; 

            //Serie 6
            case 17:Respuesta3 = Respuesta;
            Poligono_Respuesta3 = Poligono_Elegido;
            Correccion(Respuesta3, '46973');
            Guardar_Datos_Brutos_CI(Respuesta1, Poligono_Respuesta1, Respuesta2, Poligono_Respuesta2, Respuesta3, Poligono_Respuesta3);
            
                if (Errores <2){
                    Respuesta = '';
                    
                    Errores = 0;
                    Respuesta1 = "";
                    Respuesta2 = "";
                    Respuesta3 = "";
                    Poligono_Respuesta1 = "";
                    Poligono_Respuesta2 = "";
                    Poligono_Respuesta3 = "";

                    Consigna_Secuencia('Vamos a arrancar con series de seis cuadrados azules.', '4,9,2,6,7,1');
                    Respuesta = '';
                    Poligono_Elegido = '';
                    Combinacion_Poligonos= [P6, P5, P8];
                    Numero_de_Tarea +=1;
                    break
                ;}
                
                else{
                    SalidaCI();
                    break;
                } 
            
            case 18: Respuesta1 = Respuesta;  
            Poligono_Respuesta1 = Poligono_Elegido;      
            Correccion(Respuesta1,'492671');                
            Respuesta = '';                     
            Poligono_Elegido = '';                        
                
            Secuencia('5,3,1,7,4,2');
            Combinacion_Poligonos= [P12, P8, P10];
            Numero_de_Tarea +=1;
            break; 
    
            case 19: Respuesta2 = Respuesta;
            Poligono_Respuesta2 = Poligono_Elegido;
            Correccion(Respuesta2, '531742');
            Respuesta = '';
            Poligono_Elegido = '';   
                
            Secuencia ('8,6,3,9,4,1');
            Combinacion_Poligonos= [P7, P10, P8];
            Numero_de_Tarea +=1;
            break; 

            //Serie 7
            case 20:Respuesta3 = Respuesta;
            Poligono_Respuesta3 = Poligono_Elegido;
            Correccion(Respuesta3, '863941');
            Guardar_Datos_Brutos_CI(Respuesta1, Poligono_Respuesta1, Respuesta2, Poligono_Respuesta2, Respuesta3, Poligono_Respuesta3);
            
                if (Errores <2){
                    Respuesta = '';
                    
                    Errores = 0;
                    Respuesta1 = "";
                    Respuesta2 = "";
                    Respuesta3 = "";
                    Poligono_Respuesta1 = "";
                    Poligono_Respuesta2 = "";
                    Poligono_Respuesta3 = "";

                    Consigna_Secuencia('Vamos a arrancar con series de siete cuadrados azules.', '4,1,8,7,5,2,9');
                    Respuesta = '';
                    Poligono_Elegido = '';
                    Combinacion_Poligonos= [P5, P6, P8];
                    Numero_de_Tarea +=1;
                    break
                ;}
                
                else{
                    SalidaCI();
                    break;
                } 
            
            case 21: Respuesta1 = Respuesta;  
            Poligono_Respuesta1 = Poligono_Elegido;      
            Correccion(Respuesta1,'4187529');                
            Respuesta = '';                     
            Poligono_Elegido = '';                        
                
            Secuencia('3,9,7,5,4,1,6');
            Combinacion_Poligonos= [P8, P7, P5];
            Numero_de_Tarea +=1;
            break; 
    
            case 22: Respuesta2 = Respuesta;
            Poligono_Respuesta2 = Poligono_Elegido;
            Correccion(Respuesta2, '3975416');
            Respuesta = '';
            Poligono_Elegido = '';   
                
            Secuencia ('9,3,5,6,7,4,2');
            Combinacion_Poligonos= [P6, P10, P8];
            Numero_de_Tarea +=1;
            break; 

            case 23:Respuesta3 = Respuesta;
            Poligono_Respuesta3 = Poligono_Elegido;
            Correccion(Respuesta3, '9356742');
            Guardar_Datos_Brutos_CI(Respuesta1, Poligono_Respuesta1, Respuesta2, Poligono_Respuesta2, Respuesta3, Poligono_Respuesta3);
            SalidaCI();
            break;             
        };
    };

    //POLÍGONOS SVG HTML tomados de: http://w3.unpocodetodo.info/svg/poligonos-regulares.php

    //PENTÁGONO
    var P5 = '<polygon id ="P5" transform="rotate(-90 60 60)" points="110,60 75.45084971874738,107.55282581475768 19.549150281252636,89.38926261462366 19.54915028125263,30.61073738537635 75.45084971874736,12.447174185242318 110,59.999999999999986 " style="fill:#FF0000"></polygon>';

    //HEXÁGONO
    var P6 = '<polygon id ="P6"  transform="rotate(-120 60 60)" points="110,60 85,103.30127018922192 35.000000000000014,103.30127018922194 10,60.00000000000001 34.99999999999998,16.698729810778076 85,16.69872981077807 110,59.999999999999986 " style="fill:#FF0000"></polygon>';

    //HEPTÁGONO
    var P7 = '<polygon id ="P7"  transform="rotate(-90 60 60)" points="110,60 91.17449009293668,99.0915741234015 48.87395330218428,108.74639560909118 14.951556604879045,81.69418695587791 14.951556604879045,38.305813044122104 48.87395330218432,11.253604390908812 91.17449009293667,20.908425876598507 110,59.999999999999986 " style="fill:#FF0000"></polygon>';

    //OCTÓGONO
    var P8 = '<polygon id ="P8"  transform="rotate(-157.5 60 60)" points="110,60 95.35533905932738,95.35533905932738 60,110 24.64466094067263,95.35533905932738 10,60.00000000000001 24.644660940672615,24.64466094067263 59.99999999999999,10 95.35533905932738,24.644660940672615 110,59.999999999999986 " style="fill:#FF0000"></polygon>';

    //DECÁGONO
    var P10 = '<polygon id ="P10"  transform="rotate(-180 60 60)" points="110,60 100.45084971874738,89.38926261462366 75.45084971874738,107.55282581475768 44.549150281252636,107.55282581475768 19.549150281252636,89.38926261462366 10,60.00000000000001 19.54915028125263,30.61073738537635 44.54915028125262,12.447174185242325 75.45084971874736,12.447174185242318 100.45084971874736,30.610737385376332 110,59.999999999999986 " style="fill:#FF0000"></polygon>';

    //DODECÁGONO
    var P12 = '<polygon id ="P12"  transform="rotate(-75 60 60)" points="110,60 103.30127018922194,85 85,103.30127018922192 60,110 35.000000000000014,103.30127018922194 16.69872981077806,85 10,60.00000000000001 16.69872981077807,34.99999999999999 34.99999999999998,16.698729810778076 59.99999999999999,10 85,16.69872981077807 103.30127018922192,34.99999999999998 110,59.999999999999986 " style="fill:#FF0000"></polygon>';

    Numero_de_Tarea = 1;
    Itinerario_CS(1);
});