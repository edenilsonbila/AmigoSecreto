$(document).ready(function() {

    $("button[name='btnParticipar'").click(function() {
        var id = $(this).data("id");
        $.ajax({
            url: '/participante/participar/',
            type: 'POST',
            datatype: 'application/json',
            data: { amigoSecretoId: id }, //Valor passsado como parâmetro
            success: function(data) {
                if (data === "true") {
                    alert("PARABENS VOCE ESTA PARTICIPANDO DESTE AMIGO SECRETO, AGUARDE A LIBERAÇÃO DO SORTEIO, QUANDO TODOS ESTIVEREM CADASTRADOS");
                    window.location.reload();
                }
            },
            error: function(xhr, status, error) {
                alert('ERRO AO PROCESSAR REQUISIÇÃO. FAVOR INFORMAR AO ADMINISTRADOR DO SISTEMA.<br>ERRO: ' + error.toUpperCase());
            },
            complete: function() {

            }
        });
    });


    $("button[name='btnSorteio'").click(function() {
        var btnOld = $(this);
        var id = $(this).data("id");
        $.ajax({
            url: '/participante/Sortear/',
            type: 'POST',
            datatype: 'application/json',
            data: { amigoSecretoId: id }, //Valor passsado como parâmetro
            success: function(data) {
                $("#nomePessoaTirada").html(data.Nome);
                $("#Genero").val(data.Genero);
                $("#Idade").val(data.Idade);
                $("#TamCalcado").val(data.TamCalcado);
                $("#TamCamisa").val(data.TamCamisa);
                $("#TamCalca").val(data.TamCalca);
                $("#SugestaoPresente").val(data.SugestaoPresente);
                $("#modalDetalhes").modal("show");
                $(btnOld).replaceWith("<button type='button' name='btnDetalhes' data-id='" + data.Id + "'  class='btn btn-success btn-md'><span class='glyphicon glyphicon-dashboard'></span> Detalhes do Sorteado</button>");

                dados();
            },
            error: function(xhr, status, error) {
                alert('ERRO AO PROCESSAR REQUISIÇÃO. FAVOR INFORMAR AO ADMINISTRADOR DO SISTEMA.<br>ERRO: ' + error.toUpperCase());
            },
            complete: function() {

            }
        });
    });

    dados();

    $("#Login").blur(function() {

        validaUsuario();
    });

});

function dados() {
    $("button[name='btnDetalhes'").click(function () {
        var id = $(this).data("id");
        $.ajax({
            url: '/participante/DadosUsuario/',
            type: 'POST',
            datatype: 'application/json',
            data: { usuarioId: id }, //Valor passsado como parâmetro
            success: function (data) {
                $("#nomePessoaTirada").html(data.Nome);
                $("#Genero").val(data.Genero);
                $("#Idade").val(data.Idade);
                $("#TamCalcado").val(data.TamCalcado);
                $("#TamCamisa").val(data.TamCamisa);
                $("#TamCalca").val(data.TamCalca);
                $("#SugestaoPresente").val(data.SugestaoPresente);
                $("#modalDetalhes").modal("show");
            },
            error: function (xhr, status, error) {
                alert('ERRO AO PROCESSAR REQUISIÇÃO. FAVOR INFORMAR AO ADMINISTRADOR DO SISTEMA.<br>ERRO: ' + error.toUpperCase());
            },
            complete: function () {

            }
        });
    });
}

function validaUsuario() {
    $("button[name='btnDetalhes'").click(function () {
        var user = $(this).val();
        $.ajax({
            url: '/usuario/ValidaUsuario/',
            type: 'POST',
            datatype: 'application/json',
            data: { user: user }, //Valor passsado como parâmetro
            success: function (data) {
                if (data !== "true") {
                    
                }
            },
            error: function (xhr, status, error) {
                alert('ERRO AO PROCESSAR REQUISIÇÃO. FAVOR INFORMAR AO ADMINISTRADOR DO SISTEMA.<br>ERRO: ' + error.toUpperCase());
            },
            complete: function () {

            }
        });
    });
}