using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskSolution;
using TaskSolution.Models;

namespace TaskSolution.Controllers
{
    public class TarefaController : Controller
    {
        private Task_BD db = new Task_BD();
        
        public ActionResult Index()
        {
            if (new LoginController().VerificaUsuarioLogado())
            {
                var tarefa = db.tarefa.Include(t => t.categoria).Include(t => t.usuario);
                return View(tarefa.Where(model => model.cancelado == "N").ToList());
            }

            return RedirectToAction("Index", "Login");
        }
        
        public ActionResult Cadastrar()
        {
            if (new LoginController().VerificaUsuarioLogado())
            {
                ViewBag.categoria_id = new SelectList(db.categoria.Where(model => model.cancelado == "N"), "id", "nome");
                ViewBag.usuario_id = new SelectList(db.usuario.Where(model => model.cancelado == "N"), "id", "nome_completo");
                ViewBag.tarefa_id = new SelectList(db.tarefa.Where(model => model.cancelado == "N"), "id", "nome");
                return View();
            }

            return RedirectToAction("Index", "Login");            
        }

        // Adicionar Tarefa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar([Bind(Include = "id,nome,descricao,cancelado,usuario_id,categoria_id,tarefa_id,estado,horas_estimadas,horas_realizadas")] tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                db.tarefa.Add(tarefa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categoria_id = new SelectList(db.categoria, "id", "nome", tarefa.categoria_id);
            ViewBag.usuario_id = new SelectList(db.usuario, "id", "nome_completo", tarefa.usuario_id);
            ViewBag.tarefa_id = new SelectList(db.tarefa, "id", "nome", tarefa.tarefa_id);
            return View(tarefa);
        }
        
        public ActionResult Editar(int? id)
        {
            if (new LoginController().VerificaUsuarioLogado())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tarefa tarefa = db.tarefa.Find(id);
                if (tarefa == null)
                {
                    return HttpNotFound();
                }
                ViewBag.categoria_id = new SelectList(db.categoria.Where(model => model.cancelado == "N"), "id", "nome", tarefa.categoria_id);
                ViewBag.usuario_id = new SelectList(db.usuario.Where(model => model.cancelado == "N"), "id", "nome_completo", tarefa.usuario_id);
                ViewBag.tarefa_id = new SelectList(db.tarefa.Where(model => model.cancelado == "N"), "id", "nome", tarefa.tarefa_id);
                return View(tarefa);
            }

            return RedirectToAction("Index", "Login");            
        }

        // Editar Categoria
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "id,nome,descricao,cancelado,usuario_id,categoria_id,tarefa_id,estado,horas_estimadas,horas_realizadas")] tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tarefa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoria_id = new SelectList(db.categoria, "id", "nome", tarefa.categoria_id);
            ViewBag.usuario_id = new SelectList(db.usuario, "id", "nome_completo", tarefa.usuario_id);
            ViewBag.tarefa_id = new SelectList(db.tarefa, "id", "nome", tarefa.tarefa_id);
            return View(tarefa);
        }

        // Excluir Tarefa
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(int id)
        {
            tarefa tarefa = db.tarefa.Find(id);
            tarefa.cancelado = "S";
            db.Entry(tarefa).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
