using System.Collections.Generic;
using Alfred.Dal.Entities.Base;

namespace Alfred.Dal.Implementation.Fake
{
    public static class ArtifactResponseLinks
    {
        public static IList<Link> AddFirstPage(this IList<Link> links, int artifactCount)
        {
            links.Add
            (
                new Link
                {
                    Rel = "NextPage",
                    Href = 1
                }
            );
            return links;
        }

        public static IList<Link> AddLastPage(this IList<Link> links, int artifactCount, int pageSize)
        {
            links.Add
            (
                new Link
                {
                    Rel = "LastPage",
                    Href = (artifactCount + pageSize - 1) / pageSize
                }
            );
            return links;
        }

        public static IList<Link> AddNextPage(this IList<Link> links, int artifactCount, int pageSize, int page)
        {
            links.Add
            (
                new Link
                {
                    Rel = "NextPage",
                    Href = (artifactCount + pageSize - 1) / pageSize
                }
            );
            return links;
        }

        public static IList<Link> AddPreviousPage(this IList<Link> links, int page)
        {
            links.Add
            (
                new Link
                {
                    Rel = "PreviousPage",
                    Href = page - 1
                }
            );
            return links;
        }
    }
}
