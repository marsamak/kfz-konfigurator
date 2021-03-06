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
    public class OptionsController : ControllerBase
    {
        private readonly ILogger<OptionsController> _logger;

        public OptionsController(ILogger<OptionsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Option> Get()
        {
            using (var reader = Database.Instance.Execute(
                "SELECT option_id, group_name, name, price, description FROM options"
            ))
            {
                List<Option> options = new List<Option>();
                while (reader.Read())
                    options.Add(new Option
                    {
                        ID = (int)reader[0],
                        Group = reader[1] as string,
                        Name = reader[2] as string,
                        Price = (decimal)reader[3],
                        Description = reader[4] as string
                    });
                return options;
            }

        }
    }
}