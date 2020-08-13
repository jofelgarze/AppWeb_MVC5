namespace AppWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTblClientes_CambioIdentificacion : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Cliente");
            AddColumn("dbo.Cliente", "Identificacion", c => c.String(nullable: false, maxLength: 15, unicode: false));
            AddPrimaryKey("dbo.Cliente", "Identificacion");
            DropColumn("dbo.Cliente", "Indentificacion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cliente", "Indentificacion", c => c.String(nullable: false, maxLength: 15, unicode: false));
            DropPrimaryKey("dbo.Cliente");
            DropColumn("dbo.Cliente", "Identificacion");
            AddPrimaryKey("dbo.Cliente", "Indentificacion");
        }
    }
}
