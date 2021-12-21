using Marqi.Data.Timers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Marqi.V1.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class TimerController : ControllerBase
    {
        private readonly TimerCollection _timers;

        public TimerController(TimerCollection timers)
        {
            _timers = timers;
        }

        [HttpGet("create")]
        public ActionResult Create([FromQuery]string name, [FromQuery]string time)
        {
            TimeSpan timeSpan;
            if(TimeSpan.TryParse(time, out timeSpan))
            {
                _timers.CreateTimer(name, timeSpan);
                _timers.Refresh();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("cancel")]
        public ActionResult Cancel([FromQuery]string name)
        {
            _timers.CancelTimer(name);
            _timers.Refresh();
            return Ok();
        }
    }
}