namespace ControlProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(),
                        Student_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Trainer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trainers", t => t.Trainer_Id)
                .Index(t => t.Trainer_Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GrupAdi = c.String(),
                        TrainerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trainers", t => t.TrainerId)
                .Index(t => t.TrainerId);
            
            CreateTable(
                "dbo.Trainers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "TrainerId", "dbo.Trainers");
            DropForeignKey("dbo.Students", "Trainer_Id", "dbo.Trainers");
            DropForeignKey("dbo.Projects", "Student_Id", "dbo.Students");
            DropIndex("dbo.Teams", new[] { "TrainerId" });
            DropIndex("dbo.Students", new[] { "Trainer_Id" });
            DropIndex("dbo.Projects", new[] { "Student_Id" });
            DropTable("dbo.Trainers");
            DropTable("dbo.Teams");
            DropTable("dbo.Students");
            DropTable("dbo.Projects");
        }
    }
}
