using SARVA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SARVA.Controllers
{
    public class Item_PedidoController : Controller
    {
        connSARVA db = new connSARVA();
        public ActionResult AdicionarItem_Pedido(int codigoIP, int idCiclo, int idVenda, int id_Pedido)
        {
            Item_Pedido itemPedido = new Item_Pedido();

            itemPedido.codigo_IV = codigoIP;
            itemPedido.id_ciclo_IV = idCiclo;
            itemPedido.id_venda_IV = idVenda;
            itemPedido.id_pedido = id_Pedido;
            itemPedido.valor = db.Item_Venda.Where(x => x.id_ciclo_produto == idCiclo && x.codigo_produto == codigoIP).FirstOrDefault().valor;
            db.Item_Pedido.Add(itemPedido);

            db.SaveChanges();

            Pedido pedido = db.Pedido.Where(x => x.id == id_Pedido).FirstOrDefault();

            return RedirectToAction("CatalogoItensVenda", "Item_Venda", new { idPedido = id_Pedido, idEmpresa = pedido.id_empresa });
        }
    }
}