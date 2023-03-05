using Microsoft.AspNetCore.Mvc;
using SimpleEmailApp.Models;

namespace SimpleEmailApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailDto request)
        {
            for (int i = 0; i < 10; i++)
            {
                await _emailService.SendEmail(request);
            }

            return Ok();
        }
    }
}


