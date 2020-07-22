using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Zora.EmailService;
using Zora.Web.Data;
using Zora.Web.Models.Contact.BindingModels;

namespace Zora.Web.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ZoraDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;


        public RegistrationController(ZoraDbContext context, IConfiguration configuration, IEmailSender emailSender)
        {
            _context = context;
            _configuration = configuration;
            _emailSender = emailSender;
        }




        public IActionResult Contact()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Contact(ContactBindingModel model)
        {
            if (ModelState.IsValid)
            {
                //to do security check
                var attachment = new FormFileCollection();
                if (model.UploadImage != null)
                {
                    attachment.Add(model.UploadImage);
                }

                string content = $"<p><strong>Unregistered User</strong></p><p><strong> Full Name:</strong> {model.FullName} </p>" +
                $"<p><strong>Phone: </strong>{model.Phone}</p><p><p><strong>Email:</strong>{model.Email}</p><p><strong> Message:</strong ></p><p>{model.Message}</p>";
                var message = new MailMessage(new string[] { model.Email }, //to do change email
                    "Photo Album Contact Form",
                  content,
                 attachment);
                _emailSender.SendEmail(message);

                return RedirectToAction(nameof(ContactSent));
            }
            return View(model);
        }


        public IActionResult ContactSent()
        {
            return View();
        }


    }
}
