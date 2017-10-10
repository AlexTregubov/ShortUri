namespace UriShortening.Persistence.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CreatedAtAndTransferCountAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShortedUrls", "CreatedAt", c => c.String());
            AddColumn("dbo.ShortedUrls", "TransferCount", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.ShortedUrls", "TransferCount");
            DropColumn("dbo.ShortedUrls", "CreatedAt");
        }
    }
}
