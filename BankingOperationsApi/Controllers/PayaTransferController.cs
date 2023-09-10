using BankingOperationsApi.ErrorHandling;
using BankingOperationsApi.Exceptions;
using BankingOperationsApi.Filters;
using BankingOperationsApi.Infrastructure;
using BankingOperationsApi.Infrastructure.Extension;
using BankingOperationsApi.Models;
using BankingOperationsApi.Services.PayaTransfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

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
        public async Task<ActionResult<TokenOutput>> PayaTransferLogin(BasePublicLogData basePublicLog)
        {
            var result = await _payaTransferService.GetTokenAsync(basePublicLog);
            try
            {
                if (result.StatusCode != "OK")
                {
                    _logger.LogError($"{nameof(PayaTransferLogin)} not-success request - input \r\n response:{result.StatusCode}-{result.Content}");
                    return BadRequest(_baseLog.ApiResponeFailByCodeProvider<BasePublicLogData>(result.Content, result.StatusCode, result.RequestId, basePublicLog?.PublicLogData?.PublicReqId));
                }

                return Ok(_baseLog.ApiResponseSuccessByCodeProvider<TokenOutput>(result?.Content, result.StatusCode, result?.RequestId, basePublicLog?.PublicLogData?.PublicReqId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred while {nameof(PayaTransferLogin)}");
                throw new RamzNegarException(ErrorCode.InternalError, $"Exception occurred while: " +
                    $"{nameof(PayaTransferLogin)} => {ErrorCode.InternalError.GetDisplayName()}");
            }
        }

        [AllowAnonymous]
        [HttpPost("Tranfer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PayaTransferRes))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PayaTransferRes))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(PayaTransferRes))]
        public async Task<ActionResult<PayaTransferRes>> PayaTransfer(PayaTransferReqDTO transferReqDTO)
        {
            var result = await _payaTransferService.PayaTransferAsync(transferReqDTO);
            try
            {
                if (result.StatusCode != "OK")
                {
                    _logger.LogError($"{nameof(PayaTransfer)} not-success request - input \r\n response:{result.StatusCode}-{result.Content}");
                    return BadRequest(_baseLog.ApiResponeFailByCodeProvider<SatnaTransferReqDTO>(result.Content, result.StatusCode, result.RequestId, transferReqDTO?.PublicLogData?.PublicReqId));
                }
                return Ok(_baseLog.ApiResponseSuccessByCodeProvider<SatnaTransferRes>(result?.Content, result.StatusCode, result?.RequestId, transferReqDTO?.PublicLogData?.PublicReqId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred while {nameof(PayaTransfer)}");
                throw new RamzNegarException(ErrorCode.InternalError, $"Exception occurred while: {nameof(PayaTransfer)} => {ErrorCode.SatnaTransferApiError.GetDisplayName()}");
                //return ServiceHelperExtension.GenerateErrorMethodResponse<PayaTransferRes>(ErrorCode.InternalError);
            }
        }


    }
}
