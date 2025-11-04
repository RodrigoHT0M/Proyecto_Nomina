
var Id = 0;
var prueba = 0;
const radioButtons = document.querySelectorAll('input[name="Rol"]');
var Status = true;


$(document).ready(function () { //click

    GetAll();
    GetAllRols();
    GetAllEmployees();



});


function GetAll() {
    $.ajax({

        type: 'GET',
        url: 'https://localhost:7261/api/SistemaNominaApi/Usuario/GetAll',
        dataType: 'json',
        success: function (result) { //200 OK 
            $('#TableUsuarios tbody').empty();

            $.each(result.objects, function (i, usuario) {
                console.log(usuario)
                var filas =

                    '<tr id="myTableRow' + usuario.idUsuario + '">' + '<div class="d-flex align-items-center">';


                filas = filas + '<td>' + usuario.empleado.nombre + '</td>' +
                    '<td>' + usuario.empleado.apellidoPaterno + '</td>' +
                    '<td>' + usuario.empleado.departamento.descripcion + '</td>' +
                    '<td>' + usuario.nombre + '</td>' +
                    '<td>' + usuario.passwordHash + '</td>' +
                    '<td>' + usuario.status + '</td>' +
                    '<td>' + usuario.rol.descripcion + ' </td> ' +

                    '<td> <div class="d-flex align-items-center" ><div class="ms-3"> <button type="button" class="btn btn-outline-warning" onclick="GetById(' + usuario.idUsuario + ',this)"> <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16"> <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" /><path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z" /></svg></button></div></div></td>' +
                    '<td> <div class="d-flex align-items-center" ><div class="ms-3"> <button type="button" class="btn btn-outline-danger" onclick="Delete(' + usuario.idUsuario + ',this)"> <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16"> <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" /><path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z" /></svg></button></div></div></td> </tr>'
                    ;


                $("#TableUsuarios tbody").append(filas);


            });

        },

        error: function (result) {

            alert('Error en la consulta.' + result.ErrorMessage);

        }

    });

};


function GetById(IdUsuario, row) {

    $.ajax({

        type: 'GET',
        url: 'https://localhost:7261/api/SistemaNominaApi/Usuario/GetById?IdUsuario=' + IdUsuario,
        dataType: 'json',
        success: function (result) {
            console.log(result)
            //200 OK
            if (result.correct == true) {
                Id = result.object.idUsuario,

                    $("#TxtUserName").val(result.object.nombre),

                    $("#TxtIdEmpleado").val(result.object.empleado.idEmpleado),
                    $("#TxtPasswordHash").val(result.object.passwordHash),
                    $("#TxtNoFaltas").val(result.object.noFaltas),
                    $("#ddlRol").val(result.object.rol.idRol)
                if (result.object.status == true) {
                    $('input[name="Rol"][value="true"]').prop('checked', true);
                    $('input[name="Rol"][value="false"]').prop('checked', false);

                }
                else {
                    $('input[name="Rol"][value="true"]').prop('checked', false);

                    $('input[name="Rol"][value="false"]').prop('checked', true);
                }

                $('#BtnAdd').hide()
                $('#BtnUpdate').show()

                $('#exampleModalLong').modal('show');
                prueba = row

            }







        },

        error: function (result) {

            alert('Error en la consulta.' + result.ErrorMessage);

        }

    });

}

function Delete(IdUsuario, row) {


    /*    $('#exampleModal').modal('show');*/
    /*$('#Empleado').text(Nombre)*/
    /*$('#btnEliminarEmpleado').attr('href', '@Url.Action("Delete", "Empleado")' + '?IdEmpleado=' + IdEmpleado)*/

    if (confirm("Deseas eliminar al usuario con ID: " + IdUsuario)) {
        $.ajax({
            type: 'DELETE',
            url: 'https://localhost:7261/api/SistemaNominaApi/Usuario/Delete?IdUsuario=' + IdUsuario,
            dataType: 'json',
            success: function (result) {

                alert("Usuario Eliminado exitosamente");
                /*  $(this).closest('tr').remove();*/

                const rowToRemove = row.closest('tr');
                rowToRemove.remove();

            },
            error: function (result) {
                console.log(result)
            }
        });
    } else {

    }
}

function Add() {

    if (radioButtons[0].checked == true) {
        Status == true
    }
    else if (radioButtons[1].checked == true) {
        Status == false

    }
    else {
        alert("Seleccione el status del Usuario")
    }

    const Usuario = {
        "idUsuario": 0,
        "empleado": {
            "idEmpleado": Number($("#TxtIdEmpleado").val()),
            "nombre": "string",
            "apellidoPaterno": "string",
            "apellidoMaterno": "string",
            "fechaNacimiento": "string",
            "rfc": "string",
            "nss": "string",
            "curp": "string",
            "fechaIngreso": "string",
            "departamento": {
                "idDepartamento": 0,
                "descripcion": "string",
                "departamentos": [
                    "string"
                ]
            },
            "salarioBase": 0,
            "noFaltas": 0,
            "imagen": null,
            "correo": "string",
            "empleados": [
                "string"
            ]
        },
        "nombre": $("#TxtUserName").val(),
        "passwordHash": $("#TxtPasswordHash").val(),
        "status": Status,
        "rol": {
            "idRol": Number($("#TxtIdRol").val()),
            "descripcion": "string",
            "roles": [
                "string"
            ]
        },
        "usuarios": [
            "string"
        ]
    }
    

    $.ajax({
        type: 'POST',
        url: 'https://localhost:7261/api/SistemaNominaApi/Usuario/Add',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify(Usuario),
        success: function (result) {
            Id = 0

            GetAll();



            //var filas =

            //    '<tr id="myTableRow' + result.object+ '">' + '<div class="d-flex align-items-center">';


            //filas = filas + '<td>' + $("#TxtUserName").val() + '</td>' +
            //    '<td>' + usuario.empleado.apellidoPaterno + '</td>' +
            //    '<td>' + usuario.empleado.departamento.descripcion + '</td>' +
            //    '<td>' + usuario.nombre + '</td>' +
            //    '<td>' + usuario.passwordHash + '</td>' +
            //    '<td>' + usuario.status + '</td>' +
            //    '<td>' + usuario.rol.descripcion + ' </td> ' +

            //    '<td> <div class="d-flex align-items-center" ><div class="ms-3"> <button type="button" class="btn btn-outline-warning" onclick="GetById(' + usuario.idUsuario + ',this)"> <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16"> <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" /><path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z" /></svg></button></div></div></td>' +
            //    '<td> <div class="d-flex align-items-center" ><div class="ms-3"> <button type="button" class="btn btn-outline-danger" onclick="Delete(' + usuario.idUsuario + ',this)"> <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16"> <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" /><path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z" /></svg></button></div></div></td> </tr>'
            //    ;
            //console.log(row)
            //$('#TableEmpleados').append(row);
            alert("Empleado Agregado exitosamente");

            $('#exampleModalLong').modal('hide');
        },
        error: function (result) {
            alert("Hubo un error al agregar al usuario: " + result.ErrorMessage);
            $('#exampleModalLong').modal('show');

        }


    });
    $('#exampleModalLong').modal('hide');
}

function Update() {

    //const product = Number($("#TxtIdProducto").val())

    //console.log(product)


    if (Id > 0) {

        if (radioButtons[0].checked == true) {
            Status = true
        }
        else {
            Status = false;
        }
        const Usuario = {
            "idUsuario": Id,
            "empleado": {
                "idEmpleado": Number($("#TxtIdEmpleado").val()),
                "nombre": "string",
                "apellidoPaterno": "string",
                "apellidoMaterno": "string",
                "fechaNacimiento": "string",
                "rfc": "string",
                "nss": "string",
                "curp": "string",
                "fechaIngreso": "string",
                "departamento": {
                    "idDepartamento": 0,
                    "descripcion": "string",
                    "departamentos": [
                        "string"
                    ]
                },
                "salarioBase": 0,
                "noFaltas": 0,
                "imagen": null,
                "correo": "string",
                "empleados": [
                    "string"
                ]
            },
            "nombre": $("#TxtUserName").val(),
            "passwordHash": $("#TxtPasswordHash").val(),
            "status": Status,
            "rol": {
                "idRol": Number($("#ddlRol").val()),
                "descripcion": "string",
                "roles": [
                    "string"
                ]
            },
            "usuarios": [
                "string"
            ]

        }

     
        $.ajax({
            type: 'PUT',
            url: 'https://localhost:7261/api/SistemaNominaApi/Usuario/Update',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify(Usuario),
            success: function (result) {

                if (result.correct == true) {
                    console.log("prueba", prueba)
                    console.log("Id", Id)
                    //var row =

                    //    '<tr id="myTableRow' + Id + '">' + '<td> <div class="d-flex align-items-center">';
                    ///*if (empleado.imagen == null) {*/
                    //row = row + ' <img src="/img/úser.png" id="imgUsuario" alt="" style="width: 135px; height: 135px" class="rounded-circle"> </div> </td>';
                    ///*}*/
                    /////*else {*/
                    ////    filas = filas + ' <img src="data:image/png;base64,' + empleado.imagen + '" id="imgUsuario" alt="" style="width: 135px; height: 135px" class="rounded-circle"> </div> </td>';

                    ////}

                    //row = row + '<td>' + $("#TxtNombre").val() + '</td>' +
                    //    '<td>' + $("#TxtApellidoPaterno").val() + '</td>' +
                    //    '<td>' + $("#TxtApellidoMaterno").val() + '</td>' +
                    //    '<td>' + $("#TxtFechaNacimiento").val() + '</td>' +
                    //    '<td>' + $("#TxtRFC").val() + '</td>' +
                    //    '<td>' + $("#TxtNSS").val() + ' </td> ' +
                    //    '<td>' + $("#TxtCURP").val() + '</td>' +
                    //    '<td>' + $("#TxtFechaIngreso").val() + '</td>' +
                    //    '<td>' + $("#TxtSalarioBase").val() + '</td>' +
                    //    '<td>' + $("#TxtNoFaltas").val() + '</td>' +
                    //    '<td>' + $("#TxtIdDepartamento").val() + '</td>' +
                    //    '<td> <div class="d-flex align-items-center" ><div class="ms-3"> <button type="button" class="btn btn-outline-warning" onclick="GetById(' + result.object + ',this)"> <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16"> <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" /><path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z" /></svg></button></div></div></td>' +
                    //    '<td> <div class="d-flex align-items-center" ><div class="ms-3"> <button type="button" class="btn btn-outline-danger" onclick="Delete(' + result.object + ',this)"> <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16"> <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" /><path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z" /></svg></button></div></div></td> </tr>'
                    //    ;

                    //console.log("fila", row)

                    //var nuevoId = "myTableRow" + Id;


                    //console.log(nuevoId)
                    ////const rowToModify = prueba.closest('myTableRow'+Id);
                    //const rowToModify = $('#' + nuevoId)
                    ////rowToModify.find("td:eq(0)").text("New Value for Column 1"); 

                    //console.log("fila cercana", rowToModify)
                    ////rowToModify.replaceWith(row)
                    //rowToModify.replaceWith(row)
                    prueba = 0
                    Id = 0
                    GetAll();
                    $('#exampleModalLong').modal('hide');
                    alert("Empleado Modificado Exitosamente")
                }


            },
            error: function (result) {

                console.log("mal" + result)
            }
        });
    }


}

function OpenModal() {
    $('#exampleModalLong').modal('show');
    $('#BtnAdd').show()
    $('#BtnUpdate').hide()
    $("#exampleModalLong input").val("");
}

function CloseModal() {
    $('#exampleModalLong').modal('hide');

}

function GetAllRols() {


    $.ajax({
        type: 'GET',
        url: 'https://localhost:7261/api/SistemaNominaApi/ListaRoles',
        dataType: 'json',
        success: function (result) {
            console.log(result)
            // Agregar nueva opcion a dropdownlist
            $.each(result.objects, function (i, dptos) {
                console.log(dptos)
                // iteramos los valores con foreach
                $("#ddlRol").append('<option value="'  // agregar las opciones que tengamos
                    + dptos.idRol+ '">'
                    + dptos.descripcion + '</option>');
            });
        },
        error: function (ex) {
            alert('Failed.' + ex);
        }
    });
}

function GetAllEmployees() {


    $.ajax({
        type: 'GET',
        url: 'https://localhost:7261/api/SistemaNominaApi/ListaEmpleados',
        dataType: 'json',
        success: function (result) {
            console.log(result)
            // Agregar nueva opcion a dropdownlist
            $.each(result.objects, function (i, empleado) {
                console.log(empleado)
                // iteramos los valores con foreach
                $("#TxtIdEmpleado").append('<option value="'  // agregar las opciones que tengamos
                    + empleado.idEmpleado + '">'
                    + empleado.curp  + '</option>');
            });
        },
        error: function (ex) {
            alert('Failed.' + ex);
        }
    });
}


function CargarImagen(event) {
    const file = event.target.files[0];
    if (!file) return;

    const output = document.getElementById('imgUsuarioModal');
    const objectUrl = URL.createObjectURL(file);
    output.src = objectUrl;
    output.style.display = 'inline-block';

    output.onload = function () {
        URL.revokeObjectURL(objectUrl);
    };

    const reader = new FileReader();
    reader.onload = function (e) {
        const base64 = e.target.result.split(',')[1];
        document.querySelector('input[name="imgEmpleadoInput"]').value = base64;
    };
    reader.readAsDataURL(file);

    console.log(base64)
}
