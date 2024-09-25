using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FirstApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOptions<AttachmentsOptions> _attachments;//Singleton
        //private readonly IOptionsMonitor<AttachmentsOptions> _attachments;//Singleton
        //private readonly IOptionsSnapshot<AttachmentsOptions> _attachments;//Scoped

        public ConfigController(IConfiguration configuration,IOptions<AttachmentsOptions> attachments)
        {
            _configuration = configuration;
            _attachments = attachments;
        }
        [HttpGet]
        [Route("")]
        public ActionResult GetConfig() {
            var config = new
            {
                AllowedHosts = _configuration["AllowedHosts"],
                Logging = _configuration["Logging:LogLevel:Default"],
                DefaultConnection = _configuration["ConnectionStrings:DefaultConnection"],
                ConnectionStrings = _configuration.GetConnectionString("DefaultConnection"),
                TestKey = _configuration["TestKey"],
                attachmentsOptions= _attachments.Value,
            };
            return Ok(config);

        }
    }
}
