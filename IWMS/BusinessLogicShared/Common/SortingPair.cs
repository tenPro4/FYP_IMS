using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicShared.Common
{
    public class SortingPair
    {
        public bool IsAscending { get; set; }
        public string OrderBy { get; set; }
        public SortingPair(string orderBy, bool isAscending)
        {
            OrderBy = orderBy;
            IsAscending = isAscending;
        }
    }
}
