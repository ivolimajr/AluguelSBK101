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
    public class BicicletaController : Controller
    {
        private sbk101dbEntities db = new sbk101dbEntities();

        // GET: Bicicleta
        public ActionResult Index(int? pagina)
        {
            var listaBanco = db.bicicleta.SqlQuery("SELECT * FROM bicicleta WHERE vendida != ' 1 ' ORDER BY codigo").ToList();

            int tamanhoPagina = 20;
            int numeroPagina = pagina ?? 1;

            return View(listaBanco.ToPagedList(numeroPagina, tamanhoPagina));
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
                bicicleta.vendida = 0;
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
            bicicleta.vendida = 1;
            bicicleta.codigo = 0;
            db.Entry(bicicleta).State = EntityState.Modified;
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
