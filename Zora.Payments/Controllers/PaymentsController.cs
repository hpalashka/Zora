using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zora.Payments.Data.Models;
using Zora.Payments.Models.Payments.BindingModels;
using Zora.Payments.Models.Payments.ViewModels;
using Zora.Payments.Services;
using Zora.Shared.Controllers;
using Zora.Shared.Data.Models;
using Zora.Shared.Infrastructure;
using Zora.Shared.Messages.Payments;
using Zora.Shared.Services;

namespace Zora.Payments.Controllers
{
    [AuthorizeAdministrator]
    public class PaymentsController : ApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly IBus _publisher;

        public PaymentsController(IPaymentService paymentService, IBus publisher)
        {
            _paymentService = paymentService;
            _publisher = publisher;
        }


        [HttpPost]
        public async Task<int> AddPayment(PaymentsBindingModel payment)
        {

            Payment newPayment = new Payment()
            {
                Amount = payment.Amount,
                CreatedDate = DateTime.Now,
                DueDate = payment.DueDate,
                Title = payment.Title,
                StudentId = payment.StudentId,
                Paid = false
            };


            var messageData = new PaymentAddedMessage
            {
                Amount = payment.Amount
            };

            var message = new Message(messageData);

            await _paymentService.Save(newPayment, message);

            await _publisher.Publish(messageData);

            await _paymentService.MarkMessageAsPublished(message.Id);

            return newPayment.Id;
        }


        [HttpGet]
        [Route(Id)]
        public IEnumerable<PaymentsViewModel> Payments(int id)
        {
            return _paymentService.Payments(id);
        }



        [HttpGet]
        public IEnumerable<PaymentsViewModel> Payments()
        {
            return _paymentService.Payments();
        }


        //[HttpPut]
        //[Route(Id)]
        //public async Task<bool> EditPayment(int id, PaymentsBindingModel payment)
        //{

        //    Payment paymentToUpdate = await _paymentService.FindPayment(id);

        //    paymentToUpdate.Amount = payment.Amount;
        //    paymentToUpdate.DueDate = payment.DueDate;
        //    paymentToUpdate.Title = payment.Title;
        //    paymentToUpdate.UserId = payment.UserId;

        //    var messageData = new PaymentUpdatedMessage
        //    {
        //        NewAmount = payment.Amount,
        //        OldAmount = paymentToUpdate.Amount
        //    };

        //    var message = new Message(messageData);

        //    await _paymentService.Save(paymentToUpdate, message);

        //    await _publisher.Publish(messageData);

        //    await _paymentService.MarkMessageAsPublished(message.Id);

        //    return Result.Success;
        //}


        [HttpDelete]
        [Authorize]
        [Route(Id)]
        public async Task<bool> DeletePayment(int id)
        {

            return await _paymentService.DeletePayment(id);
        }


        [HttpPut]
        [Authorize]
        [Route(Id)]
        public async Task<bool> Pay(int id)
        {
            Payment paymentToUpdate = await _paymentService.FindPayment(id);

            paymentToUpdate.Paid = true;

            var messageData = new PaymentPaiddMessage
            {
                Amount = paymentToUpdate.Amount
            };

            var message = new Message(messageData);

            await _paymentService.Save(paymentToUpdate, message);

            await _publisher.Publish(messageData);

            await _paymentService.MarkMessageAsPublished(message.Id);

            //  await _paymentService.Save(paymentToUpdate);

            return Result.Success;
        }



    }
}
