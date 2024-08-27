using SARVA.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SARVA.Controllers
{
    public class ClientesController : Controller
    {
        connSARVA db = new connSARVA();
        public ActionResult BuscarCliente()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        [HttpPost]
        public JsonResult GetSearchValue(string search)
        {
            int idUsuario = Convert.ToInt32(Session["idSS"]);
            var clientes = db.Cliente.Select(x => new
            {
                nomeCliente = x.nome,
                id_Usuario = x.id_usuario,
            }).Where(x => x.nomeCliente.Contains(search) && x.id_Usuario == idUsuario);

            return Json(clientes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LerDadosCliente()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }

            int id = Convert.ToInt32(Session["idSS"]);

            if (Request["nomeCliente"] != null)
            {
                string nomeCliente = Convert.ToString(Request["nomeCliente"]);
                Cliente c = db.Cliente.Where(x => x.nome == nomeCliente && x.id_usuario == id).FirstOrDefault();
                var listOfData = db.Cliente.Where(x => x.id_usuario == id && x.id == c.id).ToList().OrderBy(x => x.nome);

                foreach (var cliente in listOfData)
                {
                    var listaPagos = new List<Venda>();
                    var vendas = cliente.Venda.ToList();
                    if (vendas.Count > 0)
                    {

                        foreach (var venda in vendas)
                        {
                            if (Convert.ToDateTime(venda.data_pagamento) <= Convert.ToDateTime(venda.data_vencimento).AddDays(7))
                            {
                                listaPagos.Add(venda);
                            }
                        }
                        // (listaPagos.Count / cliente.Venda.ToList().Count) >= 0.6
                        if (listaPagos.Count >= (cliente.Venda.ToList().Count * 0.6))
                        {
                            cliente.scoreId = 1; // Bom Pagador
                            db.SaveChanges();
                        }

                        else if (listaPagos.Count < (cliente.Venda.ToList().Count * 0.4))
                        {
                            cliente.scoreId = 2; // Mau Pagador
                            db.SaveChanges();
                        }
                        else
                        {
                            cliente.scoreId = 3; // Neutro
                            db.SaveChanges();
                        }
                    }
                    else if (vendas.Count == 0)
                    {
                        cliente.scoreId = 3;
                        db.SaveChanges();
                    }
                    db.SaveChanges();
                }
                db.SaveChanges();
                return View(listOfData);
            }
            else if (Request["filtroNiver"] != null)
            {
                DateTime filtroNiver = Convert.ToDateTime(Request["filtroNiver"]);
                Cliente c = db.Cliente.Where(x => x.aniversario == filtroNiver && x.id_usuario == id).FirstOrDefault();
                var listOfData = db.Cliente.Where(x => x.id_usuario == id && x.id == c.id).ToList().OrderBy(x => x.nome);

                foreach (var cliente in listOfData)
                {
                    var listaPagos = new List<Venda>();
                    var vendas = cliente.Venda.ToList();
                    if (vendas.Count > 0)
                    {

                        foreach (var venda in vendas)
                        {
                            if (Convert.ToDateTime(venda.data_pagamento) <= Convert.ToDateTime(venda.data_vencimento).AddDays(7))
                            {
                                listaPagos.Add(venda);
                            }
                        }
                        // (listaPagos.Count / cliente.Venda.ToList().Count) >= 0.6
                        if (listaPagos.Count >= (cliente.Venda.ToList().Count * 0.6))
                        {
                            cliente.scoreId = 1; // Bom Pagador
                            db.SaveChanges();
                        }

                        else if (listaPagos.Count < (cliente.Venda.ToList().Count * 0.4))
                        {
                            cliente.scoreId = 2; // Mau Pagador
                            db.SaveChanges();
                        }
                        else
                        {
                            cliente.scoreId = 3; // Neutro
                            db.SaveChanges();
                        }
                    }
                    else if (vendas.Count == 0)
                    {
                        cliente.scoreId = 3;
                        db.SaveChanges();
                    }
                    db.SaveChanges();
                }
                db.SaveChanges();
                return View(listOfData);
            }
            else
            {
                var listOfData = db.Cliente.Where(x => x.id_usuario == id).ToList().OrderBy(x => x.nome);

                foreach (var cliente in listOfData)
                {
                    var listaPagos = new List<Venda>();
                    var vendas = cliente.Venda.ToList();
                    if (vendas.Count > 0)
                    {

                        foreach (var venda in vendas)
                        {
                            if (Convert.ToDateTime(venda.data_pagamento) <= Convert.ToDateTime(venda.data_vencimento).AddDays(7))
                            {
                                listaPagos.Add(venda);
                            }
                        }
                        // (listaPagos.Count / cliente.Venda.ToList().Count) >= 0.6
                        if (listaPagos.Count >= (cliente.Venda.ToList().Count * 0.6))
                        {
                            cliente.scoreId = 1; // Bom Pagador
                            db.SaveChanges();
                        }

                        else if (listaPagos.Count < (cliente.Venda.ToList().Count * 0.4))
                        {
                            cliente.scoreId = 2; // Mau Pagador
                            db.SaveChanges();
                        }
                        else
                        {
                            cliente.scoreId = 3; // Neutro
                            db.SaveChanges();
                        }
                    }
                    else if (vendas.Count == 0)
                    {
                        cliente.scoreId = 3;
                        db.SaveChanges();
                    }
                    db.SaveChanges();
                }
                db.SaveChanges();
                return View(listOfData);
            }
        }

        [HttpGet]
        public ActionResult AdicionarCliente()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AdicionarCliente(Cliente model)
        {
            if (!ModelState.IsValid) // Verifica se os campos do formulário foram preenchidos
            {
                return View();
            }
            db.Cliente.Add(model);
            db.SaveChanges();
            ViewBag.Message = "Data Insert Successfully";
            return RedirectToAction("LerDadosCliente", new { id = Session["idSS"] });
        }

        [HttpGet]
        public ActionResult EditarCliente(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            var data = db.Cliente.Where(x => x.id == id).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult EditarCliente(Cliente Model)
        {
            if (!ModelState.IsValid) // Verifica se os campos do formulário foram preenchidos
            {
                return View();
            }
            var data = db.Cliente.Where(x => x.id == Model.id).FirstOrDefault();
            if (data != null)
            {
                data.id = Model.id;
                data.id_usuario = Model.id_usuario;
                data.nome = Model.nome;
                data.email = Model.email;
                data.aniversario = Model.aniversario;
                data.Score.descricao = Model.Score.descricao;
                db.SaveChanges();
            }
            return RedirectToAction("LerDadosCliente", "Clientes");
        }

        public ActionResult RemoverCliente(int id, bool confirm)
        {
            if (confirm)
            {
                var cliente = db.Cliente.Where(x => x.id == id).FirstOrDefault();
                if (cliente.Venda.Count > 0)
                {
                    var list = db.Venda.Where(x => x.id_cliente == id).ToList();
                    foreach (var item in list)
                    {
                        var listIV = db.Item_Venda.Where(x => x.id_venda == item.id).ToList();
                        foreach (var itemIV in listIV)
                        {
                            db.Item_Venda.Remove(itemIV);
                            db.Venda.Remove(item);
                        }
                    }
                }
                db.Cliente.Remove(cliente);
                db.SaveChanges();
                ViewBag.Message = "Record Delete Successfully";
                return RedirectToAction("LerDadosCliente", new { id = Session["idSS"] });
            }
            else
            {
                return RedirectToAction("LerDadosCliente", new { id = Session["idSS"] });
            }
        }
    }
}