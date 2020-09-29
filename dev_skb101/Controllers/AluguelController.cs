using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using dev_skb101.Models;
using PagedList;

namespace dev_skb101.Controllers
{
    public class AluguelController : Controller
    {
        private sbk101dbEntities db = new sbk101dbEntities();

        // GET: Aluguel
        public ActionResult Index(int? pagina)
        {
            var aluguel = db.aluguel.Where(a => a.ativa == true || a.ativa == false).Include(a => a.bicicleta).Include(a => a.tipobike).OrderByDescending(a => a.dataEntrada).ToList();

            int tamanhoPagina = 20;
            int numeroPagina = pagina ?? 1;

            return View(aluguel.ToPagedList(numeroPagina, tamanhoPagina));
        }

        // GET: Aluguel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aluguel aluguel = db.aluguel.Find(id);
            if (aluguel == null)
            {
                return HttpNotFound();
            }
            return View(aluguel);
        }

        // GET: Aluguel/Create
        public ActionResult Create()
        {
            ViewBag.bicicleta_id = new SelectList(db.bicicleta.Where(m => m.Alugada == true && m.ativa == true), "id", "nome");
            //ViewBag.bicicleta_id = new SelectList(db.bicicleta, "id", "nome");
            ViewBag.tipobike_id = new SelectList(db.tipobike, "id", "descicao");
            return View();
        }

        // POST: Aluguel/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,tipobike_id,bicicleta_id,dataEntrada,dataSaida")] aluguel aluguel)
        {
            if (ModelState.IsValid)
            {

                BicicletaController dbBike = new BicicletaController();
                int idBike = aluguel.bicicleta_id;
                bicicleta bicicleta = new bicicleta();
                bicicleta = db.bicicleta.Find(idBike);
                bicicleta.Alugada = false;
                bicicleta.vezesAlugada = (bicicleta.vezesAlugada + 1);
                db.Entry(bicicleta).State = EntityState.Modified;

                aluguel.ativa = true;

                db.aluguel.Add(aluguel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bicicleta_id = new SelectList(db.bicicleta, "id", "nome", aluguel.bicicleta_id);
            ViewBag.tipobike_id = new SelectList(db.tipobike, "id", "descicao", aluguel.tipobike_id);
            return View(aluguel);
        }

        // GET: Aluguel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aluguel aluguel = db.aluguel.Find(id);
            if (aluguel == null)
            {
                return HttpNotFound();
            }
            ViewBag.bicicleta_id = new SelectList(db.bicicleta, "id", "nome", aluguel.bicicleta_id);
            ViewBag.tipobike_id = new SelectList(db.tipobike, "id", "descicao", aluguel.tipobike_id);
            return View(aluguel);
        }

        // POST: Aluguel/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,tipobike_id,bicicleta_id,dataEntrada,dataSaida")] aluguel aluguel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aluguel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bicicleta_id = new SelectList(db.bicicleta, "id", "nome", aluguel.bicicleta_id);
            ViewBag.tipobike_id = new SelectList(db.tipobike, "id", "descicao", aluguel.tipobike_id);
            return View(aluguel);
        }

        // GET: Aluguel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aluguel aluguel = db.aluguel.Find(id);
            if (aluguel == null)
            {
                return HttpNotFound();
            }
            return View(aluguel);
        }
        public ActionResult Devolver(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aluguel aluguel = db.aluguel.Find(id);
            if (aluguel == null)
            {
                return HttpNotFound();
            }
            return View(aluguel);
        }

        // POST: Aluguel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            aluguel aluguel = db.aluguel.Find(id);
            bicicleta bicicleta = db.bicicleta.Find(aluguel.bicicleta_id);

            bicicleta.Alugada = true;
            db.Entry(bicicleta).State = EntityState.Modified;
            db.aluguel.Remove(aluguel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Devolver")]
        [ValidateAntiForgeryToken]
        public ActionResult DevolverConfirmed(int id)
        {
            aluguel aluguel = db.aluguel.Find(id);
            bicicleta bicicleta = db.bicicleta.Find(aluguel.bicicleta_id);

            bicicleta.Alugada = true;
            db.Entry(bicicleta).State = EntityState.Modified;

            aluguel.ativa = false;
            db.Entry(aluguel).State = EntityState.Modified;
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
