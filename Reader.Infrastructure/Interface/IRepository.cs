using System;
using System.Collections;
using System.Collections.Generic;

namespace Reader.Infrastructure
{
    public interface IRepository<T>
    {
        IUnitOfWork UnitOfWork { get; }       
    }
}
