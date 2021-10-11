using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkExtension.Extensions
{
    public class PagedList<T> where T: class
    {
        public PagedList()
        {
            Data = new List<T>();
        }

        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
