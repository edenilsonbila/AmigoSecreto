using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AmigoSecreto.Helpers;
using AmigoSecreto.Models;
using AmigoSecretoSystem.Helpers;

namespace AmigoSecreto.Controllers
{
    public class UsuarioController : Controller
    {
        protected Context _ctx;

        public UsuarioController()
        {
            _ctx = new Context();
        }

        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Novo()
        {
            ViewBag.Genero = SelectListItemHelper.Genero();
            return View();
        }

        public ActionResult AlterarSenha()
        {
            return View(new Usuario());
        }

        [HttpPost]
        public ActionResult AlterarSenha([Bind(Include = "Senha")] Usuario model)
        {
            if (Session["usuarioLogado"] == null)
            {
                return RedirectToAction("Logar", "Login");
            }

            var usuario = _ctx.Usuario.Find(int.Parse(Session["usuarioLogado"].ToString()));
            usuario.Senha = Criptografia.Encrypt(model.Senha);

            _ctx.Entry(usuario).State = EntityState.Modified;
            _ctx.SaveChanges();
            TempData["msgSucesso"] = "Senha Alterada com Sucesso!";
            return RedirectToAction("Inicio", "Home");
        }

        [HttpPost]
        public ActionResult Novo(Usuario model)
        {
            try
            {
                //Valida se os dados estao corretos
                if (ModelState.IsValid)
                {
                    model.Nome = Criptografia.Encrypt(model.Nome.ToUpper());
                    model.Login = Criptografia.Encrypt(model.Login.ToUpper());
                    model.Senha = Criptografia.Encrypt(model.Senha);
                    //Adiciona no banco
                    _ctx.Usuario.Add(model);
                    //Salva
                    _ctx.SaveChanges();

                    var amigoSecreto = _ctx.AmigoSecreto.First();

                    Participantes participante = new Participantes
                    {
                        UsuarioId = model.Id,
                        AmigoSecretoId = amigoSecreto.Id,
                        AmigoSecreto = amigoSecreto,
                        Sorteado = "N"
                    };

                    //insert into Participantes (UsuarioId,AmigoSecretoId,Sorteado) values (

                    _ctx.Participantes.Add(participante);
                    _ctx.SaveChanges();

                    Session.Add("usuarioLogado", model.Id);
                    return RedirectToAction("Inicio", "Home");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["MensagemRetorno"] = "Erro: " + ex.Message;

            }
            return View();
        }

        public JsonResult ValidaUsuario(string user)
        {
            try
            {
                var userencruipt = Criptografia.Encrypt(user.ToUpper());
                var usuario = _ctx.Usuario.Where(e => e.Login == userencruipt).FirstOrDefault();
                if (usuario != null)
                {
                    return Json("Usuário ja cadastrado");
                }
                else
                {
                    return Json("true");
                }
            }
            catch (Exception ex)
            {
                Auxiliares.GravaLogErro(ex);
                throw;
            }
        }

        public ActionResult Editar()
        {
            if (Session["usuarioLogado"] == null)
            {
                return RedirectToAction("Logar", "Login");
            }

            var usuario = _ctx.Usuario.Find(int.Parse(Session["usuarioLogado"].ToString()));
            usuario.Nome = Criptografia.Decript(usuario.Nome);
            //  var user = _ctx.Usuario.Find(id);
            ViewBag.Genero = SelectListItemHelper.Genero();
            return View(usuario);
        }

        [HttpPost]
        public ActionResult Editar(Usuario model)
        {
            try
            {
                model.Nome = Criptografia.Encrypt(model.Nome);
                _ctx.Entry(model).State = EntityState.Modified;
                _ctx.SaveChanges();
                TempData["msgSucesso"] = "Dados Alterados com Sucesso!";
                return RedirectToAction("Inicio", "Home");
            }
            catch (Exception ex)
            {
                TempData["msgErro"] = "Erro ao alterar dados! Erro: " + ex.Message;
                return View();
            }

        }

        public ActionResult Excluir()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Excluir(Usuario model)
        {
            return View();
        }
    }
}