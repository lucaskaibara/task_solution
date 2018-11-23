using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskSolution.Models;

namespace TaskSolution.Controllers
{
    public class DiagramaController : Controller
    {
        private Task_BD db = new Task_BD();

        public ActionResult Index()
        {
            if (new LoginController().VerificaUsuarioLogado())
            {
                ViewBag.GoJS = "onLoad=init();";
                return View(ItensTarefa());
            }

            return RedirectToAction("Index", "Login");
        }

        public JsonResult ItensDiagramaGoJS()
        {
            List<Object> lstItens = new List<object>();
            List<tarefa> lstTarefa = db.tarefa.Where(model => model.cancelado == "N").ToList();

            foreach (tarefa t in lstTarefa)
            {
                lstItens.Add(new
                {
                    key = t.id,
                    question = t.nome,
                    idPai = t.tarefa_id
                });
            }

            return Json(lstItens, JsonRequestBehavior.AllowGet);
        }
        
        public List<Object> ItensTarefa()
        {
            List<Object> lstItens = new List<object>();
            List<tarefa> lstTarefa = db.tarefa.Where(model => model.cancelado == "N" && model.tarefa_id == null).ToList();
           
            foreach (tarefa t in lstTarefa)
            {
                lstItens.Add(t);
                ItensTarefaFilho(lstItens, t.id);
            }

            return lstItens;
        }

        public void ItensTarefaFilho(List<Object> lstItens, int idPai)
        {
            List<tarefa> lstTarefa = db.tarefa.Where(model => model.cancelado == "N" && model.tarefa_id == idPai).ToList();

            foreach (tarefa t in lstTarefa)
            {
                lstItens.Add(t);
                ItensTarefaFilho(lstItens, t.id);
            }
        }
    }
}