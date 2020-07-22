﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zora.Web.Controllers;
using Zora.Web.Models.Payments.BindingModels;
using Zora.Web.Models.Payments.ViewModels;
using Zora.Web.Models.Students.BindingModels;
using Zora.Web.Services.OutstandingPayments;
using Zora.Web.Services.Payments;
using Zora.Web.Services.Statistics;
using Zora.Web.Services.Students;


namespace Zora.Web.Areas.Admin.Controllers
{
    //todo move to new coltrollers
    public class UserManagementController : AdministrationController
    {

        private readonly IStudentsService _students;
        private readonly IPaymentsService _payments;
        private readonly IOutstandingPaymentsService _outStandingPayments;
        private readonly IStatisticsService _statistics;

        public UserManagementController(

            IStudentsService students,
            IPaymentsService payments,
            IOutstandingPaymentsService outStandingPayments,
            IStatisticsService statistics
            )
        {

            _students = students;
            _payments = payments;
            _outStandingPayments = outStandingPayments;
            _statistics = statistics;
        }


        public async Task<ActionResult> Index()
        {
            return View(await _students.Students());
        }


        public async Task<ActionResult> Payments(int id)
        {
            ViewData["User"] = id;
            return View(await _payments.Payments(id));
        }


        public async Task<ActionResult> OutStandingPayments()
        {
            double Amount = await _outStandingPayments.OutstandingPayments();
            var model = new OutStandingPaymentsViewModel() { Amount = Amount };

            return View(model);
        }


        public ActionResult AddPayment(int id)
        {

            return View(new PaymentsBindingModel()
            {
                StudentId = id
            });


        }


        [HttpPost]
        public async Task<ActionResult> AddPayment(string id, PaymentsBindingModel model)
        {
            var result = await _payments.AddPayment(model);

            if (result == 0)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> DeletePayment(int id)
        {
            var result = await _payments.DeletePayment(id);

            return RedirectToAction(nameof(Index));

        }


        public async Task<ActionResult> Pay(int id)
        {

            var result = await _payments.Pay(id);

            return RedirectToAction(nameof(Index));


        }


        public ActionResult AddStudent()
        {

            return View();

        }


        [HttpPost]
        public async Task<ActionResult> AddStudent(StudentBindingModel model)
        {
            var result = await _students.AddStudent(model);

            if (result == 0)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> DeleteStudent(int id)
        {
            var result = await _students.DeleteStudent(id);

            return RedirectToAction(nameof(Index));

            //todo delete payments for student
        }


        public async Task<ActionResult> Statistics()
        {
            var model = await _statistics.Totals();

            return View(model);
        }
    }
}