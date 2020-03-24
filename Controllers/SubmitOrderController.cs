using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace kfz_configurator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubmitOrderController : ControllerBase
    {
        private readonly ILogger<SubmitOrderController> _logger;

        public SubmitOrderController(ILogger<SubmitOrderController> logger)
        {
            _logger = logger;
        }

        public struct Result
        {
            public int OrderId { get; set; }
        }
        public struct OrderRequest
        {
            public int[] OptionIds { get; set; }
            public decimal TotalPrice { get; set; }
        }

        [HttpPost]
        public Result Submit(OrderRequest request)
        {
            using (var reader = Database.Instance.Execute(
                "INSERT INTO orders (option_id_list, total_price) VALUES (@p1, @p2) RETURNING order_id",
                ("p1", string.Join(',', request.OptionIds)), ("p2", request.TotalPrice)))
            {
                if (reader.Read())
                    return new Result { OrderId = (int)reader[0] };
            }
            throw new Exception("empty result from INSERT into orders");
        }
    }
}