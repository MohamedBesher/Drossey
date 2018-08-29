using Drossey.Data.Core.Enum;

namespace Drossey.Areas.admin.Models
{
    public class RequestSearchModel:Pager
    {
        public long? PlaceId { get; set; } = 0;
        public long? CityId { get; set; } = 0;
        public RequestStatus? Status { get; set; }
        public long? ServiceId { get; set; }
    }
}