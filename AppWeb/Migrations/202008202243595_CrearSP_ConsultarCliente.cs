namespace AppWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CrearSP_ConsultarCliente : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
               "dbo.sp_guardarCliente",
               p => new
               {
                   identificacion = p.String(maxLength: 15),
                   tipo_identificacion = p.Int(),
                   nombre_completo = p.String(maxLength: 250),
                   activo = p.Boolean()
               },
               body:
                   @"SET NOCOUNT ON;
	                IF NOT EXISTS(SELECT 1 FROM [dbo].[Cliente] WHERE [Identificacion] = @identificacion AND [TipoIdentificacion] = @tipo_identificacion)
	                BEGIN
		                INSERT INTO [dbo].[Cliente]
			                   ([Identificacion]
			                   ,[TipoIdentificacion]
			                   ,[NombreCompleto]
			                   ,[Activo])
		                 VALUES
			                   (@identificacion
			                   ,@tipo_identificacion
			                   ,@nombre_completo
			                   ,@activo)
	                END 
	                ELSE 
	                BEGIN
		                UPDATE [dbo].[Cliente]
		                   SET 
			                  [NombreCompleto] = @nombre_completo,
			                  [Activo] = @activo
		                 WHERE [Identificacion] = @identificacion
			                   AND [TipoIdentificacion] = @tipo_identificacion
	                END "
           );

            CreateStoredProcedure(
               "dbo.sp_consultarCliente",
               p => new
               {
                   identificacion = p.String(maxLength: 15),
                   tipo_identificacion = p.Int()
               },
               body:
                   @"select 
		                c.TipoIdentificacion,
		                c.Identificacion,
		                c.NombreCompleto,
		                c.Activo
	                from Cliente c
	                where c.TipoIdentificacion = @tipo_identificacion
	                and c.Identificacion = @identificacion"
           );
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.sp_consultarCliente");
            DropStoredProcedure("dbo.sp_guardarCliente");
        }
    }
}
