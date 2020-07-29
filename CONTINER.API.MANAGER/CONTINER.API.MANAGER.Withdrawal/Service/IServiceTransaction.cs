using CONTINER.API.MANAGER.Withdrawal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CONTINER.API.MANAGER.Withdrawal.Service
{
    public interface IServiceTransaction
    {
        Transaction Withdrawal(Transaction transaction);
    }
}
