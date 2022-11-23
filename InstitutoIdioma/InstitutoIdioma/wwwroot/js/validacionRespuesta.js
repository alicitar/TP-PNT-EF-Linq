function validacionRespuestas() {
    var boton = document.getElementById("boton");
    var estaSeleccionado = false;
    boton.style.pointerEvents = 'none';
    var lista = new Array();
    lista = document.querySelectorAll('input[type=radio]')
    var i
  /*  for (i = 0; i <= lista.length; i++) {
        console.log(lista[i].checked)
        estaSeleccionado = lista[i].checked;
        if (estaSeleccionado == false) {
            boton.style.pointerEvents = 'none';
            console.log("NOestaSeleccioando ");
        }
        else {
            boton.style.pointerEvents = null;
            console.log("esta seleccionado")
        }
    }*/
    while (!estaSeleccionado) {
        for (i = 0; i <= lista.length; i++) {
           // console.log(lista[i].checked)
            if (lista[i].checked == true) {
                boton.style.pointerEvents = null;
            }
        }
    }

}

function validacionAsignacionNiveles() {
    var boton = document.getElementById("AsignarNiveles");
    var select = document.getElementById("Seleccionados");
    select.addEventListener('change',
        function () {
            boton.style.pointerEvents = null;
        }
    );
   
}

function validacionAsignacionPerfiles() {
    var boton = document.getElementById("AsignarPerfiles");
    var select = document.getElementById("Seleccionados");
    select.addEventListener('change',
        function () {
            boton.style.pointerEvents = null;
        }
    );
}