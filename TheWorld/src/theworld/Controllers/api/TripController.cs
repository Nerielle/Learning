using System;
using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Server.Kestrel.Http;
using Microsoft.Extensions.Logging;
using theworld.Models;
using theworld.ViewModels;

namespace theworld.Controllers.api
{
    [Route("api/trips")]
    public class TripController : Controller
    {
        private readonly IWorldRepository repository;
        private readonly ILogger<TripController> logger; 

        public TripController(IWorldRepository repository, ILogger<TripController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            var trips = Mapper.Map<IEnumerable<TripViewModel>>(repository.GetAllTripsWithStops());
            return Json(trips);
        }

        [HttpPost("")]
        public JsonResult Post([FromBody]TripViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newTrip = Mapper.Map<Trip>(vm);
                    repository.AddTrip(newTrip);
                    if (repository.SaveAll())
                    {
                        Response.StatusCode = (int) HttpStatusCode.Created;
                        return Json(Mapper.Map<TripViewModel>(newTrip));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to save new trip", ex);
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Json(new {Message = ex.Message});
            }
            Response.StatusCode = (int) HttpStatusCode.BadRequest;
            return Json("Failed");
        }
    }
}