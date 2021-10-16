using Data.DataModels;
using System;

namespace Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        CFContext Init();
    }
}