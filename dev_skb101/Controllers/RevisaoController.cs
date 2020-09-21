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
    public class RevisaoController : Controller
    {
        private sbk101dbEntities db = new sbk101dbEntities();

        // GET: Revisao
        public ActionResult Index()
        {
            var revisao = db.revisao.Include(r => r.bicicleta);
            return View(revisao.ToList());
        }

        // GET: Revisao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            revisao revisao = db.revisao.Find(id);
            if (revisao == null)
            {
                return HttpNotFound();
            }
            return View(revisao);
        }

        // GET: Revisao/Create
        public ActionResult Create()
        {
            ViewBag.bicicleta_id = new SelectList(db.bicicleta, "id", "nome");
            return View();
        }

        // POST: Revisao/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,bicicleta_id,autor,dataRevisao,descricao,observacao")] revisao revisao)
        {
            if (ModelState.IsValid)
            {
                db.revisao.Add(revisao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bicicleta_id = new SelectList(db.bicicleta, "id", "nome", revisao.bicicleta_id);
            return View(revisao);
        }

        // GET: Revisao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            revisao revisao = db.revisao.Find(id);
            if (revisao == null)
            {
                return HttpNotFound();
            }
            ViewBag.bicicleta_id = new SelectList(db.bicicleta, "id", "nome", revisao.bicicleta_id);
            return View(revisao);
        }

        // POST: Revisao/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,bicicleta_id,autor,dataRevisao,descricao,observacao")] revisao revisao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(revisao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bicicleta_id = new SelectList(db.bicicleta, "id", "nome", revisao.bicicleta_id);
            return View(revisao);
        }

        // GET: Revisao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            revisao revisao = db.revisao.Find(id);
            if (revisao == null)
            {
                return HttpNotFound();
            }
            return View(revisao);
        }

        // POST: Revisao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            revisao revisao = db.revisao.Find(id);
            db.revisao.Remove(revisao);
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
