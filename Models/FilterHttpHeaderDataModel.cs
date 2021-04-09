using System.Net;

namespace IxothPodFilterDownloader.Models
{
    public class FilterHttpHeaderDataModel
    {
        public string FilterName { get; set; } = "";
        public string ETag { get; set; } = "";
        public string ContentLength { get; set; } = "";

        public HttpStatusCode HttpStatusCode { get; set; }
    }
}