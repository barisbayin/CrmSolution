using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DataAccess
{
    public interface IQuery<T>
    {
        IQueryable<T> Query();
    }
}
