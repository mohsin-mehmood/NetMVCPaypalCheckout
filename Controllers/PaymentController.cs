using Microsoft.AspNetCore.Mvc;
using PaypalDemo.Models;
using PaypalDemo.Paypal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaypalDemo.Controllers
{
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly PaypalService _paypalService;

        public PaymentController(PaypalService paypalService)
        {
            _paypalService = paypalService;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        [Route("createorder")]
        public async Task<IActionResult> CreateOrder(dynamic body)
        {
            var invoiceId = Guid.NewGuid().ToString();
            var amount = new CreateOrderModel.AmountModel
            {
                CurrencyCode = "GBP",
                Value = "0.01"
            };

            var createOrderModel = new CreateOrderModel(Intents.CAPTURE)
            {
                PurchaseUnits = new List<CreateOrderModel.PurchaseUnitsModel>
                {
                      new CreateOrderModel.PurchaseUnitsModel(amount)
                      {
                           InvoiceId = invoiceId,
                           Description = "Subscription Fee"
                      }
                }
            };

            var response = await _paypalService.CreateOrder<CreateOrderResponse>(createOrderModel);
            return Ok(response);
        }


        [HttpPost]
        [Route("capture-paypal-order")]
        public async Task<IActionResult> CaptureOrder([FromBody] CaptureOrderRequest request)
        {
            var response = await _paypalService.CaptureOrder<CaptureOrderResponse>(request.OrderId);

            return Ok(response);
        }
    }
}
