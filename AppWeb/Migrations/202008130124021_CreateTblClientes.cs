namespace AppWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTblClientes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        Indentificacion = c.String(nullable: false, maxLength: 15, unicode: false),
                        TipoIdentificacion = c.Int(nullable: false),
                        NombreCompleto = c.String(nullable: false, maxLength: 300),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Indentificacion)
                .Index(t => t.NombreCompleto, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Cliente", new[] { "NombreCompleto" });
            DropTable("dbo.Cliente");
        }
    }
}
