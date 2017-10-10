namespace UriShortening.Persistence.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TypeChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ShortedUrls", "TransferCount", c => c.Int());
        }

        public override void Down()
        {
            AlterColumn("dbo.ShortedUrls", "TransferCount", c => c.String());
        }
    }
}
