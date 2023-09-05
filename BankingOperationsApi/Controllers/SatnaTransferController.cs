using BankingOperationsApi.ErrorHandling;
using BankingOperationsApi.Exceptions;
using BankingOperationsApi.Filters;
using BankingOperationsApi.Infrastructure;
using BankingOperationsApi.Models;
using BankingOperationsApi.Services.SatnaTransfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingOperationsApi.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("Bill/v1/[controller]")]
    [ApiExplorerSettings]
    [ApiResultFilterAttribute]
    public class SatnaTransferController : ControllerBase
    {
        private ISatnaTransferService _satnaTransferService { get; }
        private IConfiguration _configuration { get; }
        private ILogger<SatnaTransferController> _logger { get; }
        private BaseLog _baseLog { get; }
        public SatnaTransferController(ISatnaTransferService satnaTransferService, IConfiguration configuration,
            ILogger<SatnaTransferController> logger, BaseLog baseLog)
        {
            _satnaTransferService = satnaTransferService;
            _configuration = configuration;
            _logger = logger;
            _baseLog = baseLog;
        }

        [AllowAnonymous]
        [HttpPost("Token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenRes))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(TokenRes))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(TokenRes))]
        public async Task<ActionResult<TokenRes>> SatnaTransferLogin(BasePublicLogData basePublicLog)
        {
            var result = await _satnaTransferService.GetTokenAsync(basePublicLog);
            try
            {
                if (result.StatusCode != "OK")
                {
                    _logger.LogError($"{nameof(SatnaTransferLogin)} not-success request - input \r\n response:{result.StatusCode}-{result.Content}");
                    return BadRequest(_baseLog.ApiResponeFailByCodeProvider<BasePublicLogData>(result.Content, result.StatusCode, result.RequestId, basePublicLog?.PublicLogData?.PublicReqId));
                }
                return Ok(_baseLog.ApiResponseSuccessByCodeProvider<TokenRes>(result?.Content, result.StatusCode, result?.RequestId, basePublicLog?.PublicLogData?.PublicReqId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred while {nameof(SatnaTransferLogin)}");
                throw new RamzNegarException(ErrorCode.InternalError, $"Exception occurred while: {nameof(SatnaTransferLogin)} => {ex.Message}");
            }
        }



    }
}
