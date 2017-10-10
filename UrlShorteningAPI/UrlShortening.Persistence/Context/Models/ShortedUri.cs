namespace UriShortening.Persistence.Context.Models
{
    public class ShortedUrl
    {
        public int Id { get; set; }

        public string SourceUri { get; set; }

        public string ShortUri { get; set; }

        public string CreatedById { get; set; }

        public string CreatedAt { get; set; }

        public int? TransferCount { get; set; }
    }
}
