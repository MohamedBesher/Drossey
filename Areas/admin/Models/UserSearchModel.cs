namespace Drossey.Areas.admin.Models
{
    public class UserSearchModel : Pager
    {

        public long CountryId { get; set; } = 0;
        public long PlaceId { get; set; } = 0;
        public bool? IsSuspended { get; set; } = null;
    }
}