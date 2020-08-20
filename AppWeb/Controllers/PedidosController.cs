using AppWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWeb.Controllers
{
    public class PedidosController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        // GET: Pedidos
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPedidos(string filtro)
        {
            var query = db.Pedidos
                .Include("Cliente")
                .Include("Detalles")
                .Include("Detalles.Producto")
                .AsQueryable();

            if (!string.IsNullOrEmpty(filtro))
            {
                query = query.Where(c => c.Cliente.Identificacion.Contains(filtro) ||
                    c.Cliente.NombreCompleto.Contains(filtro) ||
                    c.Detalles.Any(d => d.Producto.Nombre.Contains(filtro) ||
                        d.Producto.Precio.ToString().Contains(filtro))
                    );
                    
            }
            var data = query.ToList()
                .Select(p => new PedidosVM
                {
                    Identificacion = p.Cliente.Identificacion,
                    NombreCompleto = p.Cliente.NombreCompleto,
                    FechaPedido = p.FechaPedido.Value,
                    Id = p.Id,
                    Total = p.Total
                })
                .ToList();
            return Json(new { data }, JsonRequestBehavior.AllowGet);
        }

        // GET: Pedidos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pedidos/Create
        [HttpPost]
        public ActionResult Create(PedidoNuevoVM modelo)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pedidos/Create
        public ActionResult CreateAjax()
        {
            return PartialView("_CreateAjax");
        }

        // GET: Pedidos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pedidos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pedidos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
