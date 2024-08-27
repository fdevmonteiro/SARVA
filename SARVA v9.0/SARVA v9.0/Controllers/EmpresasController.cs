using SARVA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SARVA.Controllers
{
    public class EmpresasController : Controller
    {
        // GET: Empresas

        connSARVA db = new connSARVA();

        public ActionResult BuscarEmpresa(int requisitando)
        {
            // 1 -> AdicionarProdutoRequisitado
            // 2 -> AdicionarVenda
            // 3 -> AdicionarProduto (admin)
            // 4 -> AdicionarPedido

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.Requisitando = requisitando;
            return View();
        }

        [HttpPost]
        public ActionResult BuscarEmpresa(Cliente model, int requisitando)
        {
            ViewBag.Requisitando = requisitando;
            ViewBag.NomeCliente = model.nome;
            return View();
        }

        [HttpPost]
        public JsonResult GetSearchValue(string search)
        {
            //int idUsuario = Convert.ToInt32(Session["idSS"]);

            var empresas = db.Empresa.Select(x => new
            {
                razaoSocial = x.razao_social,
                id_Usuario = x.id_usuario,
                flag = x.flag
            }).Where(x => x.razaoSocial.Contains(search) && x.flag == true);

            return Json(empresas, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult LerDadosEmpresa()
        {
            var listOfData = db.Empresa.ToList();
            return View(listOfData);
        }

        [HttpGet]
        public ActionResult AdicionarEmpresa()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AdicionarEmpresa(Empresa model)
        {
            if (!ModelState.IsValid) // Verifica se os campos do formulário foram preenchidos
            {
                return View();
            }
            else if (db.Empresa.Any(x => x.id == model.id)) // Verifica se o produto já existe no BD
            {
                ViewBag.JaExiste = "Essa empresa já foi requisitada";
                return View();
            }
            db.Empresa.Add(model);
            db.SaveChanges();
            ViewBag.Message = "Data Insert Successfully";
            int idUsuario = Convert.ToInt32(Session["idSS"]);
            Usuario u = db.Usuario.Where(x => x.id == idUsuario).FirstOrDefault();
            if (u.roleId == 1) // Se for admin...
            {
                return RedirectToAction("LerDadosEmpresa", "Empresas");
            }
            else // Se for vendedor...
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult EditarEmpresa(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            var data = db.Empresa.Where(x => x.id == id).FirstOrDefault();
            return View(data);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditarEmpresa(Empresa Model)
        {
            if (!ModelState.IsValid) // Verifica se os campos do formulário foram preenchidos
            {
                return View();
            }
            var data = db.Empresa.Where(x => x.id == Model.id).FirstOrDefault();
            if (data != null)
            {
                data.id = Model.id;
                data.id_usuario = Model.id_usuario;
                data.razao_social = Model.razao_social;
                data.flag = Model.flag;
                db.SaveChanges();
            }
            return RedirectToAction("LerDadosEmpresa", new { id = Session["idSS"] });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RemoverEmpresa(int id, bool confirm)
        {
            if (confirm)
            {
                var data = db.Empresa.Where(x => x.id == id).FirstOrDefault();
                db.Empresa.Remove(data);
                db.SaveChanges();
                ViewBag.Message = "Record Delete Successfully";
                return RedirectToAction("LerDadosEmpresa", new { id = Session["idSS"] });
            }
            else
            {
                return RedirectToAction("LerDadosEmpresa", new { id = Session["idSS"] });
            }
        }
    }
}