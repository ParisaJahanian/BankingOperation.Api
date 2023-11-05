using BankingOperationsApi.ErrorHandling;
using BankingOperationsApi.Exceptions;
using BankingOperationsApi.Filters;
using BankingOperationsApi.Infrastructure;
using BankingOperationsApi.Models;
using BankingOperationsApi.Services.SatnaTransfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace BankingOperationsApi.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("Faraboom/v1/[controller]")]
    [ApiExplorerSettings]
    [ApiResultFilterAttribute]
    public class SatnaTransferController : ControllerBase
    {
        private ISatnaTransferService _satnaTransferService { get; }
        private ILogger<SatnaTransferController> _logger { get; }
        private BaseLog _baseLog { get; }
        public SatnaTransferController(ISatnaTransferService satnaTransferService,
            ILogger<SatnaTransferController> logger, BaseLog baseLog)
        {
            _satnaTransferService = satnaTransferService;
            _logger = logger;
            _baseLog = baseLog;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(TokenOutput))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(TokenOutput))]
        public async Task<ActionResult<TokenOutput>> SatnaTransferLogin(BasePublicLogData basePublicLog)
        {
            var result = await _satnaTransferService.GetTokenAsync(basePublicLog);
            try
            {
                if (result.StatusCode != "OK")
                {
                    _logger.LogError($"{nameof(SatnaTransferLogin)} not-success request - input \r\n response:{result.StatusCode}-{result.Content}");
                    return BadRequest(_baseLog.ApiResponeFailByCodeProvider<BasePublicLogData>(result.Content, result.StatusCode, result.RequestId, basePublicLog?.PublicLogData?.PublicReqId));
                }
                
                return Ok(_baseLog.ApiResponseSuccessByCodeProvider<TokenOutput>(result?.Content, result.StatusCode, result?.RequestId, basePublicLog?.PublicLogData?.PublicReqId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred while {nameof(SatnaTransferLogin)}");
                throw new RamzNegarException(ErrorCode.InternalError, $"Exception occurred while: {nameof(SatnaTransferLogin)} => {ex.Message}");
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SatnaTransferRes))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(SatnaTransferRes))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(SatnaTransferRes))]
        public async Task<ActionResult<SatnaTransferRes>> SatnaTransfer(SatnaTransferReqDTO transferReqDTO)
        {
            var result = await _satnaTransferService.SatnaTransferAsync(transferReqDTO);
            try
            {
                if (result.StatusCode != "OK")
                {
                    _logger.LogError($"{nameof(SatnaTransfer)} not-success request - input \r\n response:{result.StatusCode}-{result.Content}");
                    return BadRequest(_baseLog.ApiResponeFailByCodeProvider<SatnaTransferReqDTO>(result.Content, result.StatusCode, result.RequestId, transferReqDTO?.PublicLogData?.PublicReqId));
                }
                return Ok(_baseLog.ApiResponseSuccessByCodeProvider<SatnaTransferResDTO>(result?.Content, result.StatusCode, result?.RequestId, transferReqDTO?.PublicLogData?.PublicReqId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred while {nameof(SatnaTransfer)}");
                throw new RamzNegarException(ErrorCode.InternalError, $"Exception occurred while: {nameof(SatnaTransfer)} => {ErrorCode.FaraboomTransferApiError.GetDisplayName()}");
            }
        }


    }
}
