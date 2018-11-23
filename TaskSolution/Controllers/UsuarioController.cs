using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TaskSolution;
using TaskSolution.Models;

namespace TaskSolution.Controllers
{
    public class UsuarioController : Controller
    {
        private Task_BD db = new Task_BD();

        public String SenhaMD5(String senha) {
            MD5 md5Hasher = MD5.Create();
            byte[] valorCriptografado = md5Hasher.ComputeHash(Encoding.Default.GetBytes(senha));
            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i < valorCriptografado.Length; i++)
            {
                strBuilder.Append(valorCriptografado[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
        
        public ActionResult Index()
        {
            if (new LoginController().VerificaUsuarioLogado())
            {
                return View(db.usuario.Where(model => model.cancelado == "N").ToList());
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

        // Adicionar Usuário
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar([Bind(Include = "id,nome_completo,data_nascimento,email,login,senha,cancelado")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    usuario.senha = SenhaMD5(usuario.senha);
                    db.usuario.Add(usuario);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                } catch (Exception e)
                {
                    return View(e);
                }
            }

            return View(usuario);
        }
        
        public ActionResult Editar(int? id)
        {
            if (new LoginController().VerificaUsuarioLogado())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                usuario usuario = db.usuario.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }

            return RedirectToAction("Index", "Login");
        }

        // Editar Usuário
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "id,nome_completo,data_nascimento,email,login,senha,cancelado")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(usuario).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                } catch (Exception e)
                {
                    return View(e);
                }
            }
            return View(usuario);
        }

        // Excluir Usuário
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(int id)
        {
            usuario usuario = db.usuario.Find(id);
            usuario.cancelado = "S";
            db.Entry(usuario).State = EntityState.Modified;
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
