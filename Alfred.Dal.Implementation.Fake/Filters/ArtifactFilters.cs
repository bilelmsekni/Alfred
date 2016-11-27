using System;
using System.Collections.Generic;
using System.Linq;
using Alfred.Dal.Implementation.Fake.EntityDtos;
using Alfred.Shared.Enums;

namespace Alfred.Dal.Implementation.Fake.Filters
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
                filters += dto => dto.Type == (int)type.Value;
            return filters;
        }
    }
}
