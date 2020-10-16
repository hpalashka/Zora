using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zora.Payments.Application.Commands.Create;
using Zora.Payments.Application.Commands.Delete;
using Zora.Payments.Application.Queries.Payments;
using Zora.Payments.Application.Queries.PaymentsForUser;
using Zora.Payments.Application.Quieries.Common;
using Zora.Payments.Domain.Models;
using Zora.Payments.Domain.Repositories;
using Zora.Payments.Web.Services;
using Zora.Shared.Domain.Models;
using Zora.Shared.Infrastructure;
using Zora.Shared.Messages.Payments;
using Zora.Shared.Web.Controllers;

namespace Zora.Payments.Web.Features
{
    [AuthorizeAdministrator]
    public class PaymentsController : ApiController
    {
        private readonly IBus _publisher;
        private readonly IPaymentService _paymentService;
        private readonly IPaymentDomainRepository _paymentRepository;


        public PaymentsController(IBus publisher, IPaymentService paymentService, IPaymentDomainRepository paymentRepository)
        {
            _publisher = publisher;
            _paymentService = paymentService;
            _paymentRepository = paymentRepository;
        }

        [HttpPost]
        public async Task<ActionResult<CreatePaymentOutputModel>> Create(CreatePaymentCommand command)
        {

            var messageData = new PaymentAddedMessage
            {
                Amount = command.Amount
            };

            var message = new Message(messageData);

            command.MessageData = message;

            var result = await Send(command);

            await _publisher.Publish(messageData);

            await _paymentService.MarkMessageAsPublished(message.Id);

            return result;
        }


        [HttpGet]
        [Route(Id)]
        [AllowAnonymous]
        public async Task<ActionResult<IList<PaymentsViewModel>>> Payments(int id)
        {
            PaymentsForUserQuery command = new PaymentsForUserQuery();
            command.Id = id;
            return await Send(command);
        }


        [HttpGet]
        public async Task<ActionResult<IList<PaymentsViewModel>>> Payments()
        {
            PaymentsQuery command = new PaymentsQuery();
            return await Send(command);
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
        public async Task<ActionResult<bool>> DeletePayment(int id)
        {
            DeletePaymentCommand command = new DeletePaymentCommand();
            command.Id = id;
            return await Send(command);
        }

        [HttpPut]
        [Authorize]
        [Route(Id)]
        public async Task<ActionResult<bool>> Pay(int id)
        {

            PayPaymentCommand command = new PayPaymentCommand();
            command.Id = id;

            Payment payment = await _paymentRepository.FindPayment(command.Id);//todo

            var messageData = new PaymentPaiddMessage
            {
                Amount = payment.Amount
            };

            var message = new Message(messageData);

            command.MessageData = message;

            var result = await Send(command);

            await _publisher.Publish(messageData);

            await _paymentService.MarkMessageAsPublished(message.Id);

            return result;

        }
    }
}

