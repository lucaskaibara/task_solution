function toastMessageSuccess(titulo, mensagem) {
    toastMessage(titulo, mensagem, 'success');
}

function toastMessageError(titulo, mensagem) {
    toastMessage(titulo, mensagem, 'error');
}

function toastMessage(titulo, mensagem, tipoMensagem) {
    $.toast({
        text: mensagem,
        heading: titulo,
        icon: tipoMensagem,
        hideAfter: 3000,
        position: 'bottom-center',
        textAlign: 'left',
        loaderBg: '#FFF'
    });
}