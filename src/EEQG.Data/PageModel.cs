using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EEQG.Data
{
    public class PageModel<T> where T : class
    {
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int PageIndex { get; set; }
        public int ItemTotalCount { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
    public static class PageModelExtensions
    {
        public static PageModel<T> Page<T>(this IQueryable<T> query, int index = 0, int pSize = 20) where T : class
        {
            PageModel<T> model = new PageModel<T>();
            model.ItemTotalCount = query.Count();
            model.PageIndex = index;
            model.PageSize = pSize;
            model.PageCount = (int)Math.Ceiling(model.ItemTotalCount * 1.0 / pSize);

            if (index == 0)
            {
                model.Items = query.Take(pSize).ToList();
            }
            else
            {
                model.Items = query.Skip(index * pSize).Take(pSize).ToList();
            }

            return model;
        }
    }
}
