using System;
using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using theworld.Models;
using theworld.ViewModels;


namespace theworld.Controllers.api
{
    [Authorize]
    [Route("api/trips")]
    public class TripController : Controller
    {
        private IWorldRepository repository;
        private ILogger<TripController> logger; 

        public TripController(IWorldRepository repository, ILogger<TripController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            var trips = repository.GetUserTripsWithStops(User.Identity.Name);
            var tripVms = Mapper.Map<IEnumerable<TripViewModel>>(trips);
            return Json(tripVms);
        }

        [HttpPost("")]
        public JsonResult Post([FromBody]TripViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newTrip = Mapper.Map<Trip>(vm);

                    newTrip.UserName = User.Identity.Name;
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