/*
 * General Reference: https://eureka.ykyuen.info/2011/02/22/jquery-javascript-capture-the-browser-or-tab-closed-event/ 
 * This javascript file checks for the brower/browser tab action.
 * It is based on the file menstioned by Daniel Melo.
 * Reference: http://stackoverflow.com/questions/1921941/close-kill-the-session-when-the-browser-or-tab-is-closed
 */
var validNavigation = false;

function wireUpEvents() {
    /**
     * For a list of events that triggers onbeforeunload on IE
     * check http://msdn.microsoft.com/en-us/library/ms536907(VS.85).aspx
     *
     * onbeforeunload for IE and chrome
     * check http://stackoverflow.com/questions/1802930/setting-onbeforeunload-on-body-element-in-chrome-and-ie-using-jquery
     */
    var dont_confirm_leave = 0; //set dont_confirm_leave to 1 when you want the user to be able to leave without confirmation
    var leave_message = 'You sure you want to leave?';
    function goodbye(e) {
        if (!validNavigation) {
            if (dont_confirm_leave !== 1) {
                if (!e) e = window.event;
                //e.cancelBubble is supported by IE - this will kill the bubbling process.
                e.cancelBubble = true;
                e.returnValue = leave_message;
                //e.stopPropagation works in Firefox.
                if (e.stopPropagation) {
                    e.stopPropagation();
                    e.preventDefault();
                }
                //return works for Chrome and Safari
                return leave_message;
            }
        }
    }
    window.onbeforeunload = function () {
        goodbye();
    };

    // Attach the event keypress to exclude the F5 refresh
    $(document).bind('keypress', function (e) {
        if (e.keyCode === 116) {
            validNavigation = true;
        }
    });

    // Attach the event click for all links in the page
    $("a").bind("click", function () {
        validNavigation = true;
    });

    // Attach the event submit for all forms in the page
    $("form").bind("submit", function () {
        validNavigation = true;
    });

    // Attach the event click for all inputs in the page
    $("input[type=submit]").bind("click", function () {
        validNavigation = true;
    });

}

// Based on: https://developer.mozilla.org/es/docs/Web/API/MutationObserver

// selecciona el nodo target
var target = document.body;

// Crea una instancia de observer
var observer = new MutationObserver(function () { chauads() });

// Configura el observer:
var config = { attributes: true, childList: true, characterData: true };

// pasa al observer el nodo y la configuracion
observer.observe(target, config);

// Posteriormente, puede detener la observacion
setTimeout(function () {
    observer.disconnect()
}, 20000)

function chauads() {
    console.log("byeads!");
    $("script[lang='JavaScript']").remove();
    $("script[src*='ads.mgmt']").remove();
    $('div[style="height: 65px;"]').remove();
    $('div[style="opacity: 0.9; z-index: 2147483647; position: fixed; left: 0px; bottom: 0px; height: 65px; right: 0px; display: block; width: 100%; background-color: #202020; margin: 0px; padding: 0px;"]').remove();
    $('div[style*="position: fixed; z-index: 2147483647; left: 0px; bottom: 0px; height: 65px; right: 0px; display: block; width: 100%; background-color: transparent; margin: 0px; padding: 0px;"]').remove();
}

// Wire up the events as soon as the DOM tree is ready
$(document).ready(function () {
    //---Elimina la publicidad agregada por el servidor
    $('center').remove();
    $('script[src="http://ads.mgmt.somee.com/serveimages/ad2/WholeInsert4.js"]').remove();
    $('body>div:not(div:first-child)').remove();
    //---
    wireUpEvents();
});