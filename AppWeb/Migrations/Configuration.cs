namespace AppWeb.Migrations
{
    using AppWeb.Models;
    using System;
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
            }

        }
    }
}
