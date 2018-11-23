$(document).ready(function () {

    // Adicionar Usuário
    $("#formAddUsuario").submit(function (event) {
        event.preventDefault(); 

        if (toastMessage("Sucesso!", "Usuário cadastrada com sucesso!")) {
            $("#formAddUsuario").submit();
        }
    });
});