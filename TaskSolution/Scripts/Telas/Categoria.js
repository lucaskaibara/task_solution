$(document).ready(function () {

    // Adicionar Categoria
    $("#formAddCategoria").submit(function (e) {
        /*toastMessage("Sucesso!", "Categoria cadastrada com sucesso!")
        e.preventDefault();
        
        setTimeout(function () {
            $("#formAddCategoria").unbind('submit').submit();
        }, 3000);*/

        e.preventDefault();

        let objCategoria = {
            nome: $("#nome").val(),
            cancelado: $("#cancelado").val()
        }

        $.ajax({
            url: "/Categoria/Cadastrar",
            method: "POST",
            data: { objCategoria: objCategoria },
            success: function (data) {
                console.log(data);
                toastMessageSuccess("Sucesso!", "Categoria cadastrada com sucesso!");
            },
            error: function (data) {
                console.log(data);
                toastMessageError("Erro!", "Categoria cadastrada com sucesso!");
            }
        });
    });

});