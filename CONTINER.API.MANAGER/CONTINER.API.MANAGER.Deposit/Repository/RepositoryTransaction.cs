using CONTINER.API.MANAGER.Deposit.Model;
using CONTINER.API.MANAGER.Deposit.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CONTINER.API.MANAGER.Deposit.Repository
{
    public class RepositoryTransaction : IRepositoryTransaction
    {
        private readonly IContextDatabase _context;
        public RepositoryTransaction(IContextDatabase context)
        {
            _context = context;
        }

        public Transaction Deposit(Transaction transaction)
        {
            _context.Transaction.Add(transaction);
            _context.SaveChanges();
            return transaction;
        }
    }
}
