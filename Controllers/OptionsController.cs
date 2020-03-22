using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace kfz_configurator.Controllers
{
    [ApiController]
    [Route("[controller]/{group?}")]
    public class OptionsController : ControllerBase
    {
        private readonly ILogger<OptionsController> _logger;

        public OptionsController(ILogger<OptionsController> logger)
        {
            _logger = logger;
        }

        public struct Option
        {
            public string Group { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
        }
        [HttpGet]
        public IEnumerable<Option> Get(string group)
        {
            return new Option[]{
                new Option{Group="Motor", Name="1.0 L Benzin", Price=1000},
                new Option{Group="Motor", Name="2.0 L Benzin", Price=2000},
                new Option{Group="Motor", Name="2.0 L Diesel", Price=3000},
                new Option{Group="Lackierung", Name="Alpinweiß Uni", Price=0},
                new Option{Group="Lackierung", Name="Carbonschwarz Metallic", Price=1000},
                new Option{Group="Lackierung", Name="Vergolded", Price=200000},
                new Option{Group="Scheibenwischer", Name="Nein", Price=0},
                new Option{Group="Scheibenwischer", Name="Ja", Price=1000},
            }.Where(option => string.IsNullOrEmpty(group) || option.Group == group);
        }
    }
}