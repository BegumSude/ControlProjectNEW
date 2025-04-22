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
                        ProjectId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Student_StudentId = c.Int(),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Students", t => t.Student_StudentId)
                .Index(t => t.Student_StudentId);
            
            CreateTable(
                "dbo.StudentProjects",
                c => new
                    {
                        StudentProjectId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.StudentProjectId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        TeacherId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                    })
                .PrimaryKey(t => t.TeacherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.StudentProjects", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Projects", "Student_StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentProjects", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Students", new[] { "TeacherId" });
            DropIndex("dbo.StudentProjects", new[] { "ProjectId" });
            DropIndex("dbo.StudentProjects", new[] { "StudentId" });
            DropIndex("dbo.Projects", new[] { "Student_StudentId" });
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.StudentProjects");
            DropTable("dbo.Projects");
        }
    }
}
