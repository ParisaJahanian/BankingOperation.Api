﻿using BankingOperationsApi.ErrorHandling;
using BankingOperationsApi.Exceptions;
using BankingOperationsApi.Filters;
using BankingOperationsApi.Infrastructure;
using BankingOperationsApi.Models;
using BankingOperationsApi.Services.PayaTransfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingOperationsApi.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("Bill/v1/[controller]")]
    [ApiExplorerSettings]
    [ApiResultFilterAttribute]
    public class PayaTransferController : ControllerBase
    {
        private IConfiguration _configuration { get; }
        private ILogger<PayaTransferController> _logger { get; }
        private BaseLog _baseLog { get; }
        private IPayaTransferService _payaTransferService { get; }
        public PayaTransferController(IConfiguration configuration, ILogger<PayaTransferController> logger,
            BaseLog baseLog, IPayaTransferService payaTransferService)
        {
            _configuration = configuration;
            _logger = logger;
            _baseLog = baseLog;
            _payaTransferService = payaTransferService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(TokenOutput))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(TokenOutput))]
        public async Task<ActionResult<TokenOutput>> PayaTransferLogin(TokenInput tokenInput)
        {
            //var result = await _payaTransferService.(tokenInput);
            //try
            //{
            //    if (result.StatusCode != "OK")
            //    {
            //        _logger.LogError($"{nameof(PayaTransferLogin)} not-success request - input \r\n response:{result.StatusCode}-{result.Content}");
            //        return BadRequest(_baseLog.ApiResponeFailByCodeProvider<tokenInput>(result.Content, result.StatusCode, result.RequestId, loginReqDTO?.PublicLogData?.PublicReqId));
            //    }
            //    return Ok(_baseLog.ApiResponseSuccessByCodeProvider<TokenOutput>(result?.Content, result.StatusCode, result?.RequestId, loginReqDTO?.PublicLogData?.PublicReqId));
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, $"Exception occurred while {nameof(PayaTransferLogin)}");
            //    throw new RamzNegarException(ErrorCode.InternalError, $"Exception occurred while: {nameof(PayaTransferLogin)} => {ex.Message}");
            //}
            return BadRequest();
        }

    }
}
