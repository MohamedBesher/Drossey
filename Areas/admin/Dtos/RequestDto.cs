using System;
using Drossey.Data.Core.Enum;

namespace Drossey.Areas.admin.Dtos
{
    public class RequestDto
    {
        public string FullName { get; set; }
        public long Id { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public string PlaceName { get; set; }
        public string Address { get; set; }
        public DateTime RequestDate { get; set; }
        public decimal? Price { get; set; }
        public string ServiceName { get; set; }
        public string CouffierName { get; set; }
        public string CouffierId { get; set; }
        public string UserId { get; set; }
        public long PlaceId { get; set; }
        public long ServiceId { get; set; }
    }
}