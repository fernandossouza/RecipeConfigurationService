using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using recipeconfigurationservice.Model;
using recipeconfigurationservice.Services.Interfaces;

namespace recipeconfigurationservice.Controllers
{
    [Route("api/[controller]")]
    public class LoadController : Controller
    {
        private readonly ILoadService _loadService;

        public LoadController(ILoadService loadService)
        {
            _loadService = loadService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]int startat,[FromQuery]int quantity)
        {
            if (quantity == 0)
                quantity = 50;
            var loads = await _loadService.getLoads(startat,quantity);
            return Ok(loads);

        }

        [HttpGet("{loadId}")]
        public async Task<IActionResult> GetId(int loadId)
        {
           
            var load = await _loadService.getLoad(loadId);
            return Ok(load);

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Load load)
        {

            if(ModelState.IsValid)
            {
            load.loadId = 0;
                load = await _loadService.addLoad(load);
                return Created($"api/Extract/{load.loadId}",load);
            }
            return BadRequest(ModelState);
        }

          [HttpPut]
        public async Task<IActionResult> Put([FromQuery]int loadId,[FromBody]Load load)
        {
            if (ModelState.IsValid)
            {

                var loadReturn = await _loadService.updateLoad(loadId,load);
                if (loadReturn == null)
                {
                    return NotFound();
                }
                return Ok(loadReturn);
            }
            return BadRequest(ModelState);
        }

           [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int loadId)
        {
            if (loadId>0)
            {

                var loadReturn = await _loadService.deleteLoad(loadId);
                if (loadReturn == null)
                {
                    return NotFound();
                }
                return Ok(loadReturn);
            }
            return BadRequest(ModelState);
        }
        
    }
}