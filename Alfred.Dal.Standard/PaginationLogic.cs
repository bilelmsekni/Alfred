using System.Collections.Generic;
using System.Linq;
using Alfred.Dal.Standard.Entities.Base;

namespace Alfred.Dal.Standard
{
    public static class PaginationLogic
    {
        public static IList<Link> AddFirstPage(this IList<Link> links, int artifactCount)
        {
            if (artifactCount > 0)
            {
                links.Add
                    (new Link
                    {
                        Rel = "firstPage",
                        Href = 1
                    });
            }
            return links;
        }

        public static IList<Link> AddLastPage(this IList<Link> links, int artifactCount, int pageSize)
        {
            if (artifactCount > 0)
            {
                links.Add
                    (
                        new Link
                        {
                            Rel = "lastPage",
                            Href = (artifactCount + pageSize - 1) / pageSize
                        }
                    );
            }
            return links;
        }

        public static IList<Link> AddNextPage(this IList<Link> links, int artifactCount, int pageSize, int page)
        {
            if (page * pageSize <= artifactCount - 1)
            {
                links.Add
                    (new Link
                    {
                        Rel = "nextPage",
                        Href = page + 1
                    }
                    );
            }
            return links;
        }

        public static IList<Link> AddPreviousPage(this IList<Link> links, int resultCount, int page)
        {
            if (resultCount > 0 && page > 1)
            {
                links.Add
                    (new Link
                    {
                        Rel = "previousPage",
                        Href = page - 1
                    }
                    );
            }
            return links;
        }

        public static IEnumerable<T> Paginate<T>(this IEnumerable<T>results, int page, int pageSize)
        {
            return results
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }

        public static bool IsPageInRange(this int page, int pageSize, int resultsCount)
        {
            return page <= (resultsCount + pageSize - 1) / pageSize;
        }
    }
}
