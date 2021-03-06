using System.Net;

namespace IxothPodFilterDownloader.Models
{
    public class FilterContentDataModel
    {
        public string FilterName { get; set; } = "";
        public string Content { get; set; } = "";
        public string ContentLength { get; set; } = "";
        public string ETag { get; set; } = "";
        public HttpStatusCode HttpStatusCode { get; set; }

        //public string Sha256 { get; set; } = "";
    }
}