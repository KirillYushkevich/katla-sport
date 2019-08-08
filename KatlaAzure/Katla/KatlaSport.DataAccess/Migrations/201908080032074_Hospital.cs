namespace KatlaSport.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Hospital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.departments",
                c => new
                    {
                        department_id = c.Int(nullable: false, identity: true),
                        department_name = c.String(),
                        department_description = c.String(),
                        department_parentid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.department_id)
                .ForeignKey("dbo.departments", t => t.department_parentid)
                .Index(t => t.department_parentid);

            CreateTable(
                "dbo.doctors",
                c => new
                    {
                        doctor_id = c.Int(nullable: false, identity: true),
                        doctor_name = c.String(),
                        doctor_surname = c.String(),
                        doctor_photo = c.String(),
                        doctor_departmentid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.doctor_id)
                .ForeignKey("dbo.departments", t => t.doctor_departmentid, cascadeDelete: true)
                .Index(t => t.doctor_departmentid);

            CreateTable(
                "dbo.patients",
                c => new
                    {
                        patient_id = c.Int(nullable: false, identity: true),
                        patient_name = c.String(),
                        patient_surname = c.String(),
                        patient_age = c.Int(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        patient_casehistorynumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.patient_id)
                .ForeignKey("dbo.doctors", t => t.DoctorId, cascadeDelete: true)
                .Index(t => t.DoctorId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.departments", "department_parentid", "dbo.departments");
            DropForeignKey("dbo.patients", "DoctorId", "dbo.doctors");
            DropForeignKey("dbo.doctors", "doctor_departmentid", "dbo.departments");
            DropIndex("dbo.patients", new[] { "DoctorId" });
            DropIndex("dbo.doctors", new[] { "doctor_departmentid" });
            DropIndex("dbo.departments", new[] { "department_parentid" });
            DropTable("dbo.patients");
            DropTable("dbo.doctors");
            DropTable("dbo.departments");
        }
    }
}
