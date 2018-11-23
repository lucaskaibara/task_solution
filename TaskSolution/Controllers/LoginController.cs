using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskSolution.Models;

namespace TaskSolution.Controllers
{
    public class LoginController : Controller
    {
        private Task_BD db = new Task_BD();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(usuario usuario)
        {
            usuario.senha = new UsuarioController().SenhaMD5(usuario.senha);

            var usuarioBanco = db.usuario.FirstOrDefault(model => model.login == usuario.login && model.senha == usuario.senha); 

            if (usuarioBanco != null)
            {
                System.Web.HttpContext.Current.Session["usuario"] = usuarioBanco.nome_completo;
                return RedirectToAction("Index", "Home");
            }

            else
            {
                return View("Index");
            }

        }
        
        public ActionResult Logoff()
        {
            System.Web.HttpContext.Current.Session["usuario"] = null;
            return RedirectToAction("Index", "Login");
        }

        public bool VerificaUsuarioLogado()
        {
            if (System.Web.HttpContext.Current.Session["usuario"] != null)
            {
                return true;
            }

            return false;
        }
       
    }
}