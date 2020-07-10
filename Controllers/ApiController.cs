using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VkProxy.Models;
using VkProxy.Service;

namespace VkProxy.Controllers
{
    [Route("")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IApiService _apiService;
        public ApiController(IApiService apiService)
        {
            _apiService = apiService;
        }
        [HttpPost("api/create")]
        public async Task<IActionResult> Create([FromForm]ParamModel model)
        {
            bool service = await _apiService.CheckId(model);
            return Ok(service);
        }
        [HttpPost("api/check")]
        public async Task<IActionResult> CheckName([FromForm] string Tag)
        {
            return Ok( await _apiService.GetName(Tag));
        }


        [HttpPost("api/test")]
        public async Task<IActionResult> Test([FromForm] ClubModel model)
        {
            //string service = await _apiService.CheckClub(model);
            return Ok(model);
        }
        [HttpPost("api/club")]
        public async Task<IActionResult> Club([FromForm] ClubModel model)
        {
            string service = await _apiService.CheckClub(model);
            return Ok(service);
        }
        [HttpGet("smartbot-domain-confirmation-v1-5f07119e28ce2b00016afd36.html")]
        public IActionResult Check()
        {
            return Ok("smartbot-domain-confirmation-v1-5f07119e28ce2b00016afd36");
        }
    }
}
