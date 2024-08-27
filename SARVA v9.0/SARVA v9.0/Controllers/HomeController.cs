using SARVA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SARVA.Controllers
{
    public class HomeController : Controller
    {
        connSARVA db = new connSARVA();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Relatorio()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.Gerou = false;
            ViewBag.VendasRealizadas = null;
            ViewBag.VendasPagas = null;
            ViewBag.ValorObtido = null;
            ViewBag.ValorASerPago = null;
            ViewBag.Lucro = null;
            ViewBag.Empresa = null;
            return View();
        }

        [HttpPost]
        public ActionResult Relatorio(bool gerou)
        {
            ViewBag.Gerou = gerou;

            string razaoSocial = Convert.ToString(Request["razaoSocial"]);

            var empresa = db.Empresa.Where(x => x.razao_social == razaoSocial).FirstOrDefault();

            int idUsuario = Convert.ToInt32(Session["idSS"]);

            var usuario = db.Usuario.Where(x => x.id == idUsuario).FirstOrDefault();
            var totalVendas = usuario.Venda.ToList().Where(x => x.id_empresa == empresa.id);
            var totalPedidos = usuario.Pedido.ToList().Where(x => x.id_empresa == empresa.id);

            var vendasPagas = totalVendas.Where(x => x.data_pagamento != null).ToList();

            decimal valorObtido = 0;
            decimal valorASerPago = 0;
            decimal lucro = 0;

            foreach (var venda in vendasPagas)
            {
                valorObtido += Convert.ToDecimal(venda.valorFinal);
            }
            
            foreach (var pedido in totalPedidos)
            {
                valorASerPago += Convert.ToDecimal(pedido.valor);
            }

            lucro = valorObtido - valorASerPago;

            ViewBag.VendasRealizadas = totalVendas;
            ViewBag.VendasPagas = vendasPagas;
            ViewBag.ValorObtido = valorObtido;
            ViewBag.ValorASerPago = valorASerPago;
            ViewBag.Lucro = lucro;
            ViewBag.Empresa = razaoSocial;

            return View();
        }
    }
}