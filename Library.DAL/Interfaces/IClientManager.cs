using Library.Entities.IdentityEntities;
using System;

namespace Library.DAL.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
    }
}
