using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using System;
using System.Linq;

namespace GroupManagerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CodeNameController : ControllerBase
    {
        private readonly ILogger<CodeNameController> _logger;


        public CodeNameController(ILogger<CodeNameController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        [RequiredScope("read")]
        [HttpGet]
        public ActionResult<string> Get()
        {
            var userName = User.Identity.Name;

            if (string.IsNullOrWhiteSpace(userName))
            {
                return Unauthorized();
            }

            return Ok(new string(userName.Reverse().ToArray()));
        }
    }
}
