using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicShared.Constants
{
    public enum TaskType
    {
        Enhancement =1,
        Bug =2,
        Design = 3,
        Review = 4,
    }

    public enum TaskPriority
    {
        HIGH = 1,
        MEDIUM = 2,
        LOW = 3,
    }


    //String oemString = Enum.GetName(typeof(GroupTypes), GroupTypes.OEM);

    //MyEnum e = (MyEnum)3;

    //if (Enum.IsDefined(typeof(MyEnum), 3)) { ... }
}
