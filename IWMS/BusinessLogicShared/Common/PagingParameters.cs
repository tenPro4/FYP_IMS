using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicShared.Common
{
    public class PagingParameters
    {
        public PagingParameters()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }

        public PagingParameters(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new Exception("The page number must be greater than 0");

            if (pageSize < 1)
                throw new Exception("The page size must be greater than 0");

            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }

        public int Skip
        {
            get
            {
                return (PageNumber - 1) * PageSize;
            }
        }
    }
}
