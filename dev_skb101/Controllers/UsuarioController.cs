using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using dev_skb101.Models;

namespace dev_skb101.Controllers
{
    public class UsuarioController : Controller
    {
        private sbk101dbEntities db = new sbk101dbEntities();

        // GET: Usuario
        public ActionResult Index()
        {
            foreach (var item in db.usuario.ToList())
            {
                if (item.admin == true)
                {
                    item.adminSaida = "Administrador";
                }
                if (item.admin == false)
                {
                    item.adminSaida = "Usuário Comum";
                }
            }
            return View(db.usuario.ToList());
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            } else
            {
                if (usuario.admin == true)
                {
                    usuario.adminSaida = "Administrador";
                }
                if (usuario.admin == false)
                {
                    usuario.adminSaida = "Usuário Comum";
                }
                return View(usuario);
            }
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nome,login,senha,admin")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.senha = Crypt.Hash(usuario.senha);
                db.usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nome,login,senha,admin")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.senha = Crypt.Hash(usuario.senha);
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (usuario.admin == true)
                {
                    usuario.adminSaida = "Administrador";
                }
                if (usuario.admin == false)
                {
                    usuario.adminSaida = "Usuário Comum";
                }
                return View(usuario);
            }
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            usuario usuario = db.usuario.Find(id);
            db.usuario.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
