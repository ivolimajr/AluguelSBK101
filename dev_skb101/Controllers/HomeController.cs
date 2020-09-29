using dev_skb101.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace dev_skb101.Controllers
{
    public class HomeController : Controller
    {
        private sbk101dbEntities db = new sbk101dbEntities();
        public ActionResult Index()
        {
            var aluguel = db.aluguel.Where(a => a.ativa == true).Include(a => a.bicicleta).Include(a => a.tipobike);
            return View(aluguel.ToList());
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autenticacao(usuario usuario)
        {
            using (sbk101dbEntities db = new sbk101dbEntities())
            {
                usuario.senha = Crypt.Hash(usuario.senha);
                var usuarioDetalhe = db.usuario.Where(x => x.login == usuario.login && x.senha == usuario.senha).FirstOrDefault();
                if (usuarioDetalhe == null)
                {
                    usuario.LoginErrorMessage = "Dados Inválidos";
                    return View("Login", usuario);
                }
                else
                {
                    Session["UserId"] = usuarioDetalhe.id;
                    Session["UserName"] = usuarioDetalhe.nome;
                    if(usuarioDetalhe.admin == true)
                    {
                        Session["UserAdmin"] = usuarioDetalhe.admin;
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Projeto da disciplina Topicos Avançados em S.I - UDF";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Logout()
        {
            int userId = (int)Session["UserId"];
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult DevolverBike(int id)
        {
            return RedirectToAction("Devolver", "Aluguel", new { id = id });
        }
    }
}