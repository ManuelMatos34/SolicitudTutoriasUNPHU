if (tipo && titulo && mensaje) {
    Swal.fire({
        icon: tipo,
        title: titulo,
        text: mensaje,
        confirmButtonColor: '#007e00'
    });
}

