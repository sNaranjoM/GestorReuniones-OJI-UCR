

function recibir(numero) {
    var valor = document.getElementById("texto" + numero).value;
    document.getElementById("codigo").value = valor;

} 


function modalModificarTipoReunion(nombre, idReunion) {

    //sessionStorage.removeItem("NombreTipoReunion");
    //sessionStorage.setItem("NombreTipoReunion", nombre);


    //sessionStorage.removeItem("IdTipoReunion");
    //sessionStorage.setItem("IdTipoReunion", idReunion);

    document.getElementById('ModificarTipoReunion').value = "";
    document.getElementById('IdModificarReunion').value = "";

    document.getElementById('ModificarTipoReunion').value = nombre;
    document.getElementById('IdModificarReunion').value = idReunion;


} 


function modalModificarOficina(idOficina, nombreOficina, codigoOficina) {

   // alert(idOficina + "   " + nombreOficina + "   " + codigoOficina);

    document.getElementById('IdModificarOficina').value = "";
    document.getElementById('ModificarOficina').value = "";
    document.getElementById('CodigoModificarOficina').value = "";

    document.getElementById('IdModificarOficina').value = idOficina;
    document.getElementById('ModificarOficina').value = nombreOficina;
    document.getElementById('CodigoModificarOficina').value = codigoOficina;


} 


function modalModificarPuesto(idPuesto, nombrePuesto, salarioPuesto) {

    document.getElementById('ModificarIdPuesto').value = "";
    document.getElementById('ModificarPuesto').value = "";
    document.getElementById('ModificarSalarioPuesto').value = "";

    document.getElementById('ModificarIdPuesto').value = idPuesto;
    document.getElementById('ModificarPuesto').value = nombrePuesto;
    document.getElementById('ModificarSalarioPuesto').value = salarioPuesto;


} 


function modalModificarRoles(idRol, nombreRol) {

    //alert("LLEGUE ");
    document.getElementById('ModificarIdRol').value = "";
    document.getElementById('ModificarRol').value = "";

    document.getElementById('ModificarIdRol').value = idRol;
    document.getElementById('ModificarRol').value = nombreRol;

} 


function modificarTipoDeReunion(IdModificarReunion, ModificarTipoReunion) {
    
    if (ModificarTipoReunion.trim() != "" ) {

        var parametros = { "TC_Nombre_Tipo_Reunion": ModificarTipoReunion, "TN_Id_Tipo_Reunion": IdModificarReunion };

        $.ajax(
            {
                data: parametros,
                url: '/Catalogo/ModificarTipoReunion',
                type: 'post',
                beforeSend: function () {
                    $("#modTipReun").html("Procesando, espere por favor ...");
                },
                success: function (response) {
                    // window.location.reload();
                    // var html = '<select id="SubCategoria" name="SubCategoria">' + @foreach(SubCategoriaModel temp in ViewBag.SubCategorias) { + '<option value="@temp.IdSubCategoria">'+ @temp.NombreSubCategoria '</option>+' + } +'</select>';                                                                                                         
                    // document.getElementById("SelectSubCategoria").innerHTML = html
                    if (response == "Si modifico") {
                        redireccion('/Catalogo/viewTipoReunionCatalogo');
                    } else {
                        $("#modTipReun").html("Error al guardar");
                    }

                    //redireccion('/Usuario/MuestraLoginUsuario');
                }
            }
        );
    } else {
        $("#modTipReun").html("Faltan espacios por rellenar");
    }

}



function modificarOficina(IdModificarOficina, ModificarOficina, CodigoModificarOficina) {
    
    if (ModificarOficina.trim() != "" && CodigoModificarOficina.trim() != "" ) {
        var parametros = { "TN_Id_Oficina": IdModificarOficina, "TC_Nombre": ModificarOficina, "TC_Codigo": CodigoModificarOficina };

        $.ajax(
            {
                data: parametros,
                url: '/Catalogo/ModificarOficina',
                type: 'post',
                beforeSend: function () {
                    $("#modOfi").html("Procesando, espere por favor ...");
                },
                success: function (response) {

                    //$("#modOfi").html(response);

                    if (response == "Si modifico") {
                        redireccion('/Catalogo/viewOfinaCatalogo');
                    } else {
                        $("#modOfi").html("Error al guardar");
                    }

                }
            }
        );
    } else {
        $("#modOfi").html("Faltan espacios por rellenar");
    }

}


function modificarPuesto(ModificarIdPuesto, ModificarPuesto, ModificarSalarioPuesto) {

    //alert(ModificarIdPuesto + ModificarSalarioPuesto)
    if (ModificarPuesto.trim() != "" && ModificarSalarioPuesto.trim() != "") {

        var parametros = { "TN_Id_Puesto": ModificarIdPuesto, "TC_Nombre_Puesto": ModificarPuesto, "TN_Salario": ModificarSalarioPuesto };

        $.ajax(
            {
                data: parametros,
                url: '/Catalogo/ModificarPuesto',
                type: 'post',
                beforeSend: function () {
                    $("#modTipReun").html("Procesando, espere por favor ...");
                },
                success: function (response) {

                    //$("#modOfi").html(response);

                    if (response == "Si modifico") {
                        redireccion('/Catalogo/viewPuestoUsuarioCatalogo');
                    } else {
                        $("#modPuesto").html("Error al guardar");
                    }

                }
            }
        );
    } else {
        $("#modPuesto").html("Faltan espacios por rellenar");
    }

}



function modificarRol(ModificarIdRol, ModificarRol) {

    if (ModificarRol.trim() != "") {

        var parametros = { "TN_Id_Rol": ModificarIdRol, "TC_Nombre_Rol": ModificarRol, "TN_ID_Permiso": document.getElementById('idPermisos').value };

        $.ajax(
            {
                data: parametros,
                url: '/Catalogo/ModificarRol',
                type: 'post',
                beforeSend: function () {
                    $("#modTipReun").html("Procesando, espere por favor ...");
                },
                success: function (response) {

                    //$("#modRol").html(response);

                    if (response == "Si modifico") {
                        redireccion('/Catalogo/viewRolSistemaCatalogo');
                    } else {
                        $("#modRol").html("Error al guardar");
                    }

                }
            }
        );
    } else {
        $("#modRol").html("Faltan espacios por rellenar");
    }

}




function redireccion(direccion) {
    document.location.href = direccion;
}