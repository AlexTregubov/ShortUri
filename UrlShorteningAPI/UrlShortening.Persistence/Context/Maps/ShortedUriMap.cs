namespace UriShortening.Persistence.Context.Maps
{
    using System.Data.Entity.ModelConfiguration;
    using Models;

    internal class ShortedUrlMap : EntityTypeConfiguration<ShortedUrl>
    {
        public ShortedUrlMap()
        {
            Map(m =>
            {
                m.ToTable("ShortedUri");
            });

            HasKey(it => it.Id);

            Property(it => it.SourceUri)
                .IsRequired();

            Property(it => it.ShortUri)
                .IsRequired();

            Property(it => it.CreatedAt)
                .IsOptional();

            Property(it => it.TransferCount)
                .IsOptional();
        }
    }
}
