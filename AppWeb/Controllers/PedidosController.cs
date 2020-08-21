using AppWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            CallProcedure();
            return View();
           
        }

        private void CallProcedure() 
        {
            var identificacion = new SqlParameter("@identificacion", "9999999999");
            var tipoIdentificacion = new SqlParameter("@tipo_identificacion", ETipoIdentificacion.CEDULA);
            var nota1 = new SqlParameter("@nombre_completo", "Consumidor Final");
            var nota2 = new SqlParameter("@activo",true);

            db.Database.ExecuteSqlCommand("sp_guardarCliente @identificacion, @tipo_identificacion, @nombre_completo, @activo", identificacion, tipoIdentificacion, nota1, nota2);

            var result = db.Database.SqlQuery<Cliente>("sp_consultarCliente @p0, @p1", 
                    parameters: new[] { "9999999999", Convert.ToString((int) ETipoIdentificacion.CEDULA) }
                );
            var registro = result.FirstOrDefault();
            Console.WriteLine(registro);
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
        public ActionResult DetailsAjax(int id)
        {
            var pedido = db.Pedidos
                .Include("Cliente")
                .Include("Detalles")
                .Include("Detalles.Producto")
                .Where(p => p.Id == id).FirstOrDefault();

            if (pedido == null)
            {
                var result = new JavaScriptResult();
                result.Script = "$('#detalle-pedido').modal('hide'); swal('Mensaje del Sistema', 'El registro no existe', 'error');";
                return result;
            }
            
            return PartialView("_DetailsAjax", pedido);
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        {
            return View(new PedidoNuevoVM());
        }

        // POST: Pedidos/Create
        [HttpPost]
        public ActionResult Create(PedidoNuevoVM modelo)
        {
            if (ModelState.IsValid)
            {
                var pedido = new Pedido();

                var cliente = db.Clientes.Find(modelo.ClienteId);

                //if (!db.Clientes.Any(c => c.Identificacion == modelo.ClienteId))
                if (cliente == null)
                {
                    return Json(new { 
                        Codigo = 400,
                        Mensaje = "El cliente no existe." 
                    });
                }

                pedido.Cliente = cliente;
                pedido.FechaPedido = modelo.FechaPedido;

                foreach (var item in modelo.Detalles)
                {
                    var producto = db.Productos.Find(item.ProductoId);
                    if (producto == null)
                    {
                        return Json(new
                        {
                            Codigo = 400,
                            Mensaje = String.Format("El producto '{0}' no existe.", item.ProductoNombre)
                        });
                    }
                    pedido.Detalles.Add(new DetallePedido { 
                        Cantidad = item.Cantidad,
                        Producto = producto
                    });
                }

                db.Pedidos.Add(pedido);
                db.SaveChanges();
                return Json(new
                {
                    Codigo = 200,
                    Mensaje = String.Format("El pedido se generó con el siguiente Id:'{0}'.", pedido.Id)
                });
            }
            return Json(new
            {
                Codigo = 400,
                Mensaje = "Existen campos invalidos, favor revisar."
            });
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
