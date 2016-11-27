using System;
using System.Collections.Generic;
using System.Linq;
using Alfred.Dal.Implementation.Fake.EntityDtos;

namespace Alfred.Dal.Implementation.Fake.Filters
{
    public static class CommunityFilters
    {
        public static Func<CommunityDto, bool> FilterOnIds(this Func<CommunityDto, bool> criteriaFilters,
            IEnumerable<int> ids)
        {
            if (ids != null && ids.Any()) criteriaFilters += dto => ids.Contains(dto.Id);
            return criteriaFilters;
        }

        public static Func<CommunityDto, bool> FilterOnEmail(this Func<CommunityDto, bool> criteriaFilters, 
            string email)
        {
            if (!string.IsNullOrEmpty(email)) criteriaFilters += dto => email.Contains(dto.Email);
            return criteriaFilters;
        }

        public static Func<CommunityDto, bool> FilterOnName(this Func<CommunityDto, bool> criteriaFilters, 
            string name)
        {
            if (!string.IsNullOrEmpty(name)) criteriaFilters += dto => name.Contains(dto.Name);
            return criteriaFilters;
        }
    }
}
