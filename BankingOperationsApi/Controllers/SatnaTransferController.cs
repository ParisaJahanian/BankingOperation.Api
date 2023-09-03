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
        /// <summary>
        ///  وب سرویس احراز هویت
        /// </summary>
        /// <param name="loginReqDTO"></param>
        /// <returns></returns>
        /// <exception cref="RamzNegarException"></exception>
        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FaraboomLoginOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(FaraboomLoginOutput))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(FaraboomLoginOutput))]
        public async Task<ActionResult<FaraboomLoginOutput>> SatnaTransferLogin(BasePublicLogData basePublicLog)
        {
            var result = await _carElectronicTollService.LoginAsync(basePublicLog);
            try
            {
                if (result.StatusCode != "OK")
                {
                    _logger.LogError($"{nameof(SatnaTransferLogin)} not-success request - input \r\n response:{result.StatusCode}-{result.Content}");
                    return BadRequest(_baseLog.ApiResponeFailByCodeProvider<BasePublicLogData>(result.Content, result.StatusCode, result.RequestId, loginReqDTO?.PublicLogData?.PublicReqId));
                }
                return Ok(_baseLog.ApiResponseSuccessByCodeProvider<FaraboomLoginOutput>(result?.Content, result.StatusCode, result?.RequestId, loginReqDTO?.PublicLogData?.PublicReqId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred while {nameof(SatnaTransferLogin)}");
                throw new RamzNegarException(ErrorCode.InternalError, $"Exception occurred while: {nameof(SatnaTransferLogin)} => {ex.Message}");
            }
        }



    }
}
