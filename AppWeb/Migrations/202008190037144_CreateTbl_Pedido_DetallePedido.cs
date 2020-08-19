namespace AppWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTbl_Pedido_DetallePedido : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DetallePedido",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cantidad = c.Int(nullable: false),
                        Pedido_Id = c.Int(nullable: false),
                        Producto_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pedido", t => t.Pedido_Id, cascadeDelete: true)
                .ForeignKey("dbo.Producto", t => t.Producto_Id, cascadeDelete: true)
                .Index(t => t.Pedido_Id)
                .Index(t => t.Producto_Id);
            
            CreateTable(
                "dbo.Pedido",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaPedido = c.DateTime(nullable: false),
                        Cliente_Identificacion = c.String(nullable: false, maxLength: 15, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cliente", t => t.Cliente_Identificacion, cascadeDelete: true)
                .Index(t => t.Cliente_Identificacion);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DetallePedido", "Producto_Id", "dbo.Producto");
            DropForeignKey("dbo.DetallePedido", "Pedido_Id", "dbo.Pedido");
            DropForeignKey("dbo.Pedido", "Cliente_Identificacion", "dbo.Cliente");
            DropIndex("dbo.Pedido", new[] { "Cliente_Identificacion" });
            DropIndex("dbo.DetallePedido", new[] { "Producto_Id" });
            DropIndex("dbo.DetallePedido", new[] { "Pedido_Id" });
            DropTable("dbo.Pedido");
            DropTable("dbo.DetallePedido");
        }
    }
}
