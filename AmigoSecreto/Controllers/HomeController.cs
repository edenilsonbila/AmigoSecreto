using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmigoSecreto.Models;
using AmigoSecreto = AmigoSecreto.Models.AmigoSecreto;

namespace AmigoSecreto.Controllers
{
    public class HomeController : Controller
    {
        protected Context _ctx;

        public HomeController()
        {
            _ctx = new Context();
        }

        public ActionResult Index()
        {
            return RedirectToAction("Logar", "Login");
        }

        public ActionResult Sair()
        {
            Session.RemoveAll();
            return RedirectToAction("Logar","Login");
        }

        public ActionResult Inicio()
        {
            if (Session["usuarioLogado"] == null)
            {
                return RedirectToAction("Logar", "Login");
            }

            var usuario = _ctx.Usuario.Find(int.Parse(Session["usuarioLogado"].ToString()));

            var amigosSecretosparticipa = _ctx.AmigoSecreto; //.Where(e => e.UsuarioId == usuario.Id);

            var participa = new List<Models.AmigoSecreto>();//Participa
            var naoparticipa = new List<Models.AmigoSecreto>();
            var jasorteou = new Models.Participantes();

            foreach (var item in amigosSecretosparticipa)
            {
                if (_ctx.Participantes.FirstOrDefault(e => e.UsuarioId == usuario.Id && e.AmigoSecretoId == item.Id) != null) //Participa
                {
                    //Valida se ja sorteou
                    var validaPessoaTirada =
                        _ctx.Participantes.FirstOrDefault(
                            e => e.UsuarioId == usuario.Id && e.AmigoSecretoId == item.Id && e.PessoaTiradaId != null);
                    if (validaPessoaTirada != null)
                    {
                        ViewBag.JaSorteou = "Sim";
                        ViewBag.PessoaSorteada = validaPessoaTirada.PessoaTirada;
                        participa.Add(item);
                    }
                    else
                    {
                        participa.Add(item);
                    }
                }
                else //Nao Participa Disponivel
                {
                    naoparticipa.Add(item);
                }
            }

            ViewBag.AmigosSecretosParticipa = participa;
            ViewBag.AmigosSecretos = naoparticipa;
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}