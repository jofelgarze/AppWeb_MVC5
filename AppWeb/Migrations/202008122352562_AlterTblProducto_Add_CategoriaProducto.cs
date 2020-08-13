namespace AppWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTblProducto_Add_CategoriaProducto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Producto", "CategoriaProducto", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Producto", "CategoriaProducto");
        }
    }
}
