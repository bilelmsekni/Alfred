using System;
using System.Collections.Generic;
using System.Linq;
using Alfred.Dal.Implementation.Fake.EntityDtos;
using Alfred.Shared.Enums;

namespace Alfred.Dal.Implementation.Fake.Filters
{
    public static class MemberFilters
    {
        public static Func<MemberDto, bool> FilterOnIds(this Func<MemberDto, bool> criteriaFilters, IEnumerable<int> ids)
        {
            if (ids != null && ids.Any()) criteriaFilters += dto => ids.Contains(dto.Id);

            return criteriaFilters;
        }

        public static Func<MemberDto, bool> FilterOnCommunityId(this Func<MemberDto, bool> criteriaFilters, int? communityId)
        {
            if (communityId.HasValue) criteriaFilters += dto => dto.CommunityId == communityId.Value;

            return criteriaFilters;
        }

        public static Func<MemberDto, bool> FilterOnEmail(this Func<MemberDto, bool> criteriaFilters, string email)
        {
            if (!string.IsNullOrEmpty(email)) criteriaFilters += dto => dto.Email.Contains(email);

            return criteriaFilters;
        }

        public static Func<MemberDto, bool> FilterOnName(this Func<MemberDto, bool> criteriaFilters, string name)
        {
            if (!string.IsNullOrEmpty(name)) criteriaFilters += dto => IsContainedInside(dto.FirstName, name) || IsContainedInside(dto.LastName, name);

            return criteriaFilters;
        }

        public static Func<MemberDto, bool> FilterOnRole(this Func<MemberDto, bool> criteriaFilters, CommunityRole? role)
        {
            if (role.HasValue) criteriaFilters += dto => (CommunityRole)dto.Role == role;

            return criteriaFilters;
        }

        private static bool IsContainedInside(string name, string word)
        {
            if (string.IsNullOrEmpty(name)) return false;
            return name.Contains(word);
        }
    }
}
