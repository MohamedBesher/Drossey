using Drossey.Data.Core.Enum;

namespace Drossey.Areas.admin.Models
{
    public class Pager
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Keyword { get; set; } = string.Empty;
        public long Id { get; set; }
        public string UserId { get; set; }

        public bool IsActive { get; set; } = false;
    }

    public class CodeSearchModel: Pager
    {
        public int Status { get; set; } = 0;
        public long SellerId { get; set; } = 0;
    }




    }
