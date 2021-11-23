using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmigoSecreto.Models;
using AmigoSecretoSystem.Helpers;

namespace AmigoSecretoSystem.Controllers
{
    public class LoginController : Controller
    {
        protected Context _ctx;

        public LoginController()
        {
            _ctx = new Context();
        }

        // GET: Login
        public ActionResult Logar()
        {
            var teste = Criptografia.Encrypt("48115100");
            return View();
        }
        [HttpPost]
        public ActionResult Logar(Usuario model)
        {
            //Remove todos os usuarios
            Session.RemoveAll();

            //Criptografa o usuario
            var userName = Criptografia.Encrypt(model.Nome.ToUpper());
            //Criptografa a senha
            var pwd = Criptografia.Encrypt(model.Senha);

            //Busca o usuario no banco de dados
            var usuario = _ctx.Usuario.FirstOrDefault(e => e.Login == userName && e.Senha == pwd);

            //Valida se encontrou o usuario
            if (usuario != null)
            {
                //Adiciona uma sessao para o usuario
                Session.Add("usuarioLogado", usuario.Id);
                //Redireciona para a tela principal
                return RedirectToAction("Inicio", "Home");
            }
            else
            {
                //Retorna que o usuaio nao foi encontrado
                TempData["retornoLogin"] = "Usuário ou senha inválidos";
            }
            //Retorna para a tela (View)
            return View();
        }
    }
}