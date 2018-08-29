using Drossey.Data.Core.Dto;
using System.Collections.Generic;

namespace Drossey.Admin.Services
{
    public interface ITimeZone
    {
        List<Dto> GetTimesZones();

        List<Dto> GetLanguages();
    }
}
