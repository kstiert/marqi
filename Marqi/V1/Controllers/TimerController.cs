using Marqi.Data.Timers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marqi.V1.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class TimerController : ControllerBase
    {
        private readonly TimerCollection _timers;
        private readonly ILogger _logger;

        public TimerController(ILogger<TimerController> logger, TimerCollection timers)
        {
            _timers = timers;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<List<MarqiTimer>> List()
        {
            return await _timers.ListTimers();
        }

        [HttpGet("create")]
        public ActionResult Create([FromQuery]string name, [FromQuery]string time, [FromQuery]string end)
        {
            TimeSpan timeSpan;
            DateTime endTime;
            if(!string.IsNullOrEmpty(end) && DateTime.TryParse(end, out endTime))
            {
                _timers.CreateTimer(name, endTime);
            }
            else if(TimeSpan.TryParse(time, out timeSpan))
            {
                _timers.CreateTimer(name, timeSpan);
            }
            else
            {
                return BadRequest();
            }

            _ = _timers.Refresh();
            return Ok();
        }

        [HttpGet("cancel")]
        public ActionResult Cancel([FromQuery]string name)
        {
            _timers.CancelTimer(name);
            _ = _timers.Refresh();
            return Ok();
        }
    }
}