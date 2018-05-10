namespace Library.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "Test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Test", c => c.String());
        }
    }
}
