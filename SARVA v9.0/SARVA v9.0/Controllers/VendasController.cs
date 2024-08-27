using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using SARVA.Models;
using static System.Net.Mime.MediaTypeNames;

namespace SARVA.Controllers
{
    public class VendasController : Controller
    {
        connSARVA db = new connSARVA();
        public ActionResult LerDadosVenda()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login","Login");
            }
            int idUsuario = Convert.ToInt32(Session["idSS"]);

            if (Request["dataInicio"] != null || Request["dataFim"] != null)
            {
                DateTime inicio = Convert.ToDateTime(Request["dataInicio"]);
                DateTime fim = Convert.ToDateTime(Request["dataFim"]);
                var listOfData = db.Venda.ToList().Where(x => x.id_usuario == idUsuario && x.data_venda >= inicio && x.data_venda <= fim).OrderBy(x => x.id);

                var listClientes = new List<Cliente>();

                foreach (var venda in listOfData)
                {
                    var cliente = db.Cliente.Where(x => x.id == venda.id_cliente).FirstOrDefault();
                    listClientes.Add(cliente);
                }
                foreach (var c in listClientes)
                {
                    var listaPagos = new List<Venda>();
                    foreach (var v in c.Venda)
                    {
                        if (Convert.ToDateTime(v.data_pagamento) <= Convert.ToDateTime(v.data_vencimento).AddDays(7))
                        {
                            listaPagos.Add(v);
                        }
                        // (listaPagos.Count / cliente.Venda.ToList().Count) >= 0.6
                        if (listaPagos.Count >= (c.Venda.Count() * 0.6))
                        {
                            c.scoreId = 1; // Bom Pagador
                            db.SaveChanges();
                        }

                        else if (listaPagos.Count < (c.Venda.Count() * 0.4))
                        {
                            c.scoreId = 2; // Mau Pagador
                            db.SaveChanges();
                        }
                        else
                        {
                            c.scoreId = 3; // Neutro
                            db.SaveChanges();
                        }
                    }
                }
                return View(listOfData);
            }
            else if (Request["filtroCliente"] != null)
            {
                string nomeCliente = Convert.ToString(Request["filtroCliente"]);

                var listOfData = db.Venda.ToList().Where(x => x.id_usuario == idUsuario && x.Cliente.nome == nomeCliente).OrderBy(x => x.id);

                var listClientes = new List<Cliente>();

                foreach (var venda in listOfData)
                {
                    var cliente = db.Cliente.Where(x => x.id == venda.id_cliente).FirstOrDefault();
                    listClientes.Add(cliente);
                }
                foreach (var c in listClientes)
                {
                    var listaPagos = new List<Venda>();
                    foreach (var v in c.Venda)
                    {
                        if (Convert.ToDateTime(v.data_pagamento) <= Convert.ToDateTime(v.data_vencimento).AddDays(7))
                        {
                            listaPagos.Add(v);
                        }
                        // (listaPagos.Count / cliente.Venda.ToList().Count) >= 0.6
                        if (listaPagos.Count >= (c.Venda.Count() * 0.6))
                        {
                            c.scoreId = 1; // Bom Pagador
                            db.SaveChanges();
                        }

                        else if (listaPagos.Count < (c.Venda.Count() * 0.4))
                        {
                            c.scoreId = 2; // Mau Pagador
                            db.SaveChanges();
                        }
                        else
                        {
                            c.scoreId = 3; // Neutro
                            db.SaveChanges();
                        }
                    }
                }
                return View(listOfData);
            }
            else
            {
                var listOfData = db.Venda.ToList().Where(x => x.id_usuario == idUsuario).OrderBy(x => x.id);

                var listClientes = new List<Cliente>();

                foreach (var venda in listOfData)
                {
                    var cliente = db.Cliente.Where(x => x.id == venda.id_cliente).FirstOrDefault();
                    listClientes.Add(cliente);
                }
                foreach (var c in listClientes)
                {
                    var listaPagos = new List<Venda>();
                    foreach (var v in c.Venda)
                    {
                        if (Convert.ToDateTime(v.data_pagamento) <= Convert.ToDateTime(v.data_vencimento).AddDays(7))
                        {
                            listaPagos.Add(v);
                        }
                        // (listaPagos.Count / cliente.Venda.ToList().Count) >= 0.6
                        if (listaPagos.Count >= (c.Venda.Count() * 0.6))
                        {
                            c.scoreId = 1; // Bom Pagador
                            db.SaveChanges();
                        }

                        else if (listaPagos.Count < (c.Venda.Count() * 0.4))
                        {
                            c.scoreId = 2; // Mau Pagador
                            db.SaveChanges();
                        }
                        else
                        {
                            c.scoreId = 3; // Neutro
                            db.SaveChanges();
                        }
                    }
                }
                return View(listOfData);
            }
        }

        [HttpPost]
        public ActionResult AdicionarVenda(Empresa model, string nomeCliente)
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            int idUsuario = Convert.ToInt32(Session["idSS"]);

            Venda venda = new Venda();
            venda.id_usuario = idUsuario;

            Cliente c = db.Cliente.Where(x => x.nome == nomeCliente && x.id_usuario == idUsuario).FirstOrDefault();
            Empresa e = db.Empresa.Where(x => x.razao_social == model.razao_social).FirstOrDefault();

            venda.id_empresa = e.id;
            venda.id_cliente = c.id;
            venda.data_venda = DateTime.Now;
            venda.data_vencimento = endDate;
            db.Venda.Add(venda);
            db.SaveChanges();
            ViewBag.Message = "Data Insert Successfully";


            return RedirectToAction("CatalogoProdutos", "Produtos", new { idVenda = venda.id, idEmpresa = venda.id_empresa });
        }

        [HttpGet]
        public ActionResult EditarVenda(int idVenda)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            int id = Convert.ToInt32(Session["idSS"]);
            var venda = db.Venda.Where(x => x.id == idVenda && x.id_usuario == id).FirstOrDefault();
            return View(venda);
        }

        [HttpPost]
        public ActionResult EditarVenda(Venda model)
        {
            Venda venda = db.Venda.Where(x => x.id == model.id).FirstOrDefault();
            if (venda != null)
            {
                venda.Item_Venda = model.Item_Venda;
                venda.id = model.id;
                venda.Cliente.nome = model.Cliente.nome;
                venda.id_usuario = model.id_usuario;
                venda.valor = model.valor;
                venda.desconto = model.desconto;
                if (model.desconto > 0)
                {
                    venda.valorFinal = model.valor - model.desconto;
                }
                else
                {
                    venda.valorFinal = model.valor;
                }
                venda.data_venda = model.data_venda;
                venda.data_vencimento = model.data_vencimento;
                var dataPagamento = Convert.ToDateTime(Request["dataPagamento"]);
                venda.data_pagamento = dataPagamento;
                db.SaveChanges();
            }
            return RedirectToAction("LerDadosVenda", "Vendas");
        }

        [HttpGet]
        public ActionResult FinalizarVenda(int idVenda)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }

            int id = Convert.ToInt32(Session["idSS"]);

            var venda = db.Venda.Where(x => x.id == idVenda).FirstOrDefault();
            var listIV = venda.Item_Venda.ToList();

            ViewBag.Venda = venda;
            ViewBag.ScoreCliente = venda.Cliente.scoreId;

            return View(listIV);
        }

        [HttpPost]
        public ActionResult FinalizarVenda(int idVenda, int dif)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }

            int id = Convert.ToInt32(Session["idSS"]);
            var venda = db.Venda.Where(x => x.id == idVenda).FirstOrDefault();

            decimal desconto = Convert.ToDecimal(Request["descontoVenda"]);
            venda.desconto = desconto;
            db.SaveChanges();

            var listIV = venda.Item_Venda.ToList();

            ViewBag.Venda = venda;
            ViewBag.ScoreCliente = venda.Cliente.scoreId;

            return View(listIV);
        }

        public ActionResult FinalizarVendaPost(int idVenda, decimal valorTotal)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }

            int idUsuario = Convert.ToInt32(Session["idSS"]);

            Venda venda = db.Venda.Where(x => x.id == idVenda).FirstOrDefault();
            venda.valor = valorTotal;
            if (venda.desconto > 0)
            {
                venda.valorFinal = valorTotal - venda.desconto;
            }
            else
            {
                venda.valorFinal = valorTotal;
            }
            db.SaveChanges();

            return RedirectToAction("LerDadosVenda", "Vendas");
        }

        public ActionResult DetalhesVenda(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            var listaItemVenda = db.Item_Venda.Where(x => x.id_venda == id).ToList();
            return View(listaItemVenda);
        }


        public ActionResult RemoverVenda(int id)
        {
            var venda = db.Venda.Where(x => x.id == id).FirstOrDefault();
            var itensPedido = db.Item_Pedido.ToList().Where(x => x.id_venda_IV == id);
            foreach (var item in itensPedido)
            {
                var pedido = db.Pedido.Where(x => x.id == item.id_pedido).FirstOrDefault();
                pedido.valor -= 0.7M * (item.valor * item.Item_Venda.quantidade);
                db.Item_Pedido.Remove(item);
            }
            

            var itensVenda = venda.Item_Venda.ToList();
            foreach (var item in itensVenda)
            {
                db.Item_Venda.Remove(item);
            }
            db.Venda.Remove(venda);
            db.SaveChanges();
            ViewBag.Message = "Record Delete Successfully";
            return RedirectToAction("LerDadosVenda", "Vendas");
        }
    }
}