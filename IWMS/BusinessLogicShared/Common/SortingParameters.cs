using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicShared.Common
{
    public class SortingParameters
    {
        public List<SortingPair> Sorters { get; set; }

        public SortingParameters()
        {
            this.Sorters = new List<SortingPair>();
        }

        public void Add(string orderBy, bool isAscending)
        {
            Sorters.Add(new SortingPair(orderBy, isAscending));
        }

        public void Add(SortingPair sortingPair)
        {
            Sorters.Add(sortingPair);
        }
    }
}
