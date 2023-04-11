using Container.DTO.Requests;
using Container.Models;
using Katech_Task.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Katech_Task.Controllers
{
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/v1/[controller]")]
    public class TrademarksController : ControllerBase
    {
        public ILogger<TrademarksController> _logger;

        private readonly ITrademarkService _service;


        public TrademarksController(ILogger<TrademarksController> logger, ITrademarkService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Route(RoutePaths.SearchMark)]
        public async Task<IActionResult> Search([FromQuery]string word)
        {

            BaseResponse<List<Trademark>> response = new BaseResponse<List<Trademark>>();

            _logger.LogInformation("Search started and search parameters:",word);

            try
            {
                response = await _service.GetMarks(word);

                _logger.LogInformation("Response service:" + response , response.Data?.ToStringArray());
            }
            catch (Exception exp)
            {

                _logger.LogError(exp.Message, exp);
            }

            return Ok(response);
        }
    }
}
