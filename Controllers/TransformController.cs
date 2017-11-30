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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var loads =  await _transformService.Extraction(1,null);
            return Ok(loads);

        }
        
    }
}