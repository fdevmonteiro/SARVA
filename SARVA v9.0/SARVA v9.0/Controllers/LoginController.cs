using SARVA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Login = SARVA.Models.Login;

namespace SARVA.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        connSARVA db = new connSARVA();
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Usuario user)
        {
            if (!ModelState.IsValid) // Verifica se os campos do formulário foram preenchidos
            {
                return View();
            }
            else if (db.Usuario.Any(x => x.userName == user.userName || x.email == user.email)) // Verifica se a conta já existe no BD
            {
                ViewBag.ContaJaExiste = "Essa conta já existe";
                return View();
            }
            else // Adiciona o novo usuário ao BD
            {
                db.Usuario.Add(user);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login)
        {
            if (!ModelState.IsValid) // Verifica se os campos do formulário foram preenchidos
            {
                return View();
            }
            var user = db.Usuario.Where(x => x.email == login.Email && x.senha == login.Senha).FirstOrDefault();
            if (user != null)
            {
                var Ticket = new FormsAuthenticationTicket(login.Email, true, 300);
                string Encrypt = FormsAuthentication.Encrypt(Ticket);

                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, Encrypt);
                //cookie.Expires = DateTime.Now.AddSeconds(1); // Essa linha de código guarda as informações da sessão e do usuário por 3000 horas
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);


                Session["idSS"] = user.id;
                Session["userNameSS"] = user.userName;
                Session["emailSS"] = user.email;

                if (user.roleId == 2)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Home");

                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}