setInterval(validacionesForms, 100);

function validacionesForms() {


    var boton = document.getElementById("registr");

  


     //VALIDACION CONTRASEÑA
    var contra = document.getElementById("contra").value;
    var contranueva = document.getElementById("contranueva").value;

    var contraerror = document.getElementById("contraerror");

    if (contra.length != 0) {
        if (contra.length >= 1 && contra.length < 8) {
            contraerror.innerHTML = "La contraseña es demasiado corta"
            deshabilitarBoton(boton);
        }
        else if (contra.match(/[A-Z]/) == null || contra.match(/[a-z]/) == null || contra.match(/[0-9]/) == null || contra.match(/[$@!%*?&.:,;]/) == null) {
            contraerror.innerHTML = "La contraseña debe tener al menos 1 caracter especial, 1 mayuscula, 1 minuscula y 1 numero";
            deshabilitarBoton(boton);
        }
        else if (contra != contranueva && contranueva != 0) {
            contraerror.innerHTML = "Las contraseñas deben coincidir";
            deshabilitarBoton(boton);
        }
        else {
            contraerror.innerHTML = "";
            habilitarBoton(boton);
        }
    }
    else {
        deshabilitarBoton(boton);

    }
  

  

    //VALIDACION EMAIL
    var email = document.getElementById("email").value;
    var emailerror = document.getElementById("emailerror");

    if (email.length != 0) {
        if (email.match(/@/) == null && email.length >= 1 && email.length < 6) {
            emailerror.innerHTML = "El email es invalido";
            deshabilitarBoton(boton);
        }
        else {
            emailerror.innerHTML = "";
            habilitarBoton(boton);
        }
    }
    else {
        deshabilitarBoton(boton);
    }


    //VALIDACION FECHA DE NACIMIENTO

    var fecha = document.getElementById("fecha").value;
    var fechaerror = document.getElementById("fechaerror");

    if (fecha != null) {
        edad = calcularEdad(fecha);
        if (edad < 10 || edad > 101) {
            fechaerror.innerHTML = "La fecha es invalida, la edad permitida es de 10 a 101 años."
            deshabilitarBoton(boton);
        }
        else {
            fechaerror.innerHTML = "";
            habilitarBoton(boton);
            //fechaerror.parentNode.removeChild(fechaerror);
        }
    }
    else {
        deshabilitarBoton(boton);
    }


    //VALIDACION DNI
    var dni = document.getElementById("dni").value;
    var dnierror = document.getElementById("dnierror");

    if (dni.length != 0) {
        if (dni.length > 1 && dni.length < 8) {
            dnierror.innerHTML = "El dni tiene que tenemos como minimo 7 caracteres";
            deshabilitarBoton(boton);
        }
        else {
            dnierror.innerHTML = "";
            habilitarBoton(boton);
        }
    }
    else {
        deshabilitarBoton(boton);
    }


    //VALIDACION NOMBRE Y DNI
    var nombre = document.getElementById("nombre").value;
    var apellido = document.getElementById("apellido").value;
    var nombreerror = document.getElementById("nombreerror");
    var apellidoerror = document.getElementById("apellidoerror");

    if (nombre.length != 0) {
        if (nombre.length > 1 && nombre.length < 3) {
            nombreerror.innerHTML = "El nombre tiene que tener como minimo 3 caracteres";
            deshabilitarBoton(boton);
        }
        else {
            nombreerror.innerHTML = "";
            habilitarBoton(boton);
        }
    }
    else {
        deshabilitarBoton(boton);
    }

    if (apellido.length != 0) {
        if (apellido.length > 1 && apellido.length < 3) {
            apellidoerror.innerHTML = "El apellido tiene que tener como minimo 3 caracteres";
            deshabilitarBoton(boton);
        }
        else {
            apellidoerror.innerHTML = "";
            habilitarBoton(boton);       
        }
    }
    else {
        deshabilitarBoton(boton);
    }



 
}

function calcularEdad(fecha) {
    var hoy = new Date();
    var fechaIngresada = new Date(fecha);
    var edad = hoy.getFullYear() - fechaIngresada.getFullYear();
    var m = hoy.getMonth() - fechaIngresada.getMonth();

    if (m < 0 || (m === 0 && hoy.getDate() < fechaIngresada.getDate())) {
        edad--;
    }
    return edad;
}


function deshabilitarBoton(boton){
    boton.style.pointerEvents = 'none';
    //boton.style.color = '#bbb';
}
function habilitarBoton(boton){
    boton.style.pointerEvents = null;

}
      


 
