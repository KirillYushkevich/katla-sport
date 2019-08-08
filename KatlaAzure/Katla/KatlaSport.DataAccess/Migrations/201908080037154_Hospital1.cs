namespace KatlaSport.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Hospital1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.departments", new[] { "department_parentid" });
            AlterColumn("dbo.departments", "department_parentid", c => c.Int());
            CreateIndex("dbo.departments", "department_parentid");
        }

        public override void Down()
        {
            DropIndex("dbo.departments", new[] { "department_parentid" });
            AlterColumn("dbo.departments", "department_parentid", c => c.Int(nullable: false));
            CreateIndex("dbo.departments", "department_parentid");
        }
    }
}
