namespace UriShortening.Persistence.Context
{
    using System.Data.Entity;
    using Configuration;
    using Models;

    public class ShortUriContext :DbContext
    {
        static ShortUriContext()
        {
            Database.SetInitializer<ShortUriContext>(null);
        }

        public ShortUriContext()
            : this(ConnectionStringReader.GetConnectionString(nameof(ShortUriContext)))
        {
        }

        public ShortUriContext(string connectionString)
            : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<ShortedUrl> ShortedUrls { get; set; }
    }
}
