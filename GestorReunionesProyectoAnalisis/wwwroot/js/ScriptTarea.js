
//FUNCIONES SOBRE AÑADIR TAREAS

function PruebaCerrar() {

    //listaUsuariosTarea = sessionStorage.getItem("ListaUsuariosTarea").split(',');
    //for (i = 0; i < listaUsuariosTarea.length; i++) {
    //    alert(listaUsuariosTarea[i]);
    //}

}


function finalizarCrearTarea(nombreTarea, descripcionTarea) {
   // alert("aaaa");
    //alert("Hola" + sessionStorage.getItem("ListaUsuariosTarea"));

    if (nombreTarea.trim() != "" && descripcionTarea.trim()!="") {

        if (sessionStorage.getItem("ListaUsuariosTarea") == null) {
            $("#tareaDatos").html("No se selecciono un usuario");
        } else {
            var parametros = { "TC_Nombre_Tarea": nombreTarea, "TC_Descripcion_Tarea": descripcionTarea, "listaUsuarios": sessionStorage.getItem("ListaUsuariosTarea") };

            $.ajax(
                {
                    data: parametros,
                    url: '/Tarea/CrearTarea',
                    type: 'post',
                    beforeSend: function () {
                        $("#tareaDatos").html("Procesando, espere por favor ...");
                    },
                    success: function (response) {

                        // $("#tareaDatos").html(response);

                        if (response == "Si Creo") {
                            $("#tareaDatos").html("Exito al guardar");

                        } else {
                            $("#tareaDatos").html("Error al guardar");
                        }

                        sessionStorage.removeItem("ListaUsuariosTarea");

                        //redireccion('/Usuario/MuestraLoginUsuario');
                    }
                }
            );
        }
    } else {
        $("#tareaDatos").html("Faltan espacios por rellenar");
    }
}


function finalizarModificarTarea(idTarea, nombreTarea, descripcionTarea) {
    //alert("hola" + sessionStorage.getItem("ListaUsuariosTarea"));
    //listaUsuariosTarea = sessionStorage.getItem("ListaUsuariosTarea").split(',');
    if (nombreTarea.trim() != "" && descripcionTarea.trim() != "") {
        var parametros = { "TN_Id_Tarea": idTarea, "TC_Nombre_Tarea": nombreTarea, "TC_Descripcion_Tarea": descripcionTarea, "listaUsuarios": sessionStorage.getItem("ListaUsuariosTarea") };

        $.ajax(
            {
                data: parametros,
                url: '/Tarea/ModificarTarea',
                type: 'post',
                beforeSend: function () {
                    $("#tareaDatos").html("Procesando, espere por favor ...");
                },
                success: function (response) {
                    //$("#tareaDatos").html(response);
                    if (response == "Si modifico") {
                        sessionStorage.removeItem("ListaUsuariosTarea");
                        redireccion('/Tarea/viewBuscarTarea');

                        $("#tareaDatos").html("Exito al guardar");

                    } else {
                        sessionStorage.removeItem("ListaUsuariosTarea");
                        $("#tareaDatos").html("Error al guardar");
                    }



                }
            }
        );
    } else {
        $("#tareaDatos").html("Faltan espacios por rellenar");
    }
}


function anadirContactosTarea() {

    //alert("Llegue al metodo");
    sessionStorage.removeItem("ListaUsuariosTarea");
    let listaUsuariosTarea = [];

    $("input[type=checkbox]:checked").each(function () {
        listaUsuariosTarea.push(this.value);
    });

    sessionStorage.setItem("ListaUsuariosTarea", listaUsuariosTarea);


}