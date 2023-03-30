
        function enviarCodigo(codigo, id) {
            const url = profUrl + '?codigo=' + codigo;
            $.get(url, function (data) {
                // Obtener el select y vaciar sus opciones actuales
                const select = $("#profesores-select");
                select.empty();

                // Agregar la opción por defecto
                select.append("<option selected disabled value=''>Elige...</option>");

                // Convertir la cadena JSON en una lista
                const profesores = JSON.parse(data);

                // Agregar cada profesor como una opción en el elemento select
                console.log(data);
                profesores.forEach(function (profesor) {
                    select.append("<option value='" + profesor + "'>"+ "Prof. " + profesor + "</option>");
                });

                // Mostrar el modal
                $('#Modal12').modal('show');

                // Asignar el valor del id al input hidden
                $('#input-hidden-id').val(id);
            });
        }

        function crearTutoria() {
            const id = $("[name='id-hidden']").val();
            const profesor = $("[name='prof']").val();
            const dia = $("[name='dia']").val();
            const hora1 = $("[name='hora1']").val();
            const hora2 = $("[name='hora2']").val();
            const url = asignarUrl;
            const data = {
                id: id,
                prof: profesor,
                dia: dia,
                hora1: hora1,
                hora2: hora2
            };
            $.post(url, data, function (response) {
                // Aquí puedes hacer algo con la respuesta del servidor
            });

            $('#Modal12').modal('hide');
        }

        $(".btn-asignar").click(function (event) {
            event.preventDefault();
            crearTutoria();
        });

        const modalEstatus = document.getElementById('Modal3');
        modalEstatus.addEventListener('show.bs.modal', function (event) {
            const button = event.relatedTarget;
            const idEstatus = button.getAttribute('data-id-estatus');
            const continuarBtnEstatus = modalEstatus.querySelector('.btn-estatus');
            continuarBtnEstatus.href = estatusUrl + '?id=' + idEstatus;
        });

        const modalConfirmar = document.getElementById('Modal1');
        modalConfirmar.addEventListener('show.bs.modal', function (event) {
            const buttonConfirmar = event.relatedTarget;
            const idConfirmar = buttonConfirmar.getAttribute('data-id-confirmar');
            const continuarBtnConfirmar = modalConfirmar.querySelector('.btn-confirmar');
            continuarBtnConfirmar.href = confirmarUrl + '?id=' + idConfirmar;
        });

        const modalRechazar = document.getElementById('Modal2');
        modalRechazar.addEventListener('show.bs.modal', function (event) {
            const buttonRechazar = event.relatedTarget;
            const idRechazar = buttonRechazar.getAttribute('data-id-rechazar');
            const continuarBtnRechazar = modalRechazar.querySelector('.btn-rechazar');
            continuarBtnRechazar.href = rechazarUrl + '?id=' + idRechazar;
        });

