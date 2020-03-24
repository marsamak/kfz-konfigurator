using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace kfz_configurator.Controllers
{
    [ApiController]
    [Route("[controller]/{orderId}")]
    public class ViewOrderController : ControllerBase
    {
        private readonly ILogger<ViewOrderController> _logger;

        public ViewOrderController(ILogger<ViewOrderController> logger)
        {
            _logger = logger;
        }

        public struct ViewOrderResult
        {
            public int OrderID { get; set; }
            public Option[] OrderedOptions { get; set; }
            public decimal TotalPrice { get; set; }
        }

        [HttpGet]
        public ViewOrderResult Get(int orderId)
        {
            ViewOrderResult result = new ViewOrderResult { OrderID = orderId };
            String options = "";
            using (var reader = Database.Instance.Execute(
                "SELECT option_id_list, total_price FROM orders WHERE order_id = @order_id", ("order_id", result.OrderID)))
            {

                if (!reader.Read())
                {
                    throw new Exception("Order not found!");
                }
                result.TotalPrice = (decimal)reader[1];
                options = reader[0] as string;
            }
            HashSet<int> orderedIds = new HashSet<int>();
            foreach (string opt in options.Split(',', ';'))
                orderedIds.Add(int.Parse(opt));

            List<Option> orderedOptionsList = new List<Option>();
            using (var reader = Database.Instance.Execute(
                           "SELECT option_id, group_name, name, price, description FROM options"
                       ))
            {
                while (reader.Read())
                {
                    int id = (int)reader[0];

                    if (orderedIds.Contains(id))
                        orderedOptionsList.Add(new Option
                        {
                            ID = id,
                            Group = reader[1] as string,
                            Name = reader[2] as string,
                            Price = (decimal)reader[3],
                            Description = reader[4] as string
                        });
                }
            }
            result.OrderedOptions = orderedOptionsList.ToArray();
            return result;
        }
    }
}