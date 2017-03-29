namespace PhotoGallery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Summary = c.String(),
                        Uri = c.String(),
                        AlbumId = c.Int(nullable: false),
                        DateUploaded = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.AlbumId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Photos", "AlbumId", "dbo.Albums");
            DropIndex("dbo.Photos", new[] { "UserId" });
            DropIndex("dbo.Photos", new[] { "AlbumId" });
            DropTable("dbo.Photos");
            DropTable("dbo.Albums");
        }
    }
}
