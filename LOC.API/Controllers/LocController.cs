using LOC.Entities.DTOs;
using LOC.Utilities;
using LOC.Utilities.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LOC.API.Controllers
{
    [Route("api/Location")]
    [ApiController]
    public class LocController : ControllerBase
    {
        private readonly ILocationServices _locServices;
        public LocController(ILocationServices locServices)
        {

            _locServices = locServices;
        }

        [HttpGet]
        [Route("All")]
        [ProducesResponseType(typeof(IList<LocationResponseModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _locServices.Get();
            if (!result.IsSuccess)
                return StatusCode(result.statusCode, result.Message);

            return Ok(result.Data);
        }

        [HttpGet]
        [Route("{locName}")]
        [ProducesResponseType(typeof(IList<LocationResponseModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByLocationName([FromRoute] string locName)
        {
            var opts = locName.Split(' ');

            var result = await _locServices.GetBy(l => opts.Any(c => l.Name.Contains(c)));
            if (!result.IsSuccess)
                return StatusCode(result.statusCode, result.Message);

            return Ok(result.Data.Distinct());
        }

        [HttpGet]
        [Route("Open/{startTime}/{endTime}")]
        [ProducesResponseType(typeof(IList<LocationResponseModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByOpenTime([FromRoute] TimeOnly startTime, [FromRoute] TimeOnly endTime)
        {
            var result = await _locServices.GetBy(l => l.OpenTime >= startTime && l.OpenTime <= endTime);
            if (!result.IsSuccess)
                return StatusCode(result.statusCode, result.Message);

            return Ok(result.Data);
        }

        [HttpGet]
        [Route("{startTime}/{endTime}/Close")]
        [ProducesResponseType(typeof(IList<LocationResponseModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByCloseTime([FromRoute] TimeOnly startTime, [FromRoute] TimeOnly endTime)
        {
            var result = await _locServices.GetBy(l => l.CloseTime >= startTime && l.CloseTime <= endTime);
            if (!result.IsSuccess)
                return StatusCode(result.statusCode, result.Message);

            return Ok(result.Data);
        }

        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(typeof(GeneralResponseModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> StartNewGame([FromBody] LocationRequest location)
        {
            var result = await _locServices.Post(location);
            if (!result.IsSuccess)
                return StatusCode(result.statusCode, result.Message);

            return Ok(result.Data);
        }

        [HttpDelete]
        [Route("Remove/{locId}")]
        [ProducesResponseType(typeof(OperationResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> EndGame([FromRoute] LocationRemove locId)
        {
            var result = await _locServices.Delete(locId);
            if (!result.IsSuccess)
                return StatusCode(result.statusCode, result.Message);

            return Ok();
        }
    }
}
