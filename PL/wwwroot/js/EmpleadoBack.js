
var Id = 0;
var prueba = 0;

$(document).ready(function () { //click

    GetAll();
    GetAllDptos();



});


let imagenEmpleadoBase64 = null;

document.getElementById("imgEmpleadoInput").addEventListener("change", async function (event) {
    const file = event.target.files[0];
    if (!file) return;

    imagenEmpleadoBase64 = await fileToBase64(file);
});

function fileToBase64(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onload = () => resolve(reader.result.split(",")[1]);
        reader.onerror = reject;
        reader.readAsDataURL(file);
    });
}
function GetAllDptos() {


    $.ajax({
        type: 'GET',
        url: 'https://localhost:7261/api/SistemaNominaApi/ListaDepartamentos',
        dataType: 'json',
        success: function (result) {
            console.log(result)
            // Agregar nueva opcion a dropdownlist
            $.each(result.objects, function (i, dptos) {
                console.log(dptos)
                // iteramos los valores con foreach
                $("#ddlDepartamento").append('<option value="'  // agregar las opciones que tengamos
                    + dptos.idDepartamento + '">'
                    + dptos.descripcion + '</option>');
            });
        },
        error: function (ex) {
            alert('Failed.' + ex);
        }
    });
}
function GetAll() {
    $.ajax({

        type: 'GET',
        url: 'https://localhost:7261/api/SistemaNominaApi/Empleado/GetAll',
        dataType: 'json',
        success: function (result) { //200 OK 
            $('#TableEmpleados tbody').empty();

            $.each(result.objects, function (i, empleado) {

                var filas =

                    '<tr id="myTableRow' + empleado.idEmpleado + '">' + '<td> <div class="d-flex align-items-center">';
                if (empleado.imagen == null) {
                    filas = filas + ' <img src="/img/úser.png" alt="" style="width: 135px; height: 135px" class="rounded-circle"> </div> </td>';
                }
                else {
                    filas = filas + ' <img src="data:image/png;base64,' + empleado.imagen + '" id="imgUsuario" alt="" style="width: 135px; height: 135px" class="rounded-circle"> </div> </td>';

                }

                filas = filas + '<td>' + empleado.nombre + '</td>' +
                    '<td>' + empleado.apellidoPaterno + '</td>' +
                    '<td>' + empleado.apellidoMaterno + '</td>' +
                    '<td>' + empleado.fechaNacimiento + '</td>' +
                    '<td>' + empleado.rfc + '</td>' +
                    '<td>' + empleado.nss + ' </td> ' +
                    '<td>' + empleado.curp + '</td>' +
                    '<td>' + empleado.correo + '</td>' +
                    '<td>' + empleado.fechaIngreso + '</td>' +
                    '<td>' + empleado.salarioBase + '</td>' +
                    '<td>' + empleado.noFaltas + '</td>' +
                    '<td>' + empleado.departamento.descripcion + '</td>' +
                    '<td> <div class="d-flex align-items-center" ><div class="ms-3"> <button type="button" class="btn btn-outline-warning" onclick="GetById(' + empleado.idEmpleado + ',this)"> <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16"> <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" /><path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z" /></svg></button></div></div></td>' +
                    '<td> <div class="d-flex align-items-center" ><div class="ms-3"> <button type="button" class="btn btn-outline-danger" onclick="Delete(' + empleado.idEmpleado + ',this)"> <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16"> <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" /><path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z" /></svg></button></div></div></td> </tr>'
                    ;


                $("#TableEmpleados tbody").append(filas);


            });

        },

        error: function (result) {

            alert('Error en la consulta.' + result.ErrorMessage);

        }

    });

};


function GetById(IdEmpleado, row) {

    $.ajax({

        type: 'GET',
        url: 'https://localhost:7261/api/SistemaNominaApi/Empleado/GetById?IdEmpleado=' + IdEmpleado,
        dataType: 'json',
        success: function (result) {
            console.log(result)
            //200 OK
            if (result.correct == true) {
                Id = result.object.idEmpleado,

                    $("#TxtNombre").val(result.object.nombre),
                    $("#TxtApellidoPaterno").val(result.object.apellidoPaterno),
                    $("#TxtApellidoMaterno").val(result.object.apellidoMaterno),
                    $("#TxtRFC").val(result.object.rfc),
                    $("#TxtNSS").val(result.object.nss),
                    $("#TxtCURP").val(result.object.curp),
                    $("#TxtCorreo").val(result.object.correo),
                    $("#TxtFechaNacimiento").val(result.object.fechaNacimiento),
                    $("#TxtFechaIngreso").val(result.object.fechaIngreso),
                    $("#ddlDepartamento").val(result.object.departamento.idDepartamento),
                    $("#TxtSalarioBase").val(result.object.salarioBase),
                    $("#TxtNoFaltas").val(result.object.noFaltas)


                if (result.object.imagen == null) {


                    $('#imgUsuarioModal').attr('src', '/img/úser.png');

                }
                else {


                    $('#imgUsuarioModal').attr('src', 'data:image/png;base64,' + result.object.imagen);
                    imagenEmpleadoBase64 = result.object.imagen;

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
            url: 'https://localhost:7261/api/SistemaNominaApi/Empleado/Delete?IdEmpleado=' + IdUsuario,
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





    const Empleado = {
        "idEmpleado": 0,
        "nombre": $("#TxtNombre").val(),
        "apellidoPaterno": $("#TxtApellidoPaterno").val(),
        "apellidoMaterno": $("#TxtApellidoMaterno").val(),
        "fechaNacimiento": $("#TxtFechaNacimiento").val(),
        "rfc": $("#TxtRFC").val(),
        "nss": $("#TxtNSS").val(),
        "curp": $("#TxtCURP").val(),
        "fechaIngreso": $("#TxtFechaIngreso").val(),
        "departamento": {
            "idDepartamento": Number($("#ddlDepartamento").val()),
            "descripcion": "string",
            "departamentos": [
                "string"
            ]
        },
        "salarioBase": Number($("#TxtSalarioBase").val()),
        "noFaltas": Number($("#TxtNoFaltas").val()),
        "imagen": imagenEmpleadoBase64,
        "correo": $("#TxtCorreo").val(),
        "empleados": [
            "string"
        ]

    }
    console.log(Empleado)

    $.ajax({
        type: 'POST',
        url: 'https://localhost:7261/api/SistemaNominaApi/Empleado/Add',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify(Empleado),
        success: function (result) {
            Id = 0

            var row =

                '<tr id="myTableRow' + result.object + '">' + '<td> <div class="d-flex align-items-center">';
            if (imagenEmpleadoBase64 == null) {
                row = row + ' <img src="/img/úser.png" alt="" style="width: 135px; height: 135px" class="rounded-circle"> </div> </td>';
            }
            else {
                row = row + ' <img src="data:image/png;base64,' + imagenEmpleadoBase64 + '" id="imgUsuario" alt="" style="width: 135px; height: 135px" class="rounded-circle"> </div> </td>';

            }

            row = row + '<td>' + $("#TxtNombre").val() + '</td>' +
                '<td>' + $("#TxtApellidoPaterno").val() + '</td>' +
                '<td>' + $("#TxtApellidoMaterno").val() + '</td>' +
                '<td>' + $("#TxtFechaNacimiento").val() + '</td>' +
                '<td>' + $("#TxtRFC").val() + '</td>' +
                '<td>' + $("#TxtNSS").val() + ' </td> ' +
                '<td>' + $("#TxtCURP").val() + '</td>' +
                '<td>' + $("#TxtCorreo").val() + '</td>' +
                '<td>' + $("#TxtFechaIngreso").val() + '</td>' +
                '<td>' + $("#TxtSalarioBase").val() + '</td>' +
                '<td>' + $("#TxtNoFaltas").val() + '</td>' +
                '<td>' + $("#ddlDepartamento option:selected").text() + '</td>' +
                '<td> <div class="d-flex align-items-center" ><div class="ms-3"> <button type="button" class="btn btn-outline-warning" onclick="GetById(' + result.object + ',this)"> <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16"> <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" /><path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z" /></svg></button></div></div></td>' +
                '<td> <div class="d-flex align-items-center" ><div class="ms-3"> <button type="button" class="btn btn-outline-danger" onclick="Delete(' + result.object + ',this)"> <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16"> <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" /><path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z" /></svg></button></div></div></td> </tr>'
                ;
            console.log(row)
            $('#TableEmpleados').append(row);
            alert("Empleado Agregado exitosamente");
            imagenEmpleadoBase64 = null;
            $('#exampleModalLong').modal('hide');
        },
        error: function (result) {
            alert("Hubo un error al agregar al usuario: " + result.ErrorMessage);
            $('#exampleModalLong').modal('show');

        }


    });
}


function Update() {



    if (Id > 0) {
        const Empleado = {
            "idEmpleado": Id,
            "nombre": $("#TxtNombre").val(),
            "apellidoPaterno": $("#TxtApellidoPaterno").val(),
            "apellidoMaterno": $("#TxtApellidoMaterno").val(),
            "fechaNacimiento": $("#TxtFechaNacimiento").val(),
            "rfc": $("#TxtRFC").val(),
            "nss": $("#TxtNSS").val(),
            "curp": $("#TxtCURP").val(),
            "fechaIngreso": $("#TxtFechaIngreso").val(),
            "departamento": {
                "idDepartamento": Number($("#ddlDepartamento").val()),
                "descripcion": "string",
                "departamentos": [
                    "string"
                ]
            },
            "salarioBase": Number($("#TxtSalarioBase").val()),
            "noFaltas": Number($("#TxtNoFaltas").val()),
            "imagen": imagenEmpleadoBase64,
            "correo": $("#TxtCorreo").val(),
            "empleados": [
                "string"
            ]

        }



        console.log(Empleado)
        $.ajax({
            type: 'PUT',
            url: 'https://localhost:7261/api/SistemaNominaApi/Empleado/Update',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify(Empleado),
            success: function (result) {

                if (result.correct == true) {
                    console.log("prueba", prueba)
                    console.log("Id", Id)
                    var row =

                        '<tr id="myTableRow' + Id + '">' + '<td> <div class="d-flex align-items-center">';
                    if (imagenEmpleadoBase64 == null) {
                        row = row + ' <img src="/img/úser.png" id="imgUsuario" alt="" style="width: 135px; height: 135px" class="rounded-circle"> </div> </td>';
                    }
                    else {
                        row = row + ' <img src="data:image/png;base64,' + imagenEmpleadoBase64 + '" id="imgUsuario" alt="" style="width: 135px; height: 135px" class="rounded-circle"> </div> </td>';

                    }

                    row = row + '<td>' + $("#TxtNombre").val() + '</td>' +
                        '<td>' + $("#TxtApellidoPaterno").val() + '</td>' +
                        '<td>' + $("#TxtApellidoMaterno").val() + '</td>' +
                        '<td>' + $("#TxtFechaNacimiento").val() + '</td>' +
                        '<td>' + $("#TxtRFC").val() + '</td>' +
                        '<td>' + $("#TxtNSS").val() + ' </td> ' +
                        '<td>' + $("#TxtCURP").val() + '</td>' +
                        '<td>' + $("#TxtCorreo").val() + '</td>' +
                        '<td>' + $("#TxtFechaIngreso").val() + '</td>' +
                        '<td>' + $("#TxtSalarioBase").val() + '</td>' +
                        '<td>' + $("#TxtNoFaltas").val() + '</td>' +
                        '<td>' + $("#ddlDepartamento option:selected").text() + '</td>' +
                        '<td> <div class="d-flex align-items-center" ><div class="ms-3"> <button type="button" class="btn btn-outline-warning" onclick="GetById(' + Id + ',this)"> <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16"> <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" /><path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z" /></svg></button></div></div></td>' +
                        '<td> <div class="d-flex align-items-center" ><div class="ms-3"> <button type="button" class="btn btn-outline-danger" onclick="Delete(' + Id + ',this)"> <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16"> <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" /><path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z" /></svg></button></div></div></td> </tr>'
                        ;

                    console.log("fila", row)

                    var nuevoId = "myTableRow" + Id;


                    console.log(nuevoId)
                    //const rowToModify = prueba.closest('myTableRow'+Id);
                    const rowToModify = $('#' + nuevoId)
                    //rowToModify.find("td:eq(0)").text("New Value for Column 1"); 

                    console.log("fila cercana", rowToModify)
                    //rowToModify.replaceWith(row)
                    rowToModify.replaceWith(row)
                    prueba = 0
                    Id = 0
                    $('#exampleModalLong').modal('hide');
                    alert("Empleado Modificado Exitosamente")
                    imagenEmpleadoBase64 = null;
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
    $('#imgUsuarioModal').attr('src', '/img/úser.png');
}

function CloseModal() {
    $('#exampleModalLong').modal('hide');

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


