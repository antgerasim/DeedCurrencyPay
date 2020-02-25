using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeedCurrencyPay.API.Infrastructure;
using DeedCurrencyPay.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DeedCurrencyPay.API.Controllers.V1
{
    //[Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            //this.config = config;
            this.accountService = accountService;
        }

        [HttpGet(ApiRoutes.Account.AccountInfo)]
        public IActionResult AccountInfo([FromRoute]long userId)
        {
            var response = accountService.GetAccountInfo(userId);
           
            return Ok(response);
        }

        [HttpGet(ApiRoutes.Account.Deposit)]
        public IActionResult Deposit([FromRoute]long userId, decimal amount)
        {
            var response = accountService.Deposit(userId, amount);

            return Ok(response);
        }

        [HttpGet(ApiRoutes.Account.Withdraw)]
        public IActionResult Withdraw([FromRoute]long userId, decimal amount)
        {
            var response = accountService.Withdraw(userId, amount);

            return Ok(response);
        }

        [HttpGet(ApiRoutes.Account.Convert)]
        public IActionResult ConvertToCurruncy([FromRoute]long userId, string targetCurrency)
        {
            var response = accountService.ConvertToCurrency(userId, targetCurrency.ToUpper());

            return Ok(response);
        }
    }
}
