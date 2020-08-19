namespace AppWeb.Migrations
{
    using AppWeb.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AppWeb.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppWeb.Models.ApplicationDbContext context)
        {

            if (!context.Clientes.Any())
            {
                var clienteConsumidorFinal = new Cliente()
                {
                    Identificacion = "9999999999",
                    NombreCompleto = "Consumidor Final",
                    TipoIdentificacion = ETipoIdentificacion.CEDULA,
                    Activo =  true
                };
                context.Clientes.AddOrUpdate(clienteConsumidorFinal);

                context.SaveChanges();
            }

            if (!context.Productos.Any())
            {
                var producto = new Producto
                {
                    CategoriaProducto = ECategoriaProducto.Accesorios,
                    FechaAprobacion = DateTime.Now.AddDays(-45),
                    Nombre = "Auriculares",
                    Precio = 48
                };

                var producto2 = new Producto
                {
                    CategoriaProducto = ECategoriaProducto.Accesorios,
                    FechaAprobacion = DateTime.Now.AddDays(-45),
                    Nombre = "Parlantes",
                    Precio = 80.58M
                };

                context.Productos.AddOrUpdate(producto);
                context.Productos.AddOrUpdate(producto2);

                context.SaveChanges();
            }

            if (!context.Pedidos.Any())
            {
                var pedido = new Pedido()
                {
                    Cliente = new Cliente() {
                        TipoIdentificacion = ETipoIdentificacion.CEDULA,
                        Identificacion = "0956815503",
                        NombreCompleto = "Josue Garcia",
                        Activo = true                        
                    },
                    FechaPedido = DateTime.Now,
                    Detalles = new List<DetallePedido>()                    
                };

                var detalle = new DetallePedido
                {
                    Pedido = pedido,
                    Cantidad = 10,
                    Producto = new Producto()
                    {
                        CategoriaProducto = ECategoriaProducto.Accesorios,
                        Nombre = "Cargador Usb Type C",
                        FechaAprobacion = DateTime.Now.AddDays(-30),
                        Precio = 20
                    }
                };

                context.Pedidos.Add(pedido);

                pedido.Detalles.Add(detalle);



                context.SaveChanges();
            }

            
        }
    }
}
