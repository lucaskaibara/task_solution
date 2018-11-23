using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskSolution.Models;

namespace TaskSolution.Controllers
{
    public class HomeController : Controller
    {
        private Task_BD db = new Task_BD();

        public ActionResult Index()
        {
            if (new LoginController().VerificaUsuarioLogado())
            {
                ViewBag.GoJS = "onLoad=init();";

                List<int> lstNumeroRegistros = new List<int>();

                lstNumeroRegistros.Add(db.usuario.Where(model => model.cancelado == "N").Count());
                lstNumeroRegistros.Add(db.categoria.Where(model => model.cancelado == "N").Count());
                lstNumeroRegistros.Add(db.tarefa.Where(model => model.cancelado == "N").Count());

                return View(lstNumeroRegistros);
            }

            return RedirectToAction("Index", "Login");
        }

        [ChildActionOnly]
        public ActionResult NomeUsuarioMenu()
        {
            return PartialView("_Layout", System.Web.HttpContext.Current.Session["usuario"]);
        }
    }
}