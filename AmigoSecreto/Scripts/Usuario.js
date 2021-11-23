$(document).ready(function () {

    //Compara as senhas
    $("#Senha").bind('input', function (e) {
        validaSenha();
    });

    //Compara as senhas
    $("#ConfirmaSenha").bind('input', function (e) {
        validaSenha();
    });

    $("#validar").click(function () {
        if (validaSenha()) {
            $("#submit1").click();
        }
    });

});

function validaSenha() {
    if ($("#Senha").val() === $("#ConfirmaSenha").val() && $("#Senha").val() !== "") {
        $("#Senha").removeClass("senha-erro");
        $("#ConfirmaSenha").removeClass("senha-erro");
        $("#Senha").addClass("senha-ok");
        $("#ConfirmaSenha").addClass("senha-ok");
        $("#validar").removeAttr("disabled");
        return true;
    } else {
        $("#Senha").removeClass("senha-ok");
        $("#ConfirmaSenha").removeClass("senha-ok");
        $("#Senha").addClass("senha-erro");
        $("#ConfirmaSenha").addClass("senha-erro");
        $("#validar").attr("disabled","disabled");
        return false;
    }
}