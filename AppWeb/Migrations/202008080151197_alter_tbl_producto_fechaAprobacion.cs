namespace AppWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_producto_fechaAprobacion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Producto", "FechaAprobacion", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Producto", "FechaAprobacion");
        }
    }
}
