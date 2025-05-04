namespace ControlProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSuccessRateToStudents : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "SuccessRate", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "SuccessRate", c => c.Int(nullable: false));
        }
    }
}
