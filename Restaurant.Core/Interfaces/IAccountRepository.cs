using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Core.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account GetByUsername(string username);

    }
}
