using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using SimpleEmailApp.Models;
using System.Net.Mail;

namespace SimpleEmailApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;
        public EmailController(IEmailService emailService, IRazorViewToStringRenderer razorViewToStringRenderer)
        {
            _emailService = emailService;
            _razorViewToStringRenderer = razorViewToStringRenderer;
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

        [HttpPost("orders")]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderViewModel orderViewModel)
        {
            // Process the order and create an OrderConfirmationViewModel
            var orderConfirmationViewModel = new OrderConfirmationViewModel
            {
                FirstName = orderViewModel.FirstName,
                LastName = orderViewModel.LastName,
                OrderNumber = "1234",
                OrderDate = DateTime.UtcNow,
                OrderTotal = 100.00m,
                ShippingAddress = orderViewModel.ShippingAddress,
                BillingAddress = orderViewModel.BillingAddress,
                OrderItems = new List<OrderItemViewModel>
                {
                    new OrderItemViewModel { ProductName = "Product 1", Price = 50.00m, Quantity = 2, TotalPrice = 100.00m }
                }
            };

            // Render the email template
            var emailTemplate = await _razorViewToStringRenderer.RenderViewToStringAsync("OrderConfirmation", orderConfirmationViewModel);

            // Send the email
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Your Company Name", "noreply@yourcompany.com"));
            //message.To.Add(new MailboxAddress(orderViewModel.Email, $"{orderViewModel.FirstName} {orderViewModel.LastName}"));
            message.Subject = "Order Confirmation";
            message.Body = new TextPart("html")
            {
                Text = emailTemplate
            };

            //using var smtpClient = new SmtpClient();
            //await smtpClient.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            //await smtpClient.AuthenticateAsync("your_email@gmail.com", "your_email_password");
            //await smtpClient.SendAsync(message);
            //await smtpClient.DisconnectAsync(true);

            return Ok();
        }

    }
}


