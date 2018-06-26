namespace RestauranteApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pratoes",
                c => new
                    {
                        IdPrato = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        IdRestaurante = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdPrato)
                .ForeignKey("dbo.Restaurantes", t => t.IdRestaurante, cascadeDelete: true)
                .Index(t => t.IdRestaurante);
            
            CreateTable(
                "dbo.Restaurantes",
                c => new
                    {
                        IdRestaurante = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.IdRestaurante);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pratoes", "IdRestaurante", "dbo.Restaurantes");
            DropIndex("dbo.Pratoes", new[] { "IdRestaurante" });
            DropTable("dbo.Restaurantes");
            DropTable("dbo.Pratoes");
        }
    }
}
