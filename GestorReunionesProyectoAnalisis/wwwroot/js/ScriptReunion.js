

sessionStorage.setItem("validacionListaTemas", 1);


function AgregarAcuerdosTareas(idTarea, acuerdoTarea, nombreTextArea) {

    var parametros = {
        "idTarea": idTarea, "acuerdoTarea": acuerdoTarea
    };

    $.ajax(
        {
            data: parametros,
            url: '/Reunion/AgregarAcuerdosTareas',
            type: 'post',
            beforeSend: function () {
            },
            success: function (response) {

                if (response == "Inserto") {

                    src = "https://cdnjs.cloudflare.com/ajax/libs/jquery/1.12.4/jquery.min.js";
                    src = "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js";

                    $(function () {
                        $("#modalValidaAcuerdos").modal();
                    });
                   
                }
                else {
                    src = "https://cdnjs.cloudflare.com/ajax/libs/jquery/1.12.4/jquery.min.js";
                    src = "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js";

                    $(function () {
                        $("#modalValidaAcuerdosError").modal();
                    });
                }

            }
        }
    );

}

function AgregarAcuerdosTemas(idTemas, acuerdoTema) {

    var parametros = {
        "idTemas": idTemas, "acuerdoTema": acuerdoTema
    };


    $.ajax(
        {
            data: parametros,
            url: '/Reunion/AgregarAcuerdosTemas',
            type: 'post',
            beforeSend: function () {
            },
            success: function (response) {

               
                if (response == "Inserto") {
                    src = "https://cdnjs.cloudflare.com/ajax/libs/jquery/1.12.4/jquery.min.js";
                    src = "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js";

                    $(function () {
                        $("#modalValidaAcuerdos").modal();
                    });
                }
                else {

                    src = "https://cdnjs.cloudflare.com/ajax/libs/jquery/1.12.4/jquery.min.js";
                    src = "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js";

                    $(function () {
                        $("#modalValidaAcuerdosError").modal();
                    });
                    
                }



            }
        }
    );

}






function anadirContactosReunion() {

    sessionStorage.removeItem("ListaUsuariosReunion");
    let listaUsuariosReunion = [];

    $("input[name=usuarioscheckbox]:checked").each(function () {
        listaUsuariosReunion.push(this.value);
    });

    sessionStorage.setItem("ListaUsuariosReunion", listaUsuariosReunion);

    //for (i = 0; i < listaUsuariosReunion.length; i++) {
        //alert(listaUsuariosReunion[i]);
    //}

}

function anadirTareasReunion() {
    //alert("tarea reunion");
    sessionStorage.removeItem("ListaTareasReunion");
    let ListaTareasReunion = [];

    $("input[name=tareascheckbox]:checked").each(function () {
        ListaTareasReunion.push(this.value);
    });

    sessionStorage.setItem("ListaTareasReunion", ListaTareasReunion);

    //for (i = 0; i < ListaTareasReunion.length; i++) {
        //alert(ListaTareasReunion[i]);
    //}

    var listaPrueba = sessionStorage.getItem("ListaTareasReunion");
    //alert(listaPrueba);


}

function anadirTemaReunion(tarea) {
    //sessionStorage.removeItem("ListaTemasReunion");
    //alert("hola");
    if (sessionStorage.getItem("validacionListaTemas") == 1) {
        
        sessionStorage.setItem("ListaTemasReunion", tarea);
        sessionStorage.setItem("validacionListaTemas", 0);
        //alert(sessionStorage.getItem("ListaTemasReunion"));
    } else {
        
        ListaTemasReunion = sessionStorage.getItem("ListaTemasReunion");

        ListaTemasReunion += "&" + tarea;

        sessionStorage.setItem("ListaTemasReunion", ListaTemasReunion);

        //alert(sessionStorage.getItem("ListaTemasReunion"));
    }

    document.getElementById('temaR').value = "";
   // sessionStorage.removeItem("ListaTemasReunion");
   
}



function AgregarArchivo() {
   
    var input = document.getElementById("archivosR");
    var files = input.files;

    var formData = new FormData();

    //se le da el formato de FormData
    for (var i = 0; i != files.length; i++) {
       // alert(files[i].name);
        formData.append("files", files[i]);
    }

    //Se llama al método que está en ArchivoController que recibe un IFormFile
    $.ajax(
        {
            url: '/Archivo/AgregarArchivo',
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
               // alert("Files Uploaded!");
            }
        }
    );

}

function listaArchivos() {
    sessionStorage.setItem("validacionListaTemas", 1);
    var input = document.getElementById("archivosR");
    var files = input.files;
    var ListaArchivosEnviar = "";
    sessionStorage.removeItem("ListaArchivosEnviar");

    for (var i = 0; i != files.length; i++) {

        if (sessionStorage.getItem("validacionListaTemas") == 1) {

            sessionStorage.setItem("validacionListaTemas", 0);

            ListaArchivosEnviar += files[i].name;

        } else {

            ListaArchivosEnviar += "?" + files[i].name;

        }

    }

    sessionStorage.setItem("ListaArchivosEnviar", ListaArchivosEnviar);
    //alert(sessionStorage.getItem("ListaArchivosEnviar"));
    sessionStorage.setItem("validacionListaTemas", 1);

}



function EliminarArchivo(TN_Id_Archivo) {

    var parametros = {
        "TN_Id_Archivo": TN_Id_Archivo
    };


    $.ajax(
        {
            data: parametros,
            url: '/Archivo/EliminarArchivo',
            type: 'post',
            beforeSend: function () {
            },
            success: function (response) {

                if (response == true) {

                    $("#" + TN_Id_Archivo).remove();
                } else {
                    $("#ArchivosEliminar").html("Error al eliminar");
                }

            }
        }
    );
}




function finalizarCrearReunion(nombreReunion, tipoReunion, descripcion, comentario, lugar, fechaReunion) {
    //alert("usuarios:"+sessionStorage.getItem("ListaUsuariosReunion") + " ListaTareas"+sessionStorage.getItem("ListaTareasReunion") + " LISTAREUNION:"+sessionStorage.getItem("ListaTemasReunion") );
   



    if (nombreReunion.trim() != "" && descripcion.trim() != "" && comentario.trim() != "" && lugar.trim() != "" && fechaReunion.trim() != "") {
       
        var date = new Date();
        var fecha = date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate();

        fechaHora = fechaReunion.split('T');

        fechaSplit = fechaHora[0].split('-');
        horaSplit = fechaHora[1].split(':');

        anno = fechaSplit[0];
        mes = fechaSplit[1];
        dia = fechaSplit[2];

        hora = horaSplit[0];
        minuto = horaSplit[1];

        if (anno > date.getFullYear()
            || anno == date.getFullYear() && mes > (date.getMonth() + 1)
            || anno == date.getFullYear() && mes == (date.getMonth() + 1) && dia > date.getDate()
            || anno == date.getFullYear() && mes == (date.getMonth() + 1) && dia == date.getDate() && hora > date.getHours()
            || anno == date.getFullYear() && mes == (date.getMonth() + 1) && dia == date.getDate() && hora == date.getHours() && minuto > date.getMinutes()) {

            if (sessionStorage.getItem("ListaUsuariosReunion") == null || sessionStorage.getItem("ListaTareasReunion") == null || sessionStorage.getItem("ListaTemasReunion") == null || sessionStorage.getItem("ListaArchivosEnviar") == null) {
                $("#reunionDatos").html("La lista de Usuarios, Tareas, Temas o Archivos esta vacia");

            } else {

                var parametros = {
                    "TC_Nombre_Reunion": nombreReunion, "TN_Id_Tipo_Reunion": tipoReunion, "TC_Descripcion": descripcion, "TC_Comentario": comentario, "TC_Lugar": lugar, "TC_Fecha_Reunion": fechaReunion,
                    "TC_Lista_Usuarios": sessionStorage.getItem("ListaUsuariosReunion"), "TC_Lista_Tareas": sessionStorage.getItem("ListaTareasReunion"),
                    "TC_Lista_Temas": sessionStorage.getItem("ListaTemasReunion"), "TC_Lista_Archivos": sessionStorage.getItem("ListaArchivosEnviar")
                };

                $.ajax(
                    {
                        data: parametros,
                        url: '/Reunion/CrearReunion',
                        type: 'post',
                        beforeSend: function () {
                            $("#tareaDatos").html("Procesando, espere por favor ...");
                        },
                        success: function (response) {

                            // $("#reunionDatos").html(response);

                            if (response == "Si Creo") {
                                AgregarArchivo();
                                $("#reunionDatos").html("Exito al guardar");

                            } else {
                                $("#reunionDatos").html("Error al guardar");
                            }

                            sessionStorage.removeItem("ListaUsuariosReunion");
                            sessionStorage.removeItem("ListaTareasReunion");
                            sessionStorage.removeItem("ListaTemasReunion");
                            sessionStorage.removeItem("ListaArchivosEnviar");
                            sessionStorage.setItem("validacionListaTemas", 1);

                        }
                    }
                );
            }
        } else {
            $("#reunionDatos").html("La fecha es menor a la fecha actual");
        }
    } else {
        $("#reunionDatos").html("Faltan espacios por rellenar");
    }
}

function finalizarModificarReunion(idReunion, nombreReunion, tipoReunion, descripcion, comentario, lugar, fechaReunion) {

    if (nombreReunion.trim() != "" && descripcion.trim() != "" && comentario.trim() != "" && lugar.trim() != "" && fechaReunion.trim() != "") {
        //alert("Hola");
        var date = new Date();
        var fecha = date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate();

        fechaHora = fechaReunion.split('T');

        fechaSplit = fechaHora[0].split('-');
        horaSplit = fechaHora[1].split(':');

        anno = fechaSplit[0];
        mes = fechaSplit[1];
        dia = fechaSplit[2];

        hora = horaSplit[0];
        minuto = horaSplit[1];

        if (anno > date.getFullYear()
            || anno == date.getFullYear() && mes > (date.getMonth() + 1)
            || anno == date.getFullYear() && mes == (date.getMonth() + 1) && dia > date.getDate()
            || anno == date.getFullYear() && mes == (date.getMonth() + 1) && dia == date.getDate() && hora > date.getHours()
            || anno == date.getFullYear() && mes == (date.getMonth() + 1) && dia == date.getDate() && hora == date.getHours() && minuto > date.getMinutes()) {

            if (sessionStorage.getItem("ListaUsuariosReunion") == null || sessionStorage.getItem("ListaTareasReunion") == null) {
                $("#reunionDatos").html("La lista de Usuarios, Tareas, Temas o Archivos esta vacia");
            } else {

                if (sessionStorage.getItem("ListaTemasReunion") == null) {
                    sessionStorage.setItem("ListaTemasReunion", "sin_temas");
                }
                if (sessionStorage.getItem("ListaArchivosEnviar") == null) {
                    sessionStorage.setItem("ListaArchivosEnviar", "sin_archivos")
                }

                var parametros = {
                    "TN_Id_Reunion": idReunion, "TC_Nombre_Reunion": nombreReunion, "TN_Id_Tipo_Reunion": tipoReunion, "TC_Descripcion": descripcion, "TC_Comentario": comentario, "TC_Lugar": lugar, "TC_Fecha_Reunion": fechaReunion,
                    "TC_Lista_Usuarios": sessionStorage.getItem("ListaUsuariosReunion"), "TC_Lista_Tareas": sessionStorage.getItem("ListaTareasReunion"),
                    "TC_Lista_Temas": sessionStorage.getItem("ListaTemasReunion"), "TC_Lista_Archivos": sessionStorage.getItem("ListaArchivosEnviar")
                };

                $.ajax(
                    {
                        data: parametros,
                        url: '/Reunion/ModificarReunion',
                        type: 'post',
                        beforeSend: function () {
                            $("#tareaDatos").html("Procesando, espere por favor ...");
                        },
                        success: function (response) {

                            // $("#reunionDatos").html(response);

                            if (response == "Si Creo") {
                                AgregarArchivo();
                                $("#reunionDatos").html("Exito al guardar");
                                redireccion("/Reunion/viewBuscaReunion");

                            } else {
                                $("#reunionDatos").html("Error al guardar");
                            }

                            sessionStorage.removeItem("ListaUsuariosReunion");
                            sessionStorage.removeItem("ListaTareasReunion");
                            sessionStorage.removeItem("ListaTemasReunion");
                            sessionStorage.removeItem("ListaArchivosEnviar");
                            sessionStorage.setItem("validacionListaTemas", 1);

                        }
                    }
                );
            }
        } else {
            $("#reunionDatos").html("La fecha es menor a la fecha actual");
        }
    } else {
        $("#reunionDatos").html("Faltan espacios por rellenar");
    }
}




