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
    public class BicicletaController : Controller
    {
        private sbk101dbEntities db = new sbk101dbEntities();

        // GET: Bicicleta
        public ActionResult Index()
        {
            List<bicicleta> listaBicicleta = new List<bicicleta>();
            listaBicicleta = db.bicicleta.ToList();
            foreach (var item in db.bicicleta.ToList())
            {
                if(item.Alugada == true)
                {
                    item.AlugadaString = "Disponível";
                }
                if(item.Alugada == false)
                {
                    item.AlugadaString = "Alugada";
                }
                if (item.ativa == true)
                {
                    item.ativaString = "Boas Condições";
                }
                if (item.ativa == false)
                {
                    item.ativaString = "Indisponível";
                }
            }
            return View(db.bicicleta.ToList());
        }

        // GET: Bicicleta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bicicleta bicicleta = db.bicicleta.Find(id);
            if (bicicleta == null)
            {
                return HttpNotFound();
            } else
            {
                if (bicicleta.Alugada == true)
                {
                    bicicleta.AlugadaString = "Disponível";
                }
                if (bicicleta.Alugada == false)
                {
                    bicicleta.AlugadaString = "Alugada";
                }
                if (bicicleta.ativa == true)
                {
                    bicicleta.ativaString = "Boas Condições";
                }
                if (bicicleta.ativa == false)
                {
                    bicicleta.ativaString = "Indisponível";
                }
                return View(bicicleta);
            }
        }

        // GET: Bicicleta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bicicleta/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nome,descricao,Alugada,ativa,vezesAlugada,codigo")] bicicleta bicicleta)
        {
            if (ModelState.IsValid)
            {
                bicicleta.vezesAlugada = 0;
                db.bicicleta.Add(bicicleta);
                db.SaveChanges();
                return RedirectToAction("index");
            }

            return View(bicicleta);
        }

        // GET: Bicicleta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bicicleta bicicleta = db.bicicleta.Find(id);
            if (bicicleta == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (bicicleta.Alugada == true)
                {
                    bicicleta.AlugadaString = "Disponível";
                }
                if (bicicleta.Alugada == false)
                {
                    bicicleta.AlugadaString = "Alugada";
                }
                if (bicicleta.ativa == true)
                {
                    bicicleta.ativaString = "Boas Condições";
                }
                if (bicicleta.ativa == false)
                {
                    bicicleta.ativaString = "Indisponível";
                }
                return View(bicicleta);
            }
        }

        // POST: Bicicleta/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nome,descricao,Alugada,ativa,vezesAlugada,codigo")] bicicleta bicicleta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bicicleta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bicicleta);
        }

        // GET: Bicicleta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bicicleta bicicleta = db.bicicleta.Find(id);
            if (bicicleta == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (bicicleta.Alugada == true)
                {
                    bicicleta.AlugadaString = "Disponível";
                }
                if (bicicleta.Alugada == false)
                {
                    bicicleta.AlugadaString = "Alugada";
                }
                if (bicicleta.ativa == true)
                {
                    bicicleta.ativaString = "Boas Condições";
                }
                if (bicicleta.ativa == false)
                {
                    bicicleta.ativaString = "Indisponível";
                }
                return View(bicicleta);
            }
        }

        // POST: Bicicleta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bicicleta bicicleta = db.bicicleta.Find(id);
            db.bicicleta.Remove(bicicleta);
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
