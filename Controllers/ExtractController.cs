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

         [HttpGet("{extractId}")]
        public async Task<IActionResult> GetId(int extractId)
        {
           
            var extracts = await _iExtractService.getExtract(extractId);
            return Ok(extracts);

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Extract extract)
        {

            if(ModelState.IsValid)
            {
            extract.extractId = 0;

                extract = await _iExtractService.addExtract(extract);
                return Created($"api/Extract/{extract.extractId}",extract);
            }
            return BadRequest(ModelState);
        }

          [HttpPut]
        public async Task<IActionResult> Put([FromQuery]int extractId,[FromBody]Extract extract)
        {
            if (ModelState.IsValid)
            {

                var extracts = await _iExtractService.updateExtract(extractId,extract);
                if (extracts == null)
                {
                    return NotFound();
                }
                return Ok(extracts);
            }
            return BadRequest(ModelState);
        }

           [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int extractId)
        {
            if (extractId>0)
            {

                var extracts = await _iExtractService.deleteExtract(extractId);
                if (extracts == null)
                {
                    return NotFound();
                }
                return Ok(extracts);
            }
            return BadRequest(ModelState);
        }
        
    }
}