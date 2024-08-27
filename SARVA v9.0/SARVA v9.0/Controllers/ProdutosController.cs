using SARVA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SARVA.Controllers
{
    public class ProdutosController : Controller
    {
        // GET: Produtos
        connSARVA db = new connSARVA();


        public ActionResult CatalogoProdutos(int idVenda, int idEmpresa)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.IdVenda = idVenda;
            ViewBag.IdEmpresa = idEmpresa;

            var listIV = db.Item_Venda.ToList().Where(x => x.id_venda == idVenda);

            List<int> listaCodigos = new List<int>();
            List<int> listaIdCiclo = new List<int>();
            

            foreach (var item in listIV)
            {
                listaCodigos.Add(item.codigo_produto);
                listaIdCiclo.Add(item.id_ciclo_produto);
            }

            ViewBag.CodigosIV = listaCodigos;
            ViewBag.IdCicloIV = listaIdCiclo;

            if (Request["filtroCodigo"] != null)
            {
                int codigo = Convert.ToInt32(Request["filtroCodigo"]);
                var listOfData = db.Produto.ToList().Where(x => x.codigo == codigo).OrderBy(x => x.nome);
                return View(listOfData);
            }

            else if (Request["filtroProduto"] != null)
            {
                string nomeProduto = Convert.ToString(Request["filtroProduto"]);
                var listOfData = db.Produto.ToList().Where(x => x.nome.Contains(nomeProduto)).OrderBy(x => x.nome);
                return View(listOfData);
            }
            else if (Request["filtroCiclo"] != null)
            {
                string nomeCiclo = Convert.ToString(Request["filtroCiclo"]);
                var listOfData = db.Produto.ToList().Where(x => x.Ciclo.nome.Contains(nomeCiclo)).OrderBy(x => x.nome);
                return View(listOfData);
            }
            else if (Request["razaoSocial"] != null)
            {
                string razaoSocial = Convert.ToString(Request["razaoSocial"]);
                var listOfData = db.Produto.ToList().Where(x => x.Empresa.razao_social.Contains(razaoSocial)).OrderBy(x => x.nome);
                return View(listOfData);
            }
            else
            {
                var listOfData = db.Produto.ToList().OrderBy(x => x.nome);
                return View(listOfData);
            }
        }

        [HttpGet]
        public ActionResult AdicionarProduto(int idEmpresa)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.IdEmpresa = idEmpresa;
            return View();
        }

        [HttpPost]
        public ActionResult AdicionarProduto(Produto model)
        {
            /*
             if (!ModelState.IsValid) // Verifica se os campos do formulário foram preenchidos
            {
                return View();
            }
             */

            var cicloNome = Convert.ToString(Request["cicloNome"]);
            var ciclo = db.Ciclo.Where(x => x.nome == cicloNome).FirstOrDefault();

            if (ciclo == null)
            {
                ViewBag.CicloErrorMessage = "Esse ciclo não existe";
                return View();
            }
            else if (db.Produto.Any(x => x.id_ciclo == model.id_ciclo)) // Verifica se o produto já existe no BD
            {
                ViewBag.JaExiste = "Esse produto já foi requisitado";
                return View();
            }
            Produto p = new Produto();
            p.nome = model.nome;
            p.codigo = model.codigo;
            p.id_empresa = model.id_empresa;
            p.id_usuario = model.id_usuario;
            p.valor = model.valor;
            p.id_ciclo = ciclo.id;
            p.flag = model.flag;
            p.pontos = model.pontos;
            db.Produto.Add(p);
            db.SaveChanges();
            return RedirectToAction("CatalogoProdutos", new { idVenda = -1, idEmpresa = -1 });
        }

        [HttpPost]
        public ActionResult AdicionarProdutoRequisitado(Empresa model)
        {
            int idUsuario = Convert.ToInt32(Session["idSS"]);
            Empresa e = db.Empresa.Where(x => x.razao_social == model.razao_social).FirstOrDefault();
            return RedirectToAction("AdicionarProduto", "Produtos", new { idEmpresa = e.id });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult EditarProduto(int codigo)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            var data = db.Produto.Where(x => x.codigo == codigo).FirstOrDefault();
            ViewBag.Codigo = codigo;
            return View(data);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditarProduto(Produto Model, int codigo)
        {
            if (!ModelState.IsValid) // Verifica se os campos do formulário foram preenchidos
            {
                return View();
            }
            
            var data = db.Produto.Where(x => x.codigo == codigo).FirstOrDefault();
            if (data != null)
            {
                data.codigo = Model.codigo;
                data.nome = Model.nome;
                data.valor = Model.valor;

                var cicloNome = Convert.ToString(Request["cicloNome"]);
                var ciclo = db.Ciclo.Where(x => x.nome == cicloNome).FirstOrDefault();

                data.id_ciclo = ciclo.id;

                data.pontos = Model.pontos;
                data.flag = Model.flag;
                data.id_usuario = Model.id_usuario;
                data.id_empresa = Model.id_empresa;
                db.SaveChanges();
            }
            return RedirectToAction("CatalogoProdutos", new { idVenda = -1, idEmpresa = -1 });
        }

        public ActionResult DetalhesProduto(int codigo)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.Code = codigo;
            var data = db.Produto.Where(x => x.codigo == codigo).FirstOrDefault();
            return View(data);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeletarProduto(int codigo, int idCiclo, bool confirm)
        {
            if (confirm)
            {
                var itensVenda = db.Item_Venda.ToList().Where(x => x.codigo_produto == codigo && x.id_ciclo_produto == idCiclo);

                var itensPedido = db.Item_Pedido.ToList().Where(x => x.codigo_IV == codigo && x.id_ciclo_IV == idCiclo);
                foreach (var item in itensPedido)
                {
                    var pedido = db.Pedido.Where(x => x.id == item.id_pedido).FirstOrDefault();
                    pedido.valor -= 0.7M * (item.valor * item.Item_Venda.quantidade);
                    db.Item_Pedido.Remove(item);
                }

                foreach (var item in itensVenda)
                {
                    var venda = db.Venda.Where(x => x.id == item.id_venda).FirstOrDefault();
                    venda.valor -= (item.valor * item.quantidade);
                    if (venda.valor == 0)
                    {
                        venda.valorFinal = 0;
                    }
                    venda.valorFinal = venda.valor - venda.desconto;
                    db.Item_Venda.Remove(item);
                }

                var data = db.Produto.Where(x => x.codigo == codigo && x.id_ciclo == idCiclo).FirstOrDefault();
                db.Produto.Remove(data);
                db.SaveChanges();
                return RedirectToAction("CatalogoProdutos", new { idVenda = -1, idEmpresa = -1 });
            }
            else
            {
                return RedirectToAction("CatalogoProdutos", new { idVenda = -1, idEmpresa = -1 });
            }
        }
    }
}