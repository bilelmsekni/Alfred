using System;
using System.Collections.Generic;
using System.Linq;
using Alfred.Dal.Implementation.Fake.Standard.EntityDtos;
using Alfred.Shared.Standard.Enums;

namespace Alfred.Dal.Implementation.Fake.Standard.Filters
{
    public static class ArtifactFilters
    {
        public static Func<ArtifactDto, bool> FilterOnIds(this Func<ArtifactDto, bool> filters, IEnumerable<int> ids)
        {
            if (ids != null && ids.Any())
                filters += dto => ids.Contains(dto.Id);
            return filters;
        }

        public static Func<ArtifactDto, bool> FilterOnTitle(this Func<ArtifactDto, bool> filters, string title)
        {
            if (!string.IsNullOrEmpty(title))
                filters += dto => dto.Title.Contains(title);
            return filters;
        }

        public static Func<ArtifactDto, bool> FilterOnType(this Func<ArtifactDto, bool> filters, ArtifactType? type)
        {
            if (type.HasValue)
                filters += dto => (ArtifactType)dto.Type == type.Value;
            return filters;
        }

        public static Func<ArtifactDto, bool> FilterOnStatus(this Func<ArtifactDto, bool> filters, ArtifactStatus? status)
        {
            if (status.HasValue)
                filters += dto => (ArtifactStatus)dto.Status == status.Value;
            return filters;
        }

        public static Func<ArtifactDto, bool> FilterOnMemberId(this Func<ArtifactDto, bool> filters, int? memberId)
        {
            if (memberId.HasValue)
                filters += dto => dto.MemberId == memberId.Value;
            return filters;
        }

        public static Func<ArtifactDto, bool> FilterOnCommunityId(this Func<ArtifactDto, bool> filters, int? communityId)
        {
            if (communityId.HasValue)
                filters += dto => dto.CommunityId == communityId.Value;
            return filters;
        }
    }
}
