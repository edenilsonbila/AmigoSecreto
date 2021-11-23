using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AmigoSecreto.Models;
using AmigoSecretoSystem.Helpers;

namespace AmigoSecretoSystem.Controllers
{
    public class ParticipanteController : Controller
    {
        protected Context _ctx;

        public ParticipanteController()
        {
            _ctx = new Context();
        }

        // GET: Participantes
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Sortear(int amigoSecretoId)
        {
            try
            {
               
                if (Session["usuarioLogado"] == null)
                {
                    Response.Redirect("~/Login/Logar");
                }

                var usuario = _ctx.Usuario.Find(int.Parse(Session["usuarioLogado"].ToString()));

                Random rd = new Random();

                //Prioriza os Nulos
                var participantesNulos = _ctx.Participantes.Where(
                        e => e.AmigoSecretoId == amigoSecretoId && e.Sorteado == "N" && e.UsuarioId != usuario.Id && e.PessoaTiradaId == null && e.QuemSorteouId == null)
                        .ToList();

                var pessoaSorteada = new Participantes();
                var usuarioSorteante =
                       _ctx.Participantes
                           .FirstOrDefault(e => e.UsuarioId == usuario.Id && e.AmigoSecretoId == amigoSecretoId);

                //Sorteia pelos nulos
                if (participantesNulos.Any())
                {

                    var numeroSorteado = rd.Next(0, participantesNulos.Count());
                    pessoaSorteada = participantesNulos.ElementAtOrDefault(numeroSorteado);
                }
                else//Sorteia pela lista geral
                {
                    var participantes =
                  _ctx.Participantes.Where(
                      e => e.AmigoSecretoId == amigoSecretoId && e.Sorteado == "N" && e.UsuarioId != usuario.Id)
                      .ToList();

                    var numeroSorteado = rd.Next(0, participantes.Count());
                    pessoaSorteada = participantes.ElementAtOrDefault(numeroSorteado);

                }


                if (pessoaSorteada != null)
                {

                    //Atualiza o usuario com a pessoa q tirou
                    usuarioSorteante.PessoaTiradaId = pessoaSorteada.UsuarioId;
                    _ctx.Entry(usuarioSorteante).State = EntityState.Modified;
                    _ctx.SaveChanges();

                    //Atualiza a pessoa tirada marcando Sorteado='S' e o campo quem Sorteou
                    pessoaSorteada.Sorteado = "S";
                    pessoaSorteada.QuemSorteouId = usuarioSorteante.UsuarioId;
                    _ctx.Entry(pessoaSorteada).State = EntityState.Modified;

                    _ctx.SaveChanges();
                }

                var usuarioRetorno = _ctx.Usuario.Find(pessoaSorteada.UsuarioId);
                usuarioRetorno.Nome = Criptografia.Decript(usuarioRetorno.Nome);

                return Json(usuarioRetorno, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        public JsonResult DadosUsuario(int usuarioId)
        {
            try
            {
                var user = _ctx.Usuario.Find(usuarioId);
                user.Nome = Criptografia.Decript(user.Nome);
                return Json(user, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        public JsonResult Participar(int amigoSecretoId)
        {
            try
            {
            
            if (Session["usuarioLogado"] == null)
            {
                Response.Redirect("~/Login/Logar");
            }

            //Vincula a porra com o carai
            var usuarioLogado = _ctx.Usuario.Find(int.Parse(Session["usuarioLogado"].ToString()));
                var amigoSecreto = _ctx.AmigoSecreto.Find(amigoSecretoId);

                Participantes participante = new Participantes
                {
                    UsuarioId = usuarioLogado.Id,
                    AmigoSecretoId = amigoSecreto.Id,
                    AmigoSecreto = amigoSecreto,
                    Sorteado = "N"
                };

                _ctx.Participantes.Add(participante);
                _ctx.SaveChanges();

            return Json("true", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
                throw;
            }
        }
    }
}