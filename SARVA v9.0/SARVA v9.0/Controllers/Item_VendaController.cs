using SARVA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SARVA.Controllers
{
    public class Item_VendaController : Controller
    {
        // GET: Item_Venda

        connSARVA db = new connSARVA();

        public ActionResult AdicionarItem_Venda(int codigoIV, int idCiclo, int idEmpresa, int idVenda, bool jaTemIV)
        {
            if (jaTemIV)
            {
                Item_Venda itemVenda = db.Item_Venda.Where(x => x.id_venda == idVenda && x.codigo_produto == codigoIV && x.id_ciclo_produto == idCiclo).FirstOrDefault();

                itemVenda.quantidade++;
                itemVenda.id_venda = idVenda;
                itemVenda.codigo_produto = codigoIV;
                itemVenda.valor = db.Produto.Where(x => x.codigo == codigoIV).FirstOrDefault().valor;
                db.SaveChanges();

                int qtd_input = Convert.ToInt32(db.Item_Venda.Where(x => x.id_venda == idVenda && x.codigo_produto == codigoIV).FirstOrDefault().quantidade);

                var listaIV = db.Item_Venda.Where(x => x.id_venda == idVenda).ToList();

                ViewBag.ListaIV = listaIV;

                return RedirectToAction("FinalizarVenda", "Vendas", new { idVenda = idVenda });
            }
            else
            {
                Item_Venda itemVenda = new Item_Venda();

                itemVenda.quantidade = 1;
                itemVenda.id_venda = idVenda;
                itemVenda.codigo_produto = codigoIV;
                itemVenda.id_ciclo_produto = idCiclo;
                itemVenda.valor = db.Produto.Where(x => x.codigo == codigoIV).FirstOrDefault().valor;
                db.Item_Venda.Add(itemVenda);

                db.SaveChanges();


                List<int> listaCodigos = new List<int>
                {
                    itemVenda.codigo_produto
                };

                return RedirectToAction("CatalogoProdutos", "Produtos", new { idVenda = idVenda, idEmpresa = idEmpresa });
            }
        }

        public ActionResult RemoverItem_Venda(int codigoIV, int idCiclo, int idVenda)
        {
            Item_Venda itemVenda = db.Item_Venda.Where(x => x.id_venda == idVenda && x.codigo_produto == codigoIV && x.id_ciclo_produto == idCiclo).FirstOrDefault();

            if (itemVenda.quantidade > 1)
            {
                itemVenda.quantidade--;
            }
            itemVenda.id_venda = idVenda;
            itemVenda.codigo_produto = codigoIV;
            itemVenda.valor = db.Produto.Where(x => x.codigo == codigoIV).FirstOrDefault().valor;

            db.SaveChanges();

            return RedirectToAction("FinalizarVenda", "Vendas", new { idVenda = idVenda });
        }


        public ActionResult CatalogoItensVenda(int idPedido, int idEmpresa)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            int idUsuario = Convert.ToInt32(Session["idSS"]);

            ViewBag.IdPedido = idPedido;
            ViewBag.IdEmpresa = idEmpresa;

            var listIP = db.Item_Pedido.ToList().Where(x => x.id_pedido == idPedido);
            var listIV = new List<Item_Venda>();

            foreach (var item in listIP)
            {
                Item_Venda iv = db.Item_Venda.Where(x => x.codigo_produto == item.codigo_IV && x.id_ciclo_produto == item.id_ciclo_IV && x.id_venda == item.id_venda_IV).FirstOrDefault();

                listIV.Add(iv);
            }

            ViewBag.ListIV = listIV;


            if (Request["numVenda"] != null)
            {
                int numVenda = Convert.ToInt32(Request["numVenda"]);
                //Venda v = db.Venda.Where(x => x.id == numVenda && x.id_usuario == idUsuario).FirstOrDefault();

                var listOfData = db.Item_Venda.ToList().Where(x => x.Venda.id_usuario == idUsuario && x.id_venda == numVenda).OrderBy(x => x.id_venda);
                return View(listOfData);
                
            }
            if (Request["nomeCliente"] != null)
            {
                string nomeCliente = Convert.ToString(Request["nomeCliente"]);
                Cliente c = db.Cliente.Where(x => x.nome == nomeCliente && x.id_usuario == idUsuario).FirstOrDefault();

                var listOfData = db.Item_Venda.ToList().Where(x => x.Venda.id_usuario == idUsuario && x.Venda.id_cliente == c.id).OrderBy(x => x.id_venda);
                return View(listOfData);
            }
            else
            {
                var listOfData = db.Item_Venda.ToList().Where(x => x.Venda.id_usuario == idUsuario).OrderBy(x => x.id_venda);
                return View(listOfData);
            }
        }
    }
}