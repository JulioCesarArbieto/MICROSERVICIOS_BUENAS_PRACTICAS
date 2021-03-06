﻿using CONTINER.API.MANAGER.Cross.RabbitMQ.RabbitMQ.Commands;

namespace CONTINER.API.MANAGER.Withdrawal.RabbitMQ.Commands
{
    public class WithdrawalCommand : Command
    {
        public int IdTransaction { get; protected set; }
        public decimal Amount { get; protected set; }
        public string Type { get; protected set; }
        public string CreationDate { get; protected set; }
        public int AccountId { get; protected set; }
    }
}
