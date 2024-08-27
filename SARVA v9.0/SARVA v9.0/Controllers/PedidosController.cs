using Newtonsoft.Json;
using SARVA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SARVA.Controllers
{
    public class PedidosController : Controller
    {
        // GET: Pedidos

        connSARVA db = new connSARVA();
        public ActionResult LerDadosPedido()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            int id = Convert.ToInt32(Session["idSS"]);
            var listOfData = db.Pedido.ToList().Where(x => x.id_usuario == id).OrderBy(x => x.id);
            return View(listOfData);
        }

        [HttpPost]
        public ActionResult LerDadosPedido(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            int idUsuario = Convert.ToInt32(Session["idSS"]);

            if (id == 1)
            {
                DateTime inicio = Convert.ToDateTime(Request["dataInicio"]);
                DateTime fim = Convert.ToDateTime(Request["dataFim"]);

                var listOfData = db.Pedido.ToList().Where(x => x.id_usuario == idUsuario && x.data_pedido >= inicio && x.data_pedido <= fim).OrderBy(x => x.id);

                
                return View(listOfData);
            }
            else
            {
                string razaoSocial = Convert.ToString(Request["filtroEmpresa"]);

                var listOfData = db.Pedido.ToList().Where(x => x.id_usuario == idUsuario && x.Empresa.razao_social == razaoSocial).OrderBy(x => x.id);

                return View(listOfData);
            }
        }

        [HttpPost]
        public ActionResult AdicionarPedido(Empresa model)
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            int idUsuario = Convert.ToInt32(Session["idSS"]);
            Empresa e = db.Empresa.Where(x => x.razao_social == model.razao_social).FirstOrDefault();


            Pedido pedido = new Pedido();

            pedido.id_usuario = idUsuario;
            pedido.id_empresa = e.id;
            pedido.data_pedido = now;
            pedido.data_vencimento = now.AddDays(21); // 21 dias
            db.Pedido.Add(pedido);
            db.SaveChanges();

            return RedirectToAction("CatalogoItensVenda", "Item_Venda", new { idPedido = pedido.id, idEmpresa = pedido.id_empresa });
        }

        [HttpGet]
        public ActionResult EditarPedido(int idPedido)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            int id = Convert.ToInt32(Session["idSS"]);
            var pedido = db.Pedido.Where(x => x.id == idPedido && x.id_usuario == id).FirstOrDefault();
            return View(pedido);
        }

        [HttpPost]
        public ActionResult EditarPedido(Pedido model)
        {
            if (!ModelState.IsValid) // Verifica se os campos do formulário foram preenchidos
            {
                return View();
            }

            Pedido pedido = db.Pedido.Where(x => x.id == model.id).FirstOrDefault();
            if (pedido != null)
            {
                pedido.id = model.id;
                pedido.id_usuario = model.id_usuario;
                pedido.id_empresa = model.id_empresa;
                pedido.valor = model.valor;
                pedido.data_pedido = model.data_pedido;
                pedido.data_vencimento = model.data_vencimento;
                pedido.Item_Pedido = model.Item_Pedido;

                db.SaveChanges();
            }
            return RedirectToAction("LerDadosPedido", "Pedidos");
        }

        [HttpGet]
        public ActionResult FinalizarPedido(int idPedido)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            int id = Convert.ToInt32(Session["idSS"]);

            var pedido = db.Pedido.Where(x => x.id == idPedido).FirstOrDefault();
            var listIP = pedido.Item_Pedido.ToList().OrderBy(x => x.id_venda_IV);

            ViewBag.Pedido = pedido;
            return View(listIP);
        }

        public ActionResult FinalizarPedidoPost(int idPedido, decimal valorTotal)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            int idUsuario = Convert.ToInt32(Session["idSS"]);

            Pedido pedido = db.Pedido.Where(x => x.id == idPedido).FirstOrDefault();
            pedido.valor = 0.7M * valorTotal;
            db.SaveChanges();

            return RedirectToAction("LerDadosPedido", "Pedidos");
        }

        public ActionResult DetalhesPedido(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            var listaItensPedido = db.Item_Pedido.Where(x => x.id_pedido == id).ToList();

            ViewBag.IdPedido = id;

            var pedido = db.Pedido.Where(x => x.id == id).FirstOrDefault();
            ViewBag.Pedido = pedido;

            return View(listaItensPedido);
        }


        public ActionResult RemoverPedido(int id, bool confirm)
        {
            if (confirm)
            {
                var pedido = db.Pedido.Where(x => x.id == id).FirstOrDefault();
                var itensPedido = pedido.Item_Pedido.ToList();
                foreach (var item in itensPedido)
                {
                    db.Item_Pedido.Remove(item);
                }
                db.Pedido.Remove(pedido);
                db.SaveChanges();
                return RedirectToAction("LerDadosPedido", new { id = Session["idSS"] });
            }
            else
            {
                return RedirectToAction("LerDadosPedido", new { id = Session["idSS"] });
            }
        }
    }
}