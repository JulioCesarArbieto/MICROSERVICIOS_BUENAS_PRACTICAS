using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CONTINER.API.MANAGER.Cross.RabbitMQ.RabbitMQ.Bus;
using CONTINER.API.MANAGER.Deposit.DTO;
using CONTINER.API.MANAGER.Deposit.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CONTINER.API.MANAGER.Deposit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IServiceTransaction _services;
        private readonly IEventBus _bus;

        public TransactionController(IServiceTransaction services)
        {
            _services = services;
        }

        [HttpPost("Deposit")]
        public IActionResult Deposit([FromBody] TransactionRequest request)
        {
            Model.Transaction transaction = new Model.Transaction()
            {
                AccountId = request.AccountId,
                Amount = request.Amount,
                CreationDate = DateTime.Now.ToShortDateString(),
                Type = "Deposit"
            };
            _services.Deposit(transaction);
            return Ok();
        }
    }
}
