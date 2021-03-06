﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppWeb.Models;

namespace AppWeb.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clientes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetClientes(string filtro) {
            var query = db.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(filtro))
            {
                query = query.Where(c => c.Identificacion.Contains(filtro) || c.NombreCompleto.Contains(filtro));
            }

            return Json(new { data = query.ToList() }, JsonRequestBehavior.AllowGet);
        }
        
        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Identificacion,TipoIdentificacion,NombreCompleto,Activo")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult EditAjax(string id)
        {
            if (id == null)
            {
                var result = new JavaScriptResult();
                result.Script = "$('#detalle-cliente').modal('hide'); alert('La cliente es inválida');";
                return result;
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                var result = new JavaScriptResult();
                result.Script = "$('#detalle-cliente').modal('hide'); alert('El cliente no existe');";
                return result;
            }
            return PartialView("_EditAjax", cliente);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAjax([Bind(Include = "Identificacion,TipoIdentificacion,NombreCompleto,Activo")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                var result = new JavaScriptResult();
                result.Script = "window.location='/Clientes/Index'";
                return result;
            }
            return PartialView("_EditAjax", cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAjax(string id)
        {
            try
            {
                Cliente cliente = db.Clientes.Find(id);
                db.Clientes.Remove(cliente);
                db.SaveChanges();
                return Json(new { data = true });
            }
            catch (Exception)
            {
                return Json(new { data = false });
            }
            
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
