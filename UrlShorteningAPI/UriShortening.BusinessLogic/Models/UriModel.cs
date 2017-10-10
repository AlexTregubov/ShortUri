namespace UriShortening.BusinessLogic.Models
{
    using System;

    public class UriModel
    {
        public string SourceUri { get; set; }

        public string ShortUri { get; set; }

        public DateTime CreatedAt { get; set; }

        public int TransferCount { get; set; }
    }
}
