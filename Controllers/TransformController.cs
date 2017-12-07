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
    public class TransformController : Controller
    {
        private readonly ITransformService _transformService;

        public TransformController(ITransformService transformService)
        {
            _transformService = transformService;
        }

        [HttpPut]
        public async Task<IActionResult> Get([FromQuery]int extractId,[FromBody]dynamic jsonExtract)
        {
            try{
                var dicExtract =  await _transformService.Extraction(extractId,jsonExtract);
                var loads = await _transformService.Loading(extractId,dicExtract);
            return Ok(new {dicExtract,loads});


            }
            catch(Exception ex)
            {
               return StatusCode(500, ex.Message);
            }
            
        }
        
    }
}