﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dev_skb101.Models;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Configuration;
using System.Data.SqlClient;

namespace dev_skb101.Controllers
{
    public class RelatorioController : Controller
    {
        private sbk101dbEntities db = new sbk101dbEntities();
        // GET: Relatorio
        public ActionResult Index()
        {
            return View();
        }
        // GET: Relatorio
        public ActionResult Bicicleta()
        {
            List<bicicleta> listaBicicleta = new List<bicicleta>();
            listaBicicleta = db.bicicleta.ToList();
            foreach (var item in db.bicicleta.ToList())
            {
                if (item.Alugada == true)
                {
                    item.AlugadaString = "Disponível";
                }
                if (item.Alugada == false)
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
        // GET: Relatorio
        public ActionResult Aluguel()
        {
            return View();
        }
        [HttpPost,]
        public ActionResult AluguelRel([Bind(Include = "dataEntrada,dataSaida")] aluguel aluguel)
        {   

            var listaBanco = db.aluguel.SqlQuery("SELECT * FROM aluguel WHERE dataSaida BETWEEN ' "+ aluguel.dataEntrada + " ' AND ' "+ aluguel.dataSaida + " ' ORDER BY dataSaida").ToList();

            return View(listaBanco.ToList());
        }
        // GET: Relatorio
        public ActionResult AluguelRel()
        {
                return View();
        }

        // GET: Relatorio
        public ActionResult Revisao()
        {
            return View();
        }
        public ActionResult revisaoRel()
        {
            return View();
        }
        [HttpPost,]
        public ActionResult revisaoRel([Bind(Include = "dataRevisao,dataFim")] revisao revisao)
        {

            var listaBanco = db.revisao.SqlQuery("SELECT * FROM revisao WHERE dataRevisao BETWEEN ' " + revisao.dataRevisao + " ' AND ' " + revisao.dataFim + " ' ORDER BY dataRevisao").ToList();

            return View(listaBanco.ToList());
        }
    }
}