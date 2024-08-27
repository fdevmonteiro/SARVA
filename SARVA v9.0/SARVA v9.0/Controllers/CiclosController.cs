using SARVA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SARVA.Controllers
{
    public class CiclosController : Controller
    {
        // GET: Ciclos

        connSARVA db = new connSARVA();

        [Authorize(Roles = "Admin")]
        public ActionResult LerDadosCiclo()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            var listOfData = db.Ciclo.ToList().OrderBy(x => x.Empresa.razao_social);
            return View(listOfData);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AdicionarCiclo()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AdicionarCiclo(Ciclo model)
        {
            int idUsuario = Convert.ToInt32(Session["idSS"]);
            var razaoSocial = Convert.ToString(Request["razaoSocial"]);
            var empresa = db.Empresa.Where(x => x.razao_social == razaoSocial && x.id_usuario == idUsuario).FirstOrDefault();

            Ciclo ciclo = new Ciclo();
            ciclo.id = model.id;
            ciclo.nome = model.nome;
            ciclo.id_empresa = empresa.id;
            DateTime dataInicio = Convert.ToDateTime(Request["dataInicio"]);
            ciclo.dataInicio = dataInicio;
            DateTime dataFim = Convert.ToDateTime(Request["dataFim"]);
            ciclo.dataFim = dataFim;

            db.Ciclo.Add(ciclo);
            db.SaveChanges();

            return RedirectToAction("LerDadosCiclo", "Ciclos");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult EditarCiclo(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            var ciclo = db.Ciclo.Where(x => x.id == id).FirstOrDefault();
            return View(ciclo);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditarCiclo(Ciclo model)
        {
            if (!ModelState.IsValid) // Verifica se os campos do formulário foram preenchidos
            {
                return View();
            }

            int idUsuario = Convert.ToInt32(Session["idSS"]);
            var razaoSocial = Convert.ToString(Request["razaoSocial"]);
            var empresa = db.Empresa.Where(x => x.razao_social == razaoSocial && x.id_usuario == idUsuario).FirstOrDefault();

            var ciclo = db.Ciclo.Where(x => x.id == model.id).FirstOrDefault();
            if (ciclo != null)
            {
                ciclo.id = model.id;
                ciclo.nome = model.nome;
                ciclo.id_empresa = empresa.id;
                DateTime dataInicio = Convert.ToDateTime(Request["dataInicio"]);
                ciclo.dataInicio = dataInicio;
                DateTime dataFim = Convert.ToDateTime(Request["dataFim"]);
                ciclo.dataFim = dataFim;
                db.SaveChanges();
            }
            return RedirectToAction("LerDadosCiclo", "Ciclos");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RemoverCiclo(int id, bool confirm)
        {
            if (confirm)
            {
                var listProdutos = db.Produto.ToList().Where(x => x.id_ciclo == id);
                foreach (var item in listProdutos)
                {
                    db.Produto.Remove(item);
                }
                var ciclo = db.Ciclo.Where(x => x.id == id).FirstOrDefault();
                db.Ciclo.Remove(ciclo);
                db.SaveChanges();
                return RedirectToAction("LerDadosCiclo", "Ciclos");
            }
            else
            {
                return RedirectToAction("LerDadosCiclo", "Ciclos");
            }
            
        }
    }
}