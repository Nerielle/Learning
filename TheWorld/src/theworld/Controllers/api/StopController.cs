using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using theworld.Models;
using theworld.Services;
using theworld.ViewModels;

namespace theworld.Controllers.api
{
    [Route("api/trips/{tripName}/stops")]
    public class StopController : Controller
    {
        private readonly ILogger<StopController> logger;
        private readonly CoordService coordService;
        private readonly IWorldRepository repository;

        public StopController(IWorldRepository repository,
            ILogger<StopController> logger,
            CoordService coordService)
        {
            this.repository = repository;
            this.logger = logger;
            this.coordService = coordService;
        }

        [HttpGet("")]
        public JsonResult Get(string tripName)
        {
            try
            {
                var trip = repository.GetTripByName(tripName);
                if (trip == null)
                {
                    return Json(null);
                }
                return Json(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(x => x.Order)));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get stops for trip {tripName}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Error occured finding trip name");
            }
        }

        public async Task<JsonResult> Post(string tripName, [FromBody] StopViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(vm);
                    var coordResult = await coordService.Lookup(newStop.Name);
                    if (!coordResult.Success)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        Json(coordResult.Message);
                    }

                    newStop.Latitude = coordResult.Latitude;
                    newStop.Longitude = coordResult.Longitude;
                    repository.AddStop(tripName, newStop);
                    if (repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<StopViewModel>(newStop));
                    }
                }
            }
            catch (Exception e)
            {
                var message = "Failed to save new stop";
                logger.LogError(message, e);
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Json(message);
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Validation failed on new stop");
        }
    }
}