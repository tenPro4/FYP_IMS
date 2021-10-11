using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework.Entities.Base
{
    public interface IEntityBase<TId>
    {
        TId Id { get; }
    }
}
