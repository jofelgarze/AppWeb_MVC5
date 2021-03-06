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
    public class ProductosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Productos
        public ActionResult Index()
        {
            return View(db.Productos.ToList());
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        public ActionResult GetProductos(string filtro)
        {
            var query = db.Productos.AsQueryable();

            if (!string.IsNullOrEmpty(filtro))
            {
                query = query.Where(c => c.Id.ToString().Contains(filtro) || c.Nombre.Contains(filtro));
            }

            return Json(new { data = query.ToList() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DetailsAjax(int? id)
        {
            if (id == null)
            {
                var result = new JavaScriptResult();
                result.Script = "$('#detalle-productos').modal('hide'); alert('La consulta es inválida');";
                return result;
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                var result = new JavaScriptResult();
                result.Script = "$('#detalle-productos').modal('hide'); alert('El producto no existe');";
                return result;
            }
            return PartialView("_DetailsAjax",producto);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Productos.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(producto);
        }

        // GET: Productos/Edit/5?dato1=data&dato2=data....
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Precio,FechaAprobacion,CategoriaProducto")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // GET: Productos/Edit/5?dato1=data&dato2=data....
        public ActionResult EditAjax(int? id)
        {
            if (id == null)
            {
                var result = new JavaScriptResult();
                result.Script = "$('#detalle-productos').modal('hide'); alert('La consulta es inválida');";
                return result;
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                var result = new JavaScriptResult();
                result.Script = "$('#detalle-productos').modal('hide'); alert('El producto no existe');";
                return result;
            }
            return PartialView("_EditAjax", producto);
        }

        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAjax([Bind(Include = "Id,Nombre,Precio,FechaAprobacion,CategoriaProducto")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                var result = new JavaScriptResult();
                result.Script = "window.location='/Productos/Index'";
                return result;

            }
            return PartialView("_EditAjax", producto);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Productos.Find(id);
            db.Productos.Remove(producto);
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
