using System;
using CONTINER.API.MANAGER.Withdrawal.DTO;
using CONTINER.API.MANAGER.Withdrawal.Service;
using Microsoft.AspNetCore.Mvc;

namespace CONTINER.API.MANAGER.Withdrawal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IServiceTransaction _services;
        public TransactionController(IServiceTransaction services)
        {
            _services = services;
        }


        [HttpPost("Withdrawal")]
        public IActionResult Withdrawal([FromBody] TransactionRequest request)
        {
            Model.Transaction transaction = new Model.Transaction()
            {
                AccountId = request.AccountId,
                Amount = request.Amount,
                CreationDate = DateTime.Now.ToShortDateString(),
                Type = "Withdrawal"
            };
            _services.Withdrawal(transaction);
            return Ok();
        }
    }
}
