namespace ControlProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "Student_StudentId", "dbo.Students");
            DropIndex("dbo.Projects", new[] { "Student_StudentId" });
            DropColumn("dbo.Projects", "Student_StudentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "Student_StudentId", c => c.Int());
            CreateIndex("dbo.Projects", "Student_StudentId");
            AddForeignKey("dbo.Projects", "Student_StudentId", "dbo.Students", "StudentId");
        }
    }
}
