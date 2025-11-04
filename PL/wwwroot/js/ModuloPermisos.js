function ProcesarPermiso(IdStatus, row) {

    var tr = $(row).closest('tr')
    var IdPermiso = $(tr).attr('id')
    $.ajax({
        type: 'PUT',
        url: 'https://localhost:7261/api/Permiso/Update?IdPermiso='+IdPermiso+'&IdStatusPermiso='+IdStatus+'&IdEmpleado='+10,
        dataType: 'json',
        success: function (result) {

            if (result.correct == true) {
                tr.remove();
            }
            else {
                alert ("Ocurrio un error interno en el sistema")
            }
            

        },
        error: function (result) {

        }


    });

}