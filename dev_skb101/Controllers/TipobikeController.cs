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
    public class TipobikeController : Controller
    {
        private sbk101dbEntities db = new sbk101dbEntities();

        // GET: Tipobike
        public ActionResult Index()
        {
            return View(db.tipobike.ToList());
        }

        // GET: Tipobike/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipobike tipobike = db.tipobike.Find(id);
            if (tipobike == null)
            {
                return HttpNotFound();
            }
            return View(tipobike);
        }

        // GET: Tipobike/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tipobike/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,valorHora,valorDiaria,descicao")] tipobike tipobike)
        {
            if (ModelState.IsValid)
            {
                db.tipobike.Add(tipobike);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipobike);
        }

        // GET: Tipobike/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipobike tipobike = db.tipobike.Find(id);
            if (tipobike == null)
            {
                return HttpNotFound();
            }
            return View(tipobike);
        }

        // POST: Tipobike/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,valorHora,valorDiaria,descicao")] tipobike tipobike)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipobike).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipobike);
        }

        // GET: Tipobike/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipobike tipobike = db.tipobike.Find(id);
            if (tipobike == null)
            {
                return HttpNotFound();
            }
            return View(tipobike);
        }

        // POST: Tipobike/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tipobike tipobike = db.tipobike.Find(id);
            db.tipobike.Remove(tipobike);
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
