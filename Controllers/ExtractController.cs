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
    public class ExtractController : Controller
    {
        private readonly IExtractService _iExtractService;

        public ExtractController(IExtractService ExtractService)
        {
            _iExtractService = ExtractService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]int startat,[FromQuery]int quantity)
        {
            if (quantity == 0)
                quantity = 50;
            var extracts = await _iExtractService.getExtracts(startat,quantity);
            return Ok(extracts);

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Extract extract)
        {
            extract.extractId = 0;

            if(ModelState.IsValid)
            {
                extract = await _iExtractService.addExtract(extract);
                return Created($"api/Extract/{extract.extractId}",extract);
            }
            return BadRequest(ModelState);
        }
        
    }
}