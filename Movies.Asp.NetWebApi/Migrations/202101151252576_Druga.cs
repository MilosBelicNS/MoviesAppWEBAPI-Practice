namespace Movies.Asp.NetWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Druga : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Directors", "Name", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Directors", "Surname", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Movies", "Name", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Movies", "Genre", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "Genre", c => c.String());
            AlterColumn("dbo.Movies", "Name", c => c.String());
            AlterColumn("dbo.Directors", "Surname", c => c.String());
            AlterColumn("dbo.Directors", "Name", c => c.String());
        }
    }
}
