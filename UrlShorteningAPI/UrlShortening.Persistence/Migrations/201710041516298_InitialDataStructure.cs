namespace UriShortening.Persistence.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialDataStructure : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShortedUrls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SourceUri = c.String(),
                        ShortUri = c.String(),
                        CreatedById = c.String(),
                    })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.ShortedUrls");
        }
    }
}
