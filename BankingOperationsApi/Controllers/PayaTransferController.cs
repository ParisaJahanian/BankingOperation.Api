using BankingOperationsApi.Filters;
using BankingOperationsApi.Infrastructure;
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
        public PayaTransferController(IConfiguration configuration, ILogger<PayaTransferController> logger, BaseLog baseLog)
        {
            _configuration = configuration;
            _logger = logger;
            _baseLog = baseLog;
        }
    }
}
