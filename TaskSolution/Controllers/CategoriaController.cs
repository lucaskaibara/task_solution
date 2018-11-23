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
    public class CategoriaController : Controller
    {
        private Task_BD db = new Task_BD();
        
        public ActionResult Index()
        {
            if (new LoginController().VerificaUsuarioLogado())
            {
                return View(db.categoria.Where(model => model.cancelado == "N").ToList());
            }

            return RedirectToAction("Index", "Login");
        }

        public ActionResult Cadastrar()
        {
            if (new LoginController().VerificaUsuarioLogado())
            {
                return View();
            }

            return RedirectToAction("Index", "Login");
        }

        // Adicionar Categoria
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar([Bind(Include = "id,nome,cancelado")] categoria categoria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.categoria.Add(categoria);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                } catch(Exception e)
                {
                    return View(e);
                }
            }

            return View(categoria);
        }
        
        public ActionResult Editar(int? id)
        {
            if (new LoginController().VerificaUsuarioLogado())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                categoria categoria = db.categoria.Find(id);
                if (categoria == null)
                {
                    return HttpNotFound();
                }
                return View(categoria);
            }

            return RedirectToAction("Index", "Login");            
        }

        // Editar Categoria
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "id,nome,cancelado")] categoria categoria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(categoria).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                } catch (Exception e)
                {
                    return View(e);
                }
            }
            return View(categoria);
        }

        // Excluir Categoria
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(int id)
        {
            categoria categoria = db.categoria.Find(id);
            categoria.cancelado = "S";
            //db.Entry(categoria).State = EntityState.Modified;
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
